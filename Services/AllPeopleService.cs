using Practice2Buha.ViewModels;
using Practice2Buha.Repositories;
using System.Threading.Tasks;

namespace Practice2Buha.Services
{
    class AllPeopleService
    {

        private static FileRepository repository = new FileRepository();

        public async Task<bool> AddNewPersonAsync(PersonViewModel person)
        {
            await repository.AddOrUpdateAsync(person);
            return true;
        }
    }
}
