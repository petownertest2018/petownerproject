using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerTest.Interfaces
{
    public interface IHeadingGrouper
    {
        List<string> GetGroup(List<PetOwner> petowners);
    }
}
