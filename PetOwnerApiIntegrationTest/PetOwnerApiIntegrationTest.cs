using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetOwnerApiClient.Interfaces;
using PetOwnerApiClient.Service;
using System.Net.Http;
using PetOwnerApiClient.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace PetOwnerApiIntegrationTest
{
    [TestClass]
    public class PetOwnerApiIntegrationTest
    {
        [TestMethod]
        public void Should_retrieve_data_from_petowner_api()
        { 
            var path = ConfigurationManager.AppSettings["PetOwnerAPI.Url"];
            var uri = new Uri(path);

            IJsonToPetOwnerConverter converter = new JsonToPetOwnerConverter();
            var httpClient = new HttpClient();
            IPetOwnerApiService apiService = new PetOwnerApiService(httpClient, uri);

            IPetFinder petFinder = new CatFinder();
            IPetOwnerFinder petOwnerFinder = new PetOwnerFinder();
            IPetOwnerFilter petOwnerFilter = new PetOwnerFilter(petOwnerFinder, petFinder);
            IComparer<string> comparer = new AlphabeticComparer();
            IPetSorter petSorter = new PetNameAlphabeticalSorter(comparer);
            IPetOwnerGrouper grouper = new PetOwnerGrouper();
            var apiClient = new PetOwnerApiClient.Client.PetOwnerApiClient(converter, apiService, petOwnerFilter, petSorter, grouper);
            var apiResult = apiClient.GetPetsByPetOwnerGender().Result;

            Assert.IsTrue(apiResult != null);
            Assert.IsTrue(string.IsNullOrEmpty(apiResult.Error));
            if (apiResult.PetsByGender != null)
            {
                var distinctGenders = apiResult.PetsByGender.Select(x => x.Gender).Distinct().ToList();
                var allGenders = apiResult.PetsByGender.Select(x => x.Gender).ToList();

                Assert.IsTrue(distinctGenders.SequenceEqual(allGenders));
                foreach (var pet in apiResult.PetsByGender)
                {
                    Assert.IsTrue(pet.PetNames.Count() > 0);
                }
            }
        }
    }
}
