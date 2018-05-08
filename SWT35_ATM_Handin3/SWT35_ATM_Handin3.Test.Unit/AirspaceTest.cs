using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SWT35_ATM_Handin3.Domain;

namespace SWT35_ATM_Handin3.Test.Unit
{
    [TestFixture()]
    public class AirspaceTest
    {
        private Airspace _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new Airspace();
        }

        [TestCase(15000, 15000, 10000)]
        public void CalculateAirspace_PointWithInAirspace_ReturnsTrue(int x, int y, int alt)
        {
            //Arrange
            var pointWithinAirspace = new Point(x, y, alt);
            
            //Act
            bool test = _uut.CalculateWithinAirspace(pointWithinAirspace);

            //Assert
            Assert.That(test, Is.EqualTo(true));
        }

        [TestCase(1, 10000, 10000)]
        public void CalculateAirspace_PointOutsideAirspace_ReturnsFalse(int x, int y, int alt)
        {
            //Arrange
            var pointOutsideAirspace = new Point(x, y, alt);

            //Act
            bool test = _uut.CalculateWithinAirspace(pointOutsideAirspace);

            //Assert
            Assert.That(test, Is.EqualTo(false));
        }
    }
}
