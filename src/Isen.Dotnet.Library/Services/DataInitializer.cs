using System;
using System.Collections.Generic;
using System.Linq;
using Isen.Dotnet.Library.Context;
using Isen.Dotnet.Library.Model;
using Microsoft.Extensions.Logging;

namespace Isen.Dotnet.Library.Services
{
    public class DataInitializer : IDataInitializer
    {
        private List<string> _firstNames => new List<string>
        {
            "Sang", 
            "Anne",
            "Boris",
            "Pierre",
            "Laura",
            "Hadrien",
            "Camille",
            "Louis",
            "Alicia"
        };
        private List<string> _lastNames => new List<string>
        {
            "Schuck",
            "Arbousset",
            "Lopasso",
            "Jubert",
            "Lebrun",
            "Dutaud",
            "Sarrazin",
            "Vu Dinh"
        };
        private List<string> _phoneNumbers => new List<string>
        {
            "0601020304",
            "0605070809",
            "0610111213",
            "0614151617",
            "0618192021",
            "0622232425",
            "0626272829",
            "0630313233"
        };
        private List<string> _mailAddresses => new List<string>
        {
            "sang.schuck@isen.fr",
            "anne.arbousset@isen.fr",
            "boris.lopasso@isen.fr",
            "pierre.jubert@isen.fr",
            "laura.lebrun@isen.fr",
            "hadrien.dutaud@isen.fr",
            "camille.sarrazin@isen.fr",
            "louis.vudinh@isen.fr"
        };
        // Générateur aléatoire
        private readonly Random _random;

        // DI de ApplicationDbContext
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DataInitializer> _logger;
        public DataInitializer(
            ILogger<DataInitializer> logger,
            ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
            _random = new Random();
        }

        // Générateur de prénom
        private string RandomFirstName => 
            _firstNames[_random.Next(_firstNames.Count)];
        // Générateur de nom
        private string RandomLastName => 
            _lastNames[_random.Next(_lastNames.Count)];

        // Générateur de date
        private DateTime RandomDate =>
            new DateTime(_random.Next(1980, 2010), 1, 1)
                .AddDays(_random.Next(0, 365));

        // Générateur de numéro de téléphone
        private string RandomPhoneNumber =>
            _phoneNumbers[_random.Next(_phoneNumbers.Count)];

        // Générateur d'adresse mail
        private string RandomMailAddress =>
            _mailAddresses[_random.Next(_mailAddresses.Count)];

        // Générateur de service
        private Service RandomServiceAffected =>
            GetServices()[_random.Next(GetServices().Count)];

        /* Générateur de role
        private Service RandomRolesAffected =>
            GetRoles()[_random.Next(GetRoles().Count)];*/

        // Générateur de personne
        private Person RandomPerson => new Person()
        {
            FirstName = RandomFirstName,
            LastName = RandomLastName,
            DateOfBirth = RandomDate,
            PhoneNumber = RandomPhoneNumber,
            MailAddress = RandomMailAddress,
            ServiceAffected = RandomServiceAffected
            //RolesAffected = RandomRolesAffected
        };
        // Générateur de personnes
        public List<Person> GetPersons(int size)
        {
            var persons = new List<Person>();
            for(var i = 0 ; i < size ; i++)
            {
                persons.Add(RandomPerson);
            }
            return persons;
        }

        public List<Service> GetServices()
        {
            return new List<Service>
            {
                new Service { Name = "Marketing"},
                new Service { Name = "Production"},
                new Service { Name = "RH"},
                new Service { Name = "Commercial"},
                new Service { Name = "Finance"},
                new Service { Name = "Achats"},
                new Service { Name = "Juridique"}
            };
        }

        public List<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role { Name = "Marchandiseur"},
                new Role { Name = "Directeur Relation Client"},
                new Role { Name = "Technicien qualité"},
                new Role { Name = "Chargé de formation"},
                new Role { Name = "Directeur commercial"},
                new Role { Name = "Business developer"},
                new Role { Name = "Trésorier"},
                new Role { Name = "Comptable"},
                new Role { Name = "Directeur financier"},
                new Role { Name = "Acheteur"},
                new Role { Name = "Juriste d'entreprise"}
            };
        }

        public void DropDatabase()
        {
            _logger.LogWarning("Dropping database");
            _context.Database.EnsureDeleted();
        }
            

        public void CreateDatabase()
        {
            _logger.LogWarning("Creating database");
            _context.Database.EnsureCreated();
        }

        public void AddPersons()
        {
            _logger.LogWarning("Adding persons...");
            // S'il y a déjà des personnes dans la base -> ne rien faire
            if (_context.PersonCollection.Any()) return;
            // Générer des personnes
            var persons = GetPersons(50);
            // Les ajouter au contexte
            _context.AddRange(persons);
            // Sauvegarder le contexte
            _context.SaveChanges();
        }

        public void AddServices()
        {
            _logger.LogWarning("Adding services...");
            var services = GetServices();
            _context.AddRange(services);
            _context.SaveChanges();
        }

        public void AddRoles()
        {   
            _logger.LogWarning("Adding roles...");
            var roles = GetRoles();
            _context.AddRange(roles);
            _context.SaveChanges();
        }
    }
}