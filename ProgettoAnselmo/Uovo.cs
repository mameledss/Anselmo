using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAnselmo
{
	public class Uovo
	{
		public Color Colore1 { get; set; } //primo colore dell'uovo (metà superiore)
		public Color Colore2 { get; set; } //e secondo (metà inferiore)
		public Uovo(Color colore1, Color colore2)
		{
			Colore1 = colore1;
			Colore2 = colore2;
		}

		//metodo per convertire stringa esadecimale in oggetto Color
		public static Color ColoreDaHex(string hex)
		{ 
			if (hex.StartsWith("#")) //se la stringa inizia con "#"
				hex = hex.Substring(1); //lo rimuove

			if (hex.Length != 6) //se la stringa ha una lunghezza errata
				throw new ArgumentException("Formato esadecimale non valido"); //eccezione

			//estrae i componenti RGB dalla stringa esadecimale
			int r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
			int g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
			int b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

			return Color.FromArgb(r, g, b); //ritorna un nuovo oggetto Color usando i valori RGB estratti
		}

		//metodo per verificare se l'uovo condivide almeno un colore con un altro uovo
		public bool CondivideColore(Uovo altroUovo)
		{
			//confronta i valori RGB dei colori per determinare se c'è una corrispondenza
			return Colore1.ToArgb() == altroUovo.Colore1.ToArgb() ||
				   Colore1.ToArgb() == altroUovo.Colore2.ToArgb() ||
				   Colore2.ToArgb() == altroUovo.Colore1.ToArgb() ||
				   Colore2.ToArgb() == altroUovo.Colore2.ToArgb();
		}
	}
}