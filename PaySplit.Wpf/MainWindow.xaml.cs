using System.Windows;

namespace PaySplit.Wpf
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DatabaseManagerWindow_Click(object sender, RoutedEventArgs e)
        {
            new DatabaseManagerWindow().Show();
        }

        private void PersonsWindow_Click(object sender, RoutedEventArgs e)
        {
            new ViewPersonsWindow().Show();
        }

        private void AddPersonWindow_Click(object sender, RoutedEventArgs e)
        {
            new AddModifyPersonWindow().Show();
        }

        private void SplitPayWindow_Click(object sender, RoutedEventArgs e)
        {
            new SplitPayWindow().Show();
        }
    }
}