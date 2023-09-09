using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca_del_Tempo
{
    internal class Program
    {

        static int VerificaId(int id, List<Socio> b)
        {
            Random r = new Random();
            id = r.Next(10000000, 99999999);
            for ( int i = 0; i < b.Count; i++ )
            {
                if ( id == b[i].Id )
                    id = r.Next(10000000, 99999999);
            }
            return id;
        }
        static void SetIdAutomatico(Socio s, List<Socio> b)
        {
            int id = 0;
            id = VerificaId(id, b);
            s.Id = id;
        }
        static void Aggiungi(List<Socio> l, Socio s, Prestazione p)
        {
            p.Ricevente = null;
            p.Erogatore = s;
            s.SetPrestazione(p);
            SetIdAutomatico(s, l);
            l.Add(s);
        }
        static void EliminaSocio(int id, List<Socio> l)
        {
            for ( int i = 0; i < l.Count; i++ )
            {
                if ( l[i].Id == id )
                    l.RemoveAt(i);
            }
        }
        static bool SegreteriaAnswer(string r)
        {
            bool a;
            if ( r == "y" || r == "Y" )
                r = "true";
            else
                r = "false";
            a = bool.Parse(r);
            return a;
        }
        static Socio TrovaSocio(int id, List<Socio> l)
        {
            Socio temp = null;
            for ( int i = 0; i < l.Count; i++ )
            {
                if ( l[i].Id == id )
                    temp = l[i];
            }
            return temp;
        }
        static void Scambio(Socio e,Socio r,int h)
        {
            Prestazione PresErogatore;
            PresErogatore = e.GetPrestazione();
            r.TempoValuta -= h;
            e.TempoTotale += h;
            PresErogatore.Ore += h;
            PresErogatore.Data = DateTime.Now;
            r.TempoComprato += h;
        }
        static void Main(string[] args)
        {
            List<Socio> BdT = new List<Socio>();
            Socio soc;
            Prestazione pre;
            int c = 1;
            do
            {
                Console.WriteLine("1-Aggiungi Socio");
                Console.WriteLine("2-Rimuovi Socio");
                Console.WriteLine("3-Visualizza tutti i Soci");
                Console.WriteLine("4-Effetua scambio di prestazioni");
                Console.WriteLine("5-Visualizza soci che fanno parte della segreteria");
                Console.WriteLine("6-Visualizza soci con debito");
                Console.WriteLine("7-Visualizza in ordine decrescente i soci in base alle ore erogate");
                Console.WriteLine("8-pulisci console");
                c = int.Parse(Console.ReadLine());
                Console.WriteLine();
                switch (c)
                {
                    case 1:
                        Console.WriteLine("Inserisci il nome del socio");
                        soc = new Socio();
                        pre = new Prestazione();
                        soc.Nome = Console.ReadLine().ToString();
                        Console.WriteLine("Inserisci il cognome del socio");
                        soc.Cognome = Console.ReadLine().ToString();
                        Console.WriteLine("Inserisci il numero di telefono del socio");
                        soc.Telefono = Console.ReadLine().ToString();
                        Console.WriteLine("il nuovo socio farà parte della segreteria? (y/n)");
                        soc.Segreteria = SegreteriaAnswer(Console.ReadLine().ToString());
                        Console.WriteLine("Dati prestazione:");
                        Console.WriteLine("inserisci il nome della prestazione");
                        pre.Nome= Console.ReadLine().ToString();
                        Aggiungi(BdT, soc, pre);
                        Console.WriteLine();
                        break;

                    case 2:
                        int ide = 0;
                        Console.WriteLine("Inserisci l'id del socio da eliminare");
                        ide = int.Parse(Console.ReadLine());
                        if ( ide.ToString().Length > 8 )
                            Console.WriteLine("L'ID è di 8 cifre!!!");
                        else
                            EliminaSocio(ide, BdT);
                            Console.WriteLine("Eliminazione effettuata");
                            Console.WriteLine();
                        break;

                    case 3:
                        for( int i = 0; i < BdT.Count; i++ )
                            Console.WriteLine( BdT[i].ToString() );
                        Console.WriteLine();
                        break;

                    case 4:
                        int ide1 = 0, ide2 = 0,ore=0;
                        Socio ero, ric;

                        Console.Write("inserire l'id dell'erogatore:");
                        ide1 = int.Parse( Console.ReadLine() );
                        ero = TrovaSocio(ide1, BdT);
                        if ( ero == null )
                        {
                            Console.WriteLine("Id dell'erogatore errato");
                            Console.WriteLine();
                            break;
                        }
                        Console.WriteLine("per quante ore?");
                        ore=int.Parse( Console.ReadLine() );
                        Console.Write("inserire l'id del ricevente:");
                        ide2=int.Parse( Console.ReadLine() );
                        ric = TrovaSocio(ide2, BdT);
                        if ( ric == null )
                        {
                            Console.WriteLine("Id del ricevente errato");
                            Console.WriteLine();
                            break;
                        }
                        Scambio(ero, ric,ore);
                        Console.WriteLine("Scambio effettuato");
                        Console.WriteLine();
                        break;

                    case 5:
                        for (int i = 0; i < BdT.Count; i++)
                        {
                            if ( BdT[i].Segreteria == true )
                                Console.WriteLine( BdT[i].ToString() );
                        }
                        Console.WriteLine();
                        break;

                    case 6:
                        for (int i = 0; i < BdT.Count; i++)
                        {
                            if (BdT[i].TempoComprato > BdT[i].TempoTotale)
                                Console.WriteLine(BdT[i].ToString());
                        }
                        Console.WriteLine();
                        break;

                    case 7:
                        List<Socio> temp = BdT.OrderByDescending(x => x.TempoTotale).ToList();
                        for (int i = 0; i < temp.Count; i++)
                        {
                            Console.WriteLine($"nome prestazione:{temp[i].GetPrestazione().Nome} ore totali erogate:{temp[i].TempoTotale}"); 
                        }
                        break;

                    case 8:
                        Console.Clear();
                        break;

                }
            } while ( c >= 1 && c <= 8 );
        }
    }
}
