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
    
    public partial class Consultorios
    {
        public int id { get; set; }
        public string NombreDoctor { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string CP { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Consultorio { get; set; }
        public Nullable<int> NoConsultorio { get; set; }
        public Nullable<int> idUsuario { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}
