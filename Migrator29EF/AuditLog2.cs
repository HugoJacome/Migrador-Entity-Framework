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
    
    public partial class AuditLog2
    {
        public long Id { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string Institution { get; set; }
        public string UserName { get; set; }
        public string ResourceName { get; set; }
        public string ResourceType { get; set; }
        public long ResourceId { get; set; }
        public string EntityType { get; set; }
        public string EntityName { get; set; }
        public string Action { get; set; }
        public string Data { get; set; }
        public int Result { get; set; }
        public Nullable<long> BranchId { get; set; }
        public string IP { get; set; }
    }
}
