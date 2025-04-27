using Microsoft.VisualBasic.Logging;
using System.Drawing.Drawing2D;
using System.Media;
using System.Windows.Forms;

namespace ProgettoAnselmo
{
	public partial class Form1 : Form
	{
		private Queue<Uovo> fabbrica = new Queue<Uovo>(); //coda per memorizzare le uova nella fabbrica
		private Queue<Uovo> prato = new Queue<Uovo>(); //coda per memorizzare le uova nel prato
		private List<Uovo> pratoVisualizzazione = new List<Uovo>(); //lista per la visualizzazione delle uova nel prato

		private static readonly Color[] ColoriDisponibili = new Color[] //array di colori per le uova
		{
			Uovo.ColoreDaHex("#93c808"), //verde
			Uovo.ColoreDaHex("#fffdd0"), //giallo
			Uovo.ColoreDaHex("#ffd4d4"), //rosa
			Uovo.ColoreDaHex("#ff9c7e"), //arancione
			Uovo.ColoreDaHex("#d1b8ff"), //viola
			Uovo.ColoreDaHex("#dee4ff")  //azzurro
		};
		private Random random = new Random();
		private ContenitoreUova contenitoreFabbrica; //controlli per visualizzare le uova nella fabbrica	 
		private ContenitoreUova contenitorePrato; //e nel prato
		private FormLogger logger; //form logger

		private MenuStrip menuStrip; //elementi del menu
		private ToolStripMenuItem menuImpostazioni; //impostazioni del menu
		private ToolStripMenuItem menuVelocita; //sottomenu per velocità dell'animazione
		private ToolStripMenuItem menuMusica; //impostazioni musica di sottofondo
		private ToolStripMenuItem menuCrediti; //autore
		private ToolStripComboBox comboVelocita; //comboBox per selezionare la velocità
		private ToolStripMenuItem playSound; //elemento del menu per far partire musica
		private ToolStripMenuItem stopSound; //e per fermare
		private ToolStripMenuItem credits; //sezione per i crediti
		private SoundPlayer player; //elemento per riprodurre musica

		private bool interrompiProcesso = false; //flag per controllare l'interruzione del processo di backtracking
		private bool animazioneInCorso = false; //flag per indicare se un'animazione è in corso

