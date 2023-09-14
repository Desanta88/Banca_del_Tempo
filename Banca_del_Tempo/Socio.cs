using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca_del_Tempo
{
    public class Socio
    {
       
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public int Telefono { get; set; }
        public bool Segreteria { get; set; }

        public int TempoTotale { get; set; } //indica il tempo che hai in totale(sia dai guadagni,sia dalle perdite)
                                             
        public  Prestazione Prestazione { get; set; }

        public Zona Zona { get; set; }

        public Socio()
        {
 
        }
        public Socio(string n, string c, int t, bool s, Zona z)
        {
            Nome = n;
            Cognome = c;
            Telefono = t;
            Segreteria = s;
            TempoTotale = 0;
            Zona = z;
        }
        public override string ToString()
        {
            return $"{Id} {Nome} {Cognome} {Telefono.ToString()} {Segreteria} {TempoTotale} ";
        }

            
    }
}
