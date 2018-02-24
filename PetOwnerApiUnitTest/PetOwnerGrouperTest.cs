using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using PetOwnerApiClient.BusinessLogic;
using PetOwnerApiClient.DataContract;

namespace PetOwnerUnitTest
{
    [TestClass]
    public class PetOwnerGrouperTest
    {
        [TestMethod]
        public void Should_return_headings_male_and_female_when_pet_owners_are_male_and_female()
        {
            var grouper = new PetOwnerGrouper();
            var pets1 = new List<Pet>();
            pets1.Add(new Pet { Name = "Fido", Type = "Dog" });
            var petowner1 = new PetOwner { Age = 20, Name = "Jack", Gender = "Male", Pets = pets1 };

            var pets2 = new List<Pet>();
            pets2.Add(new Pet { Name = "Blackie", Type = "Dog" });
            var petowner2 = new PetOwner { Age = 21, Name = "Jane", Gender = "Female", Pets = pets2 };

            var pets3 = new List<Pet>();
            pets3.Add(new Pet { Name = "Garfield", Type = "Cat" });
            var petowner3 = new PetOwner { Age = 23, Name = "Jill", Gender = "Female", Pets = pets2 };

            var owners = new List<PetOwner> { petowner1, petowner2, petowner3 };
            var actual = grouper.GetGroup(owners);
            var expected = new List<string>() { "Male" , "Female" };
            Assert.AreEqual(true, actual.SequenceEqual(expected));
        }       
    }
}
