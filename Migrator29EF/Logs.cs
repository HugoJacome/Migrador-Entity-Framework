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
    
    public partial class Logs
    {
        public int Id { get; set; }
        public Nullable<int> ActivityId { get; set; }
        public Nullable<int> EntityId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string UserId { get; set; }
        public Nullable<long> InstitutionId { get; set; }
        public string Description { get; set; }
        public Nullable<int> AgencyId { get; set; }
    
        public virtual Activities Activities { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Branches Branches { get; set; }
        public virtual Entities Entities { get; set; }
        public virtual Institutions Institutions { get; set; }
    }
}
