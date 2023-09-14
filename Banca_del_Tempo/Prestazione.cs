using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca_del_Tempo
{
    public class Prestazione :IComparable<Prestazione>
    {
        public string Nome { get; set; }
        public int ErogatoreId { get; set; }//l'id dell'erogatore della prestazione
        public int RiceventeId { get; set; }// l'id del ricevente della prestazione
        public int OreTotali { get; set; } //ore totali che il socio ha impiegato per la prestazione

        public int OreImpiegate { get; set; }//le ore impiegate per fornire una prestazione alla volta
        public DateTime Data { get; set; }//data in cui è avvenuta lo scambio

        public Prestazione()
        {

        }
        public Prestazione(string n,int e, int r, DateTime d)
        {
            Nome = n; 
            ErogatoreId = e;
            RiceventeId = r;
            OreTotali = 0;
            OreImpiegate = 0;
            Data = d;
        }
        public override string ToString()
        {
            return $"{Nome} id erogatore:{ErogatoreId} id ricevente:{RiceventeId} {OreTotali} {Data.Date} ";
        }

        public int CompareTo(Prestazione x)
        {
            return Data.CompareTo(x.Data);
        }
    }
}
