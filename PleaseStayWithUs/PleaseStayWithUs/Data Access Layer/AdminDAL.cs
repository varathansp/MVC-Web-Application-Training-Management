using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;


namespace PleaseStayWithUs.Data_Access_Layer
{
    public class AdminDAL
    {
        TrainingManagementEntities1 contextObject = new TrainingManagementEntities1();

        Manager managerObject = new Manager();
        Login loginObject = new Login();
      
        Employee employeeObject = new Employee(); 

        public int AddManager(string UserName, string Password, string Designition)
        {
            try
            {
                loginObject.UserName = UserName;
                loginObject.Pwd = Password;
                loginObject.Designation = Designition;
                managerObject.UserName = UserName;
                managerObject.Designation = Designition;

                contextObject.Logins.Add(loginObject);
                contextObject.Managers.Add(managerObject);
                contextObject.SaveChanges();
                return 1;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public int AddEmployee(string UserName, string Password, string Designition, int managerID)
        {
            try
            {

                loginObject.UserName = UserName;
                loginObject.Pwd = Password;
                employeeObject.UserName = UserName;


                employeeObject.MID = managerID;
                loginObject.Designation = Designition;
                contextObject.Logins.Add(loginObject);

                contextObject.Employees.Add(employeeObject);
                contextObject.SaveChanges();
                return 1;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int AddTrainer( string Name, string mail, string qualification)
        {
            try
            {
                int trainerMailFound = 0;
                List<Trainer> list = (from data in contextObject.Trainers select (data)).ToList();
             foreach(Trainer data in list)
                {
                    if(data.TrainerMail==mail)
                    {
                        trainerMailFound++;
                    }
                }   
                
             if(trainerMailFound==0)
                {

                    Trainer trainerObject = new Trainer();
                    trainerObject.TrainerName = Name;
                    trainerObject.TrainerMail = mail;
                    trainerObject.Qualification = qualification;
                    contextObject.Trainers.Add(trainerObject);
                    contextObject.SaveChanges();
                    trainerObject = (from data in contextObject.Trainers where data.TrainerMail == mail select data).SingleOrDefault();
                    return trainerObject.TrainerID;
                }
             if(trainerMailFound>0)
                {
                    return 3;
                }
               

            }
            catch(Exception e)
            {
                throw e;
            }
            return 0;
        }
        public int AddCourse( string courseName, string DName, DateTime SDate, DateTime EDate, string Session, string TID)
        {
            int result = 0;
            try
            {
                int flag1 = 1;
                int flag2 = 1;
            List<Course> TrainerCourses = (from data in contextObject.Courses /*where data.TrainerID == TID*/ select data).ToList();
            foreach(Course data in TrainerCourses)
            {
                flag1++;
                    if ((data.StartDate <= SDate && data.EndDate <= EDate && Session != data.Session) || (data.StartDate >= SDate && data.EndDate >= EDate && Session != data.Session) || (data.StartDate <= SDate && data.EndDate >= EDate && Session != data.Session) || (data.StartDate >= SDate && data.EndDate <= EDate && Session != data.Session) || (data.EndDate < SDate) || (data.StartDate > EDate) || ((data.StartDate == SDate || data.EndDate == EDate) && Session != data.Session))
                    {
                    flag2++;
                }

            }
            if (flag1 == flag2)
            {
                    Course courseObject = new Course();
                    courseObject.CourseName = courseName;
                courseObject.DomainName = DName;
                courseObject.StartDate = SDate;
                courseObject.EndDate = EDate;
                courseObject.Session = Session;
                
                contextObject.Courses.Add(courseObject);
                contextObject.SaveChanges();
                    int CourseTableCount = contextObject.Courses.Count();
                    courseObject = contextObject.Courses.OrderBy(a => 1 == 1).Skip(CourseTableCount - 1).FirstOrDefault();
                    result = courseObject.CourseID;
            }
            if (flag1 != flag2)
            {
                 result= 3;
            }
            }
            catch(Exception e)
            {
                throw e;
            }
            return result;
        }
        public List<int> GetManagerID()
        {
            List<Manager> managerIDListst;
            List<int> managerIDList = new List<int>();
            try
            {
                managerIDListst = (from data in contextObject.Managers where data.Pwd != null select data).ToList();
                foreach (Manager data in managerIDListst)
                {
                    managerIDList.Add(data.MID);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return managerIDList;
        }
        public List<string> GetDomain()
        {
            List<Domain> domainNameList;
            List<string> domainList = new List<string>();
            try
            {
                domainNameList = (from data in contextObject.Domains  select data).ToList();
                foreach (Domain data in domainNameList)
                {
                    domainList.Add(data.DomainName);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return domainList;
        }

        public List<int> GetTrainerID()
        {
            List<Trainer> trainerIDListst;
            List<int> trainerIDList = new List<int>();
            try
            {
                trainerIDListst = (from data in contextObject.Trainers select data).ToList();
                foreach (Trainer data in trainerIDListst)
                {
                    trainerIDList.Add(data.TrainerID);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return trainerIDList;
        }

        public List<Manager> GetmanagerIDListstDL(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            try
            {
                using (TrainingManagementEntities1 entityObject = new TrainingManagementEntities1())
                {
                    var managers = (from data in entityObject.Managers select data);
                    totalRecord = managers.Count();
                    managers = managers.OrderBy(sort + " " + sortdir);
                    if (pageSize > 0)
                    {
                        managers = managers.Skip(skip).Take(pageSize);
                    }
                    return managers.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

           
            
        }


        public List<Trainer> TrainersListDL(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            try
            {
                using (TrainingManagementEntities1 entityObject = new TrainingManagementEntities1())
                {
                    var Trainers = (from data in entityObject.Trainers select data);
                    totalRecord = Trainers.Count();
                    Trainers = Trainers.OrderBy(sort + " " + sortdir);
                    if (pageSize > 0)
                    {
                        Trainers = Trainers.Skip(skip).Take(pageSize);
                    }
                    return Trainers.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }



        }

        public List<Trainer> TrainerDetailsDL()
        {
            try
            {
                List<Trainer> TrainerList = (from data in contextObject.Trainers select data).ToList();
                return TrainerList;
            }
            catch(Exception e) {
                throw e;
            }
        }

    }
}