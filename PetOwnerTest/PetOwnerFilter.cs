using PetOwnerTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerTest
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
        public bool FilterByGender(PetOwner petowner, string gender, out PetOwner result)
        {
            var found = _petownerFinder.Find(petowner, gender);
            if (found)
            {
                result = new PetOwner { Pets = petowner.Pets, Age = petowner.Age, Gender = petowner.Gender, Name = petowner.Name };
                return true;
            }
            else
            {
                result = null;
                return false;
            }          
        }
        public bool FilterByPetType(PetOwner petowner, out PetOwner result)
        {
            var foundPets = petowner.Pets.Where(_petFinder.Find);
            if (foundPets.Any())
            {
                result = new PetOwner { Age = petowner.Age, Gender = petowner.Gender, Name = petowner.Name };
                result.Pets = foundPets.ToList();
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

    }
}
