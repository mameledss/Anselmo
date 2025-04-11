namespace Anselmo
{
	public partial class Form1 : Form
	{
		private Fabbrica fabbrica = new Fabbrica();
		public Form1()
		{
			InitializeComponent();
		}

		private void btnGeneraUova_Click(object sender, EventArgs e)
		{
			int numeroMet� = 20; // esempio: 10 uova = 20 met�
			fabbrica.GeneraUovaDaMeta(numeroMet�);
			AggiornaListaCoda();
		}

		private void AggiornaListaCoda()
		{
			Scivolo.Items.Clear();
			foreach (var uovo in fabbrica.Scivolo)
			{
				lstCodaFabbrica.Items.Add(uovo.ToString());
			}
		}
	}
}
