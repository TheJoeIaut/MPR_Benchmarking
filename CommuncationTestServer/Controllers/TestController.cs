using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CommuncationTestServer.Controllers
{    [ApiController]
    [Route("[controller]")]
    public class TestController: ControllerBase
    {
        public JsonResult  Post([FromBody]dynamic postData)  
        {  
            // Initialization  
            return new JsonResult(postData.ToString());
        }  
    }
}
