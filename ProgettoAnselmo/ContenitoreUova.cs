using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAnselmo
{
	public class ContenitoreUova : Panel
	{
		private List<UovoControl> uovaContr = new(); //lista che contiene tutti i UovoControl									  
		public ContenitoreUova()
		{
			AutoScroll = true; //abilita scorrimento automatico
			BorderStyle = BorderStyle.FixedSingle; //bordo attorno al contenitore
			this.SizeChanged += ContenitoreUova_SizeChanged; //evento che si attiva quando le dimensioni del contenitore cambiano
		}

		//evento che gestisce ridimensionamento del contenitore
		private void ContenitoreUova_SizeChanged(object sender, EventArgs e)
		{
			if (uovaContr.Count == 0) return; //se non ci sono uova, ritorna

			//fattori di scala basati su dimensioni correnti del contenitore
			float rappLargCont = this.Width / 840.0f;
			float rappAltCont = this.Height / 160.0f;

			//nuove dimensioni uova 
			int largUovo = (int)(60 * rappLargCont);
			int altUovo = (int)(80 * rappAltCont);
			int spaziatura = (int)(10 * rappLargCont); //spazio tra le uova

			int x = spaziatura; //posizione iniziale per primo uovo
			int y = spaziatura; //posizione verticale iniziale con solo lo spazio di separazione

			//itera attraverso tutti i controlli uovo per aggiornare posizione e dimensione
			foreach (UovoControl uovoControl in uovaContr)
			{
				uovoControl.Size = new Size(largUovo, altUovo); //aggiorna dimensioni
				uovoControl.Location = new Point(x, y); //e posizione
				x += largUovo + spaziatura; //calcola posizione orizzontale per il prossimo uovo

				//se l'uovo successivo supera la larghezza del contenitore, va a capo
				if (x + largUovo > Width - spaziatura)
				{
					x = spaziatura; //reimposta posizione orizzontale a inizio riga
					y += altUovo + spaziatura; //incrementa posizione verticale per passare a riga successiva
				}
			}
		}
		
		//metodo che aggiorna le uova nel contenitore
		public void AggiornaUova(IEnumerable<Uovo> uova)
		{
			foreach (UovoControl control in uovaContr) //rimuove tutti i uovocontrol esistenti dal contenitore
			{
				Controls.Remove(control);
			}
			uovaContr.Clear(); //svuota lista dei controlli

			//fattori di scala basati sulle dimensioni correnti del contenitore
			float rappLargCont = this.Width / 840.0f;
			float rappAltCont = this.Height / 160.0f;

			//dimensioni per i nuovi controlli uovo
			int largUovo = (int)(60 * rappLargCont);
			int altUovo = (int)(80 * rappAltCont);
			int spaziatura = (int)(10 * rappLargCont); //calcola spazio tra le uova

			int x = spaziatura; //posizione iniziale per primo uovo
			int y = spaziatura; //posizione verticale iniziale con solo lo spazio di separazione

			//itera attraverso le uova fornite per creare nuovi controlli
			foreach (Uovo uovo in uova)
			{
				UovoControl uovoControl = new UovoControl(uovo); //crea un nuovo uovocontrol per l'uovo corrente														 
				uovoControl.Size = new Size(largUovo, altUovo); //imposta le dimensioni del controllo										
				uovoControl.Location = new Point(x, y); //e la posizione
				Controls.Add(uovoControl);
				uovaContr.Add(uovoControl);
				x += largUovo + spaziatura; //calcola la posizione orizzontale per il prossimo uovo

				//se l'uovo successivo supera la larghezza del contenitore, va a capo
				if (x + largUovo > Width - spaziatura)
				{
					x = spaziatura; //reimposta la posizione orizzontale a inizio riga
					y += altUovo + spaziatura; //incrementa la posizione verticale per passare alla riga successiva
				}
			}
		}
	}
}