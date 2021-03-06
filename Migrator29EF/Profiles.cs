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
    
    public partial class Profiles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profiles()
        {
            this.Cards = new HashSet<Cards>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int BINId { get; set; }
        public string IMK_AC_SPEC { get; set; }
        public string CVK_SPEC { get; set; }
        public string PVK_SPEC { get; set; }
        public decimal ATMDailyLimit { get; set; }
        public int CardValidity { get; set; }
        public int PINValidity { get; set; }
        public int ProviderId { get; set; }
        public string ProviderFileNameFormat { get; set; }
        public string ProviderHeaderFormat { get; set; }
        public string ProviderRecordFormat { get; set; }
        public string ProviderFooterFormat { get; set; }
        public Nullable<decimal> PurchaseDailyLimit { get; set; }
        public Nullable<int> OrderType { get; set; }
        public Nullable<bool> InputFilePerBranch { get; set; }
        public Nullable<int> MaxCashTransPerDay { get; set; }
        public Nullable<decimal> CashLimit { get; set; }
        public Nullable<decimal> MaintenanceFee { get; set; }
        public string CVK2_SPEC { get; set; }
        public string Config { get; set; }
        public string EnvelopeFormat { get; set; }
    
        public virtual BINs BINs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cards> Cards { get; set; }
        public virtual Providers Providers { get; set; }
    }
}
