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
        public string Telefono { get; set; }
        public bool Segreteria { get; set; }

        public int Tempo { get; set; }
        private Prestazione p;

        public Socio()
        {

        }
        public Socio(string n, string c, string t, bool s)
        {
            Nome = n;
            Cognome = c;
            Telefono = t;
            Segreteria = s;
            Tempo = 0;
        }
        public string GetPrestazioneString()
        {
            return p.ToString();
        }
        public Prestazione GetPrestazione()
        {
            return this.p;
        }
        public void SetPrestazione(Prestazione pr)
        {
            p = pr;
        }
        public override string ToString()
        {
            return $"{Id} {Nome} {Cognome} {Telefono} {Segreteria} {Tempo} ";
        }
            
    }
}
