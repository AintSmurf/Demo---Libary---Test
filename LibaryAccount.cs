using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary
{
    public class LibaryAccount
    {
        private const int Max_NUM_OF_LOAN_BOOKS = 3;
        public int maxnumOfLoanBook
        {
            get { return Max_NUM_OF_LOAN_BOOKS; }
        }
        #region Private variabls
        private Reader owner;
        private List<Book> loanBooks;
        private List<Book> reservedBooks;
        private double ownerDebt;

        #endregion


        #region Properties
        public Reader Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public List<Book> LoanBooks
        {
            get { return loanBooks; }
            set { loanBooks = value; }
        }

        public List<Book> RreservedBooks
        {
            get { return reservedBooks; }
            set { reservedBooks = value; }
        }

        public double OwnerDebt
        {
            get { return ownerDebt; }
            set { ownerDebt = value; }

        }

        #endregion


        #region Constructors
        public LibaryAccount(Reader reader)
        {
            owner = reader;
            loanBooks = new List<Book>();
            reservedBooks = new List<Book>();
            ownerDebt = 0.0;//אין חוב ללקוח 

        }
        #endregion


        #region Methods
        public bool LoanBook(Book bookToLoan)
        {
            if(!((bookToLoan.Status == Book.BookStatus.intheLibary) 
                || 
               ((bookToLoan.Status == Book.BookStatus.Reserevd) && (reservedBooks.Contains(bookToLoan)))))
            {
                throw new InvalidOperationException("The Book not in the Libary or its someone already loaned it");
            }
            if (LoanBooks.Count == Max_NUM_OF_LOAN_BOOKS)
            {
                throw new InvalidOperationException("customer already has max number of loan books");
            }
            if (LoanBooks.Contains(bookToLoan))
            {
                throw new InvalidOperationException("customer already has this book");
            }
            if (OwnerDebt > 0)
            {
                throw new InvalidOperationException("customer has debt,so he load additional book");
            }
            if (
                 !(owner.Type == Reader.ReaderType.Adult && bookToLoan.Type == Book.BookType.AdultBook)
                &&
                    !(owner.Type == Reader.ReaderType.Child && bookToLoan.Type == Book.BookType.ChildrenBook)
              )
                throw new InvalidOperationException("no match between types");

            LoanBooks.Add(bookToLoan);
            bookToLoan.Status = Book.BookStatus.OutOfTheLibrary;

            return true;
        }

        public bool ReturnBook(Book bookToReturn)
        {
            if (!LoanBooks.Contains(bookToReturn))
            {
                throw new InvalidOperationException("customer cannot return a book that he didnt loan ");
            }
            loanBooks.Remove(bookToReturn);

            bookToReturn.Status = Book.BookStatus.intheLibary;

            if (bookToReturn.Status == Book.BookStatus.OutOfTheLibrary)
                bookToReturn.Status = Book.BookStatus.intheLibary;
            else if (bookToReturn.Status == Book.BookStatus.OutOfTheLibraryAndReserved)
                bookToReturn.Status = Book.BookStatus.Reserevd;

            return true;
        }

        public bool OrderBook(Book bookToOrder) 
        {
             if((bookToOrder.Status == Book.BookStatus.OutOfTheLibraryAndReserved) && (reservedBooks.Contains(bookToOrder)))
            {
                throw new InvalidOperationException("The Book not in the Libary or someone already loaned it");
            }
            if (reservedBooks.Count == Max_NUM_OF_LOAN_BOOKS)
            {
                throw new InvalidOperationException("customer already has max number of loan books");
            }
            if (reservedBooks.Contains(bookToOrder))
            {
                throw new InvalidOperationException("someone already loaned this book");
            }
            if (OwnerDebt > 0)
            {
                throw new InvalidOperationException("customer has debt,so he cannot loan additional book");
            }
            if (
                 !(owner.Type == Reader.ReaderType.Adult && bookToOrder.Type == Book.BookType.AdultBook)
                &&
                    !(owner.Type == Reader.ReaderType.Child && bookToOrder.Type == Book.BookType.ChildrenBook)
              )
                throw new InvalidOperationException("no match between types");

            reservedBooks.Add(bookToOrder);
            if (bookToOrder.Status == Book.BookStatus.OutOfTheLibrary)
                bookToOrder.Status = Book.BookStatus.OutOfTheLibraryAndReserved;
            else if (bookToOrder.Status == Book.BookStatus.intheLibary)
                bookToOrder.Status = Book.BookStatus.Reserevd;

            return true;
        }

        public bool cancelOrder(Book bookToCancel) 
        {

            if (!reservedBooks.Contains(bookToCancel))
            {
                throw new InvalidOperationException("Customer cannot cancel a book he didnt order ");
            }
            reservedBooks.Remove(bookToCancel);


            if (bookToCancel.Status == Book.BookStatus.Reserevd)
                bookToCancel.Status = Book.BookStatus.intheLibary;
            else if (bookToCancel.Status == Book.BookStatus.OutOfTheLibraryAndReserved)
                bookToCancel.Status = Book.BookStatus.Reserevd;

            return true;
        }


        #endregion
    }
}

