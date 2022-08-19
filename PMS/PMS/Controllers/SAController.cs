using PMS.Models;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Controllers
{
    public class SAController : Controller
    {
        // GET: SA
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageInstitutes()
        {
            return View();
        }

        public ActionResult GetInstitutes()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<Institute> instituteList = (from i in db.Institute orderby i.InstituteId descending select i).ToList();
                return Json(new { data = instituteList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditInstitute(Institute institute)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    if (institute.InstituteId == 0)
                    {
                        Institute validationRecord = (from i in db.Institute where i.InstituteCode.Equals(institute.InstituteCode) || i.InstituteName.Equals(institute.InstituteName) select i).FirstOrDefault<Institute>();
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

        public ActionResult ManageCampuses()
        {
            return View();
        }

        public ActionResult GetCampuses()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<Campus> campusesList = (from c in db.Campus orderby c.CampusId descending select c).ToList();
                return Json(new { data = campusesList }, JsonRequestBehavior.AllowGet);
            }
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditCampus(Campus campus)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    if (campus.CampusId == 0)
                    {
                        Campus validationRecord = (from c in db.Campus where c.CampusCode.Equals(campus.CampusCode) || c.CampusName.Equals(campus.CampusName) select c).FirstOrDefault<Campus>();
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

        public ActionResult ManageFaculties()
        {
            return View();
        }

        public ActionResult GetFaculties()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<FacultyVM> facultiesList = (from f in db.Faculty
                                                 join u in db.AspNetUsers on f.FacultyDean equals u.Id into f_u
                                                 from usr in f_u.DefaultIfEmpty()
                                                 orderby f.FacultyId descending select new FacultyVM
                                                 {
                                                     FacultyId = f.FacultyId,
                                                     FacultyCode = f.FacultyCode,
                                                     FacultyName = f.FacultyName,
                                                     FacultyDean = usr,
                                                     IsActive = f.IsActive
                                                 }).ToList();

                return Json(new { data = facultiesList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEditFaculty(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var users = (from u in db.AspNetUsers
                             where u.IsActive.Equals(true)
                             select new
                             {
                                 Text = u.FirstName + " " + u.LastName,
                                 Value = u.Id
                             }).ToList();

                List<SelectListItem> usersList = new SelectList(users, "Value", "Text").ToList();
                usersList.Insert(0, new SelectListItem() { Text = "-- N/A --", Value = "", Disabled = false, Selected = true });
                ViewBag.usersList = usersList;

                if (id == 0)
                {
                    return View(new FacultyVM());
                }
                else
                {
                    FacultyVM facultyVM = (from f in db.Faculty
                                           join u in db.AspNetUsers on f.FacultyDean equals u.Id into f_u
                                           from usr in f_u.DefaultIfEmpty()
                                           where f.FacultyId.Equals(id)
                                           select new FacultyVM
                                           {
                                               FacultyId = f.FacultyId,
                                               FacultyCode = f.FacultyCode,
                                               FacultyName = f.FacultyName,
                                               FacultyDeanId = usr.Id,
                                               FacultyDean = usr,
                                               IsActive = f.IsActive
                                           }).FirstOrDefault<FacultyVM>();
                    return View(facultyVM);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditFaculty(FacultyVM facultyVM)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    if (facultyVM.FacultyId == 0)
                    {
                        Faculty validationRecord = (from f in db.Faculty where f.FacultyCode.Equals(facultyVM.FacultyCode) || f.FacultyName.Equals(facultyVM.FacultyName) select f).FirstOrDefault<Faculty>();
                        if (validationRecord != null)
                        {
                            if (validationRecord.FacultyCode == facultyVM.FacultyCode && validationRecord.FacultyName == facultyVM.FacultyName)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Faculty Code and Name Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (validationRecord.FacultyCode == facultyVM.FacultyCode)
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
                            Faculty faculty = new Faculty();

                            faculty.FacultyCode = facultyVM.FacultyCode;
                            faculty.FacultyName = facultyVM.FacultyName;
                            faculty.FacultyDean = facultyVM.FacultyDeanId;
                            faculty.IsActive = facultyVM.IsActive;
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
                        Faculty editingFaculty = (from f in db.Faculty where f.FacultyId.Equals(facultyVM.FacultyId) select f).FirstOrDefault<Faculty>();

                        if (editingFaculty.FacultyCode != facultyVM.FacultyCode || editingFaculty.FacultyName != facultyVM.FacultyName || editingFaculty.FacultyDean != facultyVM.FacultyDeanId || editingFaculty.IsActive != facultyVM.IsActive)
                        {
                            editingFaculty.FacultyCode = facultyVM.FacultyCode;
                            editingFaculty.FacultyName = facultyVM.FacultyName;
                            editingFaculty.FacultyDean = facultyVM.FacultyDeanId;
                            editingFaculty.IsActive = facultyVM.IsActive;
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

        public ActionResult ManageDepartments()
        {
            return View();
        }

        public ActionResult GetDepartments()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<DepartmentVM> departmentsList = (from d in db.Department
                                                      join u in db.AspNetUsers on d.HOD equals u.Id into d_u
                                                      from usr in d_u.DefaultIfEmpty()
                                                      join f in db.Faculty on d.FacultyId equals f.FacultyId into d_f
                                                      from fac in d_f.DefaultIfEmpty()
                                                      orderby d.DepartmentId descending
                                                      select new DepartmentVM
                                                      {
                                                          DepartmentId = d.DepartmentId,
                                                          DepartmentCode = d.DepartmentCode,
                                                          DepartmentName = d.DepartmentName,
                                                          HOD = usr,
                                                          Faculty = fac,
                                                          IsActive = d.IsActive
                                                      }).ToList();

                return Json(new { data = departmentsList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEditDepartment(int id = 0)
        {
            using (PMSEntities db = new PMSEntities())
            {
                var users = (from u in db.AspNetUsers
                             where u.IsActive.Equals(true)
                             select new
                             {
                                 Text = u.FirstName + " " + u.LastName,
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
                    return View(new DepartmentVM());
                }
                else
                {
                    DepartmentVM departmentVM = (from d in db.Department
                                                 join u in db.AspNetUsers on d.HOD equals u.Id into d_u
                                                 from usr in d_u.DefaultIfEmpty()
                                                 join f in db.Faculty on d.FacultyId equals f.FacultyId into d_f
                                                 from fac in d_f.DefaultIfEmpty()
                                                 where d.DepartmentId.Equals(id)
                                                 select new DepartmentVM
                                                 {
                                                     DepartmentId = d.DepartmentId,
                                                     DepartmentCode = d.DepartmentCode,
                                                     DepartmentName = d.DepartmentName,
                                                     HODId = usr.Id,
                                                     HOD = usr,
                                                     FacultyId =fac.FacultyId,
                                                     Faculty = fac,
                                                     IsActive = d.IsActive
                                                 }).FirstOrDefault<DepartmentVM>();

                    return View(departmentVM);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditDepartment(DepartmentVM departmentVM)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    if (departmentVM.DepartmentId == 0)
                    {
                        Department validationRecord = (from d in db.Department where d.DepartmentCode.Equals(departmentVM.DepartmentCode) || d.DepartmentName.Equals(departmentVM.DepartmentName) select d).FirstOrDefault<Department>();
                        if (validationRecord != null)
                        {
                            if (validationRecord.DepartmentCode == departmentVM.DepartmentCode && validationRecord.DepartmentName == departmentVM.DepartmentName)
                            {
                                return Json(new
                                {
                                    success = false,
                                    message = "This Department Code and Name Already Exists"
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (validationRecord.DepartmentCode == departmentVM.DepartmentCode)
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
                            Department department = new Department();

                            department.DepartmentCode = departmentVM.DepartmentCode;
                            department.DepartmentName = departmentVM.DepartmentName;
                            department.HOD = departmentVM.HODId;
                            department.FacultyId = departmentVM.FacultyId;
                            department.IsActive = departmentVM.IsActive;
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
                        Department editingDepartment = (from d in db.Department where d.DepartmentId.Equals(departmentVM.DepartmentId) select d).FirstOrDefault<Department>();

                        if (editingDepartment.DepartmentCode != departmentVM.DepartmentCode || editingDepartment.DepartmentName != departmentVM.DepartmentName 
                            || editingDepartment.HOD != departmentVM.HODId || editingDepartment.FacultyId != departmentVM.FacultyId 
                            || editingDepartment.IsActive != departmentVM.IsActive)
                        {
                            editingDepartment.DepartmentCode = departmentVM.DepartmentCode;
                            editingDepartment.DepartmentName = departmentVM.DepartmentName;
                            editingDepartment.HOD = departmentVM.HODId;
                            editingDepartment.FacultyId = departmentVM.FacultyId;
                            editingDepartment.IsActive = departmentVM.IsActive;
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

        public ActionResult ManageLectureHalls()
        {
            return View();
        }

        public ActionResult GetLectureHalls()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<LectureHallVM> lectureHallsList = (from l in db.LectureHall
                                                        join c in db.Campus on l.CampusId equals c.CampusId
                                                        select new LectureHallVM
                                                        {
                                                            HallId = l.HallId,
                                                            CampusId = c.CampusId,
                                                            Campus = c,
                                                            Building = l.Building,
                                                            Floor = l.Floor,
                                                            HallName = l.HallName,
                                                            IsActive = l.IsActive
                                                        }).ToList();

                return Json(new { data = lectureHallsList }, JsonRequestBehavior.AllowGet);
            }
        }

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
                campusList.Insert(0, new SelectListItem() { Text = "-- Select Campus --", Value = "", Disabled = false, Selected = true });
                ViewBag.campusList = campusList;

                if (id == 0)
                {
                    return View(new LectureHallVM());
                }
                else
                {
                    LectureHallVM lectureHallVM = (from l in db.LectureHall
                                                   join c in db.Campus on l.CampusId equals c.CampusId
                                                   select new LectureHallVM
                                                   {
                                                       HallId = l.HallId,
                                                       CampusId = c.CampusId,
                                                       Campus = c,
                                                       Building = l.Building,
                                                       Floor = l.Floor,
                                                       HallName = l.HallName,
                                                       IsActive = l.IsActive
                                                   }).FirstOrDefault<LectureHallVM>();

                    return View(lectureHallVM);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditLectureHall(LectureHallVM lectureHallVM)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    if (lectureHallVM.HallId == 0)
                    {
                        LectureHall validationRecord = (from l in db.LectureHall
                                                        where l.CampusId.Equals(lectureHallVM.CampusId) && l.Building.Equals(lectureHallVM.Building)
                                                        && l.Floor.Equals(lectureHallVM.Floor) && l.HallName.Equals(lectureHallVM.HallName)
                                                        select l).FirstOrDefault<LectureHall>();
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
                            LectureHall lectureHall = new LectureHall();

                            lectureHall.CampusId = lectureHallVM.CampusId;
                            lectureHall.Building = lectureHallVM.Building;
                            lectureHall.Floor = lectureHallVM.Floor;
                            lectureHall.HallName = lectureHallVM.HallName;
                            lectureHall.IsActive = lectureHallVM.IsActive;
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
                        LectureHall editingLectureHall = (from l in db.LectureHall where l.HallId.Equals(lectureHallVM.HallId) select l).FirstOrDefault<LectureHall>();

                        if (editingLectureHall.CampusId != lectureHallVM.CampusId || editingLectureHall.Building != lectureHallVM.Building
                            || editingLectureHall.Floor != lectureHallVM.Floor || editingLectureHall.HallName != lectureHallVM.HallName
                            || editingLectureHall.IsActive != lectureHallVM.IsActive)
                        {
                            editingLectureHall.CampusId = lectureHallVM.CampusId;
                            editingLectureHall.Building = lectureHallVM.Building;
                            editingLectureHall.Floor = lectureHallVM.Floor;
                            editingLectureHall.HallName = lectureHallVM.HallName;
                            editingLectureHall.IsActive = lectureHallVM.IsActive;
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
    }
}