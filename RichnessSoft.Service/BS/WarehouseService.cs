using Microsoft.EntityFrameworkCore;
using RichnessSoft.Entity.Context;
using RichnessSoft.Entity.Model;
using RichnessSoft.Service.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichnessSoft.Service.BS
{
    public interface IWarehouseService
    {
        ResultModel GetAll(int CorpId);
        Task<ResultModel> GetAllAsync(int CorpId);

        ResultModel GetById(int Id);
        ResultModel GetByCode(int CorpId, string Code);
        ResultModel GetByName(int CorpId, string Name);
        ResultModel Add(Warehouse wh);
        ResultModel Edit(Warehouse wh);
        ResultModel Delete(Warehouse wh);
    }
    public class WarehouseService : BaseService, IWarehouseService
    {
        private readonly RicnessDbContext _db;
        private readonly ProfileStore _store;
        public WarehouseService(RicnessDbContext db, ProfileStore store)
        {
            _db = db;
            _store = store;
        }

        public ResultModel Add(Warehouse whs)
        {
            ResultModel res = new ResultModel();
            try
            {
                using (var db = new RicnessDbContext())
                {
                    whs.createby = _store.CurrentUser.username;
                    whs.createatutc = DateTime.Now;
                    whs.companyid = _store.CurentCompany.id;
                    whs.branchid = 1;
                    db.Add(whs);
                    db.SaveChanges();
                    AddLog<Warehouse>(whs);
                    res.Success = true;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message.ToString();
            }
            return res;
        }

        public ResultModel Delete(Warehouse whs)
        {
            ResultModel res = new ResultModel();
            try
            {
                using (var db = new RicnessDbContext())
                {
                    var data = db.Warehouse.Where(x => x.id == whs.id).FirstOrDefault();
                    db.Warehouse.Remove(data);
                    DeleteLog<Warehouse>(data);
                    db.SaveChanges();
                    res.Success = true;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message.ToString();
            }
            return res;
        }

        public ResultModel Edit(Warehouse whs)
        {
            ResultModel res = new ResultModel();
            try
            {
                using (var db = new RicnessDbContext())
                {
                    var Olddata = db.Warehouse.Where(x => x.id == whs.id).FirstOrDefault();
                    whs.updateby = _store.CurrentUser.username;
                    whs.companyid = _store.CurentCompany.id;
                    whs.updateatutc = DateTime.Now;
                    db.Warehouse.Update(whs);
                    db.SaveChanges();
                    _db.Entry(whs).State = EntityState.Detached;
                    UpdateLog<Warehouse>(Olddata, whs);
                    res.Success = true;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message.ToString();
            }
            return res;
        }

        public ResultModel GetAll(int CorpId)
        {
            ResultModel res = new ResultModel();
            res.Data = _db.Warehouse.Where(x => x.companyid == CorpId).ToList();
            return res;
        }

        public async Task<ResultModel> GetAllAsync(int CorpId)
        {
            ResultModel res = new ResultModel();
            res.Data = _db.Warehouse.Where(x => x.companyid == CorpId).ToList();
            return res;
        }

        public ResultModel GetByCode(int CorpId, string Code)
        {
            ResultModel res = new ResultModel();
            res.Data = _db.Warehouse.Where(x => x.companyid == CorpId && x.code.Equals(Code)).FirstOrDefault();
            return res;
        }

        public ResultModel GetById(int Id)
        {
            ResultModel res = new ResultModel();
            res.Data = _db.Warehouse.Where(x => x.id == Id).FirstOrDefault();
            return res;
        }

        public ResultModel GetByName(int CorpId, string Name)
        {
            ResultModel res = new ResultModel();
            res.Data = _db.Warehouse.Where(x => x.companyid == CorpId && x.code.Contains(Name)).ToList();
            return res;
        }
    }
}
