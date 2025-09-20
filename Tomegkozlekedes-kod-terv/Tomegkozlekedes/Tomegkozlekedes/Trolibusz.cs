using System;
namespace Tomegkozlekedes
{
	public class Trolibusz : Jarmu
	{
		public Trolibusz(int azonosito, int gyartasiEv, Ovezet hasznalatiOvezet, int ujkoriAr) : base(azonosito, gyartasiEv, hasznalatiOvezet, ujkoriAr)
		{
		}

        /*
        public override JarmuFajta Fajta()
        {
            return JarmuFajta.Trolibusz;
        }
        */

        public override double GetFaktor(Ovezet ovezet)
        {
            return ovezet.GetValue(this);
        }

        public override double GetValue(Belvaros ovezet)
        {
            return 3.0;
        }
        public override double GetValue(Kulvaros ovezet)
        {
            return 3.1;
        }

        public override double GetValue(Vegyes ovezet)
        {
            return 3.8;
        }

        /*
        public override double GetValue(Ovezet ovezet)
        {
            switch (ovezet)
            {
                case Ovezet.Belvaros:
                    return 3.0;
                case Ovezet.Kulvaros:
                    return 3.1;
                case Ovezet.Vegyes:
                    return 3.8;
            }
            throw new Exception("Hibás trolibusz értrék");
        }
        */
    }
}

