using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerTest.Interfaces
{
    public interface IPetOwnerFinder
    {
        bool Find(PetOwner owner, string gender);
    }
}
