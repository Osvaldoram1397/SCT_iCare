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
    
    public partial class EPI_AparatosSistemas
    {
        public int idAparatosSistemas { get; set; }
        public string Sintoma { get; set; }
        public string AlteracionVista { get; set; }
        public string Lentes { get; set; }
        public string Audicion { get; set; }
        public string Auditivo { get; set; }
        public string Nariz { get; set; }
        public string Gusto { get; set; }
        public string Cardiaca { get; set; }
        public string Respiratoria { get; set; }
        public string Endocrinologica { get; set; }
        public string Urinaria { get; set; }
        public string Venas { get; set; }
        public string Intestinal { get; set; }
        public string CancerSangre { get; set; }
        public string Articulaciones { get; set; }
        public string Suenio { get; set; }
        public string Apnea { get; set; }
        public string Neurologica { get; set; }
        public string CabezaCuello { get; set; }
        public string Abdomen { get; set; }
        public string Extremidades { get; set; }
        public Nullable<int> idPaciente { get; set; }
    
        public virtual Paciente Paciente { get; set; }
    }
}
