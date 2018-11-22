namespace cs408_server
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
            this.box_ip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.box_port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.box_question = new System.Windows.Forms.RichTextBox();
            this.box_answer = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // box_ip
            // 
            this.box_ip.Location = new System.Drawing.Point(118, 15);
            this.box_ip.Name = "box_ip";
            this.box_ip.Size = new System.Drawing.Size(119, 20);
            this.box_ip.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Port";
            // 
            // box_port
            // 
            this.box_port.Location = new System.Drawing.Point(118, 56);
            this.box_port.Name = "box_port";
            this.box_port.Size = new System.Drawing.Size(119, 20);
            this.box_port.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Question";
            // 
            // box_question
            // 
            this.box_question.Location = new System.Drawing.Point(117, 106);
            this.box_question.Name = "box_question";
            this.box_question.Size = new System.Drawing.Size(258, 56);
            this.box_question.TabIndex = 10;
            this.box_question.Text = "";
            // 
            // box_answer
            // 
            this.box_answer.Location = new System.Drawing.Point(117, 210);
            this.box_answer.Name = "box_answer";
            this.box_answer.Size = new System.Drawing.Size(258, 60);
            this.box_answer.TabIndex = 12;
            this.box_answer.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Answer";
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(95, 319);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(176, 37);
            this.btn_start.TabIndex = 13;
            this.btn_start.Text = "Start Server";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(522, 319);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(178, 37);
            this.btn_close.TabIndex = 14;
            this.btn_close.Text = "Disconnect";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(474, 22);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(226, 248);
            this.richTextBox1.TabIndex = 15;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 374);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.box_answer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.box_question);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.box_port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.box_ip);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox box_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox box_port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox box_question;
        private System.Windows.Forms.RichTextBox box_answer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

