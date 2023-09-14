using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca_del_Tempo
{
    public class Zona
    {
        public string Nome { get; set; }
        public int Longitudine { get; set; }
        public int Latitudine { get; set; }

        public List<Socio> Abitanti { get; set; }

        public Zona()
        {
            Longitudine = 0;
            Latitudine = 0;
            Nome = "";
            Abitanti = new List<Socio>();
        }

        public Zona(string n, int lon, int lat ,List<Socio> a)
        {
            Nome = n;
            Longitudine = lon;
            Latitudine = lat;
            Abitanti = a;
        }
    }
}
