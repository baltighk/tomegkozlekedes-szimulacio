using System;
namespace Tomegkozlekedes
{
	public abstract class Ovezet
	{
        public abstract double GetValue(Jarmu jarmu);

        

        public static Ovezet GetOvezet(string bemenet)
        {
            if(bemenet == "Belvaros")
            {
                return new Belvaros();
            }else if(bemenet == "Kulvaros")
            {
                return new Kulvaros();
            }else if(bemenet == "Vegyes")
            {
                return new Vegyes();
            }
            else
            {
                throw new Exception("Helytelen övezet");
            }
        }
    }

    public class Belvaros : Ovezet
    {
        private static Belvaros? instance;

        public static Belvaros Instance()
        {
            if (instance == null)
            {
                instance = new();
            }
            return instance;
        }

        public override double GetValue(Jarmu jarmu)
        {
            return jarmu.GetValue(this);
        }
    }

    public class Kulvaros : Ovezet
    {
        private static Kulvaros? instance;

        public static Kulvaros Instance()
        {
            if (instance == null)
            {
                instance = new();
            }
            return instance;
        }

        public override double GetValue(Jarmu jarmu)
        {
            return jarmu.GetValue(this);
        }
    }

    public class Vegyes : Ovezet
    {
        private static Vegyes? instance;

        public static Vegyes Instance()
        {
            if (instance == null)
            {
                instance = new();
            }
            return instance;
        }

        public override double GetValue(Jarmu jarmu)
        {
            return jarmu.GetValue(this);
        }
    }
}

