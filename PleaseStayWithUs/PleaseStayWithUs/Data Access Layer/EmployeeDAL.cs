using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;

namespace PleaseStayWithUs.Data_Access_Layer
{
    public class EmployeeDAL
    {
        TrainingManagementEntities1 entityObject = new TrainingManagementEntities1();
        List<Request> requestList = new List<Request>();
        Employee employeeObj = new Employee();
        
        int result;

        public void UpdateDatabase(int EmpID)
        {
            DateTime todayDate = System.DateTime.Now;

            try
            {
                List<Request> requestList = (from data in entityObject.Requests where data.EmployeeID == EmpID && (data.Status != "Requested" || data.Status != "Rejected" || data.Status != "Cancelled" )select data).ToList();

                foreach (Request data in requestList)
                {

                    if (todayDate >= data.StartDate && todayDate < data.EndDate && data.Status != "Requested" && data.Status != "Rejected" && data.Status != "Cancelled")
                    {
                        data.Status = "On Going";
                    }
                    else if (todayDate >= data.EndDate && data.Status != "Requested" && data.Status != "Rejected" && data.Status != "Cancelled")
                    {
                        data.Status = "Completed";
                    }
                    else if (todayDate < data.StartDate && data.Status != "Requested" && data.Status != "Rejected" && data.Status != "Cancelled")
                    {
                        data.Status = "Accepted";
                    }
                    else
                    { }
                    
                }
                entityObject.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Employee GetEmployeeDAL(int ID)
        {
            try
            {
                employeeObj = (from data in entityObject.Employees where data.EID == ID select data).Single();

            }
            catch (Exception e)
            {
                throw e;
            }
            return employeeObj;


        }
        public int ChangePasswordDL(int ID, string OldPass, string NewPass)
        {
            
            try
            {
                employeeObj = (from data in entityObject.Employees where data.Pwd == OldPass && data.EID == ID select data).Single();
                employeeObj.Pwd = NewPass;
                    entityObject.SaveChanges();
                result = 1;
               
            }
            catch(Exception e)
            {
                throw e;
            }
            return result;
        }

       
        

        public List<Request> ViewHistory(int ID, string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            using (TrainingManagementEntities1 entityObject = new TrainingManagementEntities1())
            {
                var list = (from a in entityObject.Requests where  a.EmployeeID == ID select a);
                totalRecord = list.Count();
                list = list.OrderBy(sort + " " + sortdir);
                if (pageSize > 0)
                {
                   list = list.Skip(skip).Take(pageSize);
                }
                return list.ToList();
            }
        }
        public int FeedbackBeForeForm(int employeeID)
        {

            int courseID = 0;
            int flag = 0;

            DateTime dt = System.DateTime.Now;
            
            try
            {
                List<Request> History = (from da in entityObject.Requests where da.EmployeeID == employeeID select da).ToList();

                foreach (Request data in History)
                {
                    TimeSpan datediff = Convert.ToDateTime(data.EndDate) - dt;
                    double diff = (datediff).TotalDays;
                    Feedback feedbackObject = entityObject.Feedbacks.Where(a => a.EmployeeID == data.EmployeeID && a.CourseID == data.CourseID).SingleOrDefault();
                    if (feedbackObject == null)
                    {
                        if (diff < 1 && diff > -1)
                        {
                        courseID = (int)data.CourseID;
                            flag = 1;
                        }

                    }
                    
                }
                if (flag == 0) { courseID = 0; }


            }
            catch(Exception e)
            {
                throw e;
            }
            return courseID;
        }
        public int FeedbackScoreDL(int EID, int courseList, int MID, double score)
        {
            int result = 0;
            try
            {
                Course TrainerObj = (from data in entityObject.Courses where data.CourseID == courseList select data).SingleOrDefault();
                Feedback feedbackObject = new Feedback();
                feedbackObject.TrainerID = TrainerObj.TrainerID;
                feedbackObject.ManagerID = MID;
                feedbackObject.EmployeeID = EID;
                feedbackObject.CourseID = courseList;
                feedbackObject.Score = Convert.ToString(score);
                
                entityObject.Feedbacks.Add(feedbackObject);
                result = entityObject.SaveChanges();
                result++;
            }
            catch (Exception e)
            {
                throw e;
            }


            return result;
        }

        public string RegisterCourseDL(int EID, int courseList)
        {
            int flag1 = 0;
            int flag2 = 0;
            string result = "hi";
            Employee EmpDetail = (from emp in entityObject.Employees where emp.EID == EID select emp).SingleOrDefault();
            Course courseDetails = (from data in entityObject.Courses where data.CourseID == courseList select data).Single();
            int MID = Convert.ToInt32(EmpDetail.MID);
            string CourseName = courseDetails.CourseName;
            string session = courseDetails.Session;
            DateTime startDate =Convert.ToDateTime(courseDetails.StartDate);
            DateTime endDate = Convert.ToDateTime(courseDetails.EndDate);

            DateTime todayDate = System.DateTime.Now;
            TimeSpan datediff = startDate - todayDate;
            double diff = (datediff).TotalDays;
             
            try
            {
                List<Request> employeeData = (from a in entityObject.Requests where a.EmployeeID == EID && (a.Status!="Cancelled" && a.Status!="Rejected") select a).ToList();
                foreach (Request data in employeeData)
                {
                   
                    flag1++;
                    if ((data.StartDate <= startDate && data.EndDate <= endDate && session != data.Session) || (data.StartDate >= startDate && data.EndDate >= endDate && session != data.Session) || (data.StartDate <= startDate && data.EndDate >= endDate && session != data.Session) || (data.StartDate >= startDate && data.EndDate <= endDate && session != data.Session) || (data.EndDate < startDate) || (data.StartDate > endDate) || ((data.StartDate == startDate || data.EndDate == endDate) && session != data.Session))
                    {
                        flag2++;
                    }

                }
                if((flag1==flag2) && diff<=1 )
                {
                    result = "Sorry! you can't Register one day before";
                }
                if ((flag1 == flag2) && diff>=1)
                {
                    result = "You have registered for this course.Wait for your manager response";
                    Request newRequest = new Request();
                    newRequest.EmployeeID = EID;
                    newRequest.ManagerID = MID;
                    newRequest.CourseID = courseList;
                    newRequest.StartDate = startDate;
                    newRequest.EndDate = endDate;
                    newRequest.Session = session;
                    newRequest.Status = "Requested";
                    newRequest.CourseName = CourseName;
                    entityObject.Requests.Add(newRequest);
                    entityObject.SaveChanges();
                }
                if (flag1!=flag2)
                {
                    result = "Sorry! On this day you will be undergoing another Course";
                }



            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
        public List<Course> GetCourseDetailsDL()
        {
            
            List<Course> courseList = new List<Course>();
            try
            {
                courseList = (from data in entityObject.Courses select data).ToList();
            }
            catch(Exception e)
            {
                throw e;
            }
            return courseList;
        }

        public List<Course> AvailableCourses()
        {
            DateTime todayDate = System.DateTime.Now;
            List<Course> CourseList = new List<Course>();
            CourseList = (from data in entityObject.Courses where data.StartDate>todayDate select data).ToList();
            return CourseList;
        }
        public string CancelRequest(int id)
        {
            string result = "";
            DateTime dt = System.DateTime.Now;
            try
            {

                Request requestObj = entityObject.Requests.Where(a => a.RequestID == id).Single();
                TimeSpan datediff = Convert.ToDateTime(requestObj.StartDate) - dt;
                double diff = (datediff).TotalDays;
                if (diff > 1)
                {
                    requestObj.Status = "Cancelled";
                    result = "Cancelled";
                    entityObject.SaveChanges();

                }
                else
                {
                    result = "Cannot Cancel";
                }
                
            }
            catch(Exception e)
            {
                throw e;
            }
            return result;
        }
        public List<Request> AplliedCourses(int eid )
        {
            List<Request> requestList = new List<Request>();
            try
            {

                requestList = (from data in entityObject.Requests where data.EmployeeID == eid && (data.Status == "Accepted" || data.Status == "Requested") select data).ToList();
            }
            catch(Exception e)
            {
                throw e;
            }
            return requestList;

        }


        public string FeedbackFormAvailabilityDL(int EID)
        {
            DateTime todayDate = System.DateTime.Now;
            string result = "NoPending";

            try

            {
                List<Request> List = (from data in entityObject.Requests where data.EmployeeID == EID  /*(data.Status == "On Going" || data.Status == "Completed")*/ select data).ToList();
                foreach (Request data in List)
                {
                    TimeSpan datediff = Convert.ToDateTime(data.EndDate) - todayDate;
                    double diff = (datediff).TotalDays;
                    if (diff < 1.5 && diff > -1.5)
                    {
                        result = "Pending";
                    }
                }

                if (List.Count == 0)
                {
                    result = "NoPending";
                }
            }
            catch(Exception e)
            {
                throw e;
            }
           
            return result;

        }
        public List<Request> FeedbackFormForCourses(int EID)
        {
            List<Request> pendingList = new List<Request>();
            try
            {
                DateTime todayDate = System.DateTime.Now;
                

                requestList = (from data in entityObject.Requests where data.EmployeeID == EID  select data).ToList();
                foreach (Request da in  requestList)
                {
                   
                        TimeSpan datediff = Convert.ToDateTime(da.EndDate) - todayDate;
                    double diff = (datediff).TotalDays;
                    if (diff < 1.5 && diff > -1.5)
                    {
                       
                        Request requestObject = new Request();
                        requestObject.RequestID = da.RequestID;
                        requestObject.CourseID = da.CourseID;
                        requestObject.EmployeeID = da.EmployeeID;
                        requestObject.ManagerID = da.ManagerID;
                        requestObject.CourseName = da.CourseName;
                        requestObject.StartDate = da.StartDate;
                        requestObject.EndDate = da.EndDate;
                        requestObject.Session = da.Session;
                        requestObject.Status = da.Status;
                        requestObject.Reason = da.Reason;
                        pendingList.Add(requestObject);

                    }
                    }
               
            }
            catch (Exception e)
            {
                throw e;
            }
            return pendingList;

        }

        public int FeedbackScoreNewDL(int RID, double score)
        {
            int result = 0;
            try
            {
                Request requestObject = (from data in entityObject.Requests where data.RequestID == RID select data).SingleOrDefault();
                Course courseObject = (from data in entityObject.Courses where data.CourseID == requestObject.CourseID select data).SingleOrDefault();
                Feedback feedbackObject = new Feedback();

                feedbackObject.ManagerID = Convert.ToInt32(requestObject.ManagerID);
                feedbackObject.EmployeeID = Convert.ToInt32(requestObject.EmployeeID);
                feedbackObject.CourseID = (int)(requestObject.CourseID);
                feedbackObject.TrainerID = (courseObject.TrainerID);
                  feedbackObject.Score = Convert.ToString(score);
              
                entityObject.Feedbacks.Add(feedbackObject);
                result = entityObject.SaveChanges();
                result++;
            }
            catch (Exception e)
            {
                throw e;
            }


            return result;
        }
        public List<Request> SearchHistoryDL(string srchtxt, int EID)
        {
            try
            {
                requestList = entityObject.Requests.Where(a => a.CourseName == srchtxt && a.EmployeeID == EID).ToList();
                
            }
            catch(Exception e)
            {
                throw e;
            }
            return requestList;
        }
    }

}