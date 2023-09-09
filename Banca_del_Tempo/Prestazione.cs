using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca_del_Tempo
{
    public class Prestazione
    {
        public string Nome { get; set; }
        public Socio Erogatore { get; set; }
        public Socio Ricevente { get; set; }
        public int Ore { get; set; }
        public DateTime Data { get; set; }

        public Prestazione()
        {

        }
        public Prestazione(string n, Socio e, Socio r, DateTime d)
        {
            Nome = n; 
            Erogatore = e;
            Ricevente = r;
            Ore = 0;
            Data = d;
        }
        public override string ToString()
        {
            return $"{Nome} {Erogatore.Nome + " " + Erogatore.Cognome} {Ricevente.Nome + " " + Ricevente.Cognome} {Ore} {Data.Date} ";
        }
    }
}
