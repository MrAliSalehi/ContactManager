
namespace ClientManager
{
    partial class AddUserForm
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
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.BTN_Submit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TB_Email = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_Note = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_Num = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_LastN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_FirstN = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Location = new System.Drawing.Point(299, 210);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(280, 23);
            this.BTN_Cancel.TabIndex = 0;
            this.BTN_Cancel.Text = "Cancel";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new System.EventHandler(this.BTN_Cancel_Click);
            // 
            // BTN_Submit
            // 
            this.BTN_Submit.Location = new System.Drawing.Point(12, 210);
            this.BTN_Submit.Name = "BTN_Submit";
            this.BTN_Submit.Size = new System.Drawing.Size(280, 23);
            this.BTN_Submit.TabIndex = 1;
            this.BTN_Submit.Text = "Submit";
            this.BTN_Submit.UseVisualStyleBackColor = true;
            this.BTN_Submit.Click += new System.EventHandler(this.BTN_Submit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TB_Email);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.TB_Note);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TB_Num);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TB_LastN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TB_FirstN);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(567, 192);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Insert Info";
            // 
            // TB_Email
            // 
            this.TB_Email.Location = new System.Drawing.Point(102, 133);
            this.TB_Email.Name = "TB_Email";
            this.TB_Email.Size = new System.Drawing.Size(160, 29);
            this.TB_Email.TabIndex = 9;
            this.TB_Email.Text = "iewf@yahoo.com";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "Email:";
            // 
            // TB_Note
            // 
            this.TB_Note.Location = new System.Drawing.Point(287, 39);
            this.TB_Note.Multiline = true;
            this.TB_Note.Name = "TB_Note";
            this.TB_Note.Size = new System.Drawing.Size(280, 53);
            this.TB_Note.TabIndex = 7;
            this.TB_Note.Text = "efwefwefwef";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Note:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Phone:";
            // 
            // TB_Num
            // 
            this.TB_Num.Location = new System.Drawing.Point(102, 98);
            this.TB_Num.Mask = "0-(000)0000000";
            this.TB_Num.Name = "TB_Num";
            this.TB_Num.Size = new System.Drawing.Size(160, 29);
            this.TB_Num.TabIndex = 4;
            this.TB_Num.Text = "33333333331";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "LastName:";
            // 
            // TB_LastN
            // 
            this.TB_LastN.Location = new System.Drawing.Point(102, 63);
            this.TB_LastN.Name = "TB_LastN";
            this.TB_LastN.Size = new System.Drawing.Size(160, 29);
            this.TB_LastN.TabIndex = 2;
            this.TB_LastN.Text = "hasanzade";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "FirstName:";
            // 
            // TB_FirstN
            // 
            this.TB_FirstN.Location = new System.Drawing.Point(102, 28);
            this.TB_FirstN.Name = "TB_FirstN";
            this.TB_FirstN.Size = new System.Drawing.Size(160, 29);
            this.TB_FirstN.TabIndex = 0;
            this.TB_FirstN.Text = "mmd";
            // 
            // AddUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 245);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BTN_Submit);
            this.Controls.Add(this.BTN_Cancel);
            this.Name = "AddUserForm";
            this.ShowIcon = false;
            this.Text = "Add New User";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BTN_Cancel;
        private System.Windows.Forms.Button BTN_Submit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox TB_Num;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_LastN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_FirstN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_Note;
        private System.Windows.Forms.TextBox TB_Email;
    }
}