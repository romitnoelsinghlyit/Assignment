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
    
    public partial class Billing
    {
        public string BillingID { get; set; }
        public string PatientID { get; set; }
        public int PatientNumber { get; set; }
        public Nullable<int> XRAY { get; set; }
        public Nullable<int> MRI { get; set; }
        public Nullable<int> CTScan { get; set; }
        public Nullable<int> Ultrasound { get; set; }
        public Nullable<int> BloodTest { get; set; }
        public Nullable<int> SugarTest { get; set; }
        public Nullable<int> AllergyTest { get; set; }
        public System.DateTime DischargeDate { get; set; }
    
        public virtual Patient Patient { get; set; }
    }
}
