using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetOwnerApiClient.BusinessLogic;
using PetOwnerApiClient.DataContract;
using ApiClient = PetOwnerApiClient.Client.PetOwnerApiClient;
using Moq;
using PetOwnerApiClient.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PetOwnerUnitTest
{
    [TestClass]
    public class PetOwnerApiClientTest
    {
        [TestMethod]
        public void Should_return_sorted_group_correctly_when_apiclient_is_called()
        {
            var pets1 = new List<Pet>();
            pets1.Add(new Pet { Name = "Fido", Type = "Dog" });
            var cat1 = new Pet { Name = "Molly", Type = "Cat" };
            pets1.Add(cat1);
            var petOwner1 = new PetOwner { Age = 20, Name = "Jack", Gender = "Male", Pets = pets1 };
            var petFilteredOwner1 = new PetOwner { Age = 20, Name = "Jack", Gender = "Male", Pets = pets1.Where(x=> x.Type == "Cat").ToList() };

            var pets2 = new List<Pet>();
            pets2.Add(new Pet { Name = "Blackie", Type = "Dog" });
            var cat2 = new Pet { Name = "Chloe", Type = "Cat" };
            pets2.Add(cat2);
            var petOwner2 = new PetOwner { Age = 21, Name = "Jane", Gender = "Female", Pets = pets2 };
            var petFilteredOwner2 = new PetOwner { Age = 21, Name = "Jane", Gender = "Female", Pets = pets2.Where(x => x.Type == "Cat").ToList() };

            var pets3 = new List<Pet>();
            var cat3 = new Pet { Name = "Garfield", Type = "Cat" };
            pets3.Add(cat3);
            var petOwner3 = new PetOwner { Age = 25, Name = "Harry", Gender = "Male", Pets = pets3 };

            var petOwners = new PetOwner[] { petOwner1, petOwner2, petOwner3 };

            var petFilteredOwners = new PetOwner[] { petFilteredOwner1, petFilteredOwner2, petOwner3 };

            var mockConverter = new Mock<IJsonToPetOwnerConverter>();
            mockConverter.Setup(x => x.Convert(It.IsAny<string>())).Returns( new PetOwnerConvertResult { PetOwners = petOwners });

            var mockService = new Mock<IPetOwnerApiService>();
            mockService.Setup(x => x.GetPetOwnerJson()).Returns(Task.FromResult(new PetOwnerApiServiceResult { PetOwnerJson = "{}"  }));

            var mockPetOwnerFilter = new Mock<IPetOwnerFilter>();

            mockPetOwnerFilter.Setup(x => x.FilterByPetType(petOwner1)).Returns(petFilteredOwner1);
            mockPetOwnerFilter.Setup(x => x.FilterByPetType(petOwner2)).Returns(petFilteredOwner2);
            mockPetOwnerFilter.Setup(x => x.FilterByPetType(petOwner3)).Returns(petOwner3);

            mockPetOwnerFilter.Setup(x => x.FilterByGender(petFilteredOwner1, "Male")).Returns(petFilteredOwner1);
            mockPetOwnerFilter.Setup(x => x.FilterByGender(petFilteredOwner2, "Female")).Returns(petFilteredOwner2);
            mockPetOwnerFilter.Setup(x => x.FilterByGender(petOwner3, "Male")).Returns(petOwner3);

            
            var mockPetSorter = new Mock<IPetSorter>();
            var catsUnSorted1 = new[] { "Molly", "Garfield" };
            var catsUnSorted2 = new[] { "Chloe" };
            var catsSorted1 = new[] { "Garfield", "Molly" };
            var catsSorted2 = new[] { "Chloe" };
            mockPetSorter.Setup(x => x.Sort(catsUnSorted1)).Returns(catsSorted1);
            mockPetSorter.Setup(x => x.Sort(catsUnSorted2)).Returns(catsSorted2);

            var mockGrouper = new Mock<IPetOwnerGrouper>();
            mockGrouper.Setup(x => x.GetGroup(petFilteredOwners)).Returns(new[] { "Male", "Female"});
            var apiClient = new ApiClient(mockConverter.Object, mockService.Object,
                                     mockPetOwnerFilter.Object, mockPetSorter.Object, mockGrouper.Object);

            var owners = apiClient.GetPetsByPetOwnerGender().Result;
            Assert.AreEqual("Male", owners.PetsByGender.FirstOrDefault().Gender);
            Assert.AreEqual(2, owners.PetsByGender.FirstOrDefault().PetNames.Count());
            Assert.IsTrue(catsSorted1.SequenceEqual(owners.PetsByGender.FirstOrDefault().PetNames));

            Assert.AreEqual("Female", owners.PetsByGender.ElementAt(1).Gender);
            Assert.AreEqual(1, owners.PetsByGender.ElementAt(1).PetNames.Count());
            Assert.AreEqual("Chloe", owners.PetsByGender.ElementAt(1).PetNames.FirstOrDefault());            

        }

        [TestMethod]
        public void Should_return_api_service_error_when_apiclient_service_has_error()
        {
            var mockConverter = new Mock<IJsonToPetOwnerConverter>();
            mockConverter.Setup(x => x.Convert(It.IsAny<string>())).Returns(new PetOwnerConvertResult { });

            var mockService = new Mock<IPetOwnerApiService>();
            mockService.Setup(x => x.GetPetOwnerJson()).Returns(Task.FromResult(new PetOwnerApiServiceResult { Error = "Service Error" }));

            var mockPetOwnerFilter = new Mock<IPetOwnerFilter>();
         
            var mockPetSorter = new Mock<IPetSorter>();           

            var mockGrouper = new Mock<IPetOwnerGrouper>();
            var apiClient = new ApiClient(mockConverter.Object, mockService.Object,
                                     mockPetOwnerFilter.Object, mockPetSorter.Object, mockGrouper.Object);

            var result = apiClient.GetPetsByPetOwnerGender().Result;
            Assert.AreEqual(null, result.PetsByGender);
            Assert.AreEqual("Service Error", result.Error);
        }
        [TestMethod]
        public void Should_return_convert_error_when_petowner_json_conversion_failed()
        {
            var mockConverter = new Mock<IJsonToPetOwnerConverter>();
            mockConverter.Setup(x => x.Convert(It.IsAny<string>())).Returns(new PetOwnerConvertResult { Error = "Convert Error" });

            var mockService = new Mock<IPetOwnerApiService>();
            mockService.Setup(x => x.GetPetOwnerJson()).Returns(Task.FromResult(new PetOwnerApiServiceResult { PetOwnerJson = "{}" }));

            var mockPetOwnerFilter = new Mock<IPetOwnerFilter>();

            var mockPetSorter = new Mock<IPetSorter>();

            var mockGrouper = new Mock<IPetOwnerGrouper>();
            var apiClient = new ApiClient(mockConverter.Object, mockService.Object,
                                     mockPetOwnerFilter.Object, mockPetSorter.Object, mockGrouper.Object);

            var result = apiClient.GetPetsByPetOwnerGender().Result;
            Assert.AreEqual(null, result.PetsByGender);
            Assert.AreEqual("Convert Error", result.Error);
        }

    }
}
