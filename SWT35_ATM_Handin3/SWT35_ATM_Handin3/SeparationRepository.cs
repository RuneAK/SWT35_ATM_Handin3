using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3
{
    public class SeparationRepository : ISeperationRepository
    {
        private List<SeparationEvent> _separations = new List<SeparationEvent>();

        public void AddSeperationEvent(SeparationEvent e)
        {
            var tempSepEvent1 = Get(e.Tag1, e.Tag2);
	        var tempSepEvent2 = Get(e.Tag2, e.Tag1);
            if (tempSepEvent1 == null && tempSepEvent2 == null)
                _separations.Add(e);
        }

        public void DeleteSeperationEvent(SeparationEvent e)
        {
            var tempSepEvent = Get(e);
            if (tempSepEvent == null) return;
            _separations.Remove(tempSepEvent);
        }

        public SeparationEvent Get(string tag1, string tag2)
        {
		        return _separations.FirstOrDefault(i => i.Tag1 == tag1 && i.Tag2 == tag2);
        }

	    public SeparationEvent Get(SeparationEvent e)
	    {
		    return _separations.Find(i => i.Tag1 == e.Tag1 && i.Tag2 == e.Tag2);
		}

	    public List<SeparationEvent> GetAll()
	    {
		    return _separations;
	    }
    }
}
