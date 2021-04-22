using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppApiService.Models;
using WpfAppApiService.Services;

namespace WpfAppApiService
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
 
        private  void Btn_Add(object sender, RoutedEventArgs e)
        {
            AddAsync();
            FillAsync();
            UpdateAsync();
            Reset();

        }
        private void DgPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PutAsync();
            Delete();
        }
        private async Task FillAsync()
        {
            string resp = await RestHelper.GetAll();
            List<Person> result = JsonConvert.DeserializeObject<List<Person>>(resp);
            DgPerson.ItemsSource = result;
        }
        private async Task AddAsync()
        {
          RestHelper.Post(TxtOld.Text,TxtName.Text,TxtSurname.Text);
        }
        private async Task UpdateAsync()
        {
            if (DgPerson.SelectedItem == null) return;
            var _selectedBook = (Person)DgPerson.SelectedItem;
            int Id = _selectedBook.id;
            RestHelper.Update(Id,TxtOld.Text, TxtName.Text, TxtSurname.Text);
        }
        private async Task Reset()
        {
            TxtOld.Text = "";
            TxtName.Text = "";
            TxtSurname.Text = "";
        }
        private async Task PutAsync()
        {
            if (DgPerson.SelectedItem == null) return;
            var _selectedBook = (Person)DgPerson.SelectedItem;
            int Id = _selectedBook.id;
            TxtOld.Text =  _selectedBook.age.ToString();
            TxtName.Text = _selectedBook.personName;
            TxtSurname.Text = _selectedBook.personSurname;
            RestHelper.Put(Id);
        }
        private async Task Delete()
        {
            if (DgPerson.SelectedItem == null) return;
            var _selectedBook = (Person)DgPerson.SelectedItem;
            int Id = _selectedBook.id;
            RestHelper.Del(Id);
        }

    }
}
 