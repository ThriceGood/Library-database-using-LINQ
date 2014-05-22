namespace Linq_to_Library
{
    partial class Insert_Book
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txb_title = new System.Windows.Forms.TextBox();
            this.txb_author = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_year = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // txb_title
            // 
            this.txb_title.Location = new System.Drawing.Point(74, 43);
            this.txb_title.Name = "txb_title";
            this.txb_title.Size = new System.Drawing.Size(268, 20);
            this.txb_title.TabIndex = 1;
            // 
            // txb_author
            // 
            this.txb_author.Location = new System.Drawing.Point(74, 69);
            this.txb_author.Name = "txb_author";
            this.txb_author.Size = new System.Drawing.Size(268, 20);
            this.txb_author.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Author";
            // 
            // txb_year
            // 
            this.txb_year.Location = new System.Drawing.Point(74, 95);
            this.txb_year.Name = "txb_year";
            this.txb_year.Size = new System.Drawing.Size(268, 20);
            this.txb_year.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Year";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(136, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 33);
            this.button1.TabIndex = 6;
            this.button1.Text = "Add to Database";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Insert_Book
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 201);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txb_year);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txb_author);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txb_title);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Insert_Book";
            this.Text = "Insert Book";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_title;
        private System.Windows.Forms.TextBox txb_author;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_year;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}