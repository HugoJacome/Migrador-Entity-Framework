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
    
    public partial class PUNTOPAGO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PUNTOPAGO()
        {
            this.USUARIO_PPAGO = new HashSet<USUARIO_PPAGO>();
        }
    
        public int PPACODIGO { get; set; }
        public string ENTCODIGO { get; set; }
        public int SUCCODIGO { get; set; }
        public Nullable<int> TCACODIGO { get; set; }
        public string PPAUBICACION { get; set; }
        public string PPADEPENDENCIA { get; set; }
        public string PPAESTADO { get; set; }
        public string PPAZONA { get; set; }
        public string PPFECHA_EXPIRACION { get; set; }
        public string PPHORA_EXPIRACION { get; set; }
        public string PPHORAF_EXPIRACION { get; set; }
        public string PMAC { get; set; }
        public string PDIAS { get; set; }
    
        public virtual AGENCIA AGENCIA { get; set; }
        public virtual INSTITUCION INSTITUCION { get; set; }
        public virtual TIPOCANAL TIPOCANAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO_PPAGO> USUARIO_PPAGO { get; set; }
    }
}
