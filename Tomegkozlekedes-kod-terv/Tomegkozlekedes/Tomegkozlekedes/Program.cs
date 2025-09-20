using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Tomegkozlekedes
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Jarmu> jarmuvek = new List<Jarmu>();
            List<Szerviz> cegek = new List<Szerviz>();
            List<Munkalap> munkalapok = new List<Munkalap>();

            Beolvas("bemenet.txt", jarmuvek, cegek, munkalapok);


            Console.WriteLine(cegek.Count + " ceg");
            Console.WriteLine(jarmuvek.Count + " jarmu");
            Console.WriteLine(munkalapok.Count + " munkalap");


            //Console.WriteLine(Jarmu.LegdragabbJarmu(jarmuvek, 2025));
            //Console.WriteLine(Jarmu.JavitasAlattiArany(jarmuvek));



            foreach (var j in jarmuvek)
            {
                Console.WriteLine(j.Azonosito.ToString() + " azonosítójú jármű faktora: " + j.GetFaktor(j.HasznalatiOvezet));
            }


            Jarmu legdragabb = Jarmu.LegdragabbJarmu(jarmuvek, 2025);
            if (legdragabb != null)
            {
                Console.WriteLine("A legdrágább jármű azonosítója: " + legdragabb.Azonosito);
            }
            else
            {
                Console.WriteLine("Nincs jármű a listában.");
            }

            double arany = Jarmu.JavitasAlattiArany(jarmuvek);
            Console.WriteLine("Javítás alatt álló járművek aránya: " + arany.ToString("P2"));

            /*
            foreach(var c in cegek)
            {
                Console.WriteLine(c.nev);
            }

            foreach(var j in jarmuvek)
            {
                Console.WriteLine(j.gyartasiEv);
            }

            */

        }

        public static void Beolvas(string fileNev, List<Jarmu> jarmuvek, List<Szerviz> cegek, List<Munkalap> munkalapok)
        {
            using (var sr = new StreamReader(fileNev))
            {
                while (!sr.EndOfStream)
                {

                    String? sor = sr.ReadLine();
                    if (sor == null)
                    {
                        continue;
                    }

                    String[] bemenet = sor.Split(';');

                    switch (bemenet[0])
                    {
                        case "Jarmu":
                            if (bemenet[1] == "Villamos")
                            {
                                jarmuvek.Add(new Villamos(int.Parse(bemenet[2]), int.Parse(bemenet[3]), Ovezet.GetOvezet(bemenet[4]), int.Parse(bemenet[5])));
                            }
                            if (bemenet[1] == "Trolibusz")
                            {
                                jarmuvek.Add(new Trolibusz(int.Parse(bemenet[2]), int.Parse(bemenet[3]), Ovezet.GetOvezet(bemenet[4]), int.Parse(bemenet[5])));
                            }
                            if (bemenet[1] == "Autobusz")
                            {
                                jarmuvek.Add(new Autobusz(int.Parse(bemenet[2]), int.Parse(bemenet[3]), Ovezet.GetOvezet(bemenet[4]), int.Parse(bemenet[5])));
                            }
                            break;
                        case "Ceg":
                            cegek.Add(new Szerviz(bemenet[1]));
                            break;
                        case "Szerviz":
                            if (jarmuvek.Count != 0 && cegek.Count != 0)
                            {

                                foreach (var j in jarmuvek)
                                {

                                    if (j.Azonosito == int.Parse(bemenet[1]))
                                    {

                                        foreach (var c in cegek)
                                        {
                                            //Console.WriteLine(bemenet[2]);
                                            //Console.WriteLine(c.nev);
                                            if (c.Nev == bemenet[2])
                                            {
                                                if (string.IsNullOrWhiteSpace(bemenet[4]))
                                                {
                                                    munkalapok.Add(new Munkalap(j, c, DateTime.Parse(bemenet[3]), null, (bemenet[5] == "Javitas" ? SzervizTipus.Javitas : SzervizTipus.Idoszakos)));
                                                    j.Munkalapok.Add(new Munkalap(j, c, DateTime.Parse(bemenet[3]), null, (bemenet[5] == "Javitas" ? SzervizTipus.Javitas : SzervizTipus.Idoszakos)));
                                                }
                                                else
                                                {
                                                    munkalapok.Add(new Munkalap(j, c, DateTime.Parse(bemenet[3]), DateTime.Parse(bemenet[4]), (bemenet[5] == "Javitas" ? SzervizTipus.Javitas : SzervizTipus.Idoszakos)));
                                                    j.Munkalapok.Add(new Munkalap(j, c, DateTime.Parse(bemenet[3]), DateTime.Parse(bemenet[4]), (bemenet[5] == "Javitas" ? SzervizTipus.Javitas : SzervizTipus.Idoszakos)));
                                                }


                                                /*
                                                foreach (var m in j.munkalapok)
                                                {
                                                    Console.WriteLine("Vege: "+m.vege);
                                                }
                                                */
                                            }
                                        }

                                    }
                                }
                            }

                            break;
                        case "Munkafolyamat":
                            if (jarmuvek.Count != 0 && cegek.Count != 0)
                            {
                                foreach (var j in jarmuvek)
                                {
                                    if (j.Azonosito == int.Parse(bemenet[1]))
                                    {
                                        //Munkalap munkalap = j.munkalapok.Last();
                                        if (j.Munkalapok.Any())
                                        {
                                            //Console.WriteLine(j.munkalapok.Last().jarmu.azonosito);
                                            j.Munkalapok.Last().Munkafolyamatok.Add(new Munkafolyamat(bemenet[2], int.Parse(bemenet[3])));
                                        }

                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

    }
    
    public enum SzervizTipus
	{
        Idoszakos,
        Javitas
    }
}
