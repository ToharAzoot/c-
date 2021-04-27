using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using Dto;

namespace SaftyChildren.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/PropertyChildren")]
    public class PropertyChildrenController : ApiController
    {
        [Route("GetAllChildren")]
        public IHttpActionResult GetAllChildren()
        {
            return Ok(Bl.PropertyChildrenBl.GetAllChildren());
        }
        [HttpGet]
        [Route("GetChild/{id}")]
        public IHttpActionResult GetChild(string id)
        {
            // 
            // return Ok(Bl.PropertyChildrenBl.GetChild(id));
            // String b = Bl.PropertyChildrenBl.GetChild(id);
            return Ok(Bl.PropertyChildrenBl.GetChild(id));
           // 
           // if (b != null)
            //   return Ok();
           // return BadRequest();
        }
        [HttpGet]
        [Route("GetUser/{id}")]
        public IHttpActionResult GetUser(string id)
        {
           // String b=
           return Ok(Bl.PropertyChildrenBl.GetUser(id));
          //  if (b!=null)
             //   return Ok();
            //return BadRequest();
        }
        [HttpGet]
        [Route("CheckIDNo/{id}")]
        public IHttpActionResult CheckIDNo(string id)
        {
            // String b=
            return Ok(Bl.PropertyChildrenBl.CheckIDNo(id));
            //  if (b!=null)
            //   return Ok();
            //return BadRequest();
        }
        [HttpGet]
        [Route("Checkifthereislike/{id}")]
        public IHttpActionResult Checkifthereislike(string id)
        {
            // String b=
            return Ok(Bl.PropertyChildrenBl.Checkifthereislike(id));
            //  if (b!=null)
            //   return Ok();
            //return BadRequest();
        }
        //[HttpPost]
        //[Route("checkID/{id}")]
        //public IHttpActionResult checkID(string id)
        //{
        // //   return Bl.PropertyChildrenBl.checkID(id);
        //}
        [HttpPost]
        [Route("Addtimecoming/{id}")]
        public IHttpActionResult Addtimecoming(int id)
        {
            bool b = Bl.PropertyChildrenBl.Addtimecoming(id);
            if (b)
                return Ok();
            return BadRequest();

        }
     
        [HttpPost]
        [Route("AddChildren")]
        public IHttpActionResult AddChildren([FromBody] PropertyChildrenDto ch)
        {
           return Ok( Bl.PropertyChildrenBl.AddChildren(ch));
          //  if (b)
          //      return Ok();
          //  return BadRequest();
        }
        [HttpPost]
        [Route("UpdateChildren")]
        public IHttpActionResult UpdateChildren([FromBody] PropertyChildrenDto ch)
        {
            bool b = Bl.PropertyChildrenBl.UpdateChildren(ch);
            if (b)
                return Ok();
            return BadRequest();
        }
        [HttpGet]
        [Route("Getallpropertyc/{id}")]
        public IHttpActionResult Getallpropertyc(string id)
        {
            return Ok(Bl.PropertyChildrenBl.Getallpropertyc(id));
        }
    }
}