		//variabili per il ridimensionamento dell'interfaccia utente
		private float fattScalaX = 1.0f; //fattore di scala orizzontale
		private float fattScalaY = 1.0f; //e verticale
		private Size dimOrigin; //dimensioni originali form
		public Form1()
		{
			InitializeComponent();
			ConfiguraInterfaccia();
			logger = new(); //crea una nuova istanza di FormLogger
			logger.Show(); //mostra il form di log
			dimOrigin = this.Size; //salva le dimensioni originali del form per il calcolo del ridimensionamento

			this.Resize += Form1_Resize; //gestore eventi per il ridimensionamento del form

			//gestori eventi per i pulsanti
			btnGenera.Click += BtnGenera_Click;
			btnNascondi.Click += BtnNascondi_Click;
			btnInterrompi.Click += BtnInterrompi_Click;
		}
		private void Form1_Resize(object sender, EventArgs e) //gestore eventi per ridimensionamento form
		{
			//se le dimensioni originali non sono state impostate
			if (dimOrigin.Width == 0 || dimOrigin.Height == 0)
				return; //ritorna

			//fattori di scala basati su dimensioni originali e correnti del form
			fattScalaX = (float)this.Width / dimOrigin.Width;
			fattScalaY = (float)this.Height / dimOrigin.Height;

			//ridimensiona i contenitori in base ai nuovi fattori di scala
			RidimensionaContenitori();
		}
		private void RidimensionaContenitori() //metodo per ridimensionare i contenitori
		{
			//ridimensiona e riposiziona il contenitore della fabbrica
			if (contenitoreFabbrica != null)
			{
				contenitoreFabbrica.Location = new Point((int)(20 * fattScalaX), (int)(60 * fattScalaY));
				contenitoreFabbrica.Size = new Size((int)(840 * fattScalaX), (int)(160 * fattScalaY));
			}
			if (contenitorePrato != null) //e del prato
			{
				contenitorePrato.Location = new Point((int)(20 * fattScalaX), (int)(260 * fattScalaY));
				contenitorePrato.Size = new Size((int)(840 * fattScalaX), (int)(160 * fattScalaY));
			}
			AggiornaInterfaccia(); //aggiorna l'interfaccia per mostrare i cambiamenti
		}
		private void ConfiguraInterfaccia() //metodo per configurare l'interfaccia utente
		{
			this.Text = "Anselmo's Lawn";
			this.FormBorderStyle = FormBorderStyle.Sizable; //form ridimensionabile
			this.MinimumSize = new Size(900, 580); //dimensioni minime

			tlpControlli.BackColor = Color.Transparent; //imposta il colore del TLP come trasparente
			ConfiguraMenu(); //configura il menu principale

			contenitoreFabbrica = new ContenitoreUova() //contenitore per le uova nella fabbrica
			{
				Location = new Point(20, 60),
				Size = new Size(840, 160),
				BackColor = Color.Transparent,
				BorderStyle = BorderStyle.None
			};
			contenitorePrato = new ContenitoreUova() //contenitore per le uova nel prato
			{
				Location = new Point(20, 260),
				Size = new Size(840, 160),
				BackColor = Color.Transparent,
				BorderStyle = BorderStyle.None
			};
			//aggiunge i contenitori al form
			this.Controls.Add(contenitoreFabbrica);
			this.Controls.Add(contenitorePrato);
		}
		private void ConfiguraMenu() //metodo per configurare il menu principale
		{
			menuStrip = new MenuStrip(); //crea la barra del menu
			menuStrip.BackColor = Color.LightYellow;
			menuStrip.Font = new Font(Font.FontFamily, 10);
			menuStrip.Dock = DockStyle.Top;
			menuImpostazioni = new ToolStripMenuItem("Settings"); //crea il menu impostazioni
			menuVelocita = new ToolStripMenuItem("Speed"); //il sottomenu velocità
			menuMusica = new ToolStripMenuItem("Music");
			menuCrediti = new ToolStripMenuItem("Credits");
			playSound = new ToolStripMenuItem("Play");
			stopSound = new ToolStripMenuItem("Stop");
			credits = new ToolStripMenuItem("Game by Manuel Dalla Santa - 4AII");

			playSound.Image = Image.FromFile("images/play.png");
			stopSound.Image = Image.FromFile("images/stop.png");
			player = new SoundPlayer("EFX/music.wav");

			comboVelocita = new ToolStripComboBox(); //e la combobox per selezionare la velocità 
			comboVelocita.DropDownStyle = ComboBoxStyle.DropDownList;
			comboVelocita.Width = 120;

			for (int i = 1; i <= 10; i++) //aggiunge le velocità da 1 a 10
			{
				comboVelocita.Items.Add(i.ToString());
			}
			comboVelocita.SelectedIndex = 4; //imposta valore predefinito a 5 (parte da 0)
			menuVelocita.DropDownItems.Add(comboVelocita); //aggiunge la combobox al sottomenu velocità
			menuImpostazioni.DropDownItems.Add(menuVelocita); //aggiunge il sottomenu velocità al menu impostazioni
			menuStrip.Items.Add(menuImpostazioni); //aggiunge il menu impostazioni alla barra del menu
			menuStrip.Items.Add(menuMusica); //aggiunge il menu musica
			menuStrip.Items.Add(menuCrediti); //aggiunge i crediti
			menuMusica.DropDownItems.Add(playSound); //aggiunge l'opzione "play" al menu musica
			menuMusica.DropDownItems.Add(stopSound); //aggiunge l'opzione "stop" al menu musica
			menuCrediti.DropDownItems.Add(credits); //aggiunge la descrizione dei crediti
			this.Controls.Add(menuStrip); //aggiunge la barra del menu al form
			this.MainMenuStrip = menuStrip;

			playSound.Click += (sender, e) => PlayAudio();
			stopSound.Click += (sender, e) => StopAudio();
		}
		private void PlayAudio()
		{
			player.PlayLooping(); //riproduce l'audio in loop
			logger.AggiungiMessaggio("Audio in riproduzione");
		}
		private void StopAudio()
		{
			player.Stop(); //ferma l'audio
			logger.AggiungiMessaggio("Audio fermato");
		}
		private void BtnGenera_Click(object sender, EventArgs e) //gestore eventi per il pulsante che genera uova
		{
			//resetta code e liste
			fabbrica.Clear();
			prato.Clear();
			pratoVisualizzazione.Clear();
			AggiornaInterfaccia();

			int numeroUova = (int)numUova.Value; //ottiene il numero da generare dal controllo numerico
			int totaleMetaUova = numeroUova * 2; //calcola il numero totale di metà uova
			int metaPerColore = totaleMetaUova / ColoriDisponibili.Length; //calcola quante metà di ogni colore utilizzare
			int metaExtra = totaleMetaUova % ColoriDisponibili.Length; //metà in eccesso da distribuire
			List<Color> tutteLeMetaColori = new List<Color>(); //lista con tutte le metà di colori

			for (int i = 0; i < ColoriDisponibili.Length; i++) //distribuisce i colori in modo bilanciato
			{
				for (int j = 0; j < metaPerColore; j++) //aggiunge il numero standard di metà per ogni colore
				{
					tutteLeMetaColori.Add(ColoriDisponibili[i]);
				}
				if (i < metaExtra) //se ci sono metà extra, ne aggiunge una in più per i primi metaExtra colori
					tutteLeMetaColori.Add(ColoriDisponibili[i]);
			}
			int randInd;
			Color temp;
			for (int i = 0; i < tutteLeMetaColori.Count; i++) //mescola casualmente la lista dei colori
			{
				randInd = random.Next(tutteLeMetaColori.Count);
				temp = tutteLeMetaColori[i];
				tutteLeMetaColori[i] = tutteLeMetaColori[randInd];
				tutteLeMetaColori[randInd] = temp;
			}

			Color colore1;
			Color colore2;
			for (int i = 0; i < numeroUova; i++) //crea le uova prendendo due colori dalla lista mescolata
			{
				colore1 = tutteLeMetaColori[i * 2]; //prima metà dell'uovo
				colore2 = tutteLeMetaColori[i * 2 + 1]; //e seconda
				Uovo uovo = new Uovo(colore1, colore2); //crea un nuovo uovo con i due colori 
				fabbrica.Enqueue(uovo); //lo aggiunge alla fabbrica
			}
			AggiornaInterfaccia(); //aggiorna l'interfaccia per mostrare le uova generate
			logger.AggiungiMessaggio("Uova generate con successo");
		}
		private async void BtnNascondi_Click(object sender, EventArgs e) //gestore eventi async per pulsante nascondi uova
		{
			if (fabbrica.Count == 0)
			{
				MessageBox.Show("Non ci sono uova da nascondere!");
				return;
			}
			if (animazioneInCorso)
			{
				MessageBox.Show("Animazione già in corso, attendere...");
				return;
			}
			//imposta i flag e gli stati dei pulsanti per l'inizio dell'animazione
			interrompiProcesso = false;
			animazioneInCorso = true;
			btnNascondi.Enabled = false; //disabilita il pulsante durante l'animazione
			btnGenera.Enabled = false; //disabilita il pulsante durante l'animazione
			btnInterrompi.Enabled = true; //abilita il pulsante di interruzione

			bool successo = await Task.Run(() => TrovaSoluzione()); //avvia processo di soluzione in un Task separato

			//reimposta i flag e gli stati dei pulsanti al termine dell'animazione
			animazioneInCorso = false;
			btnNascondi.Enabled = true;
			btnGenera.Enabled = true;
			btnInterrompi.Enabled = false;

			if (interrompiProcesso) //se il processo viene interrotto
			{
				logger.AggiungiMessaggio("Processo interrotto");
			}
			else if (successo) //se è stata trovata una soluzione completa
			{
				logger.AggiungiMessaggio("Soluzione completata");
				MessageBox.Show("Soluzione completata", "Fine Simulazione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				this.Close(); //chiude il form
			}
			else //non è stata trovata una soluzione completa
			{
				// Verifica se alcune uova sono state posizionate nel prato
				if (prato.Count > 0)
				{
					logger.AggiungiMessaggio($"Soluzione parziale trovata - {prato.Count} uova nel prato");
					MessageBox.Show($"Soluzione parziale trovata: {prato.Count} uova nel prato",
						"Fine Simulazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					logger.AggiungiMessaggio("Nessuna soluzione trovata");
					MessageBox.Show("Nessuna soluzione trovata", "Fine Simulazione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				this.Close(); //chiude il form
			}
		}
		private void BtnInterrompi_Click(object sender, EventArgs e) //gestore eventi per pulsante interrompi
		{
			interrompiProcesso = true; //flag per interrompere il processo in corso
		}
		private bool TrovaSoluzione() //metodo per avviare la ricerca di una soluzione
		{
			Invoke(new Action(() => //inizializza il prato vuoto
			{
				prato.Clear();
				pratoVisualizzazione.Clear();
				AggiornaInterfaccia();
				logger.AggiungiMessaggio("Iniziata ricerca soluzione");
			}));

			List<Uovo> uovaOriginali = new List<Uovo>(fabbrica); //crea una copia delle uova originali per non modificarle
			fabbrica.Clear();

			foreach (Uovo uovo in uovaOriginali) //rimette le uova nella fabbrica
			{
				fabbrica.Enqueue(uovo);
			}
			return BacktrackingNascondiUova(); //avvia l'algoritmo di backtracking per trovare una soluzione
		}

		private bool BacktrackingNascondiUova() //metodo ricorsivo di backtracking per trovare una soluzione
		{
			if (interrompiProcesso) //se il processo è stato interrotto dall'utente
				return false;

			int velocita = 0; //velocità corrente dell'animazione dalla combobox
			Invoke(new Action(() => velocita = int.Parse(comboVelocita.SelectedItem.ToString())));
			int pausa = 1100 - (velocita * 100); //calcola il tempo di pausa (da 100ms a 1000ms)

			if (fabbrica.Count == 0) //caso base: se la fabbrica è vuota 
				return true; //tutte le uova sono state posizionate con successo

			int numeroUovaInFabbrica = fabbrica.Count;
			int maxUovaNelPrato = pratoVisualizzazione.Count; // Memorizza il massimo numero di uova posizionate finora
			Queue<Uovo> pratoMigliore = new Queue<Uovo>(prato); // Memorizza la migliore configurazione del prato
			List<Uovo> pratoVisualizzazioneMigliore = new List<Uovo>(pratoVisualizzazione); // Per la visualizzazione

			for (int i = 0; i < numeroUovaInFabbrica; i++) //esamina tutte le uova nella fabbrica
			{
				if (interrompiProcesso) //controlla nuovamente se il processo è stato interrotto
					return false;

				Uovo uovoCorrente = fabbrica.Dequeue(); //estrae il primo uovo dalla fabbrica
														//salva lo stato corrente per il backtracking se necessario
				Queue<Uovo> pratoBackup = new Queue<Uovo>(prato);
				List<Uovo> pratoVisualizzazioneBackup = new List<Uovo>(pratoVisualizzazione);

				//verifica se l'uovo può essere messo nel prato
				bool puoEssereNascosto = pratoVisualizzazione.Count == 0 || uovoCorrente.CondivideColore(pratoVisualizzazione.Last());

				MostraTentativo(uovoCorrente, puoEssereNascosto); //mostra il tentativo nell'interfaccia
				Thread.Sleep(pausa); //pausa per visualizzare l'animazione

				if (puoEssereNascosto) //se l'uovo può essere messo nel prato
				{
					prato.Enqueue(uovoCorrente); //lo aggiunge
					pratoVisualizzazione.Add(uovoCorrente);

					MostraSpostamento(uovoCorrente, versoPrato: true); //mostra lo spostamento nell'interfaccia
					Thread.Sleep(pausa); //pausa per visualizzare l'animazione

					if (BacktrackingNascondiUova()) //ricorsione: prova a continuare con le uova rimanenti
						return true; //soluzione trovata

					// Controlla se questa configurazione parziale è migliore di quella precedente
					if (pratoVisualizzazione.Count > maxUovaNelPrato)
					{
						maxUovaNelPrato = pratoVisualizzazione.Count;
						pratoMigliore = new Queue<Uovo>(prato);
						pratoVisualizzazioneMigliore = new List<Uovo>(pratoVisualizzazione);
					}

					prato = pratoBackup; //backtracking: se la soluzione non è stata trovata, annulla l'ultima mossa
					pratoVisualizzazione = pratoVisualizzazioneBackup;

					fabbrica.Enqueue(uovoCorrente); //rimette l'uovo alla fine della fabbrica

					MostraSpostamento(uovoCorrente, versoPrato: false); //mostra il backtracking nell'interfaccia
					Thread.Sleep(pausa);
				}
				else //se l'uovo non può essere messo nel prato
				{
					fabbrica.Enqueue(uovoCorrente); //lo rimette alla fine della fabbrica
					MostraRimessaInFabbrica(uovoCorrente); //mostra che l'uovo viene rimesso in fabbrica
					Thread.Sleep(pausa);
				}
			}
			if (maxUovaNelPrato > 0) //se non è stata trovata una soluzione completa, ma abbiamo uova nel prato
			{
				Invoke(new Action(() =>
				{
					logger.AggiungiMessaggio($"Configurazione ottimale con {maxUovaNelPrato} uova nel prato");
				}));
				prato = pratoMigliore; //manteniamo la configurazione migliore trovata
				pratoVisualizzazione = pratoVisualizzazioneMigliore;

				Invoke(new Action(() => AggiornaInterfaccia())); //aggiorna interfaccia per mostrare configurazione migliore
				Thread.Sleep(pausa);
			}

			return false; //non è stata trovata una soluzione completa
		}
		private void MostraTentativo(Uovo uovo, bool puoEssereNascosto) //metodo per mostrare un tentativo nell'interfaccia
		{
			Invoke(new Action(() =>
			{
				string messaggio = puoEssereNascosto ? $"L'uovo {uovo.Colore1.Name}-{uovo.Colore2.Name} può essere spostato nel prato"
					: $"L'uovo {uovo.Colore1.Name}-{uovo.Colore2.Name} NON può essere spostato nel prato";

				logger.AggiungiMessaggio(messaggio);
				AggiornaInterfaccia();
			}));
		}
		private void MostraSpostamento(Uovo uovo, bool versoPrato) //metodo per mostrare lo spostamento di un uovo
		{
			Invoke(new Action(() =>
			{
				AggiornaInterfaccia();

				if (versoPrato)
					logger.AggiungiMessaggio($"Uovo {uovo.Colore1.Name}-{uovo.Colore2.Name} spostato nel prato");
				else
					logger.AggiungiMessaggio($"Backtracking - Uovo {uovo.Colore1.Name}-{uovo.Colore2.Name} rimesso nella fabbrica");
			}));
		}
		private void MostraRimessaInFabbrica(Uovo uovo) //metodo per mostrare che un uovo viene rimesso in fabbrica
		{
			Invoke(new Action(() =>
			{
				AggiornaInterfaccia();
				logger.AggiungiMessaggio($"Uovo {uovo.Colore1.Name}-{uovo.Colore2.Name} non compatibile, rimesso in fabbrica");
			}));
		}
		private void AggiornaInterfaccia() //metodo per aggiornare l'interfaccia utente
		{
			contenitoreFabbrica.AggiornaUova(fabbrica); //aggiorna il contenitore della fabbrica con le uova correnti
			contenitorePrato.AggiornaUova(prato); //e il contenitore del prato
		}
	}
}