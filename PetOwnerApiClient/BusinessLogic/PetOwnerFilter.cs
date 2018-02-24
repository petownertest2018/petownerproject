using PetOwnerApiClient.DataContract;
using PetOwnerApiClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.BusinessLogic
{
    public class PetOwnerFilter : IPetOwnerFilter
    {
        private IPetOwnerFinder _petownerFinder;

        private IPetFinder _petFinder;

        public PetOwnerFilter(IPetOwnerFinder petownerFinder, IPetFinder petFinder)
        {
            _petownerFinder = petownerFinder;
            _petFinder = petFinder;
        }
        public PetOwner FilterByGender(PetOwner petowner, string gender)
        {
            var found = _petownerFinder.Find(petowner, gender);
            if (found)
            {
                var result = new PetOwner { Pets = petowner.Pets, Age = petowner.Age, Gender = petowner.Gender, Name = petowner.Name };
                return result;
            }
            else
            {
                return null;                
            }          
        }
        public PetOwner FilterByPetType(PetOwner petowner)
        {
            if (petowner.Pets == null)
            {
                return null;
            }
            var foundPets = petowner.Pets.Where(_petFinder.Find);
            if (foundPets.Any())
            {
                var result = new PetOwner { Age = petowner.Age, Gender = petowner.Gender, Name = petowner.Name };
                result.Pets = foundPets.ToList();
                return result;
            }
            else
            {
               return null;
            }
        }

    }
}
