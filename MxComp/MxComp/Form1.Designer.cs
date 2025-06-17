namespace MxComp
{
    partial class Form1
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
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            button3 = new Button();
            textBox1 = new TextBox();
            button4 = new Button();
            textBox2 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            button5 = new Button();
            textBox3 = new TextBox();
            label4 = new Label();
            button6 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(30, 30);
            button1.Name = "button1";
            button1.Size = new Size(118, 73);
            button1.TabIndex = 0;
            button1.Text = "Open";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Open;
            // 
            // button2
            // 
            button2.Location = new Point(154, 30);
            button2.Name = "button2";
            button2.Size = new Size(117, 73);
            button2.TabIndex = 1;
            button2.Text = "Close";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Close;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 312);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // button3
            // 
            button3.Location = new Point(31, 221);
            button3.Name = "button3";
            button3.Size = new Size(117, 34);
            button3.TabIndex = 3;
            button3.Text = "GetDevice";
            button3.UseVisualStyleBackColor = true;
            button3.Click += GetDevice;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(154, 119);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(117, 23);
            textBox1.TabIndex = 4;
            // 
            // button4
            // 
            button4.Location = new Point(154, 222);
            button4.Name = "button4";
            button4.Size = new Size(118, 34);
            button4.TabIndex = 5;
            button4.Text = "SetDevice";
            button4.UseVisualStyleBackColor = true;
            button4.Click += SetDevice;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(154, 151);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(118, 23);
            textBox2.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 124);
            label2.Name = "label2";
            label2.Size = new Size(111, 15);
            label2.TabIndex = 7;
            label2.Text = "디바이스 주소 입력";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(37, 155);
            label3.Name = "label3";
            label3.Size = new Size(99, 15);
            label3.TabIndex = 8;
            label3.Text = "디바이스 값 입력";
            // 
            // button5
            // 
            button5.Location = new Point(31, 261);
            button5.Name = "button5";
            button5.Size = new Size(117, 34);
            button5.TabIndex = 9;
            button5.Text = "ReadDeviceBlock";
            button5.UseVisualStyleBackColor = true;
            button5.Click += ReadDeviceBlock;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(154, 185);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(117, 23);
            textBox3.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(49, 185);
            label4.Name = "label4";
            label4.Size = new Size(87, 15);
            label4.TabIndex = 11;
            label4.Text = "블록 개수 입력";
            // 
            // button6
            // 
            button6.Location = new Point(154, 262);
            button6.Name = "button6";
            button6.Size = new Size(117, 33);
            button6.TabIndex = 12;
            button6.Text = "WriteDeviceBlock";
            button6.UseVisualStyleBackColor = true;
            button6.Click += WriteDeviceBlock;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(308, 405);
            Controls.Add(button6);
            Controls.Add(label4);
            Controls.Add(textBox3);
            Controls.Add(button5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(button4);
            Controls.Add(textBox1);
            Controls.Add(button3);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Exit;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label1;
        private Button button3;
        private TextBox textBox1;
        private Button button4;
        private TextBox textBox2;
        private Label label2;
        private Label label3;
        private Button button5;
        private TextBox textBox3;
        private Label label4;
        private Button button6;
    }
}
