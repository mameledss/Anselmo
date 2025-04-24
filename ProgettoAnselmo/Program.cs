namespace ProgettoAnselmo
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize();

			//crea l'istanza del form del menu
			FormMenu formMenu = new FormMenu();
			formMenu.Show(); //lo mostra

			//esegue l'applicazione
			Application.Run();
		}
	}
}