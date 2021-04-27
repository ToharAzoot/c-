using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dto;
using System.Net.Mail;
using System.Threading;
using System.IO;
using System.Data.Entity;

namespace Bl
{
    public class UserBl
    {
        public static bool Regiater(UsrDto user)
        {
            Users u = UsrDto.Todal(user);
            return UserDal.Register(u);
        }

        public static bool AddUser(UsrDto us)
        {
            using (kindergardenEntities db = new kindergardenEntities())
            {
                Users poDal = UsrDto.Todal(us);
                int id = 0;
                id += UserDal.AddUser(poDal);
                if (id != 0)
                    return true;
            }
            return false;
        }
        public static bool UpdateUser(UsrDto us)
        {
            using (kindergardenEntities db = new kindergardenEntities())
            {
                bool id = false;
                Users poDal = UsrDto.Todal(us);
                string v = us.childId;
                if (poDal.UserName != null && poDal.Password != null)
                    id = UserDal.UpdateUser(poDal, v);
                return id;

            }
            return false;
        }
        public static Users Getallpropertyd(string id)
        {
            using (kindergardenEntities db = new kindergardenEntities())
            {
                Users pass = UserDal.Getallpropertyd(id);
                return pass;
            }
        }
        // חישוב ממוצע הגעה של ילד 
        public static DateTime CalculatingTheAverageArrivalOfAchild(int IDChild)
        {
            using (kindergardenEntities db = new kindergardenEntities())
            {
                List<ArrivalChildren> arrivalChildrens = db.ArrivalChildren.Where(c => c.ChildId == IDChild).ToList();
                var count = arrivalChildrens.Count;
                double temp = 0D;
                for (int i = 0; i < count; i++)
                {
                    temp += arrivalChildrens[i].ArrivalTime.Value.Ticks / (double)count;
                }
                var average = new DateTime((long)temp);
                return average;
            }
        }
        // בדיקה אם הילגד הגיע ואם לא לשלו הודעה 
        public static bool ChackIfSendAReminderEmail()
        {
            using (kindergardenEntities db = new kindergardenEntities())
            {
                List<DailyAlerts> dailyAlerts = db.DailyAlerts.ToList();
                Children children = new Children();
                Parent parent = new Parent();
                foreach (var DA in dailyAlerts)
                {
                    // כאן צריך להוסיף עוד בדיקת תקינות האם כבר נשלח הודעה ומתי 
                    DateTime avg = CalculatingTheAverageArrivalOfAchild(DA.ChildId);
                    if (DateTime.Now.TimeOfDay > avg.TimeOfDay && DA.IsComing_ == false && DA.IsMissing_ == false)
                    {
                        List<Message> messages = db.Message.Where(m => m.IDChild == DA.ChildId && m.Date == DateTime.Today).ToList();
                        if (messages.Count() > 0)
                        {
                            DateTime minDate = DateTime.MaxValue;
                            DateTime maxDate = DateTime.MinValue;
                            // לחפש את התאריך תאריך הכי גדול 
                            foreach (var M in messages)
                            {

                                // בדיקה האם הודיעה נשלחה לפני יותר מ15 דק אפ כן נשלח הודעה שוב 
                                if (M.Date.Value.AddMinutes(15).TimeOfDay > DateTime.Now.TimeOfDay)
                                {
                                    string mailBody = File.ReadAllText(@"C:\Users\טהר אזוט\Documents\פרויקט מעון\KinderGarden\SafetyChildren-Asp-.Net-Web-Api\SaftyChildren\file\messege.html");

                                    children = db.Children.FirstOrDefault(c => c.ChildId == DA.ChildId);
                                    mailBody = mailBody.Replace("@Pname", children.Parent.First().ParentName);
                                    mailBody = mailBody.Replace("@Cname", children.ChildName);
                                    mailBody = mailBody.Replace("@IDChild", children.ChildId.ToString());

                                    //mailBody = mailBody.Replace("@Pname", "שירה ");
                                    //mailBody = mailBody.Replace("@Cname","מיכאל");
                                    sendEmail("shira4146152@gmail.com", "הילד שלכם עדין לא הגיע היום!", mailBody);
                                }
                            }
                        }
                        else
                        {
                            children = db.Children.FirstOrDefault(c => c.ChildId == DA.ChildId);
                            string mailBody = File.ReadAllText(@"C:\Users\Shira\Desktop\טוהר\SafetyChildren-Asp-.Net-Web-Api\SaftyChildren\file\messege.html");

                            //mailBody = mailBody.Replace("@Pname", children.Parent.First().ParentName);
                            //mailBody = mailBody.Replace("@Cname", children.ChildName);
                            //mailBody = mailBody.Replace("@Pname", "שירה ");
                            //mailBody = mailBody.Replace("@Cname", "מיכאל");
                            //mailBody = mailBody.Replace("@IDChild", "12");

                            //sendEmail("shira4146152@gmail.com", "try", mailBody);
                        }

                    }
                }
            }
            return true;
        }
        // שליחת מייל 
        public static void sendEmail(string email, string Subject, string Body)
        {
            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("maonemona8@gmail.com"),
                    Subject = Subject,
                    Body = Body,
                    IsBodyHtml = true
                };
                mail.To.Add(new MailAddress(email));
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("maonemona8@gmail.com", "tohar1234"),
                    EnableSsl = true
                };
                SmtpServer.Send(mail);
                //return true;
            }
            catch (Exception ex)
            {
                //return false;
            }
        }
        // אישור הגעת ילד 
        public static bool ConfirmationArrivalChild(int IdChild)
        {
            using (kindergardenEntities db = new kindergardenEntities())
            {
                DailyAlerts dailyAlerts = db.DailyAlerts.FirstOrDefault(d => d.ChildId == IdChild);
                if (dailyAlerts != null)
                {
                    // כאן יש שאלה האם זה מעדכן כל יום איך זה עובד אולי צריך לשעות שינויים בהדעת הילד שיש שם תאריך 
                    dailyAlerts.IsMissing_ = true;
                    db.Entry(dailyAlerts).State = EntityState.Modified;// האובייקט הנשלח אמור להתעדכן

                    db.SaveChanges();

                }
                return true;

            }
        }
        // פונקציה שנשלחת ע"י הלקוח פעם ביום בשעה 6 ומאפסת את אישור הגעת הילד 
        public static bool ResetDailyAlerts()
        {
            using (kindergardenEntities db = new kindergardenEntities())
            {
                List<DailyAlerts> dailyAlerts = db.DailyAlerts.ToList();
                foreach (var item in dailyAlerts)
                {
                    item.IsComing_ = false;
                    item.IsMissing_ = false;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            return true;
        }
    }
}

