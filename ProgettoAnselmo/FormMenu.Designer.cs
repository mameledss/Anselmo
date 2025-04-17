namespace ProgettoAnselmo
{
	partial class FormMenu
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			tlpBasso = new TableLayoutPanel();
			button1 = new Button();
			tlpTitoloContenitore = new TableLayoutPanel();
			tlpTitolo = new TableLayoutPanel();
			tlpTestoSelect = new TableLayoutPanel();
			tlpBasso.SuspendLayout();
			tlpTitoloContenitore.SuspendLayout();
			SuspendLayout();
			// 
			// tlpBasso
			// 
			tlpBasso.AutoSize = true;
			tlpBasso.ColumnCount = 3;
			tlpBasso.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.2727261F));
			tlpBasso.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.4545441F));
			tlpBasso.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.2727261F));
			tlpBasso.Controls.Add(button1, 1, 0);
			tlpBasso.Location = new Point(0, 325);
			tlpBasso.Name = "tlpBasso";
			tlpBasso.RowCount = 1;
			tlpBasso.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tlpBasso.Size = new Size(802, 125);
			tlpBasso.TabIndex = 0;
			// 
			// button1
			// 
			button1.AutoSize = true;
			button1.BackgroundImage = Properties.Resources.Pulsante;
			button1.BackgroundImageLayout = ImageLayout.Zoom;
			button1.Cursor = Cursors.Hand;
			button1.Dock = DockStyle.Fill;
			button1.Location = new Point(278, 30);
			button1.Margin = new Padding(60, 30, 60, 30);
			button1.Name = "button1";
			button1.Size = new Size(244, 65);
			button1.TabIndex = 0;
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click_1;
			// 
			// tlpTitoloContenitore
			// 
			tlpTitoloContenitore.ColumnCount = 1;
			tlpTitoloContenitore.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tlpTitoloContenitore.Controls.Add(tlpTitolo, 0, 0);
			tlpTitoloContenitore.Controls.Add(tlpTestoSelect, 0, 1);
			tlpTitoloContenitore.Dock = DockStyle.Top;
			tlpTitoloContenitore.Location = new Point(0, 0);
			tlpTitoloContenitore.Name = "tlpTitoloContenitore";
			tlpTitoloContenitore.RowCount = 2;
			tlpTitoloContenitore.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tlpTitoloContenitore.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tlpTitoloContenitore.Size = new Size(800, 320);
			tlpTitoloContenitore.TabIndex = 1;
			// 
			// tlpTitolo
			// 
			tlpTitolo.BackgroundImage = Properties.Resources.Titolo;
			tlpTitolo.BackgroundImageLayout = ImageLayout.Zoom;
			tlpTitolo.ColumnCount = 1;
			tlpTitolo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tlpTitolo.Dock = DockStyle.Fill;
			tlpTitolo.Location = new Point(3, 3);
			tlpTitolo.Name = "tlpTitolo";
			tlpTitolo.RowCount = 1;
			tlpTitolo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tlpTitolo.Size = new Size(794, 154);
			tlpTitolo.TabIndex = 0;
			// 
			// tlpTestoSelect
			// 
			tlpTestoSelect.ColumnCount = 1;
			tlpTestoSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tlpTestoSelect.Dock = DockStyle.Fill;
			tlpTestoSelect.Location = new Point(3, 163);
			tlpTestoSelect.Name = "tlpTestoSelect";
			tlpTestoSelect.RowCount = 2;
			tlpTestoSelect.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tlpTestoSelect.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tlpTestoSelect.Size = new Size(794, 154);
			tlpTestoSelect.TabIndex = 1;
			// 
			// FormMenu
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackgroundImage = Properties.Resources.Sfondo;
			BackgroundImageLayout = ImageLayout.Stretch;
			ClientSize = new Size(800, 450);
			Controls.Add(tlpTitoloContenitore);
			Controls.Add(tlpBasso);
			Name = "FormMenu";
			Text = "FormMenu";
			tlpBasso.ResumeLayout(false);
			tlpBasso.PerformLayout();
			tlpTitoloContenitore.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TableLayoutPanel tlpBasso;
		private TableLayoutPanel tlpTitoloContenitore;
		private TableLayoutPanel tlpTitolo;
		private Button button1;
		private TableLayoutPanel tlpTestoSelect;
	}
}