using System;
using System.Collections.Generic;
using Practice2Buha.ViewModels;
using Practice2Buha.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2Buha.Services
{
    class PersonService
    {
        private static FileRepository repository = new FileRepository();

        public IEnumerable<PersonViewModel> GetAllPeople()
        {
            var result = new List<PersonViewModel>();
            foreach (var user in repository.GetAll())
            {
                result.Add(user);
            }

            return result;
        }

        public async void Edit(PersonViewModel person)
        {
            await repository.AddOrUpdateAsync(person);
        }
        public async void Delete(PersonViewModel person)
        {
            await repository.DeleteAsync(person);
        }
    }
}
