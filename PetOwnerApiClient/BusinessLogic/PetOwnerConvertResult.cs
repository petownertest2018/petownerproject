using PetOwnerApiClient.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.BusinessLogic
{
    public class PetOwnerConvertResult
    {
        public PetOwner[] PetOwners { get; set; }
        public string Error { get; set; }
    }
}
