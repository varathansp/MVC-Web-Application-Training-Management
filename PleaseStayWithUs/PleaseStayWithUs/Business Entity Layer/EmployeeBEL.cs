using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PleaseStayWithUs.Data_Access_Layer;

namespace PleaseStayWithUs.Business_Entity_Layer
{
    public class EmployeeBEL
    {

        EmployeeDAL employeeDALObject = new EmployeeDAL();
        Employee employeeObject;
        int result;

        public void updateDatabaseBL(int EID)
        {
            try
            {
                employeeDALObject.UpdateDatabase(EID);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public Employee getEmployeeDetailsBL(int ID)
        {
            try
            {
                employeeObject = employeeDALObject.GetEmployeeDAL(ID);
            }
            catch (Exception e)
            {
                throw e;
            }
            return employeeObject;
        }
        public int changePasswordBL(int ID, string OldPass, string NewPass)
        {
            try
            {
                int result = employeeDALObject.ChangePasswordDL(ID, OldPass, NewPass);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        
        }

        public List<Request> History(int EmpID, string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            var v = employeeDALObject.ViewHistory(EmpID, search, sort, sortdir, skip, pageSize, out totalRecord);
            return v.ToList();
        }
        public List<Course> getCourseDetailsBL()
        {
            List<Course> List = employeeDALObject.GetCourseDetailsDL();
            return List;
        }


        public int feedbackBeforeForm1(int id)
        {
            int cid = employeeDALObject.FeedbackBeForeForm(id);
            return cid;
        }

        public int FeedbackScoreBL(int EID, int CID, int MID, int ans1, int ans2, int ans3, int ans4, int ans5)
        {
            
            try
            {
                double score = (ans1 + ans2 + ans3 + ans4 + ans5) / 5;
                result = employeeDALObject.FeedbackScoreDL(EID, CID, MID, score);
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
    

      

        public string RegisterCourseBL(int EID, int CID)
        {
            try
            {
                string result = employeeDALObject.RegisterCourseDL(EID, CID);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

           
            
        }

        public List<Course> availableCoursesBL()
        {
            try
            {
                List<Course> CourseList = employeeDALObject.AvailableCourses();
                return CourseList;
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }
        public string CancelRequest(int id)
        {
            string result="";
            try
            {
                 result = employeeDALObject.CancelRequest(id);
                
            }
            catch(Exception e)
            {
                throw e;
            }
            return result;

        }
        public List<Request> AppliedCourse(int eid)
        {
            List<Request> requestList = new List<Request>();
            try
            {
                 requestList = employeeDALObject.AplliedCourses(eid);
                
            }
            catch(Exception e)
            {
                throw e;
            }
            return requestList;


        }
        public string FeedbackFormAvailabilityBL(int EID)
        {
            string result = "";
            try
            {
                result = employeeDALObject.FeedbackFormAvailabilityDL(EID);
            }
            catch(Exception e)
            {
                throw e;
            }
            
            return result;

        }
        public List<Request> PendingListBL(int EID)
        {
            try
            {
                List<Request> List = employeeDALObject.FeedbackFormForCourses(EID);
                return List;
            }
            catch (Exception e)
            {
                throw e;
            }

            
            
        }
        public int FeedbackScoreNew(int RID, int ans1, int ans2, int ans3, int ans4, int ans5)
        {
            int result = 0;
            try
            {
                double score = (ans1 + ans2 + ans3 + ans4 + ans5) / 5;
                result = employeeDALObject.FeedbackScoreNewDL(RID, score);
                
            }
            catch(Exception e)
            {
                throw e;
            }
            return result;

        }
        public List<Request> SearchHistoryBL(string srchtxt,int EID)
        {
            try
            {
                List<Request> list = employeeDALObject.SearchHistoryDL(srchtxt, EID);
                return list;

            }
            catch (Exception e)
            {
                throw e;
            }
         
        }

    }
}