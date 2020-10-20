using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PaySplit.DAL.Sqlite;

namespace PaySplit.Wpf
{
    /// <summary>
    ///     Logika interakcji dla klasy ViewPersonsWindow.xaml
    /// </summary>
    public partial class ViewPersonsWindow
    {
        public ViewPersonsWindow()
        {
            InitializeComponent();
        }

        public override async void EndInit()
        {
            base.EndInit();

            await using var db = new PaySplitSqliteDbContext();
            PersonsDataGrid.ItemsSource = await db.Persons.ToListAsync().ConfigureAwait(true);

            PersonsDataGrid.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateSource();
        }
    }
}