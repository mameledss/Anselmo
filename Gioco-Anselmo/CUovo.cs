using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anselmo
{
	public enum Colore
	{
		Verde, Azzurro, Giallo, Arancione, Rosa, Viola
	}
	public class Uovo
	{
		public Colore Colore1 { get; }
		public Colore Colore2 { get; }

		public Uovo(Colore c1, Colore c2)
		{
			Colore1 = c1;
			Colore2 = c2;
		}

		public bool HaColoreInComune(Uovo altro)
		{
			return Colore1 == altro.Colore1 || Colore1 == altro.Colore2 ||
				   Colore2 == altro.Colore1 || Colore2 == altro.Colore2;
		}

		public override string ToString()
		{
			return $"{Colore1}-{Colore2}";
		}
	}

	public class Fabbrica
	{
		private Random random = new Random();
		private Queue<Uovo> scivolo = new Queue<Uovo>();

		public Queue<Uovo> Scivolo => new Queue<Uovo>(scivolo); // copia protetta

		public void GeneraUova(int numeroMetà)
		{
			List<Colore> metà = new List<Colore>();
			for (int i = 0; i < numeroMetà; i++)
			{
				metà.Add((Colore)random.Next(0, 6));
			}

			// Assembla metà casualmente in uova
			while (metà.Count >= 2)
			{
				int index1 = random.Next(metà.Count);
				Colore c1 = metà[index1];
				metà.RemoveAt(index1);

				int index2 = random.Next(metà.Count);
				Colore c2 = metà[index2];
				metà.RemoveAt(index2);

				scivolo.Enqueue(new Uovo(c1, c2));
			}
		}

		public void GeneraUovaDaMeta(int numeroMetà)
		{
			scivolo.Clear();

			// 1. Crea lista di metà casuali
			List<Colore> metà = new List<Colore>();
			for (int i = 0; i < numeroMetà; i++)
			{
				metà.Add((Colore)random.Next(0, 6));
			}

			// 2. Assembla le metà in uova
			while (metà.Count >= 2)
			{
				int index1 = random.Next(metà.Count);
				Colore c1 = metà[index1];
				metà.RemoveAt(index1);

				int index2 = random.Next(metà.Count);
				Colore c2 = metà[index2];
				metà.RemoveAt(index2);

				scivolo.Enqueue(new Uovo(c1, c2));
			}
		}

		public bool HaUova => scivolo.Count > 0;

		public Uovo PrelevaUovo()
		{
			return scivolo.Dequeue();
		}

		public void RimettiUovo(Uovo uovo)
		{
			scivolo.Enqueue(uovo);
		}
	}

	public class Prato
	{
		private List<Uovo> uovaNascoste = new List<Uovo>();

		public IReadOnlyList<Uovo> UovaNascoste => uovaNascoste.AsReadOnly();

		public bool PossoNascondere(Uovo uovo)
		{
			if (uovaNascoste.Count == 0) return true;
			return uovo.HaColoreInComune(uovaNascoste.Last());
		}

		public void NascondiUovo(Uovo uovo)
		{
			uovaNascoste.Add(uovo);
		}

		public void RimuoviUltimo()
		{
			if (uovaNascoste.Count > 0)
				uovaNascoste.RemoveAt(uovaNascoste.Count - 1);
		}
	}

	public class Anselmo
	{
		private Fabbrica fabbrica;
		private Prato prato;
		private Stack<Uovo> backtrackStack = new Stack<Uovo>();

		public Anselmo(Fabbrica f, Prato p)
		{
			fabbrica = f;
			prato = p;
		}

		public bool NascondiUova()
		{
			return NascondiRicorsivo();
		}

		private bool NascondiRicorsivo()
		{
			if (!fabbrica.HaUova)
				return true; // tutte le uova sono state usate

			int tentativi = fabbrica.Scivolo.Count;
			for (int i = 0; i < tentativi; i++)
			{
				Uovo uovo = fabbrica.PrelevaUovo();

				if (prato.PossoNascondere(uovo))
				{
					prato.NascondiUovo(uovo);
					backtrackStack.Push(uovo);

					if (NascondiRicorsivo())
						return true;

					// Backtracking
					prato.RimuoviUltimo();
					backtrackStack.Pop();
				}

				// Rimetti alla fine
				fabbrica.RimettiUovo(uovo);
			}
			return false;
		}
	}
}