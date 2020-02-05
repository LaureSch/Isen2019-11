using System.Collections.Generic;
using Isen.Dotnet.Library.Model;

namespace Isen.Dotnet.Library.Services
{
    public interface IDataInitializer
    {
         List<Person> GetPersons(int size);
         void DropDatabase();
         void CreateDatabase();
         void AddPersons();
         void AddCities();

         List<Personne> GetPersonnes(int size);
         void AddPersonnes();
    }
}