using System;
namespace Tomegkozlekedes
{
	public class Munkafolyamat
	{
		private string megnevezes;
		private int koltseg;

		public string Megnevezes { get => megnevezes; }

		public int Koltseg{ get => koltseg; }


        public Munkafolyamat(string megnevezes, int koltseg)
		{
			this.megnevezes = megnevezes;
			this.koltseg = koltseg;
		}
	}
}

