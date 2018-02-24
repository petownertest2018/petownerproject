using PetOwnerApiClient.DataContract;
using PetOwnerApiClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.BusinessLogic
{
    public class PetOwnerFinder : IPetOwnerFinder
    {
        public bool Find(PetOwner owner, string gender)
        {
            return owner.Gender == gender;
        }
    }
}
