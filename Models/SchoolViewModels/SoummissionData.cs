using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models.SchoolViewModels
{
    public class SoummissionData
    {

        public Stage Stage { get; set; }
        public Student Stagiaire { get; set; }
        public Enseignant Encadrant { get; set; }


        public SoummissionData(Stage stage ,Enseignant  encadrant)
        {
           Stage = stage;
            Encadrant = encadrant;
        }
    }
}
