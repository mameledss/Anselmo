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

			// Stile generale
			this.Text = "Logger";
			this.Width = 550;
			this.Height = 450;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.BackColor = Color.FromArgb(30, 30, 30);
			this.Font = new Font("Consolas", 10);
			this.ForeColor = Color.Black;

			lblTitolo = new Label
			{
				Text = "Registro Attività",
				Font = new Font("Segoe UI", 14, FontStyle.Bold),
				ForeColor = Color.White,
				AutoSize = true,
				Location = new Point(20, 15)
			};

			lstLog = new ListBox
			{
				Location = new Point(20, 50),
				Size = new Size(490, 300),
				BackColor = Color.FromArgb(45, 45, 45),
				ForeColor = Color.White,
				BorderStyle = BorderStyle.FixedSingle
			};

			btnClear = new Button
			{
				Text = "Pulisci Log",
				Location = new Point(20, 370),
				Size = new Size(120, 30),
				FlatStyle = FlatStyle.Flat,
				BackColor = Color.FromArgb(60, 60, 60),
				ForeColor = Color.White
			};

			btnClear.FlatAppearance.BorderSize = 0;
			btnClear.Click += (s, e) => lstLog.Items.Clear();

			this.Controls.Add(lblTitolo);
			this.Controls.Add(lstLog);
			this.Controls.Add(btnClear);
		}

		public void AggiungiMessaggio(string messaggio)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => AggiungiMessaggio(messaggio)));
				return;
			}
			lstLog.Items.Add($"- {messaggio}");
			lstLog.TopIndex = lstLog.Items.Count - 1;
		}
	}
}
