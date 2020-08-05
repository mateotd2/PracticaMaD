using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOComment;
using Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOFavorite;
using Es.Udc.DotNet.PracticaMaD.Model.FavoritesDao;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.SportEventService
{
    public interface ISportEventService
    {
        [Inject]
        ISportEventDao SportEventDao { set; }

        [Inject]
        ICommentDao CommentDao { set; }

        [Inject]
        IUserProfileDao UserProfileDao { set; }

        [Inject]
        ITagDao TagDao { set; }

        [Inject]
        ICategoryDao CategoryDao { set; }

        [Inject]
        IFavoritesDao FavoritesDao { set; }

        [Transactional]
        List<String> GetCategories();

        /// <summary>
        /// Finds the comments from events.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>List of DTOComment</returns>
        /// <exception cref="EventNotExistsException"/>
        [Transactional]
        List<DTOComment> FindComments(long eventId, int startIndex, int count);

        /// <summary>
        /// Finds the events by keywords and category(can be null).
        /// </summary>
        /// <param name="keyword">The keywords.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <param name="category">The category.</param>
        /// <returns>SportEventBlock wich is List of Events</returns>
        [Transactional]
        SportEventBlock FindEvents(string keyword, int startIndex, int count, string category = "");

        ///// <summary>
        ///// Creates a Category.
        ///// </summary>
        ///// <param name="categoryName">The category identifier.</param>
        ///// <exception cref="CategoryExistsException"/>
        //[Transactional]
        //void CreateCategory(string categoryName);

        /// <summary>
        /// Creates a sport Event.
        /// </summary>
        /// <param name="categoryName">The category identifier.</param>
        /// <param name="eventDate">The event date.</param>
        /// <param name="eventDescription">The event description.</param>
        /// <param name="eventName">The event name.</param>
        /// <returns>Id of the SportEvent.</returns>
        /// <exception cref="CategoryNotExistsException"/>
        [Transactional]
        //long CreateSportEvent(string categoryName, DateTime eventDate,
        //    string eventDescription, string eventName);

        /// <summary>
        /// User Add new Comment with a comm description, collection of tags and sportEvent returns
        /// comment user Id
        /// </summary>
        /// <param name="userLogin"><c>userLogin</c> user Login.</param>
        /// <param name="sportEventId"><c>sportEventId</c> event id.</param>
        /// <param name="commentDescription"><c>commentDescription</c> comment description.</param>
        /// <param name="tags"><c>tags</c> collection of tags.</param>
        /// <returns>Id of the comment.</returns>
        /// <exception cref="UserNotFoundException"/>
        /// <exception cref="SportEventNotFoundException"/>
        [Transactional]
        long AddComment(string userLogin, long sportEventId, string commentDescription, List<String> tags = null);

        /// <summary>
        /// Updates the tag comment.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="tags">The tags.</param>
        [Transactional]
        void UpdateTagComment(string userLogin, long commentId, List<String> tags);

        /// <summary>
        /// Add event to favorites.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <param name="sportEventId">The sport event identifier.</param>
        /// <param name="name"></param>
        /// <param name="comment"></param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="SportEventNotFoundException"></exception>
        /// <exception cref="FavoriteExistsException"></exception>
        /// <exception cref="InternalProblemException"></exception>
        [Transactional]
        void AddToFavorites(string userLogin, long sportEventId, string name, string comment);

        /// <summary>Delete event from favorites.</summary> <param name="userLogin">The user
        /// login.</param> <param name="sportEventId">The sport event identifier.</param> <exception
        /// cref="UserNotFoundException"/> <exception cref="SportEventNotFoundException"/ <exception
        /// cref="FavoriteNotExistsException"/ <exception cref="InternalProblemException"/

        [Transactional]
        void DeleteFromFavorites(string userLogin, long sportEventId);

        /// <summary>
        /// List Favorite Events from User
        /// </summary>
        /// <param name="userLogin"><c>userLogin</c> user Login.</param>
        /// <returns>List of Favorite Events</returns>
        /// <exception cref="UserNotFoundException"/>
        List<DTOFavorite> ListFavorites(string userLogin);

        /// <summary>
        /// Update text from a comment
        /// </summary>
        /// <param name="commentId"><c>commentId</c> The comment id.</param>
        /// <param name="comment_text"><c>comment_text</c> The new comment text.</param>
        void UpdateComment(long commentId, string comment_text);

        DTOComment getComment(long commentId);
    }
}