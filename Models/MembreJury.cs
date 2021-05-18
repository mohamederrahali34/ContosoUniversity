using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public enum Role
    {
        President,Rapporteur,Normal
    }
    public class MembreJury : Enseignant
    {
        public Role Role { get; set; }
    }
}
