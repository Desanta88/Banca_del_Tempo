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

        public int TempoTotale { get; set; } //indica il tempo totale indipendentemente se lo usi
                                             //per comprare altri servizi o no
        public int TempoComprato { get; set; } //indica il tempo che compri per altri servizi

        public int TempoValuta { get; set; } //valuta che si usa per "comprare" altri servizi
        public  Prestazione p { get; set; }

        public Socio()
        {
 
        }
        public Socio(string n, string c, int t, bool s)
        {
            Nome = n;
            Cognome = c;
            Telefono = t;
            Segreteria = s;
            TempoTotale = 0;
            TempoComprato = 0;
            TempoValuta = 0;
        }
        public override string ToString()
        {
            return $"{Id} {Nome} {Cognome} {Telefono.ToString()} {Segreteria} {TempoTotale} {TempoComprato} {TempoValuta} ";
        }
            
    }
}
