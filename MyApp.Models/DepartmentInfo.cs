//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepartmentInfo
    {
        public int StudentId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string Session { get; set; }
        public string Cgpa { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual Student Student { get; set; }
    }
}