using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.SportEventService.Exceptions
{
    /// <summary>
    /// Public <c>CategoryNotExistsException</c> which captures the error categoryName is not valid
    /// </summary>
    [Serializable]
    public class CategoryNotExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryExistsException"/> class.
        /// </summary>
        /// <param name="categoryName"><c>categoryId</c> that causes the error.</param>
        public CategoryNotExistsException(string categoryName)
            : base("Incorrect categoryName exception => categoryId = " + categoryName)
        {
            this.CategoryName = categoryName;
        }

        /// <summary>
        /// Stores the category Name of the exception
        /// </summary>
        /// <value>The Name of the category.</value>
        public string CategoryName { get; private set; }
    }
}