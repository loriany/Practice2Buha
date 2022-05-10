using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Practice2Buha.ViewModels;
using Practice2Buha.Models;

namespace Practice2Buha.Repositories
{
    class FileRepository
    {
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Buha", "AllPeople");

        private static ObservableCollection<PersonViewModel> people = new ObservableCollection<PersonViewModel>()
        {
            new PersonViewModel(){Name = "Yana", Surname = "Buha", Email = "yana.buha129@gmail.com", Birthday = new DateTime(2002,09,12), Guid = new Guid("1aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Georgii", Surname = "Sanchenko", Email = "sanchenko@ukma.ua", Birthday = new DateTime(2009,05,10), Guid = new Guid("2aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Svitlana", Surname = "Svitla", Email = "svitla@gmail.com", Birthday = new DateTime(1999,12,12), Guid = new Guid("3aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Darina", Surname = "Stepanenko", Email = "darina@gmail.com", Birthday = new DateTime(2010,11,11), Guid = new Guid("4aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Nyuta", Surname = "Zheliznyak", Email = "zheliznyak@uknet.ua", Birthday = new DateTime(1976,11,19), Guid = new Guid("5aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Margarita", Surname = "Budenkova", Email = "budenkova@gmail.com", Birthday = new DateTime(2002,06,22), Guid = new Guid("6aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Ilya", Surname = "Shevchik", Email = "shevchik@ukrnet.com", Birthday = new DateTime(2001,08,15), Guid = new Guid("7aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Anton", Surname = "Smivzh", Email = "smivzh@cgs.com", Birthday = new DateTime(1998,04,14), Guid = new Guid("8aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Katrin", Surname = "Awramenko", Email = "awram@gmail.com", Birthday = new DateTime(2018,09,20), Guid = new Guid("9aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Anya", Surname = "Kudyakova", Email = "kudanya@edu.com", Birthday = new DateTime(2002,03,8), Guid = new Guid("10aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Danilo", Surname = "Korol", Email = "koroldan@email.com", Birthday = new DateTime(1997,01,18), Guid = new Guid("11aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Sasha", Surname = "Mik", Email = "miksasha@email.com", Birthday = new DateTime(2013,03,19), Guid = new Guid("12aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Yarik", Surname = "Artemenko", Email = "yaartem@email.com", Birthday = new DateTime(2001,11,10), Guid = new Guid("13aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Vlada", Surname = "Rada", Email = "rada@gmail.com", Birthday = new DateTime(1988,12,12), Guid = new Guid("14aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Senya", Surname = "Pupkin", Email = "lorein@gmail.com", Birthday = new DateTime(1942,10,16), Guid = new Guid("15aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Gleb", Surname = "Nestaiko", Email = "glebnest@gmail.com", Birthday = new DateTime(2005,03,14), Guid = new Guid("16aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Dmytro", Surname = "Parnak", Email = "parnak@gmail.com", Birthday = new DateTime(1954,02,23), Guid = new Guid("17aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Stepan", Surname = "Bahdera", Email = "slavaukraini@gmail.com", Birthday = new DateTime(1991,08,24), Guid = new Guid("18aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Daria", Surname = "Shevchenko", Email = "sheva@gmail.com", Birthday = new DateTime(2007,04,24), Guid = new Guid("19aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Galya", Surname = "Petrenko", Email = "galpet@gmail.com", Birthday = new DateTime(1983,03,17), Guid = new Guid("20aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Vasil", Surname = "Gaidun", Email = "gaidunva@net.com", Birthday = new DateTime(2001,10,12), Guid = new Guid("21aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Alex", Surname = "Hurbich", Email = "hurbich@gmail.com", Birthday = new DateTime(2002,10,12), Guid = new Guid("22aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Mike", Surname = "Hurbich", Email = "hurmike@net.com", Birthday = new DateTime(1900,09,12), Guid = new Guid("23aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Alana", Surname = "Derechey", Email = "derecheyalana@cgs.com", Birthday = new DateTime(2011,11,11), Guid = new Guid("24aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Alana", Surname = "Knut", Email = "kbutal@cgs.com", Birthday = new DateTime(1995,10,19), Guid = new Guid("25aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Emma", Surname = "Swon", Email = "swon@cgs.com", Birthday = new DateTime(1996,12,12), Guid = new Guid("26aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Clay", Surname = "Jensen", Email = "jensenclay@cgs.com", Birthday = new DateTime(2002,03,15), Guid = new Guid("27aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Vitalii", Surname = "Black", Email = "blackvi@cgs.com", Birthday = new DateTime(2011,05,9), Guid = new Guid("28aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Heinek", Surname = "White", Email = "whitesnow@cgs.com", Birthday = new DateTime(2018,07,27), Guid = new Guid("29aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Dorama", Surname = "Doriti", Email = "dorti@cgs.com", Birthday = new DateTime(1966,03,25), Guid = new Guid("30aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Alex", Surname = "Kvit", Email = "kvitalex@gmail.com", Birthday = new DateTime(1972,10,10), Guid = new Guid("31aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Maria", Surname = "Krasa", Email = "krasa@gmail.com", Birthday = new DateTime(2019,09,12), Guid = new Guid("32aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Liana", Surname = "Samoilova", Email = "leyka@gmail.com", Birthday = new DateTime(1988,03,18), Guid = new Guid("33aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Vasiliy", Surname = "Black", Email = "vasiloity@gmail.com", Birthday = new DateTime(2021,09,10), Guid = new Guid("34aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Arata", Surname = "Hurbich", Email = "agata@gmail.com", Birthday = new DateTime(2015,11,19), Guid = new Guid("35aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Maria", Surname = "Vdovik", Email = "vdovik@gmail.com", Birthday = new DateTime(2016,10,22), Guid = new Guid("36aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Maya", Surname = "Maiskaya", Email = "mayot@gmail.com", Birthday = new DateTime(1998,06,15), Guid = new Guid("37aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Katrina", Surname = "Hey", Email = "kathey@gmail.com", Birthday = new DateTime(2012,06,14), Guid = new Guid("38aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Gesha", Surname = "May", Email = "maygesha@gmail.com", Birthday = new DateTime(1989,12,20), Guid = new Guid("39aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Lilian", Surname = "Cool", Email = "lilya@gmail.com", Birthday = new DateTime(2000,06,16), Guid = new Guid("40aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Kirill", Surname = "Permyakov", Email = "kirperm@gmail.com", Birthday = new DateTime(2010,11,15), Guid = new Guid("41aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Daryna", Surname = "Vronska", Email = "vorona@gmail.com", Birthday = new DateTime(1956,09,29), Guid = new Guid("42aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Sam", Surname = "Smith", Email = "ssmith@gmail.com", Birthday = new DateTime(2000,05,27), Guid = new Guid("43aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Sergiy", Surname = "Karasin", Email = "karas@gmail.com", Birthday = new DateTime(2010,12,12), Guid = new Guid("44aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Ben", Surname = "Bron", Email = "bbron@gmail.com", Birthday = new DateTime(1975,08,11), Guid = new Guid("45aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Nick", Surname = "Semin", Email = "nsemin@gmail.com", Birthday = new DateTime(1996,12,24), Guid = new Guid("46aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Galina", Surname = "Westwood", Email = "woodgl@gmail.com", Birthday = new DateTime(2006,10,12), Guid = new Guid("47aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Mary", Surname = "Me", Email = "maryme@gmail.com", Birthday = new DateTime(1988,08,14), Guid = new Guid("48aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Alexandra", Surname = "Smell", Email = "smaelal@gmail.com", Birthday = new DateTime(2019,03,20), Guid = new Guid("49aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")},
            new PersonViewModel(){Name = "Liza", Surname = "Black", Email = "lilibeth@gmail.com", Birthday = new DateTime(2013,11,11), Guid = new Guid("50aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")}
        };

        public FileRepository()
        {
            if (!Directory.Exists(BaseFolder))
            {
                Directory.CreateDirectory(BaseFolder);
            }

            foreach (PersonViewModel p in people)
            {
                string filePath = Path.Combine(BaseFolder, p.Guid.ToString());

                if (!File.Exists(filePath))
                    _ = AddOrUpdateAsync(p);
            }
        }

        public async Task AddOrUpdateAsync(PersonViewModel person)
        {
            var stringObject = JsonSerializer.Serialize(person);

            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, person.Guid.ToString()), false))
            {
                await sw.WriteAsync(stringObject);
            }
        }


        public async Task DeleteAsync(PersonViewModel person)
        {
            string filePath = Path.Combine(BaseFolder, person.Guid.ToString());

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public async Task<Person> GetAsync(Guid guid)
        {
            string stringObject = null;
            string filePath = Path.Combine(BaseFolder, guid.ToString());
            if (!File.Exists(filePath))
                return null;
            using (StreamReader sr = new StreamReader(filePath))
            {
                stringObject = await sr.ReadToEndAsync();
            }
            return JsonSerializer.Deserialize<Person>(stringObject);
        }

        public List<PersonViewModel> GetAll()
        {
            var res = new List<PersonViewModel>();

            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObject = null;

                using (StreamReader sr = new StreamReader(file))
                {
                    stringObject = sr.ReadToEnd();
                }

                res.Add(JsonSerializer.Deserialize<PersonViewModel>(stringObject));
            }
            return res;
        }

    }
}
