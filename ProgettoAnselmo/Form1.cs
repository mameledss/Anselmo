

namespace ProgettoAnselmo
{
	public partial class Form1 : Form
	{
		private Queue<Uovo> fabbrica = new Queue<Uovo>();
		private Queue<Uovo> prato = new Queue<Uovo>();
		private List<Uovo> pratoVisualizzazione = new List<Uovo>();

		// Lista dei colori disponibili
		private static readonly Color[] ColoriDisponibili = new Color[]
		{
			Uovo.ColoreDaHex("#93c808"), //verde
			Uovo.ColoreDaHex("#fffdd0"), //giallo
			Uovo.ColoreDaHex("#ffd4d4"), //rosa
			Uovo.ColoreDaHex("#ff9c7e"), //arancione
			Uovo.ColoreDaHex("#d1b8ff"), //viola
			Uovo.ColoreDaHex("#dee4ff")  //azzurro
		};

		private Random random = new Random();
		private ContenitoreUova contenitoreFabbrica;
		private ContenitoreUova contenitorePrato;
		private Button btnGenera;
		private Button btnNascondi;
		private Button btnInterrompi;
		private NumericUpDown numUova;
		private Label lblNumUova;
		private FormLogger logger;

		private MenuStrip menuStrip;
		private ToolStripMenuItem menuImpostazioni;
		private ToolStripMenuItem menuVelocita;
		private ToolStripComboBox comboVelocita;

		// Flag per interrompere il processo
		private bool interrompiProcesso = false;
		// Indica quando è in corso l'animazione
		private bool animazioneInCorso = false;

		// Variabili per il ridimensionamento
		private float scaleFactorX = 1.0f;
		private float scaleFactorY = 1.0f;
		private Size originalFormSize;
		public Form1()
		{
			InitializeComponent();
			ConfiguraInterfaccia();
			logger = new();
			logger.Show();

			// Salva le dimensioni originali del form
			originalFormSize = this.Size;

			// Aggiungi gestore eventi per il ridimensionamento del form
			this.Resize += Form1_Resize;
		}
		private void Form1_Resize(object sender, EventArgs e)
		{
			if (originalFormSize.Width == 0 || originalFormSize.Height == 0)
				return;

			// Calcola i fattori di scala
			scaleFactorX = (float)this.Width / originalFormSize.Width;
			scaleFactorY = (float)this.Height / originalFormSize.Height;

			// Ridimensiona tutti i controlli
			RidimensionaControlli();
		}

