using MasterDetail.Implement.Repository;
using MasterDetail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasterDetail.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public readonly IEmpMstRepository _iEmpMstRepository;
        public EmployeeController(IEmpMstRepository iEmpMstRepository)
        {
            _iEmpMstRepository = iEmpMstRepository;
        }
        public ActionResult Index()
        {
            try
            {
                return View(_iEmpMstRepository.GetAllEmp());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddEmp(int paramEmpid = 0)
        {
            try
            {
                return View(_iEmpMstRepository.GetEmpMst(paramEmpid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult GetEmpDetil(tblEmpMst paramtblEmpMst)
        {
            try
            {
                if(paramtblEmpMst.EmpMstId == 0)
                {
                    int _intMaxEmpId = 0;
                    List<tblEmpDetail> objtblEmpDetailList = paramtblEmpMst.objtblEmpDetailList;
                    if (paramtblEmpMst.objtblEmpDetailList.Count() > 0)
                        _intMaxEmpId = paramtblEmpMst.objtblEmpDetailList.Select(x => x.EmpDetId).Max();
                    objtblEmpDetailList.Add(new tblEmpDetail() { EmpDetId = _intMaxEmpId + 1 });
                    paramtblEmpMst.objtblEmpDetailList = objtblEmpDetailList;
                }
                return PartialView("AddEmpDetail", paramtblEmpMst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DeleteEmpDetil(int paramEmpDetailid, tblEmpMst paramtblEmpMst)
        {
            try
            {
                if (paramEmpDetailid > 0)
                {
                    if(paramtblEmpMst.objtblEmpDetailList.Any(x=>x.RefEmpMstid > 0))
                    {
                        tblEmpMst objtblEmpMst = _iEmpMstRepository.DeleteEmpDetail(paramEmpDetailid);
                        if(objtblEmpMst != null)
                        {
                            paramtblEmpMst = objtblEmpMst;
                        }
                    }
                    else if (paramtblEmpMst != null && paramtblEmpMst.objtblEmpDetailList != null && paramtblEmpMst.objtblEmpDetailList.Count() > 0)
                    {
                        paramtblEmpMst.objtblEmpDetailList.RemoveAt(paramEmpDetailid);
                    }
                }
                return PartialView("AddEmpDetail", paramtblEmpMst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult SaveEmp(tblEmpMst paramtblEmpMst)
        {
            try
            {
                string _strResult =  _iEmpMstRepository.SaveEmpDetail(paramtblEmpMst);
                if(_strResult == "Ok")
                {
                    return RedirectToAction("Index", "Employee");
                }
                return RedirectToAction("AddEmp", "Employee",new { paramEmpid = 0 });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult GetEmpDetailByEmpMstid(int paramEmpid)
        {
            try
            {
                return View(_iEmpMstRepository.GetEmpDetailbyEmpMstid(paramEmpid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddEmpDetail(int paramEmpid = 0)
        {
            try
            {
                return View(_iEmpMstRepository.GetEmpMst(paramEmpid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}