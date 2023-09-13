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

        public string TipoTerritorio { get; set; }//dal main poi darò l'opzione di scegliere tra i 3 tipi di territori possibili
        public List<Zona> Zone { get; set; }

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
