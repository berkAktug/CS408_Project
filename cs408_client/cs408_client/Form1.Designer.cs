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
            this.btn_connect = new System.Windows.Forms.Button();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_ready = new System.Windows.Forms.Button();
            this.btn_disconnect = new System.Windows.Forms.Button();
            this.box_report = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.box_ip = new System.Windows.Forms.TextBox();
            this.box_port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.box_nick = new System.Windows.Forms.TextBox();
            this.box_question = new System.Windows.Forms.TextBox();
            this.box_answer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(25, 370);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(180, 57);
            this.btn_connect.TabIndex = 0;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(220, 370);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(180, 57);
            this.btn_send.TabIndex = 1;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // btn_ready
            // 
            this.btn_ready.Location = new System.Drawing.Point(417, 370);
            this.btn_ready.Name = "btn_ready";
            this.btn_ready.Size = new System.Drawing.Size(180, 57);
            this.btn_ready.TabIndex = 2;
            this.btn_ready.Text = "Start The Game";
            this.btn_ready.UseVisualStyleBackColor = true;
            this.btn_ready.Click += new System.EventHandler(this.btn_ready_Click);
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Location = new System.Drawing.Point(608, 370);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(180, 57);
            this.btn_disconnect.TabIndex = 3;
            this.btn_disconnect.Text = "Disconnect";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            this.btn_disconnect.Click += new System.EventHandler(this.btn_disconnect_Click);
            // 
            // box_report
            // 
            this.box_report.Enabled = false;
            this.box_report.Location = new System.Drawing.Point(409, 12);
            this.box_report.Name = "box_report";
            this.box_report.Size = new System.Drawing.Size(379, 324);
            this.box_report.TabIndex = 4;
            this.box_report.Text = "Event log\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port";
            // 
            // box_ip
            // 
            this.box_ip.Location = new System.Drawing.Point(99, 32);
            this.box_ip.Name = "box_ip";
            this.box_ip.Size = new System.Drawing.Size(160, 20);
            this.box_ip.TabIndex = 7;
            // 
            // box_port
            // 
            this.box_port.Location = new System.Drawing.Point(99, 78);
            this.box_port.Name = "box_port";
            this.box_port.Size = new System.Drawing.Size(160, 20);
            this.box_port.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Nick";
            // 
            // box_nick
            // 
            this.box_nick.Location = new System.Drawing.Point(99, 133);
            this.box_nick.Name = "box_nick";
            this.box_nick.Size = new System.Drawing.Size(160, 20);
            this.box_nick.TabIndex = 10;
            // 
            // box_question
            // 
            this.box_question.Location = new System.Drawing.Point(99, 206);
            this.box_question.Name = "box_question";
            this.box_question.Size = new System.Drawing.Size(273, 20);
            this.box_question.TabIndex = 11;
            // 
            // box_answer
            // 
            this.box_answer.Location = new System.Drawing.Point(99, 271);
            this.box_answer.Name = "box_answer";
            this.box_answer.Size = new System.Drawing.Size(273, 20);
            this.box_answer.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Question";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Answer";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(96, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Please add \"?\" to end of your question.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.box_answer);
            this.Controls.Add(this.box_question);
            this.Controls.Add(this.box_nick);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.box_port);
            this.Controls.Add(this.box_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.box_report);
            this.Controls.Add(this.btn_disconnect);
            this.Controls.Add(this.btn_ready);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.btn_connect);
            this.Name = "Form1";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_ready;
        private System.Windows.Forms.Button btn_disconnect;
        private System.Windows.Forms.RichTextBox box_report;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox box_ip;
        private System.Windows.Forms.TextBox box_port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox box_nick;
        private System.Windows.Forms.TextBox box_question;
        private System.Windows.Forms.TextBox box_answer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

