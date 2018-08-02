using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PleaseStayWithUs.Business_Entity_Layer;
using PleaseStayWithUs.Data_Access_Layer;
using System.Text.RegularExpressions;

namespace PleaseStayWithUs.Controllers
{
    public class ManagerController : Controller
    {
        
        TrainingManagementEntities1 entityObject = new TrainingManagementEntities1();
        ManagerBEL managerBELObject = new ManagerBEL();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManagerHomePage()
        {
            return View();
        }
        public ActionResult ManagerProfile(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    Manager mobj = new Manager();
                    string MgrID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(MgrID, @"\d+").Value;
                    int MID = Convert.ToInt32(output);
                    TempData["ID"] = MID;
                    mobj = managerBELObject.ManagerProfile(MID);
                    Session["UserName"] = mobj.Name;
                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }
                    ViewData["ManagerDetails"] = mobj;
                }
            }
            catch(Exception e)
            {
                ViewData["exception"] = e.Message;
            }
           
            return View();
        }
        public ActionResult ManagerChangePassword()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult ManagerChangePassword(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {


                    string MgrID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(MgrID, @"\d+").Value;
                    int MID = Convert.ToInt32(output);

                    string OldPass = Request.Form["txtOldPassword"];
                    string NewPass = Request.Form["txtNewPassword"];



                    string s = managerBELObject.ChangePasswordBL(MID, OldPass, NewPass);
                    if (s == "changed")
                    {
                        ViewData["up"] = "Your password has been changed successfully";

                    }
                    if (s == "Not changed")
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
        public ActionResult EmployeeDetails(int page = 1, string sort = "UserName", string sortdir = "asc", string search = "",string btn="")
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {

                    string MgrID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(MgrID, @"\d+").Value;
                    int MID = Convert.ToInt32(output);

                    int pageSize = 5;
                    int totalRecord = 0;
                    if (page < 1) page = 1;
                    int skip = (page * pageSize) - pageSize;
                    var data = managerBELObject.GetEmployeeDetailsBL(MID, search, sort, sortdir, skip, pageSize, out totalRecord);
                    ViewBag.TotalRows = totalRecord;


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
        public ActionResult EmployeeDetails(string btn2,int page = 1, string sort = "UserName", string sortdir = "asc", string search = "", string btn = "")
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {

                    string name = Request.Form["txtsearch"];
                    string MgrID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(MgrID, @"\d+").Value;
                    int MID = Convert.ToInt32(output);

                    int pageSize = 5;
                    int totalRecord = 0;
                    if (page < 1) page = 1;
                    int skip = (page * pageSize) - pageSize;
                    var data = managerBELObject.GetEmployeeDetailsBL(MID, search, sort, sortdir, skip, pageSize, out totalRecord);
                    ViewBag.TotalRows = totalRecord;

                    if (search == "Search")
                    {

                        List<Employee> searchlist = managerBELObject.SearchDetailsBL(name);
                        return View(searchlist);


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

       
        [HttpGet]
        public ActionResult ViewRequests(int page = 1, string sort = "CourseID", string sortdir = "asc", string search = "",string btn="")
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {

                    string MgrID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(MgrID, @"\d+").Value;
                    int MID = Convert.ToInt32(output);

                    int pageSize = 5;
                    int totalRecord = 0;
                    if (page < 1) page = 1;
                    int skip = (page * pageSize) - pageSize;
                    var data = managerBELObject.ViewRequestBL(MID, search, sort, sortdir, skip, pageSize, out totalRecord);
                    ViewBag.TotalRows = totalRecord;
                    ViewBag.search = search;
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
        public ActionResult ViewRequests(string btn2,int page = 1, string sort = "CourseID", string sortdir = "asc", string search = "", string btn = "")
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {

                    string MgrID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(MgrID, @"\d+").Value;
                    int MID = Convert.ToInt32(output);
                    string searchtext = Request.Form["txtsearch"];
                    int pageSize = 5;
                    int totalRecord = 0;
                    if (page < 1) page = 1;
                    int skip = (page * pageSize) - pageSize;
                    var data = managerBELObject.ViewRequestBL(MID, search, sort, sortdir, skip, pageSize, out totalRecord);
                    ViewBag.TotalRows = totalRecord;

                    if (search == "Search")
                    {
                        List<Request> list = managerBELObject.SearchRequestBL(searchtext, MID);
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



        //public ActionResult newview(int id, string txt)
        //{
            
        //    var data = managerBELObject.AcceptRejectBL(id, txt);
        //    ViewData["s"] = data;
        //    return RedirectToAction("reasonview");
        //}
        //[HttpGet]
        //public ActionResult reasonview(int id=0)
        //{
            
        //    return View(entityObject.Requests.Find(id));
        //}
        //[HttpPost]
        //public ActionResult reasonview(int id, string text, Request ReqObj,string btn)
        //{
        //    try
        //    {
        //        if (Session["UserIDloggedIn"] == null)
        //        {
        //            return RedirectToAction("CourseList", "Login");
        //        }
        //        if (Session["UserIDloggedIn"] != null)
        //        {

        //            id = ReqObj.RequestID;
                   
        //            string reason = Request["Reason"];
        //            var data = managerBELObject.AcceptRejectBL(id, text,reason);
        //            var dd = managerBELObject.ReasonBL(id, reason);
        //            if (btn == "SignOut")
        //            {
        //                Session.Clear();
        //                Session.Abandon();
        //                return RedirectToAction("CourseList", "Login");
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ViewData["exception"] = e.Message;
        //    }


        //    return RedirectToAction("ViewRequests");
        //}
        public ActionResult AcceptRequest(int id=0)
        {
            Session["CourseID"] = id;
           
            return View(entityObject.Requests.Find(id));
        }
        [HttpPost]
        public ActionResult AcceptRequest(int id,Request cobj, string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                      string reason = Request["Reason"];
                        int Courseid = Convert.ToInt32(Session["CourseID"]);
                        string option = "Accept";
                        string result = managerBELObject.AcceptRejectBL(Courseid, option, reason);
                        return RedirectToAction("ViewRequests", "Manager");

                   
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

        public ActionResult RejectRequest(int id = 0)
        {
            Session["CourseID"] = id;

            return View(entityObject.Requests.Find(id));
        }
        [HttpPost]
        public ActionResult RejectRequest(int id, Request cobj, string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                   
                        string reason = Request["Reason"];
                        int Courseid = Convert.ToInt32(Session["CourseID"]);
                        string option = "Reject";
                        string result = managerBELObject.AcceptRejectBL(Courseid, option, reason);
                        ViewData["result"] = result;
                        return RedirectToAction("ViewRequests", "Manager");
                    

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
        public ActionResult CousreFeedback( string btn )
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    string MgrID = (string)Session["UserIDloggedIn"];

                    string output = Regex.Match(MgrID, @"\d+").Value;
                    int MID = Convert.ToInt32(output);
                    List<FeedbackList> List = managerBELObject.ViewFeedBackBL(MID);
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

    }
}