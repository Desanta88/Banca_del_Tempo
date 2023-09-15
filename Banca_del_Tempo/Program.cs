using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Banca_del_Tempo
{
    internal class Program
    {

        static int VerificaId(int id, List<Socio> b)//verifica che l'id sia diverso dagli altri
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
        static void SetIdAutomatico(Socio s, List<Socio> b)//imposta un id automaticamente
        {
            int id = 0;
            id = VerificaId(id, b);
            s.Id = id;
        }
        static void Aggiungi(List<Socio> l, Socio s, Prestazione p)//aggiunge un socio alla banca
        {
            p.RiceventeId=0;
            s.Prestazione = p;
            SetIdAutomatico(s, l);
            p.ErogatoreId = s.Id;
            l.Add(s); 
        }
        static void EliminaSocio(int id, List<Socio> l)//elimina un socio dalla banca
        {    
            for ( int i = 0; i < l.Count; i++ )
            {
                if ( l[i].Id == id )
                    l.RemoveAt(i);
            }
        }
        static bool SegreteriaAnswer(string r)//funzione per impostare la risposta riguardo se il socio
        {                                     //faccia parte della segreteria o no
            bool a;
            if ( r == "y" || r == "Y" )
                r = "true";
            else
                r = "false";
            a = bool.Parse(r);
            return a;
        }
        static Socio TrovaSocio(int id, List<Socio> l)//trova il socio nella banca
        {
            Socio temp = null;
            for ( int i = 0; i < l.Count; i++ )
            {
                if ( l[i].Id == id )
                    temp = l[i];
            }
            return temp;
        }
        static void Scambio(Socio e,Socio r,int h)//effettua lo scambio di tempo tra due soci
        {
            e.TempoTotale += h;
            e.Prestazione.OreTotali += h;
            e.Prestazione.Data = DateTime.Now;
            r.TempoTotale -= h;
            e.Prestazione.OreImpiegate = h;
            e.Prestazione.RiceventeId = r.Id;
        }

        static void Salva(BancaDelTempo b,string f)//salva le informazioni della banca su un file
        {
            FileStream fstr = File.Open(f, FileMode.Open);
            if (fstr.Length > 0)
                fstr.SetLength(0);
            string dati = Newtonsoft.Json.JsonConvert.SerializeObject(b);
            fstr.Close();
            System.IO.File.AppendAllText(f, dati);

        }

        static void SalvaLista(List<Prestazione>s,string f)//salva la cronologia degli scambi su un file
        {
            FileStream fs = File.Open(f, FileMode.Open);
            fs.SetLength(0);
            fs.Close();
            System.IO.File.AppendAllText(f, "[");
            for (int i = 0; i < s.Count; i++)
            {
                string dati = Newtonsoft.Json.JsonConvert.SerializeObject(s[i]);
                System.IO.File.AppendAllText(f,dati);
                if(i<s.Count-1)
                    System.IO.File.AppendAllText(f, ",");
            }
            System.IO.File.AppendAllText(f, "]");
        }

        static int zonaTrovata(List<Zona> z,Zona zz)//verifica se la zona esiste già
        {
            for (int i = 0; i < z.Count; i++)
                if (zz.Nome == z[i].Nome)
                    return i;

            return -1;
        }
        static void Main(string[] args)
        {
            BancaDelTempo BdT = new BancaDelTempo();
            Socio soc;
            Prestazione pre;
            int c = 1,ct=0,zonaTro=-1;
            string filename = @"./Soci.json",nomeTerri="",nomeZona="";
            string filename2 = @"./Scambi.Json";
            List<Prestazione> scambi = new List<Prestazione>();

            //carica tutti i dati dai due file
            try
            {
                StreamReader srSoci = new StreamReader(filename);
                string jsonDataSoci = srSoci.ReadToEnd();
                srSoci.Close();
                FileStream fstreamSoci = File.Open(filename, FileMode.Open);
                if (System.IO.File.Exists(filename) == true && System.IO.File.Exists(filename2) == true)
                {
                    if (fstreamSoci.Length != 0 )
                    {
                        BdT = Newtonsoft.Json.JsonConvert.DeserializeObject<BancaDelTempo>(jsonDataSoci);
                    }
                    fstreamSoci.Close();
                }
            }catch(FileNotFoundException e)
            {
                var fst=File.Create(e.FileName);
                fst.Close();
             
            }

            try
            {
                StreamReader srScambi = new StreamReader(filename2);
                string jsonDataScambi = srScambi.ReadToEnd();
                srScambi.Close();
                FileStream fstreamScambi = File.Open(filename2, FileMode.Open);
                if (System.IO.File.Exists(filename2) == true && System.IO.File.Exists(filename2) == true)
                {
                    if (fstreamScambi.Length != 0)
                    {
                        scambi = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Prestazione>>(jsonDataScambi);
                    }
                    fstreamScambi.Close();
                }
            }
            catch(FileNotFoundException e)
            {
                var fst = File.Create(e.FileName);
                fst.Close();
            }
            //se non si hanno inserito le informazioni della banca in precedenza,verrà eseguito questo codice all'inizio dell'esecuzione
            FileStream ff = File.Open(filename, FileMode.Open);
            if (ff.Length == 0)
            {
                Console.WriteLine("Impostare le informazioni della banca del tempo");
                Console.Write("inserisci il nome della banca del tempo:");
                BdT.Nome = Console.ReadLine();
                Console.WriteLine("Scegli il tipo di territorio:");
                Console.WriteLine("1-Comune");
                Console.WriteLine("2-Quartiere");
                Console.WriteLine("3-Città");
                ct = int.Parse(Console.ReadLine());

                switch (ct)
                {
                    case 1:
                        BdT.Territorio.TipoTerritorio = "Comune";
                        Console.Write("indica il nome del comune:");
                        nomeTerri = Console.ReadLine().ToString();
                        BdT.Territorio.Nome = nomeTerri;
                        break;

                    case 2:
                        BdT.Territorio.TipoTerritorio = "Quartiere";
                        Console.Write("indica il nome del quartiere:");
                        nomeTerri = Console.ReadLine().ToString();
                        BdT.Territorio.Nome = nomeTerri;
                        break;

                    case 3:
                        BdT.Territorio.TipoTerritorio = "Città";
                        Console.Write("indica il nome della città:");
                        nomeTerri = Console.ReadLine().ToString();
                        BdT.Territorio.Nome = nomeTerri;
                        break;

                    default:
                        break;
                }
            }
            ff.Close();
            Salva(BdT, filename);

            //menù a scelta della banca
            do
            {
                Console.WriteLine("1-Aggiungi Socio");
                Console.WriteLine("2-Rimuovi Socio");
                Console.WriteLine("3-Visualizza tutti i Soci");
                Console.WriteLine("4-Effetua scambio di prestazioni");
                Console.WriteLine("5-Visualizza soci che fanno parte della segreteria");
                Console.WriteLine("6-Visualizza soci con debito");
                Console.WriteLine("7-Visualizza in ordine decrescente i soci in base alle ore erogate");
                Console.WriteLine("8-Visualizza tutti gli scambi tra soci");
                Console.WriteLine("9-modifica le informazioni della banca del tempo");
                Console.WriteLine("10-aggiungi una zona");
                Console.WriteLine("11-visualizza soci per zona");
                Console.WriteLine("12-pulisci console");
                c = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (c)
                {
                    case 1:
                        int numeroTel = 0;
                        Zona zone = new Zona();
                        Console.WriteLine("Inserisci il nome del socio");
                        soc = new Socio();
                        pre = new Prestazione();
                        soc.Nome = Console.ReadLine().ToString();
                        Console.WriteLine("Inserisci il cognome del socio");
                        soc.Cognome = Console.ReadLine().ToString();
                        Console.WriteLine("Inserisci il numero di telefono del socio");
                        numeroTel= int.Parse(Console.ReadLine());
                        if (numeroTel.ToString().Length == 10)
                            soc.Telefono = numeroTel;
                        else
                        {
                            Console.WriteLine("il numero di telefono è di 10 cifre!!!");
                            break;
                        }
                        Console.WriteLine("il nuovo socio farà parte della segreteria? (y/n)");
                        soc.Segreteria = SegreteriaAnswer(Console.ReadLine().ToString());
                        Console.WriteLine("Dati prestazione:");
                        Console.WriteLine("inserisci il nome della prestazione");
                        pre.Nome= Console.ReadLine().ToString();
                        Console.WriteLine("inserisci il nome della zona");
                        nomeZona = Console.ReadLine();
                        zone.Nome = nomeZona;
                        zonaTro = zonaTrovata(BdT.Territorio.Zone, zone);
                        if (zonaTrovata(BdT.Territorio.Zone,zone) !=-1)
                        {
                            Aggiungi(BdT.Soci, soc, pre);
                            BdT.Territorio.Zone[zonaTro].Abitanti.Add(soc);
                            Salva(BdT, filename);
                        }
                        else
                           Console.WriteLine("Non hai aggiunto zone");
                        
                        Console.WriteLine();
                        break;

                    case 2:
                        int ide = 0;
                        Console.WriteLine("Inserisci l'id del socio da eliminare");
                        ide = int.Parse(Console.ReadLine());
                        if ( ide.ToString().Length == 8)
                        {
                            EliminaSocio(ide, BdT.Soci);
                            Salva(BdT, filename);
                            Console.WriteLine("Eliminazione effettuata");
                            Console.WriteLine();
                        }
                        else
                            Console.WriteLine("L'ID è di 8 cifre!!!");
                        break;

                    case 3:
                        for (int i = 0; i < BdT.Soci.Count; i++)
                        {
                            Console.WriteLine("Nome:" + BdT.Soci[i].Nome);
                            Console.WriteLine("Cognome:" + BdT.Soci[i].Cognome);
                            Console.WriteLine("Numero di telefono:" + BdT.Soci[i].Telefono);
                            Console.WriteLine("Id:" + BdT.Soci[i].Id);
                            Console.WriteLine("Ore totali:" + BdT.Soci[i].TempoTotale);
                            Console.WriteLine("Ore impiegate nella prestazione:" + BdT.Soci[i].Prestazione.OreTotali);
                            if (BdT.Soci[i].Segreteria == true)
                                Console.WriteLine("Membro della segreteria:sì");
                            else
                                Console.WriteLine("Membro della segreteria:no");

                            Console.WriteLine();
                        }
                        break;

                    case 4:
                        int ide1 = 0, ide2 = 0,ore=0;
                        Socio ero, ric;

                        Console.Write("inserire l'id dell'erogatore:");
                        ide1 = int.Parse( Console.ReadLine() );
                        ero = TrovaSocio(ide1, BdT.Soci);
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
                        ric = TrovaSocio(ide2, BdT.Soci);
                        if ( ric == null )
                        {
                            Console.WriteLine("Id del ricevente errato");
                            Console.WriteLine();
                            break;
                        }
                        Scambio(ero, ric,ore);
                        scambi.Add(ero.Prestazione);
                        Salva(BdT, filename);
                        SalvaLista(scambi, filename2);
                        Console.WriteLine("Scambio effettuato");
                        Console.WriteLine();
                        break;

                    case 5:
                        for (int i = 0; i < BdT.Soci.Count; i++)
                        {
                            if ( BdT.Soci[i].Segreteria == true)
                            {
                                Console.WriteLine("Nome:" + BdT.Soci[i].Nome);
                                Console.WriteLine("Cognome:" + BdT.Soci[i].Cognome);
                                Console.WriteLine("Numero di telefono:" + BdT.Soci[i].Telefono);
                                Console.WriteLine("Id:" + BdT.Soci[i].Id);
                            }
                            Console.WriteLine();
                        }
                        break;

                    case 6:
                        for (int i = 0; i < BdT.Soci.Count; i++)
                        {
                            if (BdT.Soci[i].TempoTotale<0)
                                Console.WriteLine(BdT.Soci[i].ToString());
                        }
                        Console.WriteLine();
                        break;

                    case 7:
                        List<Socio> temp = BdT.Soci.OrderByDescending(x => x.Prestazione.OreTotali).ToList();
                        for (int i = 0; i < temp.Count; i++)
                        {
                            Console.WriteLine($"nome prestazione:{temp[i].Prestazione.Nome} ore totali erogate:{temp[i].Prestazione.OreTotali}"); 
                        }
                        Console.WriteLine();
                        break;

                    case 8:
                        Socio erogatore, ricevente;
                        scambi.Sort();
                        Console.WriteLine("Data:" + scambi[0].Data);
                        for (int i=1;i< scambi.Count; i++)
                        {
                            if (scambi[i].Data != scambi[i-1].Data)
                                Console.WriteLine("Data:" + scambi[i].Data);
                            erogatore = TrovaSocio(scambi[i].ErogatoreId, BdT.Soci);
                            ricevente = TrovaSocio(scambi[i].RiceventeId, BdT.Soci);
                            Console.WriteLine($"Erogatore:{erogatore.Nome} {erogatore.Cognome} {erogatore.Id}");
                            Console.WriteLine($"Ricevente:{ricevente.Nome} {ricevente.Cognome} {ricevente.Id}");
                            Console.WriteLine("ore impiegate:" + scambi[i].OreImpiegate);
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                        break;

                    case 9:
                        Console.Write("inserisci il nome della banca del tempo:");
                        BdT.Nome = Console.ReadLine();
                        Console.WriteLine("Scegli il tipo di territorio:");
                        Console.WriteLine("1-Comune");
                        Console.WriteLine("2-Quartiere");
                        Console.WriteLine("3-Città");
                        ct = int.Parse(Console.ReadLine());
                        switch (ct)
                        {
                            case 1:
                                BdT.Territorio.TipoTerritorio = "Comune";
                                Console.Write("indica il nome del comune:");
                                nomeTerri = Console.ReadLine();
                                BdT.Territorio.Nome = nomeTerri;
                                break;

                            case 2:
                                BdT.Territorio.TipoTerritorio = "Quartiere";
                                Console.Write("indica il nome del quartiere:");
                                nomeTerri = Console.ReadLine();
                                BdT.Territorio.Nome = nomeTerri;
                                break;

                            case 3:
                                BdT.Territorio.TipoTerritorio = "Città";
                                Console.Write("indica il nome della città:");
                                nomeTerri = Console.ReadLine();
                                BdT.Territorio.Nome = nomeTerri;
                                break;

                            default:
                                break;
                        }
                        Salva(BdT, filename);
                        Console.WriteLine();
                        break;


                    case 10:
                        Zona z = new Zona();
                        Console.WriteLine("inserisci il nome della zona:");
                        z.Nome = Console.ReadLine();
                        if ((zonaTrovata(BdT.Territorio.Zone, z) ==-1  && BdT.Territorio.Zone.Count > 0) || BdT.Territorio.Zone.Count==0)
                            BdT.Territorio.Zone.Add(z);
                        else
                            Console.WriteLine("Zona già presente,creane un'altra");
                        Salva(BdT, filename);
                        Console.WriteLine();
                        break;

                    case 11:

                        for (int i = 0; i < BdT.Territorio.Zone.Count; i++)
                        {
                            Console.WriteLine(BdT.Territorio.Zone[i].Nome);
                            
                            for(int j = 0; j < BdT.Territorio.Zone[i].Abitanti.Count; j++)
                            {
                                Console.WriteLine($"{BdT.Territorio.Zone[i].Abitanti[j].Nome} {BdT.Territorio.Zone[i].Abitanti[j].Cognome} {BdT.Territorio.Zone[i].Abitanti[j].Id}");
                            }
                             
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                        break;

                    case 12:
                        Console.Clear();
                        break;

                }
            } while ( c >= 1 && c <= 12 );
        }
    }
}
