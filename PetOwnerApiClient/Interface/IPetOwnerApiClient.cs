using PetOwnerApiClient.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.Interfaces
{
    public interface IPetOwnerApiClient
    {
        Task<PetByGenderResult> GetPetsByPetOwnerGender();        
    }
}
