
namespace ClientManager
{
    partial class MainForm
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
            this.TB_seach = new System.Windows.Forms.TextBox();
            this.LBL_Search = new System.Windows.Forms.Label();
            this.DGV_1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BTN_remove = new System.Windows.Forms.Button();
            this.BTN_Update = new System.Windows.Forms.Button();
            this.BTN_ADD = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TB_seach
            // 
            this.TB_seach.Dock = System.Windows.Forms.DockStyle.Right;
            this.TB_seach.Location = new System.Drawing.Point(75, 0);
            this.TB_seach.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.TB_seach.Name = "TB_seach";
            this.TB_seach.Size = new System.Drawing.Size(576, 23);
            this.TB_seach.TabIndex = 0;
            this.TB_seach.TextChanged += new System.EventHandler(this.TB_seach_TextChanged);
            this.TB_seach.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_seach_KeyPress);
            // 
            // LBL_Search
            // 
            this.LBL_Search.AutoSize = true;
            this.LBL_Search.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LBL_Search.Location = new System.Drawing.Point(1, -2);
            this.LBL_Search.Name = "LBL_Search";
            this.LBL_Search.Size = new System.Drawing.Size(74, 28);
            this.LBL_Search.TabIndex = 1;
            this.LBL_Search.Text = "Search:";
            // 
            // DGV_1
            // 
            this.DGV_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_1.Location = new System.Drawing.Point(99, 30);
            this.DGV_1.Name = "DGV_1";
            this.DGV_1.RowTemplate.Height = 25;
            this.DGV_1.Size = new System.Drawing.Size(539, 214);
            this.DGV_1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BTN_remove);
            this.groupBox1.Controls.Add(this.BTN_Update);
            this.groupBox1.Controls.Add(this.BTN_ADD);
            this.groupBox1.Controls.Add(this.DGV_1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(1, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 255);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result";
            // 
            // BTN_remove
            // 
            this.BTN_remove.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BTN_remove.Location = new System.Drawing.Point(6, 96);
            this.BTN_remove.Name = "BTN_remove";
            this.BTN_remove.Size = new System.Drawing.Size(87, 27);
            this.BTN_remove.TabIndex = 5;
            this.BTN_remove.Text = "Remove";
            this.BTN_remove.UseVisualStyleBackColor = true;
            // 
            // BTN_Update
            // 
            this.BTN_Update.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BTN_Update.Location = new System.Drawing.Point(6, 63);
            this.BTN_Update.Name = "BTN_Update";
            this.BTN_Update.Size = new System.Drawing.Size(87, 27);
            this.BTN_Update.TabIndex = 4;
            this.BTN_Update.Text = "Edit";
            this.BTN_Update.UseVisualStyleBackColor = true;
            this.BTN_Update.Click += new System.EventHandler(this.BTN_Update_Click);
            // 
            // BTN_ADD
            // 
            this.BTN_ADD.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BTN_ADD.Location = new System.Drawing.Point(6, 30);
            this.BTN_ADD.Name = "BTN_ADD";
            this.BTN_ADD.Size = new System.Drawing.Size(87, 27);
            this.BTN_ADD.TabIndex = 3;
            this.BTN_ADD.Text = "AddNew";
            this.BTN_ADD.UseVisualStyleBackColor = true;
            this.BTN_ADD.Click += new System.EventHandler(this.BTN_ADD_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 285);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LBL_Search);
            this.Controls.Add(this.TB_seach);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "ContactManager";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_seach;
        private System.Windows.Forms.Label LBL_Search;
        private System.Windows.Forms.DataGridView DGV_1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BTN_ADD;
        private System.Windows.Forms.Button BTN_remove;
        private System.Windows.Forms.Button BTN_Update;
    }
}

