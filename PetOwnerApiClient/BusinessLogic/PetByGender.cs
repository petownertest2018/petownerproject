using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.BusinessLogic
{
    public class PetByGender
    {
        public string Gender { get; set; }
        public IEnumerable<string> PetNames { get; set; }
    }
}
