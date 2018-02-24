using Newtonsoft.Json;
using PetOwnerApiClient.BusinessLogic;
using PetOwnerApiClient.DataContract;
using PetOwnerApiClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.Client
{
    public class PetOwnerApiClient : IPetOwnerApiClient
    {
        
        private IPetOwnerFilter _petOwnerFilter;
        private IPetSorter _petSorter;
        private IPetOwnerApiService _apiService;
        private IJsonToPetOwnerConverter _jsonToPetOwnerConverter;
        private IPetOwnerGrouper _grouper;
        public PetOwnerApiClient(IJsonToPetOwnerConverter jsonToPetOwnerConverter,IPetOwnerApiService apiService, IPetOwnerFilter petOwnerFilter, IPetSorter petSorter, IPetOwnerGrouper grouper)
        {
            
            _petOwnerFilter = petOwnerFilter;  
            _petSorter = petSorter;
            _apiService = apiService;
            _jsonToPetOwnerConverter = jsonToPetOwnerConverter;
            _grouper = grouper;
        }
        public async Task<PetByGenderResult> GetPetsByPetOwnerGender()
        {
            var serviceResult = await _apiService.GetPetOwnerJson();
            var result = new PetByGenderResult();

            if (string.IsNullOrEmpty(serviceResult.PetOwnerJson))
            {
                result.Error = serviceResult.Error;
            }
            else
            {
                var converterResult = _jsonToPetOwnerConverter.Convert(serviceResult.PetOwnerJson);
                if (converterResult.PetOwners == null)
                {
                    result.Error = converterResult.Error;
                }
                else
                {
                    var filteredPetOwners = FilterOwnerByCat(converterResult.PetOwners);
                    var groups = GetPetOwnerGroupByGender(filteredPetOwners);
                    result.PetsByGender = GetSortedGroupDetails(filteredPetOwners, groups);
                }                
            }
            return result;
        }
           
        private IEnumerable<PetOwner> FilterOwnerByCat(PetOwner[] owners)
        {
            var catOwners = from o in owners
                        let result = _petOwnerFilter.FilterByPetType(o)
                        where result!=null
                        select result;
            return catOwners;
        }

        private IEnumerable<string> GetPetOwnerGroupByGender(IEnumerable<PetOwner> owners)
        {
            var grp = _grouper.GetGroup(owners);
            return grp;
        }

        private IEnumerable<PetByGender> GetSortedGroupDetails(IEnumerable<PetOwner> owners, IEnumerable<string> groups)
        { 
            foreach (var gender in groups)
            {
                var heading = new PetByGender { Gender = gender };
                var petnames = GetPetNames(owners, gender);
                petnames = _petSorter.Sort(petnames);
                heading.PetNames = petnames;
                yield return heading;
            }
        }

        private IEnumerable<string> GetPetNames(IEnumerable<PetOwner> petOwners, string gender)
        {
            var names = from o in petOwners
                        let filteredGender = _petOwnerFilter.FilterByGender(o, gender)
                        where filteredGender != null
                        from p in o.Pets
                        select p.Name;
            return names;
        }
    }
}
