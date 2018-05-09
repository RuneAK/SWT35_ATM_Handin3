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

        //[TestCase("one", "two", "three", "four")]
        //public void RenderSeparations_ASeparation_DisplayFunctionsCalled(string tag1, string tag2, string tag3, string tag4)
        //{
        //    //Arrange
        //    DateTime testTime = new DateTime(2000, 1, 1, 1, 1, 1);
        //    var separationOne = new Separation(tag1, tag2, testTime);
        //    var separationTwo = new Separation(tag3, tag4, testTime);
        //    var separationList = new List<ISeparation>();
        //    separationList.Add(separationOne);
        //    separationList.Add(separationTwo);
        //    var args = new EventSeparation(separationList);

        //    //Act
        //    _separationDetector.SeparationsUpdated += Raise.EventWith(args);

        //    //Assert
        //    _display.Received(1).Write("***Separations***");
        //    _display.Received(1).Write($"Tag1: " + separationOne.Tag1 + " Tag2: " + separationOne.Tag2 + separationOne.TimeStamp);
        //    _display.Received(1).Write($"Tag1: " + separationTwo.Tag1 + " Tag2: " + separationTwo.Tag2 + separationTwo.TimeStamp);
        //}
    }
}
