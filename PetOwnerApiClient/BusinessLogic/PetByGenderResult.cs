﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerApiClient.BusinessLogic
{
    public class PetByGenderResult
    {
        public IEnumerable<PetByGender> PetsByGender { get; set; }
        public string Error { get; set; }
    }
}
