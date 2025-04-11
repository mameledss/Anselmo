using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnselmoConiglioProgetto
{
	public partial class FormPrincipale : Form
	{
		// Definizione dei colori disponibili
		private readonly string[] COLORI = { "Verde", "Azzurro", "Giallo", "Arancione", "Rosa", "Viola" };

		// Code per le uova
		private Queue<Uovo> fabbrica = new Queue<Uovo>();
		private Queue<Uovo> pratoDiUova = new Queue<Uovo>();
		private Queue<Uovo> uovaRimandate = new Queue<Uovo>();

		// Componenti per il backtracking
		private Stack<Queue<Uovo>> storiaUova = new Stack<Queue<Uovo>>();
		private Stack<Uovo> uovaNascoste = new Stack<Uovo>();

		private Random random = new Random();
		private int numeroTotaleUova = 20; // Numero predefinito di uova

		public FormPrincipale()
		{
			InitializeComponent();
		}

		private void FormPrincipale_Load(object sender, EventArgs e)
		{
			// Inizializzazione
			cmbColore1.Items.AddRange(COLORI);
			cmbColore2.Items.AddRange(COLORI);

			// Imposta alcuni valori predefiniti
			numUova.Value = numeroTotaleUova;

			AggiornaInterfaccia();
		}

		private void btnGeneraUova_Click(object sender, EventArgs e)
		{
			// Pulisci tutte le code e gli stack
			fabbrica.Clear();
			pratoDiUova.Clear();
			uovaRimandate.Clear();
			storiaUova.Clear();
			uovaNascoste.Clear();

			// Ottieni il numero di uova desiderato
			numeroTotaleUova = (int)numUova.Value;

			// Crea le metà delle uova (ogni uovo ha due metà di colore)
			List<string> metaUova = new List<string>();
			for (int i = 0; i < numeroTotaleUova * 2; i++)
			{
				metaUova.Add(COLORI[random.Next(COLORI.Length)]);
			}

			// Mescola le metà e crea le uova
			metaUova = metaUova.OrderBy(x => random.Next()).ToList();

			for (int i = 0; i < metaUova.Count; i += 2)
			{
				if (i + 1 < metaUova.Count)
				{
					Uovo uovo = new Uovo(metaUova[i], metaUova[i + 1]);
					fabbrica.Enqueue(uovo);
				}
			}

			AggiornaInterfaccia();
			logTextBox.AppendText($"Generate {fabbrica.Count} uova nella fabbrica.\r\n");
		}

		private void btnPrendiUovo_Click(object sender, EventArgs e)
		{
			if (fabbrica.Count == 0 && uovaRimandate.Count == 0)
			{
				logTextBox.AppendText("Non ci sono più uova da prendere!\r\n");
				return;
			}

			// Prendi un uovo dalla fabbrica o dalle uova rimandate
			Uovo uovoCorrente;
			if (fabbrica.Count > 0)
			{
				uovoCorrente = fabbrica.Dequeue();
				logTextBox.AppendText($"Anselmo ha preso un uovo dalla fabbrica: {uovoCorrente}\r\n");
			}
			else
			{
				uovoCorrente = uovaRimandate.Dequeue();
				logTextBox.AppendText($"Anselmo ha preso un uovo rimandato: {uovoCorrente}\r\n");
			}

			// Mostra l'uovo corrente nei controlli
			txtUovoCorrente.Text = uovoCorrente.ToString();
			cmbColore1.SelectedItem = uovoCorrente.Colore1;
			cmbColore2.SelectedItem = uovoCorrente.Colore2;

			AggiornaInterfaccia();
		}

		private void btnNascondi_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtUovoCorrente.Text))
			{
				logTextBox.AppendText("Non hai ancora preso un uovo!\r\n");
				return;
			}

			// Crea l'uovo dai colori selezionati
			Uovo uovoCorrente = new Uovo(cmbColore1.SelectedItem.ToString(), cmbColore2.SelectedItem.ToString());

			// Controlla se è possibile nascondere l'uovo
			if (pratoDiUova.Count == 0 || HaColoreInComune(pratoDiUova.Last(), uovoCorrente))
			{
				// Prima di nascondere l'uovo, salva lo stato attuale per il backtracking
				Queue<Uovo> statoAttuale = new Queue<Uovo>(pratoDiUova);
				storiaUova.Push(statoAttuale);

				// Nascondi l'uovo
				pratoDiUova.Enqueue(uovoCorrente);
				uovaNascoste.Push(uovoCorrente);

				logTextBox.AppendText($"Nascosto nel prato: {uovoCorrente}\r\n");
				txtUovoCorrente.Text = "";

				AggiornaInterfaccia();
			}
			else
			{
				// Rimanda l'uovo alla fabbrica
				uovaRimandate.Enqueue(uovoCorrente);
				logTextBox.AppendText($"L'uovo {uovoCorrente} non ha colori in comune con l'ultimo - rimandato!\r\n");
				txtUovoCorrente.Text = "";

				AggiornaInterfaccia();
			}
		}

		private void btnBacktrack_Click(object sender, EventArgs e)
		{
			if (uovaNascoste.Count == 0)
			{
				logTextBox.AppendText("Non ci sono uova da rimuovere per il backtracking.\r\n");
				return;
			}

			// Rimuovi l'ultimo uovo nascosto
			Uovo rimosso = uovaNascoste.Pop();

			// Ripristina lo stato precedente del prato
			if (storiaUova.Count > 0)
			{
				pratoDiUova = storiaUova.Pop();
			}
			else
			{
				pratoDiUova.Clear();
			}

			// Rimetti l'uovo nella coda delle uova rimandate
			uovaRimandate.Enqueue(rimosso);

			logTextBox.AppendText($"Backtracking: rimosso {rimosso} e rimesso in coda.\r\n");

			AggiornaInterfaccia();
		}

		private void btnAutoNascondi_Click(object sender, EventArgs e)
		{
			// Algoritmo automatico per nascondere le uova
			int iterazioni = 0;
			bool progresso = true;

			while (progresso && iterazioni < 100) // Limitiamo le iterazioni per evitare loop infiniti
			{
				progresso = false;
				iterazioni++;

				// Prova a scorrere tutte le uova disponibili
				int totalUova = fabbrica.Count + uovaRimandate.Count;
				for (int i = 0; i < totalUova; i++)
				{
					// Prendi un uovo
					Uovo uovoCorrente;
					if (fabbrica.Count > 0)
					{
						uovoCorrente = fabbrica.Dequeue();
					}
					else if (uovaRimandate.Count > 0)
					{
						uovoCorrente = uovaRimandate.Dequeue();
					}
					else
					{
						break;
					}

					// Controlla se possiamo nasconderlo
					if (pratoDiUova.Count == 0 || HaColoreInComune(pratoDiUova.Last(), uovoCorrente))
					{
						// Salva lo stato per il backtracking
						Queue<Uovo> statoAttuale = new Queue<Uovo>(pratoDiUova);
						storiaUova.Push(statoAttuale);

						// Nascondi l'uovo
						pratoDiUova.Enqueue(uovoCorrente);
						uovaNascoste.Push(uovoCorrente);
						progresso = true;

						logTextBox.AppendText($"Auto: Nascosto nel prato: {uovoCorrente}\r\n");
					}
					else
					{
						// Rimanda l'uovo
						uovaRimandate.Enqueue(uovoCorrente);
					}
				}

				// Se non abbiamo fatto progressi ma ci sono ancora uova, fai backtracking
				if (!progresso && (fabbrica.Count > 0 || uovaRimandate.Count > 0) && uovaNascoste.Count > 0)
				{
					btnBacktrack_Click(sender, e); // Esegui backtracking
					progresso = true; // Continuiamo a provare
				}
			}

			if (iterazioni >= 100)
			{
				logTextBox.AppendText("Raggiunto il limite di iterazioni nell'auto-nascondimento.\r\n");
			}

			AggiornaInterfaccia();
		}

		private bool HaColoreInComune(Uovo ultimo, Uovo nuovo)
		{
			return ultimo.Colore1 == nuovo.Colore1 ||
				   ultimo.Colore1 == nuovo.Colore2 ||
				   ultimo.Colore2 == nuovo.Colore1 ||
				   ultimo.Colore2 == nuovo.Colore2;
		}

		private void AggiornaInterfaccia()
		{
			// Aggiorna le etichette con i conteggi
			lblFabbricaCount.Text = $"Uova in fabbrica: {fabbrica.Count}";
			lblPratoCount.Text = $"Uova nel prato: {pratoDiUova.Count}";
			lblRimandateCount.Text = $"Uova rimandate: {uovaRimandate.Count}";

			// Aggiorna le liste
			lstFabbrica.Items.Clear();
			foreach (Uovo uovo in fabbrica)
			{
				lstFabbrica.Items.Add(uovo.ToString());
			}

			lstPrato.Items.Clear();
			foreach (Uovo uovo in pratoDiUova)
			{
				lstPrato.Items.Add(uovo.ToString());
			}

			lstRimandate.Items.Clear();
			foreach (Uovo uovo in uovaRimandate)
			{
				lstRimandate.Items.Add(uovo.ToString());
			}

			// Abilita/disabilita i pulsanti in base allo stato
			btnPrendiUovo.Enabled = (fabbrica.Count > 0 || uovaRimandate.Count > 0);
			btnNascondi.Enabled = !string.IsNullOrEmpty(txtUovoCorrente.Text);
			btnBacktrack.Enabled = (uovaNascoste.Count > 0);
			btnAutoNascondi.Enabled = (fabbrica.Count > 0 || uovaRimandate.Count > 0);
		}

		private void InitializeComponent()
		{
			this.lblTitolo = new System.Windows.Forms.Label();
			this.numUova = new System.Windows.Forms.NumericUpDown();
			this.lblNumUova = new System.Windows.Forms.Label();
			this.btnGeneraUova = new System.Windows.Forms.Button();
			this.lblFabbricaCount = new System.Windows.Forms.Label();
			this.lstFabbrica = new System.Windows.Forms.ListBox();
			this.lstPrato = new System.Windows.Forms.ListBox();
			this.lblPratoCount = new System.Windows.Forms.Label();
			this.lstRimandate = new System.Windows.Forms.ListBox();
			this.lblRimandateCount = new System.Windows.Forms.Label();
			this.btnPrendiUovo = new System.Windows.Forms.Button();
			this.btnNascondi = new System.Windows.Forms.Button();
			this.btnBacktrack = new System.Windows.Forms.Button();
			this.btnAutoNascondi = new System.Windows.Forms.Button();
			this.txtUovoCorrente = new System.Windows.Forms.TextBox();
			this.lblUovoCorrente = new System.Windows.Forms.Label();
			this.cmbColore1 = new System.Windows.Forms.ComboBox();
			this.cmbColore2 = new System.Windows.Forms.ComboBox();
			this.lblColori = new System.Windows.Forms.Label();
			this.logTextBox = new System.Windows.Forms.TextBox();
			this.lblLog = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numUova)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTitolo
			// 
			this.lblTitolo.AutoSize = true;
			this.lblTitolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitolo.Location = new System.Drawing.Point(12, 9);
			this.lblTitolo.Name = "lblTitolo";
			this.lblTitolo.Size = new System.Drawing.Size(464, 26);
			this.lblTitolo.TabIndex = 0;
			this.lblTitolo.Text = "Anselmo il Coniglio - Nascondere le Uova";
			// 
			// numUova
			// 
			this.numUova.Location = new System.Drawing.Point(151, 51);
			this.numUova.Maximum = new decimal(new int[] {
			50,
			0,
			0,
			0});
			this.numUova.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numUova.Name = "numUova";
			this.numUova.Size = new System.Drawing.Size(87, 20);
			this.numUova.TabIndex = 1;
			this.numUova.Value = new decimal(new int[] {
			20,
			0,
			0,
			0});
			// 
			// lblNumUova
			// 
			this.lblNumUova.AutoSize = true;
			this.lblNumUova.Location = new System.Drawing.Point(16, 53);
			this.lblNumUova.Name = "lblNumUova";
			this.lblNumUova.Size = new System.Drawing.Size(129, 13);
			this.lblNumUova.TabIndex = 2;
			this.lblNumUova.Text = "Numero di uova da creare:";
			// 
			// btnGeneraUova
			// 
			this.btnGeneraUova.Location = new System.Drawing.Point(258, 48);
			this.btnGeneraUova.Name = "btnGeneraUova";
			this.btnGeneraUova.Size = new System.Drawing.Size(129, 23);
			this.btnGeneraUova.TabIndex = 3;
			this.btnGeneraUova.Text = "Genera Uova";
			this.btnGeneraUova.UseVisualStyleBackColor = true;
			this.btnGeneraUova.Click += new System.EventHandler(this.btnGeneraUova_Click);
			// 
			// lblFabbricaCount
			// 
			this.lblFabbricaCount.AutoSize = true;
			this.lblFabbricaCount.Location = new System.Drawing.Point(16, 92);
			this.lblFabbricaCount.Name = "lblFabbricaCount";
			this.lblFabbricaCount.Size = new System.Drawing.Size(93, 13);
			this.lblFabbricaCount.TabIndex = 4;
			this.lblFabbricaCount.Text = "Uova in fabbrica: 0";
			// 
			// lstFabbrica
			// 
			this.lstFabbrica.FormattingEnabled = true;
			this.lstFabbrica.Location = new System.Drawing.Point(19, 108);
			this.lstFabbrica.Name = "lstFabbrica";
			this.lstFabbrica.Size = new System.Drawing.Size(150, 173);
			this.lstFabbrica.TabIndex = 5;
			// 
			// lstPrato
			// 
			this.lstPrato.FormattingEnabled = true;
			this.lstPrato.Location = new System.Drawing.Point(184, 108);
			this.lstPrato.Name = "lstPrato";
			this.lstPrato.Size = new System.Drawing.Size(150, 173);
			this.lstPrato.TabIndex = 7;
			// 
			// lblPratoCount
			// 
			this.lblPratoCount.AutoSize = true;
			this.lblPratoCount.Location = new System.Drawing.Point(181, 92);
			this.lblPratoCount.Name = "lblPratoCount";
			this.lblPratoCount.Size = new System.Drawing.Size(81, 13);
			this.lblPratoCount.TabIndex = 6;
			this.lblPratoCount.Text = "Uova nel prato: 0";
			// 
			// lstRimandate
			// 
			this.lstRimandate.FormattingEnabled = true;
			this.lstRimandate.Location = new System.Drawing.Point(349, 108);
			this.lstRimandate.Name = "lstRimandate";
			this.lstRimandate.Size = new System.Drawing.Size(150, 173);
			this.lstRimandate.TabIndex = 9;
			// 
			// lblRimandateCount
			// 
			this.lblRimandateCount.AutoSize = true;
			this.lblRimandateCount.Location = new System.Drawing.Point(346, 92);
			this.lblRimandateCount.Name = "lblRimandateCount";
			this.lblRimandateCount.Size = new System.Drawing.Size(90, 13);
			this.lblRimandateCount.TabIndex = 8;
			this.lblRimandateCount.Text = "Uova rimandate: 0";
			// 
			// btnPrendiUovo
			// 
			this.btnPrendiUovo.Location = new System.Drawing.Point(21, 309);
			this.btnPrendiUovo.Name = "btnPrendiUovo";
			this.btnPrendiUovo.Size = new System.Drawing.Size(143, 23);
			this.btnPrendiUovo.TabIndex = 10;
			this.btnPrendiUovo.Text = "Prendi un Uovo";
			this.btnPrendiUovo.UseVisualStyleBackColor = true;
			this.btnPrendiUovo.Click += new System.EventHandler(this.btnPrendiUovo_Click);
			// 
			// btnNascondi
			// 
			this.btnNascondi.Location = new System.Drawing.Point(185, 309);
			this.btnNascondi.Name = "btnNascondi";
			this.btnNascondi.Size = new System.Drawing.Size(148, 23);
			this.btnNascondi.TabIndex = 11;
			this.btnNascondi.Text = "Nascondi l\'Uovo";
			this.btnNascondi.UseVisualStyleBackColor = true;
			this.btnNascondi.Click += new System.EventHandler(this.btnNascondi_Click);
			// 
			// btnBacktrack
			// 
			this.btnBacktrack.Location = new System.Drawing.Point(356, 309);
			this.btnBacktrack.Name = "btnBacktrack";
			this.btnBacktrack.Size = new System.Drawing.Size(143, 23);
			this.btnBacktrack.TabIndex = 12;
			this.btnBacktrack.Text = "Backtrack";
			this.btnBacktrack.UseVisualStyleBackColor = true;
			this.btnBacktrack.Click += new System.EventHandler(this.btnBacktrack_Click);
			// 
			// btnAutoNascondi
			// 
			this.btnAutoNascondi.Location = new System.Drawing.Point(19, 388);
			this.btnAutoNascondi.Name = "btnAutoNascondi";
			this.btnAutoNascondi.Size = new System.Drawing.Size(480, 32);
			this.btnAutoNascondi.TabIndex = 13;
			this.btnAutoNascondi.Text = "Nascondi Automaticamente";
			this.btnAutoNascondi.UseVisualStyleBackColor = true;
			this.btnAutoNascondi.Click += new System.EventHandler(this.btnAutoNascondi_Click);
			// 
			// txtUovoCorrente
			// 
			this.txtUovoCorrente.Location = new System.Drawing.Point(74, 342);
			this.txtUovoCorrente.Name = "txtUovoCorrente";
			this.txtUovoCorrente.ReadOnly = true;
			this.txtUovoCorrente.Size = new System.Drawing.Size(90, 20);
			this.txtUovoCorrente.TabIndex = 14;
			// 
			// lblUovoCorrente
			// 
			this.lblUovoCorrente.AutoSize = true;
			this.lblUovoCorrente.Location = new System.Drawing.Point(16, 345);
			this.lblUovoCorrente.Name = "lblUovoCorrente";
			this.lblUovoCorrente.Size = new System.Drawing.Size(52, 13);
			this.lblUovoCorrente.TabIndex = 15;
			this.lblUovoCorrente.Text = "In mano:";
			// 
			// cmbColore1
			// 
			this.cmbColore1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbColore1.FormattingEnabled = true;
			this.cmbColore1.Location = new System.Drawing.Point(219, 342);
			this.cmbColore1.Name = "cmbColore1";
			this.cmbColore1.Size = new System.Drawing.Size(91, 21);
			this.cmbColore1.TabIndex = 16;
			// 
			// cmbColore2
			// 
			this.cmbColore2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbColore2.FormattingEnabled = true;
			this.cmbColore2.Location = new System.Drawing.Point(316, 342);
			this.cmbColore2.Name = "cmbColore2";
			this.cmbColore2.Size = new System.Drawing.Size(91, 21);
			this.cmbColore2.TabIndex = 17;
			// 
			// lblColori
			// 
			this.lblColori.AutoSize = true;
			this.lblColori.Location = new System.Drawing.Point(177, 345);
			this.lblColori.Name = "lblColori";
			this.lblColori.Size = new System.Drawing.Size(36, 13);
			this.lblColori.TabIndex = 18;
			this.lblColori.Text = "Colori:";
			// 
			// logTextBox
			// 
			this.logTextBox.Location = new System.Drawing.Point(523, 53);
			this.logTextBox.Multiline = true;
			this.logTextBox.Name = "logTextBox";
			this.logTextBox.ReadOnly = true;
			this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.logTextBox.Size = new System.Drawing.Size(265, 367);
			this.logTextBox.TabIndex = 19;
			// 
			// lblLog
			// 
			this.lblLog.AutoSize = true;
			this.lblLog.Location = new System.Drawing.Point(520, 37);
			this.lblLog.Name = "lblLog";
			this.lblLog.Size = new System.Drawing.Size(25, 13);
			this.lblLog.TabIndex = 20;
			this.lblLog.Text = "Log";
			// 
			// FormPrincipale
			// 
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.lblLog);
			this.Controls.Add(this.logTextBox);
			this.Controls.Add(this.lblColori);
			this.Controls.Add(this.cmbColore2);
			this.Controls.Add(this.cmbColore1);
			this.Controls.Add(this.lblUovoCorrente);
			this.Controls.Add(this.txtUovoCorrente);
			this.Controls.Add(this.btnAutoNascondi);
			this.Controls.Add(this.btnBacktrack);
			this.Controls.Add(this.btnNascondi);
			this.Controls.Add(this.btnPrendiUovo);
			this.Controls.Add(this.lstRimandate);
			this.Controls.Add(this.lblRimandateCount);
			this.Controls.Add(this.lstPrato);
			this.Controls.Add(this.lblPratoCount);
			this.Controls.Add(this.lstFabbrica);
			this.Controls.Add(this.lblFabbricaCount);
			this.Controls.Add(this.btnGeneraUova);
			this.Controls.Add(this.lblNumUova);
			this.Controls.Add(this.numUova);
			this.Controls.Add(this.lblTitolo);
			this.Name = "FormPrincipale";
			this.Text = "Anselmo il Coniglio Pasquale";
			this.Load += new System.EventHandler(this.FormPrincipale_Load);
			((System.ComponentModel.ISupportInitialize)(this.numUova)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private System.Windows.Forms.Label lblTitolo;
		private System.Windows.Forms.NumericUpDown numUova;
		private System.Windows.Forms.Label lblNumUova;
		private System.Windows.Forms.Button btnGeneraUova;
		private System.Windows.Forms.Label lblFabbricaCount;
		private System.Windows.Forms.ListBox lstFabbrica;
		private System.Windows.Forms.ListBox lstPrato;
		private System.Windows.Forms.Label lblPratoCount;
		private System.Windows.Forms.ListBox lstRimandate;
		private System.Windows.Forms.Label lblRimandateCount;
		private System.Windows.Forms.Button btnPrendiUovo;
		private System.Windows.Forms.Button btnNascondi;
		private System.Windows.Forms.Button btnBacktrack;
		private System.Windows.Forms.Button btnAutoNascondi;
		private System.Windows.Forms.TextBox txtUovoCorrente;
		private System.Windows.Forms.Label lblUovoCorrente;
		private System.Windows.Forms.ComboBox cmbColore1;
		private System.Windows.Forms.ComboBox cmbColore2;
		private System.Windows.Forms.Label lblColori;
		private System.Windows.Forms.TextBox logTextBox;
		private System.Windows.Forms.Label lblLog;
	}

	public class Uovo
	{
		public string Colore1 { get; private set; }
		public string Colore2 { get; private set; }

		public Uovo(string colore1, string colore2)
		{
			Colore1 = colore1;
			Colore2 = colore2;
		}

		public override string ToString()
		{
			return $"{Colore1}-{Colore2}";
		}
	}

	
}