using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca_del_Tempo
{
    internal class BancaDelTempo
    {
        public string Nome { get; set; }
        public List<Socio> Soci { get; set; }
        public Territorio Territorio { get; set; }

        public BancaDelTempo(string n, List<Socio> s, Territorio t)
        {
            Nome = n;
            Soci = s;
            Territorio = t;
        }

        public BancaDelTempo()
        {
            Soci = new List<Socio>();
            Territorio = new Territorio();
            Nome = "";
        }
    }
}
