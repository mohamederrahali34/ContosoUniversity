using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Enseignant 
    {
        public int EnseignantID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        [Display(Name = "Encadrant")]
        public string FullName { get
            {
                return Nom + " " + Prenom;
            } }
        public Department Department { get; set; }
       
        IEnumerable<Stage> StagesEncadres { get; set; }

    }
}
