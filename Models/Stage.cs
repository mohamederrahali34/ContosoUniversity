using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Stage
    {
        public int StageID { get; set; }
       
        [StringLength(50, MinimumLength = 3)]
        public string Description { get; set; }
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Organisme d'acceuil")]
        public string OrganismeAceuil { get; set; }
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Pays")]
        public string Pays { get; set; }
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Ville")]
        public string Ville { get; set; }

        public int SignatureValidation { get; set; } 
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Début")]
        public  DateTime  DateDebut { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Fin")]
        public DateTime  DateFin { get; set; }


       
        public Student Stagiaire { get; set; }
        public Student Binome { get; set; }
        public int EnseignantID { get; set; }
       public Enseignant Encadrant { get; set; }
    }
}
