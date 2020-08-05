using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOComment
{
    public class DTOComment
    {
        public long commentId { get; set; }
        public string loginName { get; set; }
        public long eventId { get; set; }
        public string comment_description { get; set; }
        public System.DateTime publishDate { get; set; }
        public List<string> tags { get; set; }

        public DTOComment(long commentId, string loginName, long eventId,
             string comment_description, System.DateTime publishDate, List<string> tags)
        {
            this.commentId = commentId;
            this.loginName = loginName;
            this.eventId = eventId;
            this.comment_description = comment_description;
            this.publishDate = publishDate;
            this.tags = tags;
        }
    }
}