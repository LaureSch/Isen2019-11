using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isen.Dotnet.Library.Model
{
    public class PersonneRole : BaseEntity
    {   
        public Personne Personne {get;set;}
        public int? PersonneId {get;set;}   
        public Role Role {get;set;}
        public int? RoleId {get;set;}

    }
}