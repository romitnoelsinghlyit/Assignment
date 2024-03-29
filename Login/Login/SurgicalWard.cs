//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Login
{
    using System;
    using System.Collections.Generic;
    
    public partial class SurgicalWard
    {
        public string SurgicalWardID { get; set; }
        public string PatientID { get; set; }
        public int PatientNumber { get; set; }
        public string EmergencyRecordID { get; set; }
        public string ElectiveRecordID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<System.DateTime> ArrivalDate { get; set; }
        public string Consultant { get; set; }
    
        public virtual Elective Elective { get; set; }
        public virtual Emergency Emergency { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
