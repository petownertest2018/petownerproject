using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetOwnerTest;

namespace PetOwnerUnitTest
{
    [TestClass]
    public class CatFinderTest
    {
        [TestMethod]
        public void Should_return_true_when_pet_type_is_Cat()
        {
            var catFinder = new CatFinder();
            var pet = new Pet { Type = "Cat", Name = "Garfield" };
            var result = catFinder.Find(pet);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Should_return_true_when_pet_type_is_cat_lowercase()
        {
            var catFinder = new CatFinder();
            var pet = new Pet { Type = "cat", Name = "Garfield" };
            var result = catFinder.Find(pet);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Should_return_false_when_pet_type_is_dog()
        {
            var catFinder = new CatFinder();
            var pet = new Pet { Type = "Dog", Name = "Fido" };
            var result = catFinder.Find(pet);
            Assert.AreEqual(false, result);
        }
    }
}
