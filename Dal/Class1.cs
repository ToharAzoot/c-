using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal
{
    public class Class1
    {
        public string childId;
        public int i;

        public string ChildId { get => childId; set => childId = value; }
        public int I { get => i; set => i = value; }
        public Class1(string childId)
        {
            this.ChildId = childId;
            using (kindergardenEntities db = new kindergardenEntities())
            {
                bool? status = db.empGetChildIsMissingSp(childId).FirstOrDefault();
                if (status == true)
                    this.I = 3;
                else
                {
                    status = db.empGetChildIsComingSp(childId).FirstOrDefault();
                    if (status == false)
                        this.I = 2;
                    else
                        this.I = 1;
                }
            }
        }
        public Class1(string childId, int i)
        {
            this.ChildId = childId;
            this.I = i;
        }

    }
}
