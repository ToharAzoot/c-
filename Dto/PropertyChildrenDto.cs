using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
namespace Dto
{
  public  class PropertyChildrenDto
    {
        public int ChildId { get; set; }
        public string ChildName { get; set; }
        public int ChildClass { get; set; }
        public string ChildGroup { get; set; }
        public bool IsComing_ { get; set; }
        public bool IsMissing_ { get; set; }

        public PropertyChildrenDto(string childId)
        {
        }
        public PropertyChildrenDto()
        {
        }
        public PropertyChildrenDto(Children po)
        {
            ChildId = po.ChildId;
            ChildName = po.ChildName;
            ChildClass = po.ChildClass;
            ChildGroup = po.ChildGroup;

        }
        public static Children ToDal(PropertyChildrenDto po)
        {
            return new Children
            {
            ChildId = po.ChildId,
            ChildName = po.ChildName,
            ChildClass = po.ChildClass,
            ChildGroup = po.ChildGroup
        };
        }
    }  
    public class Dtointint
        {
            public string id { set; get; }

            public Dtointint() { }

        }
}
