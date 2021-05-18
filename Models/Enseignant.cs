using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Enseignant
    {
        public int EnseignantID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public Department Department { get; set; }
       
        IEnumerable<Stage> StagesEncadres { get; set; }

    }
}
