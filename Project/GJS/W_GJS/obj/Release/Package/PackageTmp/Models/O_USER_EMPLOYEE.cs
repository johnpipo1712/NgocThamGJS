//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace W_GJS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class O_USER_EMPLOYEE
    {
        public long CD { get; set; }
        public Nullable<long> USER_CD { get; set; }
        public Nullable<long> EMPLOYEE_CD { get; set; }
        public Nullable<long> STATUS { get; set; }
        public Nullable<bool> ACTIVE { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
    
        public virtual M_EMPLOYEE M_EMPLOYEE { get; set; }
        public virtual S_USER S_USER { get; set; }
    }
}
