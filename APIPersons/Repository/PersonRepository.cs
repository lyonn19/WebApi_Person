using APIPersons.Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPersons.Repository
{
    public class PersonRepository
    {
        // Store in Memory / EntityFrw Context
        List<PersonEntity> personEntities = new List<PersonEntity>();

        private readonly PersonContext _databaseContext;
        private readonly IServiceScope _scope;

        public PersonRepository(IServiceProvider services)
        {
            _scope = services.CreateScope();
            _databaseContext = _scope.ServiceProvider.GetRequiredService<PersonContext>();
        }

        public async Task<bool> AddPerson(PersonEntity personEntity)
        {
            var success = false;
            _databaseContext.PersonEntities.Add(personEntity);
            var itemCreated = await _databaseContext.SaveChangesAsync();
            if (itemCreated == 1)
                success = true;

            return success;
        }

        public async Task<bool> DeletePerson(int id)
        {
            var success = false;

            PersonEntity found = GetPersonById(id);
            if (found != null)
            {
                _databaseContext.PersonEntities.Remove(found);

                var itemDeleted = await _databaseContext.SaveChangesAsync();

                if (itemDeleted == 1)
                    success = true;
            }



            return success;
        }

        private PersonEntity GetPersonById(int id)
        {
            return _databaseContext.PersonEntities
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public PersonEntity GetOldestPerson()
        {
            return _databaseContext.PersonEntities
                .OrderByDescending(x => x.Age)
                .FirstOrDefault();
        }

        public List<PersonEntity> GetAllPersons()
        {
            return _databaseContext.PersonEntities.ToList();
        }
    }
}
