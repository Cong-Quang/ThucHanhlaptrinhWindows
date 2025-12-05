namespace Buoi2
{
    partial class Form1
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
            this.Number1 = new System.Windows.Forms.Label();
            this.txt_Number1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Number2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Answer = new System.Windows.Forms.TextBox();
            this.btn_Cong = new System.Windows.Forms.Button();
            this.bnt_Tru = new System.Windows.Forms.Button();
            this.btn_Nhan = new System.Windows.Forms.Button();
            this.btn_Chia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Number1
            // 
            this.Number1.AutoSize = true;
            this.Number1.Location = new System.Drawing.Point(12, 21);
            this.Number1.Name = "Number1";
            this.Number1.Size = new System.Drawing.Size(53, 13);
            this.Number1.TabIndex = 0;
            this.Number1.Text = "Number 1";
            // 
            // txt_Number1
            // 
            this.txt_Number1.Location = new System.Drawing.Point(82, 18);
            this.txt_Number1.Name = "txt_Number1";
            this.txt_Number1.Size = new System.Drawing.Size(277, 20);
            this.txt_Number1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Number 2";
            // 
            // txt_Number2
            // 
            this.txt_Number2.Location = new System.Drawing.Point(82, 44);
            this.txt_Number2.Name = "txt_Number2";
            this.txt_Number2.Size = new System.Drawing.Size(277, 20);
            this.txt_Number2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Answer";
            // 
            // txt_Answer
            // 
            this.txt_Answer.Location = new System.Drawing.Point(82, 121);
            this.txt_Answer.Name = "txt_Answer";
            this.txt_Answer.Size = new System.Drawing.Size(277, 20);
            this.txt_Answer.TabIndex = 1;
            // 
            // btn_Cong
            // 
            this.btn_Cong.Location = new System.Drawing.Point(82, 82);
            this.btn_Cong.Name = "btn_Cong";
            this.btn_Cong.Size = new System.Drawing.Size(51, 23);
            this.btn_Cong.TabIndex = 2;
            this.btn_Cong.Text = "+";
            this.btn_Cong.UseVisualStyleBackColor = true;
            this.btn_Cong.Click += new System.EventHandler(this.btn_Cong_Click);
            // 
            // bnt_Tru
            // 
            this.bnt_Tru.Location = new System.Drawing.Point(139, 82);
            this.bnt_Tru.Name = "bnt_Tru";
            this.bnt_Tru.Size = new System.Drawing.Size(51, 23);
            this.bnt_Tru.TabIndex = 2;
            this.bnt_Tru.Text = "-";
            this.bnt_Tru.UseVisualStyleBackColor = true;
            this.bnt_Tru.Click += new System.EventHandler(this.bnt_Tru_Click);
            // 
            // btn_Nhan
            // 
            this.btn_Nhan.Location = new System.Drawing.Point(251, 82);
            this.btn_Nhan.Name = "btn_Nhan";
            this.btn_Nhan.Size = new System.Drawing.Size(51, 23);
            this.btn_Nhan.TabIndex = 2;
            this.btn_Nhan.Text = "x";
            this.btn_Nhan.UseVisualStyleBackColor = true;
            this.btn_Nhan.Click += new System.EventHandler(this.btn_Nhan_Click);
            // 
            // btn_Chia
            // 
            this.btn_Chia.Location = new System.Drawing.Point(308, 82);
            this.btn_Chia.Name = "btn_Chia";
            this.btn_Chia.Size = new System.Drawing.Size(51, 23);
            this.btn_Chia.TabIndex = 2;
            this.btn_Chia.Text = "/";
            this.btn_Chia.UseVisualStyleBackColor = true;
            this.btn_Chia.Click += new System.EventHandler(this.btn_Chia_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 168);
            this.Controls.Add(this.btn_Chia);
            this.Controls.Add(this.btn_Nhan);
            this.Controls.Add(this.bnt_Tru);
            this.Controls.Add(this.btn_Cong);
            this.Controls.Add(this.txt_Answer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Number2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Number1);
            this.Controls.Add(this.Number1);
            this.Name = "Form1";
            this.Text = "Caculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Number1;
        private System.Windows.Forms.TextBox txt_Number1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Number2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Answer;
        private System.Windows.Forms.Button btn_Cong;
        private System.Windows.Forms.Button bnt_Tru;
        private System.Windows.Forms.Button btn_Nhan;
        private System.Windows.Forms.Button btn_Chia;
    }
}

