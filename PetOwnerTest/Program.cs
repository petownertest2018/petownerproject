using Newtonsoft.Json;
using PetOwnerTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerTest
{
    public class PetOwner
    {
        [JsonProperty(PropertyName="name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }
        [JsonProperty(PropertyName = "age")]
        public int Age { get; set; }
        [JsonProperty(PropertyName = "pets")]
        public List<Pet> Pets { get; set; }
    }
    public class Pet
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        
    }
    public class PetOwnerDataContract
    {
        public PetOwner[] PetOwners { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var json = @"[{""name"":""Bob"", ""gender"":""Male"", ""age"": 23 ,""pets"": [{""name"":""Garfield"", ""type"":""Cat""}, {""name"":""Fido"", ""type"":""Dog""}]},
                            {""name"":""Jennifer"", ""gender"":""Female"", ""age"": 18 ,""pets"": [{""name"":""Garfield"", ""type"":""Cat""}]}]
                            ";
            
            var resultOwners = JsonConvert.DeserializeObject<PetOwner[]>(json);
            IPetFinder finder = new CatFinder();
            IPetOwnerFinder ownerFinder = new PetOwnerFinder();
            var filter = new PetOwnerFilter(ownerFinder, finder);
            var filterResults = new List<PetOwner>();
            foreach (var o in resultOwners)
            {
                if(filter.FilterByPetType(o, out PetOwner tempOwner))
                    filterResults.Add(tempOwner);
            }
           
            var grouper = new GenderHeadingGrouper();
            var grp = grouper.GetGroup(filterResults);
            
            foreach (var gender in grp)
            {                
                Console.WriteLine(gender);
                var petnames = GetPetNames(filter, filterResults, gender);
                foreach (var pet in petnames)
                {
                    Console.WriteLine(pet);
                }
            }
        }
        private static List<string> GetPetNames(IPetOwnerFilter filter , List<PetOwner> petowners, string gender)
        {
            var petnames = new List<string>();
            foreach (var o in petowners)
            {
                if (filter.FilterByGender(o, gender, out PetOwner tempOwner))
                {
                    petnames.AddRange(tempOwner.Pets.Select(x => x.Name));
                }   
            }
            return petnames;
        }
    }
}
