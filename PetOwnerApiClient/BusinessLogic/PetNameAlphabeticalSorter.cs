using PetOwnerApiClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.BusinessLogic
{
    public class PetNameAlphabeticalSorter : IPetSorter
    {
        private AlphabeticComparer _alphabeticComparer;

        public PetNameAlphabeticalSorter(IComparer<string> comparer)
        {
            _alphabeticComparer = new AlphabeticComparer();
        }
        public IEnumerable<string>Sort(IEnumerable<string> petNames)
        {   
            return petNames.OrderBy(x => x, _alphabeticComparer);
        }
    }
}
