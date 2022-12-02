using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary
{
    public class Book
    {
        #region enums
        public enum BookType
        {
            ChildrenBook,
            AdultBook
        }

        public enum BookStatus
        {
            intheLibary,//על המדף
            Reserevd,//המצא בספריה אבל לא ניתן לזמין אותו כי מוזמן
            OutOfTheLibrary,//מושאל
            OutOfTheLibraryAndReserved//מוזמן ומושאל
        }
        #endregion

        #region private variabls
        private string id;
        private BookType type;
        private BookStatus status;

        #endregion

        #region Properties
        public string Id
        {
            get { return id; }
        }
        public BookType Type
        {
            get { return type; }
        }
        public BookStatus Status
        {
            get { return status; }
            set { status = value; }
        }


        #endregion

        #region Counstructors

        public Book()
        {
            Guid guid = Guid.NewGuid();
            id = guid.ToString();
            type = BookType.AdultBook;
            status = BookStatus.intheLibary;
        }
        public Book(BookType type, BookStatus status)
        {
            Guid guid = Guid.NewGuid();
            id = guid.ToString();
            this.type = type;
            this.status = status;
        }



        #endregion
    }
}
