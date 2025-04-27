namespace ProgettoAnselmo
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			tlpControlli = new TableLayoutPanel();
			btnNascondi = new Button();
			btnGenera = new Button();
			btnInterrompi = new Button();
			numUova = new NumericUpDown();
			pictureBox1 = new PictureBox();
			tlpControlli.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numUova).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// tlpControlli
			// 
			tlpControlli.ColumnCount = 5;
			tlpControlli.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tlpControlli.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tlpControlli.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tlpControlli.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tlpControlli.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tlpControlli.Controls.Add(btnNascondi, 3, 0);
			tlpControlli.Controls.Add(btnGenera, 2, 0);
			tlpControlli.Controls.Add(btnInterrompi, 4, 0);
			tlpControlli.Controls.Add(numUova, 1, 0);
			tlpControlli.Controls.Add(pictureBox1, 0, 0);
			tlpControlli.Dock = DockStyle.Bottom;
			tlpControlli.Location = new Point(0, 578);
			tlpControlli.Name = "tlpControlli";
			tlpControlli.RowCount = 1;
			tlpControlli.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tlpControlli.Size = new Size(900, 72);
			tlpControlli.TabIndex = 0;
			// 
			// btnNascondi
			// 
			btnNascondi.BackgroundImage = Properties.Resources.Hide;
			btnNascondi.BackgroundImageLayout = ImageLayout.Zoom;
			btnNascondi.Cursor = Cursors.Hand;
			btnNascondi.Dock = DockStyle.Fill;
			btnNascondi.Location = new Point(554, 14);
			btnNascondi.Margin = new Padding(14);
			btnNascondi.Name = "btnNascondi";
			btnNascondi.Size = new Size(152, 44);
			btnNascondi.TabIndex = 2;
			btnNascondi.UseVisualStyleBackColor = true;
			// 
			// btnGenera
			// 
			btnGenera.BackgroundImage = Properties.Resources.Generate;
			btnGenera.BackgroundImageLayout = ImageLayout.Zoom;
			btnGenera.Cursor = Cursors.Hand;
			btnGenera.Dock = DockStyle.Fill;
			btnGenera.Location = new Point(374, 14);
			btnGenera.Margin = new Padding(14);
			btnGenera.Name = "btnGenera";
			btnGenera.Size = new Size(152, 44);
			btnGenera.TabIndex = 1;
			btnGenera.UseVisualStyleBackColor = true;
			// 
			// btnInterrompi
			// 
			btnInterrompi.BackgroundImage = Properties.Resources.Stop;
			btnInterrompi.BackgroundImageLayout = ImageLayout.Zoom;
			btnInterrompi.Cursor = Cursors.Hand;
			btnInterrompi.Dock = DockStyle.Fill;
			btnInterrompi.Location = new Point(734, 14);
			btnInterrompi.Margin = new Padding(14);
			btnInterrompi.Name = "btnInterrompi";
			btnInterrompi.Size = new Size(152, 44);
			btnInterrompi.TabIndex = 3;
			btnInterrompi.UseVisualStyleBackColor = true;
			// 
			// numUova
			// 
			numUova.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			numUova.Location = new Point(205, 24);
			numUova.Margin = new Padding(25, 3, 25, 3);
			numUova.Name = "numUova";
			numUova.Size = new Size(130, 23);
			numUova.TabIndex = 4;
			numUova.Value = new decimal(new int[] { 5, 0, 0, 0 });
			// 
			// pictureBox1
			// 
			pictureBox1.Dock = DockStyle.Fill;
			pictureBox1.Image = Properties.Resources.noe;
			pictureBox1.Location = new Point(8, 8);
			pictureBox1.Margin = new Padding(8);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(164, 56);
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox1.TabIndex = 5;
			pictureBox1.TabStop = false;
			// 
			// Form1
			// 
			BackColor = Color.White;
			BackgroundImage = Properties.Resources.NastroPrato2;
			BackgroundImageLayout = ImageLayout.Stretch;
			ClientSize = new Size(900, 650);
			Controls.Add(tlpControlli);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "Form1";
			tlpControlli.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)numUova).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}
		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>


		#endregion

		private TableLayoutPanel tlpControlli;
		private Button btnNascondi;
		private Button btnGenera;
		private Button btnInterrompi;
		private NumericUpDown numUova;
		private PictureBox pictureBox1;
	}
}
