using Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOSportEvent;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.SportEventService
{
    public class SportEventBlock
    {
        public List<DTOSportEvent> result { get; private set; }
        public bool existMoreSportEvents { get; private set; }

        public SportEventBlock(List<DTOSportEvent> result, bool existMoreSportEvents)
        {
            this.result = result;
            this.existMoreSportEvents = existMoreSportEvents;
        }
    }
}