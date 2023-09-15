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
        public void Aggiungi(List<Socio> l, Socio s, Prestazione p)//aggiunge un socio alla banca
        {
            p.RiceventeId = 0;
            s.Prestazione = p;
            SetIdAutomatico(s, l);
            p.ErogatoreId = s.Id;
            l.Add(s);
        }
        private void SetIdAutomatico(Socio s, List<Socio> b)//imposta un id automaticamente
        {
            int id = 0;
            id = VerificaId(id, b);
            s.Id = id;
        }
        private int VerificaId(int id, List<Socio> b)//verifica che l'id sia diverso dagli altri
        {
            Random r = new Random();
            id = r.Next(10000000, 99999999);
            for (int i = 0; i < b.Count; i++)
            {
                if (id == b[i].Id)
                    id = r.Next(10000000, 99999999);
            }
            return id;
        }
        public void EliminaSocio(int id, List<Socio> l)//elimina un socio dalla banca
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Id == id)
                    l.RemoveAt(i);
            }
        }
        public bool SegreteriaAnswer(string r)//funzione per impostare la risposta riguardo se il socio
        {                                     //faccia parte della segreteria o no
            bool a;
            if (r == "y" || r == "Y")
                r = "true";
            else
                r = "false";
            a = bool.Parse(r);
            return a;
        }
        public Socio TrovaSocio(int id, List<Socio> l)//trova il socio nella banca
        {
            Socio temp = null;
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Id == id)
                    temp = l[i];
            }
            return temp;
        }
        public void Scambio(Socio e, Socio r, int h)//effettua lo scambio di tempo tra due soci
        {
            e.TempoTotale += h;
            e.Prestazione.OreTotali += h;
            e.Prestazione.Data = DateTime.Now;
            r.TempoTotale -= h;
            e.Prestazione.OreImpiegate = h;
            e.Prestazione.RiceventeId = r.Id;
        }
    }
}
