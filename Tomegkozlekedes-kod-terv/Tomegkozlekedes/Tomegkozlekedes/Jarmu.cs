using System;
namespace Tomegkozlekedes
{
	public abstract class Jarmu
	{
        private int azonosito;
        private int gyartasiEv;
        private Ovezet hasznalatiOvezet;
        private int ujkoriAr;
        private List<Munkalap> munkalapok = new();

        public int Azonosito { get => azonosito; }
        public int GyartasiEv { get => gyartasiEv; }
        public Ovezet HasznalatiOvezet { get => hasznalatiOvezet; }
        public int UjkoriAr { get => ujkoriAr; }
        public List<Munkalap> Munkalapok { get => munkalapok; }

        public Jarmu(int azonosito, int gyartasiEv, Ovezet hasznalatiOvezet, int ujkoriAr)
        {
            this.azonosito = azonosito;
            this.gyartasiEv = gyartasiEv;
            this.hasznalatiOvezet = hasznalatiOvezet;
            this.ujkoriAr = ujkoriAr;
        }

        public abstract double GetFaktor(Ovezet ovezet);

        //public abstract Ovezet GetOvezet(string bemenet);

        /*
        public static Ovezet GetOvezet(string bemenet)
        {
            switch (bemenet)
            {
                case "Belvaros":
                    return Ovezet.Belvaros;
                case "Kulvaros":
                    return Ovezet.Kulvaros;
                case "Vegyes":
                    return Ovezet.Vegyes;
            }

            throw new Exception("Hibás Övezet!");  
            
        }
        */

        public int AktualisErtek(int jelenEv)
        {
            double faktor = this.GetFaktor(hasznalatiOvezet);

            return (int)Math.Round(ujkoriAr * (100 - (jelenEv - gyartasiEv)) / (100.0 * faktor));
        }

        //Törlendő -- public abstract JarmuFajta Fajta();

        public abstract double GetValue(Belvaros ovezet);
        public abstract double GetValue(Kulvaros ovezet);
        public abstract double GetValue(Vegyes ovezet);

        public static Jarmu LegdragabbJarmu(List<Jarmu> jarmuvek, int jelenEv)
        {
            Jarmu ?legdragabb = null;
            double maxErtek = double.MinValue;

            foreach (var j in jarmuvek)
            {
                double koltseg = j.ujkoriAr - j.AktualisErtek(jelenEv);


                foreach (var m in j.munkalapok)
                {
                    koltseg += m.OsszKoltseg();
                }


                if (koltseg > maxErtek)
                {
                    maxErtek = koltseg;
                    legdragabb = j;
                }
            }

            if (legdragabb == null)
            {
                throw new Exception("Nincsen jármű a listán");
            }
            else
            {
                return legdragabb;
            }
        }

        public static double JavitasAlattiArany(List<Jarmu> jarmuvek)
        {
            int osszes = jarmuvek.Count;
            int javitasAlatt = 0;

            foreach (var j in jarmuvek)
            {
                foreach (var m in j.munkalapok)
                {

                    if (m.Tipus == SzervizTipus.Javitas && m.Vege == null)
                    {
                        javitasAlatt++;
                        break; // csak egyszer számítson minden jármű
                    }
                }
            }

            return osszes > 0 ? (double)javitasAlatt / osszes : 0;
        }


    }
}

