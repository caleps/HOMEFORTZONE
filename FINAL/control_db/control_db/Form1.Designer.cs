namespace control_db
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.msg_listbox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.time = new System.Windows.Forms.TextBox();
            this.stop_button = new System.Windows.Forms.Button();
            this.start_button = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.room = new System.Windows.Forms.ComboBox();
            this.dev = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pintxt = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.p3 = new System.Windows.Forms.Button();
            this.p2 = new System.Windows.Forms.Button();
            this.devtxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.add_room = new System.Windows.Forms.Button();
            this.add_dev = new System.Windows.Forms.Button();
            this.dev_page = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Gray;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.msg_listbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.time);
            this.groupBox1.Controls.Add(this.stop_button);
            this.groupBox1.Controls.Add(this.start_button);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(20, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 393);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial communication settings";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(12, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 38;
            this.label3.Text = "Port";
            // 
            // msg_listbox
            // 
            this.msg_listbox.BackColor = System.Drawing.Color.DimGray;
            this.msg_listbox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.msg_listbox.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msg_listbox.ForeColor = System.Drawing.Color.Snow;
            this.msg_listbox.FormattingEnabled = true;
            this.msg_listbox.ItemHeight = 17;
            this.msg_listbox.Location = new System.Drawing.Point(15, 94);
            this.msg_listbox.Name = "msg_listbox";
            this.msg_listbox.Size = new System.Drawing.Size(378, 293);
            this.msg_listbox.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(148, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 16);
            this.label1.TabIndex = 37;
            this.label1.Text = "Time";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(58, 23);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(110, 24);
            this.comboBox1.TabIndex = 5;
            // 
            // time
            // 
            this.time.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.time.Location = new System.Drawing.Point(191, 57);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(136, 23);
            this.time.TabIndex = 36;
            // 
            // stop_button
            // 
            this.stop_button.BackColor = System.Drawing.Color.Silver;
            this.stop_button.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stop_button.ForeColor = System.Drawing.Color.Snow;
            this.stop_button.Location = new System.Drawing.Point(263, 23);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(64, 25);
            this.stop_button.TabIndex = 4;
            this.stop_button.Text = "STOP";
            this.stop_button.UseVisualStyleBackColor = false;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // start_button
            // 
            this.start_button.BackColor = System.Drawing.Color.Silver;
            this.start_button.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start_button.ForeColor = System.Drawing.Color.Snow;
            this.start_button.Location = new System.Drawing.Point(191, 23);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(66, 25);
            this.start_button.TabIndex = 3;
            this.start_button.Text = "START";
            this.start_button.UseVisualStyleBackColor = false;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.Silver;
            this.button10.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.ForeColor = System.Drawing.Color.Snow;
            this.button10.Location = new System.Drawing.Point(299, 291);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(85, 52);
            this.button10.TabIndex = 26;
            this.button10.Text = "turn off";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.Silver;
            this.button13.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.ForeColor = System.Drawing.Color.Snow;
            this.button13.Location = new System.Drawing.Point(35, 291);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(84, 52);
            this.button13.TabIndex = 29;
            this.button13.Text = "turn on";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // room
            // 
            this.room.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.room.FormattingEnabled = true;
            this.room.Location = new System.Drawing.Point(35, 57);
            this.room.Name = "room";
            this.room.Size = new System.Drawing.Size(141, 24);
            this.room.TabIndex = 41;
            this.room.SelectedIndexChanged += new System.EventHandler(this.room_SelectedIndexChanged);
            // 
            // dev
            // 
            this.dev.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dev.FormattingEnabled = true;
            this.dev.Location = new System.Drawing.Point(287, 57);
            this.dev.Name = "dev";
            this.dev.Size = new System.Drawing.Size(123, 24);
            this.dev.TabIndex = 43;
            this.dev.SelectedIndexChanged += new System.EventHandler(this.dev_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Snow;
            this.label4.Location = new System.Drawing.Point(31, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 19);
            this.label4.TabIndex = 42;
            this.label4.Text = "Rooms";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Snow;
            this.label5.Location = new System.Drawing.Point(283, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 19);
            this.label5.TabIndex = 44;
            this.label5.Text = "Devices";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Snow;
            this.label7.Location = new System.Drawing.Point(263, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 19);
            this.label7.TabIndex = 47;
            this.label7.Text = "pin id";
            // 
            // pintxt
            // 
            this.pintxt.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pintxt.Location = new System.Drawing.Point(244, 209);
            this.pintxt.Name = "pintxt";
            this.pintxt.Size = new System.Drawing.Size(110, 23);
            this.pintxt.TabIndex = 48;
            this.pintxt.TextChanged += new System.EventHandler(this.pin_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Gray;
            this.groupBox2.Controls.Add(this.p3);
            this.groupBox2.Controls.Add(this.p2);
            this.groupBox2.Controls.Add(this.devtxt);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.pintxt);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dev);
            this.groupBox2.Controls.Add(this.room);
            this.groupBox2.Controls.Add(this.button13);
            this.groupBox2.Controls.Add(this.button10);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(450, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(424, 393);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "System Controls";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // p3
            // 
            this.p3.BackColor = System.Drawing.Color.Silver;
            this.p3.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p3.ForeColor = System.Drawing.Color.Snow;
            this.p3.Location = new System.Drawing.Point(212, 291);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(81, 52);
            this.p3.TabIndex = 54;
            this.p3.Text = "p3";
            this.p3.UseVisualStyleBackColor = false;
            this.p3.Click += new System.EventHandler(this.p3_Click);
            // 
            // p2
            // 
            this.p2.BackColor = System.Drawing.Color.Silver;
            this.p2.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p2.ForeColor = System.Drawing.Color.Snow;
            this.p2.Location = new System.Drawing.Point(125, 291);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(81, 52);
            this.p2.TabIndex = 53;
            this.p2.Text = "p2";
            this.p2.UseVisualStyleBackColor = false;
            this.p2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // devtxt
            // 
            this.devtxt.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.devtxt.Location = new System.Drawing.Point(64, 209);
            this.devtxt.Name = "devtxt";
            this.devtxt.Size = new System.Drawing.Size(125, 23);
            this.devtxt.TabIndex = 52;
            this.devtxt.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Snow;
            this.label2.Location = new System.Drawing.Point(78, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 19);
            this.label2.TabIndex = 51;
            this.label2.Text = "device name";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // add_room
            // 
            this.add_room.BackColor = System.Drawing.Color.Black;
            this.add_room.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.add_room.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add_room.ForeColor = System.Drawing.Color.Snow;
            this.add_room.Location = new System.Drawing.Point(900, 321);
            this.add_room.Name = "add_room";
            this.add_room.Size = new System.Drawing.Size(164, 102);
            this.add_room.TabIndex = 50;
            this.add_room.Text = "Add Room";
            this.add_room.UseVisualStyleBackColor = false;
            this.add_room.Click += new System.EventHandler(this.button2_Click);
            // 
            // add_dev
            // 
            this.add_dev.BackColor = System.Drawing.Color.Black;
            this.add_dev.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.add_dev.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add_dev.ForeColor = System.Drawing.Color.Snow;
            this.add_dev.Location = new System.Drawing.Point(900, 197);
            this.add_dev.Name = "add_dev";
            this.add_dev.Size = new System.Drawing.Size(164, 102);
            this.add_dev.TabIndex = 51;
            this.add_dev.Text = "Add Device";
            this.add_dev.UseVisualStyleBackColor = false;
            this.add_dev.Click += new System.EventHandler(this.button3_Click);
            // 
            // dev_page
            // 
            this.dev_page.BackColor = System.Drawing.Color.Black;
            this.dev_page.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dev_page.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dev_page.ForeColor = System.Drawing.Color.Snow;
            this.dev_page.Location = new System.Drawing.Point(900, 58);
            this.dev_page.Name = "dev_page";
            this.dev_page.Size = new System.Drawing.Size(164, 100);
            this.dev_page.TabIndex = 52;
            this.dev_page.Text = "MORE about device";
            this.dev_page.UseVisualStyleBackColor = false;
            this.dev_page.Click += new System.EventHandler(this.button5_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1.Font = new System.Drawing.Font("Cooper Black", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(1037, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 26);
            this.button1.TabIndex = 76;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1083, 446);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dev_page);
            this.Controls.Add(this.add_dev);
            this.Controls.Add(this.add_room);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.ListBox msg_listbox;
        private System.Windows.Forms.TextBox time;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.ComboBox room;
        private System.Windows.Forms.ComboBox dev;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox pintxt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox devtxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button add_room;
        private System.Windows.Forms.Button add_dev;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button dev_page;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button p3;
        private System.Windows.Forms.Button p2;
    }
}

