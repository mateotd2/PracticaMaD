using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.SportEventService.Exceptions
{
    /// <summary>
    /// Public <c>CategoryExistsException</c> which captures the error categoryName is not valid
    /// </summary>
    [Serializable]
    public class CategoryExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryExistsException"/> class.
        /// </summary>
        /// <param name="categoryName"><c>categoryId</c> that causes the error.</param>
        public CategoryExistsException(string categoryName)
            : base("Incorrect categoryName exception => categoryId = " + categoryName)
        {
            this.categoryName = categoryName;
        }

        /// <summary>
        /// Stores the category Name of the exception
        /// </summary>
        /// <value>The Name of the category.</value>
        public string categoryName { get; private set; }
    }
}