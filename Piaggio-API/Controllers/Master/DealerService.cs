using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Logger;
using AppConfig;
using Core.Module;
using BusinessLogic.Master;
using Core.Module.Master;
using Microsoft.AspNetCore.Authorization;

namespace Piaggio_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerService : ControllerBase
    {
        private readonly IConfigManager _configuration;
        private DbLogger dbLogger; BDealerService bDealerService;
        CommonIUD common = new CommonIUD(); CommonList objList;
        public DealerService(IConfigManager configuration)
        {
            this._configuration = configuration;
            dbLogger = new DbLogger(this._configuration);
            bDealerService = new BDealerService(this._configuration);
        }
        [HttpPost("PostDealerService")]
        [Authorize]
        public async Task<IActionResult> PostDealerService([FromBody] DealerServiceClass obj)
        {
            try
            {
                var t1 = Task.Run(() => bDealerService.PostDealerService(obj));
                await Task.WhenAll(t1);
                common = t1.Status == TaskStatus.RanToCompletion ? t1.Result : common;
                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(common));
            }
            catch (Exception ex)
            {
                dbLogger.PostErrorLog("DealerService", ex.Message.ToString(), "PostDealerService", 10001, "Admin", true);
                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(common));
            }
        }

        [HttpPost("GetDealerService")]
        [Authorize]
        public async Task<IActionResult> GetDealerService([FromBody] DealerServiceSearch obj)
        {
            bDealerService = new BDealerService(this._configuration);
            try
            {
                var t1 = Task.Run(() => bDealerService.GetDealerService(obj));
                await Task.WhenAll(t1);
                objList = t1.Status == TaskStatus.RanToCompletion ? t1.Result : objList;
                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(objList));
            }
            catch (Exception ex)
            {
                dbLogger.PostErrorLog("DealerService", ex.Message.ToString(), "GetDealerService", 10001, "Admin", true);
                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(objList));
            }
        }
    }
}
