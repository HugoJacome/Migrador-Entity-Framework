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
    
    public partial class LOG_ACTUALIZACIONES
    {
        public int CLI_CODIGO { get; set; }
        public string CLI_IDENTIFICACION { get; set; }
        public string CLI_NOMBRE { get; set; }
        public string CTA_NUMERO { get; set; }
        public string TIP_CTA_CODIGO { get; set; }
        public string TAR_CTA_TIPO { get; set; }
        public string TAR_CTA_ESTADO { get; set; }
        public long TAR_ID { get; set; }
        public string EST_TAR_CODIGO { get; set; }
        public string TIP_TAR_CODIGO { get; set; }
        public int TAR_CUPO_DIARIO_ATM { get; set; }
        public int TAR_CUPO_DIARIO_POS { get; set; }
        public string AGE_DESCRIPCION { get; set; }
        public System.DateTime TAR_FECHA_EXPIRACION { get; set; }
        public string TRA_CODIGO { get; set; }
        public System.DateTime FECHA_LOG { get; set; }
        public Nullable<int> USU_CODIGO { get; set; }
        public string TER_CODIGO { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual TARJETA TARJETA { get; set; }
        public virtual TRANSACCION TRANSACCION { get; set; }
    }
}
