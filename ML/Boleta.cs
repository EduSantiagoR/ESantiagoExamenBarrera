using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Boleta
    {
        public int IdMateria { get; set; }
        public string Nombre { get; set; }
        public decimal Calificacion { get; set; }
        public List<object> Calificaciones { get; set; }
    }
}
