//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Migrator29EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transactions
    {
        public long Id { get; set; }
        public long AuthorizerId { get; set; }
        public long AcquirerId { get; set; }
        public string ATM_CODE { get; set; }
        public System.DateTime Process_Date { get; set; }
    
        public virtual Acquirers Acquirers { get; set; }
        public virtual Authorizers Authorizers { get; set; }
    }
}
