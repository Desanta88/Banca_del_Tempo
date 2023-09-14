using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca_del_Tempo
{
    public class Territorio
    {
        public string Nome { get; set; }

        public string TipoTerritorio { get; set; }//il tipo di territorio possibile
        public List<Zona> Zone { get; set; }//le zone del territorio

        public Territorio()
        {
            Nome = "";
            TipoTerritorio = "";
            Zone = new List<Zona>();
        }

        public Territorio(string nome, string tipoTerritorio, List<Zona> zone)
        {
            Nome = nome;
            TipoTerritorio = tipoTerritorio;
            Zone = zone;
        }
    }
}
