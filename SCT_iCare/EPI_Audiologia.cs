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
    
    public partial class EPI_Audiologia
    {
        public int idAudiologia { get; set; }
        public string Patologia { get; set; }
        public string Grafica { get; set; }
        public string NotaMedica { get; set; }
        public Nullable<int> idPaciente { get; set; }
    
        public virtual Paciente Paciente { get; set; }
    }
}