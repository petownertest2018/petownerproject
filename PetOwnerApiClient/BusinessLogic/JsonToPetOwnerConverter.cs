using PetOwnerApiClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetOwnerApiClient.DataContract;
using Newtonsoft.Json;

namespace PetOwnerApiClient.BusinessLogic
{
    public class JsonToPetOwnerConverter : IJsonToPetOwnerConverter
    {
        public PetOwnerConvertResult Convert(string json)
        {
            PetOwner[] resultOwners = new PetOwner[] { };
            try
            {
                resultOwners = JsonConvert.DeserializeObject<PetOwner[]>(json);
                return new PetOwnerConvertResult { PetOwners = resultOwners }; 
            }
            catch
            {
                return new PetOwnerConvertResult { Error = "Error occured while converting Pet Owner json data." };
            }            
        }
    }
}
