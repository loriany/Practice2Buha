using Practice2Buha.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Practice2Buha.Views
{
    /// <summary>
    /// Логика взаимодействия для PeopleControlView.xaml
    /// </summary>
    public partial class PeopleControlView : UserControl
    {
        private AllPeopleViewModel peopleViewModel;
        public PeopleControlView()
        {
            InitializeComponent();
            DataContext = peopleViewModel = new AllPeopleViewModel(GoToPersonControlAdd);
        }

        public void GoToPersonControlAdd()
        {
            Content = new PersonControlView();
        }

        public void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
        }
    }
}
