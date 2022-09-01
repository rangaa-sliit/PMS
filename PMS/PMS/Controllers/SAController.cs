﻿using PMS.Models;
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

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/17
        public ActionResult ManageInstitutes()
        {
            return View();
        }

        //Developed By:- Ranga Athapaththu
        //Developed On:- 2022/08/17
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
                                                 orderby f.FacultyId descending select new FacultyVM
                                                 {
                                                     FacultyId = f.FacultyId,
                                                     FacultyCode = f.FacultyCode,
                                                     FacultyName = f.FacultyName,
                                                     FacultyDean = usr != null ? usr.FirstName + " " + usr.LastName : null,
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
                    if (faculty.FacultyId == 0)
                    {
                        Faculty validationRecord = (from f in db.Faculty where f.FacultyCode.Equals(faculty.FacultyCode) || f.FacultyName.Equals(faculty.FacultyName) select f).FirstOrDefault<Faculty>();
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
                                                      join f in db.Faculty on d.FacultyId equals f.FacultyId into d_f
                                                      from fac in d_f.DefaultIfEmpty()
                                                      orderby d.DepartmentId descending
                                                      select new DepartmentVM
                                                      {
                                                          DepartmentId = d.DepartmentId,
                                                          DepartmentCode = d.DepartmentCode,
                                                          DepartmentName = d.DepartmentName,
                                                          HODName = usr != null ? usr.FirstName + " " + usr.LastName : null,
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
                    if (department.DepartmentId == 0)
                    {
                        Department validationRecord = (from d in db.Department where d.DepartmentCode.Equals(department.DepartmentCode) || d.DepartmentName.Equals(department.DepartmentName) select d).FirstOrDefault<Department>();
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
                campusList.Insert(0, new SelectListItem() { Text = "-- Select Campus --", Value = "", Disabled = false, Selected = true });
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
                    if (lectureHall.HallId == 0)
                    {
                        LectureHall validationRecord = (from l in db.LectureHall
                                                        where l.CampusId.Equals(lectureHall.CampusId) && l.Building.Equals(lectureHall.Building)
                                                        && l.Floor.Equals(lectureHall.Floor) && l.HallName.Equals(lectureHall.HallName)
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
                List<Title> titlesList = (from t in db.Title select t).ToList();

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
                    if (title.TitleId == 0)
                    {
                        Title validationRecord = (from t in db.Title where t.TitleName.Equals(title.TitleName) select t).FirstOrDefault<Title>();
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
                    if (calendarPeriod.Id == 0)
                    {
                        CalendarPeriod validationRecord = (from cp in db.CalendarPeriod where cp.PeriodName.Equals(calendarPeriod.PeriodName) select cp).FirstOrDefault<CalendarPeriod>();

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
                    if (appointmentType.AppointmentTypeId == 0)
                    {
                        AppointmentType validationRecord = (from at in db.AppointmentType where at.AppointmentTypeName.Equals(appointmentType.AppointmentTypeName) select at).FirstOrDefault<AppointmentType>();

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
                    if (lectureType.LectureTypeId == 0)
                    {
                        LectureType validationRecord = (from lt in db.LectureType where lt.LectureTypeName.Equals(lectureType.LectureTypeName) select lt).FirstOrDefault<LectureType>();

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

                        if (editingLectureType.LectureTypeName != lectureType.LectureTypeName || editingLectureType.IsActive != lectureType.IsActive)
                        {
                            editingLectureType.LectureTypeName = lectureType.LectureTypeName;
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
        public ActionResult GetConfigurationalSettings()
        {
            using (PMSEntities db = new PMSEntities())
            {
                List<ConfigurationalSettings> ConfigurationalSettingsList = (from c in db.ConfigurationalSettings orderby c.Id descending select c).ToList();
                return Json(new { data = ConfigurationalSettingsList }, JsonRequestBehavior.AllowGet);
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/19
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/08/22
        [HttpGet]
        public ActionResult AddOrEditConfigurationalSetting(int id = 0)
        {
            if (id == 0)
            {
                return View(new ConfigurationalSettings());
            }
            else
            {
                using (PMSEntities db = new PMSEntities())
                {
                    return View((from c in db.ConfigurationalSettings where c.Id.Equals(id) select c).FirstOrDefault<ConfigurationalSettings>());
                }
            }
        }

        //Developed By:- Dulanjalee Wickremasinghe
        //Developed On:- 2022/08/19
        //Modified By:- Ranga Athapaththu
        //Modified On:- 2022/08/22
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEditConfigurationalSetting(ConfigurationalSettings configurationalSettings)
        {
            using (PMSEntities db = new PMSEntities())
            {
                try
                {
                    var dateTime = DateTime.Now;
                    if (configurationalSettings.Id == 0)
                    {
                        ConfigurationalSettings validationRecord = (from c in db.ConfigurationalSettings where c.ConfigurationKey.Equals(configurationalSettings.ConfigurationKey) select c).FirstOrDefault<ConfigurationalSettings>();
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

                        if (editingConfigurationalSettings.ConfigurationKey != configurationalSettings.ConfigurationKey || editingConfigurationalSettings.ConfigurationValue != configurationalSettings.ConfigurationValue || editingConfigurationalSettings.IsActive != configurationalSettings.IsActive)
                        {
                            editingConfigurationalSettings.ConfigurationKey = configurationalSettings.ConfigurationKey;
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
                    if (intake.IntakeId == 0)
                    {
                        Intake validationRecord = (from i in db.Intake
                                                   where i.IntakeYear.Value.Equals(intake.IntakeYear.Value) && i.IntakeName.Equals(intake.IntakeName)
                                                   select i).FirstOrDefault<Intake>();

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
                    if (designation.DesignationId == 0)
                    {
                        Designation validationRecord = (from d in db.Designation where d.DesignationName.Equals(designation.DesignationName) select d).FirstOrDefault<Designation>();
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
                                                       join at in db.AppointmentType on a.AppointmentTypeId equals at.AppointmentTypeId
                                                       join d in db.Designation on a.DesignationId equals d.DesignationId
                                                       orderby a.AppointmentId descending
                                                       select new AppointmentVM
                                                       {
                                                           AppointmentId = a.AppointmentId,
                                                           EmployeeName = u.FirstName + " " + u.LastName,
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
                             where u.IsActive.Equals(true)
                             select new
                             {
                                 Text = u.FirstName + " " + u.LastName,
                                 Value = u.Id
                             }).ToList();

                List<SelectListItem> usersList = new SelectList(users, "Value", "Text").ToList();
                usersList.Insert(0, new SelectListItem() { Text = "-- Select Employee --", Value = "", Disabled = true, Selected = true });
                ViewBag.usersList = usersList;

                var designations = (from d in db.Designation
                                    where d.IsActive.Equals(true)
                                    select new
                                    {
                                        Text = d.DesignationName,
                                        Value = d.DesignationId
                                    }).ToList();

                List<SelectListItem> designationList = new SelectList(designations, "Value", "Text").ToList();
                designationList.Insert(0, new SelectListItem() { Text = "-- Select Designation --", Value = "", Disabled = true, Selected = true });
                ViewBag.designationList = designationList;

                var appointmentTypes = (from at in db.AppointmentType
                                        where at.IsActive.Equals(true)
                                        select new
                                        {
                                            Text = at.AppointmentTypeName,
                                            Value = at.AppointmentTypeId
                                        }).ToList();

                List<SelectListItem> appointmentTypeList = new SelectList(appointmentTypes, "Value", "Text").ToList();
                appointmentTypeList.Insert(0, new SelectListItem() { Text = "-- Select Appointment Type --", Value = "", Disabled = true, Selected = true });
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
                    if (appointment.AppointmentId == 0)
                    {
                        bool existingNotFound = true;
                        var appointmentFrom = Convert.ToDateTime(appointment.AppointmentFrom);
                        Appointment validationRecord = (from a in db.Appointment
                                                        where a.UserId.Equals(appointment.UserId) && a.DesignationId.Equals(appointment.DesignationId)
                                                        && a.AppointmentTypeId.Equals(appointment.AppointmentTypeId) && a.IsActive.Equals(true)
                                                        select a).FirstOrDefault<Appointment>();

                        if (validationRecord != null)
                        {
                            if (!appointment.AppointmentTo.HasValue)
                            {
                                if (!validationRecord.AppointmentTo.HasValue)
                                {
                                    existingNotFound = false;
                                }
                                else
                                {
                                    if(appointmentFrom <= validationRecord.AppointmentTo.Value)
                                    {
                                        existingNotFound = false;
                                    }
                                    else
                                    {
                                        existingNotFound = true;
                                    }
                                }
                            }
                            else
                            {
                                var appointmentTo = Convert.ToDateTime(appointment.AppointmentTo.Value);

                                if (!validationRecord.AppointmentTo.HasValue)
                                {
                                    if(validationRecord.AppointmentFrom <= appointmentTo)
                                    {
                                        existingNotFound = false;
                                    }
                                    else
                                    {
                                        existingNotFound = true;
                                    }
                                }
                                else
                                {
                                    if((appointmentFrom <= validationRecord.AppointmentFrom && validationRecord.AppointmentFrom <= appointmentTo)
                                        || (validationRecord.AppointmentFrom <= appointmentFrom && appointmentFrom <= validationRecord.AppointmentTo.Value)
                                        || (appointmentFrom <= validationRecord.AppointmentFrom && validationRecord.AppointmentTo.Value <= appointmentTo)
                                        || (validationRecord.AppointmentFrom <= appointmentFrom && appointmentTo <= validationRecord.AppointmentTo.Value))
                                    {
                                        existingNotFound = false;
                                    }
                                    else
                                    {
                                        existingNotFound = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            existingNotFound = true;
                        }

                        if (existingNotFound)
                        {
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
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                message = "This Appointment Already Exists"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Appointment editingAppointment = (from a in db.Appointment where a.AppointmentId.Equals(appointment.AppointmentId) select a).FirstOrDefault<Appointment>();

                        if (editingAppointment.UserId != appointment.UserId || editingAppointment.DesignationId != appointment.DesignationId 
                            || editingAppointment.AppointmentTypeId != appointment.AppointmentTypeId || editingAppointment.AppointmentFrom != appointment.AppointmentFrom 
                            || editingAppointment.AppointmentTo != appointment.AppointmentTo || editingAppointment.IsActive != appointment.IsActive)
                        {
                            editingAppointment.UserId = appointment.UserId;
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