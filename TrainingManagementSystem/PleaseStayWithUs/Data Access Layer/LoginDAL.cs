using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;

namespace PleaseStayWithUs.Data_Access_Layer
{
    public class LoginDAL
    {

        TrainingManagementEntities1 entityObject = new TrainingManagementEntities1();

        
        Employee employeeobject = new Employee();
        Manager managerObject = new Manager();
        Login loginObject = new Login();
        Admin adminObject = new Admin();

        public int Login(string name, string pwd)
        {
            
            int flag = 0;
            try
            {
                managerObject = entityObject.Managers.Where(a => a.ManagerUserID == name && a.Pwd == pwd).SingleOrDefault();
                employeeobject = entityObject.Employees.Where(a => a.UserID == name && a.Pwd == pwd).SingleOrDefault();
                adminObject = entityObject.Admins.Where(a => a.AdminID == name && a.AdminPassword == pwd).SingleOrDefault();

                if (employeeobject != null)
                {
                    if (employeeobject.UserID == name && employeeobject.Pwd == pwd)
                    {
                        flag = 1;
                    }

                }
                if (managerObject != null)
                {
                    if (managerObject.ManagerUserID == name && managerObject.Pwd == pwd)
                    {
                        flag = 2;
                    }

                }

                if (adminObject != null)
                {
                    if (adminObject.AdminID == name && adminObject.AdminPassword == pwd)
                    {
                        flag = 3;
                    }

                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return flag;
        }

        public bool CheckSecurityEmployee(string id, string qus, string ans)
        {
            bool check = false;
            try
            {
                Employee employeeobject = entityObject.Employees.Where(data => data.SecurityQuestion == qus && data.Answer == ans && data.UserID == id).SingleOrDefault();
                if (employeeobject != null && employeeobject.UserID == id)
                {
                   
                        check = true;
                   
                       
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return check;
        }

        public bool CheckSecurityManager(string id, string qus, string ans)
        {
            bool check = false;
            try
            {
                Manager managerObject = entityObject.Managers.Where(data => data.SecurityQuestion == qus && data.Answer == ans && data.ManagerUserID == id).SingleOrDefault();
                if (managerObject != null && managerObject.ManagerUserID==id)
                {
                    check = true;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return check;
        }
        public void ChangePasswordEmployee(int id, string pass)
        {
            try
            {
                Employee employeeobj = (from data in entityObject.Employees where data.EID == id select data).Single();
                employeeobj.Pwd = pass;

                entityObject.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public void ChangePasswordManager(string id, string pass)
        {
            try
            {
                Manager managerObject = (from data in entityObject.Managers where data.ManagerUserID == id select data).SingleOrDefault();

                managerObject.Pwd = pass;
                entityObject.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public string EmployeeLogin(string uname, string pwd)
        {

            string userID = " ";
            try
            {
                loginObject = entityObject.Logins.Where(a => a.UserName == uname && a.Pwd == pwd).SingleOrDefault();
                if (loginObject.Designation == "Employee")
                {
                    employeeobject = entityObject.Employees.Where(a => a.UserName == loginObject.UserName).SingleOrDefault();
                    if (employeeobject.Pwd != null)
                    {
                        userID = "User Exists";
                    }
                    else
                        userID = employeeobject.UserID;
                }

                else if (loginObject.Designation == "Manager")
                {
                    managerObject = entityObject.Managers.Where(a => a.UserName == loginObject.UserName).SingleOrDefault();
                    if (managerObject.Pwd != null)
                    {
                        userID = "User Exists";
                    }
                    else
                        userID = managerObject.ManagerUserID;
                }



            }
            catch(Exception e)
            {
                throw e;
                
            }
            return userID;
        }

        public void Register(string uid, string name, int age, string gender, string email, string pwd, string sq, string ans)
        {
            try
            {
                employeeobject = entityObject.Employees.Where(a => a.UserID == uid).SingleOrDefault();
                managerObject = entityObject.Managers.Where(a => a.ManagerUserID == uid).SingleOrDefault();


                if (employeeobject != null && employeeobject.Pwd == null)
                {
                    employeeobject.Name = name;
                    employeeobject.Age = age;
                    employeeobject.Gender = gender;
                    employeeobject.EmailID = email;
                    employeeobject.Pwd = pwd;
                    employeeobject.SecurityQuestion = sq;
                    employeeobject.Answer = ans;
                }
                if (managerObject != null && managerObject.Pwd == null)
                {
                    managerObject.Name = name;
                    managerObject.Age = age;
                    managerObject.Gender = gender;
                    managerObject.Pwd = pwd;
                    managerObject.EmailID = email;
                    managerObject.SecurityQuestion = sq;
                    managerObject.Answer = ans;
                }
                entityObject.SaveChanges();
            }

            catch (Exception e)
            {
                throw e;
            }

        }
        public List<Cours> CourseListDL( string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            
            try
            {
                DateTime todayDate = System.DateTime.Now;
                using (TrainingManagementEntities1 entityObject = new TrainingManagementEntities1())
                {
                    var tempList = (from a in entityObject.Courses
                             where a.StartDate > todayDate
                             select a
                          );
                    totalRecord = tempList.Count();
                    tempList = tempList.OrderBy(sort + " " + sortdir);
                    if (pageSize > 0)
                    {
                        tempList = tempList.Skip(skip).Take(pageSize);
                    }


                    return tempList.ToList();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
        public List<string> SecurityQuestionsDL()
        {
            List<string> SecurityQuestionList = new List<string>();
            List<SecurityQuestion> List;
            try
            {
                List = (from data in entityObject.SecurityQuestions select data).ToList();
               
                foreach (SecurityQuestion data in List)
                {
                    SecurityQuestionList.Add(data.Question);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return SecurityQuestionList;
        }
        public string EmployeeSecurityQuestionDL(string UserID)
        {
            Employee employeeObject=new Employee();
            string employeeQuestion;
            try
            {
             employeeObject = (from data in entityObject.Employees where data.UserID == UserID select data).SingleOrDefault();
                employeeQuestion = employeeObject.SecurityQuestion;
            }
            catch(Exception e)
            {
                throw e;
            }
            return employeeQuestion;
        }
    }
}