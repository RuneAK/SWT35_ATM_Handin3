using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SWT35_ATM_Handin3;
using SWT35_ATM_Handin3.Helpers;

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

        [TestCase(10000, 10000, 500, 90000, 90000, 20000)]
        public void Airspace_Default_CanInit(int x1, int y1, int alt1, int x2, int y2,
            int alt2)
        {
            Assert.That(_uut.LowerBound.X, Is.EqualTo(x1));
            Assert.That(_uut.LowerBound.Y, Is.EqualTo(y1));
            Assert.That(_uut.LowerBound.Alt, Is.EqualTo(alt1));
            Assert.That(_uut.UpperBound.X, Is.EqualTo(x2));
            Assert.That(_uut.UpperBound.Y, Is.EqualTo(y2));
            Assert.That(_uut.UpperBound.Alt, Is.EqualTo(alt2));
        }

        [TestCase(10000, 10000, 500)]
        [TestCase(89999, 89999, 19999)]
        public void Airspace_Lowerbound_CanInit(int x, int y, int alt)
        {
            _uut = new Airspace(lowerBound: new Boundary(x, y, alt));
            Assert.That(_uut.LowerBound.X, Is.EqualTo(x));
            Assert.That(_uut.LowerBound.Y, Is.EqualTo(y));
            Assert.That(_uut.LowerBound.Alt, Is.EqualTo(alt));
        }

        [TestCase(10001, 10001, 501)]
        [TestCase(90000, 90000, 20000)]
        public void Airspace_Upperbound_CanInit(int x, int y, int alt)
        {
            _uut = new Airspace(upperBound: new Boundary(x, y, alt));
            Assert.That(_uut.UpperBound.X, Is.EqualTo(x));
            Assert.That(_uut.UpperBound.Y, Is.EqualTo(y));
            Assert.That(_uut.UpperBound.Alt, Is.EqualTo(alt));
        }

        [TestCase(90000, 90000, 20000)]
        [TestCase(90000, 90000, 19999)]
        [TestCase(89999, 90000, 20000)]
        [TestCase(90000, 89999, 20000)]
        [TestCase(89999, 90000, 19999)]
        [TestCase(90000, 89999, 19999)]
        [TestCase(89999, 89999, 20000)]
        public void Airspace_Lowerbound_ThrowsException(int x, int y, int alt)
        {
            Assert.Throws<ArgumentException>(() => _uut = new Airspace(lowerBound: new Boundary(x, y, alt)));
        }

        [TestCase(10000, 10000, 500)]
        [TestCase(10000, 10000, 501)]
        [TestCase(10001, 10000, 500)]
        [TestCase(10000, 10001, 500)]
        [TestCase(10001, 10000, 501)]
        [TestCase(10000, 10001, 501)]
        [TestCase(10001, 10001, 500)]
        public void Airspace_Upperbound_ThrowsException(int x, int y, int alt)
        {
            Assert.Throws<ArgumentException>(() => _uut = new Airspace(upperBound: new Boundary(x, y, alt)));
        }
    }
}
