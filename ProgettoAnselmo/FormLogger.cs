using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProgettoAnselmo
{
	public partial class FormLogger : Form
	{
		private ListBox lstLog;
		private Button btnClear;
		private Label lblTitolo;
		private string percorsoLog;
		public FormLogger()
		{
			InitializeComponent();
			Text = "Anselmo's Diary";
			Width = 600;
			Height = 460;
			BackColor = ColorTranslator.FromHtml("#fffdd0");
			Font = new Font(Font.FontFamily, 10);
			ForeColor = Color.Black;
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;

			percorsoLog = "log.txt";

			lblTitolo = new Label
			{
				Text = "Actions",
				Font = new Font("Segoe UI", 14, FontStyle.Bold),
				ForeColor = ColorTranslator.FromHtml("#664f48"),
				AutoSize = true,
				Location = new Point(20, 15)
			};

			lstLog = new ListBox
			{
				Location = new Point(20, 50),
				Size = new Size(540, 310),
				BackColor = ColorTranslator.FromHtml("#e2d2ff"),
				ForeColor = ColorTranslator.FromHtml("#664f48"),
				BorderStyle = BorderStyle.FixedSingle
			};

			btnClear = new Button
			{
				Text = "Clean Log",
				Location = new Point(20, 370),
				Size = new Size(120, 30),
				FlatStyle = FlatStyle.Flat,
				BackColor = ColorTranslator.FromHtml("#749951"),
				ForeColor = ColorTranslator.FromHtml("#fffdd0")
			};

			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.Click += (s, e) =>
			{
				lstLog.Items.Clear();
			};
			this.Controls.Add(lblTitolo);
			this.Controls.Add(lstLog);
			this.Controls.Add(btnClear);
		}

		//metodo per scrivere un messaggio nel logger
		public void AggiungiMessaggio(string messaggio)
		{
			//se il metodo è chiamato da un thread diverso da quello principale dell'interfaccia grafica
			if (InvokeRequired)
			{
				//usa Invoke per rieseguire il metodo sul thread dell'interfaccia grafica
				Invoke(new Action(() => AggiungiMessaggio(messaggio)));
				return;
			}

			string mess = $"- {messaggio}"; //prepara il messaggio formattato

			lstLog.Items.Add(mess); //aggiunge il messaggio alla listbox
			lstLog.TopIndex = lstLog.Items.Count - 1; //scorre la ListBox verso il basso per mostrare l'ultimo elemento inserito

			try //scrive lo stesso messaggio nel file di log
			{
				string messaggioConData = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss} {mess}"; //aggiunge data e ora
				File.AppendAllText(percorsoLog, messaggioConData + Environment.NewLine); //append per aggiungere messaggio a fine file senza sovrascrivere
			}
			catch (Exception ex)
			{
				lstLog.Items.Add($"- ERRORE scrittura su file: {ex.Message}");
				lstLog.TopIndex = lstLog.Items.Count - 1;
			}
		}
	}
}