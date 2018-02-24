using PetOwnerApiClient.DataContract;
using PetOwnerApiClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.BusinessLogic
{
    public class CatFinder : IPetFinder
    {
        public bool Find(Pet pet)
        {
            return pet.Type.ToLower() == "Cat".ToLower();
        }
    }
}
