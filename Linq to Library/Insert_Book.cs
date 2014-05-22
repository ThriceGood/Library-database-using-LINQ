using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Linq_to_Library
{
    public partial class Insert_Book : Form
    {
        // declars a form1 variable
        //
        Form1 mainForm;

        // constructor takes in form1 one and sets to to the above
        //
        public Insert_Book(Form1 input)
        {
            mainForm = input;

            InitializeComponent();
        }



        // add book, if the textboxes are filled the bookinsert method is called from form1 and
        // the book details are sent to form1 to be added to the database
        //
        private void button1_Click(object sender, EventArgs e)
        {
            if (txb_author.TextLength != 0 || txb_title.TextLength != 0 || txb_year.TextLength != 0)
            {
                string available = "yes";

                mainForm.bookInsert(txb_title.Text, txb_author.Text, txb_year.Text, available);

                txb_title.Clear();
                txb_author.Clear();
                txb_year.Clear();
            }
            else
            {
                MessageBox.Show("Error: Missing field");
            }
        }
    }
}
