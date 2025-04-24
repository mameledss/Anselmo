using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgettoAnselmo
{
	public partial class FormLogger : Form
	{
		private ListBox lstLog; 
		private Button btnClear;
		private Label lblTitolo;
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
				Text = "Pulisci Log",
				Location = new Point(20, 370),
				Size = new Size(120, 30),
				FlatStyle = FlatStyle.Flat,
				BackColor = ColorTranslator.FromHtml("#749951"),
				ForeColor = ColorTranslator.FromHtml("#fffdd0")
			};

			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.Click += (s, e) => lstLog.Items.Clear();

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
			//aggiunge il messaggio alla ListBox
			lstLog.Items.Add($"- {messaggio}");
			//scorre la ListBox verso il basso per mostrare l'ultimo elemento inserito
			lstLog.TopIndex = lstLog.Items.Count - 1;
		}
	}
}
