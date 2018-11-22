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
            this.box_report = new System.Windows.Forms.RichTextBox();
            this.box_ip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.box_port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.box_send = new System.Windows.Forms.RichTextBox();
            this.btn_connect = new System.Windows.Forms.Button();
            this.btn_disconnect = new System.Windows.Forms.Button();
            this.btn_sendmessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // box_report
            // 
            this.box_report.Location = new System.Drawing.Point(17, 172);
            this.box_report.Name = "box_report";
            this.box_report.Size = new System.Drawing.Size(469, 258);
            this.box_report.TabIndex = 0;
            this.box_report.Text = "event log";
            // 
            // box_ip
            // 
            this.box_ip.Location = new System.Drawing.Point(40, 40);
            this.box_ip.Name = "box_ip";
            this.box_ip.Size = new System.Drawing.Size(119, 20);
            this.box_ip.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(181, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Port";
            // 
            // box_port
            // 
            this.box_port.Location = new System.Drawing.Point(213, 40);
            this.box_port.Name = "box_port";
            this.box_port.Size = new System.Drawing.Size(119, 20);
            this.box_port.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Send to server";
            // 
            // box_send
            // 
            this.box_send.Location = new System.Drawing.Point(109, 104);
            this.box_send.Name = "box_send";
            this.box_send.Size = new System.Drawing.Size(258, 21);
            this.box_send.TabIndex = 10;
            this.box_send.Text = "";
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(17, 436);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(120, 50);
            this.btn_connect.TabIndex = 13;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Location = new System.Drawing.Point(367, 436);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(119, 50);
            this.btn_disconnect.TabIndex = 14;
            this.btn_disconnect.Text = "Disconnect";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            this.btn_disconnect.Click += new System.EventHandler(this.btn_disconnect_Click);
            // 
            // btn_sendmessage
            // 
            this.btn_sendmessage.Location = new System.Drawing.Point(184, 436);
            this.btn_sendmessage.Name = "btn_sendmessage";
            this.btn_sendmessage.Size = new System.Drawing.Size(119, 50);
            this.btn_sendmessage.TabIndex = 15;
            this.btn_sendmessage.Text = "Send Message";
            this.btn_sendmessage.UseVisualStyleBackColor = true;
            this.btn_sendmessage.Click += new System.EventHandler(this.btn_sendmessage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 523);
            this.Controls.Add(this.btn_sendmessage);
            this.Controls.Add(this.btn_disconnect);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.box_send);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.box_port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.box_ip);
            this.Controls.Add(this.box_report);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox box_report;
        private System.Windows.Forms.TextBox box_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox box_port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox box_send;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Button btn_disconnect;
        private System.Windows.Forms.Button btn_sendmessage;
    }
}

