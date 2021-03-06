//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    
    public partial class SportEvent
    {
        public SportEvent()
        {
            this.Comment = new HashSet<Comment>();
            this.Recommendation = new HashSet<Recommendation>();
            this.Favorites = new HashSet<Favorite>();
        }
    
        public long eventId { get; set; }
        public string ev_name { get; set; }
        public System.DateTime ev_date { get; set; }
        public string ev_description { get; set; }
        public long ev_category { get; set; }
    
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_CommentSportEventId
        /// </summary>
        public virtual ICollection<Comment> Comment { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_Recommendation_EventId
        /// </summary>
        public virtual ICollection<Recommendation> Recommendation { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_Category
        /// </summary>
        public virtual Category Category { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_EVENT
        /// </summary>
        public virtual ICollection<Favorite> Favorites { get; set; }
    
    	/// <summary>
    	/// A hash code for this instance, suitable for use in hashing algorithms and data structures 
    	/// like a hash table. It uses the Josh Bloch implementation from "Effective Java"
        /// Primary key of entity is not included in the hash calculation to avoid errors
    	/// with Entity Framework creation of key values.
    	/// </summary>
    	/// <returns>
    	/// Returns a hash code for this instance.
    	/// </returns>
    	public override int GetHashCode()
    	{
    	    unchecked
    	    {
    			int multiplier = 31;
    			int hash = GetType().GetHashCode();
    
    			hash = hash * multiplier + (ev_name == null ? 0 : ev_name.GetHashCode());
    			hash = hash * multiplier + ev_date.GetHashCode();
    			hash = hash * multiplier + (ev_description == null ? 0 : ev_description.GetHashCode());
    			hash = hash * multiplier + ev_category.GetHashCode();
    
    			return hash;
    	    }
    
    	}
        
        /// <summary>
        /// Compare this object against another instance using a value approach (field-by-field) 
        /// </summary>
        /// <remarks>See http://www.loganfranken.com/blog/687/overriding-equals-in-c-part-1/ for detailed info </remarks>
    	public override bool Equals(object obj)
    	{
    
            if (ReferenceEquals(null, obj)) return false;        // Is Null?
            if (ReferenceEquals(this, obj)) return true;         // Is same object?
            if (obj.GetType() != this.GetType()) return false;   // Is same type?
    	    
            SportEvent target = obj as SportEvent;
    
    		return true
               &&  (this.eventId == target.eventId )       
               &&  (this.ev_name == target.ev_name )       
               &&  (this.ev_date == target.ev_date )       
               &&  (this.ev_description == target.ev_description )       
               &&  (this.ev_category == target.ev_category )       
               ;
    
        }
    
    
    	public static bool operator ==(SportEvent  objA, SportEvent  objB)
        {
            // Check if the objets are the same SportEvent entity
            if(Object.ReferenceEquals(objA, objB))
                return true;
      
            return objA.Equals(objB);
    }
    
    
    	public static bool operator !=(SportEvent  objA, SportEvent  objB)
        {
            return !(objA == objB);
        }
    
    
        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
    	public override String ToString()
    	{
    	    StringBuilder strSportEvent = new StringBuilder();
    
    		strSportEvent.Append("[ ");
           strSportEvent.Append(" eventId = " + eventId + " | " );       
           strSportEvent.Append(" ev_name = " + ev_name + " | " );       
           strSportEvent.Append(" ev_date = " + ev_date + " | " );       
           strSportEvent.Append(" ev_description = " + ev_description + " | " );       
           strSportEvent.Append(" ev_category = " + ev_category + " | " );       
            strSportEvent.Append("] ");    
    
    		return strSportEvent.ToString();
        }
    
    
    }
}
