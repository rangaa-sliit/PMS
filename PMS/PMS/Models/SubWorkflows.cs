//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubWorkflows
    {
        public int SubWorkflowId { get; set; }
        public int WorkflowId { get; set; }
        public string WorkflowRole { get; set; }
        public int WorkflowStep { get; set; }
        public string ConsideringArea { get; set; }
        public bool IsSpecificUser { get; set; }
        public string WorkflowUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    
        public virtual AspNetRoles AspNetRoles { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Workflows Workflows { get; set; }
    }
}
