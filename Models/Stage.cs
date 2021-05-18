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
        public Boolean EnBinome { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string Description { get; set; }
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Organisme d'acceuil")]
        public string OrganismeAceuil { get; set; }
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Country")]
        public string Pays { get; set; }
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "City")]
        public string Ville { get; set; }
       
        public Boolean SignatureValidation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public  DateTime  DateDebut { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime  DateFin { get; set; }
       

        IEnumerable<Student> Stagiaires { get; set; }

       
        Enseignant Encadrant { get; set; }
    }
}
