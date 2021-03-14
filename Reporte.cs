using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosCondominio
{
    class Reporte
    {
        string nombre;
        string apellido;
        string numCasa;
        float cuota;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string NumCasa { get => numCasa; set => numCasa = value; }
        public float Cuota { get => cuota; set => cuota = value; }
        
    }
}