		private void RidimensionaControlli()
		{
			// Ricalcola le posizioni e dimensioni dei componenti
			if (contenitoreFabbrica != null)
			{
				contenitoreFabbrica.Location = new Point((int)(20 * scaleFactorX), (int)(60 * scaleFactorY));
				contenitoreFabbrica.Size = new Size((int)(840 * scaleFactorX), (int)(160 * scaleFactorY));
			}

			if (contenitorePrato != null)
			{
				contenitorePrato.Location = new Point((int)(20 * scaleFactorX), (int)(210 * scaleFactorY));
				contenitorePrato.Size = new Size((int)(840 * scaleFactorX), (int)(160 * scaleFactorY));
			}

			int controlsY = (int)(480 * scaleFactorY);

			if (lblNumUova != null)
			{
				lblNumUova.Location = new Point((int)(20 * scaleFactorX), controlsY + 9);
				lblNumUova.Font = new Font(Font.FontFamily, 10 * Math.Min(scaleFactorX, scaleFactorY));
			}

			if (numUova != null)
			{
				numUova.Location = new Point((int)(170 * scaleFactorX), controlsY + 4);
				numUova.Size = new Size((int)(60 * scaleFactorX), (int)(25 * scaleFactorY));
				numUova.Font = new Font(Font.FontFamily, 10 * Math.Min(scaleFactorX, scaleFactorY));
			}

			int buttonWidth = (int)(140 * scaleFactorX);
			int buttonSpacing = (int)(20 * scaleFactorX);
			int firstButtonX = (int)(250 * scaleFactorX);

			if (btnGenera != null)
			{
				btnGenera.Location = new Point(firstButtonX, controlsY);
				btnGenera.Size = new Size(buttonWidth, (int)(40 * scaleFactorY));
				btnGenera.Font = new Font(Font.FontFamily, 10 * Math.Min(scaleFactorX, scaleFactorY));
			}

			if (btnNascondi != null)
			{
				btnNascondi.Location = new Point(firstButtonX + buttonWidth + buttonSpacing, controlsY);
				btnNascondi.Size = new Size(buttonWidth, (int)(40 * scaleFactorY));
				btnNascondi.Font = new Font(Font.FontFamily, 10 * Math.Min(scaleFactorX, scaleFactorY));
			}

			if (btnInterrompi != null)
			{
				btnInterrompi.Location = new Point(firstButtonX + (buttonWidth + buttonSpacing) * 2, controlsY);
				btnInterrompi.Size = new Size(buttonWidth, (int)(40 * scaleFactorY));
				btnInterrompi.Font = new Font(Font.FontFamily, 10 * Math.Min(scaleFactorX, scaleFactorY));
			}
			AggiornaInterfaccia();
		}
		private void ConfiguraInterfaccia()
		{
			// Setup the form
			this.Text = "Uova e Coniglio";
			this.Width = 900;
			this.Height = 580;

			// Imposta il form come ridimensionabile
			this.FormBorderStyle = FormBorderStyle.Sizable;
			this.MinimumSize = new Size(900, 580);  // Dimensione minima

			int controlsY = 480;

			// Create controls
			ConfiguraMenu();

			// Contenitori per uova (orizzontali)
			contenitoreFabbrica = new ContenitoreUova("Fabbrica:")
			{
				Location = new Point(20, 60),
				Size = new Size(840, 160),
				BackColor = Color.Transparent,
				BorderStyle = BorderStyle.None
			};

			contenitorePrato = new ContenitoreUova("Prato:")
			{
				Location = new Point(20, 210),
				Size = new Size(840, 160),
				BackColor = Color.Transparent,
				BorderStyle = BorderStyle.None
			};

			lblNumUova = new Label
			{
				Text = "Numero di uova:",
				Location = new Point(20, controlsY + 9),
				AutoSize = true,
				Font = new Font(Font.FontFamily, 10)
			};

			numUova = new NumericUpDown
			{
				Location = new Point(170, controlsY + 4),
				Size = new Size(60, 25),
				Minimum = 2,
				Maximum = 50,
				Value = 4,
				Font = new Font(Font.FontFamily, 10)
			};

			int buttonWidth = 140;
			int buttonSpacing = 20;
			int firstButtonX = 250;

			btnGenera = new Button
			{
				Text = "Genera Uova",
				Location = new Point(firstButtonX, controlsY),
				Size = new Size(buttonWidth, 40),
				Font = new Font(Font.FontFamily, 10),
				Cursor = Cursors.Hand
			};

			btnNascondi = new Button
			{
				Text = "Nascondi Uova",
				Location = new Point(firstButtonX + buttonWidth + buttonSpacing, controlsY),
				Size = new Size(buttonWidth, 40),
				Font = new Font(Font.FontFamily, 10),
				Cursor = Cursors.Hand
			};

			btnInterrompi = new Button
			{
				Text = "Interrompi",
				Location = new Point(firstButtonX + (buttonWidth + buttonSpacing) * 2, controlsY),
				Size = new Size(buttonWidth, 40),
				Enabled = false,
				Font = new Font(Font.FontFamily, 10),
				Cursor = Cursors.Hand
			};

			// Add event handlers
			btnGenera.Click += BtnGenera_Click;
			btnNascondi.Click += BtnNascondi_Click;
			btnInterrompi.Click += BtnInterrompi_Click;

			// Add controls to form
			this.Controls.Add(contenitoreFabbrica);
			this.Controls.Add(contenitorePrato);
			this.Controls.Add(lblNumUova);
			this.Controls.Add(numUova);
			this.Controls.Add(btnGenera);
			this.Controls.Add(btnNascondi);
			this.Controls.Add(btnInterrompi);
		}

		private void ConfiguraMenu()
		{
			// Crea il MenuStrip
			menuStrip = new MenuStrip();
			menuStrip.BackColor = Color.LightYellow;
			menuStrip.Font = new Font(Font.FontFamily, 10);

			menuStrip.Dock = DockStyle.Top; // Imposta l'ancoraggio del MenuStrip per il ridimensionamento

			// Crea il menu Impostazioni
			menuImpostazioni = new ToolStripMenuItem("Impostazioni");

			// Crea il sottomenu Velocità
			menuVelocita = new ToolStripMenuItem("Velocità");

			// Crea la ComboBox per la velocità
			comboVelocita = new ToolStripComboBox();
			comboVelocita.DropDownStyle = ComboBoxStyle.DropDownList;
			comboVelocita.Width = 120;

			// Aggiungi le opzioni di velocità
			for (int i = 1; i <= 10; i++)
			{
				comboVelocita.Items.Add(i.ToString());
			}

			// Imposta il valore predefinito (5)
			comboVelocita.SelectedIndex = 4;

			// Aggiungi la ComboBox al sottomenu Velocità
			menuVelocita.DropDownItems.Add(comboVelocita);

			// Aggiungi il sottomenu Velocità al menu Impostazioni
			menuImpostazioni.DropDownItems.Add(menuVelocita);

			// Aggiungi il menu Impostazioni al MenuStrip
			menuStrip.Items.Add(menuImpostazioni);

			// Aggiungi il MenuStrip al form
			this.Controls.Add(menuStrip);
			this.MainMenuStrip = menuStrip;
		}

