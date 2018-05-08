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

        [TestCase(10000, 10000, 500)]
        [TestCase(90000, 10000, 500)]
        [TestCase(10000, 90000, 500)]
        [TestCase(90000, 90000, 20000)]
        [TestCase(10000, 90000, 20000)]
        [TestCase(90000, 10000, 20000)]
        [TestCase(90000, 90000, 500)]
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

        [TestCase(9999, 10000, 10000)]
        [TestCase(10000, 9999, 10000)]
        [TestCase(10000, 10000, 499)]
        [TestCase(90001, 10000, 10000)]
        [TestCase(10000, 90001, 10000)]
        [TestCase(10000, 10000, 20001)]
        [TestCase(10000, 90001, 20001)]
        [TestCase(90001, 10000, 20001)]
        [TestCase(90001, 90001, 20001)]
        [TestCase(9999, 10000, 10000)]
        [TestCase(9999, 9999, 10000)]
        [TestCase(9999, 9999, 499)]
        public void CalculateAirspace_PointOutsideAirspace_ReturnsFalse(int x, int y, int alt)
        {
            //Arrange
            var pointOutsideAirspace = new Point(x, y, alt);

            //Act
            bool test = _uut.CalculateWithinAirspace(pointOutsideAirspace);

            //Assert
            Assert.That(test, Is.EqualTo(false));
        }

        [TestCase(90001, 20000, 10000)]
        [TestCase(20000, 90001, 10000)]
        [TestCase(20000, 20000, 20001)]
        [TestCase(90001, 90001, 10000)]
        [TestCase(90001, 90001, 20001)]
        public void AirspaceConstructor_LowerBoundInvalidParameters_ThrowsException(int x, int y, int alt)
        {
            //Arrange
            var lowerBound = new Point(x, y, alt);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => _uut = new Airspace(
                lowerBound));
        }

        [TestCase(9999, 20000, 10000)]
        [TestCase(20000, 9999, 10000)]
        [TestCase(20000, 20000, 499)]
        [TestCase(9999, 9999, 10000)]
        [TestCase(9999, 9999, 499)]
        [TestCase(20000, 9999, 499)]
        [TestCase(9999, 20000, 499)]
        public void AirspaceConstructor_UpperBoundInvalidParamters_ThrowsException(int x, int y, int alt)
        {
            //Arrange
            var upperBound = new Point(x, y, alt);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => _uut = new Airspace(null,
                upperBound));
        }
    }
}
