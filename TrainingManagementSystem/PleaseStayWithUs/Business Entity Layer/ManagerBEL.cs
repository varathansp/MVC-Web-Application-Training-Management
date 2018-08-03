using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PleaseStayWithUs.Data_Access_Layer;

namespace PleaseStayWithUs.Business_Entity_Layer
{
    public class ManagerBEL
    {
        ManaherDAL managerDALObject = new ManaherDAL();
        Manager managerObject = new Manager();
        Login loginObject = new Login();

        

        public List<Employee> GetEmployeeDetailsBL(int MID, string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            try
            {
                var employeeList = managerDALObject.GetEmployeesDL(MID, search, sort, sortdir, skip, pageSize, out totalRecord);
                return employeeList.ToList();
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

     
        public Manager ManagerProfile(int ID)
        {
            try
            {
                managerObject = managerDALObject.ManagerProfile(ID);
                return managerObject;
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
        public string  ChangePasswordBL(int ID, string OldPass, string NewPass)
        {
            try
            {
                string result = managerDALObject.ChangePasswordDL(ID, OldPass, NewPass);
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
          
        }

        public List<Request> ViewRequestBL(int MID, string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            try
            {
                var tempList = managerDALObject.ViewRequest(MID, search, sort, sortdir, skip, pageSize, out totalRecord);
                return tempList.ToList();
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
        public string AcceptRejectBL(int id, string option,string reason)
        {
            try
            {
                string result = managerDALObject.AcceptRejectDL(id, option,reason);
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }

        public int ReasonBL(int id, string reason)
        {
            try
            {
                int result = managerDALObject.ReasonDL(id, reason);
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
        public List<FeedbackList> ViewFeedBackBL(int MID) 
        {
            try
            {
                List<FeedbackList> feedbackList = managerDALObject.ViewFeedBack(MID);
                return feedbackList;
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
        public List<Employee> SearchDetailsBL(string name)
        {
            try
            {
                List<Employee> searchList = managerDALObject.SearchDetailsDL(name);
                return searchList;
            }
            catch (Exception e)
            {
                throw e;
            }
          
        }
        public List<Request> SearchRequestBL(string searchtext,int MID)
        {
            try
            {
                List<Request> searchList = managerDALObject.SearchRequestsDL(searchtext, MID);
                return searchList;
            }
            catch (Exception e)
            {
                throw e;
            }
          
        }


    }
}