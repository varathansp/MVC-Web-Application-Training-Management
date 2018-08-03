using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PleaseStayWithUs.Business_Entity_Layer;
using System.Globalization;

namespace PleaseStayWithUs.Controllers
{
    public class AdminController : Controller
    {
        AdminBEL adminBELObject = new AdminBEL();
        
        public ActionResult Index()
        {
            return View();
        }
      

        public ActionResult AddManager()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddManager(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    if (btn == "ADD")
                    {
                        string UserName = Request.Form["txtUserName"];
                        string Password = Request.Form["txtPassword"];
                        string Designation = "Manager"; /*Request.Form["txtDesignation"];*/
                        int s = adminBELObject.AddManager(UserName, Password, Designation);
                        if (s == 1) { ViewData["alertManager"] = "Manager Added"; }
                        else { ViewData["alertManager1"] = "Manager already Exist"; }
                    }
                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }
                }

            }
            catch(Exception e)
            {
                ViewData["alertManager1"] = "Manager already Exist"; 
            }
           
           


            return View();
        }

        public ActionResult AddEmployee()
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    List<SelectListItem> managerDropDown = new List<SelectListItem>();
                    List<int> managerList = adminBELObject.GetManager();
                    managerDropDown.Add(new SelectListItem { Text="Select" ,Value=""});

                    foreach (int data in managerList)
                    {
                        managerDropDown.Add(new SelectListItem { Text = Convert.ToString(data), Value = Convert.ToString(data) });
                    }
                    ViewData["managerDropDown"] = managerDropDown;
                }
            }
            catch (Exception e)
            {
                ViewData["exception"]=e.Message;
            }
            
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    List<SelectListItem> managerDropDown = new List<SelectListItem>();
                    List<int> managerList = adminBELObject.GetManager();
                    managerDropDown.Add(new SelectListItem { Text = "Select",Value="" });
                    foreach (int data in managerList)
                    {
                        managerDropDown.Add(new SelectListItem { Text = Convert.ToString(data), Value = Convert.ToString(data) });
                    }
                    ViewData["managerDropDown"] = managerDropDown;
                    if (btn == "ADD")
                    {
                        string UserName = Request.Form["txtUserName"];
                        string Password = Request.Form["txtPassword"];
                        string Designation = "Employee"; /* Request.Form["txtDesignation"];*/
                        var MID = Request.Form["txtManagerID"];
                        int s = adminBELObject.AddEmployee(UserName, Password, Designation, Convert.ToInt32(MID));
                        if (s == 1) { ViewData["alertEmployee1"] = "Employee Added"; }
                        if (s == 2) { ViewData["alertEmployee"] = "Employee already Exist"; }
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
                ViewData["exception"]=e.Message;
            }
          
            return View();
        }

        public ActionResult AddTrainer()
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                   

                    return View();
                }
            }
            catch (Exception e)
            {
                ViewData["exception"]=e.Message;
            }
           
            return View();
        }
        [HttpPost]
        public ActionResult AddTrainer(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                   
                    if (btn == "ADD")
                    {
                     
                        string Name = Request.Form["txtTrainerName"];
                        string Mail = Request.Form["txtTrainerMail"];
                        string Qual = Request.Form["txtQualification"];
                        int s = adminBELObject.AddTrainer( Name, Mail, Qual);
                       
                        if (s == 2) { ViewData["alertTrainer"] = "Trainer ID already exist use other ID"; }
                        if (s == 3) { ViewData["alertTrainer"] = "Trainer mail id already exists"; }
                       else
                        {
                            ViewData["alertTrainer1"] = "Trainer added and trainer ID is";

                            ViewData["ID"] = s;
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
                ViewData["exception"]=e.Message;
            }
           
            return View();
        }
        public ActionResult AddCourse()
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                      
                    List<SelectListItem> domainDropDown = new List<SelectListItem>();
                    List<string> domain = adminBELObject.GetDomain();
                    domainDropDown.Add(new SelectListItem { Text = "Select",Value="" });
                    foreach (string data in domain)
                    {
                        domainDropDown.Add(new SelectListItem { Text = (data), Value = (data) });
                    }
                    ViewData["domainDropDown"] = domainDropDown;

                    List<SelectListItem> Trainerddl = new List<SelectListItem>();
                    List<int> TList = adminBELObject.GetTrainer();
                    Trainerddl.Add(new SelectListItem { Text = "Select", Value = "" });
                    foreach (int data in TList)
                    {
                        Trainerddl.Add(new SelectListItem { Text = Convert.ToString(data), Value = Convert.ToString(data) });
                    }
                    ViewData["TrainerDDL"] = Trainerddl;


             

                    return View();
                }
            }
            catch (Exception e)
            {
                ViewData["exception"]=e.Message;
            }
          
            return View();

        }


    
        [HttpPost]
        public ActionResult AddCourse(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    List<SelectListItem> domainDropDown = new List<SelectListItem>();
                    List<string> domain = adminBELObject.GetDomain();

                    foreach (string data in domain)
                    {
                        domainDropDown.Add(new SelectListItem { Text = (data), Value = (data) });
                    }
                    ViewData["domainDropDown"] = domainDropDown;

                    List<SelectListItem> Trainerddl = new List<SelectListItem>();
                    List<int> TList = adminBELObject.GetTrainer();
                    foreach (int data in TList)
                    {
                        Trainerddl.Add(new SelectListItem { Text = Convert.ToString(data), Value = Convert.ToString(data) });
                    }
                    ViewData["TrainerDDL"] = Trainerddl;


                    

                    if (btn == "ADD")
                    {
                        
                        string Name = Request.Form["txtCourseName"];
                        string DName = Request.Form["domain"];

                        DateTime SDate = DateTime.ParseExact(Request.Form["txtStartDate"], "dd/MM/yyyy", null);
                        DateTime EDate = DateTime.ParseExact(Request.Form["txtEndDate"], "dd/MM/yyyy", null);
                        string time = Request.Form["Session"];
                        string TID = Request.Form["txTrainerID"];
                        if (SDate < DateTime.UtcNow.Date) { ViewData["alertCourse"] = "Please Enter forecoming Date"; }
                        else if (SDate > EDate) { ViewData["alertCourse"] = "End Date can't be less than start Date"; }
                        else
                        {
                            int s = adminBELObject.AddCourse( Name, DName, SDate, EDate, time, TID);
                            
                            if (s == 2) { ViewData["alertCourse"] = "Course already exists"; }
                            if (s == 3) { ViewData["alertCourse"] = "Trainer allocated to other course"; }
                            else
                            { ViewData["alertCourse1"] = "Course added and course id is ";

                                ViewData["ID"] = s;
                            }
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
                ViewData["exception"]=e.Message;
            }
          
            return View();
        }
        public ActionResult AdminHome(int page = 1, string sort = "Name", string sortdir = "asc", string search = "",string btn="")
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    Session["UserName"] = "Admin";
                    int pageSize = 10;
                    int totalRecord = 0;
                    if (page < 1) page = 1;
                    int skip = (page * pageSize) - pageSize;
                    var data = adminBELObject.GetManagerListBL(search, sort, sortdir, skip, pageSize, out totalRecord);
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
                ViewData["exception"]=e.Message;
            }
           
            return View();
        }
      
        public ActionResult TrainerDetails(string btn)
        {
            try
            {
                if (Session["UserIDloggedIn"] == null)
                {
                    return RedirectToAction("CourseList", "Login");
                }
                if (Session["UserIDloggedIn"] != null)
                {
                    var data = adminBELObject.TrainerDetailsBL();
                
                    if (btn == "SignOut")
                    {
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("CourseList", "Login");
                    }
                    return View(data); 
                }
            }
            catch(Exception e) {
                ViewData["exception"] = e.Message;

            }
            return View();
            
        }
    }
}