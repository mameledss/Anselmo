using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace ProvaAnselmo
{
	public partial class Form1 : Form
	{
		private Queue<Uovo> fabbrica = new Queue<Uovo>();
		private Queue<Uovo> prato = new Queue<Uovo>(); 
					
		private List<Uovo> pratoVisualizzazione = new List<Uovo>();

		private Random random = new Random();
		private ListBox lstFabbrica;
		private ListBox lstPrato;
		private Button btnGenera;
		private Button btnNascondi;
		private Label lblInfo;
		private Label lblPasso;
		private TrackBar trackBarVelocita;
		private Label lblVelocita;
		private Button btnInterrompi;

		// Flag per interrompere il processo
		private bool interrompiProcesso = false;
		// Indica quando è in corso l'animazione
		private bool animazioneInCorso = false;

		public Form1()
		{
			InitializeComponent();
			ConfiguraInterfaccia();
		}

		private void ConfiguraInterfaccia()
		{
			// Setup the form
			this.Text = "Uova e Coniglio";
			this.Width = 600;
			this.Height = 550;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;

			// Create controls
			lblInfo = new Label
			{
				Text = "Simulazione uova e coniglio",
				Location = new Point(20, 20),
				AutoSize = true
			};

			lstFabbrica = new ListBox
			{
				Location = new Point(20, 50),
				Size = new Size(250, 300)
			};

			lstPrato = new ListBox
			{
				Location = new Point(290, 50),
				Size = new Size(250, 300)
			};

			btnGenera = new Button
			{
				Text = "Genera Uova",
				Location = new Point(20, 370),
				Size = new Size(250, 30)
			};

			btnNascondi = new Button
			{
				Text = "Nascondi Uova",
				Location = new Point(290, 370),
				Size = new Size(250, 30)
			};

			lblPasso = new Label
			{
				Text = "Passaggio: Nessuna operazione in corso",
				Location = new Point(20, 410),
				Size = new Size(520, 20),
				AutoSize = false
			};

			lblVelocita = new Label
			{
				Text = "Velocità:",
				Location = new Point(20, 440),
				AutoSize = true
			};

			trackBarVelocita = new TrackBar
			{
				Location = new Point(80, 435),
				Size = new Size(200, 45),
				Minimum = 1,
				Maximum = 10,
				Value = 5,
				TickFrequency = 1,
				Cursor = Cursors.Hand,
			};

			btnInterrompi = new Button
			{
				Text = "Interrompi",
				Location = new Point(290, 435),
				Size = new Size(250, 30),
				Enabled = false
			};

			// Add event handlers
			btnGenera.Click += BtnGenera_Click;
			btnNascondi.Click += BtnNascondi_Click;
			btnInterrompi.Click += BtnInterrompi_Click;

			// Add controls to form
			this.Controls.Add(lblInfo);
			this.Controls.Add(lstFabbrica);
			this.Controls.Add(lstPrato);
			this.Controls.Add(btnGenera);
			this.Controls.Add(btnNascondi);
			this.Controls.Add(lblPasso);
			this.Controls.Add(lblVelocita);
			this.Controls.Add(trackBarVelocita);
			this.Controls.Add(btnInterrompi);

			// Add labels
			Label lblFabbrica = new Label
			{
				Text = "Fabbrica:",
				Location = new Point(20, 35),
				AutoSize = true
			};

			Label lblPrato = new Label
			{
				Text = "Prato:",
				Location = new Point(290, 35),
				AutoSize = true
			};

			this.Controls.Add(lblFabbrica);
			this.Controls.Add(lblPrato);
		}

		private void BtnGenera_Click(object sender, EventArgs e)
		{
			// Reset
			fabbrica.Clear();
			prato.Clear();
			pratoVisualizzazione.Clear();
			AggiornaInterfaccia();

			// Genera un numero pari di uova (per esempio 6)
			int numeroUova = 4;

			for (int i = 0; i < numeroUova; i++)
			{
				// Scegli due colori casuali
				Colore colore1 = (Colore)random.Next(Enum.GetValues(typeof(Colore)).Length);
				Colore colore2 = (Colore)random.Next(Enum.GetValues(typeof(Colore)).Length);

				// Crea un nuovo uovo e aggiungilo alla fabbrica
				Uovo uovo = new Uovo(colore1, colore2);
				fabbrica.Enqueue(uovo);
			}

			AggiornaInterfaccia();
			lblInfo.Text = $"Generate {numeroUova} uova nella fabbrica";
			lblPasso.Text = "Passaggio: Uova generate con successo";
		}

		private async void BtnNascondi_Click(object sender, EventArgs e)
		{
			if (fabbrica.Count == 0)
			{
				lblInfo.Text = "Non ci sono uova da nascondere!";
				return;
			}

			if (animazioneInCorso)
			{
				lblInfo.Text = "Un'animazione è già in corso, attendere...";
				return;
			}

			// Imposta i flag e gli stati dei pulsanti
			interrompiProcesso = false;
			animazioneInCorso = true;
			btnNascondi.Enabled = false;
			btnGenera.Enabled = false;
			btnInterrompi.Enabled = true;

			// Avvia il processo di soluzione in un Task separato
			bool successo = await Task.Run(() => TrovaSoluzione());

			// Reimposta i flag e gli stati dei pulsanti
			animazioneInCorso = false;
			btnNascondi.Enabled = true;
			btnGenera.Enabled = true;
			btnInterrompi.Enabled = false;

			if (interrompiProcesso)
			{
				lblInfo.Text = "Processo interrotto dall'utente.";
				lblPasso.Text = "Passaggio: Processo interrotto";
			}
			else if (successo)
			{
				lblInfo.Text = "Tutte le uova sono state nascoste con successo!";
				lblPasso.Text = "Passaggio: Soluzione completata";
			}
			else
			{
				lblInfo.Text = "Non è stato possibile nascondere tutte le uova. Alcune rimangono in fabbrica.";
				lblPasso.Text = "Passaggio: Nessuna soluzione trovata";
			}
		}

		private void BtnInterrompi_Click(object sender, EventArgs e)
		{
			interrompiProcesso = true;
		}

		private bool TrovaSoluzione()
		{
			// Iniziamo con un prato vuoto
			Invoke(new Action(() =>
			{
				prato.Clear();
				pratoVisualizzazione.Clear();
				AggiornaInterfaccia();
				lblPasso.Text = "Passaggio: Iniziata ricerca soluzione";
			}));

			// Copiamo la fabbrica originale per non alterarla durante la ricerca
			List<Uovo> uovaOriginali = new List<Uovo>(fabbrica);
			fabbrica.Clear();

			foreach (var uovo in uovaOriginali)
			{
				fabbrica.Enqueue(uovo);
			}

			return BacktrackingNascondiUova();
		}

		private bool BacktrackingNascondiUova()
		{
			// Controlla se il processo è stato interrotto
			if (interrompiProcesso)
				return false;

			// Ottieni la velocità attuale (inversa - più basso = più lento)
			int velocita = 0;
			Invoke(new Action(() => velocita = trackBarVelocita.Value));
			int pausa = 1100 - (velocita * 100); // Da 1000ms a 100ms

			// Base case: se la fabbrica è vuota, abbiamo trovato una soluzione
			if (fabbrica.Count == 0)
				return true;

			// Esaminiamo tutte le uova nella fabbrica
			int numeroUovaInFabbrica = fabbrica.Count;
			List<Uovo> uovaSpostate = new List<Uovo>();

			// Copia temporanea della fabbrica per iterare senza alterarla
			Queue<Uovo> fabbricaTemporanea = new Queue<Uovo>();

			for (int i = 0; i < numeroUovaInFabbrica; i++)
			{
				// Controlla se il processo è stato interrotto
				if (interrompiProcesso)
					return false;

				Uovo uovoCorrente = fabbrica.Dequeue();

				// Salviamo una copia della coda del prato e della lista di visualizzazione
				Queue<Uovo> pratoBackup = new Queue<Uovo>(prato);
				List<Uovo> pratoVisualizzazioneBackup = new List<Uovo>(pratoVisualizzazione);


				// Visualizza l'uovo corrente che stiamo valutando
				bool puoEssereNascosto = pratoVisualizzazione.Count == 0 || uovoCorrente.CondivideColore(pratoVisualizzazione.Last());
				MostraTentativo(uovoCorrente, puoEssereNascosto);
				Thread.Sleep(pausa);

				if (puoEssereNascosto)
				{
					// Nascondi l'uovo
					prato.Enqueue(uovoCorrente);
					pratoVisualizzazione.Add(uovoCorrente);

					// Visualizza lo spostamento
					MostraSpostamento(uovoCorrente, versoPrato: true);
					Thread.Sleep(pausa);

					// Ricorsione
					if (BacktrackingNascondiUova())
						return true;

					// Backtracking: rimuovi l'ultimo uovo dal prato
					// Ripristino le code ai valori precedenti
					prato = pratoBackup;
					pratoVisualizzazione = pratoVisualizzazioneBackup;

					// Rimetti l'uovo alla fine della fabbrica
					fabbrica.Enqueue(uovoCorrente);

					// Visualizza il backtracking
					MostraSpostamento(uovoCorrente, versoPrato: false);
					Thread.Sleep(pausa);
				}
				else
				{
					// Rimetti l'uovo alla fine della fabbrica
					fabbrica.Enqueue(uovoCorrente);

					// Visualizza che l'uovo non può essere spostato e viene rimesso in fabbrica
					MostraRimessaInFabbrica(uovoCorrente);
					Thread.Sleep(pausa / 2); // Più veloce perché è solo un'azione informativa
				}
			}

			// Se arriviamo qui, non abbiamo trovato una soluzione con l'attuale configurazione
			return false;
		}

		private void MostraTentativo(Uovo uovo, bool puoEssereNascosto)
		{
			Invoke(new Action(() =>
			{
				string messaggio = puoEssereNascosto
					? $"Tentativo: L'uovo {uovo} può essere spostato nel prato"
					: $"Tentativo: L'uovo {uovo} NON può essere spostato nel prato";

				lblPasso.Text = messaggio;

				// Aggiorna l'interfaccia per mostrare lo stato attuale
				AggiornaInterfaccia();

				// Seleziona l'uovo corrente se possibile
				if (lstFabbrica.Items.Count > 0)
				{
					lstFabbrica.SelectedIndex = 0; // Il primo elemento nella coda
				}
			}));
		}

		private void MostraSpostamento(Uovo uovo, bool versoPrato)
		{
			Invoke(new Action(() =>
			{
				AggiornaInterfaccia();

				if (versoPrato && lstPrato.Items.Count > 0)
				{
					lstPrato.SelectedIndex = lstPrato.Items.Count - 1;
					lblPasso.Text = $"Passaggio: Uovo {uovo} spostato nel prato";
				}
				else if (!versoPrato)
				{
					if (lstFabbrica.Items.Count > 0)
					{
						lstFabbrica.SelectedIndex = lstFabbrica.Items.Count - 1; // L'uovo è stato appena aggiunto alla fine della coda
					}
					lblPasso.Text = $"Passaggio: Backtracking - Uovo {uovo} rimesso nella fabbrica";
				}
			}));
		}

		private void MostraRimessaInFabbrica(Uovo uovo)
		{
			Invoke(new Action(() =>
			{
				AggiornaInterfaccia();
				lblPasso.Text = $"Passaggio: Uovo {uovo} non compatibile, rimesso in fabbrica";
			}));
		}

		private void AggiornaInterfaccia()
		{
			// Aggiorna la lista della fabbrica
			lstFabbrica.Items.Clear();
			foreach (var uovo in fabbrica)
			{
				lstFabbrica.Items.Add(uovo.ToString());
			}

			// Aggiorna la lista del prato
			lstPrato.Items.Clear();
			foreach (var uovo in prato)
			{
				lstPrato.Items.Add(uovo.ToString());
			}
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// FormUovaEConiglio
			// 
			this.ClientSize = new System.Drawing.Size(278, 244);
			this.Name = "FormUovaEConiglio";
			this.ResumeLayout(false);
		}
	}
}