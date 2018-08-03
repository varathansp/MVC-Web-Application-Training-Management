using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PleaseStayWithUs.Data_Access_Layer;
using PleaseStayWithUs.Business_Entity_Layer;
using System.Text.RegularExpressions;

namespace PleaseStayWithUs.Controllers
{
    public class EmployeeController : Controller
    {
        TrainingManagementEntities1 contextobj = new TrainingManagementEntities1();  
        EmployeeBEL employeeBELObject = new EmployeeBEL();
        Employee empObj;

        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EmployeeUserProfile(string btn)

        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    string EmpID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(EmpID, @"\d+").Value;
                    int EID = Convert.ToInt32(output);
                    employeeBELObject.updateDatabaseBL(EID);
                    empObj = employeeBELObject.getEmployeeDetailsBL(EID);
                    Session["UserName"] = empObj.Name;

                    TempData["ID"] = EID;
                    Session["MID"] = empObj.MID;
                    ViewData["EmployeeDetails"] = empObj;
                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }
                }

            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
         
            return View();
        }
        public ActionResult EmployeeChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeChangePassword(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    //int id = Convert.ToInt32(TempData["ID"]);
                    string EmpID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(EmpID, @"\d+").Value;
                    int EID = Convert.ToInt32(output);
                    string OldPass = Request.Form["txtOldPassword"];
                    string NewPass = Request.Form["txtNewPassword"];

                    int s = employeeBELObject.changePasswordBL(EID, OldPass, NewPass);
                    if (s == 1)
                    {
                        ViewData["up"] = "Your password has been changed successfully";
                    }
                    else
                    {
                        ViewData["up"] = "Sorry incorrect old password";
                    }

                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }
                }
            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
          
           
            return View();
        }
        [HttpGet]
        public ActionResult seeHistory(int page = 1, string sort = "CourseID", string sortdir = "asc", string search = "", string btn = "")
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    string EmpID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(EmpID, @"\d+").Value;
                    int EID = Convert.ToInt32(output);

                    int pageSize = 10;
                    int totalRecord = 0;
                    if (page < 1) page = 1;
                    int skip = (page * pageSize) - pageSize;
                    var data = employeeBELObject.History(EID, search, sort, sortdir, skip, pageSize, out totalRecord);
                    ViewBag.TotalRows = totalRecord;
                    // ViewBag.search = search;

                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }

                    return View(data);

                }
            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
           
            return View();
        }

        [HttpPost]
        public ActionResult seeHistory(string btn2,int page = 1, string sort = "CourseID", string sortdir = "asc", string search = "",string btn="")
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    string srchtxt = Request.Form["txtsearch"];
                    string EmpID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(EmpID, @"\d+").Value;
                    int EID = Convert.ToInt32(output);

                    int pageSize = 10;
                    int totalRecord = 0;
                    if (page < 1) page = 1;
                    int skip = (page * pageSize) - pageSize;
                    var data = employeeBELObject.History(EID, search, sort, sortdir, skip, pageSize, out totalRecord);
                    ViewBag.TotalRows = totalRecord;
                    
                    if (search == "Search")
                    {
                        List<Request> list = employeeBELObject.SearchHistoryBL(srchtxt, EID);
                        return View(list);
                    }
                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }

                    return View(data);

                }

            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }

          
            
            return View();
        }

        public ActionResult FeedbackForm(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    string EmpID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(EmpID, @"\d+").Value;
                    int EID = Convert.ToInt32(output);
                    int courseid = employeeBELObject.feedbackBeforeForm1(EID);
                    

                    Session["CourseID"] = courseid;

                    if (courseid == 0)
                    { ViewData["Notification"] = "You Have No Pending Feedbacks"; }
                    else
                    {
                        return RedirectToAction("FeedbackFormAfter", "Employee");
                    }
                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }
                }
            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
          
            return View();
        }
        public ActionResult FeedbackFormAfter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FeedbackFormAfter(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    string EmpID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(EmpID, @"\d+").Value;
                    int EID = Convert.ToInt32(output);
                    try
                    {

                        if (btn == "Submit")
                        {

                            int ManID = Convert.ToInt32(Session["MID"]);

                            int CIDD = Convert.ToInt32(  Session["CourseID"].ToString());
                            int ans1 = Convert.ToInt32(Request.Form["rdo1"]);
                            int ans2 = Convert.ToInt32(Request.Form["rdo2"]);
                            int ans3 = Convert.ToInt32(Request.Form["rdo3"]);
                            int ans4 = Convert.ToInt32(Request.Form["rdo4"]);
                            int ans5 = Convert.ToInt32(Request.Form["rdo5"]);
                            int result = employeeBELObject.FeedbackScoreBL(EID, CIDD, ManID, ans1, ans2, ans3, ans4, ans5);

                            if (result > 0)
                                return RedirectToAction("FeedbackForm", "Employee");
                            else
                                ViewData["aa"] = "Hi";
                        }
                        if (btn == "SignOut")
                        {
                            Session.Clear();
                            Session.Abandon();
                            return RedirectToAction("CourseList", "Login");
                        }
                    }
                    catch (Exception e)
                    {
                        ViewData["exception"] = e.Message;
                    }
                }


            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
           
            return View();
        }


        public ActionResult AvailableCourse(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    int count = employeeBELObject.getCourseDetailsBL().Count();
                    List<Cours> List = employeeBELObject.availableCoursesBL();
                    ViewData["count"] = count;

                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }
                    return View(List);
                }

            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
           
            return View();
        }

        public ActionResult RequestCourse(int id = 0)
        {
            Session["CourseID"] = id;

            return View(contextobj.Courses.Find(id));
        }
        [HttpPost]
        public ActionResult RequestCourse(Cours cobj,string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    string EmpID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(EmpID, @"\d+").Value;
                    int EID = Convert.ToInt32(output);
                    int Courseid = Convert.ToInt32( Session["CourseID"]);
                    string result = employeeBELObject.RegisterCourseBL(EID, Courseid);
                    ViewData["result"] = result;

                }
                if (btn == "SignOut")
                {
                    Session.Clear();
                    Session.Abandon();
                    return RedirectToAction("CourseList", "Login");
                }
            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
            
            return View();
        }
        public ActionResult welcome(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
        
            return View();
        }

        public ActionResult upcomingCourses(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    List<Cours> list = employeeBELObject.availableCoursesBL();
                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }
                    return View(list);
                }
            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
          return View();
        }
        public ActionResult CancelRequest(int id = 0)
        {
            Session["RequestID"] = id;

            return View(contextobj.Requests.Find(id));
        }
        [HttpPost]
        public ActionResult CancelRequest(Request cobj,string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    if (btn == "Cancel")
                    {
                        string EmpID = (string)Session["UserIDloggedIn"];

                        string output = Regex.Match(EmpID, @"\d+").Value;
                        int EID = Convert.ToInt32(output);
                        int Requestid = Convert.ToInt32(Session["RequestID"]);

                        string result = employeeBELObject.CancelRequest(Requestid);

                        if (result == "Cancelled")
                        {
                            ViewData["result"] = "Your Request is Cancelled";
                        }
                        if (result == "Cannot Cancel")
                        {
                            ViewData["result"] = "Sorry! Request can't be cancelled One day before start of course";
                        }
                    }
                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }
                }
            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
           

                return View();
        }
        public ActionResult AplliedCourse(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    string EmpID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(EmpID, @"\d+").Value;
                    int EID = Convert.ToInt32(output);
                    List<Request> list = employeeBELObject.AppliedCourse(EID);

                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }
                    return View(list);
                }
            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
           
            return View();
        }


       
       

     

    }
}