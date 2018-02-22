using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerTest.Interfaces
{
    public interface IPetOwnerFilter
    {
        bool FilterByGender(PetOwner petowner, string gender, out PetOwner result);
    }
}
