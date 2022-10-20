using RichnessSoft.Common;
using RichnessSoft.Entity.Context;
using RichnessSoft.Entity.Model;
using RichnessSoft.Service.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static RichnessSoft.Common.LogHelper;

namespace RichnessSoft.Service.Logs
{
    public interface IerpLogService
    {
        void add(Erp_Log erpLog);
        ResultModel Insert<TEntity>(TEntity NesData) where TEntity : class;
        ResultModel Update<TEntity>(TEntity NesData, TEntity OldDate) where TEntity : class;

    }
    public class erpLogService : IerpLogService
    {
        private readonly RichnessLogDbContext _logContext;
        private ProfileStore _store;
        public erpLogService(RichnessLogDbContext logDbContext, ProfileStore store)
        {
            _logContext = logDbContext;
            _store = store;
        }
        public void add(Erp_Log erpLog)
        {
            try
            {
                SetProperties(ref erpLog);
                _logContext.Set<Erp_Log>().Add(erpLog);
                _logContext.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        public ResultModel Insert<TEntity>(TEntity NesData) where TEntity : class
        {
            ResultModel result = new ResultModel();
            try
            {
                string jData = JsonSerializer.Serialize(NesData);
                Erp_Log erp_Log = new Erp_Log();
                SetProperties(ref erp_Log);
                erp_Log.data_new = jData;
                erp_Log.logtype = gbVar.ModeInsert;
                _logContext.Set<Erp_Log>().Add(erp_Log);
                _logContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public ResultModel Update<TEntity>(TEntity NesData, TEntity OldDate) where TEntity : class
        {
            throw new NotImplementedException();
        }

        private void SetProperties(ref Erp_Log erplog)
        {
            erplog.GetType().GetProperty("createatutc", BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).SetValue(erplog, DateTime.Now);
            erplog.GetType().GetProperty("createby", BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public).SetValue(erplog, GetCurrentLoginUserId());
        }
        public string GetCurrentLoginUserId()
        {
            string result;
            try
            {
                result = _store.CurrentUser.username;
                //result = _userManager.GetUserId(_httpContextAccessor.HttpContext.user);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
