using PetOwnerApiClient.BusinessLogic;
using PetOwnerApiClient.Interfaces;
using PetOwnerApiClient.Service;
using ApiClient =  PetOwnerApiClient.Client.PetOwnerApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.Factory
{
    public class PetOwnerApiClientFactory : IPetOwnerApiClientFactory
    {
        private List<KeyValuePair<ApiClientType, IPetOwnerApiClient>> apiClients = new List<KeyValuePair<ApiClientType, IPetOwnerApiClient>>();
        public void RegisterApiClient(ApiClientType apiClientType, IPetOwnerApiClient apiClient)
        {
            apiClients.Add(new KeyValuePair<ApiClientType, IPetOwnerApiClient>(apiClientType, apiClient));
        }
        public IPetOwnerApiClient GetApiClient(ApiClientType apiClientType)
        {
            var apiClient = apiClients.FirstOrDefault(x => x.Key == apiClientType);
            if (!apiClient.Equals(default(KeyValuePair<ApiClientType, IPetOwnerApiClient>)))
                return apiClient.Value;
            else
                return null;
        }
    }
}
