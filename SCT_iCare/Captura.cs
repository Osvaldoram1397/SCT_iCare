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
    
    public partial class Captura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Captura()
        {
            this.IncidenciaDictamen = new HashSet<IncidenciaDictamen>();
        }
    
        public int idCaptura { get; set; }
        public string NombrePaciente { get; set; }
        public string NoExpediente { get; set; }
        public string TipoTramite { get; set; }
        public string EstatusCaptura { get; set; }
        public Nullable<System.DateTime> InicioCaptura { get; set; }
        public Nullable<System.DateTime> FinalCaptura { get; set; }
        public string Doctor { get; set; }
        public string Sucursal { get; set; }
        public string Capturista { get; set; }
        public Nullable<int> idPaciente { get; set; }
    
        public virtual Paciente Paciente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IncidenciaDictamen> IncidenciaDictamen { get; set; }
    }
}
