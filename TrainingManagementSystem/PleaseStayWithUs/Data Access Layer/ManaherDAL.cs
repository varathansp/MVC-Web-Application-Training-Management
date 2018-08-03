using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;

namespace PleaseStayWithUs.Data_Access_Layer
{
    public class ManaherDAL
    {
        TrainingManagementEntities1 entityObj = new TrainingManagementEntities1();
        Manager managerObject = new Manager();
        public List<Employee> GetEmployeesDL(int MID, string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (TrainingManagementEntities1 entityObject = new TrainingManagementEntities1())
                {
                    var employeeDetails = (from data in entityObject.Employees where data.MID == MID select data);

                    totalRecord = employeeDetails.Count();
                    employeeDetails = employeeDetails.OrderBy(sort + " " + sortdir);
                    if (pageSize > 0)
                    {
                        employeeDetails = employeeDetails.Skip(skip).Take(pageSize);
                        employees = employeeDetails.ToList();
                    }
                    
                }
                return employees;
            }
            catch (Exception e)
            {
                throw e;
            }
            

        }
       
        public Manager ManagerProfile(int ID)
        {

            try
            {
                managerObject = (from data in entityObj.Managers where data.MID == ID select data).Single();

            }
            catch (Exception e)
            {
                throw e;
            }
            return managerObject;
        }
        public  string  ChangePasswordDL(int ID,string OldPass ,string NewPass)   
        {
            string result = "";
            try
            {
                
                managerObject = entityObj.Managers.Where(a => a.MID == ID && a.Pwd == OldPass).Single();
                managerObject.Pwd = NewPass;
                entityObj.SaveChanges();
                result = "changed";
            }
            catch(Exception e)
            {
                throw e;
            }
            return result;

        }
        public List<Request> ViewRequest(int MID, string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            List<Request> requestList = new List<Request>();
            totalRecord = 0;
            try
            {
                DateTime todayDate = System.DateTime.Now;
                using (TrainingManagementEntities1 dc = new TrainingManagementEntities1())
                {
                    var tempList = (from data in dc.Requests
                             where data.ManagerID == MID && data.StartDate > todayDate && (data.Status == "Requested" | data.Status == "Accepted") && data.Status != "Cancelled"
                             select data);
                    totalRecord = tempList.Count();
                    tempList = tempList.OrderBy(sort + " " + sortdir);
                    if (pageSize > 0)
                    {
                        tempList = tempList.Skip(skip).Take(pageSize);
                        requestList= tempList.ToList();
                    }
                }
            }
            catch
            {
            }

            return requestList;


        }
        public string AcceptRejectDL(int id, string Option,string reason)
        {
            Request RequestObj = new Request();
            try
            {
                using (TrainingManagementEntities1 entityObject = new TrainingManagementEntities1())
                {
                    if (Option == "Accept")
                    {
                        RequestObj = entityObject.Requests.Where(a => a.RequestID == id).Single();
                        RequestObj.Status = "Accepted";
                        RequestObj.Reason = reason;

                        entityObject.SaveChanges();
                    }
                    if (Option == "Reject")
                    {
                        RequestObj = entityObject.Requests.Where(a => a.RequestID == id).Single();
                        RequestObj.Status = "Rejected";
                        RequestObj.Reason = reason;

                        entityObject.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
                return "Done";
        }
        public int ReasonDL(int id, string reason)
        {
            int result = 0;
            try
            {
                Request RequestObj = new Request();
                RequestObj = entityObj.Requests.Where(a => a.RequestID == id).Single();
                RequestObj.Reason = reason;
                 result = entityObj.SaveChanges();
                
            }
            catch
            {

            }
            return result;

        }

        public List<FeedbackList> ViewFeedBack(int MID)
        {
            List<FeedbackList> feedbackList = new List<FeedbackList>(); 
            try
            {
                var tempList = (from feedBack in entityObj.Feedbacks join courses in entityObj.Courses on feedBack.CourseID equals courses.CourseID join trainer in entityObj.Trainers on courses.TrainerID equals trainer.TrainerID where feedBack.ManagerID == MID select new { feedBack.CourseID, courses.CourseName, feedBack.EmployeeID, feedBack.TrainerID, trainer.TrainerName, feedBack.Score }).ToList();
                foreach (var data in tempList)
                {
                    FeedbackList feedbackObject = new FeedbackList();
                    feedbackObject.CourseID =  (data.CourseID);
                    feedbackObject.CourseName = data.CourseName;
                    feedbackObject.EmployeeID = data.EmployeeID;
                    feedbackObject.TrainerID = (int)data.TrainerID;
                    feedbackObject.TrainerName = data.TrainerName;
                    feedbackObject.Score = data.Score;
                    feedbackList.Add(feedbackObject);
                }

            }
            catch(Exception e)
            {
                throw e;
            }
            return feedbackList;
        }
        public List<Employee> SearchDetailsDL(string name)
        {
            try
            {
                List<Employee> searchlist = entityObj.Employees.Where(a => a.UserName.Contains(name) | a.EmailID.Contains(name) | a.Name.Contains(name)).ToList();
                return searchlist;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<Request> SearchRequestsDL(string searchtext,int MID)
        {
            try
            {
                List<Request> searchList = entityObj.Requests.Where(a => a.CourseName.Contains(searchtext) | a.Status.Contains(searchtext) | a.Reason.Contains(searchtext) && a.ManagerID == MID).ToList();
                return searchList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }



}
