using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignments_11._2
{
    // ASSIGNMENTS 11.2 : BOOKS
    // CCAD12 : MAI LOR

      //***********************//
     //      [ C R U D ]      //
    //***********************// 

    interface CRUD
    {
        void AddRecord(Book newBook);           // create book
        void DeleteRecord(Book book);           // delete book
        ICollection<Book> GetAllRecords();      // read book
        void UpdateRecord(string ISBN, Book book); // update book
        Book GetBook(string ISBN);              // find book
    }

      //*******************************************//
     //      [ BOOK REPOSITORY CLASS : CRUD]      //
    //*******************************************// 

    internal class BookRepository : CRUD
    {
        // create instance of SQL database
        // this is set to static so no instance is accidentally added
        public static BOOKdbEntities Entities = new BOOKdbEntities();

         //----------------------------------------------------------------------------------------------------------//
        //___[ ADD ]________________________________________________________________________________________________//
        public void AddRecord(Book newBook)
        {
            Entities.Books.Add(newBook);    // add new book
            Entities.SaveChanges();         // fires insert query
        }

         //----------------------------------------------------------------------------------------------------------//
        //___[ ADD RECORD ]_________________________________________________________________________________________//
        public void DeleteRecord(Book book)
        {
            Entities.Books.Remove(book);    // remove book
            Entities.SaveChanges();         // fires insert query
        }

         //----------------------------------------------------------------------------------------------------------//
        //___[ GET RECORDS ]________________________________________________________________________________________//
        public ICollection<Book> GetAllRecords()
        {
            return Entities.Books.ToList(); // return books
        }

         //----------------------------------------------------------------------------------------------------------//
        //___[ GET BOOK ]___________________________________________________________________________________________//
        public Book GetBook(string ISBN)
        {
            return Entities.Books.Find(ISBN); // find ISBN
        }

         //----------------------------------------------------------------------------------------------------------//
        //___[ UPDATE RECORD ]______________________________________________________________________________________//
        public void UpdateRecord(string ISBN, Book book)
        {
            var booktoupdate = Entities.Books.Find(ISBN);   // retrieve ISBN and allows for editing values in db
            booktoupdate.ISBN = book.ISBN;
            booktoupdate.Name = book.Name;
            booktoupdate.Author = book.Author;
            booktoupdate.Description = book.Description;
            Entities.SaveChanges();                         // fires insert query
        }

         //----------------------------------------------------------------------------------------------------------//
        //___[ GENERATE ISBN ]______________________________________________________________________________________//
        public string GenerateISBN()
        {
            // ISBN contains 10 or 13 numerical values
            // this sample generates 10 random numbers
            // formatted to resemble ISBN-10 
            Random isbn = new Random();
            int group = isbn.Next(0,1);
            int publisher = isbn.Next(100,999);
            int title = isbn.Next(10000, 99999);
            int check = isbn.Next(1, 9);
            // ISBN-10 format
            string formatIsbn = $"{group}-{publisher}-{title}-{check}";
            return formatIsbn;
        }
    }
}
