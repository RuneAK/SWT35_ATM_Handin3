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
    public class TrackRenderTest
    {
        //Stubs
        private IDisplay _display;
        private IUpdate _update;
        //Fields

        //Unit under test
        private TrackRender _uut;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _display = Substitute.For<IDisplay>();
            _update = Substitute.For<IUpdate>();
            //Fields

            //Unit under test
            _uut = new TrackRender(_update, _display);
        }

        [Test]
        private void RenderTracks_ATrack_DisplayFunctionsCalled()
        {

        }
    }
}