		private void BtnGenera_Click(object sender, EventArgs e)
		{
			// Reset
			fabbrica.Clear();
			prato.Clear();
			pratoVisualizzazione.Clear();
			AggiornaInterfaccia();

			// Genera il numero di uova specificato
			int numeroUova = (int)numUova.Value;

			// Calcola quante metà di ciascun colore avremo bisogno
			// Poiché ogni uovo ha 2 metà, avremo bisogno di numeroUova*2 metà totali
			int totaleMetaUova = numeroUova * 2;

			// Calcola quante metà di ogni colore devono essere utilizzate
			// Vogliamo bilanciare uniformemente tra tutti i colori disponibili
			int metaPerColore = totaleMetaUova / ColoriDisponibili.Length;
			int metaExtra = totaleMetaUova % ColoriDisponibili.Length;

			// Creiamo una lista con tutte le metà di colori che utilizzeremo
			List<Color> tutteLeMetaColori = new List<Color>();

			for (int i = 0; i < ColoriDisponibili.Length; i++)
			{
				// Aggiungi il numero standard di metà per questo colore
				for (int j = 0; j < metaPerColore; j++)
				{
					tutteLeMetaColori.Add(ColoriDisponibili[i]);
				}

				// Se ci sono metà extra da distribuire, aggiungi una in più per i primi 'metaExtra' colori
				if (i < metaExtra)
				{
					tutteLeMetaColori.Add(ColoriDisponibili[i]);
				}
			}

			// Mescola la lista dei colori disponibili per la randomizzazione
			for (int i = 0; i < tutteLeMetaColori.Count; i++)
			{
				int randomIndex = random.Next(tutteLeMetaColori.Count);
				Color temp = tutteLeMetaColori[i];
				tutteLeMetaColori[i] = tutteLeMetaColori[randomIndex];
				tutteLeMetaColori[randomIndex] = temp;
			}

			// Crea le uova prendendo due colori alla volta dalla lista mescolata
			for (int i = 0; i < numeroUova; i++)
			{
				Color colore1 = tutteLeMetaColori[i * 2];
				Color colore2 = tutteLeMetaColori[i * 2 + 1];

				// Crea un nuovo uovo e aggiungilo alla fabbrica
				Uovo uovo = new Uovo(colore1, colore2);
				fabbrica.Enqueue(uovo);
			}

			AggiornaInterfaccia();
			logger.AggiungiMessaggio("Passaggio: Uova generate con successo");
		}

		private async void BtnNascondi_Click(object sender, EventArgs e)
		{
			if (fabbrica.Count == 0)
			{
				MessageBox.Show("Non ci sono uova da nascondere!");
				return;
			}

			if (animazioneInCorso)
			{
				MessageBox.Show("Un'animazione è già in corso, attendere...");
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
				logger.AggiungiMessaggio("Passaggio: Processo interrotto");
			}
			else if (successo)
			{
				logger.AggiungiMessaggio("Passaggio: Soluzione completata");
				MessageBox.Show("Soluzione completata", "Fine Simulazione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				this.Close();
			}
			else
			{
				logger.AggiungiMessaggio("Passaggio: Nessuna soluzione trovata");
				MessageBox.Show("Nessuna soluzione trovata", "Fine Simulazione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.Close();
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
				logger.AggiungiMessaggio("Passaggio: Iniziata ricerca soluzione");
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
			Invoke(new Action(() => velocita = int.Parse(comboVelocita.SelectedItem.ToString())));
			int pausa = 1100 - (velocita * 100); // Da 1000ms a 100ms

			// Base case: se la fabbrica è vuota, abbiamo trovato una soluzione
			if (fabbrica.Count == 0)
				return true;

			// Esaminiamo tutte le uova nella fabbrica
			int numeroUovaInFabbrica = fabbrica.Count;

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

				logger.AggiungiMessaggio(messaggio);

				// Aggiorna l'interfaccia per mostrare lo stato attuale
				AggiornaInterfaccia();

				// Evidenzia l'uovo corrente
				//contenitoreFabbrica.EvidenziaPrimo();
			}));
		}

		private void MostraSpostamento(Uovo uovo, bool versoPrato)
		{
			Invoke(new Action(() =>
			{
				AggiornaInterfaccia();

				if (versoPrato)
				{
					//contenitorePrato.EvidenziaUltimo();
					logger.AggiungiMessaggio($"Passaggio: Uovo {uovo} spostato nel prato");
				}
				else
				{
					//contenitoreFabbrica.EvidenziaUltimo();
					logger.AggiungiMessaggio($"Passaggio: Backtracking - Uovo {uovo} rimesso nella fabbrica");
				}
			}));
		}

		private void MostraRimessaInFabbrica(Uovo uovo)
		{
			Invoke(new Action(() =>
			{
				AggiornaInterfaccia();
				//contenitoreFabbrica.EvidenziaUltimo();
				logger.AggiungiMessaggio($"Passaggio: Uovo {uovo} non compatibile, rimesso in fabbrica");
			}));
		}
		private void AggiornaInterfaccia()
		{
			// Aggiorna il contenitore della fabbrica
			contenitoreFabbrica.AggiornaUova(fabbrica);

			// Aggiorna il contenitore del prato
			contenitorePrato.AggiornaUova(prato);
		}
		private void InitializeComponent()
		{
			SuspendLayout();
			// 
			// Form1
			// 
			BackColor = Color.White;
			BackgroundImage = Properties.Resources.Nastroprato;
			BackgroundImageLayout = ImageLayout.Stretch;
			ClientSize = new Size(900, 650);
			Name = "Form1";
			ResumeLayout(false);
		}
	}
}
