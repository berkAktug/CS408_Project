using System;

namespace cs408_client
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.box_nick = new System.Windows.Forms.TextBox();
            this.box_ip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.box_port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.box_question = new System.Windows.Forms.RichTextBox();
            this.box_answer = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_connect = new System.Windows.Forms.Button();
            this.btn_disconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(391, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(397, 258);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // box_nick
            // 
            this.box_nick.Location = new System.Drawing.Point(95, 55);
            this.box_nick.Name = "box_nick";
            this.box_nick.Size = new System.Drawing.Size(119, 20);
            this.box_nick.TabIndex = 1;
            this.box_nick.TextChanged += new System.EventHandler(this.Nickbox_TextChanged);
            // 
            // box_ip
            // 
            this.box_ip.Location = new System.Drawing.Point(95, 89);
            this.box_ip.Name = "box_ip";
            this.box_ip.Size = new System.Drawing.Size(119, 20);
            this.box_ip.TabIndex = 2;
            this.box_ip.TextChanged += new System.EventHandler(this.IPbox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nick";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Port";
            // 
            // box_port
            // 
            this.box_port.Location = new System.Drawing.Point(95, 130);
            this.box_port.Name = "box_port";
            this.box_port.Size = new System.Drawing.Size(119, 20);
            this.box_port.TabIndex = 6;
            this.box_port.TextChanged += new System.EventHandler(this.Portbox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Question";
            // 
            // box_question
            // 
            this.box_question.Location = new System.Drawing.Point(95, 177);
            this.box_question.Name = "box_question";
            this.box_question.Size = new System.Drawing.Size(258, 21);
            this.box_question.TabIndex = 10;
            this.box_question.Text = "";
            // 
            // box_answer
            // 
            this.box_answer.Location = new System.Drawing.Point(95, 249);
            this.box_answer.Name = "box_answer";
            this.box_answer.Size = new System.Drawing.Size(258, 21);
            this.box_answer.TabIndex = 12;
            this.box_answer.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Answer";
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(95, 319);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(198, 70);
            this.btn_connect.TabIndex = 13;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Location = new System.Drawing.Point(522, 319);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(198, 70);
            this.btn_disconnect.TabIndex = 14;
            this.btn_disconnect.Text = "Disconnect";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            this.btn_disconnect.Click += new System.EventHandler(this.btn_disconnect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_disconnect);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.box_answer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.box_question);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.box_port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.box_ip);
            this.Controls.Add(this.box_nick);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Portbox_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void IPbox_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Nickbox_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox box_nick;
        private System.Windows.Forms.TextBox box_ip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox box_port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox box_question;
        private System.Windows.Forms.RichTextBox box_answer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Button btn_disconnect;
    }
}

