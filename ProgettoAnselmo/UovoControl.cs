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
		public Uovo Uovo { get; set; } //riferimento all'uovo associato a questo controllo
		private float largOrigin = 60; //larghezza originale uovo in pixel 
		private float altOrigin = 80; //e altezza
		public UovoControl(Uovo uovo)
		{
			Uovo = uovo;
			Size = new Size((int)largOrigin, (int)altOrigin); //dimensioni iniziali basate sulle dimensioni originali
			DoubleBuffered = true; //double-buffering per ridurre sfarfallio durante il ridisegno
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true); //supporto per lo sfondo trasparente
			this.BackColor = Color.Transparent; //colore dello sfondo trasparente
			Paint += UovoControl_Paint; //evento Paint chiamato quando il controllo deve essere ridisegnato
		}
		//evento Paint con logica per disegnare l'uovo
		private void UovoControl_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics; //contesto grafico da utilizzare per il disegno
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //modalità per avere bordi più "smooth"

			float scalaX = Width / largOrigin; //fattore di scala orizzontale in base a dimensioni correnti del controllo
			float scalaY = Height / altOrigin; //e verticale

			g.ScaleTransform(scalaX, scalaY); //applica trasformazione di scala al contesto grafico
			Rectangle rett = new Rectangle(5, 5, (int)largOrigin - 10, (int)altOrigin - 10); //rettangolo che contiene la forma d'uovo, con un margine di 5 pixel

			using (GraphicsPath path = new GraphicsPath()) //crea percorso grafico per disegnare forma dell'uovo
			{
				path.AddEllipse(rett); //aggiunge un'ellisse al percorso grafico

				//crea due pennelli con i colori definiti nell'oggetto Uovo
				using (SolidBrush penn1 = new SolidBrush(Uovo.Colore1))
				using (SolidBrush penn2 = new SolidBrush(Uovo.Colore2))
				{
					//salva lo stato corrente dell'area di ritaglio per poterlo ripristinare in seguito
					Region origClip = g.Clip;
					Rectangle metaSup = new Rectangle(rett.X, rett.Y, rett.Width, rett.Height / 2); //rettangolo per la metà superiore dell'uovo
																									
					g.SetClip(metaSup); //imposta area di ritaglio al rettangolo della metà superiore
					g.FillPath(penn1, path); //riempie il percorso dell'uovo con il primo colore nella metà superiore

					//rettangolo per la metà inferiore
					Rectangle metaInf = new Rectangle(rett.X, rett.Y + rett.Height / 2, rett.Width, rett.Height / 2);
			
					g.SetClip(metaInf); //imposta area di ritaglio al rettangolo della metà inferiore
					g.FillPath(penn2, path); //riempie percorso dell'uovo con il secondo colore nella metà inferiore

					g.Clip = origClip; //ripristina l'area di ritaglio originale

					//pennello per disegnare la linea di separazione tra le metà dell'uovo
					using (Pen lineaSepar = new Pen(Color.Black, 2))
					{
						//disegna una linea orizzontale che separa le due metà dell'uovo
						g.DrawLine(lineaSepar,
								  rett.X, rett.Y + rett.Height / 2,
								  rett.X + rett.Width, rett.Y + rett.Height / 2);
					}
					
					using (Pen pen = new Pen(Color.Black, 3)) //pennello per il contorno dell'uovo
					{
						g.DrawEllipse(pen, rett); //disegna il contorno dell'ellisse che forma l'uovo
					}
				}
			}
			g.ResetTransform(); //ripristina le trasformazioni di scala al loro stato originale
		}
	}
}