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
    
    public partial class Paciente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paciente()
        {
            this.Captura = new HashSet<Captura>();
            this.Cita = new HashSet<Cita>();
            this.Dictamen = new HashSet<Dictamen>();
            this.Expedientes = new HashSet<Expedientes>();
            this.DictamenProblema = new HashSet<DictamenProblema>();
            this.CarruselMedico = new HashSet<CarruselMedico>();
            this.EPI_Laboratorio = new HashSet<EPI_Laboratorio>();
            this.EPI_SignosVitales = new HashSet<EPI_SignosVitales>();
            this.EPI_Cardiologia = new HashSet<EPI_Cardiologia>();
            this.Epi_Oftalmologia = new HashSet<Epi_Oftalmologia>();
            this.EPI_A_Heredofamiliares = new HashSet<EPI_A_Heredofamiliares>();
            this.EPI_A_NoPatologicos = new HashSet<EPI_A_NoPatologicos>();
            this.EPI_A_Patologicos = new HashSet<EPI_A_Patologicos>();
            this.EPI_AparatosSistemas = new HashSet<EPI_AparatosSistemas>();
            this.EPI_Audiologia = new HashSet<EPI_Audiologia>();
            this.EPI_ExploracionFisica = new HashSet<EPI_ExploracionFisica>();
            this.EPI_DictamenAptitud = new HashSet<EPI_DictamenAptitud>();
            this.EPI_Odontologia = new HashSet<EPI_Odontologia>();
        }
    
        public int idPaciente { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Folio { get; set; }
        public string CURP { get; set; }
        public string HASH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Captura> Captura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cita> Cita { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dictamen> Dictamen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expedientes> Expedientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DictamenProblema> DictamenProblema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarruselMedico> CarruselMedico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPI_Laboratorio> EPI_Laboratorio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPI_SignosVitales> EPI_SignosVitales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPI_Cardiologia> EPI_Cardiologia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Epi_Oftalmologia> Epi_Oftalmologia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPI_A_Heredofamiliares> EPI_A_Heredofamiliares { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPI_A_NoPatologicos> EPI_A_NoPatologicos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPI_A_Patologicos> EPI_A_Patologicos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPI_AparatosSistemas> EPI_AparatosSistemas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPI_Audiologia> EPI_Audiologia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPI_ExploracionFisica> EPI_ExploracionFisica { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPI_DictamenAptitud> EPI_DictamenAptitud { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EPI_Odontologia> EPI_Odontologia { get; set; }
    }
}
