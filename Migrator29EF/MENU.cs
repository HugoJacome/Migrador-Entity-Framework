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
    
    public partial class MENU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MENU()
        {
            this.MENU_PERFIL = new HashSet<MENU_PERFIL>();
        }
    
        public int MEN_CODIGO { get; set; }
        public string MEN_NOMBRE { get; set; }
        public string MEN_URL { get; set; }
        public string MEN_ICONO { get; set; }
        public string MEN_TITULO { get; set; }
        public string MEN_PARENT { get; set; }
        public Nullable<int> MEN_PARENT_ID { get; set; }
        public Nullable<int> MEN_ORDINAL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MENU_PERFIL> MENU_PERFIL { get; set; }
    }
}
