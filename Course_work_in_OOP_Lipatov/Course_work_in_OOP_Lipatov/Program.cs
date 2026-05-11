using System.Text;

namespace Course_work_in_OOP_Lipatov
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            ApplicationConfiguration.Initialize();
            Application.Run(new AuthorForm());
        }
    }
}