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
    
    public partial class CUENTA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CUENTA()
        {
            this.CLIENTE_CUENTA = new HashSet<CLIENTE_CUENTA>();
        }
    
        public string CTA_NUMERO { get; set; }
        public Nullable<System.DateTime> CTA_FECHA_APERTURA { get; set; }
        public Nullable<int> EST_CTA_CODIGO { get; set; }
        public Nullable<int> CTA_MONEDA { get; set; }
        public int TIP_CTA_CODIGO { get; set; }
        public string INST_CODIGO { get; set; }
        public Nullable<decimal> CTA_SALDO_CONTABLE { get; set; }
        public Nullable<decimal> CTA_SALDO_DISPONIBLE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENTE_CUENTA> CLIENTE_CUENTA { get; set; }
        public virtual ESTADO_CUENTA ESTADO_CUENTA { get; set; }
        public virtual INSTITUCION INSTITUCION { get; set; }
        public virtual TIPO_CUENTA TIPO_CUENTA { get; set; }
    }
}
