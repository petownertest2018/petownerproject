using PetOwnerTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerTest
{
    public class CatFinder : IPetFinder
    {
        public bool Find(Pet pet)
        {
            return pet.Type.ToLower() == "Cat".ToLower();
        }
    }
}
