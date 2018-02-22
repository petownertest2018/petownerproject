using PetOwnerTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerTest
{
    public class GenderHeadingGrouper : IHeadingGrouper
    {
        public List<string> GetGroup(List<PetOwner> petowners)
        {
            return petowners.GroupBy(x => x.Gender).Select(x => x.Key).ToList();
        }
    }
}
