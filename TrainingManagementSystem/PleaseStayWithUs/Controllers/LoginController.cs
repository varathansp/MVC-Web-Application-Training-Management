using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PleaseStayWithUs.Business_Entity_Layer;
using System.Web.UI;
using PleaseStayWithUs.Data_Access_Layer;
using System.Text.RegularExpressions;

namespace PleaseStayWithUs.Controllers
{
    public class LoginController : Controller
    {
        
        TrainingManagementEntities1 entityObject = new TrainingManagementEntities1();
        List<Cours> courseList = new List<Cours>();
        LoginBEL businessLayerObj = new LoginBEL();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult TrainingLogin(string btn)
        {
            Session["UserIDForForgrtPassword"] = Request.Form["txtUserID"];
            try
            {
                int e;
                if (btn == "Login")
                {
                    
                    Session["UserID"] = Request.Form["txtUserID"];

                    e = businessLayerObj.Login(Request.Form["txtUserID"], Request.Form["txtPassword"]);
                    if (e == 1)
                    {
                        Session["UserIDloggedIn"] = Request.Form["txtUserID"];
                        return RedirectToAction("EmployeeUserProfile", "Employee");
                    }
                    if (e == 2)
                    {
                        Session["UserIDloggedIn"] = Request.Form["txtUserID"];
                        return RedirectToAction("ManagerProfile", "Manager");
                    }
                    if (e == 3)
                    {
                        Session["UserIDloggedIn"] = Request.Form["txtUserID"];
                        return RedirectToAction("AdminHome", "Admin");
                    }
                    else
                    {
                        Session["UserIDForForgrtPassword"] = Request.Form["txtUserID"];
                        ViewData["error"] = "Incorrect user name or password"; }

                }
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }
         
        }
        public ActionResult MyQuestion(string btn)
        {
            try
            {
                if (btn == "MyQuestion")

                {
                    string EmployeeQuestion ;
                    Session["ForgetPasswordID"] = Request.Form["txtUserID"];
                    EmployeeQuestion = businessLayerObj.EmployeeSecurityQuestionBL(Request.Form["txtUserID"]);
                    Session["EmployeeQuestion"] = EmployeeQuestion;
                    if (EmployeeQuestion != null)
                    {
                        return RedirectToAction("ForgotPassword", "Login");
                    }
                    if(EmployeeQuestion==null)
                    {
                        ViewData["error"] = "Incorrect userID";
                    }
               
                   
                }
                return View();
            }
            catch (Exception e)
            {
                ViewData["exception"] = "User ID not found";
            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            try
            {

                return View();
            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string btn)
        {
           
            try
            {
                string EmployeeQuestion="";
                if (btn == "MyQuestion")

                {
                    Session["ForgetPasswordID"]=Request.Form["txtUserID"];
                    EmployeeQuestion = businessLayerObj.EmployeeSecurityQuestionBL(Request.Form["txtUserID"]);
                    Session["EmployeeQuestion"] = EmployeeQuestion;
                    ViewData["EmployeeQuestion"] = EmployeeQuestion;
                    return View();
                }
                if (btn == "SUBMIT")
                {
                    bool result = businessLayerObj.CheckSecurityEmployee(Session["ForgetPasswordID"].ToString(), Session["EmployeeQuestion"].ToString(), Request.Form["Answer"]);
                    ViewData["Error"] = "Submit";
                    if (result == true)
                    {
                        Session["FUserID"] = Request.Form["txtUserID"];
                        return RedirectToAction("RecreatePassword", "Login");

                    }
                    bool res = businessLayerObj.CheckSecurityManager(Session["ForgetPasswordID"].ToString(), Session["EmployeeQuestion"].ToString(), Request.Form["Answer"]);
                    if (res == true)
                    {
                        Session["FUserID"] = Request.Form["txtUserID"];
                        return RedirectToAction("RecreatePassword", "Login");
                    }
                    else
                    {
                        ViewData["Error"] = "Incorrect Details";
                    }

                }
                return View();
            }
            catch (Exception e)
            {
                ViewData["exception"] = e.Message;
            }
            return View();
        }
        public ActionResult RecreatePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecreatePassword(string btn)
        {
            try
            {
                string EmpID = (string)Session["FUserID"];

                string output = Regex.Match(EmpID, @"\d+").Value;
                int EID = Convert.ToInt32(output);
                
                if (btn == "SUBMIT")
                {
                    businessLayerObj.ChangePasswordEmployee(EID, Request.Form["pwd"]);
                    return RedirectToAction("CourseList", "Login");
                    
                }
                else
                {

                }
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
        
        }
        public ActionResult TrainingRegister()
        {

            //try
            //{
                List<SelectListItem> SecurityQuestionDDL = new List<SelectListItem>();
                string id = Session["RUUserID"].ToString();
                ViewData["ID"] = id;
                List<string> SecurityQuestionList = businessLayerObj.SecurityQuestionsBL();
                SecurityQuestionDDL.Add(new SelectListItem { Text = "Select", Value = "" });
                foreach (string data in SecurityQuestionList)
                {
                    SecurityQuestionDDL.Add(new SelectListItem { Text = Convert.ToString(data), Value = Convert.ToString(data) });
                }
                ViewData["SecurityQuestionDDL"] = SecurityQuestionDDL;
                return View();
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
           
        }
        [HttpPost]
        public ActionResult TrainingRegister(string btn)
        {
            
            try
            {
                List<SelectListItem> SecurityQuestionDDL = new List<SelectListItem>();
                List<string> SecurityQuestionList = businessLayerObj.SecurityQuestionsBL();
                
                foreach (string data in SecurityQuestionList)
                {
                    SecurityQuestionDDL.Add(new SelectListItem { Text = Convert.ToString(data), Value = Convert.ToString(data) });
                }
                ViewData["SecurityQuestionDDL"] = SecurityQuestionDDL;

                string id = Session["RUUserID"].ToString();

                if (btn == "REGISTER")
                {

                    businessLayerObj.Register(id, Request.Form["name"], Convert.ToInt32(Request.Form["age"]), Request.Form["Gender"], Request.Form["email"], Request.Form["password"], Request.Form["Question"], Request.Form["answer"]);
                    ViewBag.Message = string.Format("Registered SuccessFully");
                    return RedirectToAction("CourseList", "Login");

                }
                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
          
        }

        public ActionResult EmployeeLogin(string btn)
        {
            try
            {
                if (btn == "SignIn")
                {
                    string access = businessLayerObj.EmployeeLogin(Request.Form["username"], Request.Form["password"]);
                    if (access == "NotFound")
                    {
                        ViewData["Error"] = "Incorrect details";
                    }
                    else if (access == "User Exists")
                    {
                        ViewData["Error"] = "You have already registered";
                    }
                    else
                    {

                        Session["RUUserID"] = access;

                        return RedirectToAction("TrainingRegister", "Login");
                    }

                }




                return View();
            }
            catch (Exception e)
            {
                throw e;
            }
          
        }
        public ActionResult RegisterLogin(string btn)
        {
            try
            {
                int e;
                if (btn == "Signin")
                {
                    Session["UserID"] = Request.Form["txtUserID"];

                    e = businessLayerObj.Login(Request.Form["txtUserID"], Request.Form["txtPassword"]);
                    if (e == 1)
                    {
                        Session["UserIDloggedIn"] = Request.Form["txtUserID"];
                        return RedirectToAction("EmployeeUserProfile", "Employee");
                    }
                    if (e == 2)
                    {
                        Session["UserIDloggedIn"] = Request.Form["txtUserID"];
                        return RedirectToAction("ManagerHomePage", "Manager");
                    }
                    if (e == 3)
                    {
                        Session["UserIDloggedIn"] = Request.Form["txtUserID"];
                        return RedirectToAction("AdminHome", "Admin");
                    }
                    else
                    { ViewData["error"] = "Incorrect User Details"; }

                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
      
        }


        public ActionResult CourseList(int page = 1, string sort = "CourseID", string sortdir = "asc", string search = "",string btn="")
        {
            //try
            //{
                int pageSize = 10;
                int totalRecord = 0;
                if (page < 1) page = 1;
                int skip = (page * pageSize) - pageSize;
                var data = businessLayerObj.CourseListBL(search, sort, sortdir, skip, pageSize, out totalRecord);
                ViewBag.TotalRows = totalRecord;
                ViewBag.search = search;


                int e;
                if (btn == "Signin")
                {
                    Session["UserID"] = Request.Form["txtUserID"];

                    e = businessLayerObj.Login(Request.Form["txtUserID"], Request.Form["txtPassword"]);
                    if (e == 1)
                    {
                        Session["UserIDloggedIn"] = Request.Form["txtUserID"];
                        return RedirectToAction("EmployeeUserProfile", "Employee");
                    }
                    if (e == 2)
                    {
                        Session["UserIDloggedIn"] = Request.Form["txtUserID"];
                        return RedirectToAction("ManagerProfile", "Manager");
                    }
                    if (e == 3)
                    {
                        Session["UserIDloggedIn"] = Request.Form["txtUserID"];
                        return RedirectToAction("AdminHome", "Admin");
                    }

                    else
                    { ViewData["error"] = "Incorrect user name or password"; }

                }


                return View(data);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

          
        }
    }
}