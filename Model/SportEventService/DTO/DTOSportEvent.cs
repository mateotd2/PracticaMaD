using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOSportEvent
{
    public class DTOSportEvent
    {
        public long eventId { get; set; }
        public string ev_name { get; set; }
        public System.DateTime ev_date { get; set; }
        public string ev_description { get; set; }
        public string ev_category { get; set; }

        public DTOSportEvent(long eventId, string ev_name, System.DateTime ev_date,
                string event_description, string ev_category)
        {
            this.eventId = eventId;
            this.ev_name = ev_name;
            this.ev_date = ev_date;
            this.ev_description = event_description;
            this.ev_category = ev_category;
        }

        public override bool Equals(object obj)
        {
            return obj is DTOSportEvent @event &&
                   eventId == @event.eventId &&
                   ev_name == @event.ev_name &&
                   ev_date == @event.ev_date &&
                   ev_description == @event.ev_description &&
                   ev_category == @event.ev_category;
        }

        public override int GetHashCode()
        {
            int hashCode = 1721425831;
            hashCode = hashCode * -1521134295 + eventId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ev_name);
            hashCode = hashCode * -1521134295 + ev_date.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ev_description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ev_category);
            return hashCode;
        }
    }
}