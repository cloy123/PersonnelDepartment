using System;
using System.Windows;

namespace PersonnelDepartment
{
    public partial class ShowWorkerWindow : Window
    {
        public Worker Worker;
        public bool IsOk = false;

        public ShowWorkerWindow(Worker worker)
        {
            InitializeComponent();
            if (worker != null)
            {
                Worker = worker;
                Date.SelectedDate = worker.EmploymentDate;
                Name.Text = worker.Name;
                SecondName.Text = worker.SecondName;
                Patronymic.Text = worker.Patronymic;
                Position.Text = worker.Position;
                Title = worker.SecondNameAndInitials;
            }
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (!Date.SelectedDate.HasValue || Name.Text.Trim() == "" || SecondName.Text.Trim() == "" || Patronymic.Text.Trim() == "" || Position.Text.Trim() == "")
            {
                MessageBox.Show("Ошибка: Не все поля заполнены.");
            }
            else
            {
                IsOk = true;
                Worker = new Worker(Name.Text, SecondName.Text, Patronymic.Text, Position.Text, (DateTime)Date.SelectedDate);
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        { Close(); }
    }
}
