using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isen.Dotnet.Library.Model
{
    public class Role : BaseEntity
    {      
        public string Nom {get;set;}
        public MyCollection<PersonneRole> PersonneRole {get;set;}

        public override string ToString() =>
            $"{Nom}";
        
    }
}