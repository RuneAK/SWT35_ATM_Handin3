using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SWT35_ATM_Handin3;

namespace SWT35_ATM_Handin3.Test.Unit
{
    [TestFixture()]
    public class PointTest
    {
	    private Point _uut;
	    [SetUp]
	    public void SetUp()
	    {
			_uut = new Point();
	    }

	    [Test]
	    public void Test_Point_CanBeSet()
	    {
			//Act
		    _uut.X = 1;
		    _uut.Y = 2;
		    _uut.Alt = 3;

			//Assert
			Assert.That(_uut.X, Is.EqualTo(1));
		    Assert.That(_uut.Y, Is.EqualTo(2));
			Assert.That(_uut.Alt, Is.EqualTo(3));
	    }

	    [Test]
	    public void Test_Point_CanBeSetInContructor()
	    {
		    //Act
		    _uut = new Point(1,2,3);

		    //Assert
		    Assert.That(_uut.X, Is.EqualTo(1));
		    Assert.That(_uut.Y, Is.EqualTo(2));
		    Assert.That(_uut.Alt, Is.EqualTo(3));
	    }
	}
}
