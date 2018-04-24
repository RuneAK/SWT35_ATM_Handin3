using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT35_ATM_Handin3.Interfaces
{
    public interface ISeperationRepository
    {
        void AddSeperationEvent(SeparationEvent e);

        void DeleteSeperationEvent(SeparationEvent e);

        SeparationEvent Get(string tag1, string tag2);
    }
}
