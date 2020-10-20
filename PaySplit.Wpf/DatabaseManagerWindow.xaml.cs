using System.Threading.Tasks;
using System.Windows;
using PaySplit.DAL.Sqlite;

namespace PaySplit.Wpf
{
    /// <summary>
    ///     Logika interakcji dla klasy DatabaseManagerWindow.xaml
    /// </summary>
    public partial class DatabaseManagerWindow
    {
        public DatabaseManagerWindow()
        {
            InitializeComponent();
        }

        private async void RecreateButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to recreate the db? This cannot be undone.", "Question",
                MessageBoxButton.YesNoCancel);

            if(result != MessageBoxResult.Yes)
                return;

            try
            {
                LockUi(true);
                await RecreateDbAsync().ConfigureAwait(true);
            }
            finally
            {
                LockUi(false);
            }
            MessageBox.Show("Success!");
        }

        private static async Task RecreateDbAsync()
        {
            await using var db = new PaySplitSqliteDbContext();
            await db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            await db.Database.EnsureCreatedAsync().ConfigureAwait(false);
        }

        private void LockUi(bool doLock)
        {
            RecreateButton.IsEnabled = !doLock;
        }
    }
}