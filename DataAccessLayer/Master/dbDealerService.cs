using AppConfig;
using Core.Module.Master;
using DataAccessLayer.DataAccess;
using System;
using System.Data.SqlClient;
using Logger;

namespace DataAccessLayer.Master
{
   public class dbDealerService
    {
        DbLogger dbLogger;
        private readonly AppConfig.IConfigManager _configuration;
        public dbDealerService(IConfigManager configuration)
        {
            this._configuration = configuration;
        }
        public long PostdbDealerService(DealerServiceClass obj)
        {
            DBAccess DB = new DBAccess(this._configuration);
            try
            {
                DB.Parameters.Add(new SqlParameter("@ServiceId", obj.ServiceId));
                DB.Parameters.Add(new SqlParameter("@ServiceName", obj.ServiceName.Trim()));
                DB.Parameters.Add(new SqlParameter("@Unit", obj.Unit));
                DB.Parameters.Add(new SqlParameter("@Price", obj.Price));
                DB.Parameters.Add(new SqlParameter("@IsActive", obj.IsActive));
                DB.Parameters.Add(new SqlParameter("@Action", obj.Action.ToLower()));
                return DB.ExecuteScalar("usp_Insert_Update_DealerServiceMaster");
            }
            catch (Exception ex)
            {
                dbLogger.PostErrorLog("dbDealerService", ex.Message.ToString(), "PostdbDealerService", 10001, obj.CreatedBy == null ? (obj.UpdatedBy == null ? null : Convert.ToString(obj.UpdatedBy)) : Convert.ToString(obj.CreatedBy), true);
                if (ex.Message.Contains("UNIQUE KEY"))
                {
                    //throw ex;
                    return -99;
                }
                return 0;
            }
        }
    }
}
