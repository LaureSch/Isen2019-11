using System.Collections.Generic;
using Isen.Dotnet.Library.Model;

namespace Isen.Dotnet.Library.Services
{
    public interface IDataInitializer
    {
         void DropDatabase();
         void CreateDatabase();
         void AddServices();
         void AddRoles();

         List<Personne> GetPersonnes(int size);
         void AddPersonnes();
    }
}