using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Linq_to_Library
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // creating the library databse object
        //
        LibraryDBDataContext Lib_Data = new LibraryDBDataContext();


        // strings for the books
        //
        string GTitle = "";
        string GAuthor = "";
        string GReturn = "";




        // form load, sets the combo boxes 
        //
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbx_search.SelectedIndex = 1;
            cmbx_order.SelectedIndex = 2;
            cmbx_rentTime.SelectedIndex = 0;
        }





        // connect menu, when clicked displays the database contents in the datagrid
        //
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            
            // LINQ query to get all database contents
            // stored in results
            //
            var results = from item in Lib_Data.Books
                          orderby item.ID
                          select item;

            // this loop displays each row of the database in the rows of the datagrid
            //
            foreach (var item in results)
            {
                dataGridView1.Rows.Add(item.ID, item.Title, item.Author, item.Year, item.Available, item.Return_Date, item.Late);
            }

            // orders the datagrid according to the order combo box
            //
            dataGridView1.Sort(dataGridView1.Columns[cmbx_order.SelectedIndex], ListSortDirection.Ascending);

        }






        // insert menu, opens the insert form
        //
        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Insert_Book insB = new Insert_Book(this);
            insB.ShowDialog();
        }






        // insert method, when called (from the insert form) takes in the books
        // details and sets them to the corresponding fields of a book object
        // the book is then inserted into the database
        //
        public void bookInsert(string title, string author, string year, string avail)
        {
            Book BookIn = new Book();

            BookIn.Title = title;
            BookIn.Author = author;
            BookIn.Year = year;
            BookIn.Available = avail;

            Lib_Data.Books.InsertOnSubmit(BookIn);
            Lib_Data.SubmitChanges();

            // connect button click is called to refresh the datagrid
            //
            connectToolStripMenuItem_Click(null, null);
        }






        // search button, used to allow the user to search the database
        //
        private void btn_search_Click(object sender, EventArgs e)
        {

            // creating a list to allow search results to be added to it
            //
            List<Book> myresults = new List<Book>();

            // clears the datagrid
            //
            dataGridView1.Rows.Clear();

            // setting search string to null, used to stop errors in the LINQ query
            //
            string search = null;

            // if there is text in the search textbox then search is set to that text
            // otherwise it remains as null
            //
            if (txb_search.TextLength > 0)
            {
                search = txb_search.Text;
            }

            // clears the search textbox
            //
            txb_search.Clear();

            // if search combo box is set to ID
            //
            if (cmbx_search.SelectedIndex == 0)
            {
                // LINQ query to get all database entries where ID is = to the users search
                // if the search variable is null then no results are found and the program does not crash
                //
                var results = from item in Lib_Data.Books
                              where (item.ID == (int)Convert.ToInt32(search) || search == null)
                              select item;

                // loop adds the results to the list
                //
                foreach (Book item in results)
                {
                    myresults.Add(item);
                }
            }

            // if search combo box is set to Title
            //
            if (cmbx_search.SelectedIndex == 1)
            {
                var results = from item in Lib_Data.Books
                              where (item.Title == search || search == null)
                              select item;

                foreach (Book item in results)
                {
                    myresults.Add(item);
                }
            }

            // if search combo box is set to Author
            //
            if (cmbx_search.SelectedIndex == 2)
            {
                var results = from item in Lib_Data.Books
                              where (item.Author == search || search == null)
                              select item;

                foreach (Book item in results)
                {
                    myresults.Add(item);
                }
            }

            // if search combo box is set to Year
            //
            if (cmbx_search.SelectedIndex == 3)
            {
                var results = from item in Lib_Data.Books
                              where (item.Year == search || search == null)
                              select item;

                foreach (Book item in results)
                {
                    myresults.Add(item);
                }
            }

            // loops through the list of results and adds them to the datagrid
            //
            foreach (var item in myresults)
            {
                dataGridView1.Rows.Add(item.ID, item.Title, item.Author, item.Year, item.Available, item.Return_Date, item.Late);
            }

            // orders the datagrid according to the order combo box
            //
            dataGridView1.Sort(dataGridView1.Columns[cmbx_order.SelectedIndex], ListSortDirection.Ascending);
        }





        // delete button, deletes selected row of the database
        //
        private void btn_delete_Click(object sender, EventArgs e)
        {
            // trys to delete, and if no item is selected then an error message is shown
            //
            try
            {
                // stored the ID of the currently selected row
                //
                int delete_cell = (int)Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());

                // LINQ query to get database entry matching selected row
                //
                var remove = from item in Lib_Data.Books
                             where item.ID == delete_cell
                             select item;

                // if remove is not = to null, or has a result, loop deletes selected row
                // from the database
                //
                if (remove != null)
                {
                    foreach (var item in remove)
                    {
                        Lib_Data.Books.DeleteOnSubmit(item);
                    }
                }

                // trys to submit the changes and gives an error message if it cant
                // 
                try
                {
                    Lib_Data.SubmitChanges();
                }
                catch
                {
                    MessageBox.Show("Error: Failed to delete");
                }

                // connect click called to refresh the datagrid
                //
                connectToolStripMenuItem_Click(null, null);
            }
            catch
            {
                MessageBox.Show("Error: No item selected");
            }
        }





        // if the order combo box is changed then the datagrid order is changed accordingly
        //
        private void cmbx_order_SelectedIndexChanged(object sender, EventArgs e)
        {
                dataGridView1.Sort(dataGridView1.Columns[cmbx_order.SelectedIndex], ListSortDirection.Ascending);
        }






        // rent book out, when clicked the selected book is updated with a return time, 
        // availability is set to no and late is set to no
        //
        private void btn_rent_Click(object sender, EventArgs e)
        {
            string temp = "";
            string RentTime = "";

            // trys to update and displays an error message if not possible
            //
            try
            {
                // if the rent time combo box is set to 1 week then 7 days are added to 
                // a today DateTime instance and substringed to allow it to be entered into the database
                //
                if (cmbx_rentTime.SelectedIndex == 0)
                {
                    temp = DateTime.Today.AddDays(7.0).ToString();
                    RentTime = temp.Substring(0, 10);
                }

                // 2 weeks
                //
                if (cmbx_rentTime.SelectedIndex == 1)
                {
                    temp = DateTime.Today.AddDays(14.0).ToString();
                    RentTime = temp.Substring(0, 10);
                }

                // 3 weeks
                //
                if (cmbx_rentTime.SelectedIndex == 2)
                {
                    temp = DateTime.Today.AddDays(21.0).ToString();
                    RentTime = temp.Substring(0, 10);
                }

                // four weeks
                //
                if (cmbx_rentTime.SelectedIndex == 3)
                {
                    temp = DateTime.Today.AddDays(28.0).ToString();
                    RentTime = temp.Substring(0, 10);
                }

                // 1 day late, used for testing 'check rentals' feature
                //
                if (cmbx_rentTime.SelectedIndex == 4)
                {
                    temp = DateTime.Today.AddDays(-1.0).ToString();
                    RentTime = temp.Substring(0, 10);
                }


                // updates the book of the selected rows ID
                //
                Book update = Lib_Data.Books.Single(b => b.ID == (int)Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));

                // sets the fields of the book
                //
                update.Return_Date = RentTime;
                update.Late = "No";
                update.Available = "No";

                // submits the changes
                //
                Lib_Data.SubmitChanges();

                // connect click called to refresh datagrid
                //
                connectToolStripMenuItem_Click(null, null);
            }
            catch
            {
                MessageBox.Show("Error: No item selected");
            }
        }




        // retrun book, when clicked updates the selected book as returned
        //
        private void btn_return_Click(object sender, EventArgs e)
        {
            try
            {
                // updates book of selected rows ID
                Book updt = Lib_Data.Books.Single(p => p.ID == (int)Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));

                // sets the fields of the book
                //
                updt.Return_Date = null;
                updt.Late = null;
                updt.Available = "Yes";

                // submits the changes
                //
                Lib_Data.SubmitChanges();

                connectToolStripMenuItem_Click(null, null);
            }
            catch
            {
                MessageBox.Show("Error: No item selected");
            }
        }





        // check late rentals, used to loop through the database and compare the return times
        // of rented books to todays date
        //
        private void button1_Click(object sender, EventArgs e)
        {
            // gets all results from database
            //
            var results = from item in Lib_Data.Books
                          select item;

            // time variables
            //
            string rentedTime = "";
            int day = 0;
            int month = 0;
            int year = 0;

            // loop compares return date to todays date and updates the database
            // to say wether the book is late or not
            //
            foreach (var item in results)
            {
                // var for return date
                //
                rentedTime = item.Return_Date;

                // if there is a return time to check
                //
                if (rentedTime != null)
                {
                    // substrings used to set day, month and year
                    //
                    day = (int)Convert.ToInt32(rentedTime.Substring(0, 2));
                    month = (int)Convert.ToInt32(rentedTime.Substring(3, 2));
                    year = (int)Convert.ToInt32(rentedTime.Substring(6, 4));


                    // time of return datetime instance is created and set to the time vars
                    //
                    DateTime TimeOfRent = new DateTime(year, month, day);

                    // todays date datetime instance is created
                    //
                    DateTime Today = DateTime.Today;

                    // compares the two times, returns a minus number if the date of return
                    // is less than the today date
                    //
                    if (TimeOfRent.CompareTo(Today) < 0)
                    {
                        // if the late field is not already set to yes then BGW sends an alert email to a specified address
                        // and the late book is set to late in the database
                        // 
                        if (item.Late != "Yes")
                        {
                            GTitle = item.Title;
                            GAuthor = item.Author;
                            GReturn = item.Return_Date;

                            backgroundWorker1.RunWorkerAsync();

                            item.Late = "Yes";
                            Lib_Data.SubmitChanges();
                        }
                    }
                }
            }

            connectToolStripMenuItem_Click(null, null);
        }




        // email sending bgw, sends email to a specified address using an SMTP server
        //
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // creates a message object
                //
                MailMessage message = new MailMessage();

                // details of the message, dest address, host address, message etc
                //
                message.To.Add("specified email");
                message.Subject = "Late Rental Alert";
                message.From = new MailAddress("specified email");
                message.Body = "\n\n" + GTitle + " by " + GAuthor + " is late, it was suppose to be returned on " + GReturn + ", layeth the smackdown.";

                // creates an SMTP client object, settings are set, message is sent
                //
                SmtpClient smtp = new SmtpClient("smtp.live.com");
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("specified email", "password");
                smtp.Send(message);
            }
            catch
            {
                MessageBox.Show("Error: Email not sent, no connection");
            }
        }  





























        // not used
        //
        private void rentalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        
    }
}
