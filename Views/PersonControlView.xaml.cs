using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Practice2Buha.ViewModels;


namespace Practice2Buha.Views
{
    /// <summary>
    /// Логика взаимодействия для PersonControlView.xaml
    /// </summary>
    public partial class PersonControlView : UserControl
    {
        private PersonViewModel personViewModel;
        public PersonControlView()
        {
            InitializeComponent();
            DataContext = personViewModel = new PersonViewModel();
        }
    }
}
