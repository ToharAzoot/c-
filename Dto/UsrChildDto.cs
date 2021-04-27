using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
 namespace Dto
{
    public class UsrChildDto
    {
        public string childId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UsrChildDto(Users u)
        {
            UserName = u.UserName;
            Password = u.Password;
        }
        public UsrChildDto()
        {

        }
        public static Users Todal(UsrChildDto u)
        {
            return new Users
            {
                UserName = u.UserName,
                Password = u.Password
            };
        }

    }
}
