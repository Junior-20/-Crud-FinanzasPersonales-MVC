//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinanzasPersonales_G_
{
    using System;
    using System.Collections.Generic;
    
    public partial class TRANSACION
    {
        public int ID { get; set; }
        public string Tipo_Transacion { get; set; }
        public int Usuario { get; set; }
        public string Evento { get; set; }
        public int Tipo_Pago { get; set; }
        public System.DateTime Fecha_Transacion { get; set; }
        public System.DateTime Fecha_Registro { get; set; }
        public decimal Monto_Transacion { get; set; }
        public int NO_Tarjeta_CR { get; set; }
        public string Comentario { get; set; }
        public bool Estado { get; set; }
    
        public virtual PAGO_TIPO PAGO_TIPO { get; set; }
        public virtual USUARIO USUARIO1 { get; set; }
    }
}