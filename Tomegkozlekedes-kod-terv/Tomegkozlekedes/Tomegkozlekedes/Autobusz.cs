using System;
namespace Tomegkozlekedes
{
	public class Autobusz : Jarmu
	{
		public Autobusz(int azonosito, int gyartasiEv, Ovezet hasznalatiOvezet, int ujkoriAr) : base(azonosito, gyartasiEv, hasznalatiOvezet, ujkoriAr)
		{
		}

        /*
        public override JarmuFajta Fajta()
        {
            return JarmuFajta.Autobusz;
        }
        */

        public override double GetFaktor(Ovezet ovezet)
        {
            return ovezet.GetValue(this);
        }



        public override double GetValue(Belvaros ovezet)
        {
            return 2.0;
        }
        public override double GetValue(Kulvaros ovezet)
        {
            return 2.0;
        }

        public override double GetValue(Vegyes ovezet)
        {
            return 2.5;
        }



        public static double EloregedettAutobuszokAranya(List<Jarmu> jarmuvek, int jelenEv)
        {
            int osszBusz = 0;
            int regiBusz = 0;


            foreach (var j in jarmuvek)
            {
                if (j is Autobusz)
                {
                    osszBusz++;
                    if (jelenEv - j.GyartasiEv > 15)
                    {
                        regiBusz++;
                    }
                }
            }

            if (osszBusz == 0)
            {
                return 0;
            }

            return (double)regiBusz / osszBusz;
        }
    }
}

