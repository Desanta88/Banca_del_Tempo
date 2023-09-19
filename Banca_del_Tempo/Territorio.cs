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
        public int zonaTrovata(List<Zona> z, Zona zz)//verifica se la zona esiste g
        {
            for (int i = 0; i < z.Count; i++)
                if (zz.Nome == z[i].Nome)
                    return i;

            return -1;
        }
       public void EliminaAbitante(int id, List<Zona> z)
        {
            for (int i = 0; i < z.Count; i++)
            {
                for(int x = 0; x < z[i].Abitanti.Count; x++)
                {
                    if (z[i].Abitanti[x].Id == id)
                        z[i].Abitanti.RemoveAt(x);
                }
            }
        }
    }
}
