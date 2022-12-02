using System;
using NUnit.Framework;

namespace Libary.UnitTest
{
    [TestFixture]
    public class LibaryAccount_NUNIT
    {
        #region NUNIT Tests for LoanBook
        [Test]
        public void LoanBook_ChildTriesToGetAvailablChildrenBook_ReturnTrueandUpdateList()
        {
            //Arrange
            Reader reader = new Reader("john", "snow", Reader.ReaderType.Child);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.ChildrenBook, Book.BookStatus.intheLibary);

            bool actual;
            //Act

            actual = libaryAccount.LoanBook(book);

            //Assert
            Assert.That(actual == true, "bug: a child reder didn't mange to loan a children book");
            Assert.IsTrue(libaryAccount.LoanBooks.Contains(book), "bug: we took a book out of the libary,however is the loan book list");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we took a book out of the library,however it is still ma");
            Assert.AreEqual(book.Status, Book.BookStatus.OutOfTheLibrary, "bug: we took a book out of the libary,however is the loan book list");
        }

        [Test]
        public void LoanBook_AdultdTriesToGetAvailbleChildrenBook_ReturnFalseandNotUpdateList()
        {
            //Arrange
            Reader reader = new Reader("john", "snow", Reader.ReaderType.Adult);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.ChildrenBook, Book.BookStatus.intheLibary);


            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => libaryAccount.LoanBook(book));
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "bug: we took a book out of the libary,however its in the loan book list");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we took a book out of the library,however it is still ma");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "BUG : thee srtutus was modified");
        }
        [Test]
        public void LoanBook_CostumerרHasDebtAndHisTryingToGetAdultBook_returnsfalse()
        {
            //Arrange

            Reader reader = new Reader("john", "snow", Reader.ReaderType.Adult);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            libaryAccount.OwnerDebt = 0.1;

            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.intheLibary);

            //Act & Assert
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "bug: we didnt even add the book cause costumer has debt");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we didnt even add the book cause costumer has debt");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "BUG : book supposed to be in the libary costumer has debt couldnt take it");


        }
        [Test]
        public void LoanBook_ChiledTriedToGetReservedBookFromAnotherChild_returnFalseAndNotUpdatList()
        {
            // Arrange

            Reader reader = new Reader("john", "snow", Reader.ReaderType.Child);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.ChildrenBook, Book.BookStatus.Reserevd);

            libaryAccount.RreservedBooks.Add(book);

            Reader reader1 = new Reader("Bob", "Marley", Reader.ReaderType.Child);

            LibaryAccount libaryAccount1 = new LibaryAccount(reader1);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => libaryAccount1.LoanBook(book));
            Assert.IsFalse(libaryAccount1.LoanBooks.Contains(book), "bug: Book Already Reserved By somone Else");
            Assert.IsFalse(libaryAccount1.RreservedBooks.Contains(book), "bug : Book Already Reserved By somone Else");
            Assert.IsTrue(book.Status == Book.BookStatus.Reserevd, "BUG : book status shouldnt be changed ");
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "BUG: Book is Reserved not Loaned");
            Assert.IsTrue(libaryAccount.RreservedBooks.Contains(book), "BUG: Book supposed to be in this child reservedbooks ");

        }
        #endregion


    }
}
