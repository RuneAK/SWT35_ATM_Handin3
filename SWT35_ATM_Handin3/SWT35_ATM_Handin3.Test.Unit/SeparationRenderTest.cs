using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SWT35_ATM_Handin3.Domain;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Test.Unit
{
    [TestFixture()]
    public class SeparationRenderTest
    {
        //Stubs
        private IDisplay _display;
        private ISeparationDetector _separationDetector;
		//Fields
		private Point _pointOne;
	    private Point _pointTwo;
		//Unit under test
		private SeparationRender _uut;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _display = Substitute.For<IDisplay>();
            _separationDetector = Substitute.For<ISeparationDetector>();
			//Fields
			
			//Unit under test
			_uut = new SeparationRender(_separationDetector, _display);
        }
		
		[Test]
		public void RenderSeparations_ASeparation_DisplayFunctionsCalled()
		{
			//Arrange
			var separationList = new List<ISeparation>();
			var separationOne = new Separation("tag1", "tag2", new DateTime(2000, 1, 1, 1, 1, 1));
			var separationTwo = new Separation("tag3", "tag4", new DateTime(2000, 1, 1, 1, 1, 1));
			separationList.Add(separationOne);
			separationList.Add(separationTwo);
			var args= new EventSeparation(separationList);

			//Act
			_separationDetector.SeparationsUpdated += Raise.EventWith(args);

			//Assert
			_display.Received(1).Write("***Separations***");
			_display.Received(1).Write($"Tag1: " + separationOne.Tag1 + " Tag2: " + separationOne.Tag2 + separationOne.TimeStamp);
			_display.Received(1).Write($"Tag1: " + separationTwo.Tag1 + " Tag2: " + separationTwo.Tag2 + separationTwo.TimeStamp);
		}		
	}
}
