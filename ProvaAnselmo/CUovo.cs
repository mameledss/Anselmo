using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaAnselmo
{
	public enum Colore
	{
		Verde,
		Azzurro,
		Giallo,
		Arancione,
		Rosa,
		Viola
	}
	public class Uovo
	{
		public Colore Colore1 { get; set; }
		public Colore Colore2 { get; set; }

		public Uovo(Colore colore1, Colore colore2)
		{
			Colore1 = colore1;
			Colore2 = colore2;
		}

		public override string ToString()
		{
			return $"{Colore1}-{Colore2}";
		}

		// Check if this egg shares a color with another egg
		public bool CondivideColore(Uovo altroUovo)
		{
			return Colore1 == altroUovo.Colore1 ||
				   Colore1 == altroUovo.Colore2 ||
				   Colore2 == altroUovo.Colore1 ||
				   Colore2 == altroUovo.Colore2;
		}
	}
}
