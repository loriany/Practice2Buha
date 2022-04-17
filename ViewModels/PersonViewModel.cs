using Practice2Buha.Models;
using Practice2Buha.Tools;
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
        private bool isEnabled = true;
        #endregion


        #region Properties
        public string Name
        {
            get
            {
                if (DateIsCorrect()) { return person.Name; }
                return " ";
            }
            set { person.Name = value; }
        }

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
            set { person.Email = value; }
        }

        public DateTime Birthday
        {
            get
            { return person.Birthday; }

            set
            {
                if (person.Birthday != value)
                {
                    person.Birthday = value;
                    IsEnabled = false;
                    Task.Run(async () => await setAsyncData());
                    IsEnabled = true;
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

        private void Proceed()
        {
            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Surname");
            NotifyPropertyChanged("Email");
            NotifyPropertyChanged("DayOfBirthToString");
            NotifyPropertyChanged("IsAdult");
            NotifyPropertyChanged("SunSign");
            NotifyPropertyChanged("ChineseSign");
            NotifyPropertyChanged("IsBirthday");
            if (!DateIsCorrect())
            {
                MessageBox.Show("Wrong! You must be older than 0 or younger than 135");
            }
            else
            {
                if (person.IsBirthday)
                {
                    MessageBox.Show("Happy birthday!");
                }

            }
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
