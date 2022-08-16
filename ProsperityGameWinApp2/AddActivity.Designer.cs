
namespace ProsperityGameWinApp2
{
    partial class AddActivity
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
            this.label9 = new System.Windows.Forms.Label();
            this.cbSelectTask = new System.Windows.Forms.ComboBox();
            this.txtActivityTitle = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAddActivity = new System.Windows.Forms.Button();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("IRANSans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(464, 30);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(69, 34);
            this.label9.TabIndex = 29;
            this.label9.Text = "تسک :";
            // 
            // cbSelectTask
            // 
            this.cbSelectTask.FormattingEnabled = true;
            this.cbSelectTask.Location = new System.Drawing.Point(89, 37);
            this.cbSelectTask.Name = "cbSelectTask";
            this.cbSelectTask.Size = new System.Drawing.Size(366, 23);
            this.cbSelectTask.TabIndex = 28;
            // 
            // txtActivityTitle
            // 
            this.txtActivityTitle.Location = new System.Drawing.Point(89, 83);
            this.txtActivityTitle.Name = "txtActivityTitle";
            this.txtActivityTitle.Size = new System.Drawing.Size(366, 23);
            this.txtActivityTitle.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("IRANSans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(464, 76);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label10.Size = new System.Drawing.Size(81, 34);
            this.label10.TabIndex = 25;
            this.label10.Text = "فعالیت :";
            // 
            // btnAddActivity
            // 
            this.btnAddActivity.Font = new System.Drawing.Font("IRANSans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddActivity.Location = new System.Drawing.Point(36, 176);
            this.btnAddActivity.Name = "btnAddActivity";
            this.btnAddActivity.Size = new System.Drawing.Size(94, 34);
            this.btnAddActivity.TabIndex = 26;
            this.btnAddActivity.Text = "ثبت";
            this.btnAddActivity.UseVisualStyleBackColor = true;
            this.btnAddActivity.Click += new System.EventHandler(this.btnAddActivity_Click);
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(260, 134);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(134, 23);
            this.txtTime.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("IRANSans", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(400, 127);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(145, 34);
            this.label1.TabIndex = 30;
            this.label1.Text = "زمان ( ساعت ) :";
            // 
            // AddActivity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 263);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbSelectTask);
            this.Controls.Add(this.txtActivityTitle);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnAddActivity);
            this.Name = "AddActivity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddActivity";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbSelectTask;
        private System.Windows.Forms.TextBox txtActivityTitle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnAddActivity;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label label1;
    }
}