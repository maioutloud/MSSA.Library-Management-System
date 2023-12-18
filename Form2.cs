using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignments_11._2
{
    // ASSIGNMENTS 11.2 : BOOKS
    // CCAD12 : MAI LOR
    public partial class Form2 : Form
    {
        // connect to bookrepository
        BookRepository bookRepository;

          //----------------------------------------------------------------------------------------------------------//
         //___[ INITIALIZE AND LOAD FORM2 ]__________________________________________________________________________//
        //      ** custom parameter to pull values from Form1 when updating or adding **
        public Form2(string isbn = null, string name = "", string author = "", string description = "", bool isUpdate = false)
        {
            InitializeComponent();
            
            // create instance of book reposity
            bookRepository = new BookRepository();
            
            // disable isbn textbox
            txtIsbn.Enabled = false;

            // UPDATE BUTTON SEQUENCE
                 /* - set textboxes with pulled book values
                /   - HIDE 'ADD' button */
            if (isUpdate)
            {
                txtIsbn.Text = isbn;
                txtName.Text = name;
                txtAuthor.Text = author;
                txtDescription.Text = description;
                btnAdd.Visible = false;
            }
            else
            {
            // ADD BUTTON SEQUENCE
                 /* - randomly generated isbn-10 values & disable
                /   - HIDE 'UPDATE' button 
                        - '? :' is a conditional ternary operator and is short-hand for 'if-else' statement //
                        - if isbn is available, use it, else generate a new ISBN                            */
                txtIsbn.Text = !string.IsNullOrEmpty(isbn) ? isbn : bookRepository.GenerateISBN();
                btnUpdate.Visible = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

         //----------------------------------------------------------------------------------------------------------//
        //___[ ** ADD RECORD ** ]___________________________________________________________________________________//
        public void btnAdd_Click(object sender, EventArgs e)
        {
             // *** ISBN auto-generated during Form Load ***
            //  check to make sure all textboxes are not null else book will not be added
            if(!string.IsNullOrEmpty(txtIsbn.Text) 
                && !string.IsNullOrEmpty(txtName.Text) 
                && !string.IsNullOrEmpty(txtAuthor.Text) 
                && !string.IsNullOrEmpty(txtDescription.Text))
            {
                // create new book with user data input
                var newBook = new Book();
                newBook.ISBN = txtIsbn.Text;
                newBook.Name = txtName.Text;
                newBook.Author = txtAuthor.Text;
                newBook.Description = txtDescription.Text;

                // add book to repository and show successful message
                bookRepository.AddRecord(newBook);
                MessageBox.Show("Book is added", "Added!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // return to Form1
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

         //----------------------------------------------------------------------------------------------------------//
        //___[ ** UPDATE RECORD ** ]________________________________________________________________________________//
        private void btnUpdate_Click(object sender, EventArgs e)
        {
             // retrieve isbn from bookRepository
            //  allow user modification
            var isbn = txtIsbn.Text;
            var bookToupdate = bookRepository.GetBook(isbn);
            bookToupdate.Name = txtName.Text;
            bookToupdate.Author = txtAuthor.Text;
            bookToupdate.Description = txtDescription.Text;

             // update record and show message success
            //  return to Form1
            bookRepository.UpdateRecord(isbn, bookToupdate);
            MessageBox.Show("Book is updated", "Updated!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

         //----------------------------------------------------------------------------------------------------------//
        //___[ CANCEL RETURN TO MAIN ]______________________________________________________________________________//
        private void btnCanx_Click(object sender, EventArgs e)
        {
            // return to Form1
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

         //----------------------------------------------------------------------------------------------------------//
        //___[ EXIT APPLICATION ]___________________________________________________________________________________//
        private void btnExit_Click(object sender, EventArgs e)
        {
            // exit application
            Application.Exit();
        }

    }
}
