using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetOwnerApiClient.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerUnitTest
{

    [TestClass]
    public class AlphabeticComparerTest
    {
        [TestMethod]
        public void Should_return_minus_1_when_Fido_x_Compare_With_Garfield_y()
        {
            var comparer = new AlphabeticComparer();
            var actual = comparer.Compare("Fido", "Garfield");
            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void Should_return_1_when_Garfield_x_Compare_With_Fido_y()
        {
            var comparer = new AlphabeticComparer();
            var actual = comparer.Compare("Garfield", "Fido");
            Assert.AreEqual(1, actual);

        }
    }
}