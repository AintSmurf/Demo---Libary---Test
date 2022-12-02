using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Libary.UnitTest
{

    [TestClass]
    public class LibraryAccounTests
    {
        #region TimeoutFunction d:
        [TestMethod]
        [Timeout(3)]
        public void TimeoutLoanBook_ChildTriesToGetAvailablChildrenBook_ReturnTrueandUpdateList()
        {
            //Arrange
            Reader reader = new Reader("john", "snow", Reader.ReaderType.Child);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.ChildrenBook, Book.BookStatus.intheLibary);

            bool actual;
            //Act

            actual = libaryAccount.LoanBook(book);

            //Assert
            Assert.IsTrue(actual, "bug: a child reader didn't manage to loan a children book");
            Assert.IsTrue(libaryAccount.LoanBooks.Contains(book), "bug: we took a book out of the libary,however its in the the loan book list");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we took a book out of the library,however it is still in the libary");
            Assert.AreEqual(book.Status, Book.BookStatus.OutOfTheLibrary, "bug: we took a book out of the libary,however is the loan book list");
        }
        #endregion d:

        #region dataRow functions b:
        [TestMethod]
        [DataRow("john", "snow", Reader.ReaderType.Child, true)]
        [DataRow("johnny", "dep", Reader.ReaderType.Child, true)]
        [DataRow("bob", "marley", Reader.ReaderType.Child, true)]
        public void DataRow_LoanBook_ChildTriesToGetAvailablChildrenBook_ReturnTrueandUpdateList(String FirstName, String LastName, Reader.ReaderType r, bool expected)
        {
            //Arrange
            Reader reader = new Reader(FirstName, LastName, r);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.ChildrenBook, Book.BookStatus.intheLibary);

            bool actual;
            //Act

            actual = libaryAccount.LoanBook(book);

            //Assert
            Assert.AreEqual(actual, expected);
            Assert.IsTrue(actual, "bug: a child reder didn't mange to loan a children book");
            Assert.IsTrue(libaryAccount.LoanBooks.Contains(book), "bug: we took a book out of the libary,however its still in the loan book list");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug :we took a book out of the library,however it is still in the libary");
            Assert.AreEqual(book.Status, Book.BookStatus.OutOfTheLibrary, "bug: we took a book out of the libary,however is the loan book list");
        }
        [TestMethod]
        [DataRow("john", "snow", Reader.ReaderType.Adult)]
        [DataRow("johnny", "dep", Reader.ReaderType.Adult)]
        [DataRow("bob", "marley", Reader.ReaderType.Adult)]
        public void DataRow_LoanBook_AdultdTriesToGetAvailbleChildrenBook_ReturnFalseandNotUpdateList(String FirstName, String LastName, Reader.ReaderType r)
        {
            //Arrange
            Reader reader = new Reader(FirstName, LastName, r);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.ChildrenBook, Book.BookStatus.intheLibary);


            //Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => libaryAccount.LoanBook(book));
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "bug: we took a book out of the libary,however its in the loan book list");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we took a book out of the library,however it is still ma");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "BUG : the status was modified");
        }
        [TestMethod]
        [DataRow("john", "snow", Reader.ReaderType.Adult,false, 5)]
        public void DataRow_LoanBook_CostumerרHasDebtAndHisTryingToGetAdultBook_returnsfalse(String FirstName, String LastName, Reader.ReaderType r, bool expected, double debt)
        {
            Reader reader = new Reader(FirstName, LastName, r);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            libaryAccount.OwnerDebt = debt;

            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.intheLibary);

            //Act & Assert
            Assert.IsFalse(expected);
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "bug: we didnt even add the book cause costumer has debt");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we didnt even add the book cause costumer has debt");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "BUG : book supposed to be in the libary costumer has debt couldnt take it");
        }
        #endregion

        #region functions for LoanBook  
        [TestMethod]
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
            Assert.IsTrue(actual, "bug: a child reder didn't mange to loan a children book");
            Assert.IsTrue(libaryAccount.LoanBooks.Contains(book), "bug: we took a book out of the libary,however is the loan book list");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we took a book out of the library,however it is still ma");
            Assert.AreEqual(book.Status, Book.BookStatus.OutOfTheLibrary, "bug: we took a book out of the libary,however is the loan book list");
        }

        [TestMethod]
        public void LoanBook_AdultdTriesToGetAvailbleChildrenBook_ReturnFalseandNotUpdateList()
        {
            //Arrange
            Reader reader = new Reader("john", "snow", Reader.ReaderType.Adult);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.ChildrenBook, Book.BookStatus.intheLibary);


            //Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => libaryAccount.LoanBook(book));
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "bug: we took a book out of the libary,however its in the loan book list");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we took a book out of the library,however it is still ma");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "BUG : thee srtutus was modified");
        }
        [TestMethod]
        public void LoanBook_CostumerרHasDebtAndHisTryingToGetAdultBook_returnsfalse() 
        {
            //Arrange

            Reader reader = new Reader("john", "snow", Reader.ReaderType.Adult);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            libaryAccount.OwnerDebt = 0.1;

            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.intheLibary);

            //Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => libaryAccount.LoanBook(book));
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "bug: we didnt even add the book cause costumer has debt");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we didnt even add the book cause costumer has debt");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "BUG : book supposed to be in the libary costumer has debt couldnt take it");


        }
        [TestMethod]
        public void LoanBook_ChiledTriedToGetReservedBookFromAnotherChild_returnFalseAndNotUpdatList()
        {
            // Arrange

            Reader reader = new Reader("john", "snow", Reader.ReaderType.Child);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.ChildrenBook, Book.BookStatus.Reserevd);

            libaryAccount.RreservedBooks.Add(book);

            Reader reader1= new Reader("Bob","Marley", Reader.ReaderType.Child);

            LibaryAccount libaryAccount1 = new LibaryAccount(reader1);

            //Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => libaryAccount1.LoanBook(book));
            Assert.IsFalse(libaryAccount1.LoanBooks.Contains(book), "bug: Book Already Reserved By somone Else");
            Assert.IsFalse(libaryAccount1.RreservedBooks.Contains(book), "bug : Book Already Reserved By somone Else");
            Assert.IsTrue(book.Status == Book.BookStatus.Reserevd, "BUG : book status shouldnt be changed ");
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book),"BUG: Book is Reserved not Loaned");
            Assert.IsTrue(libaryAccount.RreservedBooks.Contains(book), "BUG: Book supposed to be in this child reservedbooks ");

        }
        [TestMethod]
        public void LoanBook_CostumerTriesToOrderMoreThanTheLimit_returnFalse()
        {
            // Arrange
            Reader reader = new Reader("john", "snow", Reader.ReaderType.Child);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.intheLibary);

            libaryAccount.LoanBooks.Add(book);
            libaryAccount.LoanBooks.Add(book);
            libaryAccount.LoanBooks.Add(book);

            //Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => libaryAccount.LoanBook(book), "Customer Reached Max");
            Assert.IsTrue(libaryAccount.LoanBooks.Contains(book), "bug: customer has books loaned");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : he coulndt Order cause he reached max");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "Status shouldnt be changed");
        }

        #endregion

        #region functions for ReturnBook
        [TestMethod]
        public void ReturnBook_CostumerTriesToReturnBookHeDidntTake_returnsFalse()
        {
            //Arrange
            Reader reader = new Reader("john", "snow", Reader.ReaderType.Adult);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.intheLibary);

            //Act & Assert
            Assert.ThrowsException<InvalidOperationException>(()=> libaryAccount.ReturnBook(book));
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "BUG: costumer Didnt Have The Book");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "BUG : costumer Didnt Have The Book");
            Assert.AreEqual(book.Status, Book.BookStatus.intheLibary, "BUG: suuposed to be in the libary in first place costumer didnt took or loan the book");


        }
        [TestMethod]
        public void ReturnBook_CostumerReturnsBookHeTookAndSomeoneElseOrdersIt_returnTrue()
        {
            //Arrange

            Reader reader = new Reader("john", "snow", Reader.ReaderType.Adult);
            Reader reader1 = new Reader("Bob", "Marley", Reader.ReaderType.Adult);

            LibaryAccount libaryAccount = new LibaryAccount(reader);
            LibaryAccount libaryAccount1 = new LibaryAccount(reader1);

            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.intheLibary);
            libaryAccount.LoanBook(book);

            //Act
            libaryAccount.ReturnBook(book);
            libaryAccount1.OrderBook(book);
            
            

            //Assert
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "BUG: costumer Didnt Have The Book");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "BUG : costumer Didnt Have The Book But he Ordered it");
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "BUG: costumer Didnt Have The Book");
            Assert.IsTrue(libaryAccount1.RreservedBooks.Contains(book), "BUG : costumer Ordered The book");
            Assert.AreEqual(book.Status, Book.BookStatus.Reserevd, "BUG: suuposed to be in the libary and Reserved");

        }
        #endregion

        #region functions for XML e:
        private TestContext context;
        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }
        }
        //add the absolute path to the xml file after the @
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"", "Debt", DataAccessMethod.Sequential)]
        public void XML_LoanBook_CostumerרHasDebtAndHisTryingToGetAdultBook_returnsfalse()
        {
            String firstname = TestContext.DataRow["val1"].ToString();
            String lastname = TestContext.DataRow["val2"].ToString();
            String expectedResult = TestContext.DataRow["expectedResult"].ToString();
            String debt = TestContext.DataRow["debt"].ToString();

            System.Diagnostics.Trace.WriteLine("the following data came from the XML:" +
                                                 "\nFirstName: " + firstname +
                                                 "\nLastName: " + lastname +
                                                 "\nExpectedResult: " + expectedResult +
                                                 "\nDebt: " + debt);
            Reader reader = new Reader(firstname, lastname, Reader.ReaderType.Child);

            LibaryAccount libaryAccount = new LibaryAccount(reader);
            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.intheLibary);

            //Act
            bool result = Convert.ToBoolean(expectedResult);
            double db = Convert.ToDouble(debt);
            libaryAccount.OwnerDebt = db;

            //Assert
            Assert.IsFalse(result,"The function Supposed to Fail cause costumer has debt");
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "bug: we didnt even add the book cause costumer has debt");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we didnt even add the book cause costumer has debt");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "BUG : book supposed to be in the libary costumer has debt couldnt take it");
        }
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"", "func",DataAccessMethod.Sequential)]
        public void XML_LoanBook_ChildTriesToGetAvailablChildrenBook_ReturnTrueandUpdateList()
        {
            String firstname = TestContext.DataRow["val1"].ToString();
            String lastname = TestContext.DataRow["val2"].ToString();
            String expectedResult = TestContext.DataRow["expectedResult"].ToString();

            System.Diagnostics.Trace.WriteLine("the following data came from the XML:" +
                                                 "\nFirstName: "+firstname+
                                                 "\nLastName: "+lastname+
                                                 "\nExpectedResult: " + expectedResult);
            Reader reader = new Reader(firstname, lastname, Reader.ReaderType.Child);


            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.ChildrenBook, Book.BookStatus.intheLibary);

            bool actual;
            bool result = Convert.ToBoolean(expectedResult);
            //Act

            actual = libaryAccount.LoanBook(book);

            //Assert
            Assert.AreEqual(actual, result);
            Assert.IsTrue(actual, "bug: a child reder didn't mange to loan a children book");
            Assert.IsTrue(libaryAccount.LoanBooks.Contains(book), "bug: we took a book out of the libary,however is the loan book list");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we took a book out of the library,however it is still ma");
            Assert.AreEqual(book.Status, Book.BookStatus.OutOfTheLibrary, "bug: we took a book out of the libary,however is the loan book list");


        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"", "func1", DataAccessMethod.Sequential)]
        public void XML_LoanBook_ChiledTriedToGetReservedBookFromAnotherChild_returnFalseAndNotUpdatList() 
        {
            String firstname = TestContext.DataRow["val1"].ToString();
            String lastname = TestContext.DataRow["val2"].ToString();
            String Sfirstname = TestContext.DataRow["val3"].ToString();
            String Slastname = TestContext.DataRow["val4"].ToString();
            String expectedResult = TestContext.DataRow["expectedResult"].ToString();

            System.Diagnostics.Trace.WriteLine("the following data came from the XML:" +
                                                 "\nFirstName: " + firstname +
                                                 "\nLastName: " + lastname +
                                                  "\nFirstName: " + Sfirstname +
                                                 "\nLastName: " + Slastname +
                                                 "\nExpectedResult: " + expectedResult);

            Reader reader = new Reader(firstname, lastname, Reader.ReaderType.Child);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.ChildrenBook, Book.BookStatus.Reserevd);

            libaryAccount.RreservedBooks.Add(book);

            Reader reader1 = new Reader(Sfirstname, Slastname, Reader.ReaderType.Child);

            LibaryAccount libaryAccount1 = new LibaryAccount(reader1);
            bool result = Convert.ToBoolean(expectedResult);

            //Act & Assert
            Assert.IsFalse(result, "BUG: The fuction supposed to return false");
            Assert.ThrowsException<InvalidOperationException>(() => libaryAccount1.LoanBook(book));
            Assert.IsFalse(libaryAccount1.LoanBooks.Contains(book), "bug: Book Already Reserved By somone Else");
            Assert.IsFalse(libaryAccount1.RreservedBooks.Contains(book), "bug : Book Already Reserved By somone Else");
            Assert.IsTrue(book.Status == Book.BookStatus.Reserevd, "BUG : book status shouldnt be changed ");
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "BUG: Book is Reserved not Loaned");
            Assert.IsTrue(libaryAccount.RreservedBooks.Contains(book), "BUG: Book supposed to be in this child reservedbooks ");
        }

        #endregion

        #region funcions for cancelOrder
        [TestMethod]
        public void cancelOrder_costumerCancelsOrder_returnsTrue()
        {
            //Arange
            Reader reader = new Reader("john", "snow", Reader.ReaderType.Adult);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.Reserevd);
            
            libaryAccount.RreservedBooks.Add(book);

            bool actual;

            //Act 

            actual = libaryAccount.cancelOrder(book);

            //Assert
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "BUG: Book is Reserved not Loaned");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "BUG: costumer canceled his order ");
            Assert.IsTrue(actual, "Costumer did cancel the Order");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "BUG : costumer canceled his order");

        }

        [TestMethod]
        public void cancelOrder_costumerCancelsOrderThatOutsidTheLibary_returnsTrue()
        {
            //Arange
            Reader reader = new Reader("john", "snow", Reader.ReaderType.Adult);

            Reader reader1 = new Reader("Bob", "Marley", Reader.ReaderType.Adult);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            LibaryAccount libaryAccount1 = new LibaryAccount(reader1);

            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.intheLibary);

            bool actual;

            //Act 
            libaryAccount1.LoanBook(book);
            libaryAccount1.ReturnBook(book);
            libaryAccount.OrderBook(book);
            actual = libaryAccount.cancelOrder(book);

            //Assert
            Assert.IsFalse(libaryAccount1.LoanBooks.Contains(book), "BUG: costumer already returned the book");
            Assert.IsFalse(libaryAccount1.RreservedBooks.Contains(book), "BUG: costumer already returned the book ");
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "BUG: this costumer didnt loan the book");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "BUG: costumer canceled his order ");
            Assert.IsTrue(actual, "Costumer did cancel the Order");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "BUG : costumer canceled his order");

        }


        #endregion

        #region functions for orderBook
        [TestMethod]
        public void OrderBook_customerHasDebtAndHisTryingToGetAdultBook_returnsfalse()
        {
            //Arrange

            Reader reader = new Reader("john", "snow", Reader.ReaderType.Adult);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            libaryAccount.OwnerDebt = 0.1;

            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.intheLibary);

            //Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => libaryAccount.OrderBook(book));   
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "bug: we didnt even add the book cause costumer has debt");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we didnt even add the book cause costumer has debt");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "BUG : book supposed to be in the libary costumer has debt couldnt take it");


        }
        [TestMethod]
        public void OrderBook_customerTriesToOrderBookThatSomeoneAlreadyTook_returnTrue()
        {
            //Arrange

            Reader reader = new Reader("john", "snow", Reader.ReaderType.Adult);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.OutOfTheLibrary);

            bool actual;

            //Act
            actual = libaryAccount.OrderBook(book);

            //Assert
            Assert.IsTrue(actual, "customer should be able to Order");
            Assert.IsTrue(libaryAccount.RreservedBooks.Contains(book), "customer has ordered the book");
            Assert.IsTrue(book.Status == Book.BookStatus.OutOfTheLibraryAndReserved,"customer did order that already outside the libary");
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book),"customer did order the book he didnt loan");

        }
        [TestMethod]
        public void OrderBook_ChildTriesToOrderAdultBook_retunsFalse()
        {
            //Arrange
            Reader reader = new Reader("john", "snow", Reader.ReaderType.Child);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.intheLibary);

            //Act && Assert
            Assert.ThrowsException<InvalidOperationException>(() => libaryAccount.OrderBook(book),"Child shouldnt Be Able To Order Adult Book");
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "bug: we didnt even add the book cause costumer is child");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we didnt even add the book cause costumer is child");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "Status shouldnt be changed");


        }
        [TestMethod]
        public void OrderBook_AdultTriesToOrderChildBook_retunsFalse()
        {
            //Arrange
            Reader reader = new Reader("john", "snow", Reader.ReaderType.Adult);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.ChildrenBook, Book.BookStatus.intheLibary);

            //Act && Assert
            Assert.ThrowsException<InvalidOperationException>(() => libaryAccount.OrderBook(book), "Adult shouldnt Be Able To Order Adult Book");
            Assert.IsFalse(libaryAccount.LoanBooks.Contains(book), "bug: we didnt even add the book cause costumer is Adult");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : we didnt even add the book cause costumer is Adult");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "Status shouldnt be changed");


        }
        [TestMethod]
        public void OrderBook_CostumerTriesToOrderMoreThanTheLimit_returnFalse()
        {
            // Arrange
            Reader reader = new Reader("john", "snow", Reader.ReaderType.Child);

            LibaryAccount libaryAccount = new LibaryAccount(reader);

            Book book = new Book(Book.BookType.AdultBook, Book.BookStatus.intheLibary);
            
            libaryAccount.LoanBooks.Add(book);
            libaryAccount.LoanBooks.Add(book);
            libaryAccount.LoanBooks.Add(book);

            //Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => libaryAccount.OrderBook(book),"Customer Reached Max");
            Assert.IsTrue(libaryAccount.LoanBooks.Contains(book), "bug: customer has books loaned");
            Assert.IsFalse(libaryAccount.RreservedBooks.Contains(book), "bug : he coulndt Order cause he reached max");
            Assert.IsTrue(book.Status == Book.BookStatus.intheLibary, "Status shouldnt be changed");
        }

        #endregion
    }


}
