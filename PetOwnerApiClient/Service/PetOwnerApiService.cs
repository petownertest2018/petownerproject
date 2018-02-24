using PetOwnerApiClient.BusinessLogic;
using PetOwnerApiClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.Service
{
    public class PetOwnerApiService : IPetOwnerApiService
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _uri;
        public PetOwnerApiService(HttpClient httpClient, Uri uri)
        {
            _httpClient = httpClient;
            _uri = uri;
        }

        public async Task<PetOwnerApiServiceResult> GetPetOwnerJson() 
        {
            using (_httpClient)
            {
                try
                {
                    var response = await _httpClient.GetAsync(_uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        return new PetOwnerApiServiceResult { PetOwnerJson = json };
                    }
                    else
                    {
                        return new PetOwnerApiServiceResult { Error = "Pet Owner Api could not respond with result." };
                    }
                }
                catch
                {
                    return new PetOwnerApiServiceResult { Error = "Error occured while communicating to Pet Owner Api." };
                }
            }
        }
    }
}
