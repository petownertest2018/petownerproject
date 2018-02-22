using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetOwnerTest;
using PetOwnerTest.Interfaces;
using Moq;
using System.Collections.Generic;

namespace PetOwnerUnitTest
{
    [TestClass]
    public class PetOwnerFilterTest
    {
        [TestMethod]
        public void Should_return_true_and_set_pets_correctly_when_pet_finder_condition_is_true()
        {
            var mock = new Mock<IPetFinder>();
            mock.Setup(x => x.Find(It.IsAny<Pet>())).Returns(true);

            var mockOwnerFinder = new Mock<IPetOwnerFinder>();

            var filter = new PetOwnerFilter( mockOwnerFinder.Object, mock.Object);
            var pets = new List<Pet>();
            pets.Add(new Pet { Name = "Fido", Type = "Dog" });
            var petowner = new PetOwner { Age = 20, Name = "Jack", Gender = "Male", Pets = pets };

            PetOwner actualOwner = null;
            var found = filter.FilterByPetType(petowner, out actualOwner);
            Assert.AreEqual(true, found);
            Assert.AreEqual(petowner.Age, actualOwner.Age);
            Assert.AreEqual(petowner.Gender, actualOwner.Gender);
            Assert.AreEqual(petowner.Name, actualOwner.Name);
            Assert.AreEqual(pets[0].Name, actualOwner.Pets[0].Name);
            Assert.AreEqual(pets[0].Type, actualOwner.Pets[0].Type);
        }
        [TestMethod]
        public void Should_return_false_and_set_pets_correctly_when_pet_finder_condition_is_false()
        {
            var mock = new Mock<IPetFinder>();
            mock.Setup(x => x.Find(It.IsAny<Pet>())).Returns(false);

            var mockOwnerFinder = new Mock<IPetOwnerFinder>();

            var filter = new PetOwnerFilter(mockOwnerFinder.Object, mock.Object);
            var pets = new List<Pet>();
            pets.Add(new Pet { Name = "Fido", Type = "Dog" });
            var petowner = new PetOwner { Age = 20, Name = "Jack", Gender = "Male", Pets = pets };

            PetOwner actualOwner = null;
            var found = filter.FilterByPetType(petowner, out actualOwner);
            Assert.AreEqual(false, found);
            Assert.AreEqual(null, actualOwner);
            
        }

    }
}
