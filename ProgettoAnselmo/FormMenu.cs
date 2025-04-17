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
	public partial class FormMenu : Form
	{
		public FormMenu()
		{
			InitializeComponent();
			//rende tutti i TLP trasparenti
			tlpBasso.BackColor = Color.Transparent;
			tlpTitolo.BackColor = Color.Transparent;
			tlpTitoloContenitore.BackColor = Color.Transparent;
			//tlpTesto2.BackColor = Color.Transparent;
			tlpTestoSelect.BackColor = Color.Transparent;
		}

		

		private void button1_Click_1(object sender, EventArgs e)
		{
			// Crea una nuova istanza di Form1
			Form1 form1 = new Form1();
			// Chiudi il form menu
			this.Close();
			// Mostra Form1
			form1.Show();
			// Quando Form1 viene chiuso, termina l'applicazione
			form1.FormClosed += (s, args) => Application.Exit();
		}
	}
}
