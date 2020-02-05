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
        private List<string> _prenoms => new List<string>
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
        private List<string> _noms => new List<string>
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
        private List<string> _telephones => new List<string>
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
        private List<string> _adressesMail => new List<string>
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
            _prenoms[_random.Next(_prenoms.Count)];
        // Générateur de nom
        private string RandomLastName => 
            _noms[_random.Next(_noms.Count)];

        // Générateur de date
        private DateTime RandomDate =>
            new DateTime(_random.Next(1980, 2010), 1, 1)
                .AddDays(_random.Next(0, 365));

        // Générateur de numéro de téléphone
        private string RandomPhoneNumber =>
            _telephones[_random.Next(_telephones.Count)];

        // Générateur d'adresse mail
        private string RandomMailAddress =>
            _adressesMail[_random.Next(_adressesMail.Count)];

        // Générateur de service
        private Service RandomServiceAffected =>
            GetServices()[_random.Next(GetServices().Count)];

        /* Générateur de role
        private Service RandomRolesAffected =>
            GetRoles()[_random.Next(GetRoles().Count)];*/

        // Générateur de personne
        private Personne RandomPersonne => new Personne()
        {
            Prenom = RandomFirstName,
            Nom = RandomLastName,
            DateDeNaissance = RandomDate,
            Telephone = RandomPhoneNumber,
            AdresseMail = RandomMailAddress,
            Service = RandomServiceAffected
            //RolesAffected = RandomRolesAffected
        };
        // Générateur de personnes
        public List<Personne> GetPersonnes(int size)
        {
            var personnes = new List<Personne>();
            for(var i = 0 ; i < size ; i++)
            {
                personnes.Add(RandomPersonne);
            }
            return personnes;
        }

        public List<Service> GetServices()
        {
            return new List<Service>
            {
                new Service { Nom = "Marketing"},
                new Service { Nom = "Production"},
                new Service { Nom = "RH"},
                new Service { Nom = "Commercial"},
                new Service { Nom = "Finance"},
                new Service { Nom = "Achats"},
                new Service { Nom = "Juridique"}
            };
        }

        public List<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role { Nom = "Marchandiseur"},
                new Role { Nom = "Directeur Relation Client"},
                new Role { Nom = "Technicien qualité"},
                new Role { Nom = "Chargé de formation"},
                new Role { Nom = "Directeur commercial"},
                new Role { Nom = "Business developer"},
                new Role { Nom = "Trésorier"},
                new Role { Nom = "Comptable"},
                new Role { Nom = "Directeur financier"},
                new Role { Nom = "Acheteur"},
                new Role { Nom = "Juriste d'entreprise"}
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

        public void AddPersonnes()
        {
            _logger.LogWarning("Adding persons...");
            // S'il y a déjà des personnes dans la base -> ne rien faire
            if (_context.PersonneCollection.Any()) return;
            // Générer des personnes
            var personnes = GetPersonnes(50);
            // Les ajouter au contexte
            _context.AddRange(personnes);
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