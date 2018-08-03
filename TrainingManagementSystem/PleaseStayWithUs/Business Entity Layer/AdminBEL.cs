using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PleaseStayWithUs.Data_Access_Layer;

namespace PleaseStayWithUs.Business_Entity_Layer
{
    public class AdminBEL
    {
        
        AdminDAL adminDALObject = new AdminDAL();
        
        int result = 0;
        public int AddEmployee(string UserName, string Password, string Desigantion, int managerID)
        {
            
            try
            {

                result = adminDALObject.AddEmployee(UserName, Password, Desigantion, managerID);

            }
            catch(Exception e)
            {
                throw e;
            }
            return result;
        }

        public int AddManager(string UserName, string Password, string Desigantion)
        {
            int resultt = 0;
            try
            {

                resultt = adminDALObject.AddManager(UserName, Password, Desigantion);

            }
            catch (Exception e)
            {
                throw e;
            }
            return resultt;
        }
        public int AddTrainer( string Name, string mail, string Qual)
        {
            
            try
            {
                result = adminDALObject.AddTrainer( Name, mail, Qual);

            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
        public int AddCourse( string CName, string DName, DateTime SDate, DateTime EDate, string Session, string TID)
        {
            
            try
            {
                result = adminDALObject.AddCourse( CName, DName, SDate, EDate, Session, TID);

            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
        public List<int> GetManager()
        {
            List<int> managerList = new List<int>();
            try
            {
                managerList = adminDALObject.GetManagerID();

            }
            catch (Exception e)
            {
                throw e;
            }
            return managerList;
        }

        public List<string> GetDomain()
        {
            List<string> domainList = new List<string>();
            try
            {
                domainList = adminDALObject.GetDomain();

            }
            catch (Exception e)
            {
                throw e;
            }
            return domainList;
        }

        public List<int> GetTrainer()
        {
            List<int> trainerList = new List<int>();
            try
            {
                trainerList = adminDALObject.GetTrainerID();

            }
            catch (Exception e)
            {
                throw e;
            }
            return trainerList;
        }
        public List<Manager> GetManagerListBL(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            try
            {
                var managerList = adminDALObject.GetmanagerIDListstDL(search, sort, sortdir, skip, pageSize, out totalRecord);
                return managerList.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }

           
        }
        public List<Trainer> TrainersListBL(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            
            try
            {
               var TrainerList = adminDALObject.TrainersListDL(search, sort, sortdir, skip, pageSize, out totalRecord);
                return TrainerList.ToList();
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
         public List<Trainer> TrainerDetailsBL()
        {
            try
            {
                List<Trainer> TrainerList = adminDALObject.TrainerDetailsDL();
                return TrainerList;
            }
            catch(Exception e) {
                throw e;
            }
        }
    }
}