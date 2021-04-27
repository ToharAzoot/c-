using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
namespace Dto
{
public class ConnectionDto
    {
        public string UserId { get; set; }
        public string ChildId { get; set; }
        public ConnectionDto(Connections u)
        {
            UserId = u.UserId;
            ChildId = u.ChildId;
        }
        public ConnectionDto(string a,string b)
        {
            UserId = a;
            ChildId = b;
        }
        public static Connections Todal(ConnectionDto u)
        {
            return new Connections
            { 
                UserId = u.UserId,
                ChildId = u.ChildId
            };
        }
    }
}
