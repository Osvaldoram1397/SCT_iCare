//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCT_iCare
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrdenConekta
    {
        public int idOrden { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Producto { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string Descripcion { get; set; }
        public string Concepto { get; set; }
        public Nullable<System.DateTime> FechaCita { get; set; }
        public string MetodoPago { get; set; }
        public string idCargo { get; set; }
        public string Estatus { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public Nullable<int> Total { get; set; }
        public string idOrdenConekta { get; set; }
        public string ReferenciaOxxo { get; set; }
        public string LinkPago { get; set; }
        public string CheckoutRequestName { get; set; }
        public Nullable<int> idConsultorio { get; set; }
        public string Asignacion { get; set; }
    }
}
