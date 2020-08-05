using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOFavorite
{
    public class DTOFavorite
    {
        public long eventId { get; set; }

        public string fv_name { get; set; }

        public DateTime fv_date { get; set; }

        public string comment { get; set; }

        public DTOFavorite(long eventId, string fv_name, DateTime fv_date,
            string comment)
        {
            this.fv_name = fv_name;
            this.eventId = eventId;
            this.fv_date = fv_date;
            this.comment = comment;
        }

        public override bool Equals(object obj)
        {
            return obj is DTOFavorite favorite &&
                   eventId == favorite.eventId &&
                   fv_name == favorite.fv_name &&
                   fv_date == favorite.fv_date &&
                   comment == favorite.comment;
        }

        public override int GetHashCode()
        {
            int hashCode = -185030377;
            hashCode = hashCode * -1521134295 + eventId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(fv_name);
            hashCode = hashCode * -1521134295 + fv_date.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(comment);
            return hashCode;
        }
    }
}