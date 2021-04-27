using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
namespace Dto
{
    public class UsrDto
    {
        public string childId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UsrDto(Users u)
        {
            UserName = u.UserName;
            Password = u.Password;
        }
        public UsrDto()
        {

        }
        public static Users Todal(UsrDto u)
        {
            return new Users
            {
                UserName = u.UserName,
                Password = u.Password
            };
        }

    }
}
