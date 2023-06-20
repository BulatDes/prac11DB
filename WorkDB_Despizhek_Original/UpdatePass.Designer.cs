namespace WorkDB_Despizhek
{
    partial class UpdatePass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if ( disposing && (components != null) )
            {
                components.Dispose( );
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.oldPassText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.newpassCheckText = new System.Windows.Forms.TextBox();
            this.newpassText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // oldPassText
            // 
            this.oldPassText.Location = new System.Drawing.Point(72, 48);
            this.oldPassText.Name = "oldPassText";
            this.oldPassText.Size = new System.Drawing.Size(218, 20);
            this.oldPassText.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Старый пароль";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(119, 242);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 30);
            this.button1.TabIndex = 13;
            this.button1.Text = "Изменить пароль";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Новый пароль";
            // 
            // newpassCheckText
            // 
            this.newpassCheckText.Location = new System.Drawing.Point(72, 194);
            this.newpassCheckText.Name = "newpassCheckText";
            this.newpassCheckText.Size = new System.Drawing.Size(218, 20);
            this.newpassCheckText.TabIndex = 12;
            // 
            // newpassText
            // 
            this.newpassText.Location = new System.Drawing.Point(72, 117);
            this.newpassText.Name = "newpassText";
            this.newpassText.Size = new System.Drawing.Size(218, 20);
            this.newpassText.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Подтверждение";
            // 
            // UpdatePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 450);
            this.Controls.Add(this.oldPassText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newpassCheckText);
            this.Controls.Add(this.newpassText);
            this.Controls.Add(this.label3);
            this.Name = "UpdatePass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Изменение пароля";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdatePass_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox oldPassText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newpassCheckText;
        private System.Windows.Forms.TextBox newpassText;
        private System.Windows.Forms.Label label3;
    }
}