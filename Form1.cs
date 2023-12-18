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

    public partial class Form1 : Form
    {
        // give access to book repository db
        BookRepository bookRepository = new BookRepository();

         //----------------------------------------------------------------------------------------------------------//
        //___[ INITIALIZE AND LOAD FORM1 ]__________________________________________________________________________//
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // initialize grid
            gridBook.DataSource = bookRepository.GetAllRecords();
        }

         //----------------------------------------------------------------------------------------------------------//
        //___[ ** ADD RECORD ** ]___________________________________________________________________________________//
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // goto Form2 to add new book data
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Close();
        }

         //---------------------------------------------------------------------------------------------------------//
        //___[ ** UPDATE RECORD ** ]_______________________________________________________________________________//
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // collect values from row cells
            var isbn = gridBook.CurrentRow.Cells[0].Value.ToString();
            var name = gridBook.CurrentRow.Cells[1].Value.ToString();
            var author = gridBook.CurrentRow.Cells[2].Value.ToString();
            var description = gridBook.CurrentRow.Cells[3].Value.ToString();

            // connect to Form2
            // pass values to textboxes in Form2; the last parameter is set to true for update method
            Form2 form2 = new Form2(isbn, name, author, description, true);
            form2.ShowDialog();
            this.Close();
        }

        //---------------------------------------------------------------------------------------------------------//
        //___[ REFRESH ]___________________________________________________________________________________________//
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // retrieve data from book repository (used specifically after record deletion)
            gridBook.DataSource = bookRepository.GetAllRecords();
        }

         //----------------------------------------------------------------------------------------------------------//
        //___[ DELETE RECORD ]______________________________________________________________________________________//
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // pull isbn for selected row and utilize delete method to remove record from db
            var isbn = gridBook.CurrentRow.Cells[0].Value;
            var bookTodel = bookRepository.GetBook((string)isbn);
            bookRepository.DeleteRecord(bookTodel);
            MessageBox.Show("Book is deleted", "Deleted!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

         //---------------------------------------------------------------------------------------------------------//
        //___[ EXIT APPLICATION ]__________________________________________________________________________________//
        private void btnExit_Click(object sender, EventArgs e)
        {
            // exit application
            Application.Exit();
        }
    }
}
