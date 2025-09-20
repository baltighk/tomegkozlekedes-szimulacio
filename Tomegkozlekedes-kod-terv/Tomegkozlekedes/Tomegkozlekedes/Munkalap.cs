using System;
namespace Tomegkozlekedes
{
    public class Munkalap
    {
        private Jarmu jarmu;
        private Szerviz szerviz;
        private DateTime kezdete;
        private DateTime? vege;
        private SzervizTipus tipus;
        private List<Munkafolyamat> munkafolyamatok = new List<Munkafolyamat>();


        public Jarmu Jarmu { get => jarmu; }
        public Szerviz Szerviz { get => szerviz; }
        public DateTime Kezdete { get => kezdete; set => kezdete = value; }
        public DateTime? Vege { get => vege; set => vege = value; }
        public SzervizTipus Tipus { get => tipus; set => tipus = value; }

        public List<Munkafolyamat> Munkafolyamatok { get => munkafolyamatok; }

        public Munkalap(Jarmu jarmu, Szerviz szerviz, DateTime kezdete, DateTime? vege, SzervizTipus tipus)
        {
            this.kezdete = kezdete;
            this.vege = vege;
            this.tipus = tipus;
            this.jarmu = jarmu;
            this.szerviz = szerviz;
        }

        public int OsszKoltseg()
        {
            
            int osszeg = 0;

            foreach (var mf in munkafolyamatok)
            {
                osszeg += mf.Koltseg;
            }

            return osszeg;

        }
    }
}