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
        public int ErogatoreId { get; set; }
        public int RiceventeId { get; set; }
        public int Ore { get; set; }
        public DateTime Data { get; set; }

        public Prestazione()
        {

        }
        public Prestazione(string n,int e, int r, DateTime d)
        {
            Nome = n; 
            ErogatoreId = e;
            RiceventeId = r;
            Ore = 0;
            Data = d;
        }
        public override string ToString()
        {
            return $"{Nome} id erogatore:{ErogatoreId} id ricevente:{RiceventeId} {Ore} {Data.Date} ";
        }
    }
}
