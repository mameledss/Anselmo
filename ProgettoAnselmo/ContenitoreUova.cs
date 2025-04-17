using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAnselmo
{
	public class ContenitoreUova : Panel
	{
		private List<UovoControl> uovaControls = new List<UovoControl>();
		private Label etichetta;

		// Variabili per ridimensionamento
		private float scaleFactorX = 1.0f;
		private float scaleFactorY = 1.0f;

		public ContenitoreUova(string nome)
		{
			AutoScroll = true;
			BorderStyle = BorderStyle.FixedSingle;

			// Creiamo un'etichetta per il contenitore
			etichetta = new Label
			{
				Text = nome,
				AutoSize = true,
				Location = new Point(10, 10),
				Font = new Font(Font.FontFamily, 10, FontStyle.Bold)
			};
			Controls.Add(etichetta);

			// Aggiungi evento per il ridimensionamento del contenitore
			this.SizeChanged += ContenitoreUova_SizeChanged;
		}

		private void ContenitoreUova_SizeChanged(object sender, EventArgs e)
		{
			// Aggiorna il testo dell'etichetta con la nuova dimensione del carattere
			if (etichetta != null)
			{
				// Calcola i fattori di scala dal contenitore originale
				float containerWidthRatio = this.Width / 840.0f;  // 840 è la larghezza originale
				float containerHeightRatio = this.Height / 160.0f; // 160 è l'altezza originale

				// Aggiorna il font dell'etichetta
				float newFontSize = 10 * Math.Min(containerWidthRatio, containerHeightRatio);
				newFontSize = Math.Max(8, newFontSize); // Imposta un minimo di 8
				etichetta.Font = new Font(etichetta.Font.FontFamily, newFontSize, FontStyle.Bold);

				// Aggiorna la posizione
				etichetta.Location = new Point((int)(10 * containerWidthRatio), (int)(10 * containerHeightRatio));
			}

			// Ridimensiona e riposiziona le uova
			AggiornaLayoutUova();
		}

		private void AggiornaLayoutUova()
		{
			if (uovaControls.Count == 0) return;

			// Calcola i fattori di scala
			float containerWidthRatio = this.Width / 840.0f;
			float containerHeightRatio = this.Height / 160.0f;

			// Dimensioni dell'uovo scalate
			int uovoWidth = (int)(60 * containerWidthRatio);
			int uovoHeight = (int)(80 * containerHeightRatio);
			int spacing = (int)(10 * containerWidthRatio);

			// Posizione iniziale
			int x = spacing;
			int y = etichetta.Bottom + spacing;

			foreach (var uovoControl in uovaControls)
			{
				// Aggiorna dimensione dell'uovo
				uovoControl.Size = new Size(uovoWidth, uovoHeight);
				uovoControl.Location = new Point(x, y);

				// Sposta la posizione per il prossimo uovo
				x += uovoWidth + spacing;

				// Se l'uovo va oltre la larghezza del contenitore, vai a capo
				if (x + uovoWidth > Width - spacing)
				{
					x = spacing;
					y += uovoHeight + spacing;
				}
			}
		}

		public void AggiornaUova(IEnumerable<Uovo> uova)
		{
			// Rimuovi tutti i controlli esistenti
			foreach (var control in uovaControls)
			{
				Controls.Remove(control);
			}
			uovaControls.Clear();

			// Calcola i fattori di scala
			float containerWidthRatio = this.Width / 840.0f;
			float containerHeightRatio = this.Height / 160.0f;

			// Dimensioni dell'uovo scalate
			int uovoWidth = (int)(60 * containerWidthRatio);
			int uovoHeight = (int)(80 * containerHeightRatio);
			int spacing = (int)(10 * containerWidthRatio);

			// Posizione iniziale
			int x = spacing;
			int y = etichetta.Bottom + spacing;

			foreach (var uovo in uova)
			{
				var uovoControl = new UovoControl(uovo);
				uovoControl.Size = new Size(uovoWidth, uovoHeight);
				uovoControl.Location = new Point(x, y);
				Controls.Add(uovoControl);
				uovaControls.Add(uovoControl);

				// Sposta la posizione per il prossimo uovo
				x += uovoWidth + spacing;

				// Se l'uovo va oltre la larghezza del contenitore, vai a capo
				if (x + uovoWidth > Width - spacing)
				{
					x = spacing;
					y += uovoHeight + spacing;
				}
			}
		}

		//public void EvidenziaUltimo()
		//{
		//	// Rimuovi l'evidenziazione da tutti
		//	foreach (var control in uovaControls)
		//	{
		//		control.BackColor = Color.Transparent;
		//	}

		//	// Evidenzia l'ultimo se disponibile
		//	if (uovaControls.Count > 0)
		//	{
		//		uovaControls.Last().BackColor = Color.LightYellow;
		//	}
		//}

		//public void EvidenziaPrimo()
		//{
		//	// Rimuovi l'evidenziazione da tutti
		//	foreach (var control in uovaControls)
		//	{
		//		control.BackColor = Color.Transparent;
		//	}

		//	// Evidenzia il primo se disponibile
		//	if (uovaControls.Count > 0)
		//	{
		//		uovaControls.First().BackColor = Color.LightYellow;
		//	}
		//}
	}
}
