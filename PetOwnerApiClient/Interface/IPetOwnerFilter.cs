using PetOwnerApiClient.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.Interfaces
{
    public interface IPetOwnerFilter
    {
        PetOwner FilterByGender(PetOwner petowner, string gender);
        PetOwner FilterByPetType(PetOwner petowner);
    }
}
