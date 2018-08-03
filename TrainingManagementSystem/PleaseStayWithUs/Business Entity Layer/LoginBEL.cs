using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PleaseStayWithUs.Data_Access_Layer;

namespace PleaseStayWithUs.Business_Entity_Layer
{
    public class LoginBEL
    {
        LoginDAL loginDALObject = new LoginDAL();

        public int Login(string uid, string pwd)
        {
            try
            {
                int result;
                result = loginDALObject.Login(uid, pwd);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public bool CheckSecurityEmployee(string id, string ques, string ans)
        {
            try
            {
                bool result;
                result = loginDALObject.CheckSecurityEmployee(id, ques, ans);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public bool CheckSecurityManager(string id, string ques, string ans)
        {
            try
            {
                bool result;
                result = loginDALObject.CheckSecurityManager(id, ques, ans);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public void ChangePasswordEmployee(int id, string pass)
        {
            try
            {
                loginDALObject.ChangePasswordEmployee(id, pass);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public void ChangePasswordManager(string id, string pass)
        {
            try
            {
                loginDALObject.ChangePasswordManager(id, pass);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public string EmployeeLogin(string uname, string pwd)
        {
            try
            {
                string id = loginDALObject.EmployeeLogin(uname, pwd);
                return id;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public void Register(string uid, string name, int age, string gender, string email, string pwd, string sq, string ans)
        {
            try
            {
                loginDALObject.Register(uid, name, age, gender, email, pwd, sq, ans);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<Cours> CourseListBL(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            try
            {
                var courseList = loginDALObject.CourseListDL(search, sort, sortdir, skip, pageSize, out totalRecord);
                return courseList.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public List<string> SecurityQuestionsBL()
        {
            List<string> SecurityQuestionList;
            //try
            //{
                SecurityQuestionList = loginDALObject.SecurityQuestionsDL();
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            return SecurityQuestionList;
        }
        public string EmployeeSecurityQuestionBL(string UserID)
        {
            string employeeQuestion;
            try {
                employeeQuestion = loginDALObject.EmployeeSecurityQuestionDL(UserID);
            }
            catch(Exception e)
            { throw e; }
            return employeeQuestion;
        }
    }
}