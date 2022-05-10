using Practice2Buha.Services;
using Practice2Buha.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;

namespace Practice2Buha.ViewModels
{
    class AllPeopleViewModel
    {

        #region Fields
        private ObservableCollection<PersonViewModel> people;
        private ObservableCollection<PersonViewModel> selectedPeople;
        private PersonViewModel selectedPerson;
        private PersonService personService;
        private Action goToPersonView;
        private RelayCommand<object> add;
        private RelayCommand<object> edit;
        private RelayCommand<object> delete;
        private RelayCommand<object> filter;
        private string wordToFind;
        private string wordToSearch;
        private List<string> allColumns;
        
        #endregion

        #region Property
        public ObservableCollection<PersonViewModel> People
        {
            get { return people; }
            set
            {
                if (people == value) return;
                people = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<PersonViewModel> SelectedPeople
        {
            get { return selectedPeople; }
            set
            {
                selectedPeople = value;
                NotifyPropertyChanged();
            }
        }

        public PersonViewModel MyProperty
        {
            get
            { return selectedPerson; }
            set
            {
                if (selectedPerson == value) return;
                selectedPerson = value;
                NotifyPropertyChanged();
            }
        }
       
        public string WordToFind
        {
            get
            {
                return wordToFind;
            }
            set
            {
                wordToFind = value;
                NotifyPropertyChanged();
            }
        }

        public string WordToSearch
        {
            get
            {
                return wordToSearch;
            }
            set
            {
                wordToSearch = value;
                NotifyPropertyChanged();
            }
        }

        public List<string> AllColumns
        {
            get
            {
                return allColumns;
            }

        }
        #endregion

        #region Constructor
        public AllPeopleViewModel(Action gotoPersonView)
        {
            personService = new PersonService();
            People = new ObservableCollection<PersonViewModel>(personService.GetAllPeople());
            goToPersonView = gotoPersonView;

            selectedPeople = new ObservableCollection<PersonViewModel>();

            allColumns = new List<string>();
            allColumns.Add("Name");
            allColumns.Add("Surname");
            allColumns.Add("Email");
            allColumns.Add("Birthday");
            allColumns.Add("Is adult?");
            allColumns.Add("Western zodiac sign");
            allColumns.Add("Chinese zodiac sign");
            allColumns.Add("Is today birthday?");
        }
        #endregion

        #region RelayCommand
        public RelayCommand<object> AddPersonCommand
        {
            get
            {
                return add ??= new RelayCommand<object>(_ => OpenAdditionWindow());
            }
        }

        public RelayCommand<object> EditPersonCommand
        {
            get
            {
                return edit ??= new RelayCommand<object>(_ => EditPersonAsync());
            }
        }

        public RelayCommand<object> DeletePersonCommand
        {
            get
            {
                return delete ??= new RelayCommand<object>(_ => DeletePerson());
            }
        }

        public RelayCommand<object> FilterCommand
        {
            get
            {
                return filter ??= new RelayCommand<object>(_ => OpenFilterWindow());
            }
        }
        #endregion
        private void OpenAdditionWindow()
        {
            goToPersonView.Invoke();
        }

        private async Task EditPersonAsync()
        {
            if (DateIsCorrect() && CorrectEmail())
            {
                var peopleService = new AllPeopleService();
                bool newPerson = await peopleService.AddNewPersonAsync(selectedPerson);
                MessageBox.Show("Changes set!");
            }
            else
            {
                if (!DateIsCorrect())
                {
                    MessageBox.Show("Changes won't be set until correct date is entered.");
                }
                if (!CorrectEmail())
                {
                    MessageBox.Show("Changes won't be set until correct email is entered.");
                }
            }
        }
        private void DeletePerson()
        {
            personService.Delete(MyProperty);
            People.Remove(MyProperty);
        }

        private void OpenFilterWindow()
        {
            List<PersonViewModel> selectedPerson = new List<PersonViewModel>();
            bool goToDefault = false;
            switch (WordToFind)
            {
                case "Name":
                    var s = from person in people
                            where person.Name.Equals(WordToSearch)
                            select person;
                    selectedPerson = s.ToList();
                    break;
                case "Surname":
                    s = from person in people
                        where person.Surname.Equals(WordToSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                case "Email":
                    s = from person in people
                        where person.Email.Equals(WordToSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                case "Birthday":
                    s = from person in people
                        where person.DayOfBirthToString.Equals(WordToSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                case "Is adult?":
                    s = from person in people
                        where person.IsAdult.Equals(WordToSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                case "Western zodiac sign":
                    s = from person in people
                        where person.SunSign.Equals(WordToSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                case "Chinese zodiac sign":
                    s = from person in people
                        where person.ChineseSign.Equals(WordToSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                case "Is today birthday?":
                    s = from person in people
                        where person.IsBirthday.Equals(WordToSearch)
                        select person;
                    selectedPerson = s.ToList();
                    break;
                default:
                    goToDefault = true;
                    MessageBox.Show("Choose column to be filtered");
                    break;
            }
            SelectedPeople.Clear();
            if (selectedPerson.Count == 0 && !goToDefault)
            {
                MessageBox.Show("Matches not found");
            }
            foreach (PersonViewModel p in selectedPerson)
            {
                SelectedPeople.Add(p);
            }
            NotifyPropertyChanged("SelectedPeople");

        }

        #region Check
       
        public bool DateIsCorrect()
        {
            if (selectedPerson.Birthday > DateTime.Today) return false;
            if (selectedPerson.Age() < 0 || selectedPerson.Age() > 135) return false;
            return true;
        }

        public bool CorrectEmail()
        {
            string email = selectedPerson.Email;
            Regex r = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return r.Match(email).Success;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
