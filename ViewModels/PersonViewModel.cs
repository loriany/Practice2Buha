using Practice2Buha.Models;
using Practice2Buha.Tools;
using Practice2Buha.Exceptions;
using Practice2Buha.Services;
using System.Text.RegularExpressions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace Practice2Buha.ViewModels
{
    class PersonViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person person = new Person(null, null, null);
        private RelayCommand<object> proceed;
        private Action goToPeople;
        private bool isEnabled = true;
        #endregion


        #region Properties

        public PersonViewModel(Action gotoAllPeople)
        {
            goToPeople = gotoAllPeople;
            Guid = Guid.NewGuid();
        }

        public PersonViewModel()
        {
            Guid = Guid.NewGuid();
        }

        public string Name
        {
            get
            {
                if (DateIsCorrect()) { return person.Name; }
                return " ";
            }
            set { person.Name = value; }
        }

        public Guid Guid { get; set; }

        public string Surname
        {
            get
            {
                if (DateIsCorrect()) { return person.Surname; }
                return " ";
            }
            set { person.Surname = value; }
        }

        public string Email
        {
            get
            {
                if (DateIsCorrect()) { return person.Email; }
                return " ";
            }
            set {
                try
                {
                    person.Email = value;
                }
                catch (EmailException ex)
                {
                    MessageBox.Show(ex.Message + $"\nInvalid value: {ex.Value}");
                }
            }
        }

        public DateTime Birthday
        {
            get
            { return person.Birthday; }

            set
            {
                if (person.Birthday != value)
                {
                    try
                    {
                        person.Birthday = value;
                        Task.Run(async () => await setAsyncData());
                    }
                    catch (DateException ex)
                    {
                        MessageBox.Show(ex.Message + $"\nInvalid value: {ex.Value.ToString("d")}");
                    }
                    NotifyPropertyChanged("IsAdult");
                    NotifyPropertyChanged("SunSign");
                    NotifyPropertyChanged("ChineseSign");
                    NotifyPropertyChanged("IsBirthday");
                }
            }
        }

         public string IsAdult
         {
            get
            {
                if (DateIsCorrect()) 
                {
                    if (person.IsAdult)
                        return "Yes";
                    return "No";
                }
                return " ";
            }
        }
        

        public string SunSign
        {
            get
            {
                if (DateIsCorrect()) { return person.SunSign; }
                return " ";
            }
        }

        public string ChineseSign
        {
            get
            {
                if (DateIsCorrect()) { return person.ChineseSign; }
                return " ";
            }
        }

        public string IsBirthday
        {
            get
            {
                if (DateIsCorrect())
                {
                    if (person.IsBirthday)
                        return "Yes";
                    return "No";
                }
                return " ";
            }
        }

        public string DayOfBirthToString
        {
            get
            {
                if (DateIsCorrect()) { return Birthday.ToString("d"); }
                return " ";
            }
        }

        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public bool DateIsCorrect()
        {
            if (Birthday > DateTime.Today) return false;
            if (person.Age() < 0 || person.Age() > 135) return false;
            return true;
        }
        public bool CorrectEmail(string email)
        {
            Regex r = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return r.Match(email).Success;
        }
        private async Task setAsyncData()
        {
            await Task.Run(() => DateIsCorrect());
            await Task.Run(() => IsBirthday);
            await Task.Run(() => IsAdult);
            await Task.Run(() => SunSign);
            await Task.Run(() => ChineseSign);
            await Task.Run(() => IsBirthday);
        }
        public RelayCommand<object> SelectDateCommand
        {
            get
            {
                return proceed ??= new RelayCommand<object>(_ => Proceed(), CanExecute);
            }
        }

        private async void Proceed()
        {
            
            if (!DateIsCorrect())
            {
                try
                {
                    throw new DateException("Wrong! You must be older than 0 or younger than 135!", Birthday);
                }
                catch (DateException ex)
                {
                    MessageBox.Show(ex.Message + $"\nInvalid value: {ex.Value.ToString("d")}");
                }
            }
            else
            {
                if (person.IsBirthday)
                {
                    MessageBox.Show("Happy birthday!");
                }
                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("Surname");
                NotifyPropertyChanged("Email");
                NotifyPropertyChanged("DayOfBirthToString");
                NotifyPropertyChanged("IsAdult");
                NotifyPropertyChanged("SunSign");
                NotifyPropertyChanged("ChineseSign");
                NotifyPropertyChanged("IsBirthday");

                var peopleService = new AllPeopleService();
                bool newPerson = await peopleService.AddNewPersonAsync(this);
                if (!newPerson)
                {
                    MessageBox.Show("Person with this email already exists.");
                }
                goToPeople.Invoke();
            }

        }

        public int Age()
        {
            if (DateTime.Today.Year == Birthday.Year && (DateTime.Today.Month < Birthday.Month ||
                (DateTime.Today.Month == Birthday.Month && DateTime.Today.Day < Birthday.Day)))
                return -1;
            if (DateTime.Today.Month < Birthday.Month || (DateTime.Today.Month == Birthday.Month && DateTime.Today.Day < Birthday.Day))
            {
                return DateTime.Today.Year - Birthday.Year - 1;
            }
            return DateTime.Today.Year - Birthday.Year;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
        private bool CanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(person.Name) && !String.IsNullOrWhiteSpace(person.Surname) && !String.IsNullOrWhiteSpace(person.Email);
        }
    }
}
