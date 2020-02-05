using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isen.Dotnet.Library.Model
{
    public class Personne : BaseEntity
    {      
        public string Nom {get;set;}
        public string Prenom {get;set;}
        public DateTime? DateDeNaissance {get;set;}
        public string Telephone {get;set;}
        public string AdresseMail {get;set;}
        public Service Service {get;set;}
        public int? ServiceId {get;set;}
        public Role Role {get;set;}
        public int? RoleId {get;set;}
        public MyCollection<PersonneRole> PersonneRole {get;set;}

        public override string ToString() =>
            $"{Prenom} {Nom} | {DateDeNaissance} | {Telephone} / {AdresseMail}";
        
    }
}