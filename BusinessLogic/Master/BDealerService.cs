using AppConfig;
using BusinessLogic.Healper;
using Core.Module;
using Core.Module.Master;
using DataAccessLayer;
using DataAccessLayer.Master;
using Logger;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BusinessLogic.Master
{
   public class BDealerService
    {
        private readonly IConfigManager _configuration;
        dbDealerService dbDealerService; DbLogger dbLogger;
        CommonIUD commonIUD; dbCommon dbCommon; CommonList objList;

        public BDealerService(IConfigManager configuration)
        {
            this._configuration = configuration;
            dbDealerService = new dbDealerService(this._configuration);
            dbLogger = new DbLogger(this._configuration);
        }
        public async Task<CommonIUD> PostDealerService(DealerServiceClass obj)
        {
            commonIUD = new CommonIUD();
            var Recid = (dynamic)null;
            try
            {
                if (obj.ServiceId != null && obj.ServiceId != 0) { obj.Action = "update"; } else { obj.Action = "insert"; obj.ServiceId = 0; }
                var t1 = Task.Run(() => dbDealerService.PostdbDealerService(obj));
                await Task.WhenAll(t1);
                Recid = t1.Status == TaskStatus.RanToCompletion ? t1.Result : null;
                if (Recid == -99)
                {
                    commonIUD.FinalMode = DBReturnInsertUpdateDelete.DUPLICATE;
                    commonIUD.Message = "Record already exists !";
                }
                else if (Recid != null && Recid != 0)
                {
                    commonIUD.FinalMode = DBReturnInsertUpdateDelete.INSERT;
                    if (obj.ServiceId != null && obj.ServiceId != 0) { commonIUD.FinalMode = DBReturnInsertUpdateDelete.UPDATE; }
                    commonIUD.Recid = Recid;
                    if (obj.Action == "update") { commonIUD.Message = "Data Updated Successfully!"; } else { commonIUD.Message = "Data Inserted Successfully!"; obj.ServiceId = 0; }
                    commonIUD.AdditionalParameter = "";
                }
                else
                {
                    commonIUD.FinalMode = DBReturnInsertUpdateDelete.ERROR;
                }
                return commonIUD;
            }
            catch (Exception ex)
            {
                commonIUD.FinalMode = DBReturnInsertUpdateDelete.ERROR;
                if (ex.Message.Contains("UNIQUE KEY"))
                {
                    commonIUD.FinalMode = DBReturnInsertUpdateDelete.DUPLICATE;
                    commonIUD.Message = "Cannot insert duplicate record.";
                }
                dbLogger.PostErrorLog("BDealerService", ex.Message.ToString(), "PostDealerService", 10001, "Admin", true);
                return commonIUD;
            }
        }

        public async Task<CommonList> GetDealerService(DealerServiceSearch obj)
        {
            objList = new CommonList();
            DataTable dt;
            dbCommon = new dbCommon(this._configuration);
            try
            {
                QueryBuilder queryBuilder = new QueryBuilder();

                var t1 = Task.Run(() => queryBuilder.BuildQuerySearch(obj));
                await Task.WhenAll(t1);
                string WhereCond = t1.Status == TaskStatus.RanToCompletion ? t1.Result : null;

                var t2 = Task.Run(() => dbCommon.DynamicQuery("service", WhereCond));
                await Task.WhenAll(t2);
                dt = t2.Status == TaskStatus.RanToCompletion ? t2.Result : null;

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objList.FinalMode = DBReturnGridRecord.RecordFound;
                        objList.Data = dt;
                        objList.Count = dt.Rows.Count;
                        objList.Message = "";
                        objList.AdditionalParameter = "";
                        return objList;
                    }
                }
                return objList;
            }
            catch (Exception ex)
            {
                dbLogger.PostErrorLog("BDealerService", ex.Message.ToString(), "GetDealerService", 10001, "Admin", true);
                return objList;
            }
        }
    }
}
