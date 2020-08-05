using Castle.Core.Internal;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOComment;
using Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOFavorite;
using Es.Udc.DotNet.PracticaMaD.Model.DTO.DTOSportEvent;
using Es.Udc.DotNet.PracticaMaD.Model.FavoritesDao;
using Es.Udc.DotNet.PracticaMaD.Model.RecommendationGroupService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventDao;
using Es.Udc.DotNet.PracticaMaD.Model.SportEventService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.SportEventService
{
    public class SportEventService : ISportEventService
    {
        [Inject]
        public ISportEventDao SportEventDao { private get; set; }

        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        [Inject]
        public ITagDao TagDao { private get; set; }

        [Inject]
        public IFavoritesDao FavoritesDao { private get; set; }

        /// <summary>
        /// Check if User exists, if not throw exception
        /// </summary>
        /// <param name="loginName"><c>loginName</c> User login name.</param>
        /// <returns>UserProfile.</returns>
        /// <exception cref="UserNotFoundException"/>
        private UserProfile CheckUser(string loginName)
        {
            UserProfile user;
            try
            {
                user = UserProfileDao.FindByLoginName(loginName);
            }
            catch (InstanceNotFoundException)
            {
                throw new UserNotFoundException(loginName);
            }
            return user;
        }

        /// <summary>
        /// Check if sportEvent exists, if not throw exception
        /// </summary>
        /// <param name="sportEventId"><c>sportEventId</c> Sport Event ifentifier.</param>
        /// <returns>SportEvent</returns>
        /// <exception cref="SportEventNotFoundException"/>
        private SportEvent CheckSportEvent(long sportEventId)
        {
            SportEvent sportEvent;
            try
            {
                sportEvent = SportEventDao.Find(sportEventId);
            }
            catch (InstanceNotFoundException)
            {
                throw new SportEventNotFoundException(sportEventId);
            }
            return sportEvent;
        }

        [Transactional]
        public List<DTOComment> FindComments(long eventId, int startIndex, int count)
        {
            List<DTOComment> result = new List<DTOComment>();

            if (!SportEventDao.Exists(eventId))
            {
                throw new EventNotExistsException(eventId);
            }

            List<Comment> listComment = CommentDao.FindCommentsByEventIdOrderByDate(eventId, startIndex, count);
            UserProfile userOwner;
            foreach (var comment in listComment)
            {
                userOwner = UserProfileDao.Find(comment.ownerId);
                result.Add(new DTOComment(comment.commentId, userOwner.loginName, comment.eventId,
                    comment.comment_description, comment.publishDate, tagsToListOfStrings(comment.Tags.ToList())));
            }
            return result;
        }

        private List<string> tagsToListOfStrings(List<Tag> tags)
        {
            List<string> result = new List<string>();
            foreach (var tag in tags)
            {
                result.Add(tag.tagName);
            }

            return result;
        }

        /// <summary>
        /// Finds the events by keywords and category(can be null).
        /// </summary>
        /// <param name="keyword">The keywords.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <param name="category">The category.</param>
        /// <returns>List of DTOSportEvent</returns>

        #region

        [Transactional]
        //public SportEventBlock FindEvents(string keywords, int startIndex, int count, string category="")
        //{
        //    List<SportEvent> sportEventList = new List<SportEvent>();
        //    Category categoria_busqueda = null;
        //    List<DTOSportEvent> result = new List<DTOSportEvent>() ;
        //    List<String> cadenas = null;
        //    int i = 0;

        // if (!(category == "")) // Busqueda con categoria { categoria_busqueda =
        // CategoryDao.FindByName(category); if (keywords == "") // Sin kewywords-> Todos los
        // eventos con una categoria. { sportEventList = categoria_busqueda.SportEvents.ToList(); }
        // else { cadenas = separateKeywords(keywords); foreach (String element in cadenas) {
        // sportEventList.InsertRange(i, SportEventDao.FindByKeywords(element, startIndex, count+1, categoria_busqueda.categoryId));

        // i += count+1; }

        // }

        // }else //Busqueda sin categoria { if (keywords == "") //Sin keywords -> Todos los eventos
        // { sportEventList=SportEventDao.GetAllElements(); } else // Con keywords -> busqueda por
        // keywords { cadenas = separateKeywords(keywords); foreach (String element in cadenas) {
        // sportEventList.InsertRange(i, SportEventDao.FindByKeywords(element, startIndex, count+1));

        // i += count+1; } } } string category_event = null; foreach (var sportEvent in
        // sportEventList) { category_event = sportEvent.Category.categoryName; result.Add(new
        // DTOSportEvent(sportEvent.eventId, sportEvent.ev_name, sportEvent.ev_date,
        // sportEvent.ev_description, category_event));

        // }

        // bool existMoreSportEvents = (result.Count == count + 1);

        // if (existMoreSportEvents) result.RemoveAt(count);

        // return new SportEventBlock(result, existMoreSportEvents);

        //}
        #endregion

        [Transactional]
        public SportEventBlock FindEvents(string keywords, int startIndex, int count, string category = "")
        {
            long categoryId = 0;
            List<SportEvent> sportEventList = new List<SportEvent>();
            String[] separator = { " " };
            int wordCount = 0, index = 0;

            if (!category.IsNullOrEmpty())
            {
                categoryId = CategoryDao.FindByName(category).categoryId;
            }

            #region Obtain and split words from keywords

            #endregion

            //Separar los keywords

            if (!keywords.IsNullOrEmpty())
            {
                while (index < keywords.Length && char.IsWhiteSpace(keywords[index]))
                    index++;

                while (index < keywords.Length)
                {
                    // check if current char is part of a word
                    while (index < keywords.Length && !char.IsWhiteSpace(keywords[index]))
                        index++;

                    wordCount++;

                    // skip whitespace until next word
                    while (index < keywords.Length && char.IsWhiteSpace(keywords[index]))
                        index++;
                }

                List<String> strlist = keywords.Split(separator, wordCount, StringSplitOptions.RemoveEmptyEntries).ToList();
                int i = 0;
                int length = 0;
                foreach (String element in strlist)
                {
                    sportEventList.InsertRange(i, SportEventDao.FindByKeywords(element, startIndex, count + 1, categoryId));
                    length = sportEventList.Count;
                    i += length;
                }
            }
            //else
            //{
            //    //Podria hacer aqui un insert range
            //    if (categoryId == 0)
            //    {
            //        sportEventList = SportEventDao.GetAllElements();
            //    }
            //    else
            //    {
            //        sportEventList = CategoryDao.Find(categoryId).SportEvents.ToList();
            //    }
            //}

            List<DTOSportEvent> result = new List<DTOSportEvent>();
            foreach (var sportEvent in sportEventList)
            {
                result.Add(new DTOSportEvent(sportEvent.eventId, sportEvent.ev_name,
                    sportEvent.ev_date, sportEvent.ev_description, sportEvent.Category.categoryName));
            }
            bool existMoreSportEvents = (result.Count == count + 1);

            if (existMoreSportEvents)
                result.RemoveAt(count);

            //return result;

            return new SportEventBlock(result, existMoreSportEvents);
        }

        private List<string> separateKeywords(string keywords)
        {
            #region Obtain and split words from keywords
            String[] separator = { " " };

            int wordCount = 0, index = 0;

            while (index < keywords.Length && char.IsWhiteSpace(keywords[index]))
                index++;

            while (index < keywords.Length)
            {
                // check if current char is part of a word
                while (index < keywords.Length && !char.IsWhiteSpace(keywords[index]))
                    index++;

                wordCount++;

                // skip whitespace until next word
                while (index < keywords.Length && char.IsWhiteSpace(keywords[index]))
                    index++;
            }

            //Separar los keywords
            List<String> strlist = keywords.Split(separator, wordCount, StringSplitOptions.RemoveEmptyEntries).ToList();
            #endregion
            return strlist;
        }

        #region CreateCategory
        ///// <summary>
        ///// Creates a new Sport event Category, wich have unique name
        ///// </summary>
        ///// <param name="categoryName"><c>userId</c> category name.</param>
        ///// <exception cref="CategoryExistsException"/>
        //[Transactional]
        //public void CreateCategory(string categoryName)
        //{
        //    if (CategoryDao.Exists(categoryName)){
        //        throw new CategoryExistsException(categoryName);
        //    }
        //    else
        //    {
        //        Category cat = new Category();
        //        cat.categoryName = categoryName;
        //        CategoryDao.Create(cat);
        //    }

        //}
        #endregion
        #region CreateSportEvent

        /// <summary>
        /// Creates a new Sport event with a existing category
        /// </summary>
        /// <param name="categoryName"><c>categoryName</c> category name.</param>
        /// <param name="eventDate"><c>eventDate</c> event date.</param>
        /// <param name="eventDescription"><c>eventDescription</c> event description.</param>
        /// <param name="eventName"><c>eventName</c> event name.</param>
        /// <exception cref="CategoryNotExistsException"/>
        [Transactional]
        public long CreateSportEvent(string categoryName, DateTime eventDate,
            string eventDescription, string eventName)
        {
            Category category;
            SportEvent sportEvent = new SportEvent();
            try
            {
                category = CategoryDao.FindByName(categoryName);
            }
            catch (InstanceNotFoundException)
            {
                throw new CategoryNotExistsException(categoryName);
            }

            sportEvent.Category = category;
            sportEvent.ev_date = eventDate;
            sportEvent.ev_description = eventDescription;
            sportEvent.ev_name = eventName;

            SportEventDao.Create(sportEvent);
            return sportEvent.eventId;
        }

        #endregion

        [Transactional]
        public long AddComment(string userLogin, long sportEventId, string commentDescription, List<String> tags = null)
        {
            UserProfile user = CheckUser(userLogin);

            SportEvent sportEvent = CheckSportEvent(sportEventId);
            string date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            DateTime dateTime = DateTime.UtcNow.Date;

            Comment comment = new Comment
            {
                ownerId = user.userId,
                publishDate = DateTime.UtcNow,
                comment_description = commentDescription,
                eventId = sportEventId,
            };
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    String tagLower = tag.ToLower();
                    if (!TagDao.Exists(tagLower))
                    {
                        Tag newTag = new Tag
                        {
                            tagName = tagLower
                        };
                        newTag.Comment.Add(comment);
                        TagDao.Create(newTag);
                        comment.Tags.Add(newTag);
                    }
                    else
                    {
                        Tag tagInDatabase = TagDao.Find(tagLower);
                        comment.Tags.Add(tagInDatabase);
                        tagInDatabase.Comment.Add(comment);
                    }
                }
            }

            CommentDao.Create(comment);
            return comment.commentId;
        }

        // METODOS TAGS
        /// <summary>
        /// Updates the tags comment.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="tags">The tags.</param>
        [Transactional]
        public void UpdateTagComment(string userLogin, long commentId, List<String> tags)
        {
            Comment comment = CommentDao.Find(commentId);
            comment.Tags.Clear();

            foreach (var tag in tags)
            {
                String tagLower = tag.ToLower();
                if (!TagDao.Exists(tagLower))
                {
                    Tag newTag = new Tag
                    {
                        tagName = tagLower
                    };
                    newTag.Comment.Add(comment);
                    TagDao.Create(newTag);
                    comment.Tags.Add(newTag);
                }
                else
                {
                    Tag tagInDatabase = TagDao.Find(tagLower);
                    comment.Tags.Add(tagInDatabase);
                    tagInDatabase.Comment.Add(comment);
                }
            }
        }

        public List<string> GetCategories()
        {
            List<string> categories = new List<string>();
            categories = this.CategoriesToListOfStrings(CategoryDao.GetAllElements());

            return categories;
        }

        private List<string> CategoriesToListOfStrings(List<Category> categories)
        {
            List<string> result = new List<string>();
            foreach (var category in categories)
            {
                result.Add(category.categoryName);
            }

            return result;
        }

        public void AddToFavorites(string userLogin, long sportEventId, string name, string comment)
        {
            UserProfile user = CheckUser(userLogin);

            SportEvent sportEvent = CheckSportEvent(sportEventId);

            if (FavoritesDao.FindFavoriteByUserIdAndEventId(user.userId, sportEventId) != null)
            {
                throw new DuplicateInstanceException("UserLogin = " + userLogin + " SportEventId = " + sportEventId,
                    typeof(Favorite).FullName);
            }

            Favorite favorite = new Favorite
            {
                fv_event = sportEventId,
                fv_owner = user.userId,
                fv_comment = comment,
                fv_date = DateTime.UtcNow,
                fv_name = name
            };

            FavoritesDao.Create(favorite);
        }

        /// <summary>
        /// Deletes from favorites.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <param name="sportEventId">The sport event identifier.</param>
        /// <exception cref="FavoriteNotExistsException"></exception>
        /// <exception cref="InternalProblemException"></exception>
        public void DeleteFromFavorites(string userLogin, long sportEventId)
        {
            UserProfile user = CheckUser(userLogin);

            SportEvent sportEvent = CheckSportEvent(sportEventId);

            Favorite favorite = FavoritesDao.FindFavoriteByUserIdAndEventId(user.userId, sportEventId);

            //foreach(Favorite element in user.Favorites)
            //{
            //    if (element.fv_event == sportEventId)
            //    {
            //        favorite = element;
            //        break;
            //    }
            //}

            if (favorite == null)
            {
                throw new FavoriteNotExistsException(userLogin, sportEventId);
            }
            else
            {
                try
                {
                    user.Favorites.Remove(favorite);
                    sportEvent.Favorites.Remove(favorite);
                    FavoritesDao.Remove(favorite.fv_Id);
                }
                catch (NotSupportedException e)
                {
                    throw new InternalProblemException(e);
                }
            }
        }

        public List<DTOFavorite> ListFavorites(string userLogin)
        {
            UserProfile user = CheckUser(userLogin);
            SportEvent sportEvent;
            List<DTOFavorite> result = new List<DTOFavorite>();

            foreach (Favorite element in user.Favorites)
            {
                sportEvent = element.SportEvent;
                result.Add(new DTOFavorite(element.fv_event, sportEvent.ev_name, element.fv_date, element.fv_comment));
            }

            return result;
        }

        public void UpdateComment(long commentId, string comment_text)
        {
            Comment comment;
            comment = CommentDao.Find(commentId);
            comment.comment_description = comment_text;

            //UpdateTagComment(commentId,tags);
            CommentDao.Update(comment);
        }

        public DTOComment getComment(long commentId)
        {
            Comment comment;
            comment = CommentDao.Find(commentId);

            //////////FALTA DEVOLVER LOS TAGS DEL COMENTARIO en el DTO
            return new DTOComment(commentId, comment.UserProfile.loginName, comment.eventId, comment.comment_description, comment.publishDate, null);
        }
    }
}