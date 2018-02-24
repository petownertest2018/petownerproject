using PetOwnerApiClient.DataContract;
using PetOwnerApiClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.BusinessLogic
{
    public class PetOwnerGrouper : IPetOwnerGrouper
    {
        public IEnumerable<string> GetGroup(IEnumerable<PetOwner> petowners)
        {
            return petowners.GroupBy(x => x.Gender).Select(x => x.Key).ToList();
        }
    }
}
