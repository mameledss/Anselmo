using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAnselmo
{
	public class Uovo
	{
		public Color Colore1 { get; set; } //colore 1
		public Color Colore2 { get; set; } //colore 2

		public Uovo(Color colore1, Color colore2)
		{
			Colore1 = colore1;
			Colore2 = colore2;
		}

		public override string ToString() //metodo per restituire i colori delle due metà in stringhe
		{
			return $"{ColorToString(Colore1)}-{ColorToString(Colore2)}";
		}

		private string ColorToString(Color color)
		{
			if (color == Color.Green) return "Verde";
			if (color == Color.SkyBlue) return "Azzurro";
			if (color == Color.Yellow) return "Giallo";
			if (color == Color.Orange) return "Arancione";
			if (color == Color.Pink) return "Rosa";
			if (color == Color.Purple) return "Viola";
			return color.Name;
		}
		public static Color ColoreDaHex(string hex) //metodo per convertire in rgb da hex
		{
			if (hex.StartsWith("#"))
				hex = hex.Substring(1);

			if (hex.Length != 6)
				throw new ArgumentException("Formato esadecimale non valido");

			int r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
			int g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
			int b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

			return Color.FromArgb(r, g, b);
		}

		//metodo per verificare se un uovo condivide un colore con un altro
		public bool CondivideColore(Uovo altroUovo)
		{
			return Colore1.ToArgb() == altroUovo.Colore1.ToArgb() ||
				   Colore1.ToArgb() == altroUovo.Colore2.ToArgb() ||
				   Colore2.ToArgb() == altroUovo.Colore1.ToArgb() ||
				   Colore2.ToArgb() == altroUovo.Colore2.ToArgb();
		}
	}
}
