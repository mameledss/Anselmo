using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoAnselmo
{
	public class UovoControl : Panel
	{
		public Uovo Uovo { get; set; }

		// Variabili per dimensioni originali
		private float largOrigin = 60;
		private float altOrigin = 80;

		public UovoControl(Uovo uovo)
		{
			Uovo = uovo;
			Size = new Size((int)largOrigin, (int)altOrigin);
			DoubleBuffered = true;

			// Make the control transparent
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.BackColor = Color.Transparent;

			Paint += UovoControl_Paint;
		}

		private void UovoControl_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			// Calcola il fattore di scala basato sulle dimensioni correnti
			float scalaX = Width / largOrigin;
			float scalaY = Height / altOrigin;

			// Imposta la trasformazione per scalare il disegno
			g.ScaleTransform(scalaX, scalaY);

			// Disegna la forma dell'uovo
			Rectangle rett = new Rectangle(5, 5, (int)largOrigin - 10, (int)altOrigin - 10);

			using (GraphicsPath path = new GraphicsPath())
			{
				// Crea la forma dell'uovo
				path.AddEllipse(rett);

				// Creiamo due pennelli per le due metà dell'uovo
				using (SolidBrush penn1 = new SolidBrush(Uovo.Colore1))
				using (SolidBrush penn2 = new SolidBrush(Uovo.Colore2))
				{
					// Salva lo stato corrente del graphics
					Region originalClip = g.Clip;

					// Disegna la metà superiore dell'uovo
					Rectangle metaSup = new Rectangle(rett.X, rett.Y, rett.Width, rett.Height / 2);
					g.SetClip(metaSup);
					g.FillPath(penn1, path);

					// Disegna la metà inferiore dell'uovo
					Rectangle metaInf = new Rectangle(rett.X, rett.Y + rett.Height / 2, rett.Width, rett.Height / 2);
					g.SetClip(metaInf);
					g.FillPath(penn2, path);

					// Ripristina il clip originale
					g.Clip = originalClip;

					// Disegna la linea di separazione orizzontale
					using (Pen lineaSepar = new Pen(Color.Black, 2))
					{
						g.DrawLine(lineaSepar,
								  rett.X, rett.Y + rett.Height / 2,
								  rett.X + rett.Width, rett.Y + rett.Height / 2);
					}

					// Disegna il contorno dell'uovo
					using (Pen pen = new Pen(Color.Black, 3))
					{
						g.DrawEllipse(pen, rett);
					}
				}
			}
			// Ripristina la trasformazione
			g.ResetTransform();
		}
	}
}
