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
    
    public partial class ESTADOS_TARJETA
    {
        public int EST_TAR_CODIGO { get; set; }
        public int EST_TAR_CODIGO_PARENT { get; set; }
        public string EST_TAR_CODIGO_ESTADO { get; set; }
    
        public virtual ESTADO_TARJETA ESTADO_TARJETA { get; set; }
        public virtual ESTADO_TARJETA ESTADO_TARJETA1 { get; set; }
    }
}
