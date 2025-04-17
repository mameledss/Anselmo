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

			// Crea l'istanza del form menu
			FormMenu formMenu = new FormMenu();
			formMenu.Show();

			// Esegui il loop principale dell'applicazione senza specificare un form principale
			Application.Run();
		}
	}
}