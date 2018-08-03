using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PleaseStayWithUs.Data_Access_Layer
{
    public class FeedbackList
    {
        [Display(Name = "Course ID")]
        public int CourseID { get; set; }
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        [Display(Name = "Employee ID")]
        public Nullable<int> EmployeeID { get; set; }
        [Display(Name = "Trainer ID")]
        public int TrainerID { get; set; }
        [Display(Name = "Trainer Name")]
        public string TrainerName { get; set; }
        public string Score { get; set; }
        
        
    }
}