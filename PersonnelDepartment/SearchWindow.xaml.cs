using System.Windows;

namespace PersonnelDepartment
{
    public partial class SearchWindow : Window
    {
        public bool IsOk = false;
        public string SeconName = "";
        public SearchWindow()
        { InitializeComponent(); }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            IsOk = true;
            SeconName = text.Text.Trim();
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        { Close();  }
    }
}
