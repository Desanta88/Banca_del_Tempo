﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca_del_Tempo
{
    public class Zona
    {
        public string Nome { get; set; }
        public int Longitudine { get; set; }
        public int Latitudine { get; set; }

        public Zona()
        {
            Longitudine = 0;
            Latitudine = 0;
            Nome = "";
        }

        public Zona(string n, int lon, int lat)
        {
            Nome = n;
            Longitudine = lon;
            Latitudine = lat;
        }
    }
}
