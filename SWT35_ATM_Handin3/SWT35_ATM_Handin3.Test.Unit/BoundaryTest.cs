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
    public class BoundaryTest
    {
        [TestCase(1, 1, 1)]
        [TestCase(0, 1, 1)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 0)]
        [TestCase(0, 0, 1)]
        [TestCase(0, 1, 0)]
        [TestCase(1, 0, 0)]
        [TestCase(0, 0, 0)]
        public void Boundary_Postive_ReturnTrue(int x, int y, int alt)
        {
            Assert.That(new Boundary(x, y, alt).Positive(), Is.True);
        }

        [TestCase(-1, -1, -1)]
        [TestCase(0, -1, -1)]
        [TestCase(-1, 0, 1)]
        [TestCase(-1, -1, 0)]
        [TestCase(0, 0, -1)]
        [TestCase(0, -1, 0)]
        [TestCase(-1, 0, 0)]
        public void Boundary_Postive_ReturnFalse(int x, int y, int alt)
        {
            Assert.That(new Boundary(x, y, alt).Positive(), Is.False);
        }
    }
}
