namespace PeltierController
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            label3 = new Label();
            label4 = new Label();
            comboBox1 = new ComboBox();
            button2 = new Button();
            button3 = new Button();
            groupBox1 = new GroupBox();
            label11 = new Label();
            label10 = new Label();
            label6 = new Label();
            label5 = new Label();
            groupBox2 = new GroupBox();
            button9 = new Button();
            button8 = new Button();
            groupBox5 = new GroupBox();
            label8 = new Label();
            textBox2 = new TextBox();
            button5 = new Button();
            groupBox4 = new GroupBox();
            button7 = new Button();
            label7 = new Label();
            textBox1 = new TextBox();
            button6 = new Button();
            button4 = new Button();
            groupBox3 = new GroupBox();
            label9 = new Label();
            label12 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(6, 27);
            label1.Name = "label1";
            label1.Size = new Size(210, 38);
            label1.TabIndex = 0;
            label1.Text = "Current status:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(236, 27);
            label2.Name = "label2";
            label2.Size = new Size(91, 38);
            label2.TabIndex = 1;
            label2.Text = "label2";
            label2.Click += label2_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(6, 30);
            button1.Name = "button1";
            button1.Size = new Size(187, 43);
            button1.TabIndex = 2;
            button1.Text = "Reset Peltier";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 65);
            label3.Name = "label3";
            label3.Size = new Size(235, 38);
            label3.TabIndex = 3;
            label3.Text = "Controller value:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(271, 65);
            label4.Name = "label4";
            label4.Size = new Size(91, 38);
            label4.TabIndex = 4;
            label4.Text = "label4";
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(310, 30);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(182, 33);
            comboBox1.TabIndex = 5;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(166, 30);
            button2.Name = "button2";
            button2.Size = new Size(81, 34);
            button2.TabIndex = 6;
            button2.Text = "Refresh";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.Location = new Point(6, 30);
            button3.Name = "button3";
            button3.Size = new Size(154, 34);
            button3.TabIndex = 7;
            button3.Text = "Connect";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label4);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(21, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(498, 197);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Module status";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(256, 141);
            label11.Name = "label11";
            label11.Size = new Size(106, 38);
            label11.TabIndex = 8;
            label11.Text = "label11";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(6, 141);
            label10.Name = "label10";
            label10.Size = new Size(236, 38);
            label10.TabIndex = 7;
            label10.Text = "Temperature [C]:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(166, 103);
            label6.Name = "label6";
            label6.Size = new Size(91, 38);
            label6.TabIndex = 6;
            label6.Text = "label6";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(6, 103);
            label5.Name = "label5";
            label5.Size = new Size(146, 38);
            label5.TabIndex = 5;
            label5.Text = "Direction:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button9);
            groupBox2.Controls.Add(button8);
            groupBox2.Controls.Add(groupBox5);
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(21, 215);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(498, 350);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Module controls";
            // 
            // button9
            // 
            button9.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button9.Location = new Point(305, 30);
            button9.Name = "button9";
            button9.Size = new Size(187, 43);
            button9.TabIndex = 10;
            button9.Text = "Automatic control";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button8
            // 
            button8.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button8.Location = new Point(17, 30);
            button8.Name = "button8";
            button8.Size = new Size(187, 43);
            button8.TabIndex = 9;
            button8.Text = "Manual control";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label8);
            groupBox5.Controls.Add(textBox2);
            groupBox5.Controls.Add(button5);
            groupBox5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox5.Location = new Point(11, 251);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(481, 93);
            groupBox5.TabIndex = 6;
            groupBox5.TabStop = false;
            groupBox5.Text = "Automatic temperature control";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(313, 42);
            label8.Name = "label8";
            label8.Size = new Size(23, 25);
            label8.TabIndex = 7;
            label8.Text = "C";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(213, 36);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(94, 31);
            textBox2.TabIndex = 5;
            // 
            // button5
            // 
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button5.Location = new Point(6, 30);
            button5.Name = "button5";
            button5.Size = new Size(201, 43);
            button5.TabIndex = 3;
            button5.Text = "Set temperature";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(button7);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(textBox1);
            groupBox4.Controls.Add(button6);
            groupBox4.Controls.Add(button4);
            groupBox4.Controls.Add(button1);
            groupBox4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox4.Location = new Point(6, 96);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(486, 149);
            groupBox4.TabIndex = 5;
            groupBox4.TabStop = false;
            groupBox4.Text = "Manual PWM Control";
            groupBox4.Enter += groupBox4_Enter;
            // 
            // button7
            // 
            button7.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button7.Location = new Point(306, 30);
            button7.Name = "button7";
            button7.Size = new Size(94, 43);
            button7.TabIndex = 8;
            button7.Text = "Heating";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(313, 88);
            label7.Name = "label7";
            label7.Size = new Size(27, 25);
            label7.TabIndex = 6;
            label7.Text = "%";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(206, 85);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(94, 31);
            textBox1.TabIndex = 5;
            // 
            // button6
            // 
            button6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button6.Location = new Point(206, 30);
            button6.Name = "button6";
            button6.Size = new Size(94, 43);
            button6.TabIndex = 7;
            button6.Text = "Cooling";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.Location = new Point(6, 79);
            button4.Name = "button4";
            button4.Size = new Size(187, 43);
            button4.TabIndex = 3;
            button4.Text = "Set PWM duty cycle";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(button3);
            groupBox3.Controls.Add(comboBox1);
            groupBox3.Controls.Add(button2);
            groupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(21, 571);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(498, 80);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "COM port";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(12, 654);
            label9.Name = "label9";
            label9.Size = new Size(295, 21);
            label9.TabIndex = 11;
            label9.Text = "Bram Laurens @ HU 2025 [Open Source]";
            label9.Click += label9_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.Location = new Point(468, 654);
            label12.Name = "label12";
            label12.Size = new Size(51, 21);
            label12.TabIndex = 12;
            label12.Text = "v1.0.3";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(531, 684);
            Controls.Add(label12);
            Controls.Add(label9);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "PeltierController";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button button1;
        private Label label3;
        private Label label4;
        private ComboBox comboBox1;
        private Button button2;
        private Button button3;
        private GroupBox groupBox1;
        private Label label6;
        private Label label5;
        private GroupBox groupBox2;
        private Button button4;
        private GroupBox groupBox3;
        private GroupBox groupBox5;
        private TextBox textBox2;
        private Button button5;
        private GroupBox groupBox4;
        private TextBox textBox1;
        private Label label8;
        private Label label7;
        private Label label9;
        private Button button7;
        private Button button6;
        private Label label10;
        private Label label11;
        private Button button9;
        private Button button8;
        private Label label12;
    }
}
