using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetOwnerApiClient.BusinessLogic;
using PetOwnerApiClient.DataContract;
using System.Collections.Generic;

namespace PetOwnerUnitTest
{
    [TestClass]
    public class JsonToPetOwnerConverterTest
    {
        [TestMethod]
        public void Should_return_correct_result_when_input_Json_is_valid()
        {
            var json = @"[{""name"":""Bob"", ""gender"":""Male"", ""age"": 23 ,""pets"": [{""name"":""Garfield"", ""type"":""Cat""}, {""name"":""Fido"", ""type"":""Dog""}]},
                         {""name"":""Jennifer"", ""gender"":""Female"", ""age"": 18 ,""pets"": [{""name"":""Garfield"", ""type"":""Cat""}]}] ";
            var converter = new JsonToPetOwnerConverter();
            var result =  converter.Convert(json);

            var pets1 = new List<Pet>();
            pets1.Add(new Pet { Name = "Garfield", Type = "Cat" });
            pets1.Add(new Pet { Name = "Fido", Type = "Dog" });
            var petowner1 = new PetOwner { Age = 23, Name = "Bob", Gender = "Male", Pets = pets1 };

            var pets2 = new List<Pet>();
            pets2.Add(new Pet { Name = "Garfield", Type = "Cat" });
            var petowner2 = new PetOwner { Age = 18, Name = "Jennifer", Gender = "Female", Pets = pets2 };


            Assert.AreEqual(petowner1.Name, result.PetOwners[0].Name);
            Assert.AreEqual(petowner1.Age, result.PetOwners[0].Age);
            Assert.AreEqual(petowner1.Gender, result.PetOwners[0].Gender);
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
