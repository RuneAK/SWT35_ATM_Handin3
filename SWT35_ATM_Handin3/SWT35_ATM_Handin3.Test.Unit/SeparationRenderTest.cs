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
        private SeparationRender _uut;
        //Unit under test

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

        }
    }
}
