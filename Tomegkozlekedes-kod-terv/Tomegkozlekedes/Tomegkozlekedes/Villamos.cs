using System;
namespace Tomegkozlekedes
{
	public class Villamos : Jarmu
	{
		public Villamos(int azonosito, int gyartasiEv, Ovezet hasznalatiOvezet, int ujkoriAr) : base(azonosito, gyartasiEv, hasznalatiOvezet, ujkoriAr)
		{
		}

        /*
        public override JarmuFajta Fajta()
        {
            return JarmuFajta.Villamos;
        }
        */

        public override double GetFaktor(Ovezet ovezet)
        {
            return ovezet.GetValue(this);
        }

        public override double GetValue(Belvaros ovezet)
        {
            return 1.0;
        }
        public override double GetValue(Kulvaros ovezet)
        {
            return 0.9;
        }

        public override double GetValue(Vegyes ovezet)
        {
            return 1.2;
        }

        /*
        public override double GetValue(Ovezet ovezet)
        {
            switch (ovezet)
            {
                case Ovezet.Belvaros:
                    return 1.0;
                case Ovezet.Kulvaros:
                    return 0.9;
                case Ovezet.Vegyes:
                    return 1.2;
            }
            throw new Exception("Hibás Villamos érték");
        }
        */
    }
}

