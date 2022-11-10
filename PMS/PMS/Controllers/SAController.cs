using ExcelDataReader;
using Newtonsoft.Json;
using OfficeOpenXml;
using PMS.Custom_Classes;
using PMS.Functions;
using PMS.Models;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PMS.Controllers
{
    public class SAController : Controller
    {
        // GET: SA
        public ActionResult Index()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/17
        //[CustomAuthorize("/SA/ManageInstitutes")]
        public ActionResult ManageInstitutes()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/17
        //[CustomAuthorize("/SA/GetInstitutes")]
        public ActionResult GetInstitutes()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<Institute> instituteList = (from i in db.Institute orderby i.InstituteId descending select i).ToList();
                return Json(new { data = instituteList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/17
        [HttpGet]
        //[CustomAuthorize("/SA/AddOrEditInstitute/View", "/SA/AddOrEditInstitute/Add", "/SA/AddOrEditInstitute/Edit")]
        public ActionResult AddOrEditInstitute(int id = 0)
        {
            if(id == 0)
            {
                return View(new Institute());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from i in db.Institute where i.InstituteId.Equals(id) select i).FirstOrDefault<Institute>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/17
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditInstitute(Institute institute)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Institute validationRecord = (from i in db.Institute where i.InstituteCode.Equals(institute.InstituteCode) || i.InstituteName.Equals(institute.InstituteName) select i).FirstOrDefault<Institute>();

                    if (institute.InstituteId == 0)
                    {
                        if (validationRecord != null)
                        {
                            if(validationRecord.InstituteCode == institute.InstituteCode && validationRecord.InstituteName == institute.InstituteName)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Institute Code and Name Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if(validationRecord.InstituteCode == institute.InstituteCode)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Institute Code Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Institute Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        else
                        {
                            institute.CreatedBy = "Ranga";
                            institute.CreatedDate = dateTime;
                            institute.ModifiedBy = "Ranga";
                            institute.ModifiedDate = dateTime;

                            db.Institute.Add(institute);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Institute editingInstitute = (from i in db.Institute where i.InstituteId.Equals(institute.InstituteId) select i).FirstOrDefault<Institute>();

                        if (editingInstitute.InstituteCode != institute.InstituteCode || editingInstitute.InstituteName != institute.InstituteName || editingInstitute.IsActive != institute.IsActive)
                        {
                            if (validationRecord != null && validationRecord.InstituteId != institute.InstituteId)
                            {
                                if (validationRecord.InstituteCode == institute.InstituteCode && validationRecord.InstituteName == institute.InstituteName)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Institute Code and Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    if (validationRecord.InstituteCode == institute.InstituteCode)
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Institute Code Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Institute Name Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            else
                            {
                                editingInstitute.InstituteCode = institute.InstituteCode;
                                editingInstitute.InstituteName = institute.InstituteName;
                                editingInstitute.IsActive = institute.IsActive;
                                editingInstitute.ModifiedBy = "Ranga";
                                editingInstitute.ModifiedDate = dateTime;

                                db.Entry(editingInstitute).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/17
        public ActionResult ManageCampuses()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/17
        public ActionResult GetCampuses()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<Campus> campusesList = (from c in db.Campus orderby c.CampusId descending select c).ToList();
                return Json(new { data = campusesList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/17
        [HttpGet]
        public ActionResult AddOrEditCampus(int id = 0)
        {
            if (id == 0)
            {
                return View(new Campus());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from c in db.Campus where c.CampusId.Equals(id) select c).FirstOrDefault<Campus>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/17
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditCampus(Campus campus)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Campus validationRecord = (from c in db.Campus where c.CampusCode.Equals(campus.CampusCode) || c.CampusName.Equals(campus.CampusName) select c).FirstOrDefault<Campus>();

                    if (campus.CampusId == 0)
                    {
                        if (validationRecord != null)
                        {
                            if (validationRecord.CampusCode == campus.CampusCode && validationRecord.CampusName == campus.CampusName)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Campus Code and Name Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (validationRecord.CampusCode == campus.CampusCode)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Campus Code Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Campus Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        else
                        {
                            campus.CreatedBy = "Ranga";
                            campus.CreatedDate = dateTime;
                            campus.ModifiedBy = "Ranga";
                            campus.ModifiedDate = dateTime;

                            db.Campus.Add(campus);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Campus editingCampus = (from c in db.Campus where c.CampusId.Equals(campus.CampusId) select c).FirstOrDefault<Campus>();

                        if (editingCampus.CampusCode != campus.CampusCode || editingCampus.CampusName != campus.CampusName || editingCampus.IsActive != campus.IsActive)
                        {
                            if (validationRecord != null && validationRecord.CampusId != campus.CampusId)
                            {
                                if (validationRecord.CampusCode == campus.CampusCode && validationRecord.CampusName == campus.CampusName)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Campus Code and Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    if (validationRecord.CampusCode == campus.CampusCode)
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Campus Code Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Campus Name Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            else
                            {
                                editingCampus.CampusCode = campus.CampusCode;
                                editingCampus.CampusName = campus.CampusName;
                                editingCampus.IsActive = campus.IsActive;
                                editingCampus.ModifiedBy = "Ranga";
                                editingCampus.ModifiedDate = dateTime;

                                db.Entry(editingCampus).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/18
        public ActionResult ManageFaculties()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/18
        public ActionResult GetFaculties()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<FacultyVM> facultiesList = (from f in db.Faculty
                                                 join u in db.AspNetUsers on f.FacultyDean equals u.Id into f_u
                                                 from usr in f_u.DefaultIfEmpty()
                                                 join t in db.Title on usr.EmployeeTitle equals t.TitleId into u_t
                                                 from ttl in u_t.DefaultIfEmpty()
                                                 orderby f.FacultyId descending select new FacultyVM
                                                 {
                                                     FacultyId = f.FacultyId,
                                                     FacultyCode = f.FacultyCode,
                                                     FacultyName = f.FacultyName,
                                                     FacultyDean = usr != null ? ttl.TitleName + " " + usr.FirstName + " " + usr.LastName : null,
                                                     IsActive = f.IsActive
                                                 }).ToList();

                return Json(new { data = facultiesList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/18
        [HttpGet]
        public ActionResult AddOrEditFaculty(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var users = (from u in db.AspNetUsers
                             join t in db.Title on u.EmployeeTitle equals t.TitleId
                             where u.IsActive.Equals(true)
                             select new
                             {
                                 Text = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                 Value = u.Id
                             }).ToList();

                List<SelectListItem> usersList = new SelectList(users, "Value", "Text").ToList();
                usersList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                ViewBag.usersList = usersList;

                if (id == 0)
                {
                    return View(new Faculty());
                }
                else
                {
                    return View((from f in db.Faculty where f.FacultyId.Equals(id) select f).FirstOrDefault<Faculty>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/18
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditFaculty(Faculty faculty)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Faculty validationRecord = (from f in db.Faculty where f.FacultyCode.Equals(faculty.FacultyCode) || f.FacultyName.Equals(faculty.FacultyName) select f).FirstOrDefault<Faculty>();

                    if (faculty.FacultyId == 0)
                    {
                        if (validationRecord != null)
                        {
                            if (validationRecord.FacultyCode == faculty.FacultyCode && validationRecord.FacultyName == faculty.FacultyName)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Faculty Code and Name Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (validationRecord.FacultyCode == faculty.FacultyCode)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Faculty Code Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Faculty Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        else
                        {
                            faculty.CreatedBy = "Ranga";
                            faculty.CreatedDate = dateTime;
                            faculty.ModifiedBy = "Ranga";
                            faculty.ModifiedDate = dateTime;

                            db.Faculty.Add(faculty);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Faculty editingFaculty = (from f in db.Faculty where f.FacultyId.Equals(faculty.FacultyId) select f).FirstOrDefault<Faculty>();

                        if (editingFaculty.FacultyCode != faculty.FacultyCode || editingFaculty.FacultyName != faculty.FacultyName || editingFaculty.FacultyDean != faculty.FacultyDean || editingFaculty.IsActive != faculty.IsActive)
                        {
                            if (validationRecord != null && validationRecord.FacultyId != faculty.FacultyId)
                            {
                                if (validationRecord.FacultyCode == faculty.FacultyCode && validationRecord.FacultyName == faculty.FacultyName)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Faculty Code and Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    if (validationRecord.FacultyCode == faculty.FacultyCode)
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Faculty Code Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Faculty Name Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            else
                            {
                                editingFaculty.FacultyCode = faculty.FacultyCode;
                                editingFaculty.FacultyName = faculty.FacultyName;
                                editingFaculty.FacultyDean = faculty.FacultyDean;
                                editingFaculty.IsActive = faculty.IsActive;
                                editingFaculty.ModifiedBy = "Ranga";
                                editingFaculty.ModifiedDate = dateTime;

                                db.Entry(editingFaculty).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/19
        public ActionResult ManageDepartments()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/19
        public ActionResult GetDepartments()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<DepartmentVM> departmentsList = (from d in db.Department
                                                      join u in db.AspNetUsers on d.HOD equals u.Id into d_u
                                                      from usr in d_u.DefaultIfEmpty()
                                                      join t in db.Title on usr.EmployeeTitle equals t.TitleId into u_t
                                                      from ttl in u_t.DefaultIfEmpty()
                                                      join f in db.Faculty on d.FacultyId equals f.FacultyId into d_f
                                                      from fac in d_f.DefaultIfEmpty()
                                                      orderby d.DepartmentId descending
                                                      select new DepartmentVM
                                                      {
                                                          DepartmentId = d.DepartmentId,
                                                          DepartmentCode = d.DepartmentCode,
                                                          DepartmentName = d.DepartmentName,
                                                          HODName = usr != null ? ttl.TitleName + " " + usr.FirstName + " " + usr.LastName : null,
                                                          FacultyName = fac.FacultyName,
                                                          IsActive = d.IsActive
                                                      }).ToList();

                return Json(new { data = departmentsList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/19
        [HttpGet]
        public ActionResult AddOrEditDepartment(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var users = (from u in db.AspNetUsers
                             join t in db.Title on u.EmployeeTitle equals t.TitleId
                             where u.IsActive.Equals(true)
                             select new
                             {
                                 Text = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                 Value = u.Id
                             }).ToList();

                List<SelectListItem> usersList = new SelectList(users, "Value", "Text").ToList();
                usersList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                ViewBag.usersList = usersList;

                var faculties = (from f in db.Faculty
                                 where f.IsActive.Equals(true)
                                 select new
                                 {
                                     Text = f.FacultyName,
                                     Value = f.FacultyId
                                 }).ToList();

                List<SelectListItem> facultyList = new SelectList(faculties, "Value", "Text").ToList();
                facultyList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                ViewBag.facultyList = facultyList;

                if (id == 0)
                {
                    return View(new Department());
                }
                else
                {
                    return View((from d in db.Department where d.DepartmentId.Equals(id) select d).FirstOrDefault<Department>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/19
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditDepartment(Department department)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Department validationRecord = (from d in db.Department where d.DepartmentCode.Equals(department.DepartmentCode) || d.DepartmentName.Equals(department.DepartmentName) select d).FirstOrDefault<Department>();

                    if (department.DepartmentId == 0)
                    {
                        if (validationRecord != null)
                        {
                            if (validationRecord.DepartmentCode == department.DepartmentCode && validationRecord.DepartmentName == department.DepartmentName)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Department Code and Name Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (validationRecord.DepartmentCode == department.DepartmentCode)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Department Code Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Department Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        else
                        {
                            department.CreatedBy = "Ranga";
                            department.CreatedDate = dateTime;
                            department.ModifiedBy = "Ranga";
                            department.ModifiedDate = dateTime;

                            db.Department.Add(department);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Department editingDepartment = (from d in db.Department where d.DepartmentId.Equals(department.DepartmentId) select d).FirstOrDefault<Department>();

                        if (editingDepartment.DepartmentCode != department.DepartmentCode || editingDepartment.DepartmentName != department.DepartmentName 
                            || editingDepartment.HOD != department.HOD || editingDepartment.FacultyId != department.FacultyId 
                            || editingDepartment.IsActive != department.IsActive)
                        {
                            if (validationRecord != null && validationRecord.DepartmentId != department.DepartmentId)
                            {
                                if (validationRecord.DepartmentCode == department.DepartmentCode && validationRecord.DepartmentName == department.DepartmentName)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Department Code and Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    if (validationRecord.DepartmentCode == department.DepartmentCode)
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Department Code Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Department Name Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            else
                            {
                                editingDepartment.DepartmentCode = department.DepartmentCode;
                                editingDepartment.DepartmentName = department.DepartmentName;
                                editingDepartment.HOD = department.HOD;
                                editingDepartment.FacultyId = department.FacultyId;
                                editingDepartment.IsActive = department.IsActive;
                                editingDepartment.ModifiedBy = "Ranga";
                                editingDepartment.ModifiedDate = dateTime;

                                db.Entry(editingDepartment).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/19
        public ActionResult ManageLectureHalls()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/19
        public ActionResult GetLectureHalls()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<LectureHallVM> lectureHallsList = (from l in db.LectureHall
                                                        join c in db.Campus on l.CampusId equals c.CampusId
                                                        orderby l.HallId descending
                                                        select new LectureHallVM
                                                        {
                                                            HallId = l.HallId,
                                                            CampusId = c.CampusId,
                                                            CampusName = c.CampusName,
                                                            Building = l.Building,
                                                            Floor = l.Floor,
                                                            HallName = l.HallName,
                                                            IsActive = l.IsActive
                                                        }).ToList();

                return Json(new { data = lectureHallsList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/19
        [HttpGet]
        public ActionResult AddOrEditLectureHall(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var campuses = (from c in db.Campus
                                where c.IsActive.Equals(true)
                                select new
                                {
                                    Text = c.CampusName,
                                    Value = c.CampusId
                                }).ToList();

                List<SelectListItem> campusList = new SelectList(campuses, "Value", "Text").ToList();
                //campusList.Insert(0, new SelectListItem() { Text = "-- Select Campus --", Value = "", Disabled = true, Selected = true });
                ViewBag.campusList = campusList;

                if (id == 0)
                {
                    return View(new LectureHall());
                }
                else
                {
                    return View((from l in db.LectureHall where l.HallId.Equals(id) select l).FirstOrDefault<LectureHall>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/19
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditLectureHall(LectureHall lectureHall)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    LectureHall validationRecord = (from l in db.LectureHall
                                                    where l.CampusId.Equals(lectureHall.CampusId) && l.Building.Equals(lectureHall.Building)
                                                    && l.Floor.Equals(lectureHall.Floor) && l.HallName.Equals(lectureHall.HallName)
                                                    select l).FirstOrDefault<LectureHall>();

                    if (lectureHall.HallId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Lecture Hall Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            lectureHall.CreatedBy = "Ranga";
                            lectureHall.CreatedDate = dateTime;
                            lectureHall.ModifiedBy = "Ranga";
                            lectureHall.ModifiedDate = dateTime;

                            db.LectureHall.Add(lectureHall);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        LectureHall editingLectureHall = (from l in db.LectureHall where l.HallId.Equals(lectureHall.HallId) select l).FirstOrDefault<LectureHall>();

                        if (editingLectureHall.CampusId != lectureHall.CampusId || editingLectureHall.Building != lectureHall.Building
                            || editingLectureHall.Floor != lectureHall.Floor || editingLectureHall.HallName != lectureHall.HallName
                            || editingLectureHall.IsActive != lectureHall.IsActive)
                        {
                            if (validationRecord != null && validationRecord.HallId != lectureHall.HallId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Lecture Hall Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingLectureHall.CampusId = lectureHall.CampusId;
                                editingLectureHall.Building = lectureHall.Building;
                                editingLectureHall.Floor = lectureHall.Floor;
                                editingLectureHall.HallName = lectureHall.HallName;
                                editingLectureHall.IsActive = lectureHall.IsActive;
                                editingLectureHall.ModifiedBy = "Ranga";
                                editingLectureHall.ModifiedDate = dateTime;

                                db.Entry(editingLectureHall).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/22
        public ActionResult ManageUserTitles()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/22
        public ActionResult GetUserTitles()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<Title> titlesList = (from t in db.Title orderby t.TitleId descending select t).ToList();

                return Json(new { data = titlesList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/22
        [HttpGet]
        public ActionResult AddOrEditUserTitle(int id = 0)
        {
            if (id == 0)
            {
                return View(new Title());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from t in db.Title where t.TitleId.Equals(id) select t).FirstOrDefault<Title>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/22
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditUserTitle(Title title)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Title validationRecord = (from t in db.Title where t.TitleName.Equals(title.TitleName) select t).FirstOrDefault<Title>();

                    if (title.TitleId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This User Title Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            title.CreatedBy = "Ranga";
                            title.CreatedDate = dateTime;
                            title.ModifiedBy = "Ranga";
                            title.ModifiedDate = dateTime;

                            db.Title.Add(title);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Title editingTitle = (from t in db.Title where t.TitleId.Equals(title.TitleId) select t).FirstOrDefault<Title>();

                        if (editingTitle.TitleName != title.TitleName || editingTitle.Description != title.Description || editingTitle.IsActive != title.IsActive)
                        {
                            if (validationRecord != null && validationRecord.TitleId != title.TitleId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This User Title Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingTitle.TitleName = title.TitleName;
                                editingTitle.Description = title.Description;
                                editingTitle.IsActive = title.IsActive;
                                editingTitle.ModifiedBy = "Ranga";
                                editingTitle.ModifiedDate = dateTime;

                                db.Entry(editingTitle).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/17
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/08/22
        public ActionResult ManageCalendarPeriods()
        {
            return View();
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/17
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/08/22
        public ActionResult GetCalendarPeriods()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<CalendarPeriod> CalendarPeriodList = (from cp in db.CalendarPeriod orderby cp.Id descending select cp).ToList();
                return Json(new { data = CalendarPeriodList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/17
        [HttpGet]
        public ActionResult AddOrEditCalendarPeriod(int id = 0)
        {
            if (id == 0)
            {
                return View(new CalendarPeriod());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from cp in db.CalendarPeriod where cp.Id.Equals(id) select cp).FirstOrDefault<CalendarPeriod>());
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/17
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/08/22
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditCalendarPeriod(CalendarPeriod calendarPeriod)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    CalendarPeriod validationRecord = (from cp in db.CalendarPeriod where cp.PeriodName.Equals(calendarPeriod.PeriodName) select cp).FirstOrDefault<CalendarPeriod>();

                    if (calendarPeriod.Id == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Calendar Period Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            calendarPeriod.CreatedBy = "Dulanjalee";
                            calendarPeriod.CreatedDate = dateTime;
                            calendarPeriod.ModifiedBy = "Dulanjalee";
                            calendarPeriod.ModifiedDate = dateTime;

                            db.CalendarPeriod.Add(calendarPeriod);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        CalendarPeriod editingCalendarPeriod = (from cp in db.CalendarPeriod where cp.Id.Equals(calendarPeriod.Id) select cp).FirstOrDefault<CalendarPeriod>();

                        if (editingCalendarPeriod.PeriodName != calendarPeriod.PeriodName || editingCalendarPeriod.Description != calendarPeriod.Description || editingCalendarPeriod.IsActive != calendarPeriod.IsActive)
                        {
                            if (validationRecord != null && validationRecord.Id != calendarPeriod.Id)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Calendar Period Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingCalendarPeriod.PeriodName = calendarPeriod.PeriodName;
                                editingCalendarPeriod.Description = calendarPeriod.Description;
                                editingCalendarPeriod.IsActive = calendarPeriod.IsActive;
                                editingCalendarPeriod.ModifiedBy = "Dulanjalee";
                                editingCalendarPeriod.ModifiedDate = dateTime;

                                db.Entry(editingCalendarPeriod).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/18
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/08/22
        public ActionResult ManageAppointmentTypes()
        {
            return View();
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/18
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/08/22
        public ActionResult GetAppointmentTypes()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<AppointmentType> AppointmentTypeList = (from at in db.AppointmentType orderby at.AppointmentTypeId descending select at).ToList();
                return Json(new { data = AppointmentTypeList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/18
        [HttpGet]
        public ActionResult AddOrEditAppointmentType(int id = 0)
        {
            if (id == 0)
            {
                return View(new AppointmentType());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from at in db.AppointmentType where at.AppointmentTypeId.Equals(id) select at).FirstOrDefault<AppointmentType>());
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/18
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditAppointmentType(AppointmentType appointmentType)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    AppointmentType validationRecord = (from at in db.AppointmentType where at.AppointmentTypeName.Equals(appointmentType.AppointmentTypeName) select at).FirstOrDefault<AppointmentType>();

                    if (appointmentType.AppointmentTypeId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Appointment Type Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            appointmentType.CreatedBy = "Dulanjalee";
                            appointmentType.CreatedDate = dateTime;
                            appointmentType.ModifiedBy = "Dulanjalee";
                            appointmentType.ModifiedDate = dateTime;

                            db.AppointmentType.Add(appointmentType);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        AppointmentType editingAppointmentType = (from at in db.AppointmentType where at.AppointmentTypeId.Equals(appointmentType.AppointmentTypeId) select at).FirstOrDefault<AppointmentType>();

                        if (editingAppointmentType.AppointmentTypeName != appointmentType.AppointmentTypeName || editingAppointmentType.IsActive != appointmentType.IsActive)
                        {
                            if (validationRecord != null && validationRecord.AppointmentTypeId != appointmentType.AppointmentTypeId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Appointment Type Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingAppointmentType.AppointmentTypeName = appointmentType.AppointmentTypeName;
                                editingAppointmentType.IsActive = appointmentType.IsActive;
                                editingAppointmentType.ModifiedBy = "Dulanjalee";
                                editingAppointmentType.ModifiedDate = dateTime;

                                db.Entry(editingAppointmentType).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/18
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/08/22
        public ActionResult ManageLectureTypes()
        {
            return View();
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/18
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/08/22
        public ActionResult GetLectureTypes()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<LectureType> LectureTypeList = (from lt in db.LectureType orderby lt.LectureTypeId descending select lt).ToList();
                return Json(new { data = LectureTypeList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/18
        [HttpGet]
        public ActionResult AddOrEditLectureType(int id = 0)
        {
            if (id == 0)
            {
                return View(new LectureType());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from lt in db.LectureType where lt.LectureTypeId.Equals(id) select lt).FirstOrDefault<LectureType>());
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/18
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditLectureType(LectureType lectureType)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    LectureType validationRecord = (from lt in db.LectureType where lt.LectureTypeName.Equals(lectureType.LectureTypeName) select lt).FirstOrDefault<LectureType>();

                    if (lectureType.LectureTypeId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Lecture Type Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            lectureType.CreatedBy = "Dulanjalee";
                            lectureType.CreatedDate = dateTime;
                            lectureType.ModifiedBy = "Dulanjalee";
                            lectureType.ModifiedDate = dateTime;

                            db.LectureType.Add(lectureType);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        LectureType editingLectureType = (from lt in db.LectureType where lt.LectureTypeId.Equals(lectureType.LectureTypeId) select lt).FirstOrDefault<LectureType>();

                        if (editingLectureType.LectureTypeName != lectureType.LectureTypeName || editingLectureType.ConsiderMinimumStudentCount != lectureType.ConsiderMinimumStudentCount || editingLectureType.IsActive != lectureType.IsActive)
                        {
                            if (validationRecord != null && validationRecord.LectureTypeId != lectureType.LectureTypeId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Lecture Type Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingLectureType.LectureTypeName = lectureType.LectureTypeName;
                                editingLectureType.ConsiderMinimumStudentCount = lectureType.ConsiderMinimumStudentCount;
                                editingLectureType.IsActive = lectureType.IsActive;
                                editingLectureType.ModifiedBy = "Dulanjalee";
                                editingLectureType.ModifiedDate = dateTime;

                                db.Entry(editingLectureType).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/19
        public ActionResult ManageConfigurationalSettings()
        {
            return View();
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/19
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/11/02
        public ActionResult GetConfigurationalSettings()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<ConfigurationalSettingsVM> ConfigurationalSettingsList = (from c in db.ConfigurationalSettings
                                                                               join f in db.Faculty on c.FacultyId.Value equals f.FacultyId into c_f
                                                                               from fac in c_f.DefaultIfEmpty()
                                                                               orderby c.Id descending
                                                                               select new ConfigurationalSettingsVM {
                                                                                   Id = c.Id,
                                                                                   ConfigurationKey = c.ConfigurationKey,
                                                                                   IsFacultyWise = c.IsFacultyWise,
                                                                                   FacultyName = fac != null ? fac.FacultyName : null,
                                                                                   ConfigurationValue = c.ConfigurationValue,
                                                                                   IsActive = c.IsActive
                                                                               }).ToList();
                return Json(new { data = ConfigurationalSettingsList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/19
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/11/02
        [HttpGet]
        public ActionResult AddOrEditConfigurationalSetting(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var faculties = (from f in db.Faculty
                                 where f.IsActive.Equals(true)
                                 select new
                                 {
                                     Text = f.FacultyName,
                                     Value = f.FacultyId
                                 }).ToList();

                List<SelectListItem> facultyList = new SelectList(faculties, "Value", "Text").ToList();
                ViewBag.facultyList = facultyList;

                if (id == 0)
                {
                    return View(new ConfigurationalSettings());
                }
                else
                {
                    return View((from c in db.ConfigurationalSettings where c.Id.Equals(id) select c).FirstOrDefault<ConfigurationalSettings>());
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/19
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/11/02
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditConfigurationalSetting(ConfigurationalSettings configurationalSettings)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    ConfigurationalSettings validationRecord = (from c in db.ConfigurationalSettings
                                                                where c.ConfigurationKey.Equals(configurationalSettings.ConfigurationKey) && c.FacultyId.Value.Equals(configurationalSettings.FacultyId.Value)
                                                                select c).FirstOrDefault<ConfigurationalSettings>();

                    if (configurationalSettings.Id == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Configuration Key Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            configurationalSettings.CreatedBy = "Dulanjalee";
                            configurationalSettings.CreatedDate = dateTime;
                            configurationalSettings.ModifiedBy = "Dulanjalee";
                            configurationalSettings.ModifiedDate = dateTime;

                            db.ConfigurationalSettings.Add(configurationalSettings);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        ConfigurationalSettings editingConfigurationalSettings = (from c in db.ConfigurationalSettings where c.Id.Equals(configurationalSettings.Id) select c).FirstOrDefault<ConfigurationalSettings>();

                        if (editingConfigurationalSettings.ConfigurationValue != configurationalSettings.ConfigurationValue || editingConfigurationalSettings.IsActive != configurationalSettings.IsActive
                            || editingConfigurationalSettings.IsFacultyWise != configurationalSettings.IsFacultyWise || editingConfigurationalSettings.FacultyId.Value != configurationalSettings.FacultyId.Value)
                        {
                            if (validationRecord != null && validationRecord.Id != configurationalSettings.Id)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Configuration Key Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingConfigurationalSettings.IsFacultyWise = configurationalSettings.IsFacultyWise;
                                editingConfigurationalSettings.FacultyId = configurationalSettings.FacultyId.Value;
                                editingConfigurationalSettings.ConfigurationValue = configurationalSettings.ConfigurationValue;
                                editingConfigurationalSettings.IsActive = configurationalSettings.IsActive;
                                editingConfigurationalSettings.ModifiedBy = "Dulanjalee";
                                editingConfigurationalSettings.ModifiedDate = dateTime;

                                db.Entry(editingConfigurationalSettings).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/24
        public ActionResult ManageIntakes()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/24
        public ActionResult GetIntakes()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<IntakeVM> intakesList = (from i in db.Intake
                                              orderby i.IntakeId descending
                                              select new IntakeVM
                                              {
                                                  IntakeId = i.IntakeId,
                                                  IntakeYear = i.IntakeYear,
                                                  IntakeCode = i.IntakeCode,
                                                  IntakeName = i.IntakeName,
                                                  FromDate = i.FromDate.ToString().Substring(0, 10),
                                                  ToDate = i.ToDate.ToString().Substring(0, 10),
                                                  IsActive = i.IsActive
                                              }).ToList();

                return Json(new { data = intakesList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/24
        [HttpGet]
        public ActionResult AddOrEditIntake(int id = 0)
        {
            if (id == 0)
            {
                return View(new Intake());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from i in db.Intake where i.IntakeId.Equals(id) select i).FirstOrDefault<Intake>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/24
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditIntake(Intake intake)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Intake validationRecord = (from i in db.Intake
                                               where i.IntakeYear.Value.Equals(intake.IntakeYear.Value) && i.IntakeName.Equals(intake.IntakeName)
                                               select i).FirstOrDefault<Intake>();

                    if (intake.IntakeId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Intake Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            intake.CreatedBy = "Ranga";
                            intake.CreatedDate = dateTime;
                            intake.ModifiedBy = "Ranga";
                            intake.ModifiedDate = dateTime;

                            db.Intake.Add(intake);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Intake editingIntake = (from i in db.Intake where i.IntakeId.Equals(intake.IntakeId) select i).FirstOrDefault<Intake>();

                        if (editingIntake.IntakeYear != intake.IntakeYear || editingIntake.IntakeCode != intake.IntakeCode
                            || editingIntake.IntakeName != intake.IntakeName || editingIntake.FromDate != intake.FromDate
                            || editingIntake.ToDate != intake.ToDate || editingIntake.IsActive != intake.IsActive)
                        {
                            if (validationRecord != null && validationRecord.IntakeId != intake.IntakeId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Intake Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingIntake.IntakeYear = intake.IntakeYear;
                                editingIntake.IntakeCode = intake.IntakeCode;
                                editingIntake.IntakeName = intake.IntakeName;
                                editingIntake.FromDate = intake.FromDate;
                                editingIntake.ToDate = intake.ToDate;
                                editingIntake.IsActive = intake.IsActive;
                                editingIntake.ModifiedBy = "Ranga";
                                editingIntake.ModifiedDate = dateTime;

                                db.Entry(editingIntake).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/25
        public ActionResult ManageDesignations()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/25
        public ActionResult GetDesignations()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<Designation> designationsList = (from d in db.Designation orderby d.DesignationId descending select d).ToList();
                return Json(new { data = designationsList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/25
        [HttpGet]
        public ActionResult AddOrEditDesignation(int id = 0)
        {
            if (id == 0)
            {
                return View(new Designation());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from d in db.Designation where d.DesignationId.Equals(id) select d).FirstOrDefault<Designation>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/25
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditDesignation(Designation designation)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Designation validationRecord = (from d in db.Designation where d.DesignationName.Equals(designation.DesignationName) select d).FirstOrDefault<Designation>();

                    if (designation.DesignationId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Designation Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            designation.CreatedBy = "Ranga";
                            designation.CreatedDate = dateTime;
                            designation.ModifiedBy = "Ranga";
                            designation.ModifiedDate = dateTime;

                            db.Designation.Add(designation);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Designation editingDesignation = (from d in db.Designation where d.DesignationId.Equals(designation.DesignationId) select d).FirstOrDefault<Designation>();

                        if (editingDesignation.DesignationName != designation.DesignationName || editingDesignation.IsActive != designation.IsActive)
                        {
                            if (validationRecord != null && validationRecord.DesignationId != designation.DesignationId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Designation Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingDesignation.DesignationName = designation.DesignationName;
                                editingDesignation.IsActive = designation.IsActive;
                                editingDesignation.ModifiedBy = "Ranga";
                                editingDesignation.ModifiedDate = dateTime;

                                db.Entry(editingDesignation).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/25
        public ActionResult ManageAppointments()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/25
        public ActionResult GetAppointments()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<AppointmentVM> appointmentList = (from a in db.Appointment
                                                       join u in db.AspNetUsers on a.UserId equals u.Id
                                                       join t in db.Title on u.EmployeeTitle equals t.TitleId
                                                       join at in db.AppointmentType on a.AppointmentTypeId equals at.AppointmentTypeId
                                                       join d in db.Designation on a.DesignationId equals d.DesignationId
                                                       orderby a.AppointmentId descending
                                                       select new AppointmentVM
                                                       {
                                                           AppointmentId = a.AppointmentId,
                                                           EmployeeName = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                                           AppointmentTypeName = at.AppointmentTypeName,
                                                           DesignationName = d.DesignationName,
                                                           AppointmentFrom = a.AppointmentFrom.ToString().Substring(0, 10),
                                                           AppointmentTo = a.AppointmentTo.HasValue ? a.AppointmentTo.ToString().Substring(0, 10) : null,
                                                           IsActive = a.IsActive
                                                       }).ToList();

                return Json(new { data = appointmentList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/25
        [HttpGet]
        public ActionResult AddOrEditAppointment(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var users = (from u in db.AspNetUsers
                             join t in db.Title on u.EmployeeTitle equals t.TitleId
                             where u.IsActive.Equals(true)
                             select new
                             {
                                 Text = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                 Value = u.Id
                             }).ToList();

                List<SelectListItem> usersList = new SelectList(users, "Value", "Text").ToList();
                //usersList.Insert(0, new SelectListItem() { Text = "-- Select Employee --", Value = "", Disabled = true, Selected = true });
                ViewBag.usersList = usersList;

                var designations = (from d in db.Designation
                                    where d.IsActive.Equals(true)
                                    select new
                                    {
                                        Text = d.DesignationName,
                                        Value = d.DesignationId
                                    }).ToList();

                List<SelectListItem> designationList = new SelectList(designations, "Value", "Text").ToList();
                //designationList.Insert(0, new SelectListItem() { Text = "-- Select Designation --", Value = "", Disabled = true, Selected = true });
                ViewBag.designationList = designationList;

                var appointmentTypes = (from at in db.AppointmentType
                                        where at.IsActive.Equals(true)
                                        select new
                                        {
                                            Text = at.AppointmentTypeName,
                                            Value = at.AppointmentTypeId
                                        }).ToList();

                List<SelectListItem> appointmentTypeList = new SelectList(appointmentTypes, "Value", "Text").ToList();
                //appointmentTypeList.Insert(0, new SelectListItem() { Text = "-- Select Appointment Type --", Value = "", Disabled = true, Selected = true });
                ViewBag.appointmentTypeList = appointmentTypeList;

                if (id == 0)
                {
                    return View(new Appointment());
                }
                else
                {
                    return View((from a in db.Appointment where a.AppointmentId.Equals(id) select a).FirstOrDefault<Appointment>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/26
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditAppointment(Appointment appointment)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Appointment validationRecord = (from a in db.Appointment
                                                    where a.UserId.Equals(appointment.UserId) && a.DesignationId.Equals(appointment.DesignationId)
                                                    && a.AppointmentTypeId.Equals(appointment.AppointmentTypeId)
                                                    select a).FirstOrDefault<Appointment>();

                    if (appointment.AppointmentId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Designation Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var currentDate = dateTime.Date;
                            var appointmentFrom = Convert.ToDateTime(appointment.AppointmentFrom);

                            List<Appointment> activeAppointments = (from a in db.Appointment
                                                                    where a.UserId.Equals(appointment.UserId) && a.IsActive.Equals(true)
                                                                    select a).ToList();

                            for (var i = 0; i < activeAppointments.Count; i++)
                            {
                                if (!activeAppointments[i].AppointmentTo.HasValue)
                                {
                                    if(appointmentFrom >= currentDate)
                                    {
                                        activeAppointments[i].AppointmentTo = appointment.AppointmentFrom;
                                        activeAppointments[i].IsActive = true;
                                    }
                                    else
                                    {
                                        activeAppointments[i].AppointmentTo = currentDate;
                                        activeAppointments[i].IsActive = false;
                                    }
                                }
                                else
                                {
                                    if(activeAppointments[i].AppointmentTo.Value >= currentDate)
                                    {
                                        if(appointmentFrom <= activeAppointments[i].AppointmentTo.Value)
                                        {
                                            activeAppointments[i].AppointmentTo = appointment.AppointmentFrom;
                                        }
                                        activeAppointments[i].IsActive = true;
                                    }
                                    else
                                    {
                                        activeAppointments[i].IsActive = false;
                                    }
                                }

                                
                                activeAppointments[i].Comment = "Due to New Appointment Creation";
                                activeAppointments[i].ModifiedBy = "Ranga";
                                activeAppointments[i].ModifiedDate = dateTime;
                                db.Entry(activeAppointments[i]).State = EntityState.Modified;
                            }

                            appointment.CreatedBy = "Ranga";
                            appointment.CreatedDate = dateTime;
                            appointment.ModifiedBy = "Ranga";
                            appointment.ModifiedDate = dateTime;

                            db.Appointment.Add(appointment);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Appointment editingAppointment = (from a in db.Appointment where a.AppointmentId.Equals(appointment.AppointmentId) select a).FirstOrDefault<Appointment>();

                        if (editingAppointment.DesignationId != appointment.DesignationId || editingAppointment.AppointmentTypeId != appointment.AppointmentTypeId 
                            || editingAppointment.AppointmentFrom != appointment.AppointmentFrom || editingAppointment.AppointmentTo != appointment.AppointmentTo 
                            || editingAppointment.IsActive != appointment.IsActive)
                        {
                            if (validationRecord != null && validationRecord.AppointmentId != appointment.AppointmentId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Designation Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingAppointment.DesignationId = appointment.DesignationId;
                                editingAppointment.AppointmentTypeId = appointment.AppointmentTypeId;
                                editingAppointment.AppointmentFrom = appointment.AppointmentFrom;
                                editingAppointment.AppointmentTo = appointment.AppointmentTo;
                                editingAppointment.IsActive = appointment.IsActive;
                                editingAppointment.ModifiedBy = "Ranga";
                                editingAppointment.ModifiedDate = dateTime;

                                db.Entry(editingAppointment).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/24
        public ActionResult ManageSubject()
        {
            return View();
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/24
        public ActionResult GetSubject()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<SubjectVM> subjectList = (from s in db.Subject
                                               join d in db.Degree on s.DegreeId equals d.DegreeId into s_d
                                               from deg in s_d.DefaultIfEmpty()
                                               orderby s.SubjectId descending
                                               select new SubjectVM {
                                                   SubjectId = s.SubjectId,
                                                   SubjectCode = s.SubjectCode,
                                                   SubjectName = s.SubjectName,
                                                   DegreeOrCommon = s.IsCommon == true ? "-- Common Subject --" : deg.Name,
                                                   IsActive = s.IsActive
                                               }).ToList();

                return Json(new { data = subjectList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/24
        [HttpGet]
        public ActionResult AddOrEditSubject(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var degrees = (from d in db.Degree
                               where d.IsActive.Equals(true)
                               select new
                               {
                                   Text = d.Name,
                                   Value = d.DegreeId
                               }).ToList();

                List<SelectListItem> degreeList = new SelectList(degrees, "Value", "Text").ToList();
                ViewBag.degreeList = degreeList;

                if (id == 0)
                {
                    return View(new Subject());
                }
                else
                {
                    return View((from s in db.Subject where s.SubjectId.Equals(id) select s).FirstOrDefault<Subject>());
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/24
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditSubject(Subject subject)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Subject validationRecord = (from s in db.Subject where s.SubjectCode.Equals(subject.SubjectCode) || s.SubjectName.Equals(subject.SubjectName) select s).FirstOrDefault<Subject>();

                    if (subject.SubjectId == 0)
                    {
                        if (validationRecord != null)
                        {
                            if (validationRecord.SubjectCode == subject.SubjectCode && validationRecord.SubjectName == subject.SubjectName)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Subject Code and Name Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (validationRecord.SubjectCode == subject.SubjectCode)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Subject Code Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Subject Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        else
                        {
                            subject.CreatedBy = "Dulanjalee";
                            subject.CreatedDate = dateTime;
                            subject.ModifiedBy = "Dulanjalee";
                            subject.ModifiedDate = dateTime;

                            db.Subject.Add(subject);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Subject editingSubject = (from i in db.Subject where i.SubjectId.Equals(subject.SubjectId) select i).FirstOrDefault<Subject>();

                        if (editingSubject.SubjectCode != subject.SubjectCode || editingSubject.SubjectName != subject.SubjectName 
                            || editingSubject.IsCommon != subject.IsCommon || editingSubject.DegreeId != subject.DegreeId || editingSubject.IsActive != subject.IsActive)
                        {
                            if (validationRecord != null && validationRecord.SubjectId != subject.SubjectId)
                            {
                                if (validationRecord.SubjectCode == subject.SubjectCode && validationRecord.SubjectName == subject.SubjectName)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Subject Code and Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    if (validationRecord.SubjectCode == subject.SubjectCode)
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Subject Code Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Subject Name Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            else
                            {
                                editingSubject.SubjectCode = subject.SubjectCode;
                                editingSubject.SubjectName = subject.SubjectName;
                                editingSubject.IsCommon = subject.IsCommon;
                                editingSubject.DegreeId = subject.DegreeId;
                                editingSubject.IsActive = subject.IsActive;
                                editingSubject.ModifiedBy = "Dulanjalee";
                                editingSubject.ModifiedDate = dateTime;

                                db.Entry(editingSubject).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/24
        public ActionResult ManageDegree()
        {
            return View();
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/24
        public ActionResult GetDegree()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<DegreeVM> degreeList = (from d in db.Degree
                                             join f in db.Faculty on d.FacultyId equals f.FacultyId into d_f
                                             from fac in d_f.DefaultIfEmpty()
                                             join i in db.Institute on d.InstituteId equals i.InstituteId into d_i
                                             from ins in d_i.DefaultIfEmpty()
                                             join dp in db.Department on d.DepartmentId equals dp.DepartmentId into d_dp
                                             from dep in d_dp.DefaultIfEmpty()
                                             orderby d.DegreeId descending
                                             select new DegreeVM
                                             {
                                                 DegreeId = d.DegreeId,
                                                 Code = d.Code,
                                                 Name = d.Name,
                                                 Description = d.Description,
                                                 FacultyId = fac.FacultyId,
                                                 FacultyName = fac.FacultyName,
                                                 InstituteId = ins.InstituteId,
                                                 InstituteName = ins.InstituteName,
                                                 DepartmentId = dep.DepartmentId,
                                                 DepartmentName = dep.DepartmentName,
                                                 IsActive = d.IsActive
                                             }).ToList();

                return Json(new { data = degreeList }, JsonRequestBehavior.AllowGet);
            }
        }


        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/24
        [HttpGet]
        public ActionResult AddOrEditDegree(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var faculty = (from f in db.Faculty
                             where f.IsActive.Equals(true)
                             select new
                             {
                                 Text = f.FacultyName,
                                 Value = f.FacultyId
                             }).ToList();

                List<SelectListItem> facultyList = new SelectList(faculty, "Value", "Text").ToList();
                //facultyList.Insert(0, new SelectListItem() { Text = "-- Select Faculty --", Value = "", Disabled = true, Selected = true });
                ViewBag.facultyList = facultyList;

                var institute = (from i in db.Institute
                                 where i.IsActive.Equals(true)
                                 select new
                                 {
                                     Text = i.InstituteName,
                                     Value = i.InstituteId
                                 }).ToList();

                List<SelectListItem> instituteList = new SelectList(institute, "Value", "Text").ToList();
                //instituteList.Insert(0, new SelectListItem() { Text = "-- Select Institute --", Value = "", Disabled = true, Selected = true });
                ViewBag.instituteList = instituteList;

                var department = (from d in db.Department
                                 where d.IsActive.Equals(true)
                                 select new
                                 {
                                     Text = d.DepartmentName,
                                     Value = d.DepartmentId
                                 }).ToList();

                List<SelectListItem> departmentList = new SelectList(department, "Value", "Text").ToList();
                departmentList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                ViewBag.departmentList = departmentList;

                if (id == 0)
                {
                    return View(new Degree());
                }
                else
                {
                    return View((from d in db.Degree where d.DegreeId.Equals(id) select d).FirstOrDefault<Degree>());
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/24
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditDegree(Degree degree)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Degree validationRecord = (from d in db.Degree where d.Code.Equals(degree.Code) || d.Name.Equals(degree.Name) select d).FirstOrDefault<Degree>();

                    if (degree.DegreeId == 0)
                    {
                        if (validationRecord != null)
                        {
                            if (validationRecord.Code == degree.Code && validationRecord.Name == degree.Name)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Degree Code and Name Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (validationRecord.Code == degree.Code)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Degree Code Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Degree Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        else
                        {
                            degree.CreatedBy = "Dulanjalee";
                            degree.CreatedDate = dateTime;
                            degree.ModifiedBy = "Dulanjalee";
                            degree.ModifiedDate = dateTime;

                            db.Degree.Add(degree);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Degree editingDegree = (from d in db.Degree where d.DegreeId.Equals(degree.DegreeId) select d).FirstOrDefault<Degree>();

                        if (editingDegree.Code != degree.Code || editingDegree.Name != degree.Name || editingDegree.Description != degree.Description || editingDegree.FacultyId != degree.FacultyId || editingDegree.InstituteId != degree.InstituteId || editingDegree.DepartmentId != degree.DepartmentId || editingDegree.IsActive != degree.IsActive)
                        {
                            if (validationRecord != null && validationRecord.DegreeId != degree.DegreeId)
                            {
                                if (validationRecord.Code == degree.Code && validationRecord.Name == degree.Name)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Degree Code and Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    if (validationRecord.Code == degree.Code)
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Degree Code Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Degree Name Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            else
                            {
                                editingDegree.Code = degree.Code;
                                editingDegree.Name = degree.Name;
                                editingDegree.Description = degree.Description;
                                editingDegree.FacultyId = degree.FacultyId;
                                editingDegree.InstituteId = degree.InstituteId;
                                editingDegree.DepartmentId = degree.DepartmentId;
                                editingDegree.IsActive = degree.IsActive;
                                editingDegree.ModifiedBy = "Dulanjalee";
                                editingDegree.ModifiedDate = dateTime;

                                db.Entry(editingDegree).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/25
        public ActionResult ManageSpecialization()
        {
            return View();
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/25
        public ActionResult GetSpecialization()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<SpecializationVM> specializationList = (from s in db.Specialization
                                                             join d in db.Degree on s.DegreeId equals d.DegreeId into s_d
                                                             from deg in s_d.DefaultIfEmpty()
                                                             join i in db.Institute on s.InstituteId equals i.InstituteId into s_i
                                                             from ins in s_i.DefaultIfEmpty()
                                                             join dp in db.Department on s.DepartmentId equals dp.DepartmentId into s_dp
                                                             from dep in s_dp.DefaultIfEmpty()
                                                             orderby s.SpecializationId descending
                                                             select new SpecializationVM
                                                             {
                                                             SpecializationId = s.SpecializationId,
                                                             Code = s.Code,
                                                             Name = s.Name,
                                                             DegreeId=deg.DegreeId,
                                                             DegreeName = deg.Name,
                                                             InstituteId = ins.InstituteId,
                                                             InstituteName = ins.InstituteName,
                                                             DepartmentId = dep.DepartmentId,
                                                             DepartmentName = dep.DepartmentName,
                                                             IsActive = s.IsActive
                                                             }).ToList();

                return Json(new { data = specializationList }, JsonRequestBehavior.AllowGet);
            }
        }


        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/25
        [HttpGet]
        public ActionResult AddOrEditSpecialization(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var degree = (from d in db.Degree
                               where d.IsActive.Equals(true)
                               select new
                               {
                                   Text = d.Name,
                                   Value = d.DegreeId
                               }).ToList();

                List<SelectListItem> degreeList = new SelectList(degree, "Value", "Text").ToList();
                //degreeList.Insert(0, new SelectListItem() { Text = "-- Select Degree --", Value = "", Disabled = true, Selected = true });
                ViewBag.degreeList = degreeList;

                var institute = (from i in db.Institute
                                 where i.IsActive.Equals(true)
                                 select new
                                 {
                                     Text = i.InstituteName,
                                     Value = i.InstituteId
                                 }).ToList();

                List<SelectListItem> instituteList = new SelectList(institute, "Value", "Text").ToList();
                //instituteList.Insert(0, new SelectListItem() { Text = "-- Select Institute --", Value = "", Disabled = true, Selected = true });
                ViewBag.instituteList = instituteList;

                var department = (from d in db.Department
                                  where d.IsActive.Equals(true)
                                  select new
                                  {
                                      Text = d.DepartmentName,
                                      Value = d.DepartmentId
                                  }).ToList();

                List<SelectListItem> departmentList = new SelectList(department, "Value", "Text").ToList();
                departmentList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                ViewBag.departmentList = departmentList;

                if (id == 0)
                {
                    return View(new Specialization());
                }
                else
                {
                    return View((from s in db.Specialization where s.SpecializationId.Equals(id) select s).FirstOrDefault<Specialization>());
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/25
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditSpecialization(Specialization specialization)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Specialization validationRecord = (from s in db.Specialization where s.Code.Equals(specialization.Code) || s.Name.Equals(specialization.Name) select s).FirstOrDefault<Specialization>();

                    if (specialization.SpecializationId == 0)
                    {
                        if (validationRecord != null)
                        {
                            if (validationRecord.Code == specialization.Code && validationRecord.Name == specialization.Name)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Specialization Code and Name Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (validationRecord.Code == specialization.Code)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Specialization Code Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Specialization Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        else
                        {
                            specialization.CreatedBy = "Dulanjalee";
                            specialization.CreatedDate = dateTime;
                            specialization.ModifiedBy = "Dulanjalee";
                            specialization.ModifiedDate = dateTime;

                            db.Specialization.Add(specialization);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Specialization editingSpecialization = (from s in db.Specialization where s.SpecializationId.Equals(specialization.SpecializationId) select s).FirstOrDefault<Specialization>();

                        if (editingSpecialization.Code != specialization.Code || editingSpecialization.Name != specialization.Name || editingSpecialization.DegreeId != specialization.DegreeId || editingSpecialization.InstituteId != specialization.InstituteId || editingSpecialization.DepartmentId != specialization.DepartmentId || editingSpecialization.IsActive != specialization.IsActive)
                        {
                            if (validationRecord != null && validationRecord.SpecializationId != specialization.SpecializationId)
                            {
                                if (validationRecord.Code == specialization.Code && validationRecord.Name == specialization.Name)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "This Specialization Code and Name Already Exists"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    if (validationRecord.Code == specialization.Code)
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Specialization Code Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "This Specialization Name Already Exists"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            else
                            {
                                editingSpecialization.Code = specialization.Code;
                                editingSpecialization.Name = specialization.Name;
                                editingSpecialization.DegreeId = specialization.DegreeId;
                                editingSpecialization.InstituteId = specialization.InstituteId;
                                editingSpecialization.DepartmentId = specialization.DepartmentId;
                                editingSpecialization.IsActive = specialization.IsActive;
                                editingSpecialization.ModifiedBy = "Dulanjalee";
                                editingSpecialization.ModifiedDate = dateTime;

                                db.Entry(editingSpecialization).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }


        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/25
        public ActionResult ManagePaymentRate()
        {
            return View();
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/25
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/09/03
        public ActionResult GetPaymentRate()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<PaymentRateVM> paymentRateList = (from pr in db.PaymentRate
                                                       join ds in db.Designation on pr.DesignationId equals ds.DesignationId
                                                       join f in db.Faculty on pr.FacultyId equals f.FacultyId into pr_f
                                                       from fac in pr_f.DefaultIfEmpty()
                                                       join d in db.Degree on pr.DegreeId equals d.DegreeId into pr_d
                                                       from deg in pr_d.DefaultIfEmpty()
                                                       join sp in db.Specialization on pr.SpecializationId equals sp.SpecializationId into pr_sp
                                                       from spc in pr_sp.DefaultIfEmpty()
                                                       join s in db.Subject on pr.SubjectId equals s.SubjectId into pr_s
                                                       from sub in pr_s.DefaultIfEmpty()
                                                       orderby pr.Id descending
                                                       select new PaymentRateVM
                                                       {
                                                           Id = pr.Id,
                                                           DegreeName = deg.Name,
                                                           FacultyName = fac.FacultyName,
                                                           SubjectName = sub.SubjectName,
                                                           SpecializationName = spc.Name,
                                                           DesignationName = ds.DesignationName,
                                                           IsApproved = pr.IsApproved,
                                                           IsActive = pr.IsActive
                                                       }).ToList();

                return Json(new { data = paymentRateList }, JsonRequestBehavior.AllowGet);
            }
        }


        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/31
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/09/03
        [HttpGet]
        public ActionResult AddOrEditPaymentRate(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var designation = (from ds in db.Designation
                                   where ds.IsActive.Equals(true)
                                   select new
                                   {
                                       Text = ds.DesignationName,
                                       Value = ds.DesignationId
                                   }).ToList();

                List<SelectListItem> designationList = new SelectList(designation, "Value", "Text").ToList();
                //designationList.Insert(0, new SelectListItem() { Text = "-- Select Designation --", Value = "", Disabled = true, Selected = true });
                ViewBag.designationList = designationList;

                var faculty = (from f in db.Faculty
                               where f.IsActive.Equals(true)
                               select new
                               {
                                   Text = f.FacultyName,
                                   Value = f.FacultyId
                               }).ToList();

                List<SelectListItem> facultyList = new SelectList(faculty, "Value", "Text").ToList();
                facultyList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                ViewBag.facultyList = facultyList;

                var subject = (from s in db.Subject
                               where s.IsActive.Equals(true)
                               select new
                               {
                                   Text = s.SubjectName,
                                   Value = s.SubjectId
                               }).ToList();

                List<SelectListItem> subjectList = new SelectList(subject, "Value", "Text").ToList();
                subjectList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                ViewBag.subjectList = subjectList;

                if (id == 0)
                {
                    List<SelectListItem> degreeList = new List<SelectListItem>();
                    degreeList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                    ViewBag.degreeList = degreeList;

                    List<SelectListItem> specializationList = new List<SelectListItem>();
                    specializationList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                    ViewBag.specializationList = specializationList;

                    return View(new PaymentRate());
                }
                else
                {
                    PaymentRate editingPaymentRate = (from pr in db.PaymentRate where pr.Id.Equals(id) select pr).FirstOrDefault<PaymentRate>();

                    if(editingPaymentRate.FacultyId != null)
                    {
                        var degrees = (from d in db.Degree
                                       where d.FacultyId == editingPaymentRate.FacultyId && d.IsActive.Equals(true)
                                       select new
                                       {
                                           Text = d.Name,
                                           Value = d.DegreeId
                                       }).ToList();

                        List<SelectListItem> degreeList = new SelectList(degrees, "Value", "Text").ToList();
                        degreeList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                        ViewBag.degreeList = degreeList;

                        if(editingPaymentRate.DegreeId != null)
                        {
                            var specializations = (from s in db.Specialization
                                                   where s.DegreeId == editingPaymentRate.DegreeId && s.IsActive.Equals(true)
                                                   select new
                                                   {
                                                       Text = s.Name,
                                                       Value = s.SpecializationId
                                                   }).ToList();

                            List<SelectListItem> specializationList = new SelectList(specializations, "Value", "Text").ToList();
                            specializationList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                            ViewBag.specializationList = specializationList;
                        }
                        else
                        {
                            List<SelectListItem> specializationList = new List<SelectListItem>();
                            specializationList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                            ViewBag.specializationList = specializationList;
                        }
                    }
                    else
                    {
                        List<SelectListItem> degreeList = new List<SelectListItem>();
                        degreeList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                        ViewBag.degreeList = degreeList;

                        List<SelectListItem> specializationList = new List<SelectListItem>();
                        specializationList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                        ViewBag.specializationList = specializationList;
                    }

                    return View(editingPaymentRate);
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/26
        [HttpGet]
        public ActionResult GetDegreesByFaculty(int id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var degrees = (from d in db.Degree
                               where d.FacultyId.Value == id && d.IsActive.Equals(true)
                               select new
                               {
                                   Text = d.Name,
                                   Value = d.DegreeId
                               }).ToList();

                return Json(degrees, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/31
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/09/03
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditPaymentRate(PaymentRate paymentRate)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    PaymentRate validationRecord = (from pr in db.PaymentRate
                                                    where pr.DesignationId.Equals(paymentRate.DesignationId) && pr.FacultyId.Value.Equals(paymentRate.FacultyId.Value)
                                                    && pr.DegreeId.Value.Equals(paymentRate.DegreeId.Value) && pr.SpecializationId.Value.Equals(paymentRate.SpecializationId.Value)
                                                    && pr.SubjectId.Value.Equals(paymentRate.SubjectId.Value)
                                                    select pr).FirstOrDefault<PaymentRate>();

                    if (paymentRate.Id == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This PaymentRate Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            paymentRate.IsApproved = false;
                            paymentRate.CreatedBy = "Dulanjalee";
                            paymentRate.CreatedDate = dateTime;
                            paymentRate.ModifiedBy = "Dulanjalee";
                            paymentRate.ModifiedDate = dateTime;

                            db.PaymentRate.Add(paymentRate);

                            PaymentRateLog prLog = new PaymentRateLog();

                            prLog.DesignationId = paymentRate.DesignationId;
                            prLog.FacultyId = paymentRate.FacultyId;
                            prLog.DegreeId = paymentRate.DegreeId;
                            prLog.SpecializationId = paymentRate.SpecializationId;
                            prLog.SubjectId = paymentRate.SubjectId;
                            prLog.RatePerHour = paymentRate.RatePerHour;
                            prLog.IsActive = paymentRate.IsActive;
                            prLog.IsApproved = false;
                            prLog.CreatedBy = "Ranga";
                            prLog.CreatedDate = dateTime;
                            prLog.ModifiedBy = "Ranga";
                            prLog.ModifiedDate = dateTime;
                            prLog.PaymentRateId = paymentRate.Id;

                            db.PaymentRateLog.Add(prLog);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        PaymentRate editingPaymentRate = (from pr in db.PaymentRate where pr.Id.Equals(paymentRate.Id) select pr).FirstOrDefault<PaymentRate>();

                        if (editingPaymentRate.DesignationId != paymentRate.DesignationId || editingPaymentRate.FacultyId != paymentRate.FacultyId
                            || editingPaymentRate.DegreeId != paymentRate.DegreeId || editingPaymentRate.SpecializationId != paymentRate.SpecializationId 
                            || editingPaymentRate.SubjectId != paymentRate.SubjectId || editingPaymentRate.RatePerHour != paymentRate.RatePerHour || editingPaymentRate.IsActive != paymentRate.IsActive)
                        {
                            if (validationRecord != null && validationRecord.Id != paymentRate.Id)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This PaymentRate Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingPaymentRate.DesignationId = paymentRate.DesignationId;
                                editingPaymentRate.FacultyId = paymentRate.FacultyId;
                                editingPaymentRate.DegreeId = paymentRate.DegreeId;
                                editingPaymentRate.SpecializationId = paymentRate.SpecializationId;
                                editingPaymentRate.SubjectId = paymentRate.SubjectId;
                                editingPaymentRate.RatePerHour = paymentRate.RatePerHour;
                                editingPaymentRate.IsActive = paymentRate.IsActive;
                                editingPaymentRate.ModifiedBy = "Dulanjalee";
                                editingPaymentRate.ModifiedDate = dateTime;

                                db.Entry(editingPaymentRate).State = EntityState.Modified;

                                PaymentRateLog prLog = new PaymentRateLog();

                                prLog.DesignationId = paymentRate.DesignationId;
                                prLog.FacultyId = paymentRate.FacultyId;
                                prLog.DegreeId = paymentRate.DegreeId;
                                prLog.SpecializationId = paymentRate.SpecializationId;
                                prLog.SubjectId = paymentRate.SubjectId;
                                prLog.RatePerHour = paymentRate.RatePerHour;
                                prLog.IsActive = paymentRate.IsActive;
                                prLog.IsApproved = editingPaymentRate.IsApproved;
                                prLog.CreatedBy = editingPaymentRate.CreatedBy;
                                prLog.CreatedDate = editingPaymentRate.CreatedDate;
                                prLog.ModifiedBy = "Ranga";
                                prLog.ModifiedDate = dateTime;
                                prLog.PaymentRateId = editingPaymentRate.Id;

                                db.PaymentRateLog.Add(prLog);
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/03
        public ActionResult ManageSemesterRegistrations()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/05
        public ActionResult GetSemesterRegistrations()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<SemesterRegistrationVM> semesterRegistrationList = (from sr in db.SemesterRegistration
                                                                         join cp in db.CalendarPeriod on sr.CalendarPeriodId equals cp.Id into sr_cp
                                                                         from calP in sr_cp.DefaultIfEmpty()
                                                                         join it in db.Intake on sr.IntakeId equals it.IntakeId into sr_it
                                                                         from intk in sr_it.DefaultIfEmpty()
                                                                         join f in db.Faculty on sr.FacultyId equals f.FacultyId into sr_f
                                                                         from fac in sr_f.DefaultIfEmpty()
                                                                         join ins in db.Institute on sr.InstituteId equals ins.InstituteId into sr_ins
                                                                         from inst in sr_ins.DefaultIfEmpty()
                                                                         join d in db.Degree on sr.DegreeId equals d.DegreeId into sr_d
                                                                         from dg in sr_d.DefaultIfEmpty()
                                                                         join sp in db.Specialization on sr.SpecializationId equals sp.SpecializationId into sr_sp
                                                                         from splz in sr_sp.DefaultIfEmpty()
                                                                         orderby sr.SemesterId descending
                                                                         select new SemesterRegistrationVM
                                                                         {
                                                                             SemesterId = sr.SemesterId,
                                                                             CalendarYear = sr.CalendarYear,
                                                                             CalendarPeriodName = calP.PeriodName,
                                                                             IntakeYear = intk.IntakeYear,
                                                                             IntakeName = intk.IntakeName,
                                                                             AcademicYear = sr.AcademicYear,
                                                                             AcademicSemester = sr.AcademicSemester,
                                                                             FacultyName = fac.FacultyName,
                                                                             InstituteName = inst.InstituteName,
                                                                             DegreeName = dg.Name,
                                                                             SpecializationName = splz.Name,
                                                                             FromDate = sr.FromDate.Value.ToString().Substring(0, 10),
                                                                             ToDate = sr.ToDate.Value.ToString().Substring(0, 10),
                                                                             IsActive = sr.IsActive
                                                                         }).ToList();

                return Json(new { data = semesterRegistrationList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/25
        [HttpGet]
        public ActionResult AddOrEditSemesterRegistration(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var calendarPeriods = (from cp in db.CalendarPeriod
                                       where cp.IsActive.Equals(true)
                                       select new
                                       {
                                           Text = cp.PeriodName,
                                           Value = cp.Id
                                       }).ToList();

                List<SelectListItem> calendarPeriodList = new SelectList(calendarPeriods, "Value", "Text").ToList();
                //calendarPeriodList.Insert(0, new SelectListItem() { Text = "-- Select Calendar Period --", Value = "", Disabled = true, Selected = true });
                ViewBag.calendarPeriodList = calendarPeriodList;

                var intakes = (from i in db.Intake
                               where i.IsActive.Equals(true)
                               select new {
                                   Text = i.IntakeYear,
                                   Value = i.IntakeYear
                               }).Distinct().OrderBy(i => i.Value).ToList();

                List<SelectListItem> intakeYearList = new SelectList(intakes, "Value", "Text").ToList();
                //intakeYearList.Insert(0, new SelectListItem() { Text = "-- Select Intake Year --", Value = "", Disabled = true, Selected = true });
                ViewBag.intakeYearList = intakeYearList;

                var faculties = (from f in db.Faculty
                                 where f.IsActive.Equals(true)
                                 select new
                                 {
                                     Text = f.FacultyName,
                                     Value = f.FacultyId
                                 }).ToList();

                List<SelectListItem> facultyList = new SelectList(faculties, "Value", "Text").ToList();
                //facultyList.Insert(0, new SelectListItem() { Text = "-- Select Faculty --", Value = "", Disabled = true, Selected = true });
                ViewBag.facultyList = facultyList;

                var institutes = (from i in db.Institute
                                  where i.IsActive.Equals(true)
                                  select new
                                  {
                                      Text = i.InstituteName,
                                      Value = i.InstituteId
                                  }).ToList();

                List<SelectListItem> instituteList = new SelectList(institutes, "Value", "Text").ToList();
                //instituteList.Insert(0, new SelectListItem() { Text = "-- Select Awarding Institute --", Value = "", Disabled = true, Selected = true });
                ViewBag.instituteList = instituteList;

                if (id == 0)
                {
                    List<SelectListItem> intakeList = new List<SelectListItem>();
                    //intakeList.Insert(0, new SelectListItem() { Text = "-- Select Intake --", Value = "", Disabled = true, Selected = true });
                    ViewBag.intakeList = intakeList;

                    List<SelectListItem> degreeList = new List<SelectListItem>();
                    //degreeList.Insert(0, new SelectListItem() { Text = "-- Select Degree --", Value = "", Disabled = true, Selected = true });
                    ViewBag.degreeList = degreeList;

                    List<SelectListItem> specializationList = new List<SelectListItem>();
                    specializationList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                    ViewBag.specializationList = specializationList;

                    return View(new SemesterRegistration());
                }
                else
                {
                    SemesterRegistration editingSemesterRegistration = (from sr in db.SemesterRegistration where sr.SemesterId.Equals(id) select sr).FirstOrDefault<SemesterRegistration>();

                    var intakesforIntakeYear = (from i in db.Intake
                                                where i.IntakeYear == editingSemesterRegistration.IntakeYear && i.IsActive.Equals(true)
                                                select new
                                                {
                                                    Text = i.IntakeName,
                                                    Value = i.IntakeId
                                                }).ToList();

                    List<SelectListItem> intakeList = new SelectList(intakesforIntakeYear, "Value", "Text").ToList();
                    //intakeList.Insert(0, new SelectListItem() { Text = "-- Select Intake --", Value = "", Disabled = true, Selected = true });
                    ViewBag.intakeList = intakeList;

                    var degrees = (from d in db.Degree
                                   where d.FacultyId == editingSemesterRegistration.FacultyId && d.InstituteId == editingSemesterRegistration.InstituteId
                                   && d.IsActive.Equals(true)
                                   select new
                                   {
                                       Text = d.Name,
                                       Value = d.DegreeId
                                   }).ToList();

                    List<SelectListItem> degreeList = new SelectList(degrees, "Value", "Text").ToList();
                    //degreeList.Insert(0, new SelectListItem() { Text = "-- Select Degree --", Value = "", Disabled = true, Selected = true });
                    ViewBag.degreeList = degreeList;

                    var specializations = (from s in db.Specialization
                                           where s.DegreeId == editingSemesterRegistration.DegreeId && s.IsActive.Equals(true)
                                           select new
                                           {
                                               Text = s.Name,
                                               Value = s.SpecializationId
                                           }).ToList();

                    List<SelectListItem> specializationList = new SelectList(specializations, "Value", "Text").ToList();
                    specializationList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                    ViewBag.specializationList = specializationList;

                    return View(editingSemesterRegistration);
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/06
        [HttpGet]
        public ActionResult GetIntakesByIntakeYear(int id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var intakes = (from i in db.Intake
                               where i.IntakeYear == id && i.IsActive.Equals(true)
                               select new
                               {
                                   Text = i.IntakeName,
                                   Value = i.IntakeId
                               }).ToList();

                return Json(intakes, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/07
        [HttpPost]
        public ActionResult GetDegreesByFacultyInstitute(SemesterRegistrationCC semesterRegistrationCC)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var degrees = (from d in db.Degree
                               where d.FacultyId == semesterRegistrationCC.FacultyId && d.InstituteId == semesterRegistrationCC.InstituteId
                               && d.IsActive.Equals(true)
                               select new
                               {
                                   Text = d.Name,
                                   Value = d.DegreeId
                               }).ToList();

                return Json(degrees, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/07
        [HttpGet]
        public ActionResult GetSpecializationsByDegree(int id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var specializations = (from s in db.Specialization
                                       where s.DegreeId == id && s.IsActive.Equals(true)
                                       select new
                                       {
                                           Text = s.Name,
                                           Value = s.SpecializationId
                                       }).ToList();

                return Json(specializations, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/08
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditSemesterRegistration(SemesterRegistration semesterRegistration)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    SemesterRegistration validationRecord = (from s in db.SemesterRegistration
                                                             where s.CalendarYear.Value.Equals(semesterRegistration.CalendarYear.Value) && s.CalendarPeriodId.Value.Equals(semesterRegistration.CalendarPeriodId.Value)
                                                             && s.IntakeYear.Value.Equals(semesterRegistration.IntakeYear.Value) && s.IntakeId.Value.Equals(semesterRegistration.IntakeId.Value)
                                                             && s.AcademicYear.Value.Equals(semesterRegistration.AcademicYear.Value) && s.AcademicSemester.Value.Equals(semesterRegistration.AcademicSemester.Value)
                                                             && s.FacultyId.Value.Equals(semesterRegistration.FacultyId.Value) && s.InstituteId.Value.Equals(semesterRegistration.InstituteId.Value)
                                                             && s.DegreeId.Value.Equals(semesterRegistration.DegreeId.Value) && s.SpecializationId.Value.Equals(semesterRegistration.SpecializationId.Value)
                                                             select s).FirstOrDefault<SemesterRegistration>();

                    if (semesterRegistration.SemesterId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Semester Registration Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            semesterRegistration.CreatedBy = "Ranga";
                            semesterRegistration.CreatedDate = dateTime;
                            semesterRegistration.ModifiedBy = "Ranga";
                            semesterRegistration.ModifiedDate = dateTime;

                            db.SemesterRegistration.Add(semesterRegistration);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        SemesterRegistration editingSemesterRegistration = (from s in db.SemesterRegistration where s.SemesterId.Equals(semesterRegistration.SemesterId) select s).FirstOrDefault<SemesterRegistration>();

                        if (editingSemesterRegistration.CalendarYear != semesterRegistration.CalendarYear || editingSemesterRegistration.CalendarPeriodId != semesterRegistration.CalendarPeriodId 
                            || editingSemesterRegistration.IntakeYear != semesterRegistration.IntakeYear || editingSemesterRegistration.IntakeId != semesterRegistration.IntakeId
                            || editingSemesterRegistration.AcademicYear != semesterRegistration.AcademicYear || editingSemesterRegistration.AcademicSemester != semesterRegistration.AcademicSemester
                            || editingSemesterRegistration.FacultyId != semesterRegistration.FacultyId || editingSemesterRegistration.InstituteId != semesterRegistration.InstituteId
                            || editingSemesterRegistration.DegreeId != semesterRegistration.DegreeId || editingSemesterRegistration.SpecializationId != semesterRegistration.SpecializationId
                            || editingSemesterRegistration.FromDate != semesterRegistration.FromDate || editingSemesterRegistration.ToDate != semesterRegistration.ToDate || editingSemesterRegistration.IsActive != semesterRegistration.IsActive)
                        {
                            if(validationRecord != null && validationRecord.SemesterId != semesterRegistration.SemesterId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Semester Registration Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingSemesterRegistration.CalendarYear = semesterRegistration.CalendarYear.Value;
                                editingSemesterRegistration.CalendarPeriodId = semesterRegistration.CalendarPeriodId.Value;
                                editingSemesterRegistration.IntakeYear = semesterRegistration.IntakeYear.Value;
                                editingSemesterRegistration.IntakeId = semesterRegistration.IntakeId.Value;
                                editingSemesterRegistration.AcademicYear = semesterRegistration.AcademicYear.Value;
                                editingSemesterRegistration.AcademicSemester = semesterRegistration.AcademicSemester.Value;
                                editingSemesterRegistration.FacultyId = semesterRegistration.FacultyId;
                                editingSemesterRegistration.InstituteId = semesterRegistration.InstituteId;
                                editingSemesterRegistration.DegreeId = semesterRegistration.DegreeId;
                                editingSemesterRegistration.SpecializationId = semesterRegistration.SpecializationId;
                                editingSemesterRegistration.FromDate = semesterRegistration.FromDate.Value;
                                editingSemesterRegistration.ToDate = semesterRegistration.ToDate.Value;
                                editingSemesterRegistration.IsActive = semesterRegistration.IsActive;
                                editingSemesterRegistration.ModifiedBy = "Ranga";
                                editingSemesterRegistration.ModifiedDate = dateTime;

                                db.Entry(editingSemesterRegistration).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/09
        public ActionResult ManageSemesterSubjects(int id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                SemesterSubjectCC semesterSubjectDetails = (from sr in db.SemesterRegistration
                                                            join cp in db.CalendarPeriod on sr.CalendarPeriodId equals cp.Id into sr_cp
                                                            from calP in sr_cp.DefaultIfEmpty()
                                                            join it in db.Intake on sr.IntakeId equals it.IntakeId into sr_it
                                                            from intk in sr_it.DefaultIfEmpty()
                                                            join f in db.Faculty on sr.FacultyId equals f.FacultyId into sr_f
                                                            from fac in sr_f.DefaultIfEmpty()
                                                            join ins in db.Institute on sr.InstituteId equals ins.InstituteId into sr_ins
                                                            from inst in sr_ins.DefaultIfEmpty()
                                                            join d in db.Degree on sr.DegreeId equals d.DegreeId into sr_d
                                                            from dg in sr_d.DefaultIfEmpty()
                                                            join sp in db.Specialization on sr.SpecializationId equals sp.SpecializationId into sr_sp
                                                            from splz in sr_sp.DefaultIfEmpty()
                                                            where sr.SemesterId.Equals(id)
                                                            select new SemesterSubjectCC
                                                            {
                                                                SemesterId = sr.SemesterId,
                                                                CalendarYear = sr.CalendarYear,
                                                                CalendarPeriodName = calP.PeriodName,
                                                                IntakeYear = intk.IntakeYear,
                                                                IntakeName = intk.IntakeName,
                                                                AcademicYear = sr.AcademicYear,
                                                                AcademicSemester = sr.AcademicSemester,
                                                                FacultyName = fac.FacultyName,
                                                                InstituteName = inst.InstituteName,
                                                                DegreeName = dg.Name,
                                                                SpecializationName = splz.Name != null ? splz.Name : "N/A",
                                                                SubjectList = (from su in db.Subject
                                                                               join de in db.Degree on su.DegreeId equals de.DegreeId into su_de
                                                                               from dgr in su_de.DefaultIfEmpty()
                                                                               where su.IsActive.Equals(true) && (su.IsCommon.Equals(true) || dgr.FacultyId.Value == fac.FacultyId)
                                                                               select su).ToList(),
                                                                ViewingSemesterSubjectIdList = (from ss in db.SemesterSubject
                                                                                                join s in db.Subject on ss.SubjectId equals s.SubjectId
                                                                                                where ss.SemesterRegistrationId.Equals(id) && ss.IsActive.Equals(true)
                                                                                                select s.SubjectId).ToList()
                                                            }).FirstOrDefault<SemesterSubjectCC>();

                return View(semesterSubjectDetails);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/08
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditSemesterSubject(SemesterSubjectCC semesterSubjectObj)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    var passingSubjectList = new JavaScriptSerializer().Deserialize<List<string>>(semesterSubjectObj.PassingSemesterSubjectIdList).ToList();
                    List<SemesterSubject> semesterSubjectsForSemester = (from ss in db.SemesterSubject where ss.SemesterRegistrationId == semesterSubjectObj.SemesterId select ss).ToList();

                    if (passingSubjectList.Count != 0)
                    {
                        for (int i = 0; i < passingSubjectList.Count; i++)
                        {
                            if (semesterSubjectsForSemester.Find(ss => ss.SubjectId == int.Parse(passingSubjectList[i])) == null)
                            {
                                SemesterSubject semSub = new SemesterSubject();
                                semSub.SemesterRegistrationId = semesterSubjectObj.SemesterId;
                                semSub.SubjectId = int.Parse(passingSubjectList[i]);
                                semSub.CreatedBy = "Ranga";
                                semSub.CreatedDate = dateTime;
                                semSub.ModifiedBy = "Ranga";
                                semSub.ModifiedDate = dateTime;
                                semSub.IsActive = true;

                                db.SemesterSubject.Add(semSub);
                            }
                            else
                            {
                                int matchingIndex = semesterSubjectsForSemester.FindIndex(s => s.SubjectId == int.Parse(passingSubjectList[i]));
                                if(semesterSubjectsForSemester[matchingIndex].IsActive == false)
                                {
                                    semesterSubjectsForSemester[matchingIndex].IsActive = true;
                                    semesterSubjectsForSemester[matchingIndex].ModifiedBy = "Ranga";
                                    semesterSubjectsForSemester[matchingIndex].ModifiedDate = dateTime;

                                    db.Entry(semesterSubjectsForSemester[matchingIndex]).State = EntityState.Modified;
                                }
                                semesterSubjectsForSemester.RemoveAt(semesterSubjectsForSemester.FindIndex(ss => ss.SubjectId == int.Parse(passingSubjectList[i])));
                            }
                        }

                        db.SaveChanges();

                        if (semesterSubjectsForSemester.Count != 0)
                        {
                            for (int j = 0; j < semesterSubjectsForSemester.Count; j++)
                            {
                                semesterSubjectsForSemester[j].IsActive = false;
                                semesterSubjectsForSemester[j].ModifiedBy = "Ranga";
                                semesterSubjectsForSemester[j].ModifiedDate = dateTime;

                                db.Entry(semesterSubjectsForSemester[j]).State = EntityState.Modified;
                            }

                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        if (semesterSubjectsForSemester.Count != 0)
                        {
                            for (int i = 0; i < semesterSubjectsForSemester.Count; i++)
                            {
                                semesterSubjectsForSemester[i].IsActive = false;
                                semesterSubjectsForSemester[i].ModifiedBy = "Ranga";
                                semesterSubjectsForSemester[i].ModifiedDate = dateTime;

                                db.Entry(semesterSubjectsForSemester[i]).State = EntityState.Modified;
                            }

                            db.SaveChanges();
                        }
                    }
                    //if(semesterSubjectsForSemester.Count != 0)
                    //{
                    //    if (semesterSubjectObj.SemesterSubjectIdList.Count != 0)
                    //    {
                    //        for (int i = 0; i < semesterSubjectObj.SemesterSubjectIdList.Count; i++)
                    //        {
                    //            if (semesterSubjectsForSemester.Find(s => s.SubjectId == semesterSubjectObj.SemesterSubjectIdList[i]) == null)
                    //            {
                    //                SemesterSubject semSub = new SemesterSubject();
                    //                semSub.SemesterRegistrationId = semesterSubjectObj.SemesterId;
                    //                semSub.SubjectId = semesterSubjectObj.SemesterSubjectIdList[i];
                    //                semSub.CreatedBy = "Ranga";
                    //                semSub.CreatedDate = dateTime;
                    //                semSub.ModifiedBy = "Ranga";
                    //                semSub.ModifiedDate = dateTime;
                    //                semSub.IsActive = true;

                    //                db.SemesterSubject.Add(semSub);
                    //            }
                    //            else
                    //            {
                    //                int matchingIndex = semesterSubjectsForSemester.FindIndex(s => s.SubjectId == semesterSubjectObj.SemesterSubjectIdList[i]);
                    //                if (semesterSubjectsForSemester[matchingIndex].IsActive == false)
                    //                {
                    //                    semesterSubjectsForSemester[matchingIndex].IsActive = true;
                    //                    semesterSubjectsForSemester[matchingIndex].ModifiedBy = "Ranga";
                    //                    semesterSubjectsForSemester[matchingIndex].ModifiedDate = dateTime;

                    //                    db.Entry(semesterSubjectsForSemester[matchingIndex]).State = EntityState.Modified;
                    //                }
                    //                semesterSubjectsForSemester.RemoveAt(semesterSubjectsForSemester.FindIndex(s => s.SubjectId == semesterSubjectObj.SemesterSubjectIdList[i]));
                    //            }
                    //        }
                    //    }

                    //    if(semesterSubjectsForSemester.Count != 0)
                    //    {
                    //        for (int i = 0; i < semesterSubjectsForSemester.Count; i++)
                    //        {
                    //            semesterSubjectsForSemester[i].IsActive = false;
                    //            semesterSubjectsForSemester[i].ModifiedBy = "Ranga";
                    //            semesterSubjectsForSemester[i].ModifiedDate = dateTime;

                    //            db.Entry(semesterSubjectsForSemester[i]).State = EntityState.Modified;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if(semesterSubjectObj.SemesterSubjectIdList.Count != 0)
                    //    {
                    //        for (int i = 0; i < semesterSubjectObj.SemesterSubjectIdList.Count; i++)
                    //        {
                    //            SemesterSubject semSub = new SemesterSubject();
                    //            semSub.SemesterRegistrationId = semesterSubjectObj.SemesterId;
                    //            semSub.SubjectId = semesterSubjectObj.SemesterSubjectIdList[i];
                    //            semSub.CreatedBy = "Ranga";
                    //            semSub.CreatedDate = dateTime;
                    //            semSub.ModifiedBy = "Ranga";
                    //            semSub.ModifiedDate = dateTime;
                    //            semSub.IsActive = true;

                    //            db.SemesterSubject.Add(semSub);
                    //        }
                    //    }
                    //}

                    //db.SaveChanges();

                    return Json(new
                    {
                        success = true,
                        message = "Successfully Saved"
                    }, JsonRequestBehavior.AllowGet);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/20
        public ActionResult ManageAccessGroups()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/20
        public ActionResult GetAccessGroups()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<AccessGroup> accessGroupList = (from ag in db.AccessGroup orderby ag.AccessGroupId descending select ag).ToList();
                return Json(new { data = accessGroupList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/20
        [HttpGet]
        public ActionResult AddOrEditAccessGroup(int id = 0)
        {
            if (id == 0)
            {
                return View(new AccessGroup());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from ag in db.AccessGroup where ag.AccessGroupId.Equals(id) select ag).FirstOrDefault<AccessGroup>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/20
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditAccessGroup(AccessGroup accessGroup)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    AccessGroup validationRecord = (from ag in db.AccessGroup where ag.AccessGroupName.Equals(accessGroup.AccessGroupName) select ag).FirstOrDefault<AccessGroup>();

                    if (accessGroup.AccessGroupId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Access Group Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            accessGroup.CreatedBy = "Ranga";
                            accessGroup.CreatedDate = dateTime;
                            accessGroup.ModifiedBy = "Ranga";
                            accessGroup.ModifiedDate = dateTime;

                            db.AccessGroup.Add(accessGroup);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        AccessGroup editingAccessGroup = (from ag in db.AccessGroup where ag.AccessGroupId.Equals(accessGroup.AccessGroupId) select ag).FirstOrDefault<AccessGroup>();

                        if (editingAccessGroup.AccessGroupName != accessGroup.AccessGroupName || editingAccessGroup.Description != accessGroup.Description || editingAccessGroup.IsActive != accessGroup.IsActive)
                        {
                            if (validationRecord != null && validationRecord.AccessGroupId != accessGroup.AccessGroupId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Access Group Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingAccessGroup.AccessGroupName = accessGroup.AccessGroupName;
                                editingAccessGroup.Description = accessGroup.Description;
                                editingAccessGroup.IsActive = accessGroup.IsActive;
                                editingAccessGroup.ModifiedBy = "Ranga";
                                editingAccessGroup.ModifiedDate = dateTime;

                                db.Entry(editingAccessGroup).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/20
        public ActionResult ManageAccessGroupRoles()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/20
        public ActionResult GetAccessGroupRoles(int id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<AspNetRoles> accessGroupRolesList = (from agr in db.AspNetRoles where agr.AccessGroupId.Equals(id) orderby agr.Id descending select agr).ToList();
                return Json(new { data = accessGroupRolesList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/20
        [HttpGet]
        public ActionResult AddOrEditAccessGroupRole(string id)
        {
            if (id == null)
            {
                return View(new AspNetRoles());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from agr in db.AspNetRoles where agr.Id.Equals(id) select agr).FirstOrDefault<AspNetRoles>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/20
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditAccessGroupRole(AspNetRoles accessGroupRole)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    AspNetRoles validationRecord = (from agr in db.AspNetRoles where agr.AccessGroupId.Equals(accessGroupRole.AccessGroupId) && agr.Name.Equals(accessGroupRole.Name) select agr).FirstOrDefault<AspNetRoles>();

                    if (accessGroupRole.Id == null)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Role Already Exists For Selected Access Group"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            accessGroupRole.Id = Guid.NewGuid().ToString();
                            accessGroupRole.CreatedBy = "Ranga";
                            accessGroupRole.CreatedDate = dateTime;
                            accessGroupRole.ModifiedBy = "Ranga";
                            accessGroupRole.ModifiedDate = dateTime;

                            db.AspNetRoles.Add(accessGroupRole);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        AspNetRoles editingAccessGroupRole = (from agr in db.AspNetRoles where agr.Id.Equals(accessGroupRole.Id) select agr).FirstOrDefault<AspNetRoles>();

                        if (editingAccessGroupRole.Name != accessGroupRole.Name || editingAccessGroupRole.IsActive != accessGroupRole.IsActive)
                        {
                            if (validationRecord != null && validationRecord.Id != accessGroupRole.Id)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Role Already Exists For Selected Access Group"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingAccessGroupRole.Name = accessGroupRole.Name;
                                editingAccessGroupRole.IsActive = accessGroupRole.IsActive;
                                editingAccessGroupRole.ModifiedBy = "Ranga";
                                editingAccessGroupRole.ModifiedDate = dateTime;

                                db.Entry(editingAccessGroupRole).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/21
        public ActionResult ManageClaims()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/21
        public ActionResult GetClaims()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<Claim> claimList = (from c in db.Claim orderby c.ClaimId descending select c).ToList();
                return Json(new { data = claimList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/21
        [HttpGet]
        public ActionResult AddOrEditClaim(int id = 0)
        {
            var asm = Assembly.GetAssembly(typeof(PMS.MvcApplication));
            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new {
                        Controller = x.DeclaringType.Name.Split(new string[] { "Controller" }, StringSplitOptions.None)[0],
                        Action = x.Name
                    })
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).Distinct().ToList();

            List<SelectListItem> controllerActions = new List<SelectListItem>();

            foreach (var item in controlleractionlist)
            {
                if (item.Controller == "SA" || item.Controller == "User")
                {
                    controllerActions.Add(new SelectListItem()
                    {
                        Text = "/" + item.Controller + "/" + item.Action,
                        Value = "/" + item.Controller + "/" + item.Action
                    });
                }
            }

            //controllerActions.Insert(0, new SelectListItem() { Text = "-- Select Claim Value --", Value = "", Disabled = true, Selected = false });
            ViewBag.claimValueList = controllerActions;

            if (id == 0)
            {
                return View(new Claim());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from c in db.Claim where c.ClaimId.Equals(id) select c).FirstOrDefault<Claim>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/22
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditClaim(Claim claim)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Claim validationRecord = (from c in db.Claim
                                              where c.ClaimName.Equals(claim.ClaimName) && c.ClaimValue.Equals(claim.ClaimValue) 
                                              && c.SubOperation.Equals(claim.SubOperation)
                                              select c).FirstOrDefault<Claim>();

                    if (claim.ClaimId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Claim Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            claim.CreatedBy = "Ranga";
                            claim.CreatedDate = dateTime;
                            claim.ModifiedBy = "Ranga";
                            claim.ModifiedDate = dateTime;

                            db.Claim.Add(claim);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Claim editingClaim = (from c in db.Claim where c.ClaimId.Equals(claim.ClaimId) select c).FirstOrDefault<Claim>();

                        if (editingClaim.ClaimName != claim.ClaimName || editingClaim.ClaimValue != claim.ClaimValue
                            || editingClaim.SubOperation != claim.SubOperation || editingClaim.Description != claim.Description || editingClaim.IsActive != claim.IsActive)
                        {
                            if (validationRecord != null && validationRecord.ClaimId != claim.ClaimId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Claim Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingClaim.ClaimName = claim.ClaimName;
                                editingClaim.ClaimValue = claim.ClaimValue;
                                editingClaim.SubOperation = claim.SubOperation;
                                editingClaim.Description = claim.Description;
                                editingClaim.IsActive = claim.IsActive;
                                editingClaim.ModifiedBy = "Ranga";
                                editingClaim.ModifiedDate = dateTime;

                                db.Entry(editingClaim).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/21
        public ActionResult TestMethod()
        {
            //    List<string> list = new List<string>();
            //    var types =
            //from a in AppDomain.CurrentDomain.GetAssemblies()
            //from t in a.GetTypes()
            //where typeof(IController).IsAssignableFrom(t) &&
            //        string.Equals("SA" + "Controller", t.Name, StringComparison.OrdinalIgnoreCase)
            //select t;

            //    var controllerType = types.FirstOrDefault();

            //    if (controllerType == null)
            //    {
            //        list = Enumerable.Empty<string>().ToList();
            //    }
            //   list = new ReflectedControllerDescriptor(controllerType)
            //        .GetCanonicalActions().Select(x => x.ActionName)
            //        .ToList();

            var asm = Assembly.GetAssembly(typeof(PMS.MvcApplication));
            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new {
                        Controller = x.DeclaringType.Name.Split(new string[] { "Controller" }, StringSplitOptions.None)[0],
                        Action = x.Name
                    })
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).Distinct().ToList();

            var list = new List<ControllerActions>();

            foreach (var item in controlleractionlist)
            {
                if(item.Controller == "SA" || item.Controller == "User")
                {
                    list.Add(new ControllerActions()
                    {
                        Controller = item.Controller,
                        Action = item.Action
                    });
                }
            }

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TestMethod1(string id)
        {
            //ExcelPackage ep = new ExcelPackage();
            //ExcelWorksheet sheet = ep.Workbook.Worksheets.Add("Errors List");
            using(PMSEntities db = new PMSEntities())
            {
                var currentDateTime = DateTime.Now;
                var monthStartDate = new DateTime(currentDateTime.Year, currentDateTime.Month, 1);
                var username = "ranga.a";
                //int deadlineDays = 0;

                //List<ConductedLectures> conductedLectureRecords = new List<ConductedLectures>();
                List<ConfigurationalSettings> deadlineCSList = new List<ConfigurationalSettings>();
                List<SubWorkflow_WorkflowCC> nextWorkflows = new List<SubWorkflow_WorkflowCC>();

                var userRecord = (from u in db.AspNetUsers
                                  join f in db.Faculty on u.FacultyId equals f.FacultyId into u_f
                                  from fac in u_f.DefaultIfEmpty()
                                  where u.UserName.Equals(username)
                                  select u).FirstOrDefault();

                var conductedLectureRecords = (from cl in db.ConductedLectures
                                               join tt in db.LectureTimetable on cl.TimetableId equals tt.TimetableId
                                               join sem in db.SemesterRegistration on tt.SemesterId equals sem.SemesterId
                                               where tt.LecturerId.Equals(userRecord.Id) && cl.IsActive.Equals(true) && tt.IsActive.Equals(true)
                                               && string.IsNullOrEmpty(cl.CurrentStage.Value.ToString())
                                               select new
                                               {
                                                   lectureRecord = cl,
                                                   facultyId = sem.FacultyId.Value
                                               }).ToList();

                for (var i = 0; i < conductedLectureRecords.Count; i++)
                {
                    int deadlineDays = 0;
                    var paymentConsideringMonth = 0;
                    int facultyId = conductedLectureRecords[i].facultyId;
                    var checkingCS = deadlineCSList.Find(cs => cs.FacultyId.Value.Equals(facultyId));

                    if (checkingCS == null)
                    {
                        var deadlineCSRecord = (from c in db.ConfigurationalSettings
                                                where c.ConfigurationKey.Equals("Lecture Submission Deadline Date") && c.FacultyId.Value == facultyId
                                                select c).FirstOrDefault();

                        if (deadlineCSRecord != null)
                        {
                            deadlineCSList.Add(deadlineCSRecord);
                            deadlineDays = int.Parse(deadlineCSRecord.ConfigurationValue);
                        }
                    }
                    else
                    {
                        deadlineDays = int.Parse(checkingCS.ConfigurationValue);
                    }

                    if (deadlineDays != 0)
                    {
                        var deadlineDate = monthStartDate.AddDays(deadlineDays);

                        if (currentDateTime <= deadlineDate)
                        {
                            paymentConsideringMonth = currentDateTime.AddMonths(-1).Month;
                        }
                        else
                        {
                            paymentConsideringMonth = currentDateTime.Month;
                        }
                    }
                    else
                    {
                        paymentConsideringMonth = currentDateTime.Month;
                    }

                    //var checkingWorkflow = nextWorkflows.Find(sw => sw.WorkflowRecord.FacultyId.Value == conductedLectureRecords[i].facultyId);

                    //if (checkingWorkflow == null)
                    //{
                    //    var nextWorkflowRecord = (from sw in db.SubWorkflows
                    //                              join w in db.Workflows on sw.WorkflowId equals w.Id
                    //                              join r in db.AspNetRoles on sw.WorkflowRole equals r.Id
                    //                              where sw.WorkflowStep.Equals(2) && w.FacultyId.Value.Equals(conductedLectureRecords[i].facultyId)
                    //                              select new SubWorkflow_WorkflowCC
                    //                              {
                    //                                  SubWorkflowRecord = sw,
                    //                                  WorkflowRole = r.Name,
                    //                                  WorkflowRecord = w
                    //                              }).FirstOrDefault();

                    //    if (nextWorkflowRecord != null)
                    //    {
                    //        nextWorkflows.Add(nextWorkflowRecord);

                    //        if (conductedLectureRecords[i].lectureRecord.ActualLectureDate.Year == currentDateTime.Year
                    //            && conductedLectureRecords[i].lectureRecord.ActualLectureDate.Month == paymentConsideringMonth)
                    //        {
                    //            conductedLectureRecords[i].lectureRecord.CurrentStage = nextWorkflowRecord.SubWorkflowRecord.SubWorkflowId;
                    //            conductedLectureRecords[i].lectureRecord.CurrentStageDisplayName = "Submitted to " + nextWorkflowRecord.WorkflowRole;
                    //            conductedLectureRecords[i].lectureRecord.ModifiedDate = currentDateTime;
                    //            conductedLectureRecords[i].lectureRecord.ModifiedBy = "Ranga";

                    //            db.Entry(conductedLectureRecords[i].lectureRecord).State = EntityState.Modified;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (conductedLectureRecords[i].lectureRecord.ActualLectureDate.Year == currentDateTime.Year
                    //            && conductedLectureRecords[i].lectureRecord.ActualLectureDate.Month == paymentConsideringMonth)
                    //    {
                    //        conductedLectureRecords[i].lectureRecord.CurrentStage = checkingWorkflow.SubWorkflowRecord.SubWorkflowId;
                    //        conductedLectureRecords[i].lectureRecord.CurrentStageDisplayName = "Submitted to " + checkingWorkflow.WorkflowRole;
                    //        conductedLectureRecords[i].lectureRecord.ModifiedDate = currentDateTime;
                    //        conductedLectureRecords[i].lectureRecord.ModifiedBy = "Ranga";

                    //        db.Entry(conductedLectureRecords[i].lectureRecord).State = EntityState.Modified;
                    //    }
                    //}
                }

                return Json(new { data = deadlineCSList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/23
        public ActionResult ManageStudentBatches()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/23
        public ActionResult GetStudentBatches(int id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<StudentBatch> studentBatchesList = (from sb in db.StudentBatch where sb.SemesterRegistrationId.Equals(id) orderby sb.StudentBatchId descending select sb).ToList();
                return Json(new { data = studentBatchesList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/23
        [HttpGet]
        public ActionResult AddOrEditStudentBatch(int id = 0)
        {
            if (id == 0)
            {
                return View(new StudentBatch());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from sb in db.StudentBatch where sb.StudentBatchId.Equals(id) select sb).FirstOrDefault<StudentBatch>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/23
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditStudentBatch(StudentBatch studentBatch)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    StudentBatch validationRecord = (from sb in db.StudentBatch where sb.SemesterRegistrationId.Equals(studentBatch.SemesterRegistrationId) && sb.BatchName.Equals(studentBatch.BatchName) select sb).FirstOrDefault<StudentBatch>();

                    if (studentBatch.StudentBatchId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Student Batch Already Exists For Selected Semester Registration"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            studentBatch.CreatedBy = "Ranga";
                            studentBatch.CreatedDate = dateTime;
                            studentBatch.ModifiedBy = "Ranga";
                            studentBatch.ModifiedDate = dateTime;

                            db.StudentBatch.Add(studentBatch);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        StudentBatch editingStudentBatch = (from sb in db.StudentBatch where sb.StudentBatchId.Equals(studentBatch.StudentBatchId) select sb).FirstOrDefault<StudentBatch>();

                        if (editingStudentBatch.BatchName != studentBatch.BatchName || editingStudentBatch.IsActive != studentBatch.IsActive)
                        {
                            if (validationRecord != null && validationRecord.StudentBatchId != studentBatch.StudentBatchId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Student Batch Already Exists For Selected Semester Registration"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingStudentBatch.BatchName = studentBatch.BatchName;
                                editingStudentBatch.IsActive = studentBatch.IsActive;
                                editingStudentBatch.ModifiedBy = "Ranga";
                                editingStudentBatch.ModifiedDate = dateTime;

                                db.Entry(editingStudentBatch).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/26
        public ActionResult ManageUsers()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/26
        public ActionResult GetUsers()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<UserVM> usersList = (from u in db.AspNetUsers
                                          join t in db.Title on u.EmployeeTitle equals t.TitleId
                                          join f in db.Faculty on u.FacultyId equals f.FacultyId into u_f
                                          from fac in u_f.DefaultIfEmpty()
                                          join d in db.Department on u.DepartmentId equals d.DepartmentId into u_d
                                          from dep in u_d.DefaultIfEmpty()
                                          orderby u.EmployeeNumber ascending
                                          select new UserVM
                                          {
                                              Id = u.Id,
                                              EmployeeNumber = u.EmployeeNumber,
                                              EmployeeName = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                              Email = u.Email,
                                              PhoneNumber = u.PhoneNumber,
                                              FacultyName = fac != null ? fac.FacultyName : null,
                                              DepartmentName = dep != null ? dep.DepartmentName : null,
                                              IsActive = u.IsActive
                                          }).ToList();
                return Json(new { data = usersList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/26
        [HttpGet]
        public ActionResult AddOrEditUser(string id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var titles = (from t in db.Title
                              where t.IsActive.Equals(true)
                              select new
                              {
                                  Text = t.TitleName,
                                  Value = t.TitleId
                              }).ToList();

                List<SelectListItem> titleList = new SelectList(titles, "Value", "Text").ToList();
                ViewBag.titleList = titleList;

                var faculties = (from f in db.Faculty
                                 where f.IsActive.Equals(true)
                                 select new
                                 {
                                     Text = f.FacultyName,
                                     Value = f.FacultyId
                                 }).ToList();

                List<SelectListItem> facultyList = new SelectList(faculties, "Value", "Text").ToList();
                facultyList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                ViewBag.facultyList = facultyList;

                var departments = (from d in db.Department
                                   where d.IsActive.Equals(true)
                                   select new
                                   {
                                       Text = d.DepartmentName,
                                       Value = d.DepartmentId
                                   }).ToList();

                List<SelectListItem> departmentList = new SelectList(departments, "Value", "Text").ToList();
                departmentList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                ViewBag.departmentList = departmentList;

                if (id == null)
                {
                    return View(new AspNetUsers());
                }
                else
                {
                    return View((from u in db.AspNetUsers where u.Id.Equals(id) select u).FirstOrDefault<AspNetUsers>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/26
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditUser(AspNetUsers user)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    UserFunctions usrFunctions = new UserFunctions();
                    string employeeNumber = usrFunctions.GenerateEmployeeNumber(usrFunctions.ExtractNumbers(user.EmployeeNumber));
                    AspNetUsers validationRecord = (from u in db.AspNetUsers
                                                    where u.EmployeeNumber.Equals(employeeNumber) || ((u.FirstName.Equals(user.FirstName)) && (u.LastName.Equals(user.LastName))) 
                                                    || u.Email.Equals(user.Email) select u).FirstOrDefault<AspNetUsers>();

                    if (user.Id == null)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This User Already Exist"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if(new UserFunctions().IsValidSLIITEmail(user.Email) == true)
                            {
                                user.Id = Guid.NewGuid().ToString();
                                user.EmployeeNumber = employeeNumber;
                                user.UserName = user.Email.ToString().Substring(0, user.Email.ToString().IndexOf("@"));
                                user.CreatedBy = "Ranga";
                                user.CreatedDate = dateTime;
                                user.ModifiedBy = "Ranga";
                                user.ModifiedDate = dateTime;
                                user.EmailConfirmed = false;
                                user.PhoneNumberConfirmed = false;
                                user.TwoFactorEnabled = false;
                                user.AccessFailedCount = 0;
                                user.LockoutEnabled = true;
                                user.SecurityStamp = Guid.NewGuid().ToString();

                                db.AspNetUsers.Add(user);
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Saved"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "Invalid SLIIT Email"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        AspNetUsers editingUser = (from u in db.AspNetUsers where u.Id.Equals(user.Id) select u).FirstOrDefault<AspNetUsers>();

                        if (editingUser.EmployeeNumber != employeeNumber || editingUser.EmployeeTitle != user.EmployeeTitle 
                            || editingUser.FirstName != user.FirstName || editingUser.LastName != user.LastName 
                            || editingUser.Email != user.Email || editingUser.PhoneNumber != user.PhoneNumber || editingUser.IsAcademicUser != user.IsAcademicUser
                            || editingUser.FacultyId != user.FacultyId || editingUser.DepartmentId != user.DepartmentId || editingUser.IsActive != user.IsActive)
                        {
                            if (validationRecord != null && validationRecord.Id != user.Id)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This User Already Exist"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if(new UserFunctions().IsValidSLIITEmail(user.Email) == true)
                                {
                                    editingUser.EmployeeNumber = employeeNumber;
                                    editingUser.EmployeeTitle = user.EmployeeTitle;
                                    editingUser.FirstName = user.FirstName;
                                    editingUser.LastName = user.LastName;
                                    editingUser.Email = user.Email;
                                    editingUser.UserName = user.Email.ToString().Substring(0, user.Email.ToString().IndexOf("@"));
                                    editingUser.IsAcademicUser = user.IsAcademicUser;
                                    editingUser.PhoneNumber = user.PhoneNumber;
                                    editingUser.FacultyId = user.FacultyId;
                                    editingUser.DepartmentId = user.DepartmentId;
                                    editingUser.IsActive = user.IsActive;
                                    editingUser.ModifiedBy = "Ranga";
                                    editingUser.ModifiedDate = dateTime;

                                    db.Entry(editingUser).State = EntityState.Modified;
                                    db.SaveChanges();

                                    return Json(new
                                    {
                                        success = true,
                                        message = "Successfully Updated"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "Invalid SLIIT Email"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/27
        public ActionResult ManageUserClaims(string id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                UserVM user = (from u in db.AspNetUsers
                            join t in db.Title on u.EmployeeTitle equals t.TitleId
                            where u.Id.Equals(id)
                            select new UserVM
                            {
                                EmployeeNumber = u.EmployeeNumber,
                                EmployeeName = t.TitleName + " " + u.FirstName + " " + u.LastName
                            }).FirstOrDefault<UserVM>();

                ViewBag.userData = user;

                UserClaimCC userClaim = new UserClaimCC {
                    UserId = id
                };

                userClaim.UserGroups = (from ag in db.AccessGroup where ag.IsActive.Equals(true) select ag).ToList();

                List<AspNetRoles> selectedUserRoles = (from ur in db.AspNetUserRoles
                                                       join r in db.AspNetRoles on ur.RoleId equals r.Id
                                                       where ur.UserId.Equals(id) && ur.IsActive.Equals(true)
                                                       select r).Distinct().ToList();

                userClaim.SelectedUserGroupIds = new List<int>();
                userClaim.SelectedUserRoleIds = new List<string>();
                userClaim.UserRoles = new List<AspNetRoles>();
                userClaim.ClaimsForAccessGroups = new List<AccessGroupClaim_ClaimCC>();

                if (selectedUserRoles.Count != 0)
                {
                    userClaim.SelectedUserGroupIds = selectedUserRoles.Select(r => r.AccessGroupId).ToList();
                    userClaim.SelectedUserRoleIds = selectedUserRoles.Select(r => r.Id).ToList();

                    userClaim.UserRoles = (from r in db.AspNetRoles where userClaim.SelectedUserGroupIds.Select(sr => sr).Contains(r.AccessGroupId) select r).ToList();

                    if(userClaim.SelectedUserGroupIds.Count != 0)
                    {
                        userClaim.ClaimsForAccessGroups = (from agc in db.AccessGroupClaims
                                                           join c in db.Claim on agc.ClaimId equals c.ClaimId
                                                           where userClaim.SelectedUserGroupIds.Contains(agc.AccessGroupId) && agc.IsActive.Equals(true)
                                                           select new AccessGroupClaim_ClaimCC
                                                           {
                                                               AccessGroupClaimId = agc.Id,
                                                               ClaimName = c.ClaimName
                                                           }).ToList();
                    }
                }

                List<AspNetUserClaims> selectedUserClaims = (from uc in db.AspNetUserClaims where uc.UserId.Equals(id) && uc.IsActive.Equals(true) select uc).ToList();
                userClaim.SelectedUserClaimIds = selectedUserClaims.Select(uc => uc.AccessGroupClaimId).ToList();

                return View(userClaim);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/28
        [HttpPost]
        public ActionResult GetUserRolesByAccessGroups(UserClaimCC userClaimCC)
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<AspNetRoles> userRoles = new List<AspNetRoles>();

                if (userClaimCC.SelectedUserGroupIds.Count != 0)
                {
                    userRoles = (from ag in db.AccessGroup
                                 join r in db.AspNetRoles on ag.AccessGroupId equals r.AccessGroupId
                                 where userClaimCC.SelectedUserGroupIds.Contains(ag.AccessGroupId) && r.IsActive.Equals(true)
                                 select r).ToList();
                }

                return Json(userRoles, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/03
        [HttpPost]
        public ActionResult GetClaimsByAccessGroups(UserClaimCC userClaimCC)
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<AccessGroupClaim_ClaimCC> claimList = new List<AccessGroupClaim_ClaimCC>();

                if (userClaimCC.SelectedUserGroupIds.Count != 0)
                {
                    claimList = (from agc in db.AccessGroupClaims
                                 join c in db.Claim on agc.ClaimId equals c.ClaimId
                                 where userClaimCC.SelectedUserGroupIds.Contains(agc.AccessGroupId) && agc.IsActive.Equals(true)
                                 select new AccessGroupClaim_ClaimCC {
                                     AccessGroupClaimId = agc.Id,
                                     ClaimName = c.ClaimName
                                 }).ToList();
                }

                return Json(claimList, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/09/28
        [HttpPost]
        public ActionResult AddOrEditUserClaim(UserClaimCC userClaimCC)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    var passingUserGroups = new JavaScriptSerializer().Deserialize<List<int>>(userClaimCC.passingUserGroupIds).ToList();
                    var passingUserRoles = new JavaScriptSerializer().Deserialize<List<string>>(userClaimCC.passingUserRoleIds).ToList();
                    var passingUserClaims = new JavaScriptSerializer().Deserialize<List<int>>(userClaimCC.passingUserClaimIds).ToList();

                    List<AspNetUserRoles> userRoles = (from ur in db.AspNetUserRoles where ur.UserId.Equals(userClaimCC.UserId) select ur).ToList();

                    List<AspNetUserClaims> userClaims = (from uc in db.AspNetUserClaims where uc.UserId.Equals(userClaimCC.UserId) select uc).ToList();

                    if(passingUserRoles.Count != 0)
                    {
                        for(int i = 0; i < passingUserRoles.Count; i++)
                        {
                            if(userRoles.Find(ur => ur.RoleId == passingUserRoles[i]) == null)
                            {
                                AspNetUserRoles userRoleObj = new AspNetUserRoles();
                                userRoleObj.UserId = userClaimCC.UserId;
                                userRoleObj.RoleId = passingUserRoles[i];
                                userRoleObj.CreatedBy = "Ranga";
                                userRoleObj.CreatedDate = dateTime;
                                userRoleObj.ModifiedBy = "Ranga";
                                userRoleObj.ModifiedDate = dateTime;
                                userRoleObj.IsActive = true;

                                db.AspNetUserRoles.Add(userRoleObj);
                            }
                            else
                            {
                                int matchingIndex = userRoles.FindIndex(ur => ur.RoleId == passingUserRoles[i]);
                                if (userRoles[matchingIndex].IsActive == false)
                                {
                                    userRoles[matchingIndex].IsActive = true;
                                    userRoles[matchingIndex].ModifiedBy = "Ranga";
                                    userRoles[matchingIndex].ModifiedDate = dateTime;

                                    db.Entry(userRoles[matchingIndex]).State = EntityState.Modified;
                                }
                                userRoles.RemoveAt(userRoles.FindIndex(ur => ur.RoleId == passingUserRoles[i]));
                            }
                        }

                        db.SaveChanges();

                        if (userRoles.Count != 0)
                        {
                            for (int i = 0; i < userRoles.Count; i++)
                            {
                                userRoles[i].IsActive = false;
                                userRoles[i].ModifiedBy = "Ranga";
                                userRoles[i].ModifiedDate = dateTime;

                                db.Entry(userRoles[i]).State = EntityState.Modified;
                            }

                            db.SaveChanges();
                        }

                        if (passingUserClaims.Count != 0)
                        {
                            for (int i = 0; i < passingUserClaims.Count; i++)
                            {
                                if (userClaims.Find(uc => uc.AccessGroupClaimId == passingUserClaims[i]) == null)
                                {
                                    AspNetUserClaims userClaimObj = new AspNetUserClaims();
                                    userClaimObj.UserId = userClaimCC.UserId;
                                    userClaimObj.AccessGroupClaimId = passingUserClaims[i];
                                    userClaimObj.CreatedBy = "Ranga";
                                    userClaimObj.CreatedDate = dateTime;
                                    userClaimObj.ModifiedBy = "Ranga";
                                    userClaimObj.ModifiedDate = dateTime;
                                    userClaimObj.IsActive = true;

                                    db.AspNetUserClaims.Add(userClaimObj);
                                }
                                else
                                {
                                    int matchingIndex = userClaims.FindIndex(uc => uc.AccessGroupClaimId == passingUserClaims[i]);
                                    if (userClaims[matchingIndex].IsActive == false)
                                    {
                                        userClaims[matchingIndex].IsActive = true;
                                        userClaims[matchingIndex].ModifiedBy = "Ranga";
                                        userClaims[matchingIndex].ModifiedDate = dateTime;

                                        db.Entry(userClaims[matchingIndex]).State = EntityState.Modified;
                                    }
                                    userClaims.RemoveAt(userClaims.FindIndex(uc => uc.AccessGroupClaimId == passingUserClaims[i]));
                                }
                            }

                            db.SaveChanges();

                            if (userClaims.Count != 0)
                            {
                                for (int i = 0; i < userClaims.Count; i++)
                                {
                                    userClaims[i].IsActive = false;
                                    userClaims[i].ModifiedBy = "Ranga";
                                    userClaims[i].ModifiedDate = dateTime;

                                    db.Entry(userClaims[i]).State = EntityState.Modified;
                                }

                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            if (userClaims.Count != 0)
                            {
                                for (int i = 0; i < userClaims.Count; i++)
                                {
                                    userClaims[i].IsActive = false;
                                    userClaims[i].ModifiedBy = "Ranga";
                                    userClaims[i].ModifiedDate = dateTime;

                                    db.Entry(userClaims[i]).State = EntityState.Modified;
                                }

                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        if(userRoles.Count != 0)
                        {
                            for(int i = 0; i < userRoles.Count; i++)
                            {
                                userRoles[i].IsActive = false;
                                userRoles[i].ModifiedBy = "Ranga";
                                userRoles[i].ModifiedDate = dateTime;

                                db.Entry(userRoles[i]).State = EntityState.Modified;
                            }

                            db.SaveChanges();
                        }

                        if(userClaims.Count != 0)
                        {
                            for (int i = 0; i < userClaims.Count; i++)
                            {
                                userClaims[i].IsActive = false;
                                userClaims[i].ModifiedBy = "Ranga";
                                userClaims[i].ModifiedDate = dateTime;

                                db.Entry(userClaims[i]).State = EntityState.Modified;
                            }

                            db.SaveChanges();
                        }
                    }

                    return Json(new
                    {
                        success = true,
                        message = "Successfully Saved"
                    }, JsonRequestBehavior.AllowGet);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        ////Developed By:- Ranga Athapaththu
        ////Developed On:- 2022/10/01
        //public ActionResult ManageUserCategories()
        //{
        //    return View();
        //}

        ////Developed By:- Ranga Athapaththu
        ////Developed On:- 2022/10/01
        //public ActionResult GetUserCategories()
        //{
        //    using (PMSEntities db = new PMSEntities())
        //    {
        //        List<UserCategory> userCategoryList = (from uc in db.UserCategory orderby uc.Id descending select uc).ToList();
        //        return Json(new { data = userCategoryList }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        ////Developed By:- Ranga Athapaththu
        ////Developed On:- 2022/10/01
        //[HttpGet]
        //public ActionResult AddOrEditUserCategory(int id = 0)
        //{
        //    if (id == 0)
        //    {
        //        return View(new UserCategory());
        //    }
        //    else
        //    {
        //        using (PMSEntities db = new PMSEntities())
        //        {
        //            return View((from uc in db.UserCategory where uc.Id.Equals(id) select uc).FirstOrDefault<UserCategory>());
        //        }
        //    }
        //}

        ////Developed By:- Ranga Athapaththu
        ////Developed On:- 2022/10/01
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddOrEditUserCategory(UserCategory userCategory)
        //{
        //    using (PMSEntities db = new PMSEntities())
        //    {
        //        try
        //        {
        //            var dateTime = DateTime.Now;
        //            UserCategory validationRecord = (from uc in db.UserCategory where uc.CategoryName.Equals(userCategory.CategoryName) select uc).FirstOrDefault<UserCategory>();

        //            if (userCategory.Id == 0)
        //            {
        //                if (validationRecord != null)
        //                {
        //                    return Json(new
        //                    {
        //                        success = false,
        //                        message = "This User Category Already Exists"
        //                    }, JsonRequestBehavior.AllowGet);
        //                }
        //                else
        //                {
        //                    userCategory.CreatedBy = "Ranga";
        //                    userCategory.CreatedDate = dateTime;
        //                    userCategory.ModifiedBy = "Ranga";
        //                    userCategory.ModifiedDate = dateTime;

        //                    db.UserCategory.Add(userCategory);
        //                    db.SaveChanges();

        //                    return Json(new
        //                    {
        //                        success = true,
        //                        message = "Successfully Saved"
        //                    }, JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //            else
        //            {
        //                UserCategory editingUserCategory = (from uc in db.UserCategory where uc.Id.Equals(userCategory.Id) select uc).FirstOrDefault<UserCategory>();

        //                if (editingUserCategory.CategoryName != userCategory.CategoryName || editingUserCategory.Description != userCategory.Description || editingUserCategory.IsActive != userCategory.IsActive)
        //                {
        //                    if (validationRecord != null && validationRecord.Id != userCategory.Id)
        //                    {
        //                        return Json(new
        //                        {
        //                            success = false,
        //                            message = "This User Category Already Exists"
        //                        }, JsonRequestBehavior.AllowGet);
        //                    }
        //                    else
        //                    {
        //                        editingUserCategory.CategoryName = userCategory.CategoryName;
        //                        editingUserCategory.Description = userCategory.Description;
        //                        editingUserCategory.IsActive = userCategory.IsActive;
        //                        editingUserCategory.ModifiedBy = "Ranga";
        //                        editingUserCategory.ModifiedDate = dateTime;

        //                        db.Entry(editingUserCategory).State = EntityState.Modified;
        //                        db.SaveChanges();

        //                        return Json(new
        //                        {
        //                            success = true,
        //                            message = "Successfully Updated"
        //                        }, JsonRequestBehavior.AllowGet);
        //                    }
        //                }
        //                else
        //                {
        //                    return Json(new
        //                    {
        //                        success = false,
        //                        message = "You didn't make any new changes"
        //                    }, JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //        }
        //        catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
        //        {
        //            Exception raise = dbEx;
        //            foreach (var validationErrors in dbEx.EntityValidationErrors)
        //            {
        //                foreach (var validationError in validationErrors.ValidationErrors)
        //                {
        //                    string message = string.Format("{0}:{1}",
        //                        validationErrors.Entry.Entity.ToString(),
        //                        validationError.ErrorMessage);
        //                    raise = new InvalidOperationException(message, raise);
        //                }
        //            }
        //            throw raise;
        //        }
        //    }
        //}

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/01
        public ActionResult ManageAccessGroupClaims(int id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                AccessGroupClaimCC accessGroupClaimDetails = new AccessGroupClaimCC();
                accessGroupClaimDetails.AccessGroupId = id;

                accessGroupClaimDetails.AccessGroupName = (from ag in db.AccessGroup where ag.AccessGroupId.Equals(id) select ag.AccessGroupName).FirstOrDefault<string>();
                accessGroupClaimDetails.ClaimsList = (from c in db.Claim where c.IsActive.Equals(true) select c).ToList();
                accessGroupClaimDetails.SelectedAccessGroupClaims = (from agc in db.AccessGroupClaims
                                                                       join c in db.Claim on agc.ClaimId equals c.ClaimId
                                                                       where agc.AccessGroupId.Equals(id) && agc.IsActive.Equals(true)
                                                                       select c).ToList();

                return View(accessGroupClaimDetails);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/02
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditAccessGroupClaim(AccessGroupClaimCC accessGroupClaimObj)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    var passingClaimList = new JavaScriptSerializer().Deserialize<List<string>>(accessGroupClaimObj.passingAccessGroupClaimIds).ToList();
                    List<AccessGroupClaims> claimsForAccessGroup = (from agc in db.AccessGroupClaims where agc.AccessGroupId == accessGroupClaimObj.AccessGroupId select agc).ToList();

                    if (passingClaimList.Count != 0)
                    {
                        for (int i = 0; i < passingClaimList.Count; i++)
                        {
                            if (claimsForAccessGroup.Find(agc => agc.ClaimId == int.Parse(passingClaimList[i])) == null)
                            {
                                AccessGroupClaims accessGroupClaim = new AccessGroupClaims();
                                accessGroupClaim.AccessGroupId = accessGroupClaimObj.AccessGroupId;
                                accessGroupClaim.ClaimId = int.Parse(passingClaimList[i]);
                                accessGroupClaim.CreatedBy = "Ranga";
                                accessGroupClaim.CreatedDate = dateTime;
                                accessGroupClaim.ModifiedBy = "Ranga";
                                accessGroupClaim.ModifiedDate = dateTime;
                                accessGroupClaim.IsActive = true;

                                db.AccessGroupClaims.Add(accessGroupClaim);
                            }
                            else
                            {
                                int matchingIndex = claimsForAccessGroup.FindIndex(agc => agc.ClaimId == int.Parse(passingClaimList[i]));
                                if (claimsForAccessGroup[matchingIndex].IsActive == false)
                                {
                                    claimsForAccessGroup[matchingIndex].IsActive = true;
                                    claimsForAccessGroup[matchingIndex].ModifiedBy = "Ranga";
                                    claimsForAccessGroup[matchingIndex].ModifiedDate = dateTime;

                                    db.Entry(claimsForAccessGroup[matchingIndex]).State = EntityState.Modified;
                                }
                                claimsForAccessGroup.RemoveAt(claimsForAccessGroup.FindIndex(agc => agc.ClaimId == int.Parse(passingClaimList[i])));
                            }
                        }

                        db.SaveChanges();

                        if (claimsForAccessGroup.Count != 0)
                        {
                            for (int j = 0; j < claimsForAccessGroup.Count; j++)
                            {
                                claimsForAccessGroup[j].IsActive = false;
                                claimsForAccessGroup[j].ModifiedBy = "Ranga";
                                claimsForAccessGroup[j].ModifiedDate = dateTime;

                                db.Entry(claimsForAccessGroup[j]).State = EntityState.Modified;
                            }

                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        if (claimsForAccessGroup.Count != 0)
                        {
                            for (int i = 0; i < claimsForAccessGroup.Count; i++)
                            {
                                claimsForAccessGroup[i].IsActive = false;
                                claimsForAccessGroup[i].ModifiedBy = "Ranga";
                                claimsForAccessGroup[i].ModifiedDate = dateTime;

                                db.Entry(claimsForAccessGroup[i]).State = EntityState.Modified;
                            }

                            db.SaveChanges();
                        }
                    }

                    return Json(new
                    {
                        success = true,
                        message = "Successfully Saved"
                    }, JsonRequestBehavior.AllowGet);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/02
        public ActionResult ManageWorkflows()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/02
        public ActionResult GetWorkflows()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<WorkflowVM> workflowList = (from w in db.Workflows
                                                 join f in db.Faculty on w.FacultyId.Value equals f.FacultyId into w_f
                                                 from fac in w_f.DefaultIfEmpty()
                                                 select new WorkflowVM
                                                 {
                                                     Id = w.Id,
                                                     WorkflowName = w.WorkflowName,
                                                     Description = w.Description,
                                                     FacultyName = fac != null ? fac.FacultyName : null,
                                                     IsActive = w.IsActive
                                                 }).ToList();

                return Json(new { data = workflowList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/02
        [HttpGet]
        public ActionResult AddOrEditWorkflow(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var faculties = (from f in db.Faculty
                                 where f.IsActive.Equals(true)
                                 select new
                                 {
                                     Text = f.FacultyName,
                                     Value = f.FacultyId
                                 }).ToList();

                List<SelectListItem> facultyList = new SelectList(faculties, "Value", "Text").ToList();
                ViewBag.facultyList = facultyList;

                if (id == 0)
                {
                    return View(new Workflows());
                }
                else
                {
                    return View((from w in db.Workflows where w.Id.Equals(id) select w).FirstOrDefault<Workflows>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/02
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditWorkflow(Workflows workflowObj)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    Workflows validationRecord = (from w in db.Workflows
                                                  where w.WorkflowName.Equals(workflowObj.WorkflowName) && w.FacultyId.Value.Equals(workflowObj.FacultyId.Value)
                                                  select w).FirstOrDefault<Workflows>();

                    if (workflowObj.Id == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Workflow Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            workflowObj.CreatedBy = "Ranga";
                            workflowObj.CreatedDate = dateTime;
                            workflowObj.ModifiedBy = "Ranga";
                            workflowObj.ModifiedDate = dateTime;

                            db.Workflows.Add(workflowObj);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Workflows editingWorkflow = (from w in db.Workflows where w.Id.Equals(workflowObj.Id) select w).FirstOrDefault<Workflows>();

                        if (editingWorkflow.WorkflowName != workflowObj.WorkflowName || editingWorkflow.Description != workflowObj.Description
                            || editingWorkflow.FacultyId.Value != workflowObj.FacultyId.Value || editingWorkflow.IsActive != workflowObj.IsActive)
                        {
                            if (validationRecord != null && validationRecord.Id != workflowObj.Id)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Workflow Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingWorkflow.WorkflowName = workflowObj.WorkflowName;
                                editingWorkflow.Description = workflowObj.Description;
                                editingWorkflow.FacultyId = workflowObj.FacultyId.Value;
                                editingWorkflow.IsActive = workflowObj.IsActive;
                                editingWorkflow.ModifiedBy = "Ranga";
                                editingWorkflow.ModifiedDate = dateTime;

                                db.Entry(editingWorkflow).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/01
        public ActionResult ManageSubWorkflow()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:-2022/11/01
        public ActionResult GetSubWorkflows(int id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<SubWorkflowVM> subWorkflowsList = (from sw in db.SubWorkflows
                                                        join r in db.AspNetRoles on sw.WorkflowRole equals r.Id
                                                        join ag in db.AccessGroup on r.AccessGroupId equals ag.AccessGroupId
                                                        join u in db.AspNetUsers on sw.WorkflowUser equals u.Id into w_u
                                                        from usr in w_u.DefaultIfEmpty()
                                                        join t in db.Title on usr.EmployeeTitle equals t.TitleId into u_t
                                                        from ttl in u_t.DefaultIfEmpty()
                                                        where sw.WorkflowId.Equals(id)
                                                        orderby sw.SubWorkflowId descending
                                                        select new SubWorkflowVM
                                                        {
                                                            SubWorkflowId = sw.SubWorkflowId,
                                                            WorkflowId = sw.WorkflowId,
                                                            WorkflowRole = ag.AccessGroupName + " - " + r.Name,
                                                            WorkflowStep = sw.WorkflowStep,
                                                            ConsideringArea = sw.ConsideringArea,
                                                            IsSpecificUser = sw.IsSpecificUser,
                                                            WorkflowUser = usr != null ? ttl.TitleName + " " + usr.FirstName + " " + usr.LastName : null,
                                                            IsActive = sw.IsActive
                                                        }).ToList();

                return Json(new { data = subWorkflowsList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/01
        [HttpGet]
        public ActionResult AddOrEditSubWorkflow(int id = 0, int operation = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var roles = (from r in db.AspNetRoles
                             join ag in db.AccessGroup on r.AccessGroupId equals ag.AccessGroupId
                             where r.IsActive.Equals(true)
                             orderby ag.AccessGroupId ascending
                             select new
                             {
                                 Text = ag.AccessGroupName + " - " + r.Name,
                                 Value = r.Id
                             }).ToList();

                List<SelectListItem> roleList = new SelectList(roles, "Value", "Text").ToList();
                ViewBag.roleList = roleList;

                var workFlows = (from sw in db.SubWorkflows where sw.WorkflowId.Equals(id) && sw.IsActive.Equals(true) select sw.WorkflowStep).ToList();
                ViewBag.activeWorkflowCount = workFlows.Count;

                var users = (from u in db.AspNetUsers
                             join t in db.Title on u.EmployeeTitle equals t.TitleId
                             where u.IsActive.Equals(true)
                             select new
                             {
                                 Text = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                 Value = u.Id
                             }).ToList();

                List<SelectListItem> usersList = new SelectList(users, "Value", "Text").ToList();
                ViewBag.usersList = usersList;

                if (operation == 0)
                {
                    return View(new SubWorkflowCC() { WorkflowId = id });
                }
                else
                {
                    SubWorkflowCC subWorkflowRecord = (from sw in db.SubWorkflows
                                                       where sw.SubWorkflowId.Equals(operation)
                                                       select new SubWorkflowCC
                                                       {
                                                           SubWorkflowId = sw.SubWorkflowId,
                                                           WorkflowId = sw.WorkflowId,
                                                           WorkflowRole = sw.WorkflowRole,
                                                           CurrentPosition = sw.WorkflowStep,
                                                           ConsideringArea = sw.ConsideringArea,
                                                           IsSpecificUser = sw.IsSpecificUser,
                                                           IsActive = sw.IsActive
                                                       }).FirstOrDefault<SubWorkflowCC>();

                    return View(subWorkflowRecord);
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/01
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditSubWorkflow(SubWorkflowCC workflowCC)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    SubWorkflows validationRecord = (from sw in db.SubWorkflows where sw.WorkflowId.Equals(workflowCC.WorkflowId) && sw.WorkflowRole.Equals(workflowCC.WorkflowRole) select sw).FirstOrDefault<SubWorkflows>();

                    if (workflowCC.SubWorkflowId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Workflow Item Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            SubWorkflows newSubWorkFlow = new SubWorkflows();
                            newSubWorkFlow.WorkflowId = workflowCC.WorkflowId;
                            newSubWorkFlow.WorkflowRole = workflowCC.WorkflowRole;

                            if (workflowCC.IsInitial == true)
                            {
                                newSubWorkFlow.WorkflowStep = 1;
                            }
                            else
                            {
                                int landingWorkflowStep = (from sw in db.SubWorkflows where sw.WorkflowId.Equals(workflowCC.WorkflowId) && sw.WorkflowRole.Equals(workflowCC.LandingRole) select sw.WorkflowStep).FirstOrDefault<int>();

                                if (workflowCC.Prefix == "Before")
                                {
                                    if(landingWorkflowStep == 1)
                                    {
                                        newSubWorkFlow.WorkflowStep = 1;
                                    }
                                    else
                                    {
                                        newSubWorkFlow.WorkflowStep = landingWorkflowStep - 1;
                                    }

                                    List<SubWorkflows> tarversingWorkflows = (from sw in db.SubWorkflows where sw.WorkflowId.Equals(workflowCC.WorkflowId) && (sw.IsActive.Equals(true)) && (sw.WorkflowStep >= landingWorkflowStep) select sw).ToList();

                                    for (var i = 0; i < tarversingWorkflows.Count; i++)
                                    {
                                        tarversingWorkflows[i].WorkflowStep = tarversingWorkflows[i].WorkflowStep + 1;
                                        tarversingWorkflows[i].ModifiedDate = dateTime;
                                        tarversingWorkflows[i].ModifiedBy = "Ranga";

                                        db.Entry(tarversingWorkflows[i]).State = EntityState.Modified;
                                    }
                                }
                                else if(workflowCC.Prefix == "On")
                                {
                                    newSubWorkFlow.WorkflowStep = landingWorkflowStep;
                                }
                                else
                                {
                                    newSubWorkFlow.WorkflowStep = landingWorkflowStep + 1;

                                    List<SubWorkflows> tarversingWorkflows = (from sw in db.SubWorkflows where sw.WorkflowId.Equals(workflowCC.WorkflowId) && (sw.IsActive.Equals(true)) && (sw.WorkflowStep > landingWorkflowStep) select sw).ToList();

                                    for (var i = 0; i < tarversingWorkflows.Count; i++)
                                    {
                                        tarversingWorkflows[i].WorkflowStep = tarversingWorkflows[i].WorkflowStep + 1;
                                        tarversingWorkflows[i].ModifiedDate = dateTime;
                                        tarversingWorkflows[i].ModifiedBy = "Ranga";

                                        db.Entry(tarversingWorkflows[i]).State = EntityState.Modified;
                                    }
                                }
                            }

                            newSubWorkFlow.ConsideringArea = workflowCC.ConsideringArea;
                            newSubWorkFlow.IsSpecificUser = workflowCC.IsSpecificUser;
                            newSubWorkFlow.WorkflowUser = workflowCC.WorkflowUser;
                            newSubWorkFlow.IsActive = workflowCC.IsActive;
                            newSubWorkFlow.CreatedBy = "Ranga";
                            newSubWorkFlow.CreatedDate = dateTime;
                            newSubWorkFlow.ModifiedBy = "Ranga";
                            newSubWorkFlow.ModifiedDate = dateTime;

                            db.SubWorkflows.Add(newSubWorkFlow);
                            db.SaveChanges();

                            this.ReArrangeWorkflow(db, workflowCC.WorkflowId, dateTime);

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        SubWorkflows editingSubWorkflow = (from sw in db.SubWorkflows where sw.SubWorkflowId.Equals(workflowCC.SubWorkflowId) select sw).FirstOrDefault<SubWorkflows>();

                        if (validationRecord != null && validationRecord.SubWorkflowId != workflowCC.SubWorkflowId)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Workflow Item Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (workflowCC.Prefix != "" && workflowCC.LandingRole != null)
                            {
                                int landingWorkflowStep = (from sw in db.SubWorkflows where sw.WorkflowId.Equals(workflowCC.WorkflowId) && sw.WorkflowRole.Equals(workflowCC.LandingRole) select sw.WorkflowStep).FirstOrDefault<int>();

                                if (workflowCC.Prefix == "Before")
                                {
                                    editingSubWorkflow.WorkflowStep = landingWorkflowStep;

                                    //editingWorkflow.ModifiedDate = dateTime;
                                    //editingWorkflow.ModifiedBy = "Ranga";

                                    //db.Entry(editingWorkflow).State = EntityState.Modified;
                                    //db.SaveChanges();

                                    List<SubWorkflows> tarversingWorkflows = (from sw in db.SubWorkflows where (sw.IsActive.Equals(true)) && (sw.SubWorkflowId != workflowCC.SubWorkflowId) && (sw.WorkflowStep >= landingWorkflowStep) select sw).ToList();

                                    for (var i = 0; i < tarversingWorkflows.Count; i++)
                                    {
                                        int wfStep = tarversingWorkflows[i].WorkflowStep;
                                        tarversingWorkflows[i].WorkflowStep = wfStep + 1;
                                        tarversingWorkflows[i].ModifiedDate = dateTime;
                                        tarversingWorkflows[i].ModifiedBy = "Ranga";

                                        db.Entry(tarversingWorkflows[i]).State = EntityState.Modified;
                                    }

                                    //db.SaveChanges();
                                }
                                else if (workflowCC.Prefix == "On")
                                {
                                    editingSubWorkflow.WorkflowStep = landingWorkflowStep;
                                    //editingWorkflow.ModifiedDate = dateTime;
                                    //editingWorkflow.ModifiedBy = "Ranga";

                                    //db.Entry(editingWorkflow).State = EntityState.Modified;
                                    //db.SaveChanges();
                                }
                                else
                                {
                                    editingSubWorkflow.WorkflowStep = landingWorkflowStep + 1;
                                    //editingWorkflow.ModifiedDate = dateTime;
                                    //editingWorkflow.ModifiedBy = "Ranga";

                                    //db.Entry(editingWorkflow).State = EntityState.Modified;
                                    //db.SaveChanges();

                                    List<SubWorkflows> tarversingWorkflows = (from sw in db.SubWorkflows where (sw.IsActive.Equals(true)) && (sw.SubWorkflowId != workflowCC.SubWorkflowId) && (sw.WorkflowStep > landingWorkflowStep) select sw).ToList();

                                    for (var i = 0; i < tarversingWorkflows.Count; i++)
                                    {
                                        int wfStep = tarversingWorkflows[i].WorkflowStep;
                                        tarversingWorkflows[i].WorkflowStep = wfStep + 1;
                                        tarversingWorkflows[i].ModifiedDate = dateTime;
                                        tarversingWorkflows[i].ModifiedBy = "Ranga";

                                        db.Entry(tarversingWorkflows[i]).State = EntityState.Modified;
                                    }

                                    //db.SaveChanges();
                                }

                                editingSubWorkflow.ConsideringArea = workflowCC.ConsideringArea;
                                editingSubWorkflow.IsSpecificUser = workflowCC.IsSpecificUser;
                                editingSubWorkflow.WorkflowUser = workflowCC.WorkflowUser;
                                editingSubWorkflow.IsActive = workflowCC.IsActive;
                                editingSubWorkflow.ModifiedDate = dateTime;
                                editingSubWorkflow.ModifiedBy = "Ranga";

                                db.Entry(editingSubWorkflow).State = EntityState.Modified;
                                db.SaveChanges();
                                //db.Entry(editingWorkflow).State = EntityState.Modified;

                                this.ReArrangeWorkflow(db, workflowCC.WorkflowId, dateTime);

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (editingSubWorkflow.ConsideringArea != workflowCC.ConsideringArea || editingSubWorkflow.IsSpecificUser != workflowCC.IsSpecificUser || editingSubWorkflow.WorkflowUser != workflowCC.WorkflowUser || editingSubWorkflow.IsActive != workflowCC.IsActive)
                                {
                                    editingSubWorkflow.ConsideringArea = workflowCC.ConsideringArea;
                                    editingSubWorkflow.IsSpecificUser = workflowCC.IsSpecificUser;
                                    editingSubWorkflow.WorkflowUser = workflowCC.WorkflowUser;
                                    editingSubWorkflow.IsActive = workflowCC.IsActive;
                                    editingSubWorkflow.ModifiedBy = "Ranga";
                                    editingSubWorkflow.ModifiedDate = dateTime;

                                    db.Entry(editingSubWorkflow).State = EntityState.Modified;
                                    db.SaveChanges();

                                    this.ReArrangeWorkflow(db, workflowCC.WorkflowId, dateTime);

                                    return Json(new
                                    {
                                        success = true,
                                        message = "Successfully Updated"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "You didn't make any new changes"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/09
        [HttpPost]
        public void ReArrangeWorkflow(PMSEntities dbConnection, int workflowId, DateTime dateTime)
        {
            List<SubWorkflows> allWorkFlows = (from sw in dbConnection.SubWorkflows
                                               where sw.WorkflowId.Equals(workflowId) && sw.IsActive.Equals(true)
                                               orderby sw.WorkflowStep ascending
                                               select sw).ToList();

            if(allWorkFlows.Count != 0)
            {
                var firstWorkflowItemVal = allWorkFlows[0].WorkflowStep;

                if(firstWorkflowItemVal != 1)
                {
                    allWorkFlows[0].WorkflowStep = 1;
                    allWorkFlows[0].ModifiedBy = "Ranga";
                    allWorkFlows[0].ModifiedDate = dateTime;

                    dbConnection.Entry(allWorkFlows[0]).State = EntityState.Modified;
                }

                List<SubWorkflows> matchingWorkflows = allWorkFlows.FindAll(w => w.WorkflowStep == firstWorkflowItemVal);

                for(var i = 0; i < matchingWorkflows.Count; i++)
                {
                    matchingWorkflows[i].WorkflowStep = 1;
                    matchingWorkflows[i].ModifiedBy = "Ranga";
                    matchingWorkflows[i].ModifiedDate = dateTime;

                    dbConnection.Entry(matchingWorkflows[i]).State = EntityState.Modified;
                }

                //dbConnection.SaveChanges();

                int nextVal = 1;
                int previousVal = 1;

                for (var i = 0; i < allWorkFlows.Count; i++)
                {
                    if (allWorkFlows[i].WorkflowStep > nextVal)
                    {
                        nextVal += 1;

                        allWorkFlows[i].WorkflowStep = nextVal;
                        allWorkFlows[i].ModifiedBy = "Ranga";
                        allWorkFlows[i].ModifiedDate = dateTime;

                        dbConnection.Entry(allWorkFlows[i]).State = EntityState.Modified;
                    }

                    previousVal = allWorkFlows[i].WorkflowStep;
                }

                dbConnection.SaveChanges();
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/10
        [HttpGet]
        public ActionResult ViewSubWorkFlowMap(int id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<SubWorkflowVM> subWorkflowList = (from sw in db.SubWorkflows
                                                       where sw.WorkflowId.Equals(id) && sw.IsActive.Equals(true)
                                                       group sw by sw.WorkflowStep into g
                                                       select new SubWorkflowVM
                                                       {
                                                           WorkflowStep = g.Key,
                                                           WorkflowMapRoles = (from wf in db.SubWorkflows
                                                                     join r in db.AspNetRoles on wf.WorkflowRole equals r.Id
                                                                     join ag in db.AccessGroup on r.AccessGroupId equals ag.AccessGroupId
                                                                     where wf.WorkflowId.Equals(id) && wf.WorkflowStep.Equals(g.Key) && wf.IsActive.Equals(true)
                                                                     select " " + r.Name).ToList()
                                                       }).ToList();

                return Json(subWorkflowList, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/12
        public ActionResult ManageLecturerAssignments(int id)
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/12
        public ActionResult GetLecturerAssignments(int id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<LecturerAssignmentsVM> lecturerAssignmentsList = (from la in db.LecturerAssignments
                                                                       join sr in db.SemesterRegistration on la.SemesterId equals sr.SemesterId
                                                                       join u in db.AspNetUsers on la.LecturerId equals u.Id
                                                                       join t in db.Title on u.EmployeeTitle equals t.TitleId
                                                                       join ss in db.SemesterSubject on la.SemesterSubjectId equals ss.Id
                                                                       join s in db.Subject on ss.SubjectId equals s.SubjectId
                                                                       select new LecturerAssignmentsVM {
                                                                           Id = la.Id,
                                                                           SemesterId = la.SemesterId,
                                                                           LecturerName = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                                                           SubjectName = s.SubjectCode + " " + s.SubjectName,
                                                                           StudentBatches = la.StudentBatches,
                                                                           IsActive = la.IsActive
                                                                       }).ToList();

                return Json(new { data = lecturerAssignmentsList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/13
        //[HttpGet]
        //public ActionResult AddOrEditLecturerAssignment(int id = 0, int additionalId = 0)
        //{
        //    using (PMSEntities db = new PMSEntities())
        //    {
        //        var users = (from u in db.AspNetUsers
        //                     join t in db.Title on u.EmployeeTitle equals t.TitleId
        //                     where u.IsActive.Equals(true)
        //                     select new
        //                     {
        //                         Text = t.TitleName + " " + u.FirstName + " " + u.LastName,
        //                         Value = u.Id
        //                     }).ToList();

        //        List<SelectListItem> usersList = new SelectList(users, "Value", "Text").ToList();
        //        ViewBag.usersList = usersList;

        //        var subjects = (from ss in db.SemesterSubject
        //                        join s in db.Subject on ss.SubjectId equals s.SubjectId
        //                        where ss.IsActive.Equals(true) && ss.SemesterRegistrationId.Equals(additionalId)
        //                        select new
        //                        {
        //                            Text = s.SubjectCode + " - " + s.SubjectName,
        //                            Value = ss.Id
        //                        }).ToList();

        //        List<SelectListItem> subjectsList = new SelectList(subjects, "Value", "Text").ToList();
        //        ViewBag.subjectsList = subjectsList;

        //        var studentBatches = (from sb in db.StudentBatch
        //                              join s in db.SemesterRegistration on sb.SemesterRegistrationId equals s.SemesterId
        //                              where sb.IsActive.Equals(true) && sb.SemesterRegistrationId.Equals(additionalId)
        //                              select new
        //                              {
        //                                  Text = sb.BatchName,
        //                                  Value = sb.BatchName
        //                              }).ToList();

        //        List<SelectListItem> studentBatchesList = new SelectList(studentBatches, "Value", "Text").ToList();
        //        ViewBag.studentBatchesList = studentBatchesList;

        //        if (id == 0)
        //        {
        //            return View(new LecturerAssignments());
        //        }
        //        else
        //        {
        //            WorkflowCC workflowRecord = (from w in db.Workflow
        //                                         where w.Id.Equals(id)
        //                                         select new WorkflowCC
        //                                         {
        //                                             Id = w.Id,
        //                                             WorkflowRole = w.WorkflowRole,
        //                                             CurrentPosition = w.WorkflowStep,
        //                                             IsSpecificUser = w.IsSpecificUser,
        //                                             IsActive = w.IsActive
        //                                         }).FirstOrDefault<WorkflowCC>();

        //            return View(workflowRecord);
        //        }
        //    }
        //}

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/18
        public ActionResult ManageSemesterTimetable(int id)
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/18
        public ActionResult GetSemesterTimetable(int id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<SemesterTimetableVM> semesterTimetableRecordsList = (from tt in db.LectureTimetable
                                                                          join s in db.SemesterRegistration on tt.SemesterId equals s.SemesterId
                                                                          join ss in db.SemesterSubject on tt.SemesterSubjectId equals ss.Id
                                                                          join sub in db.Subject on ss.SubjectId equals sub.SubjectId
                                                                          join lt in db.LectureType on tt.LectureTypeId equals lt.LectureTypeId
                                                                          join lh in db.LectureHall on tt.LocationId equals lh.HallId into tt_lh
                                                                          from hll in tt_lh.DefaultIfEmpty()
                                                                          join c in db.Campus on hll.CampusId equals c.CampusId
                                                                          join u in db.AspNetUsers on tt.LecturerId equals u.Id into tt_u
                                                                          from usr in tt_u.DefaultIfEmpty()
                                                                          join ttl in db.Title on usr.EmployeeTitle equals ttl.TitleId
                                                                          where tt.SemesterId.Equals(id)
                                                                          orderby tt.TimetableId descending
                                                                          select new SemesterTimetableVM
                                                                          {
                                                                              TimetableId = tt.TimetableId,
                                                                              SubjectName = sub.SubjectCode + " - " + sub.SubjectName,
                                                                              LectureDate = tt.LectureDate.ToString(),
                                                                              FromTime = tt.FromTime.ToString().Substring(0,5),
                                                                              ToTime = tt.ToTime.ToString().Substring(0, 5),
                                                                              Location = hll != null ? c.CampusName + " - " + hll.Building + " - " + hll.Floor + " - " + hll.HallName : null,
                                                                              LectureTypeName = lt.LectureTypeName,
                                                                              LecturerName = usr != null ? ttl.TitleName + " " + usr.FirstName + " " + usr.LastName : null,
                                                                              StudentBatches = tt.StudentBatches,
                                                                              IsActive = tt.IsActive
                                                                          }).ToList();

                return Json(new { data = semesterTimetableRecordsList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/22
        public void DownloadSemesterTimetableFormat(int id)
        {
            ExcelPackage ep = new ExcelPackage();
            ExcelWorksheet ttSheet = ep.Workbook.Worksheets.Add("Semester Timetable");
            ttSheet.Cells["A1"].Value = "Subject Code";
            ttSheet.Cells["B1"].Value = "Start Date";
            ttSheet.Cells["C1"].Value = "Time From";
            ttSheet.Cells["D1"].Value = "Time To";
            ttSheet.Cells["E1"].Value = "Lecture Type";
            ttSheet.Cells["F1"].Value = "Location";
            ttSheet.Cells["G1"].Value = "Lecturer";
            ttSheet.Cells["H1"].Value = "Student Batches";

            var ttHeaderCells = ttSheet.Cells[1, 1, 1, ttSheet.Dimension.Columns];
            ttHeaderCells.Style.Font.Bold = true;

            ttSheet.Column(1).AutoFit();
            ttSheet.Column(2).AutoFit();
            ttSheet.Column(3).AutoFit();
            ttSheet.Column(4).AutoFit();
            ttSheet.Column(5).AutoFit();
            ttSheet.Column(6).AutoFit();
            ttSheet.Column(7).AutoFit();
            ttSheet.Column(8).AutoFit();

            using (PMSEntities db = new PMSEntities())
            {
                var semesterSubjects = (from ss in db.SemesterSubject
                                        join s in db.Subject on ss.SubjectId equals s.SubjectId
                                        where ss.SemesterRegistrationId.Equals(id) && ss.IsActive.Equals(true) && s.IsActive.Equals(true)
                                        select s).ToList();

                ExcelWorksheet sSheet = ep.Workbook.Worksheets.Add("Semester Subjects");
                sSheet.Cells["A1"].Value = "Subject Code";
                sSheet.Cells["B1"].Value = "Subject Name";

                sSheet.Cells[1, 1, 1, sSheet.Dimension.Columns].Style.Font.Bold = true;

                var sRowIndex = 2;

                foreach(var sub in semesterSubjects)
                {
                    sSheet.Cells[sRowIndex, 1].Value = sub.SubjectCode;
                    sSheet.Cells[sRowIndex, 2].Value = sub.SubjectName;
                    sRowIndex++;
                }

                sSheet.Column(1).AutoFit();
                sSheet.Column(2).AutoFit();

                var lectureTypes = (from lt in db.LectureType where lt.IsActive.Equals(true) select lt).ToList();

                ExcelWorksheet ltSheet = ep.Workbook.Worksheets.Add("Lecture types");
                ltSheet.Cells["A1"].Value = "Lecture Type Name";

                ltSheet.Cells[1, 1, 1, ltSheet.Dimension.Columns].Style.Font.Bold = true;

                var ltRowIndex = 2;

                foreach (var typ in lectureTypes)
                {
                    ltSheet.Cells[ltRowIndex, 1].Value = typ.LectureTypeName;
                    ltRowIndex++;
                }

                ltSheet.Column(1).AutoFit();

                var lectureHalls = (from h in db.LectureHall
                                    join c in db.Campus on h.CampusId equals c.CampusId
                                    where h.IsActive.Equals(true) select new {
                                        hall = h,
                                        belongCampus = c
                                    }).ToList();

                ExcelWorksheet lhSheet = ep.Workbook.Worksheets.Add("Locations (Lecture Halls)");
                lhSheet.Cells["A1"].Value = "Location Id";
                lhSheet.Cells["B1"].Value = "Campus";
                lhSheet.Cells["C1"].Value = "Building";
                lhSheet.Cells["D1"].Value = "Floor";
                lhSheet.Cells["E1"].Value = "Hall Name";

                lhSheet.Cells[1, 1, 1, lhSheet.Dimension.Columns].Style.Font.Bold = true;

                var lhRowIndex = 2;

                foreach (var hllObj in lectureHalls)
                {
                    lhSheet.Cells[lhRowIndex, 1].Value = hllObj.hall.HallId;
                    lhSheet.Cells[lhRowIndex, 2].Value = hllObj.belongCampus.CampusName;
                    lhSheet.Cells[lhRowIndex, 3].Value = hllObj.hall.Building;
                    lhSheet.Cells[lhRowIndex, 4].Value = hllObj.hall.Floor;
                    lhSheet.Cells[lhRowIndex, 5].Value = hllObj.hall.HallName;
                    lhRowIndex++;
                }

                lhSheet.Column(1).AutoFit();
                lhSheet.Column(2).AutoFit();
                lhSheet.Column(3).AutoFit();
                lhSheet.Column(4).AutoFit();
                lhSheet.Column(5).AutoFit();

                var lecturers = (from a in db.Appointment
                                 join u in db.AspNetUsers on a.UserId equals u.Id
                                 join t in db.Title on u.EmployeeTitle equals t.TitleId
                                 where a.IsActive.Equals(true) && u.IsActive.Equals(true)
                                 select new
                                 {
                                     empNumber = u.EmployeeNumber,
                                     name = t.TitleName + " " + u.FirstName + " " + u.LastName
                                 }).Distinct().OrderBy(u => u.empNumber).ToList();

                ExcelWorksheet lecSheet = ep.Workbook.Worksheets.Add("Lecturers");
                lecSheet.Cells["A1"].Value = "Employee Number";
                lecSheet.Cells["B1"].Value = "Name";

                lecSheet.Cells[1, 1, 1, lecSheet.Dimension.Columns].Style.Font.Bold = true;

                var lecRowIndex = 2;

                foreach (var lecturer in lecturers)
                {
                    lecSheet.Cells[lecRowIndex, 1].Value = lecturer.empNumber;
                    lecSheet.Cells[lecRowIndex, 2].Value = lecturer.name;
                    lecRowIndex++;
                }

                lecSheet.Column(1).AutoFit();
                lecSheet.Column(2).AutoFit();

                var studentBatches = (from sb in db.StudentBatch where sb.SemesterRegistrationId.Equals(id) && sb.IsActive.Equals(true) select sb).ToList();

                ExcelWorksheet sbSheet = ep.Workbook.Worksheets.Add("Student Batches");
                sbSheet.Cells["A1"].Value = "Student Batch Id";
                sbSheet.Cells["B1"].Value = "Student Batch Name";

                sbSheet.Cells[1, 1, 1, sbSheet.Dimension.Columns].Style.Font.Bold = true;

                var sbRowIndex = 2;

                foreach (var batch in studentBatches)
                {
                    sbSheet.Cells[sbRowIndex, 1].Value = batch.StudentBatchId;
                    sbSheet.Cells[sbRowIndex, 2].Value = batch.BatchName;
                    sbRowIndex++;
                }

                sbSheet.Column(1).AutoFit();
                sbSheet.Column(2).AutoFit();
            }

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename=SemesterTimetable.xlsx");
            Response.BinaryWrite(ep.GetAsByteArray());
            Response.End();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/22
        [HttpPost]
        public ActionResult UploadSemesterTimetable(SemesterTimetableCC stCC)
        {
            using (PMSEntities db = new PMSEntities())
            {
                if(stCC.UploadedFile != null)
                {
                    if(stCC.UploadedFile.FileName.EndsWith(".xlsx") || stCC.UploadedFile.FileName.EndsWith(".xls"))
                    {
                        Stream stream = stCC.UploadedFile.InputStream;
                        IExcelDataReader reader = null;

                        if (stCC.UploadedFile.FileName.EndsWith(".xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }

                        int fieldCount = reader.FieldCount;
                        int rowCount = reader.RowCount;

                        DataTable dt = new DataTable();
                        DataRow dtRow;

                        DataTable dt_ = new DataTable();

                        dt_ = reader.AsDataSet().Tables["Semester Timetable"];

                        if(dt_ != null)
                        {
                            for (int i = 0; i < dt_.Columns.Count; i++)
                            {
                                dt.Columns.Add(dt_.Rows[0][i].ToString());
                            }

                            int rowCounter = 0;

                            for (int row = 1; row < dt_.Rows.Count; row++)
                            {
                                dtRow = dt.NewRow();

                                for (int col = 0; col < dt_.Columns.Count; col++)
                                {
                                    dtRow[col] = dt_.Rows[row][col].ToString().Trim();
                                    rowCounter++;
                                }
                                dt.Rows.Add(dtRow);
                            }

                            if(dt.Rows.Count != 0)
                            {
                                foreach(DataRow row in dt.Rows)
                                {
                                    row.SetField(1, row[1].ToString().Trim().Split(' ')[0]);
                                    var st = row[2].ToString().Trim().Split(' ')[1];
                                    row.SetField(2, st.Substring(0, st.LastIndexOf(":00")));
                                    var et = row[3].ToString().Trim().Split(' ')[1];
                                    row.SetField(3, et.Substring(0, et.LastIndexOf(":00")));
                                }

                                DataColumnCollection columns = dt.Columns;

                                if(columns.Contains("Subject Code") && columns.Contains("Start Date") && columns.Contains("Time From")
                                    && columns.Contains("Time To") && columns.Contains("Lecture Type") && columns.Contains("Location")
                                    && columns.Contains("Lecturer") && columns.Contains("Student Batches"))
                                {
                                    bool errorsFound = false;

                                    SemesterRegistration semReg = (from sr in db.SemesterRegistration where sr.SemesterId.Equals(stCC.SemesterId) select sr).FirstOrDefault<SemesterRegistration>();

                                    var semesterSubjectsList = (from ss in db.SemesterSubject
                                                                join s in db.Subject on ss.SubjectId equals s.SubjectId
                                                                where ss.SemesterRegistrationId.Equals(stCC.SemesterId) && ss.IsActive.Equals(true) && s.IsActive.Equals(true)
                                                                select new {
                                                                    semesterSubject = ss,
                                                                    subject = s
                                                                }).ToList();

                                    List<LectureType> lectureTypesList = (from lt in db.LectureType where lt.IsActive.Equals(true) select lt).ToList();

                                    List<LectureHall> lectureHallsList = (from lh in db.LectureHall where lh.IsActive.Equals(true) select lh).ToList();

                                    List<AspNetUsers> lecturersList = (from a in db.Appointment
                                                                       join u in db.AspNetUsers on a.UserId equals u.Id
                                                                       where a.IsActive.Equals(true) && u.IsActive.Equals(true)
                                                                       select u).Distinct().ToList();

                                    List<string> studentBatchesList = (from sb in db.StudentBatch
                                                                       where sb.SemesterRegistrationId.Equals(stCC.SemesterId) && sb.IsActive.Equals(true)
                                                                       select sb.BatchName).ToList();

                                    ExcelPackage ep = new ExcelPackage();
                                    ExcelWorksheet ttSheet = ep.Workbook.Worksheets.Add("Semester Timetable");
                                    ttSheet.Cells["A1"].LoadFromDataTable(dt, true);
                                    ttSheet.Cells["I1"].Value = "Errors";

                                    var ttHeaderCells = ttSheet.Cells[1, 1, 1, ttSheet.Dimension.Columns];
                                    ttHeaderCells.Style.Font.Bold = true;

                                    int index = 2;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (String.IsNullOrEmpty(row["Subject Code"].ToString().Trim()) || String.IsNullOrEmpty(row["Start Date"].ToString().Trim())
                                            || String.IsNullOrEmpty(row["Time From"].ToString().Trim()) || String.IsNullOrEmpty(row["Time To"].ToString().Trim())
                                            || String.IsNullOrEmpty(row["Lecture Type"].ToString().Trim()) || String.IsNullOrEmpty(row["Location"].ToString().Trim())
                                            || String.IsNullOrEmpty(row["Lecturer"].ToString().Trim()) || String.IsNullOrEmpty(row["Student Batches"].ToString().Trim()))
                                        {
                                            errorsFound = true;

                                            if (String.IsNullOrEmpty(row["Subject Code"].ToString().Trim()))
                                            {
                                                ttSheet.Cells[index, 1].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Start Date"].ToString().Trim()))
                                            {
                                                ttSheet.Cells[index, 2].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Time From"].ToString().Trim()))
                                            {
                                                ttSheet.Cells[index, 3].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Time To"].ToString().Trim()))
                                            {
                                                ttSheet.Cells[index, 4].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Lecture Type"].ToString().Trim()))
                                            {
                                                ttSheet.Cells[index, 5].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Location"].ToString().Trim()))
                                            {
                                                ttSheet.Cells[index, 6].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Lecturer"].ToString().Trim()))
                                            {
                                                ttSheet.Cells[index, 7].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Student Batches"].ToString().Trim()))
                                            {
                                                ttSheet.Cells[index, 8].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }

                                            ttSheet.Cells["I" + index].Value = "Null or Empty Records Found";
                                            ttSheet.Cells[index, 9].Style.Font.Color.SetColor(Color.Red);
                                        }
                                        else
                                        {
                                            var errorMessage = "";
                                            DateTime startDate;
                                            TimeSpan fromTime, toTime;
                                            List<string> uploadedStudentBatches = row["Student Batches"].ToString().Trim().Split(',').Select(b => b.Trim()).ToList();

                                            if (semesterSubjectsList.Find(s => s.subject.SubjectCode == row["Subject Code"].ToString().Trim()) == null)
                                            {
                                                errorsFound = true;
                                                ttSheet.Cells[index, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                ttSheet.Cells[index, 1].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                                errorMessage += "Subject not found for selected semester";
                                            }

                                            if (!DateTime.TryParseExact(row["Start Date"].ToString().Trim(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
                                            {
                                                errorsFound = true;
                                                ttSheet.Cells[index, 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                ttSheet.Cells[index, 2].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Start Date is not valid";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Start Date is not valid";
                                                }
                                            }
                                            else
                                            {
                                                startDate = Convert.ToDateTime(row["Start Date"].ToString().Trim());

                                                if (startDate < semReg.FromDate)
                                                {
                                                    errorsFound = true;
                                                    ttSheet.Cells[index, 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                    ttSheet.Cells[index, 2].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                    if (errorMessage == "")
                                                    {
                                                        errorMessage += "Start Date is less than Semester Registration start date";
                                                    }
                                                    else
                                                    {
                                                        errorMessage += ", Start Date is less than Semester Registration start date";
                                                    }
                                                }
                                                if (startDate > semReg.ToDate)
                                                {
                                                    errorsFound = true;
                                                    ttSheet.Cells[index, 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                    ttSheet.Cells[index, 2].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                    if (errorMessage == "")
                                                    {
                                                        errorMessage += "Semester Registration end date has been passed";
                                                    }
                                                    else
                                                    {
                                                        errorMessage += ", Semester Registration end date has been passed";
                                                    }
                                                }
                                            }

                                            if (!TimeSpan.TryParse(row["Time From"].ToString().Trim(), out fromTime))
                                            {
                                                errorsFound = true;
                                                ttSheet.Cells[index, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                ttSheet.Cells[index, 3].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Time From is not valid";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Time From is not valid";
                                                }
                                            }
                                            else
                                            {
                                                fromTime = TimeSpan.Parse(row["Time From"].ToString().Trim());
                                            }

                                            if (!TimeSpan.TryParse(row["Time To"].ToString().Trim(), out toTime))
                                            {
                                                errorsFound = true;
                                                ttSheet.Cells[index, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                ttSheet.Cells[index, 4].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Time To is not valid";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Time To is not valid";
                                                }
                                            }
                                            else
                                            {
                                                toTime = TimeSpan.Parse(row["Time To"].ToString().Trim());
                                            }

                                            if (fromTime >= toTime)
                                            {
                                                errorsFound = true;
                                                ttSheet.Cells[index, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                ttSheet.Cells[index, 3].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Time From must be less than Time To";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Time From must be less than Time To";
                                                }
                                            }

                                            if (lectureTypesList.Find(lt => lt.LectureTypeName == row["Lecture Type"].ToString().Trim()) == null)
                                            {
                                                errorsFound = true;
                                                ttSheet.Cells[index, 5].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                ttSheet.Cells[index, 5].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Lecture Type not found";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Lecture Type not found";
                                                }
                                            }
                                            if (lectureHallsList.Find(lh => lh.HallId == int.Parse(row["Location"].ToString().Trim())) == null)
                                            {
                                                errorsFound = true;
                                                ttSheet.Cells[index, 6].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                ttSheet.Cells[index, 6].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Location not found";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Location not found";
                                                }
                                            }
                                            if (lecturersList.Find(l => l.EmployeeNumber == row["Lecturer"].ToString().Trim()) == null)
                                            {
                                                errorsFound = true;
                                                ttSheet.Cells[index, 7].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                ttSheet.Cells[index, 7].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Lecturer / Instructor not found";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Lecturer / Instructor not found";
                                                }
                                            }
                                            if (!uploadedStudentBatches.All(sb => studentBatchesList.Contains(sb)))
                                            {
                                                errorsFound = true;
                                                ttSheet.Cells[index, 8].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                ttSheet.Cells[index, 8].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "One Or More Student Batch(es) not found";
                                                }
                                                else
                                                {
                                                    errorMessage += ", One Or More Student Batch(es) not found";
                                                }
                                            }

                                            ttSheet.Cells["I" + index].Value = errorMessage;
                                            ttSheet.Cells[index, 9].Style.Font.Color.SetColor(Color.Red);
                                        }

                                        index++;
                                    }

                                    if (errorsFound)
                                    {
                                        ttSheet.Column(1).AutoFit();
                                        ttSheet.Column(2).AutoFit();
                                        ttSheet.Column(3).AutoFit();
                                        ttSheet.Column(4).AutoFit();
                                        ttSheet.Column(5).AutoFit();
                                        ttSheet.Column(6).AutoFit();
                                        ttSheet.Column(7).AutoFit();
                                        ttSheet.Column(8).AutoFit();
                                        ttSheet.Column(9).AutoFit();

                                        Session["semsterTimetableErrorsExcel"] = ep.GetAsByteArray();
                                        return Json(new
                                        {
                                            success = "null",
                                            message = "Errors found in the excel sheet"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        var dateTime = DateTime.Now;

                                        foreach (DataRow row in dt.Rows)
                                        {
                                            DateTime lectureStartDate = Convert.ToDateTime(row["Start Date"].ToString().Trim());
                                            TimeSpan lectureStartTime = TimeSpan.Parse(row["Time From"].ToString().Trim());
                                            TimeSpan lectureEndTime = TimeSpan.Parse(row["Time To"].ToString().Trim());

                                            var lectureSubject = semesterSubjectsList.Find(s => s.subject.SubjectCode == row["Subject Code"].ToString().Trim());
                                            LectureType typeOfLecture = lectureTypesList.Find(lt => lt.LectureTypeName == row["Lecture Type"].ToString().Trim());
                                            LectureHall lectureLocation = lectureHallsList.Find(lh => lh.HallId == int.Parse(row["Location"].ToString().Trim()));
                                            AspNetUsers lecturerDetails = lecturersList.Find(l => l.EmployeeNumber == row["Lecturer"].ToString().Trim());

                                            int timetableRecordsCount = (from tt in db.LectureTimetable
                                                                         where tt.SemesterId.Equals(stCC.SemesterId) && tt.SemesterSubjectId.Equals(lectureSubject.semesterSubject.Id)
                                                                         && tt.LectureTypeId.Equals(typeOfLecture.LectureTypeId) && tt.IsActive.Equals(true)
                                                                         select tt).Count();

                                            
                                            for (var i = lectureStartDate; i <= semReg.ToDate; i = i.AddDays(7))
                                            {
                                                if (timetableRecordsCount == 0)
                                                {
                                                    LectureTimetable timeTableObj = new LectureTimetable();

                                                    timeTableObj.SemesterId = stCC.SemesterId;
                                                    timeTableObj.SemesterSubjectId = lectureSubject.semesterSubject.Id;
                                                    timeTableObj.LectureDate = i;
                                                    timeTableObj.FromTime = lectureStartTime;
                                                    timeTableObj.ToTime = lectureEndTime;
                                                    timeTableObj.LocationId = lectureLocation.HallId;
                                                    timeTableObj.LectureTypeId = typeOfLecture.LectureTypeId;
                                                    timeTableObj.LecturerId = lecturerDetails.Id;
                                                    timeTableObj.StudentBatches = row["Student Batches"].ToString().Trim();
                                                    timeTableObj.CreatedDate = dateTime;
                                                    timeTableObj.CreatedBy = "Ranga";
                                                    timeTableObj.ModifiedDate = dateTime;
                                                    timeTableObj.ModifiedBy = "Ranga";
                                                    timeTableObj.IsActive = true;

                                                    db.LectureTimetable.Add(timeTableObj);
                                                    db.SaveChanges();

                                                    LectureTimetableLog timeTableLogObj = new LectureTimetableLog();

                                                    timeTableLogObj.TimetableId = timeTableObj.TimetableId;
                                                    timeTableLogObj.SemesterId = stCC.SemesterId;
                                                    timeTableLogObj.SemesterSubjectId = lectureSubject.semesterSubject.Id;
                                                    timeTableLogObj.LectureDate = i;
                                                    timeTableLogObj.FromTime = lectureStartTime;
                                                    timeTableLogObj.ToTime = lectureEndTime;
                                                    timeTableLogObj.LocationId = lectureLocation.HallId;
                                                    timeTableLogObj.LectureTypeId = typeOfLecture.LectureTypeId;
                                                    timeTableLogObj.LecturerId = lecturerDetails.Id;
                                                    timeTableLogObj.StudentBatches = row["Student Batches"].ToString().Trim();
                                                    timeTableLogObj.CreatedDate = dateTime;
                                                    timeTableLogObj.CreatedBy = "Ranga";
                                                    timeTableLogObj.ModifiedDate = dateTime;
                                                    timeTableLogObj.ModifiedBy = "Ranga";
                                                    timeTableLogObj.IsActive = true;

                                                    db.LectureTimetableLog.Add(timeTableLogObj);
                                                    db.SaveChanges();
                                                }
                                            }
                                        }

                                        return Json(new
                                        {
                                            success = true,
                                            message = "Successfully Saved"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    var errorMessage = "";
                                    if(!columns.Contains("Subject Code"))
                                    {
                                        errorMessage += "Subject Code";
                                    }
                                    if (!columns.Contains("Start Date"))
                                    {
                                        if(errorMessage == "")
                                        {
                                            errorMessage += "Start Date";
                                        }
                                        else {
                                            errorMessage += ", Start Date";
                                        }
                                    }
                                    if (!columns.Contains("Time From"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Time From";
                                        }
                                        else
                                        {
                                            errorMessage += ", Time From";
                                        }
                                    }
                                    if (!columns.Contains("Time To"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Time To";
                                        }
                                        else
                                        {
                                            errorMessage += ", Time To";
                                        }
                                    }
                                    if (!columns.Contains("Lecture Type"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Lecture Type";
                                        }
                                        else
                                        {
                                            errorMessage += ", Lecture Type";
                                        }
                                    }
                                    if (!columns.Contains("Location"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Location";
                                        }
                                        else
                                        {
                                            errorMessage += ", Location";
                                        }
                                    }
                                    if (!columns.Contains("Lecturer"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Lecturer";
                                        }
                                        else
                                        {
                                            errorMessage += ", Lecturer";
                                        }
                                    }
                                    if (!columns.Contains("Student Batches"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Student Batches";
                                        }
                                        else
                                        {
                                            errorMessage += ", Student Batches";
                                        }
                                    }

                                    errorMessage += " column(s) missing from excel sheet";

                                    return Json(new
                                    {
                                        success = false,
                                        message = errorMessage
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "No records found in the excel sheet"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "Semester Timetable excel sheet not found"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Only Excel files are allowed"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "No file selected"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/26
        [HttpGet]
        public ActionResult DownloadSemesterTimetableErrors()
        {
            return File(Session["semsterTimetableErrorsExcel"] as Byte[], "application/vnd.ms-excel", "Semester_Timetable_Errors.xlsx");
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/26
        [HttpGet]
        public ActionResult AddOrEditSemesterTimetable(int id = 0, int operation = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var semesterSubjects = (from ss in db.SemesterSubject
                                        join s in db.Subject on ss.SubjectId equals s.SubjectId
                                        where ss.SemesterRegistrationId.Equals(id) && ss.IsActive.Equals(true) && s.IsActive.Equals(true)
                                        select new
                                        {
                                            Text = s.SubjectCode + " - " + s.SubjectName,
                                            Value = ss.Id
                                        }).ToList();

                List<SelectListItem> semesterSubjectList = new SelectList(semesterSubjects, "Value", "Text").ToList();
                ViewBag.semesterSubjectList = semesterSubjectList;

                var lectureHalls = (from lh in db.LectureHall
                                    join c in db.Campus on lh.CampusId equals c.CampusId
                                    where lh.IsActive.Equals(true)
                                    select new {
                                        Text = c.CampusName + " - " + lh.Building + " - " + lh.Floor + " - " + lh.HallName,
                                        Value = lh.HallId
                                    }).ToList();

                List<SelectListItem> lectureHallsList = new SelectList(lectureHalls, "Value", "Text").ToList();
                ViewBag.lectureHallsList = lectureHallsList;

                var lectureTypes = (from lt in db.LectureType
                                    where lt.IsActive.Equals(true)
                                    select new {
                                        Text = lt.LectureTypeName,
                                        Value = lt.LectureTypeId
                                    }).ToList();

                List<SelectListItem> lectureTypesList = new SelectList(lectureTypes, "Value", "Text").ToList();
                ViewBag.lectureTypesList = lectureTypesList;

                var lecturers = (from a in db.Appointment
                                 join u in db.AspNetUsers on a.UserId equals u.Id
                                 join t in db.Title on u.EmployeeTitle equals t.TitleId
                                 where a.IsActive.Equals(true) && u.IsActive.Equals(true)
                                 select new {
                                     Text = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                     Value = u.Id
                                 }).Distinct().ToList();

                List<SelectListItem> lecturersList = new SelectList(lecturers, "Value", "Text").ToList();
                ViewBag.lecturersList = lecturersList;

                var studentBatches = (from sb in db.StudentBatch
                                      where sb.SemesterRegistrationId.Equals(id) && sb.IsActive.Equals(true)
                                      select new {
                                          Text = sb.BatchName,
                                          Value = sb.BatchName
                                      }).ToList();

                List<SelectListItem> studentBatchesList = new SelectList(studentBatches, "Value", "Text").ToList();
                ViewBag.studentBatchesList = studentBatchesList;

                if (operation == 0)
                {
                    return View(new LectureTimetable() { SemesterId = id });
                }
                else
                {
                    return View((from tt in db.LectureTimetable where tt.TimetableId.Equals(operation) select tt).FirstOrDefault<LectureTimetable>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/28
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditSemesterTimetable(LectureTimetable lectureTT)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    LectureTimetable validationRecord = (from tt in db.LectureTimetable
                                                         where tt.SemesterId.Equals(lectureTT.SemesterId) && tt.SemesterSubjectId.Equals(lectureTT.SemesterSubjectId)
                                                         && tt.LecturerId.Equals(lectureTT.LecturerId) && tt.LectureDate.Value.Equals(lectureTT.LectureDate.Value)
                                                         && ((lectureTT.FromTime.Value <= tt.FromTime.Value && tt.FromTime.Value < lectureTT.ToTime.Value) || (lectureTT.FromTime.Value < tt.ToTime.Value && tt.ToTime.Value <= lectureTT.ToTime.Value)
                                                         || (tt.FromTime.Value <= lectureTT.FromTime.Value && lectureTT.FromTime.Value < tt.ToTime.Value) || (tt.FromTime.Value < lectureTT.ToTime.Value && lectureTT.ToTime.Value <= tt.ToTime.Value))
                                                         select tt).FirstOrDefault<LectureTimetable>();

                    if (lectureTT.TimetableId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "Lecture Session Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            lectureTT.CreatedBy = "Ranga";
                            lectureTT.CreatedDate = dateTime;
                            lectureTT.ModifiedBy = "Ranga";
                            lectureTT.ModifiedDate = dateTime;

                            db.LectureTimetable.Add(lectureTT);
                            db.SaveChanges();

                            LectureTimetableLog timeTableLogObj = new LectureTimetableLog();

                            timeTableLogObj.TimetableId = lectureTT.TimetableId;
                            timeTableLogObj.SemesterId = lectureTT.SemesterId;
                            timeTableLogObj.SemesterSubjectId = lectureTT.SemesterSubjectId;
                            timeTableLogObj.LectureDate = lectureTT.LectureDate.Value;
                            timeTableLogObj.FromTime = lectureTT.FromTime.Value;
                            timeTableLogObj.ToTime = lectureTT.ToTime.Value;
                            timeTableLogObj.LocationId = lectureTT.LocationId.Value;
                            timeTableLogObj.LectureTypeId = lectureTT.LectureTypeId;
                            timeTableLogObj.LecturerId = lectureTT.LecturerId;
                            timeTableLogObj.StudentBatches = lectureTT.StudentBatches;
                            timeTableLogObj.CreatedDate = dateTime;
                            timeTableLogObj.CreatedBy = "Ranga";
                            timeTableLogObj.ModifiedDate = dateTime;
                            timeTableLogObj.ModifiedBy = "Ranga";
                            timeTableLogObj.IsActive = lectureTT.IsActive;

                            db.LectureTimetableLog.Add(timeTableLogObj);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        LectureTimetable editingLectureTimetable = (from tt in db.LectureTimetable where tt.TimetableId.Equals(lectureTT.TimetableId) select tt).FirstOrDefault<LectureTimetable>();

                        if (editingLectureTimetable.SemesterSubjectId != lectureTT.SemesterSubjectId || editingLectureTimetable.LectureDate.Value != lectureTT.LectureDate.Value
                            || editingLectureTimetable.FromTime.Value != lectureTT.FromTime.Value || editingLectureTimetable.ToTime.Value != lectureTT.ToTime.Value
                            || editingLectureTimetable.LocationId.Value != lectureTT.LocationId.Value || editingLectureTimetable.LectureTypeId != lectureTT.LectureTypeId
                            || editingLectureTimetable.LecturerId != lectureTT.LecturerId || editingLectureTimetable.StudentBatches != lectureTT.StudentBatches
                            || editingLectureTimetable.IsActive != lectureTT.IsActive)
                        {
                            if (validationRecord != null && validationRecord.TimetableId != lectureTT.TimetableId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "Lecture Session Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingLectureTimetable.SemesterSubjectId = lectureTT.SemesterSubjectId;
                                editingLectureTimetable.LectureDate = lectureTT.LectureDate.Value;
                                editingLectureTimetable.FromTime = lectureTT.FromTime.Value;
                                editingLectureTimetable.ToTime = lectureTT.ToTime.Value;
                                editingLectureTimetable.LocationId = lectureTT.LocationId.Value;
                                editingLectureTimetable.LectureTypeId = lectureTT.LectureTypeId;
                                editingLectureTimetable.LecturerId = lectureTT.LecturerId;
                                editingLectureTimetable.StudentBatches = lectureTT.StudentBatches;
                                editingLectureTimetable.IsActive = lectureTT.IsActive;
                                editingLectureTimetable.ModifiedBy = "Ranga";
                                editingLectureTimetable.ModifiedDate = dateTime;

                                db.Entry(editingLectureTimetable).State = EntityState.Modified;

                                LectureTimetableLog timeTableLogObj = new LectureTimetableLog();

                                timeTableLogObj.TimetableId = editingLectureTimetable.TimetableId;
                                timeTableLogObj.SemesterId = editingLectureTimetable.SemesterId;
                                timeTableLogObj.SemesterSubjectId = lectureTT.SemesterSubjectId;
                                timeTableLogObj.LectureDate = lectureTT.LectureDate.Value;
                                timeTableLogObj.FromTime = lectureTT.FromTime.Value;
                                timeTableLogObj.ToTime = lectureTT.ToTime.Value;
                                timeTableLogObj.LocationId = lectureTT.LocationId.Value;
                                timeTableLogObj.LectureTypeId = lectureTT.LectureTypeId;
                                timeTableLogObj.LecturerId = lectureTT.LecturerId;
                                timeTableLogObj.StudentBatches = lectureTT.StudentBatches;
                                timeTableLogObj.CreatedDate = dateTime;
                                timeTableLogObj.CreatedBy = "Ranga";
                                timeTableLogObj.ModifiedDate = dateTime;
                                timeTableLogObj.ModifiedBy = "Ranga";
                                timeTableLogObj.IsActive = lectureTT.IsActive;

                                db.LectureTimetableLog.Add(timeTableLogObj);
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/29
        public void DownloadSemesterRegistrationFormat()
        {
            ExcelPackage ep = new ExcelPackage();
            ExcelWorksheet ttSheet = ep.Workbook.Worksheets.Add("Semester Registration");
            ttSheet.Cells["A1"].Value = "Calendar Year";
            ttSheet.Cells["B1"].Value = "Calendar Period";
            ttSheet.Cells["C1"].Value = "Intake Year";
            ttSheet.Cells["D1"].Value = "Intake Name";
            ttSheet.Cells["E1"].Value = "Academic Year";
            ttSheet.Cells["F1"].Value = "Academic Semester";
            ttSheet.Cells["G1"].Value = "Faculty";
            ttSheet.Cells["H1"].Value = "Institute";
            ttSheet.Cells["I1"].Value = "Degree";
            ttSheet.Cells["J1"].Value = "Specialization";
            ttSheet.Cells["K1"].Value = "From Date";
            ttSheet.Cells["L1"].Value = "To Date";
            ttSheet.Cells["M1"].Value = "Student Batches";
            ttSheet.Cells["N1"].Value = "Semester Subjects";

            var ttHeaderCells = ttSheet.Cells[1, 1, 1, ttSheet.Dimension.Columns];
            ttHeaderCells.Style.Font.Bold = true;

            ttSheet.Column(1).AutoFit();
            ttSheet.Column(2).AutoFit();
            ttSheet.Column(3).AutoFit();
            ttSheet.Column(4).AutoFit();
            ttSheet.Column(5).AutoFit();
            ttSheet.Column(6).AutoFit();
            ttSheet.Column(7).AutoFit();
            ttSheet.Column(8).AutoFit();
            ttSheet.Column(9).AutoFit();
            ttSheet.Column(10).AutoFit();
            ttSheet.Column(11).AutoFit();
            ttSheet.Column(12).AutoFit();
            ttSheet.Column(13).AutoFit();
            ttSheet.Column(14).AutoFit();

            using (PMSEntities db = new PMSEntities())
            {
                var calendarPeriods = (from cp in db.CalendarPeriod where cp.IsActive.Equals(true) select cp).ToList();

                ExcelWorksheet cpSheet = ep.Workbook.Worksheets.Add("Calendar Periods");
                cpSheet.Cells["A1"].Value = "Calendar Period Id";
                cpSheet.Cells["B1"].Value = "Calendar Period Name";

                cpSheet.Cells[1, 1, 1, cpSheet.Dimension.Columns].Style.Font.Bold = true;

                var cpRowIndex = 2;

                foreach (var calP in calendarPeriods)
                {
                    cpSheet.Cells[cpRowIndex, 1].Value = calP.Id;
                    cpSheet.Cells[cpRowIndex, 2].Value = calP.PeriodName;
                    cpRowIndex++;
                }

                cpSheet.Column(1).AutoFit();
                cpSheet.Column(2).AutoFit();

                var intakes = (from i in db.Intake where i.IsActive.Equals(true) orderby i.IntakeId descending select i).ToList();

                ExcelWorksheet intkSheet = ep.Workbook.Worksheets.Add("Intakes");
                intkSheet.Cells["A1"].Value = "Intake Year";
                intkSheet.Cells["B1"].Value = "Intake Name";

                intkSheet.Cells[1, 1, 1, intkSheet.Dimension.Columns].Style.Font.Bold = true;

                var intkRowIndex = 2;

                foreach (var intk in intakes)
                {
                    intkSheet.Cells[intkRowIndex, 1].Value = intk.IntakeYear;
                    intkSheet.Cells[intkRowIndex, 2].Value = intk.IntakeName;
                    intkRowIndex++;
                }

                intkSheet.Column(1).AutoFit();
                intkSheet.Column(2).AutoFit();

                var faculties = (from f in db.Faculty where f.IsActive.Equals(true) select f).ToList();

                ExcelWorksheet facSheet = ep.Workbook.Worksheets.Add("Faculties");
                facSheet.Cells["A1"].Value = "Faculty Code";
                facSheet.Cells["B1"].Value = "Faculty Name";

                facSheet.Cells[1, 1, 1, facSheet.Dimension.Columns].Style.Font.Bold = true;

                var facRowIndex = 2;

                foreach (var fac in faculties)
                {
                    facSheet.Cells[facRowIndex, 1].Value = fac.FacultyCode;
                    facSheet.Cells[facRowIndex, 2].Value = fac.FacultyName;
                    facRowIndex++;
                }

                facSheet.Column(1).AutoFit();
                facSheet.Column(2).AutoFit();

                var institutes = (from i in db.Institute where i.IsActive.Equals(true) select i).ToList();

                ExcelWorksheet insSheet = ep.Workbook.Worksheets.Add("Institutes");
                insSheet.Cells["A1"].Value = "Institute Code";
                insSheet.Cells["B1"].Value = "Institute Name";

                insSheet.Cells[1, 1, 1, insSheet.Dimension.Columns].Style.Font.Bold = true;

                var insRowIndex = 2;

                foreach (var inst in institutes)
                {
                    insSheet.Cells[insRowIndex, 1].Value = inst.InstituteCode;
                    insSheet.Cells[insRowIndex, 2].Value = inst.InstituteName;
                    insRowIndex++;
                }

                insSheet.Column(1).AutoFit();
                insSheet.Column(2).AutoFit();

                var degreesSpecializations = (from d in db.Degree
                                              where d.IsActive.Equals(true)
                                              select new
                                              {
                                                  degreeCode = d.Code,
                                                  degreeName = d.Name,
                                                  specializations = (from sp in db.Specialization
                                                                     where sp.DegreeId.Value.Equals(d.DegreeId) && sp.IsActive.Equals(true)
                                                                     select new
                                                                     {
                                                                         spCode = sp.Code,
                                                                         spName = sp.Name
                                                                     }).ToList()
                                              }).ToList();

                ExcelWorksheet dspSheet = ep.Workbook.Worksheets.Add("Degrees & Specializations");
                dspSheet.Cells["A1"].Value = "Degree Code";
                dspSheet.Cells["B1"].Value = "Degree Name";
                dspSheet.Cells["C1"].Value = "Specialization Code";
                dspSheet.Cells["D1"].Value = "Specialization Name";

                dspSheet.Cells[1, 1, 1, dspSheet.Dimension.Columns].Style.Font.Bold = true;

                var dspRowIndex = 2;

                foreach (var dsp in degreesSpecializations)
                {
                    dspSheet.Cells[dspRowIndex, 1].Value = dsp.degreeCode;
                    dspSheet.Cells[dspRowIndex, 2].Value = dsp.degreeName;

                    foreach (var sp in dsp.specializations)
                    {
                        dspSheet.Cells[dspRowIndex, 3].Value = sp.spCode;
                        dspSheet.Cells[dspRowIndex, 4].Value = sp.spName;
                        dspRowIndex++;
                    }
                }

                dspSheet.Column(1).AutoFit();
                dspSheet.Column(2).AutoFit();
                dspSheet.Column(3).AutoFit();
                dspSheet.Column(4).AutoFit();

                var subjects = (from s in db.Subject where s.IsActive.Equals(true) select s).ToList();

                ExcelWorksheet subSheet = ep.Workbook.Worksheets.Add("Subjects");
                subSheet.Cells["A1"].Value = "Subject Code";
                subSheet.Cells["B1"].Value = "Subject Name";

                subSheet.Cells[1, 1, 1, subSheet.Dimension.Columns].Style.Font.Bold = true;

                var subRowIndex = 2;

                foreach (var sub in subjects)
                {
                    subSheet.Cells[subRowIndex, 1].Value = sub.SubjectCode;
                    subSheet.Cells[subRowIndex, 2].Value = sub.SubjectName;
                    subRowIndex++;
                }

                subSheet.Column(1).AutoFit();
                subSheet.Column(2).AutoFit();
            }

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename=SemesterRegistration.xlsx");
            Response.BinaryWrite(ep.GetAsByteArray());
            Response.End();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/30
        [HttpPost]
        public ActionResult UploadSemesterRegistration(SemesterRegistrationCC srCC)
        {
            using (PMSEntities db = new PMSEntities())
            {
                if (srCC.UploadedFile != null)
                {
                    if (srCC.UploadedFile.FileName.EndsWith(".xlsx") || srCC.UploadedFile.FileName.EndsWith(".xls"))
                    {
                        Stream stream = srCC.UploadedFile.InputStream;
                        IExcelDataReader reader = null;

                        if (srCC.UploadedFile.FileName.EndsWith(".xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }

                        int fieldCount = reader.FieldCount;
                        int rowCount = reader.RowCount;

                        DataTable dt = new DataTable();
                        DataRow dtRow;

                        DataTable dt_ = new DataTable();

                        dt_ = reader.AsDataSet().Tables["Semester Registration"];

                        if (dt_ != null)
                        {
                            for (int i = 0; i < dt_.Columns.Count; i++)
                            {
                                dt.Columns.Add(dt_.Rows[0][i].ToString());
                            }

                            int rowCounter = 0;

                            for (int row = 1; row < dt_.Rows.Count; row++)
                            {
                                dtRow = dt.NewRow();

                                for (int col = 0; col < dt_.Columns.Count; col++)
                                {
                                    dtRow[col] = dt_.Rows[row][col].ToString().Trim();
                                    rowCounter++;
                                }
                                dt.Rows.Add(dtRow);
                            }

                            if (dt.Rows.Count != 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    row.SetField(10, row[10].ToString().Trim().Split(' ')[0]);
                                    row.SetField(11, row[11].ToString().Trim().Split(' ')[0]);
                                }

                                DataColumnCollection columns = dt.Columns;

                                if (columns.Contains("Calendar Year") && columns.Contains("Calendar Period") && columns.Contains("Intake Year")
                                    && columns.Contains("Intake Name") && columns.Contains("Academic Year") && columns.Contains("Academic Semester")
                                    && columns.Contains("Faculty") && columns.Contains("Institute") && columns.Contains("Degree")
                                    && columns.Contains("From Date") && columns.Contains("To Date")
                                    && columns.Contains("Student Batches") && columns.Contains("Semester Subjects"))
                                {
                                    bool errorsFound = false;

                                    List<CalendarPeriod> calendarPeriodsList = (from cp in db.CalendarPeriod where cp.IsActive.Equals(true) select cp).ToList();

                                    List<Intake> intakesList = (from i in db.Intake where i.IsActive.Equals(true) select i).ToList();

                                    List<Faculty> facultiesList = (from f in db.Faculty where f.IsActive.Equals(true) select f).ToList();

                                    List<Institute> institutesList = (from i in db.Institute where i.IsActive.Equals(true) select i).ToList();

                                    var degreesSpecializations = (from d in db.Degree
                                                                  where d.IsActive.Equals(true)
                                                                  select new
                                                                  {
                                                                      degreeId = d.DegreeId,
                                                                      degreeCode = d.Code,
                                                                      degreeName = d.Name,
                                                                      specializations = (from sp in db.Specialization
                                                                                         where sp.DegreeId.Value.Equals(d.DegreeId) && sp.IsActive.Equals(true)
                                                                                         select new
                                                                                         {
                                                                                             spId = sp.SpecializationId,
                                                                                             spCode = sp.Code,
                                                                                             spName = sp.Name
                                                                                         }).ToList()
                                                                  }).ToList();

                                    List<string> subjectsList = (from s in db.Subject where s.IsActive.Equals(true) select s.SubjectCode).ToList();

                                    ExcelPackage ep = new ExcelPackage();
                                    ExcelWorksheet srSheet = ep.Workbook.Worksheets.Add("Semester Registration");
                                    srSheet.Cells["A1"].LoadFromDataTable(dt, true);
                                    srSheet.Cells["O1"].Value = "Errors";

                                    var srHeaderCells = srSheet.Cells[1, 1, 1, srSheet.Dimension.Columns];
                                    srHeaderCells.Style.Font.Bold = true;

                                    int index = 2;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        if (String.IsNullOrEmpty(row["Calendar Year"].ToString().Trim()) || String.IsNullOrEmpty(row["Calendar Period"].ToString().Trim())
                                            || String.IsNullOrEmpty(row["Intake Year"].ToString().Trim()) || String.IsNullOrEmpty(row["Intake Name"].ToString().Trim())
                                            || String.IsNullOrEmpty(row["Academic Year"].ToString().Trim()) || String.IsNullOrEmpty(row["Academic Semester"].ToString().Trim())
                                            || String.IsNullOrEmpty(row["Faculty"].ToString().Trim()) || String.IsNullOrEmpty(row["Institute"].ToString().Trim())
                                            || String.IsNullOrEmpty(row["Degree"].ToString().Trim()) || String.IsNullOrEmpty(row["From Date"].ToString().Trim())
                                            || String.IsNullOrEmpty(row["To Date"].ToString().Trim()) || String.IsNullOrEmpty(row["Student Batches"].ToString().Trim())
                                            || String.IsNullOrEmpty(row["Semester Subjects"].ToString().Trim()))
                                        {
                                            errorsFound = true;

                                            if (String.IsNullOrEmpty(row["Calendar Year"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 1].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Calendar Period"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 2].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Intake Year"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 3].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Intake Name"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 4].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Academic Year"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 5].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Academic Semester"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 6].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Faculty"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 7].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Institute"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 8].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Degree"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 9].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["From Date"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 11].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["To Date"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 12].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Student Batches"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 13].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }
                                            if (String.IsNullOrEmpty(row["Semester Subjects"].ToString().Trim()))
                                            {
                                                srSheet.Cells[index, 14].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            }

                                            srSheet.Cells["O" + index].Value = "Null or Empty Records Found";
                                            srSheet.Cells[index, 15].Style.Font.Color.SetColor(Color.Red);
                                        }
                                        else
                                        {
                                            var errorMessage = "";
                                            int calendarYear, intakeYear, academicYear, academicSemester;
                                            DateTime fromDate, toDate;
                                            List<string> uploadedSemesterSubjects = row["Semester Subjects"].ToString().Trim().Split(',').Select(b => b.Trim()).ToList();

                                            if (!int.TryParse(row["Calendar Year"].ToString().Trim(), out calendarYear))
                                            {
                                                errorsFound = true;
                                                srSheet.Cells[index, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                srSheet.Cells[index, 1].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                                errorMessage += "Calendar Year is not a number";
                                            }
                                            if (calendarPeriodsList.Find(cp => cp.PeriodName == row["Calendar Period"].ToString().Trim()) == null)
                                            {
                                                errorsFound = true;
                                                srSheet.Cells[index, 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                srSheet.Cells[index, 2].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Calendar Period not found";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Calendar Period not found";
                                                }
                                            }
                                            if (!int.TryParse(row["Intake Year"].ToString().Trim(), out intakeYear))
                                            {
                                                errorsFound = true;
                                                srSheet.Cells[index, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                srSheet.Cells[index, 3].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Intake Year is not a number";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Intake Year is not a number";
                                                }
                                            }
                                            else
                                            {
                                                if (intakesList.Find(i => i.IntakeYear == int.Parse(row["Intake Year"].ToString().Trim()) && i.IntakeName == row["Intake Name"].ToString().Trim()) == null)
                                                {
                                                    errorsFound = true;
                                                    srSheet.Cells[index, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                    srSheet.Cells[index, 3].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                                    srSheet.Cells[index, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                    srSheet.Cells[index, 4].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                    if (errorMessage == "")
                                                    {
                                                        errorMessage += "Intake not found";
                                                    }
                                                    else
                                                    {
                                                        errorMessage += ", Intake not found";
                                                    }
                                                }
                                            }
                                            
                                            if (!int.TryParse(row["Academic Year"].ToString().Trim(), out academicYear))
                                            {
                                                errorsFound = true;
                                                srSheet.Cells[index, 5].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                srSheet.Cells[index, 5].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Academic Year is not a number";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Academic Year is not a number";
                                                }
                                            }
                                            if (!int.TryParse(row["Academic Semester"].ToString().Trim(), out academicSemester))
                                            {
                                                errorsFound = true;
                                                srSheet.Cells[index, 6].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                srSheet.Cells[index, 6].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Academic Semester is not a number";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Academic Semester is not a number";
                                                }
                                            }
                                            if (facultiesList.Find(f => f.FacultyCode == row["Faculty"].ToString().Trim()) == null)
                                            {
                                                errorsFound = true;
                                                srSheet.Cells[index, 7].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                srSheet.Cells[index, 7].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Faculty not found";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Faculty not found";
                                                }
                                            }
                                            if (institutesList.Find(i => i.InstituteCode == row["Institute"].ToString().Trim()) == null)
                                            {
                                                errorsFound = true;
                                                srSheet.Cells[index, 8].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                srSheet.Cells[index, 8].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Institute not found";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Institute not found";
                                                }
                                            }
                                            if (degreesSpecializations.Find(dsp => dsp.degreeCode == row["Degree"].ToString().Trim()) == null)
                                            {
                                                errorsFound = true;
                                                srSheet.Cells[index, 9].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                srSheet.Cells[index, 9].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "Degree not found";
                                                }
                                                else
                                                {
                                                    errorMessage += ", Degree not found";
                                                }
                                            }
                                            else
                                            {
                                                if (!String.IsNullOrEmpty(row["Specialization"].ToString().Trim()))
                                                {
                                                    var dspRecord = degreesSpecializations.Find(dsp => dsp.degreeCode == row["Degree"].ToString().Trim());

                                                    if (dspRecord.specializations.Find(sp => sp.spCode == row["Specialization"].ToString().Trim()) == null)
                                                    {
                                                        errorsFound = true;
                                                        srSheet.Cells[index, 10].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                        srSheet.Cells[index, 10].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                        if (errorMessage == "")
                                                        {
                                                            errorMessage += "Specialization not found for given Degree";
                                                        }
                                                        else
                                                        {
                                                            errorMessage += ", Specialization not found for given Degree";
                                                        }
                                                    }
                                                }
                                            }
                                            if (!DateTime.TryParseExact(row["From Date"].ToString().Trim(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate))
                                            {
                                                errorsFound = true;
                                                srSheet.Cells[index, 11].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                srSheet.Cells[index, 11].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "From Date is not valid";
                                                }
                                                else
                                                {
                                                    errorMessage += ", From Date is not valid";
                                                }
                                            }
                                            else
                                            {
                                                fromDate = DateTime.Parse(row["From Date"].ToString().Trim());
                                            }

                                            if (!DateTime.TryParseExact(row["To Date"].ToString().Trim(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate))
                                            {
                                                errorsFound = true;
                                                srSheet.Cells[index, 12].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                srSheet.Cells[index, 12].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "To Date is not valid";
                                                }
                                                else
                                                {
                                                    errorMessage += ", To Date is not valid";
                                                }
                                            }
                                            else
                                            {
                                                toDate = DateTime.Parse(row["To Date"].ToString().Trim());
                                            }

                                            if (fromDate >= toDate)
                                            {
                                                errorsFound = true;
                                                srSheet.Cells[index, 11].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                srSheet.Cells[index, 11].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "From Date must be less than To Date";
                                                }
                                                else
                                                {
                                                    errorMessage += ", From Date must be less than To Date";
                                                }
                                            }
                                            if (!uploadedSemesterSubjects.All(ss => subjectsList.Contains(ss)))
                                            {
                                                errorsFound = true;
                                                srSheet.Cells[index, 14].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                srSheet.Cells[index, 14].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                                if (errorMessage == "")
                                                {
                                                    errorMessage += "One Or More Semester Subject(s) not found";
                                                }
                                                else
                                                {
                                                    errorMessage += ", One Or More Semester Subject(s) not found";
                                                }
                                            }

                                            srSheet.Cells["O" + index].Value = errorMessage;
                                            srSheet.Cells[index, 15].Style.Font.Color.SetColor(Color.Red);
                                        }

                                        index++;
                                    }

                                    if (errorsFound)
                                    {
                                        srSheet.Column(1).AutoFit();
                                        srSheet.Column(2).AutoFit();
                                        srSheet.Column(3).AutoFit();
                                        srSheet.Column(4).AutoFit();
                                        srSheet.Column(5).AutoFit();
                                        srSheet.Column(6).AutoFit();
                                        srSheet.Column(7).AutoFit();
                                        srSheet.Column(8).AutoFit();
                                        srSheet.Column(9).AutoFit();
                                        srSheet.Column(10).AutoFit();
                                        srSheet.Column(11).AutoFit();
                                        srSheet.Column(12).AutoFit();
                                        srSheet.Column(13).AutoFit();
                                        srSheet.Column(14).AutoFit();
                                        srSheet.Column(15).AutoFit();

                                        Session["semsterRegistrationErrorsExcel"] = ep.GetAsByteArray();
                                        return Json(new
                                        {
                                            success = "null",
                                            message = "Errors found in the excel sheet"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        var dateTime = DateTime.Now;

                                        List<Subject> allSubjectsList = (from s in db.Subject where s.IsActive.Equals(true) select s).ToList();

                                        foreach (DataRow row in dt.Rows)
                                        {
                                            DateTime srFromDate = Convert.ToDateTime(row["From Date"].ToString().Trim());
                                            DateTime srToDate = Convert.ToDateTime(row["To Date"].ToString().Trim());

                                            CalendarPeriod calPeriod = calendarPeriodsList.Find(cp => cp.PeriodName == row["Calendar Period"].ToString().Trim());
                                            Intake intake = intakesList.Find(i => i.IntakeYear == int.Parse(row["Intake Year"].ToString().Trim()) && i.IntakeName == row["Intake Name"].ToString().Trim());
                                            Faculty faculty = facultiesList.Find(f => f.FacultyCode == row["Faculty"].ToString().Trim());
                                            Institute institute = institutesList.Find(i => i.InstituteCode == row["Institute"].ToString().Trim());
                                            var degreeSpecialization = degreesSpecializations.Find(dsp => dsp.degreeCode == row["Degree"].ToString().Trim());

                                            int calendarYear = int.Parse(row["Calendar Year"].ToString().Trim());
                                            int intakeYear = int.Parse(row["Intake Year"].ToString().Trim());
                                            int academicYear = int.Parse(row["Academic Year"].ToString().Trim());
                                            int academicSemester = int.Parse(row["Academic Semester"].ToString().Trim());

                                            SemesterRegistration srRecord = null;

                                            if (String.IsNullOrEmpty(row["Specialization"].ToString().Trim()))
                                            {
                                                srRecord = (from sr in db.SemesterRegistration
                                                                  where sr.CalendarYear.Value.Equals(calendarYear) && sr.CalendarPeriodId.Value.Equals(calPeriod.Id)
                                                                  && sr.IntakeYear.Value.Equals(intakeYear) && sr.IntakeId.Value.Equals(intake.IntakeId)
                                                                  && sr.AcademicYear.Value.Equals(academicYear) && sr.AcademicSemester.Value.Equals(academicSemester)
                                                                  && sr.FacultyId.Value.Equals(faculty.FacultyId) && sr.InstituteId.Value.Equals(institute.InstituteId)
                                                                  && sr.DegreeId.Value.Equals(degreeSpecialization.degreeId)
                                                                  select sr).FirstOrDefault<SemesterRegistration>();
                                            }
                                            else
                                            {
                                                var specializationRecord = degreeSpecialization.specializations.Find(sp => sp.spCode == row["Specialization"].ToString().Trim());

                                                srRecord = (from sr in db.SemesterRegistration
                                                                  where sr.CalendarYear.Value.Equals(calendarYear) && sr.CalendarPeriodId.Value.Equals(calPeriod.Id)
                                                                  && sr.IntakeYear.Value.Equals(intakeYear) && sr.IntakeId.Value.Equals(intake.IntakeId)
                                                                  && sr.AcademicYear.Value.Equals(academicYear) && sr.AcademicSemester.Value.Equals(academicSemester)
                                                                  && sr.FacultyId.Value.Equals(faculty.FacultyId) && sr.InstituteId.Value.Equals(institute.InstituteId)
                                                                  && sr.DegreeId.Value.Equals(degreeSpecialization.degreeId) && sr.SpecializationId.Value.Equals(specializationRecord.spId)
                                                                  select sr).FirstOrDefault<SemesterRegistration>();
                                            }

                                            if (srRecord == null)
                                            {
                                                List<string> uploadedStudentBatches = row["Student Batches"].ToString().Trim().Split(',').Select(b => b.Trim()).ToList();
                                                List<string> uploadedSemesterSubjects = row["Semester Subjects"].ToString().Trim().Split(',').Select(b => b.Trim()).ToList();

                                                SemesterRegistration semRegObj = new SemesterRegistration();

                                                semRegObj.CalendarYear = int.Parse(row["Calendar Year"].ToString().Trim());
                                                semRegObj.CalendarPeriodId = calPeriod.Id;
                                                semRegObj.IntakeYear = int.Parse(row["Intake Year"].ToString().Trim());
                                                semRegObj.IntakeId = intake.IntakeId;
                                                semRegObj.AcademicYear = int.Parse(row["Academic Year"].ToString().Trim());
                                                semRegObj.AcademicSemester = int.Parse(row["Academic Semester"].ToString().Trim());
                                                semRegObj.FacultyId = faculty.FacultyId;
                                                semRegObj.InstituteId = institute.InstituteId;
                                                semRegObj.DegreeId = degreeSpecialization.degreeId;

                                                if (!String.IsNullOrEmpty(row["Specialization"].ToString().Trim()))
                                                {
                                                    semRegObj.SpecializationId = degreeSpecialization.specializations.Find(sp => sp.spCode == row["Specialization"].ToString().Trim()).spId;
                                                }

                                                semRegObj.FromDate = srFromDate;
                                                semRegObj.ToDate = srToDate;
                                                semRegObj.CreatedDate = dateTime;
                                                semRegObj.CreatedBy = "Ranga";
                                                semRegObj.ModifiedDate = dateTime;
                                                semRegObj.ModifiedBy = "Ranga";
                                                semRegObj.IsActive = true;

                                                db.SemesterRegistration.Add(semRegObj);
                                                db.SaveChanges();

                                                foreach (var sb in uploadedStudentBatches)
                                                {
                                                    StudentBatch sbObj = new StudentBatch();

                                                    sbObj.SemesterRegistrationId = semRegObj.SemesterId;
                                                    sbObj.BatchName = sb;
                                                    sbObj.CreatedDate = dateTime;
                                                    sbObj.CreatedBy = "Ranga";
                                                    sbObj.ModifiedDate = dateTime;
                                                    sbObj.ModifiedBy = "Ranga";
                                                    sbObj.IsActive = true;

                                                    db.StudentBatch.Add(sbObj);
                                                }

                                                foreach (var ss in uploadedSemesterSubjects)
                                                {
                                                    SemesterSubject ssObj = new SemesterSubject();

                                                    ssObj.SemesterRegistrationId = semRegObj.SemesterId;
                                                    ssObj.SubjectId = allSubjectsList.Find(s => s.SubjectCode == ss).SubjectId;
                                                    ssObj.CreatedDate = dateTime;
                                                    ssObj.CreatedBy = "Ranga";
                                                    ssObj.ModifiedDate = dateTime;
                                                    ssObj.ModifiedBy = "Ranga";
                                                    ssObj.IsActive = true;

                                                    db.SemesterSubject.Add(ssObj);
                                                }

                                                db.SaveChanges();
                                            }
                                        }

                                        return Json(new
                                        {
                                            success = true,
                                            message = "Successfully Saved"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    var errorMessage = "";
                                    if (!columns.Contains("Calendar Year"))
                                    {
                                        errorMessage += "Calendar Year";
                                    }
                                    if (!columns.Contains("Calendar Period"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Calendar Period";
                                        }
                                        else
                                        {
                                            errorMessage += ", Calendar Period";
                                        }
                                    }
                                    if (!columns.Contains("Intake Year"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Intake Year";
                                        }
                                        else
                                        {
                                            errorMessage += ", Intake Year";
                                        }
                                    }
                                    if (!columns.Contains("Intake Name"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Intake Name";
                                        }
                                        else
                                        {
                                            errorMessage += ", Intake Name";
                                        }
                                    }
                                    if (!columns.Contains("Academic Year"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Academic Year";
                                        }
                                        else
                                        {
                                            errorMessage += ", Academic Year";
                                        }
                                    }
                                    if (!columns.Contains("Academic Semester"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Academic Semester";
                                        }
                                        else
                                        {
                                            errorMessage += ", Academic Semester";
                                        }
                                    }
                                    if (!columns.Contains("Faculty"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Faculty";
                                        }
                                        else
                                        {
                                            errorMessage += ", Faculty";
                                        }
                                    }
                                    if (!columns.Contains("Institute"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Institute";
                                        }
                                        else
                                        {
                                            errorMessage += ", Institute";
                                        }
                                    }
                                    if (!columns.Contains("Degree"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Degree";
                                        }
                                        else
                                        {
                                            errorMessage += ", Degree";
                                        }
                                    }
                                    if (!columns.Contains("From Date"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "From Date";
                                        }
                                        else
                                        {
                                            errorMessage += ", From Date";
                                        }
                                    }
                                    if (!columns.Contains("To Date"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "To Date";
                                        }
                                        else
                                        {
                                            errorMessage += ", To Date";
                                        }
                                    }
                                    if (!columns.Contains("Student Batches"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Student Batches";
                                        }
                                        else
                                        {
                                            errorMessage += ", Student Batches";
                                        }
                                    }
                                    if (!columns.Contains("Semester Subjects"))
                                    {
                                        if (errorMessage == "")
                                        {
                                            errorMessage += "Semester Subjects";
                                        }
                                        else
                                        {
                                            errorMessage += ", Semester Subjects";
                                        }
                                    }

                                    errorMessage += " column(s) missing from excel sheet";

                                    return Json(new
                                    {
                                        success = false,
                                        message = errorMessage
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "No records found in the excel sheet"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "Semester Registration excel sheet not found"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Only Excel files are allowed"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "No file selected"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/30
        [HttpGet]
        public ActionResult DownloadSemesterRegistrationErrors()
        {
            return File(Session["semsterRegistrationErrorsExcel"] as Byte[], "application/vnd.ms-excel", "Semester_Registration_Errors.xlsx");
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/31
        public ActionResult ManageLectureSessions()
        {
            using (PMSEntities db = new PMSEntities())
            {
                var deadlineDate = (from cs in db.ConfigurationalSettings where cs.ConfigurationKey.Equals("Lecture Submission Deadline Date") && cs.FacultyId.Value.Equals(1) select cs.ConfigurationValue).FirstOrDefault();

                ViewBag.deadlineDate = deadlineDate;

                return View();
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/31
        public ActionResult GetLectureSessions()
        {
            using (PMSEntities db = new PMSEntities())
            {
                AspNetUsers lecturer = (from u in db.AspNetUsers where u.UserName.Equals("ranga.a") select u).FirstOrDefault<AspNetUsers>();

                List<SemesterTimetableVM> semesterTimetableRecordsList = (from tt in db.LectureTimetable
                                                                          join s in db.SemesterRegistration on tt.SemesterId equals s.SemesterId
                                                                          join ss in db.SemesterSubject on tt.SemesterSubjectId equals ss.Id
                                                                          join sub in db.Subject on ss.SubjectId equals sub.SubjectId
                                                                          join lt in db.LectureType on tt.LectureTypeId equals lt.LectureTypeId
                                                                          join lh in db.LectureHall on tt.LocationId equals lh.HallId into tt_lh
                                                                          from hll in tt_lh.DefaultIfEmpty()
                                                                          join c in db.Campus on hll.CampusId equals c.CampusId
                                                                          join u in db.AspNetUsers on tt.LecturerId equals u.Id into tt_u
                                                                          from usr in tt_u.DefaultIfEmpty()
                                                                          join ttl in db.Title on usr.EmployeeTitle equals ttl.TitleId
                                                                          where tt.LecturerId.Equals(lecturer.Id) && tt.IsActive.Equals(true)
                                                                          orderby tt.TimetableId descending
                                                                          select new SemesterTimetableVM
                                                                          {
                                                                              TimetableId = tt.TimetableId,
                                                                              SubjectName = sub.SubjectCode + " - " + sub.SubjectName,
                                                                              LectureDate = tt.LectureDate.ToString(),
                                                                              FromTime = tt.FromTime.ToString().Substring(0, 5),
                                                                              ToTime = tt.ToTime.ToString().Substring(0, 5),
                                                                              Location = hll != null ? c.CampusName + " - " + hll.Building + " - " + hll.Floor + " - " + hll.HallName : null,
                                                                              LectureTypeName = lt.LectureTypeName,
                                                                              StudentBatches = tt.StudentBatches,
                                                                              IsActive = tt.IsActive,
                                                                              IsLectureRecordAdded = (from cl in db.ConductedLectures
                                                                                                      where cl.TimetableId.Equals(tt.TimetableId)
                                                                                                      select cl).FirstOrDefault<ConductedLectures>() != null ? true : false
                                                                          }).ToList();

                return Json(new { data = semesterTimetableRecordsList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/10/31
        [HttpGet]
        public ActionResult AddOrEditConductedLecture(int id = 0, int operation = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                SemesterTimetableVM semesterTimetableRecord = (from tt in db.LectureTimetable
                                                               join s in db.SemesterRegistration on tt.SemesterId equals s.SemesterId
                                                               join ss in db.SemesterSubject on tt.SemesterSubjectId equals ss.Id
                                                               join sub in db.Subject on ss.SubjectId equals sub.SubjectId
                                                               join lt in db.LectureType on tt.LectureTypeId equals lt.LectureTypeId
                                                               join lh in db.LectureHall on tt.LocationId equals lh.HallId into tt_lh
                                                               from hll in tt_lh.DefaultIfEmpty()
                                                               join c in db.Campus on hll.CampusId equals c.CampusId
                                                               join u in db.AspNetUsers on tt.LecturerId equals u.Id into tt_u
                                                               from usr in tt_u.DefaultIfEmpty()
                                                               join ttl in db.Title on usr.EmployeeTitle equals ttl.TitleId
                                                               where tt.TimetableId.Equals(id)
                                                               select new SemesterTimetableVM
                                                               {
                                                                   TimetableId = tt.TimetableId,
                                                                   SubjectName = sub.SubjectCode + " - " + sub.SubjectName,
                                                                   LectureDate = tt.LectureDate.ToString(),
                                                                   FromTime = tt.FromTime.ToString().Substring(0, 5),
                                                                   ToTime = tt.ToTime.ToString().Substring(0, 5),
                                                                   Location = hll != null ? c.CampusName + " - " + hll.Building + " - " + hll.Floor + " - " + hll.HallName : null,
                                                                   LectureTypeName = lt.LectureTypeName,
                                                                   StudentBatches = tt.StudentBatches,
                                                                   IsLectureRecordAdded = (from cl in db.ConductedLectures
                                                                                           where cl.TimetableId.Equals(tt.TimetableId)
                                                                                           select cl).FirstOrDefault<ConductedLectures>() != null ? true : false
                                                               }).FirstOrDefault<SemesterTimetableVM>();

                ViewBag.semesterTimetableRecord = semesterTimetableRecord;

                var lectureHalls = (from lh in db.LectureHall
                                    join c in db.Campus on lh.CampusId equals c.CampusId
                                    where lh.IsActive.Equals(true)
                                    select new
                                    {
                                        Text = c.CampusName + " - " + lh.Building + " - " + lh.Floor + " - " + lh.HallName,
                                        Value = lh.HallId
                                    }).ToList();

                List<SelectListItem> lectureHallsList = new SelectList(lectureHalls, "Value", "Text").ToList();
                ViewBag.lectureHallsList = lectureHallsList;

                var campuses = (from c in db.Campus
                                where c.IsActive.Equals(true)
                                select new
                                {
                                    Text = c.CampusName,
                                    Value = c.CampusId
                                }).ToList();

                List<SelectListItem> campusList = new SelectList(campuses, "Value", "Text").ToList();
                ViewBag.campusList = campusList;

                if (operation == 0)
                {
                    return View(new ConductedLectures() { StudentBatches = semesterTimetableRecord.StudentBatches });
                }
                else
                {
                    ConductedLectures conductedLectureRecord = (from cl in db.ConductedLectures where cl.CLId.Equals(operation) select cl).FirstOrDefault<ConductedLectures>();

                    if (conductedLectureRecord.StudentAttendanceSheetLocation != null)
                    {
                        var splittedUploadedFileName = conductedLectureRecord.StudentAttendanceSheetLocation.Split('/');
                        ViewBag.uploadedFileName = splittedUploadedFileName[3];
                    }

                    return View(conductedLectureRecord);
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/01
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditConductedLecture(ConductedLectures clObj)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;

                    if (clObj.CLId == 0)
                    {
                        ConductedLectures validationRecord = (from cl in db.ConductedLectures where cl.TimetableId.Equals(clObj.TimetableId) select cl).FirstOrDefault<ConductedLectures>();

                        if(validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Lecture Record Already Submitted"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            LectureTimetable timetableRecord = (from tt in db.LectureTimetable where tt.TimetableId.Equals(clObj.TimetableId) select tt).FirstOrDefault<LectureTimetable>();

                            TimeSpan duration = DateTime.Parse(timetableRecord.ToTime.ToString()).Subtract(DateTime.Parse(timetableRecord.FromTime.ToString()));

                            int numbeofHours = duration.Hours;
                            int numbeofMinutes = duration.Minutes;

                            int paymentRate = 1000;
                            int paymentAmount = 0;

                            ConductedLecturesLog clLogObj = new ConductedLecturesLog();

                            if (clObj.postedFile != null)
                            {
                                List<string> allowedFileTypes = new List<string> { "pdf", "jpg", "jpeg", "png" };
                                var uploadedFileExtension = Path.GetExtension(clObj.postedFile.FileName).Substring(1);
                                if (allowedFileTypes.Contains(uploadedFileExtension))
                                {
                                    if (clObj.postedFile.ContentLength > 1000000)
                                    {
                                        return Json(new
                                        {
                                            success = false,
                                            message = "File size should be less than 1 MB"
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        string renamedFileName = clObj.TimetableId.ToString() + "." + uploadedFileExtension;
                                        string path = "/UploadedFiles/AttendanceSheets/" + renamedFileName;
                                        clObj.postedFile.SaveAs(Server.MapPath(Path.Combine("~/UploadedFiles/AttendanceSheets", renamedFileName)));
                                        clObj.StudentAttendanceSheetLocation = path;
                                        clLogObj.StudentAttendanceSheetLocation = path;
                                    }
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "Only PDF & Image files supported"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }

                            if(numbeofHours != 0)
                            {
                                paymentAmount = paymentAmount + paymentRate * numbeofHours;
                            }

                            if(numbeofMinutes != 0)
                            {
                                paymentAmount = paymentAmount + paymentRate * numbeofMinutes;
                            }

                            clObj.CurrentStageDisplayName = "Saved";
                            clObj.PaymentAmount = paymentAmount;
                            clObj.CreatedBy = "Ranga";
                            clObj.CreatedDate = dateTime;
                            clObj.ModifiedBy = "Ranga";
                            clObj.ModifiedDate = dateTime;

                            db.ConductedLectures.Add(clObj);
                            db.SaveChanges();

                            clLogObj.CLId = clObj.CLId;
                            clLogObj.TimetableId = clObj.TimetableId;
                            clLogObj.ActualLectureDate = clObj.ActualLectureDate;
                            clLogObj.ActualFromTime = clObj.ActualFromTime;
                            clLogObj.ActualToTime = clObj.ActualToTime;
                            clLogObj.ActualLocationId = clObj.ActualLocationId;
                            clLogObj.CampusId = clObj.CampusId;
                            clLogObj.StudentBatches = clObj.StudentBatches;
                            clLogObj.StudentCount = clObj.StudentCount;
                            clLogObj.Comment = clObj.Comment;
                            clLogObj.CurrentStageDisplayName = clObj.CurrentStageDisplayName;
                            clLogObj.PaymentAmount = clObj.PaymentAmount;
                            clLogObj.CreatedDate = clObj.CreatedDate;
                            clLogObj.CreatedBy = clObj.CreatedBy;
                            clLogObj.ModifiedDate = clObj.ModifiedDate;
                            clLogObj.ModifiedBy = clObj.ModifiedBy;
                            clLogObj.IsActive = clObj.IsActive;

                            db.ConductedLecturesLog.Add(clLogObj);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        ConductedLectures editingConductedLecture = (from cl in db.ConductedLectures where cl.CLId.Equals(clObj.CLId) select cl).FirstOrDefault<ConductedLectures>();

                        LectureTimetable timetableRecord = (from tt in db.LectureTimetable where tt.TimetableId.Equals(clObj.TimetableId) select tt).FirstOrDefault<LectureTimetable>();

                        TimeSpan duration = DateTime.Parse(timetableRecord.ToTime.ToString()).Subtract(DateTime.Parse(timetableRecord.FromTime.ToString()));

                        int numbeofHours = duration.Hours;
                        int numbeofMinutes = duration.Minutes;

                        int paymentRate = 1000;
                        int paymentAmount = 0;

                        ConductedLecturesLog clLogObj = new ConductedLecturesLog();

                        if (clObj.postedFile != null)
                        {
                            List<string> allowedFileTypes = new List<string> { "pdf", "jpg", "jpeg", "png" };
                            var uploadedFileExtension = Path.GetExtension(clObj.postedFile.FileName).Substring(1);
                            if (allowedFileTypes.Contains(uploadedFileExtension))
                            {
                                if (clObj.postedFile.ContentLength > 1000000)
                                {
                                    return Json(new
                                    {
                                        success = false,
                                        message = "File size should be less than 1 MB"
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    string renamedFileName = clObj.TimetableId.ToString() + "." + uploadedFileExtension;
                                    string path = "/UploadedFiles/AttendanceSheets/" + renamedFileName;
                                    string absolutePath = Server.MapPath(Path.Combine("~/UploadedFiles/AttendanceSheets", renamedFileName));

                                    if (System.IO.File.Exists(absolutePath))
                                    {
                                        System.IO.File.Delete(absolutePath);
                                    }

                                    clObj.postedFile.SaveAs(absolutePath);
                                    editingConductedLecture.StudentAttendanceSheetLocation = path;
                                    clLogObj.StudentAttendanceSheetLocation = path;
                                }
                            }
                            else
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "Only PDF & Image files supported"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (numbeofHours != 0)
                        {
                            paymentAmount = paymentAmount + paymentRate * numbeofHours;
                        }

                        if (numbeofMinutes != 0)
                        {
                            paymentAmount = paymentAmount + paymentRate * numbeofMinutes;
                        }

                        editingConductedLecture.ActualLectureDate = clObj.ActualLectureDate;
                        editingConductedLecture.ActualFromTime = clObj.ActualFromTime;
                        editingConductedLecture.ActualToTime = clObj.ActualToTime;
                        editingConductedLecture.ActualLocationId = clObj.ActualLocationId;
                        editingConductedLecture.CampusId = clObj.CampusId;
                        editingConductedLecture.StudentBatches = clObj.StudentBatches;
                        editingConductedLecture.StudentCount = clObj.StudentCount;
                        editingConductedLecture.Comment = clObj.Comment;
                        editingConductedLecture.PaymentAmount = paymentAmount;
                        editingConductedLecture.ModifiedBy = "Ranga";
                        editingConductedLecture.ModifiedDate = dateTime;
                        editingConductedLecture.IsActive = clObj.IsActive;

                        db.Entry(editingConductedLecture).State = EntityState.Modified;

                        clLogObj.CLId = editingConductedLecture.CLId;
                        clLogObj.TimetableId = editingConductedLecture.TimetableId;
                        clLogObj.ActualLectureDate = clObj.ActualLectureDate;
                        clLogObj.ActualFromTime = clObj.ActualFromTime;
                        clLogObj.ActualToTime = clObj.ActualToTime;
                        clLogObj.ActualLocationId = clObj.ActualLocationId;
                        clLogObj.CampusId = clObj.CampusId;
                        clLogObj.StudentBatches = clObj.StudentBatches;
                        clLogObj.StudentCount = clObj.StudentCount;
                        clLogObj.Comment = clObj.Comment;
                        clLogObj.CurrentStage = editingConductedLecture.CurrentStage;
                        clLogObj.CurrentStageDisplayName = editingConductedLecture.CurrentStageDisplayName;
                        clLogObj.PaymentAmount = paymentAmount;
                        clLogObj.CreatedDate = editingConductedLecture.CreatedDate;
                        clLogObj.CreatedBy = editingConductedLecture.CreatedBy;
                        clLogObj.ModifiedDate = dateTime;
                        clLogObj.ModifiedBy = "Ranga";
                        clLogObj.IsActive = clObj.IsActive;

                        db.ConductedLecturesLog.Add(clLogObj);
                        db.SaveChanges();

                        return Json(new
                        {
                            success = true,
                            message = "Successfully Updated"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/04
        public ActionResult ManageSemesterSubjectLICs(int id)
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/04
        public ActionResult GetSemesterSubjectLICs()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<SemesterSubjectLICVM> ssLICList = (from ssl in db.SemesterSubjectLIC
                                                        join ss in db.SemesterSubject on ssl.SemesterSubjectId equals ss.Id
                                                        join s in db.Subject on ss.SubjectId equals s.SubjectId
                                                        join u in db.AspNetUsers on ssl.LICId equals u.Id
                                                        join t in db.Title on u.EmployeeTitle equals t.TitleId
                                                        select new SemesterSubjectLICVM
                                                        {
                                                            SSLICId = ssl.SSLICId,
                                                            SemesterSubjectName = s.SubjectCode + " - " + s.SubjectName,
                                                            LICName = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                                            IsActive = ssl.IsActive
                                                        }).ToList();

                return Json(new { data = ssLICList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/04
        [HttpGet]
        public ActionResult AddOrEditSemesterSubjectLIC(int id = 0, int operation = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var semesterSubjects = (from ss in db.SemesterSubject
                                        join s in db.Subject on ss.SubjectId equals s.SubjectId
                                        where ss.SemesterRegistrationId.Equals(id) && ss.IsActive.Equals(true) && s.IsActive.Equals(true)
                                        select new
                                        {
                                            Text = s.SubjectCode + " - " + s.SubjectName,
                                            Value = ss.Id
                                        }).ToList();

                List<SelectListItem> semesterSubjectList = new SelectList(semesterSubjects, "Value", "Text").ToList();
                ViewBag.semesterSubjectList = semesterSubjectList;

                var users = (from u in db.AspNetUsers
                             join t in db.Title on u.EmployeeTitle equals t.TitleId
                             where u.IsActive.Equals(true)
                             select new
                             {
                                 Text = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                 Value = u.Id
                             }).ToList();

                List<SelectListItem> usersList = new SelectList(users, "Value", "Text").ToList();
                ViewBag.usersList = usersList;

                if (operation == 0)
                {
                    return View(new SemesterSubjectLIC());
                }
                else
                {
                    return View((from ssl in db.SemesterSubjectLIC where ssl.SSLICId.Equals(operation) select ssl).FirstOrDefault<SemesterSubjectLIC>());
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/04
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditSemesterSubjectLIC(SemesterSubjectLIC ssLICObj)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    SemesterSubjectLIC validationRecord = (from ssl in db.SemesterSubjectLIC
                                                           where ssl.SemesterSubjectId.Equals(ssLICObj.SemesterSubjectId) && ssl.LICId.Equals(ssLICObj.LICId)
                                                           select ssl).FirstOrDefault<SemesterSubjectLIC>();

                    if (ssLICObj.SSLICId == 0)
                    {
                        if (validationRecord != null)
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This LIC Already Exists For Selected Semester Subject"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            ssLICObj.CreatedBy = "Ranga";
                            ssLICObj.CreatedDate = dateTime;
                            ssLICObj.ModifiedBy = "Ranga";
                            ssLICObj.ModifiedDate = dateTime;

                            db.SemesterSubjectLIC.Add(ssLICObj);
                            db.SaveChanges();

                            return Json(new
                            {
                                success = true,
                                message = "Successfully Saved"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        SemesterSubjectLIC editingSSLIC = (from ssl in db.SemesterSubjectLIC where ssl.SSLICId.Equals(ssLICObj.SSLICId) select ssl).FirstOrDefault<SemesterSubjectLIC>();

                        if (editingSSLIC.SemesterSubjectId != ssLICObj.SemesterSubjectId || editingSSLIC.LICId != ssLICObj.LICId || editingSSLIC.IsActive != ssLICObj.IsActive)
                        {
                            if (validationRecord != null && validationRecord.SSLICId != ssLICObj.SSLICId)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This LIC Already Exists For Selected Semester Subject"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                editingSSLIC.SemesterSubjectId = ssLICObj.SemesterSubjectId;
                                editingSSLIC.LICId = ssLICObj.LICId;
                                editingSSLIC.IsActive = ssLICObj.IsActive;
                                editingSSLIC.ModifiedBy = "Ranga";
                                editingSSLIC.ModifiedDate = dateTime;

                                db.Entry(editingSSLIC).State = EntityState.Modified;
                                db.SaveChanges();

                                return Json(new
                                {
                                    success = true,
                                    message = "Successfully Updated"
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "You didn't make any new changes"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/05
        public ActionResult ManageConductedLectures()
        {
            using (PMSEntities db = new PMSEntities())
            {
                return View();
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/05
        public ActionResult GetConductedLectures()
        {
            using (PMSEntities db = new PMSEntities())
            {
                AspNetUsers lecturer = (from u in db.AspNetUsers where u.UserName.Equals("ranga.a") select u).FirstOrDefault<AspNetUsers>();

                List<ConductedLecturesVM> conductedLecturesList = (from cl in db.ConductedLectures
                                                                   join tt in db.LectureTimetable on cl.TimetableId equals tt.TimetableId
                                                                   join s in db.SemesterRegistration on tt.SemesterId equals s.SemesterId
                                                                   join ss in db.SemesterSubject on tt.SemesterSubjectId equals ss.Id
                                                                   join sub in db.Subject on ss.SubjectId equals sub.SubjectId
                                                                   join lt in db.LectureType on tt.LectureTypeId equals lt.LectureTypeId
                                                                   join lh in db.LectureHall on tt.LocationId equals lh.HallId into tt_lh
                                                                   from hll in tt_lh.DefaultIfEmpty()
                                                                   join c in db.Campus on hll.CampusId equals c.CampusId
                                                                   join aLh in db.LectureHall on cl.ActualLocationId equals aLh.HallId into cl_aLh
                                                                   from aHll in cl_aLh.DefaultIfEmpty()
                                                                   join aCmps in db.Campus on aHll.CampusId equals aCmps.CampusId
                                                                   join ac in db.Campus on cl.CampusId equals ac.CampusId into cl_ac
                                                                   from aCam in cl_ac.DefaultIfEmpty()
                                                                   join u in db.AspNetUsers on tt.LecturerId equals u.Id into tt_u
                                                                   from usr in tt_u.DefaultIfEmpty()
                                                                   join ttl in db.Title on usr.EmployeeTitle equals ttl.TitleId
                                                                   where tt.LecturerId.Equals(lecturer.Id)
                                                                   orderby cl.CLId descending
                                                                   select new ConductedLecturesVM
                                                                   {
                                                                       CLId = cl.CLId,
                                                                       TimetableId = cl.TimetableId,
                                                                       ActualLectureDate = cl.ActualLectureDate.ToString(),
                                                                       ActualFromTime = cl.ActualFromTime.ToString().Substring(0,5),
                                                                       ActualToTime = cl.ActualToTime.ToString().Substring(0, 5),
                                                                       ActualLocation = aHll != null ? aCmps.CampusName + " - " + aHll.Building + " - " + aHll.Floor + " - " + aHll.HallName : null,
                                                                       CampusName = aCam != null ? aCam.CampusName : null,
                                                                       StudentBatches = cl.StudentBatches,
                                                                       StudentCount = cl.StudentCount,
                                                                       StudentAttendanceSheetLocation = cl.StudentAttendanceSheetLocation,
                                                                       Comment = cl.Comment,
                                                                       CurrentStage = cl.CurrentStage,
                                                                       CurrentStageDisplayName = cl.CurrentStageDisplayName,
                                                                       PaymentAmount = cl.PaymentAmount,
                                                                       IsActive = cl.IsActive,
                                                                       timetableRecords = new SemesterTimetableVM()
                                                                       {
                                                                           TimetableId = tt.TimetableId,
                                                                           SubjectName = sub.SubjectCode + " - " + sub.SubjectName,
                                                                           LectureDate = tt.LectureDate.ToString(),
                                                                           FromTime = tt.FromTime.ToString().Substring(0, 5),
                                                                           ToTime = tt.ToTime.ToString().Substring(0, 5),
                                                                           Location = hll != null ? c.CampusName + " - " + hll.Building + " - " + hll.Floor + " - " + hll.HallName : null,
                                                                           LectureTypeName = lt.LectureTypeName,
                                                                           StudentBatches = tt.StudentBatches,
                                                                           IsActive = tt.IsActive,
                                                                       }
                                                                   }).ToList();

                return Json(new { data = conductedLecturesList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/06
        [HttpPost]
        public ActionResult ConductedLectureSendToApproval()
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var currentDateTime = DateTime.Now;
                    var monthStartDate = new DateTime(currentDateTime.Year, currentDateTime.Month, 1);
                    var username = "ranga.a";
                    //int deadlineDays = 0;

                    //List<ConductedLectures> conductedLectureRecords = new List<ConductedLectures>();
                    List<ConfigurationalSettings> deadlineCSList = new List<ConfigurationalSettings>();
                    List<SubWorkflow_WorkflowCC> nextWorkflows = new List<SubWorkflow_WorkflowCC>();

                    var userRecord = (from u in db.AspNetUsers
                                      join f in db.Faculty on u.FacultyId equals f.FacultyId into u_f
                                      from fac in u_f.DefaultIfEmpty()
                                      where u.UserName.Equals(username)
                                      select u).FirstOrDefault();

                    var conductedLectureRecords = (from cl in db.ConductedLectures
                                                   join tt in db.LectureTimetable on cl.TimetableId equals tt.TimetableId
                                                   join sem in db.SemesterRegistration on tt.SemesterId equals sem.SemesterId
                                                   where tt.LecturerId.Equals(userRecord.Id) && cl.IsActive.Equals(true) && tt.IsActive.Equals(true)
                                                   && string.IsNullOrEmpty(cl.CurrentStage.Value.ToString())
                                                   select new {
                                                       lectureRecord = cl,
                                                       facultyId = sem.FacultyId.Value
                                                   }).ToList();

                    for (var i = 0; i < conductedLectureRecords.Count; i++)
                    {
                        int deadlineDays = 0;
                        var paymentConsideringMonth = 0;
                        int facultyId = conductedLectureRecords[i].facultyId;

                        var checkingCS = deadlineCSList.Find(cs => cs.FacultyId.Value == facultyId);
                        
                        if(checkingCS == null)
                        {
                            var deadlineCSRecord = (from c in db.ConfigurationalSettings
                                                    where c.ConfigurationKey.Equals("Lecture Submission Deadline Date") && c.FacultyId.Value == facultyId
                                                    select c).FirstOrDefault();

                            if(deadlineCSRecord != null)
                            {
                                deadlineCSList.Add(deadlineCSRecord);
                                deadlineDays = int.Parse(deadlineCSRecord.ConfigurationValue);
                            }
                        }
                        else
                        {
                            deadlineDays = int.Parse(checkingCS.ConfigurationValue);
                        }

                        if (deadlineDays != 0)
                        {
                            var deadlineDate = monthStartDate.AddDays(deadlineDays);

                            if (currentDateTime <= deadlineDate)
                            {
                                paymentConsideringMonth = currentDateTime.AddMonths(-1).Month;
                            }
                            else
                            {
                                paymentConsideringMonth = currentDateTime.Month;
                            }
                        }
                        else
                        {
                            paymentConsideringMonth = currentDateTime.Month;
                        }

                        var checkingWorkflow = nextWorkflows.Find(sw => sw.WorkflowRecord.FacultyId.Value == facultyId);

                        if (checkingWorkflow == null)
                        {
                            var nextWorkflowRecord = (from sw in db.SubWorkflows
                                                      join w in db.Workflows on sw.WorkflowId equals w.Id
                                                      join r in db.AspNetRoles on sw.WorkflowRole equals r.Id
                                                      where sw.WorkflowStep.Equals(2) && w.FacultyId.Value == facultyId
                                                      select new SubWorkflow_WorkflowCC {
                                                          SubWorkflowRecord = sw,
                                                          WorkflowRole = r.Name,
                                                          WorkflowRecord = w
                                                      }).FirstOrDefault();

                            if (nextWorkflowRecord != null)
                            {
                                nextWorkflows.Add(nextWorkflowRecord);

                                if (conductedLectureRecords[i].lectureRecord.ActualLectureDate.Year == currentDateTime.Year 
                                    && conductedLectureRecords[i].lectureRecord.ActualLectureDate.Month == paymentConsideringMonth)
                                {
                                    conductedLectureRecords[i].lectureRecord.CurrentStage = nextWorkflowRecord.SubWorkflowRecord.SubWorkflowId;
                                    conductedLectureRecords[i].lectureRecord.CurrentStageDisplayName = "Submitted to " + nextWorkflowRecord.WorkflowRole;
                                    conductedLectureRecords[i].lectureRecord.ModifiedDate = currentDateTime;
                                    conductedLectureRecords[i].lectureRecord.ModifiedBy = "Ranga";

                                    db.Entry(conductedLectureRecords[i].lectureRecord).State = EntityState.Modified;
                                }
                            }
                        }
                        else
                        {
                            if (conductedLectureRecords[i].lectureRecord.ActualLectureDate.Year == currentDateTime.Year
                                    && conductedLectureRecords[i].lectureRecord.ActualLectureDate.Month == paymentConsideringMonth)
                            {
                                conductedLectureRecords[i].lectureRecord.CurrentStage = checkingWorkflow.SubWorkflowRecord.SubWorkflowId;
                                conductedLectureRecords[i].lectureRecord.CurrentStageDisplayName = "Submitted to " + checkingWorkflow.WorkflowRole;
                                conductedLectureRecords[i].lectureRecord.ModifiedDate = currentDateTime;
                                conductedLectureRecords[i].lectureRecord.ModifiedBy = "Ranga";

                                db.Entry(conductedLectureRecords[i].lectureRecord).State = EntityState.Modified;
                            }
                        }
                    }

                    db.SaveChanges();

                    return Json(new
                    {
                        success = true,
                        message = "Successfully Sent for Approval"
                    }, JsonRequestBehavior.AllowGet);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/06
        public ActionResult ManageLectureApprovals()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/07
        public ActionResult GetConductedLecturesForApproval()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<ConductedLectureApprovalVM> conductedLecturesList = new List<ConductedLectureApprovalVM>();
                List<SubWorkflows> subWorkflowsList = new List<SubWorkflows>();
                List<string> consideringAreas = new List<string>();
                List<int> stagesList = new List<int>();
                var username = "roshan.v";
                int stepListMin = 0;
                int stepListMax = 0;

                var userRecord = (from u in db.AspNetUsers
                                  join f in db.Faculty on u.FacultyId equals f.FacultyId into u_f
                                  from fac in u_f.DefaultIfEmpty()
                                  join d in db.Department on u.DepartmentId equals d.DepartmentId into u_d
                                  from dep in u_d.DefaultIfEmpty()
                                  where u.UserName.Equals(username)
                                  select new
                                  {
                                      user = u,
                                      faculty = fac != null ? fac : null,
                                      department = dep != null ? dep : null
                                  }).FirstOrDefault();

                var userRoles = (from ur in db.AspNetUserRoles
                                 join u in db.AspNetUsers on ur.UserId equals u.Id
                                 join r in db.AspNetRoles on ur.RoleId equals r.Id
                                 where u.UserName.Equals(username) && ur.IsActive.Equals(true) && r.IsActive.Equals(true)
                                 select r.Id).ToList();

                if (userRecord.faculty != null)
                {
                    subWorkflowsList = (from sw in db.SubWorkflows
                                        join w in db.Workflows on sw.WorkflowId equals w.Id
                                        where (sw.ConsideringArea != "Initial Level") && w.FacultyId.Value.Equals(userRecord.faculty.FacultyId) && (sw.WorkflowStep >= 2)
                                        && sw.IsActive.Equals(true) && w.IsActive.Equals(true)
                                        select sw).ToList();
                }
                else
                {
                    subWorkflowsList = (from sw in db.SubWorkflows
                                        join w in db.Workflows on sw.WorkflowId equals w.Id
                                        where (sw.ConsideringArea != "Initial Level") && (sw.WorkflowStep >= 2) && sw.IsActive.Equals(true) && w.IsActive.Equals(true)
                                        select sw).ToList();
                }

                List<string> distinctSubWorkflowRoles = subWorkflowsList.Select(sw => sw.WorkflowRole).Distinct().ToList();

                var matchingRoles = distinctSubWorkflowRoles.Intersect(userRoles).ToList();

                for(var i = 0; i < matchingRoles.Count; i++)
                {
                    var subWorkflows = subWorkflowsList.FindAll(sw => sw.WorkflowRole == matchingRoles[i]);

                    if(subWorkflows != null)
                    {
                        foreach(SubWorkflows sw in subWorkflows)
                        {
                            if (!consideringAreas.Contains(sw.ConsideringArea))
                            {
                                consideringAreas.Add(sw.ConsideringArea);
                            }

                            if (!stagesList.Contains(sw.SubWorkflowId))
                            {
                                stagesList.Add(sw.SubWorkflowId);
                            }
                        }
                    }
                }

                stepListMin = stagesList.Min();
                stepListMax = stagesList.Max();

                if (consideringAreas.Contains("All"))
                {
                    conductedLecturesList = (from cl in db.ConductedLectures
                                             join tt in db.LectureTimetable on cl.TimetableId equals tt.TimetableId
                                             join u in db.AspNetUsers on tt.LecturerId equals u.Id
                                             join t in db.Title on u.EmployeeTitle equals t.TitleId
                                             where stagesList.Contains(cl.CurrentStage.Value) && cl.IsActive.Equals(true) && tt.IsActive.Equals(true)
                                             orderby cl.CreatedDate descending
                                             select new ConductedLectureApprovalVM
                                             {
                                                 LecturerId = u.Id,
                                                 LecturerName = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                                 RecordMonth = cl.ActualLectureDate.Year.ToString() + "-" + cl.ActualLectureDate.Month.ToString(),
                                                 PendingRecordsCount = (from ncl in db.ConductedLectures
                                                                        join ntt in db.LectureTimetable on ncl.TimetableId equals ntt.TimetableId
                                                                        where ntt.LecturerId.Equals(u.Id) && ncl.ActualLectureDate.Year.Equals(cl.ActualLectureDate.Year)
                                                                        && ncl.ActualLectureDate.Month.Equals(cl.ActualLectureDate.Month) && stagesList.Contains(ncl.CurrentStage.Value)
                                                                        && ncl.IsActive.Equals(true) && ntt.IsActive.Equals(true)
                                                                        select ncl).Count(),
                                                 ApprovedRecordsCount = (from ncl in db.ConductedLectures
                                                                         join ntt in db.LectureTimetable on ncl.TimetableId equals ntt.TimetableId
                                                                         where ntt.LecturerId.Equals(u.Id) && ncl.ActualLectureDate.Year.Equals(cl.ActualLectureDate.Year)
                                                                         && ncl.ActualLectureDate.Month.Equals(cl.ActualLectureDate.Month) && ncl.CurrentStage > stepListMax
                                                                         && ncl.IsActive.Equals(true) && ntt.IsActive.Equals(true)
                                                                         select ncl).Count(),
                                                 RejectedRecordsCount = (from ncl in db.ConductedLectures
                                                                         join ntt in db.LectureTimetable on ncl.TimetableId equals ntt.TimetableId
                                                                         where ntt.LecturerId.Equals(u.Id) && ncl.ActualLectureDate.Year.Equals(cl.ActualLectureDate.Year)
                                                                         && ncl.ActualLectureDate.Month.Equals(cl.ActualLectureDate.Month) && ncl.IsApprovedOrRejected.Value.Equals(false)
                                                                         && ncl.IsActive.Equals(true) && ntt.IsActive.Equals(true)
                                                                         select ncl).Count(),
                                             }).Distinct().ToList();
                }
                else if (consideringAreas.Contains("Faculty Only"))
                {
                    if(userRecord.faculty != null)
                    {
                        //List<int> allowedSubjects = new List<int>();

                        //List<Subject> clSubjectList = (from cl in db.ConductedLectures
                        //                               join tt in db.LectureTimetable on cl.TimetableId equals tt.TimetableId
                        //                               join ss in db.SemesterSubject on tt.SemesterSubjectId equals ss.Id
                        //                               join s in db.Subject on ss.SubjectId equals s.SubjectId
                        //                               where stepsList.Contains(cl.CurrentStage) && cl.IsActive.Equals(true) && tt.IsActive.Equals(true)
                        //                               select s).ToList();

                        //List<Subject> facultySubjects = (from s in db.Subject
                        //                                 join d in db.Degree on s.DegreeId equals d.DegreeId into s_d
                        //                                 from dgr in s_d.DefaultIfEmpty()
                        //                                 where dgr.FacultyId.Value.Equals(userRecord.faculty.FacultyId)
                        //                                 select s).ToList();

                        //for(var i = 0; i < clSubjectList.Count; i++)
                        //{
                        //    if(clSubjectList[i].IsCommon == true)
                        //    {
                        //        allowedSubjects.Add(clSubjectList[i].SubjectId);
                        //    }
                        //    else
                        //    {
                        //        if (facultySubjects.Contains(clSubjectList[i]))
                        //        {
                        //            allowedSubjects.Add(clSubjectList[i].SubjectId);
                        //        }
                        //    }
                        //}

                        conductedLecturesList = (from cl in db.ConductedLectures
                                                 join tt in db.LectureTimetable on cl.TimetableId equals tt.TimetableId
                                                 join sem in db.SemesterRegistration on tt.SemesterId equals sem.SemesterId
                                                 join ss in db.SemesterSubject on tt.SemesterSubjectId equals ss.Id
                                                 join s in db.Subject on ss.SubjectId equals s.SubjectId
                                                 join u in db.AspNetUsers on tt.LecturerId equals u.Id
                                                 join t in db.Title on u.EmployeeTitle equals t.TitleId
                                                 where stagesList.Contains(cl.CurrentStage.Value) && sem.FacultyId.Value.Equals(userRecord.faculty.FacultyId) && cl.IsActive.Equals(true) && tt.IsActive.Equals(true)
                                                 orderby cl.CreatedDate descending
                                                 select new ConductedLectureApprovalVM
                                                 {
                                                     LecturerId = u.Id,
                                                     LecturerName = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                                     RecordMonth = cl.ActualLectureDate.Year.ToString() + "-" + cl.ActualLectureDate.Month.ToString(),
                                                     PendingRecordsCount = (from ncl in db.ConductedLectures
                                                                            join ntt in db.LectureTimetable on ncl.TimetableId equals ntt.TimetableId
                                                                            join nsem in db.SemesterRegistration on ntt.SemesterId equals nsem.SemesterId
                                                                            join nss in db.SemesterSubject on ntt.SemesterSubjectId equals nss.Id
                                                                            where nsem.FacultyId.Value.Equals(userRecord.faculty.FacultyId) && ntt.LecturerId.Equals(u.Id) && ncl.ActualLectureDate.Year.Equals(cl.ActualLectureDate.Year)
                                                                            && ncl.ActualLectureDate.Month.Equals(cl.ActualLectureDate.Month) && stagesList.Contains(ncl.CurrentStage.Value)
                                                                            && ncl.IsActive.Equals(true) && ntt.IsActive.Equals(true)
                                                                            select ncl).Count(),
                                                     ApprovedRecordsCount = (from ncl in db.ConductedLectures
                                                                             join ntt in db.LectureTimetable on ncl.TimetableId equals ntt.TimetableId
                                                                             join nsem in db.SemesterRegistration on ntt.SemesterId equals nsem.SemesterId
                                                                             join nss in db.SemesterSubject on ntt.SemesterSubjectId equals nss.Id
                                                                             where nsem.FacultyId.Value.Equals(userRecord.faculty.FacultyId) && ntt.LecturerId.Equals(u.Id) && ncl.ActualLectureDate.Year.Equals(cl.ActualLectureDate.Year)
                                                                             && ncl.ActualLectureDate.Month.Equals(cl.ActualLectureDate.Month) && ncl.CurrentStage > stepListMax
                                                                             && ncl.IsActive.Equals(true) && ntt.IsActive.Equals(true)
                                                                             select ncl).Count(),
                                                     RejectedRecordsCount = (from ncl in db.ConductedLectures
                                                                             join ntt in db.LectureTimetable on ncl.TimetableId equals ntt.TimetableId
                                                                             join nsem in db.SemesterRegistration on ntt.SemesterId equals nsem.SemesterId
                                                                             join nss in db.SemesterSubject on ntt.SemesterSubjectId equals nss.Id
                                                                             where nsem.FacultyId.Value.Equals(userRecord.faculty.FacultyId) && ntt.LecturerId.Equals(u.Id) && ncl.ActualLectureDate.Year.Equals(cl.ActualLectureDate.Year)
                                                                             && ncl.ActualLectureDate.Month.Equals(cl.ActualLectureDate.Month) && ncl.IsApprovedOrRejected.Value.Equals(false)
                                                                             && ncl.IsActive.Equals(true) && ntt.IsActive.Equals(true)
                                                                             select ncl).Count(),
                                                 }).Distinct().ToList();
                    }
                }
                else if (consideringAreas.Contains("Department Only"))
                {
                    if (userRecord.department != null)
                    {
                        //List<int> allowedSubjects = new List<int>();

                        //List<Subject> clSubjectList = (from cl in db.ConductedLectures
                        //                               join tt in db.LectureTimetable on cl.TimetableId equals tt.TimetableId
                        //                               join ss in db.SemesterSubject on tt.SemesterSubjectId equals ss.Id
                        //                               join s in db.Subject on ss.SubjectId equals s.SubjectId
                        //                               where stepsList.Contains(cl.CurrentStage) && cl.IsActive.Equals(true) && tt.IsActive.Equals(true)
                        //                               select s).ToList();

                        //List<Subject> departmentSubjects = (from s in db.Subject
                        //                                    join d in db.Degree on s.DegreeId equals d.DegreeId into s_d
                        //                                    from dgr in s_d.DefaultIfEmpty()
                        //                                    where dgr.DepartmentId.Value.Equals(userRecord.department.DepartmentId)
                        //                                    select s).ToList();

                        //for (var i = 0; i < clSubjectList.Count; i++)
                        //{
                        //    if (clSubjectList[i].IsCommon == true)
                        //    {
                        //        allowedSubjects.Add(clSubjectList[i].SubjectId);
                        //    }
                        //    else
                        //    {
                        //        if (departmentSubjects.Contains(clSubjectList[i]))
                        //        {
                        //            allowedSubjects.Add(clSubjectList[i].SubjectId);
                        //        }
                        //    }
                        //}

                        conductedLecturesList = (from cl in db.ConductedLectures
                                                 join tt in db.LectureTimetable on cl.TimetableId equals tt.TimetableId
                                                 join ss in db.SemesterSubject on tt.SemesterSubjectId equals ss.Id
                                                 join s in db.Subject on ss.SubjectId equals s.SubjectId
                                                 join d in db.Degree on s.DegreeId equals d.DegreeId into s_d
                                                 from dgr in s_d.DefaultIfEmpty()
                                                 join u in db.AspNetUsers on tt.LecturerId equals u.Id
                                                 join t in db.Title on u.EmployeeTitle equals t.TitleId
                                                 where stagesList.Contains(cl.CurrentStage.Value) && dgr.DepartmentId.Value.Equals(userRecord.department.DepartmentId) && cl.IsActive.Equals(true) && tt.IsActive.Equals(true)
                                                 orderby cl.CreatedDate descending
                                                 select new ConductedLectureApprovalVM
                                                 {
                                                     LecturerId = u.Id,
                                                     LecturerName = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                                     RecordMonth = cl.ActualLectureDate.Year.ToString() + "-" + cl.ActualLectureDate.Month.ToString(),
                                                     PendingRecordsCount = (from ncl in db.ConductedLectures
                                                                            join ntt in db.LectureTimetable on ncl.TimetableId equals ntt.TimetableId
                                                                            join nss in db.SemesterSubject on ntt.SemesterSubjectId equals nss.Id
                                                                            join ns in db.Subject on nss.SubjectId equals ns.SubjectId
                                                                            join nd in db.Degree on ns.DegreeId equals nd.DegreeId into ns_nd
                                                                            from ndgr in ns_nd.DefaultIfEmpty()
                                                                            where ndgr.DepartmentId.Value.Equals(userRecord.department.DepartmentId) && ntt.LecturerId.Equals(u.Id) && ncl.ActualLectureDate.Year.Equals(cl.ActualLectureDate.Year)
                                                                            && ncl.ActualLectureDate.Month.Equals(cl.ActualLectureDate.Month) && stagesList.Contains(ncl.CurrentStage.Value)
                                                                            && ncl.IsActive.Equals(true) && ntt.IsActive.Equals(true)
                                                                            select ncl).Count(),
                                                     ApprovedRecordsCount = (from ncl in db.ConductedLectures
                                                                             join ntt in db.LectureTimetable on ncl.TimetableId equals ntt.TimetableId
                                                                             join nss in db.SemesterSubject on ntt.SemesterSubjectId equals nss.Id
                                                                             join ns in db.Subject on nss.SubjectId equals ns.SubjectId
                                                                             join nd in db.Degree on ns.DegreeId equals nd.DegreeId into ns_nd
                                                                             from ndgr in ns_nd.DefaultIfEmpty()
                                                                             where ndgr.DepartmentId.Value.Equals(userRecord.department.DepartmentId) && ntt.LecturerId.Equals(u.Id) && ncl.ActualLectureDate.Year.Equals(cl.ActualLectureDate.Year)
                                                                             && ncl.ActualLectureDate.Month.Equals(cl.ActualLectureDate.Month) && ncl.CurrentStage > stepListMax
                                                                             && ncl.IsActive.Equals(true) && ntt.IsActive.Equals(true)
                                                                             select ncl).Count(),
                                                     RejectedRecordsCount = (from ncl in db.ConductedLectures
                                                                             join ntt in db.LectureTimetable on ncl.TimetableId equals ntt.TimetableId
                                                                             join nss in db.SemesterSubject on ntt.SemesterSubjectId equals nss.Id
                                                                             join ns in db.Subject on nss.SubjectId equals ns.SubjectId
                                                                             join nd in db.Degree on ns.DegreeId equals nd.DegreeId into ns_nd
                                                                             from ndgr in ns_nd.DefaultIfEmpty()
                                                                             where ndgr.DepartmentId.Value.Equals(userRecord.department.DepartmentId) && ntt.LecturerId.Equals(u.Id) && ncl.ActualLectureDate.Year.Equals(cl.ActualLectureDate.Year)
                                                                             && ncl.ActualLectureDate.Month.Equals(cl.ActualLectureDate.Month) && ncl.IsApprovedOrRejected.Value.Equals(false)
                                                                             && ncl.IsActive.Equals(true) && ntt.IsActive.Equals(true)
                                                                             select ncl).Count(),
                                                 }).Distinct().ToList();
                    }
                }
                else if (consideringAreas.Contains("Subject Only"))
                {
                    List<int> licSubjectList = (from sslic in db.SemesterSubjectLIC
                                                join ss in db.SemesterSubject on sslic.SemesterSubjectId equals ss.Id
                                                join s in db.Subject on ss.SubjectId equals s.SubjectId
                                                join u in db.AspNetUsers on sslic.LICId equals u.Id
                                                where u.UserName.Equals(username) && u.IsActive.Equals(true) && sslic.IsActive.Equals(true)
                                                && ss.IsActive.Equals(true) && s.IsActive.Equals(true)
                                                select ss.Id).ToList();

                    if(licSubjectList.Count != 0)
                    {
                        conductedLecturesList = (from cl in db.ConductedLectures
                                                 join tt in db.LectureTimetable on cl.TimetableId equals tt.TimetableId
                                                 join ss in db.SemesterSubject on tt.SemesterSubjectId equals ss.Id
                                                 join u in db.AspNetUsers on tt.LecturerId equals u.Id
                                                 join t in db.Title on u.EmployeeTitle equals t.TitleId
                                                 where stagesList.Contains(cl.CurrentStage.Value) && licSubjectList.Contains(ss.Id) && cl.IsActive.Equals(true) && tt.IsActive.Equals(true)
                                                 orderby cl.CreatedDate descending
                                                 select new ConductedLectureApprovalVM
                                                 {
                                                     LecturerId = u.Id,
                                                     LecturerName = t.TitleName + " " + u.FirstName + " " + u.LastName,
                                                     RecordMonth = cl.ActualLectureDate.Year.ToString() + "-" + cl.ActualLectureDate.Month.ToString(),
                                                     PendingRecordsCount = (from ncl in db.ConductedLectures
                                                                            join ntt in db.LectureTimetable on ncl.TimetableId equals ntt.TimetableId
                                                                            join nss in db.SemesterSubject on ntt.SemesterSubjectId equals nss.Id
                                                                            where licSubjectList.Contains(nss.Id) && ntt.LecturerId.Equals(u.Id) && ncl.ActualLectureDate.Year.Equals(cl.ActualLectureDate.Year)
                                                                            && ncl.ActualLectureDate.Month.Equals(cl.ActualLectureDate.Month) && stagesList.Contains(ncl.CurrentStage.Value)
                                                                            && ncl.IsActive.Equals(true) && ntt.IsActive.Equals(true)
                                                                            select ncl).Count(),
                                                     ApprovedRecordsCount = (from ncl in db.ConductedLectures
                                                                             join ntt in db.LectureTimetable on ncl.TimetableId equals ntt.TimetableId
                                                                             join nss in db.SemesterSubject on ntt.SemesterSubjectId equals nss.Id
                                                                             where licSubjectList.Contains(nss.Id) && ntt.LecturerId.Equals(u.Id) && ncl.ActualLectureDate.Year.Equals(cl.ActualLectureDate.Year)
                                                                             && ncl.ActualLectureDate.Month.Equals(cl.ActualLectureDate.Month) && ncl.CurrentStage > stepListMax
                                                                             && ncl.IsActive.Equals(true) && ntt.IsActive.Equals(true)
                                                                             select ncl).Count(),
                                                     RejectedRecordsCount = (from ncl in db.ConductedLectures
                                                                             join ntt in db.LectureTimetable on ncl.TimetableId equals ntt.TimetableId
                                                                             join nss in db.SemesterSubject on ntt.SemesterSubjectId equals nss.Id
                                                                             where licSubjectList.Contains(nss.Id) && ntt.LecturerId.Equals(u.Id) && ncl.ActualLectureDate.Year.Equals(cl.ActualLectureDate.Year)
                                                                             && ncl.ActualLectureDate.Month.Equals(cl.ActualLectureDate.Month) && ncl.IsApprovedOrRejected.Value.Equals(false)
                                                                             && ncl.IsActive.Equals(true) && ntt.IsActive.Equals(true)
                                                                             select ncl).Count(),
                                                 }).Distinct().ToList();
                    }
                }

                return Json(new { data = conductedLecturesList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/08
        public ActionResult ManageLecturerMonthlyApprovals(string id, string operation)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var userRecord = (from u in db.AspNetUsers
                                  join t in db.Title on u.EmployeeTitle equals t.TitleId
                                  where u.Id.Equals(id)
                                  select t.TitleName + " " + u.FirstName + " " + u.LastName).FirstOrDefault();

                ViewBag.LecturerName = userRecord;
                ViewBag.RecordMonth = operation;
                return View();
            }
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/11/08
        public ActionResult GetLecturerMonthlyRecords(string id, string operation)
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<ConductedLecturesVM> conductedLecturesList = new List<ConductedLecturesVM>();
                List<SubWorkflows> subWorkflowsList = new List<SubWorkflows>();
                List<string> consideringAreas = new List<string>();
                List<int> stagesList = new List<int>();
                var username = "roshan.v";

                var userRecord = (from u in db.AspNetUsers
                                  join f in db.Faculty on u.FacultyId equals f.FacultyId into u_f
                                  from fac in u_f.DefaultIfEmpty()
                                  join d in db.Department on u.DepartmentId equals d.DepartmentId into u_d
                                  from dep in u_d.DefaultIfEmpty()
                                  where u.UserName.Equals(username)
                                  select new
                                  {
                                      user = u,
                                      faculty = fac != null ? fac : null,
                                      department = dep != null ? dep : null
                                  }).FirstOrDefault();

                var userRoles = (from ur in db.AspNetUserRoles
                                 join u in db.AspNetUsers on ur.UserId equals u.Id
                                 join r in db.AspNetRoles on ur.RoleId equals r.Id
                                 where u.UserName.Equals(username) && ur.IsActive.Equals(true) && r.IsActive.Equals(true)
                                 select r.Id).ToList();

                if (userRecord.faculty != null)
                {
                    subWorkflowsList = (from sw in db.SubWorkflows
                                        join w in db.Workflows on sw.WorkflowId equals w.Id
                                        where (sw.ConsideringArea != "Initial Level") && w.FacultyId.Value.Equals(userRecord.faculty.FacultyId) && (sw.WorkflowStep >= 2)
                                        && sw.IsActive.Equals(true) && w.IsActive.Equals(true)
                                        select sw).ToList();
                }
                else
                {
                    subWorkflowsList = (from sw in db.SubWorkflows
                                        join w in db.Workflows on sw.WorkflowId equals w.Id
                                        where (sw.ConsideringArea != "Initial Level") && (sw.WorkflowStep >= 2) && sw.IsActive.Equals(true) && w.IsActive.Equals(true)
                                        select sw).ToList();
                }

                List<string> distinctSubWorkflowRoles = subWorkflowsList.Select(sw => sw.WorkflowRole).Distinct().ToList();

                var matchingRoles = distinctSubWorkflowRoles.Intersect(userRoles).ToList();

                for (var i = 0; i < matchingRoles.Count; i++)
                {
                    var subWorkflows = subWorkflowsList.FindAll(sw => sw.WorkflowRole == matchingRoles[i]);

                    if (subWorkflows != null)
                    {
                        foreach (SubWorkflows sw in subWorkflows)
                        {
                            if (!consideringAreas.Contains(sw.ConsideringArea))
                            {
                                consideringAreas.Add(sw.ConsideringArea);
                            }

                            if (!stagesList.Contains(sw.SubWorkflowId))
                            {
                                stagesList.Add(sw.SubWorkflowId);
                            }
                        }
                    }
                }

                var lectureMonthInDateTime = Convert.ToDateTime(operation);

                List<ConductedLecturesVM> conductedLectureRecords = (from cl in db.ConductedLectures
                                                                     join tt in db.LectureTimetable on cl.TimetableId equals tt.TimetableId
                                                                     join s in db.SemesterRegistration on tt.SemesterId equals s.SemesterId
                                                                     join ss in db.SemesterSubject on tt.SemesterSubjectId equals ss.Id
                                                                     join sub in db.Subject on ss.SubjectId equals sub.SubjectId
                                                                     join d in db.Degree on s.DegreeId equals d.DegreeId into s_d
                                                                     from dgr in s_d.DefaultIfEmpty()
                                                                     join lt in db.LectureType on tt.LectureTypeId equals lt.LectureTypeId
                                                                     join lh in db.LectureHall on tt.LocationId equals lh.HallId into tt_lh
                                                                     from hll in tt_lh.DefaultIfEmpty()
                                                                     join c in db.Campus on hll.CampusId equals c.CampusId
                                                                     join aLh in db.LectureHall on cl.ActualLocationId equals aLh.HallId into cl_aLh
                                                                     from aHll in cl_aLh.DefaultIfEmpty()
                                                                     join aCmps in db.Campus on aHll.CampusId equals aCmps.CampusId
                                                                     join ac in db.Campus on cl.CampusId equals ac.CampusId into cl_ac
                                                                     from aCam in cl_ac.DefaultIfEmpty()
                                                                     join u in db.AspNetUsers on tt.LecturerId equals u.Id into tt_u
                                                                     from usr in tt_u.DefaultIfEmpty()
                                                                     join ttl in db.Title on usr.EmployeeTitle equals ttl.TitleId
                                                                     where !string.IsNullOrEmpty(cl.CurrentStage.Value.ToString()) && tt.LecturerId.Equals(id) && cl.ActualLectureDate.Year.Equals(lectureMonthInDateTime.Year)
                                                                     && cl.ActualLectureDate.Month.Equals(lectureMonthInDateTime.Month) && cl.IsActive.Equals(true)
                                                                     && tt.IsActive.Equals(true)
                                                                     orderby cl.CLId descending
                                                                     select new ConductedLecturesVM
                                                                     {
                                                                         CLId = cl.CLId,
                                                                         TimetableId = cl.TimetableId,
                                                                         ActualLectureDate = cl.ActualLectureDate.ToString(),
                                                                         ActualFromTime = cl.ActualFromTime.ToString().Substring(0, 5),
                                                                         ActualToTime = cl.ActualToTime.ToString().Substring(0, 5),
                                                                         ActualLocation = aHll != null ? aCmps.CampusName + " - " + aHll.Building + " - " + aHll.Floor + " - " + aHll.HallName : null,
                                                                         CampusName = aCam != null ? aCam.CampusName : null,
                                                                         StudentBatches = cl.StudentBatches,
                                                                         StudentCount = cl.StudentCount.Value,
                                                                         StudentAttendanceSheetLocation = cl.StudentAttendanceSheetLocation,
                                                                         Comment = cl.Comment,
                                                                         CurrentStage = cl.CurrentStage.Value,
                                                                         CurrentStageDisplayName = cl.CurrentStageDisplayName,
                                                                         PaymentAmount = cl.PaymentAmount,
                                                                         IsActive = cl.IsActive,
                                                                         canSendToApproval = false,
                                                                         FacultyId = s.FacultyId.Value,
                                                                         DepartmentId = dgr.DepartmentId.Value,
                                                                         timetableRecords = new SemesterTimetableVM()
                                                                         {
                                                                             TimetableId = tt.TimetableId,
                                                                             SemesterSubjectId = ss.Id,
                                                                             SubjectName = sub.SubjectCode + " - " + sub.SubjectName,
                                                                             LectureDate = tt.LectureDate.ToString(),
                                                                             FromTime = tt.FromTime.ToString().Substring(0, 5),
                                                                             ToTime = tt.ToTime.ToString().Substring(0, 5),
                                                                             Location = hll != null ? c.CampusName + " - " + hll.Building + " - " + hll.Floor + " - " + hll.HallName : null,
                                                                             LectureTypeName = lt.LectureTypeName,
                                                                             StudentBatches = tt.StudentBatches,
                                                                             IsActive = tt.IsActive,
                                                                         }
                                                                     }).ToList();

                if (consideringAreas.Contains("All"))
                {
                    foreach (ConductedLecturesVM clRecord in conductedLectureRecords)
                    {
                        if (stagesList.Contains(clRecord.CurrentStage.Value))
                        {
                            clRecord.canSendToApproval = true;
                        }
                    }
                    conductedLecturesList = conductedLectureRecords;
                }
                else if (consideringAreas.Contains("Faculty Only"))
                {
                    if (userRecord.faculty != null)
                    {
                        var filteredRecords = conductedLectureRecords.Where(cl => cl.FacultyId == userRecord.faculty.FacultyId).ToList();

                        foreach (ConductedLecturesVM clRecord in filteredRecords)
                        {
                            if (stagesList.Contains(clRecord.CurrentStage.Value))
                            {
                                clRecord.canSendToApproval = true;
                            }
                        }
                        conductedLecturesList = filteredRecords;
                    }
                }
                else if (consideringAreas.Contains("Department Only"))
                {
                    if (userRecord.department != null)
                    {
                        var filteredRecords = conductedLectureRecords.Where(cl => cl.DepartmentId == userRecord.department.DepartmentId).ToList();

                        //foreach (ConductedLecturesVM clRecord in filteredRecords)
                        //{
                        //    if (stagesList.Contains(clRecord.CurrentStage.Value))
                        //    {
                        //        clRecord.canSendToApproval = true;
                        //    }
                        //}
                        conductedLecturesList = filteredRecords;
                    }
                }
                else if (consideringAreas.Contains("Subject Only"))
                {
                    List<int> licSubjectList = (from sslic in db.SemesterSubjectLIC
                                                join ss in db.SemesterSubject on sslic.SemesterSubjectId equals ss.Id
                                                join s in db.Subject on ss.SubjectId equals s.SubjectId
                                                join u in db.AspNetUsers on sslic.LICId equals u.Id
                                                where u.UserName.Equals(username) && u.IsActive.Equals(true) && sslic.IsActive.Equals(true)
                                                && ss.IsActive.Equals(true) && s.IsActive.Equals(true)
                                                select ss.Id).ToList();

                    if (licSubjectList.Count != 0)
                    {
                        var filteredRecords = conductedLectureRecords.Where(cl => licSubjectList.Contains(cl.timetableRecords.SemesterSubjectId)).ToList();

                        //foreach (ConductedLecturesVM clRecord in filteredRecords)
                        //{
                        //    if (stagesList.Contains(clRecord.CurrentStage.Value))
                        //    {
                        //        clRecord.canSendToApproval = true;
                        //    }
                        //}
                        conductedLecturesList = filteredRecords;
                    }
                }

                return Json(new { data = conductedLecturesList }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}