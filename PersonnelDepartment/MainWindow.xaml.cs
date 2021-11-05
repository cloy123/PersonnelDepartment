using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PersonnelDepartment
{
    public partial class MainWindow : Window
    {
        bool descending = true;
        Workers workers = new Workers();
        List<ListViewItem> listViewItems = new List<ListViewItem>();
        string fileName = "";

        public MainWindow(string fileName)
        {
            InitializeComponent();
            if(!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                string ext = Path.GetExtension(fileName).ToLower();
                if(ext == ".json")
                {
                    try
                    {
                        this.fileName = fileName;
                        workers = Workers.Load(fileName);
                        CreateListViewItems();
                    }
                    catch
                    {
                        MessageBox.Show("Не получилось открыть файл.", "Ошибка");
                    }
                }
                else
                {
                    MessageBox.Show("Не получилось открыть файл.", "Ошибка");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }
        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        { Save(); }

        private void MenuItemSaveAs_Click(object sender, RoutedEventArgs e)
        { SaveAs(); }

        private void MenuItemCreate_Click(object sender, RoutedEventArgs e)
        { Create(); }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        { Close(); }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        { Open(); }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        { Exit(); }

        private void Exit()
        {
            if (workers.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Сохранить?", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                { Save(); }
            }
        }

        private void Create()
        {
            if(workers.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?.", "Сохранить?", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Save();
                }
            }
            workers = new Workers();
            CreateListViewItems();
            fileName = "";
        }

        private void Open()
        {
            if (workers.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Сохранить?", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                { Save(); }
            }
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "json files (*.json)|*.json";
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.ShowDialog();
            try
            { workers = Workers.Load(fileDialog.FileName); }
            catch
            { MessageBox.Show("Не получилось открыть файл.", "Ошибка"); }
            fileName = fileDialog.FileName;
            CreateListViewItems();
        }

        private void SaveAs()
        {
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog();
            fileDialog.Filter = "json files (*.json)|*.json";
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.ShowDialog();
            Workers.Save(workers, fileDialog.FileName);
            fileName = fileDialog.FileName;
        }

        private void Save()
        {
            if(fileName.Length > 0)
                Workers.Save(workers, fileName);
            else
                SaveAs();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        { SearchWorkerDialog(); }

        private void SearchWorkerDialog()
        {
            if (workers.Count == 0)
            {
                MessageBox.Show("Не введено ни одного работника.", "Ошибка");
            }
            else
            {
                SearchWindow searchWindow = new SearchWindow();
                searchWindow.ShowDialog();
                if (searchWindow.IsOk)
                {
                    ShowWorkerDialog(workers.Search(searchWindow.SeconName));
                }
            }
        }

        private void ShowWorkerDialog(Worker worker)
        {
            if (worker != null)
            {
                ShowWorkerWindow showWorker = new ShowWorkerWindow(worker);
                showWorker.ShowDialog();
                if (showWorker.IsOk)
                {
                    workers.Remove(worker);
                    workers.Add(showWorker.Worker);
                    CreateListViewItems();
                }
            }
            else
            {
                MessageBox.Show("Не найдено ни одного работника с такой фамилией.", "Ошибка");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWorkerWindow addWorker = new AddWorkerWindow();
            addWorker.ShowDialog();
            if (addWorker.IsOk)
            {
                workers.Add(addWorker.worker);
                CreateListViewItems();
            }
        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            workers.SortByName();
            if (descending)
            {
                workers.Reverse();
            }
            descending = !descending;
            CreateListViewItems();
        }

        private void GridViewColumnHeader_Click_1(object sender, RoutedEventArgs e)
        {
            workers.SortByPosition();
            if (descending)
            {
                workers.Reverse();
            }
            descending = !descending;
            CreateListViewItems();
        }

        private void GridViewColumnHeader_Click_2(object sender, RoutedEventArgs e)
        {
            workers.SortByExperience();
            if (descending)
            {
                workers.Reverse();
            }
            descending = !descending;
            CreateListViewItems();
        }

        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowWorkerDialog(workers[ListView.SelectedIndex]);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить этого работника?", "Удалить?", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                workers.RemoveAt(ListView.SelectedIndex);
                CreateListViewItems();
            }
        }

        private void IntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CreateListViewItems();
        }

        private void CreateListViewItems()
        {
            listViewItems = new List<ListViewItem>();
            for (int i = 0; i < workers.Count; i++)
            {
                if (workers[i].Experience < IntegerUpDown.Value)
                {
                    ListViewItem listView = new ListViewItem() { Visibility = Visibility.Hidden, Height = 0 };
                    listViewItems.Add(listView);
                }
                else
                {
                    listViewItems.Add(new ListViewItem() { Content = workers[i].Clone() });
                }
            }
            ListView.ItemsSource = listViewItems;
        }
    }
}