using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KekTura
{
    class Program
    {
        class Tura
        {
            private int tengerszint;
            private List<Szakasz> szakaszPontok;

            public Tura()
            {
                tengerszint = 0;
                szakaszPontok = new List<Szakasz>();
            }

            public Tura(int szint)
            {
                tengerszint = szint;
                szakaszPontok = new List<Szakasz>();
            }

            public Tura(List<Szakasz> pontok)
            {
                tengerszint = 0;
                szakaszPontok = pontok;
            }

            public Tura(int szint, List<Szakasz> pontok)
            {
                tengerszint = szint;
                szakaszPontok = pontok;
            }

            public int TengerszintFelettiMagassag
            {
                get
                {
                    return tengerszint;
                }

                set
                {
                    tengerszint = value;
                }
            }

            public List<Szakasz> Szakaszok
            {
                get
                {
                    return szakaszPontok;
                }

                set
                {
                    szakaszPontok = value;
                }
            }
        }

        class Szakasz
        {
            private string kiindulo;
            private string veg;
            private float hossz;
            private int emelkedes;
            private int lejtes;
            private bool pecsetelo;

            public string KiinduloPont
            {
                get
                {
                    return kiindulo;
                }

                set
                {
                    kiindulo = value;
                }
            }

            public string VegPont
            {
                get
                {
                    return veg;
                }

                set
                {
                    veg = value;
                }
            }

            public float SzakaszHossz
            {
                get
                {
                    return hossz;
                }

                set
                {
                    hossz = value;
                }
            }

            public int Emelkedes
            {
                get
                {
                    return emelkedes;
                }

                set
                {
                    emelkedes = value;
                }
            }

            public int Lejtes
            {
                get
                {
                    return lejtes;
                }

                set
                {
                    lejtes = value;
                }
            }

            public bool PecseteloPont
            {
                get
                {
                    return pecsetelo;
                }

                set
                {
                    pecsetelo = value;
                }
            }
        }

        static void Main(string[] args)
        {
            // beolvasás eleje
            var tartalom = File.ReadAllLines("kektura.csv");

            int maxhossz = 0;

            if (tartalom.Length <= 100)
            {
                maxhossz = tartalom.Length;
            }
            else
            {
                maxhossz = 100;
            }

            Tura t = new Tura();
            List<Szakasz> pontok = new List<Szakasz>();

            for (int i = 0; i < maxhossz; i++)
            {
                if (i == 0)
                {
                    t.TengerszintFelettiMagassag = int.Parse(tartalom[i]);
                }
                else
                {
                    var adatok = tartalom[i].Split(';');
                    var szp = new Szakasz();
                    szp.KiinduloPont = adatok[0];
                    szp.VegPont = adatok[1];
                    szp.SzakaszHossz = float.Parse(adatok[2]);
                    szp.Emelkedes = int.Parse(adatok[3]);
                    szp.Lejtes = int.Parse(adatok[4]);

                    if (adatok[5].Equals("i"))
                    {
                        szp.PecseteloPont = true;
                    }
                    else
                    {
                        szp.PecseteloPont = false;
                    }

                    pontok.Add(szp);
                }
            }

            t.Szakaszok = pontok;
            // beolvasás vége

            // 3. feladat
            Console.WriteLine("3. feladat: Szakaszok száma: " + t.Szakaszok.Count + " db");

            // 4. feladat

            var ossztav = 0.0f;

            foreach (var szakasz in t.Szakaszok)
            {
                ossztav += szakasz.SzakaszHossz;
            }

            Console.WriteLine("4. feladat: A túra teljes hossza: " + ossztav + " km");

            // 5. feladat

            Szakasz legrovidebb = new Szakasz();

            foreach (var szakasz in t.Szakaszok)
            {
                if (legrovidebb.SzakaszHossz == 0 || szakasz.SzakaszHossz < legrovidebb.SzakaszHossz)
                {
                    legrovidebb = szakasz;
                }
            }

            Console.WriteLine("5. feladat: A legrövidebb szakasz adatai:");
            Console.WriteLine("\tKezdete: " + legrovidebb.KiinduloPont);
            Console.WriteLine("\tVége: " + legrovidebb.VegPont);
            Console.WriteLine("\tTávolság: " + legrovidebb.SzakaszHossz + " km");

            // 7. feladat

            var hianyos = new List<string>();

            foreach (var szakasz in t.Szakaszok)
            {
                if (HianyosNev(szakasz))
                {
                    hianyos.Add(szakasz.VegPont);
                }
            }

            if (hianyos.Count != 0)
            {
                Console.WriteLine("7. feladat: Hiányos állomásnevek:");
                foreach (var am in hianyos)
                {
                    Console.WriteLine("\t" + am);
                }
            }
            else
            {
                Console.WriteLine("7. feladat: Hiányos állomásnevek:\n\tNincs hiányos állomásnév!");
            }

            // 8. feladat

            var valosmagassagok = new int[t.Szakaszok.Count];

            for (int i = 0; i < t.Szakaszok.Count; i++)
            {
                var aktualis = 0;

                if (i == 0)
                {
                    aktualis = t.TengerszintFelettiMagassag + t.Szakaszok[i].Emelkedes - t.Szakaszok[i].Lejtes;
                }
                else
                {
                    aktualis = valosmagassagok[i - 1] + t.Szakaszok[i].Emelkedes - t.Szakaszok[i].Lejtes;
                }

                valosmagassagok[i] = aktualis;
            }

            int legmagasabb = t.TengerszintFelettiMagassag;
            int index = 0;

            for (int i = 0; i < valosmagassagok.Length; i++)
            {
                if (valosmagassagok[i] > legmagasabb)
                {
                    legmagasabb = valosmagassagok[i];
                    index = i;
                }
            }

            Console.WriteLine("8. feladat: A túra legmagasabban fekvő végpontja:");
            Console.WriteLine("\tA végpont neve: " + t.Szakaszok[index].VegPont);
            Console.WriteLine("\tA végpont tengerszint feletti magassága: " + legmagasabb + " m");

            // 9. feladat

            for (int i = 0; i < tartalom.Length; i++)
            {
                if (tartalom[i].Contains(";i") && !tartalom[i].Contains("pecsetelohely"))
                {
                    var adatok = tartalom[i].Split(';');
                    adatok[1] = adatok[1] + " pecsetelohely";
                    tartalom[i] = adatok[0] + ";" + adatok[1] + ";" + adatok[2] + ";" + adatok[3] + ";" + adatok[4] + ";" + adatok[5];
                }
            }

            File.WriteAllLines(".\\kektura2.csv", tartalom);

            Console.ReadKey();
        }

        // 6. feladat
        static bool HianyosNev(Szakasz szakasz)
        {
            if (!szakasz.VegPont.Contains("pecsetelohely") && szakasz.PecseteloPont)
            {
                return true;
            }

            return false;
        }
    }
}
