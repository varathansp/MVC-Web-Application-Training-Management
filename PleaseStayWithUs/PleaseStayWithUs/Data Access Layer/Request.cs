//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PleaseStayWithUs.Data_Access_Layer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public int RequestID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<int> ManagerID { get; set; }
        public Nullable<int> CourseID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Session { get; set; }
        public string CourseName { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Manager Manager { get; set; }
    }
}
