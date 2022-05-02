using System;
using Practice2Buha.Exceptions;
using System.Text.RegularExpressions;
using Practice2Buha.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2Buha.Models
{
    class Person
    {

        #region Fields
        private string name;
        private string surname;
        private string email;
        private DateTime birthday;

        #endregion

        #region Properties
        public string Name
        {
            get {
                return name; 
            }
            set {
                name = value; 
            }
        }

        public string Surname
        {
            get {
                return surname; 
            }
            set {

                surname = value; 
            }
        }

        public string Email
        {
            get {
                return email; 
            }
            set {
                if (!CorrectEmail(value) && !value.Equals(""))
                {
                    email = "";
                    throw new EmailException("Email is wrong.", value);
                }
                else
                {
                    email = value;
                }
            }
        }

        public DateTime Birthday
        {
            get
            {
                return birthday.Date; 
            }

            set
            {
                if (birthday != value)
                {
                    birthday = value;
                    if (!DateIsCorrect())
                    {
                        throw new DateException("Invalid birthday. It must be between 0 and 135: ", value);
                    }
                }
            }
        }
        #endregion

        #region Constructors
        public Person(string firstName, string lastName, string emailValue, DateTime dateOfBirth)
        {
            name = firstName;
            surname = lastName;
            email = emailValue;
            birthday = dateOfBirth;
        }

        public Person(string firstName, string lastName, string emailValue)
        {
            name = firstName;
            surname = lastName;
            email = emailValue;
            birthday = DateTime.Today.AddDays(1);
        }

        public Person(string firstName, string lastName, DateTime dateOfBirth)
        {
            name = firstName;
            surname = lastName;
            email = null;
            birthday = dateOfBirth;
        }
        #endregion

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
        public bool DateIsCorrect()
        {
            if (Birthday > DateTime.Today) return false;
            if (Age() < 0 || Age() > 135) return false;
            return true;
        }

        public bool CorrectEmail(string email)
        {
            Regex r = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return r.Match(email).Success;
        }

        #region Read-only properties
        public bool IsAdult
        {
            get
            {
                if (Age() >= 18) return true;
                return false;
            }
        }

        public bool IsBirthday
        {
            get
            {
                if (birthday.Day == DateTime.Today.Day && birthday.Month == DateTime.Today.Month) return true;
                return false;
            }
        }

        public string SunSign
        {
            get
            {
                switch (birthday.Month)
                {
                    case 1:
                        if (birthday.Day < 21)
                            return "Capricorn";
                        else
                            return "Aquarius";
                    case 2:
                        if (birthday.Day < 20)
                            return "Aquarius";
                        else
                            return "Pisces";
                    case 3:
                        if (birthday.Day < 21)
                            return "Pisces";
                        else
                            return "Aries";
                    case 4:
                        if (birthday.Day < 21)
                            return "Aries";
                        else
                            return "Taurus";
                    case 5:
                        if (birthday.Day < 22)
                            return "Taurus";
                        else
                            return "Gemini";
                    case 6:
                        if (birthday.Day < 22)
                            return "Gemini";
                        else
                            return "Cancer";
                    case 7:
                        if (birthday.Day < 23)
                            return "Cancer";
                        else
                            return "Leo";
                    case 8:
                        if (birthday.Day < 22)
                            return "Leo";
                        else
                            return "Virgo";
                    case 9:
                        if (birthday.Day < 24)
                            return "Virgo";
                        else
                            return "Libra";
                    case 10:
                        if (birthday.Day < 24)
                            return "Libra";
                        else
                            return "Scorpio";
                    case 11:
                        if (birthday.Day < 24)
                            return "Scorpio";
                        else
                            return "Sagittarius";
                    case 12:
                        if (birthday.Day < 23)
                            return "Sagittarius";
                        else
                            return "Capricorn";
                }
                return "Wrong date";
            }
        }


        public string ChineseSign
        {
            get
            {
                switch ((birthday.Year - 4) % 12)
                {
                    case 0:
                        return "Rat";
                    case 1:
                        return "Ox";
                    case 2:
                        return "Tiger";
                    case 3:
                        return "Rabbit";
                    case 4:
                        return "Dragon";
                    case 5:
                        return "Snake";
                    case 6:
                        return "Horse";
                    case 7:
                        return "Goat";
                    case 8:
                        return "Monkey";
                    case 9:
                        return "Rooster";
                    case 10:
                        return "Dog";
                    case 11:
                        return "Pig";
                }
                return "Wrong date";
            }
            
        }
        
        #endregion

    }


}
