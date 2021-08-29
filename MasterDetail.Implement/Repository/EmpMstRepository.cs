using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterDetail.Model;

namespace MasterDetail.Implement.Repository
{
    public interface IEmpMstRepository
    {
        IEnumerable<tblEmpMst> GetAllEmp();
        tblEmpMst GetEmpMst(int EmpId);

        IEnumerable<tblEmpDetail> GetEmpDetailbyEmpMstid(int Empmstid);

        string SaveEmpDetail(tblEmpMst paramObjtblEmpMst);

        tblEmpMst DeleteEmpDetail(int EmpDetailId);
    }
    public class EmpMstRepository : IEmpMstRepository
    {
        private readonly MasterDetailDbContext objdbContext;
        public EmpMstRepository(MasterDetailDbContext _dbContext)
        {
            this.objdbContext = _dbContext;
        }
        public IEnumerable<tblEmpMst> GetAllEmp()
        {
            return objdbContext.tblEmpMst;
        }
        public tblEmpMst GetEmpMst(int EmpId)
        {
            tblEmpMst objtblEmpMst = objdbContext.tblEmpMst.FirstOrDefault(x => x.EmpMstId == EmpId);
            if (objtblEmpMst == null)
            {
                objtblEmpMst = new tblEmpMst();
                //List<tblEmpDetail> objtblEmpDetailList = new List<tblEmpDetail>();
                //objtblEmpDetailList.Add(new tblEmpDetail() { EmpDetId = 0 });
                //objtblEmpMst.objtblEmpDetailList = objtblEmpDetailList;
            }
            else
            {
                objtblEmpMst.objtblEmpDetailList = objdbContext.tblEmpDetail.Where(x => x.RefEmpMstid == EmpId).ToList();
            }
            return objtblEmpMst;
        }

        public IEnumerable<tblEmpDetail> GetEmpDetailbyEmpMstid(int Empmstid)
        {
            return objdbContext.tblEmpDetail.Where(x => x.RefEmpMstid == Empmstid);
        }

        public string SaveEmpDetail(tblEmpMst paramObjtblEmpMst)
        {
            if (paramObjtblEmpMst != null)
            {
                if (paramObjtblEmpMst.EmpMstId > 0)
                {
                    tblEmpMst objtblEmpMst = GetEmpMst(paramObjtblEmpMst.EmpMstId);
                    List<tblEmpDetail> objtblEmpDetailList = objtblEmpMst.objtblEmpDetailList;//GetEmpDetailbyEmpMstid(paramObjtblEmpMst.EmpMstId).ToList();
                    objtblEmpMst.EmpName = paramObjtblEmpMst.EmpName;
                    objtblEmpMst.EmpMob = paramObjtblEmpMst.EmpMob;

                    if (objtblEmpMst.objtblEmpDetailList.Count() > 0)
                    {
                        for (int i = 0; i < objtblEmpMst.objtblEmpDetailList.Count(); i++)
                        {
                            objtblEmpDetailList[i].EmpAddress = paramObjtblEmpMst.objtblEmpDetailList[i].EmpAddress;
                        }
                    }
                    IEnumerable<tblEmpDetail> objtblEmpDetailIen = paramObjtblEmpMst.objtblEmpDetailList.Where(x => x.RefEmpMstid == 0);
                    foreach (var item in objtblEmpDetailIen)
                    {
                        item.RefEmpMstid = paramObjtblEmpMst.EmpMstId;
                        objdbContext.tblEmpDetail.Add(item);
                    }
                    objdbContext.SaveChanges();
                    return "Ok";
                }
                else
                {
                    objdbContext.tblEmpMst.Add(paramObjtblEmpMst);
                    objdbContext.SaveChanges();
                    int _intEmpMstid = paramObjtblEmpMst.EmpMstId;
                    if (_intEmpMstid > 0)
                    {
                        foreach (var item in paramObjtblEmpMst.objtblEmpDetailList)
                        {
                            item.RefEmpMstid = _intEmpMstid;
                            objdbContext.tblEmpDetail.Add(item);
                        }
                    }
                    objdbContext.SaveChanges();
                    return "Ok";
                }
            }
            return "Error";
        }

        public tblEmpMst DeleteEmpDetail(int EmpDetailId)
        {
            tblEmpDetail objtblEmpDetail = objdbContext.tblEmpDetail.FirstOrDefault(x => x.EmpDetId == EmpDetailId);
            if(objtblEmpDetail != null)
            {
                objdbContext.tblEmpDetail.Remove(objtblEmpDetail);
                objdbContext.SaveChanges();
                return GetEmpMst(objtblEmpDetail.RefEmpMstid);
            }
            return null;
        }
    }
}
