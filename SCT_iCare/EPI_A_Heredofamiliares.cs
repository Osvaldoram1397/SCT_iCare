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
    
    public partial class EPI_A_Heredofamiliares
    {
        public int idHeredofamiliares { get; set; }
        public string MadreVive { get; set; }
        public string PadreVive { get; set; }
        public string HermanosViven { get; set; }
        public string FamiliaGrave { get; set; }
        public string Diabetes { get; set; }
        public string Hipertension { get; set; }
        public string Obesidad { get; set; }
        public string Cardiopatia { get; set; }
        public string VascularCerebral { get; set; }
        public string Infarto { get; set; }
        public string Tiroides { get; set; }
        public string Neoplasticas { get; set; }
        public Nullable<int> idPaciente { get; set; }
    
        public virtual Paciente Paciente { get; set; }
    }
}
