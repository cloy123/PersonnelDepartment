using System;
using System.Windows;

namespace PersonnelDepartment
{
    public partial class AddWorkerWindow : Window
    {
        public bool IsOk = false;
        public Worker worker;
        public AddWorkerWindow()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if(!Date.SelectedDate.HasValue || Name.Text.Trim() == "" || SecondName.Text.Trim() == "" || Patronymic.Text.Trim() == "" || Position.Text.Trim() == "")
            {
                MessageBox.Show("Ошибка: Не все поля заполнены.");
            }
            else
            {
                IsOk = true;
                worker = new Worker(Name.Text, SecondName.Text, Patronymic.Text, Position.Text, (DateTime)Date.SelectedDate);
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        { Close(); }
    }
}
