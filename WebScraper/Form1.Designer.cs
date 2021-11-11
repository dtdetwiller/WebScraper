
namespace WebScraper
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.courseTextBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numEnrollments = new System.Windows.Forms.NumericUpDown();
            this.yearTextBox2 = new System.Windows.Forms.TextBox();
            this.semesterTextBox2 = new System.Windows.Forms.ComboBox();
            this.findEnrollments = new System.Windows.Forms.Button();
            this.findCourseInfo = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.yearTextBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.semesterComboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numEnrollments)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Course";
            // 
            // courseTextBox1
            // 
            this.courseTextBox1.Location = new System.Drawing.Point(11, 32);
            this.courseTextBox1.Name = "courseTextBox1";
            this.courseTextBox1.PlaceholderText = "Ex: CS 2420";
            this.courseTextBox1.Size = new System.Drawing.Size(163, 27);
            this.courseTextBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 349);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Semester";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Number of Enrollments";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 296);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Year";
            // 
            // numEnrollments
            // 
            this.numEnrollments.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numEnrollments.Location = new System.Drawing.Point(11, 266);
            this.numEnrollments.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numEnrollments.Name = "numEnrollments";
            this.numEnrollments.Size = new System.Drawing.Size(163, 27);
            this.numEnrollments.TabIndex = 5;
            this.numEnrollments.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // yearTextBox2
            // 
            this.yearTextBox2.Location = new System.Drawing.Point(11, 319);
            this.yearTextBox2.Name = "yearTextBox2";
            this.yearTextBox2.PlaceholderText = "Ex: 2019";
            this.yearTextBox2.Size = new System.Drawing.Size(163, 27);
            this.yearTextBox2.TabIndex = 6;
            // 
            // semesterTextBox2
            // 
            this.semesterTextBox2.FormattingEnabled = true;
            this.semesterTextBox2.Items.AddRange(new object[] {
            "Fall",
            "Spring",
            "Summer"});
            this.semesterTextBox2.Location = new System.Drawing.Point(11, 372);
            this.semesterTextBox2.Name = "semesterTextBox2";
            this.semesterTextBox2.Size = new System.Drawing.Size(163, 28);
            this.semesterTextBox2.TabIndex = 7;
            // 
            // findEnrollments
            // 
            this.findEnrollments.Location = new System.Drawing.Point(10, 406);
            this.findEnrollments.Name = "findEnrollments";
            this.findEnrollments.Size = new System.Drawing.Size(164, 29);
            this.findEnrollments.TabIndex = 8;
            this.findEnrollments.Text = "Find Enrollments";
            this.findEnrollments.UseVisualStyleBackColor = true;
            this.findEnrollments.Click += new System.EventHandler(this.findEnrollments_Click);
            // 
            // findCourseInfo
            // 
            this.findCourseInfo.Location = new System.Drawing.Point(11, 172);
            this.findCourseInfo.Name = "findCourseInfo";
            this.findCourseInfo.Size = new System.Drawing.Size(163, 29);
            this.findCourseInfo.TabIndex = 4;
            this.findCourseInfo.Text = "Find Course Info";
            this.findCourseInfo.UseVisualStyleBackColor = true;
            this.findCourseInfo.Click += new System.EventHandler(this.findCourseInfo_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Year";
            // 
            // yearTextBox1
            // 
            this.yearTextBox1.Location = new System.Drawing.Point(12, 85);
            this.yearTextBox1.Name = "yearTextBox1";
            this.yearTextBox1.PlaceholderText = "Ex: 2019";
            this.yearTextBox1.Size = new System.Drawing.Size(163, 27);
            this.yearTextBox1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Semester";
            // 
            // semesterComboBox1
            // 
            this.semesterComboBox1.FormattingEnabled = true;
            this.semesterComboBox1.Items.AddRange(new object[] {
            "Fall",
            "Spring",
            "Summer"});
            this.semesterComboBox1.Location = new System.Drawing.Point(12, 138);
            this.semesterComboBox1.Name = "semesterComboBox1";
            this.semesterComboBox1.Size = new System.Drawing.Size(163, 28);
            this.semesterComboBox1.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "________________________";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(533, 1);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(94, 28);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // outputBox
            // 
            this.outputBox.FormattingEnabled = true;
            this.outputBox.ItemHeight = 20;
            this.outputBox.Location = new System.Drawing.Point(181, 32);
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(446, 404);
            this.outputBox.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 443);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.semesterComboBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.yearTextBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.findCourseInfo);
            this.Controls.Add(this.findEnrollments);
            this.Controls.Add(this.semesterTextBox2);
            this.Controls.Add(this.yearTextBox2);
            this.Controls.Add(this.numEnrollments);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.courseTextBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numEnrollments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox courseTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numEnrollments;
        private System.Windows.Forms.TextBox yearTextBox2;
        private System.Windows.Forms.ComboBox semesterTextBox2;
        private System.Windows.Forms.Button findEnrollments;
        private System.Windows.Forms.Button findCourseInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox yearTextBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox semesterComboBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ListBox outputBox;
    }
}

