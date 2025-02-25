using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace RestMenef
{
    /// <summary>
    /// Логика взаимодействия для Waiter_Window.xaml
    /// </summary>
    /// 


    public partial class Waiter_Window : Window
    {
        List<RestFlow.Dish> dishes;
        RestFlow.Dish? currentDish = null;
        public Waiter_Window()
        {
            InitializeComponent();
            LoadDishes();
            DataContext = new MainViewModel(-1);
        }


        #region MainViewModel
        public class MainViewModel : INotifyPropertyChanged
        {
            internal ObservableCollection<RestFlow.Dish> _currentDish;
            internal ObservableCollection<RestFlow.Dish> CurrentDish
            {
                get => _currentDish;
                set
                {
                    if (_currentDish != null)
                        _currentDish.CollectionChanged -= CurrentDish_CollectionChanged;

                    _currentDish = value;

                    if (_currentDish != null)
                        _currentDish.CollectionChanged += CurrentDish_CollectionChanged;

                    OnPropertyChanged();
                }
            }

            public MainViewModel(int id)
            {
                if (id >= 0)
                {
                    CurrentDish = new ObservableCollection<RestFlow.Dish>();
                    CurrentDish.CollectionChanged += CurrentDish_CollectionChanged;
                    _ = LoadCurrentDishAsync(id);
                }
            }

            private async void CurrentDish_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (_isCollectionUpdating) return;
                
                //логика
            }

            private bool _isCollectionUpdating;

            private async Task LoadCurrentDishAsync(int id)
            {
                try
                {
                    _isCollectionUpdating = true;

                    var currentOrderCompound = DB.Tables.GetOrderCompound(id)
                        .SelectMany(kvp => Enumerable.Repeat(new RestFlow.Dish(kvp.Key), kvp.Value));

                    CurrentDish = new ObservableCollection<RestFlow.Dish>(currentOrderCompound);
                }
                finally
                {
                    _isCollectionUpdating = false;
                }
            }

            private static void AddDish(RestFlow.Dish dish)
            {

            }

            public event PropertyChangedEventHandler? PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private void LoadDishes()
        {
            dishes = DB.Tables.GetDishes().Select(e => new RestFlow.Dish(e)).ToList();
            ComboBox_ChooseDish.ItemsSource = dishes.Select(e => e.Name).ToList();
        }

        private void Button_AddDish_Click(object sender, RoutedEventArgs e)
        {
            if (currentDish != null)
            {
                DataContext = new MainViewModel(currentDish.Id);

            }
            else
            {
                int newId = DB.Tables.GetOrders()[-1].Id + 1;
                DataContext = new MainViewModel(newId);

            }
        }
    }
}
