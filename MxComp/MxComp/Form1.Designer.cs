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
            label1.Location = new Point(30, 272);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // button3
            // 
            button3.Location = new Point(30, 184);
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
            button4.Location = new Point(155, 184);
            button4.Name = "button4";
            button4.Size = new Size(117, 34);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(308, 362);
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
    }
}
