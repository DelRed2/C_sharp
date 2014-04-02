namespace FunctionPlotter {
    partial class MainForm {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.PlotBox = new System.Windows.Forms.PictureBox();
            this.LabelPosition = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CheckBoxIntersect = new System.Windows.Forms.CheckBox();
            this.ComboBoxPlot = new System.Windows.Forms.ComboBox();
            this.LabelPlotWidth = new System.Windows.Forms.Label();
            this.NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ButtonColorPlot = new System.Windows.Forms.Button();
            this.ButtonDrawPlot = new System.Windows.Forms.Button();
            this.EditFunction2 = new System.Windows.Forms.TextBox();
            this.LabelFunction2 = new System.Windows.Forms.Label();
            this.BoxFromToY = new System.Windows.Forms.GroupBox();
            this.CheckAutoY = new System.Windows.Forms.CheckBox();
            this.EditToY = new System.Windows.Forms.TextBox();
            this.EditFromY = new System.Windows.Forms.TextBox();
            this.LabelToY = new System.Windows.Forms.Label();
            this.LablelFromY = new System.Windows.Forms.Label();
            this.BoxFromToX = new System.Windows.Forms.GroupBox();
            this.EditToX = new System.Windows.Forms.TextBox();
            this.EditFromX = new System.Windows.Forms.TextBox();
            this.ToLabel = new System.Windows.Forms.Label();
            this.FromLabel = new System.Windows.Forms.Label();
            this.LabelFunction1 = new System.Windows.Forms.Label();
            this.EditFunction1 = new System.Windows.Forms.TextBox();
            this.AxisBox = new System.Windows.Forms.PictureBox();
            this.ColorDialogPlot = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.PlotBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown)).BeginInit();
            this.BoxFromToY.SuspendLayout();
            this.BoxFromToX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AxisBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PlotBox
            // 
            this.PlotBox.BackColor = System.Drawing.Color.White;
            this.PlotBox.Enabled = false;
            this.PlotBox.Location = new System.Drawing.Point(119, 28);
            this.PlotBox.Name = "PlotBox";
            this.PlotBox.Size = new System.Drawing.Size(1000, 400);
            this.PlotBox.TabIndex = 0;
            this.PlotBox.TabStop = false;
            this.PlotBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlotBox_MouseDown);
            this.PlotBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PlotBox_MouseMove);
            this.PlotBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PlotBox_MouseUp);
            // 
            // LabelPosition
            // 
            this.LabelPosition.AutoSize = true;
            this.LabelPosition.BackColor = System.Drawing.Color.White;
            this.LabelPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelPosition.Location = new System.Drawing.Point(15, 457);
            this.LabelPosition.Name = "LabelPosition";
            this.LabelPosition.Size = new System.Drawing.Size(56, 15);
            this.LabelPosition.TabIndex = 10;
            this.LabelPosition.Text = "000 x 000";
            this.LabelPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LabelPosition.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CheckBoxIntersect);
            this.panel1.Controls.Add(this.ComboBoxPlot);
            this.panel1.Controls.Add(this.LabelPlotWidth);
            this.panel1.Controls.Add(this.NumericUpDown);
            this.panel1.Controls.Add(this.ButtonColorPlot);
            this.panel1.Controls.Add(this.ButtonDrawPlot);
            this.panel1.Controls.Add(this.EditFunction2);
            this.panel1.Controls.Add(this.LabelFunction2);
            this.panel1.Controls.Add(this.BoxFromToY);
            this.panel1.Controls.Add(this.BoxFromToX);
            this.panel1.Controls.Add(this.LabelFunction1);
            this.panel1.Controls.Add(this.EditFunction1);
            this.panel1.Location = new System.Drawing.Point(15, 478);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1154, 158);
            this.panel1.TabIndex = 14;
            // 
            // CheckBoxIntersect
            // 
            this.CheckBoxIntersect.AutoSize = true;
            this.CheckBoxIntersect.Enabled = false;
            this.CheckBoxIntersect.Location = new System.Drawing.Point(510, 50);
            this.CheckBoxIntersect.Name = "CheckBoxIntersect";
            this.CheckBoxIntersect.Size = new System.Drawing.Size(122, 17);
            this.CheckBoxIntersect.TabIndex = 24;
            this.CheckBoxIntersect.Text = "Search intersections";
            this.CheckBoxIntersect.UseVisualStyleBackColor = true;
            this.CheckBoxIntersect.CheckedChanged += new System.EventHandler(this.CheckBoxIntersect_CheckedChanged);
            // 
            // ComboBoxPlot
            // 
            this.ComboBoxPlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxPlot.FormattingEnabled = true;
            this.ComboBoxPlot.Items.AddRange(new object[] {
            "Solid",
            "Dot",
            "Dash",
            "Dash Dot",
            "Dash Dot Dot"});
            this.ComboBoxPlot.Location = new System.Drawing.Point(827, 13);
            this.ComboBoxPlot.Name = "ComboBoxPlot";
            this.ComboBoxPlot.Size = new System.Drawing.Size(121, 21);
            this.ComboBoxPlot.TabIndex = 23;
            // 
            // LabelPlotWidth
            // 
            this.LabelPlotWidth.AutoSize = true;
            this.LabelPlotWidth.Location = new System.Drawing.Point(715, 16);
            this.LabelPlotWidth.Name = "LabelPlotWidth";
            this.LabelPlotWidth.Size = new System.Drawing.Size(38, 13);
            this.LabelPlotWidth.TabIndex = 22;
            this.LabelPlotWidth.Text = "Width:";
            // 
            // NumericUpDown
            // 
            this.NumericUpDown.Location = new System.Drawing.Point(759, 14);
            this.NumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDown.Name = "NumericUpDown";
            this.NumericUpDown.Size = new System.Drawing.Size(37, 20);
            this.NumericUpDown.TabIndex = 21;
            this.NumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ButtonColorPlot
            // 
            this.ButtonColorPlot.BackColor = System.Drawing.Color.Red;
            this.ButtonColorPlot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonColorPlot.Location = new System.Drawing.Point(649, 0);
            this.ButtonColorPlot.Name = "ButtonColorPlot";
            this.ButtonColorPlot.Size = new System.Drawing.Size(47, 44);
            this.ButtonColorPlot.TabIndex = 20;
            this.ButtonColorPlot.Text = "Color";
            this.ButtonColorPlot.UseVisualStyleBackColor = false;
            this.ButtonColorPlot.Click += new System.EventHandler(this.ButtonColorPlot_Click);
            // 
            // ButtonDrawPlot
            // 
            this.ButtonDrawPlot.Location = new System.Drawing.Point(1076, 3);
            this.ButtonDrawPlot.Name = "ButtonDrawPlot";
            this.ButtonDrawPlot.Size = new System.Drawing.Size(75, 23);
            this.ButtonDrawPlot.TabIndex = 19;
            this.ButtonDrawPlot.Text = "Draw plot";
            this.ButtonDrawPlot.UseVisualStyleBackColor = true;
            this.ButtonDrawPlot.Click += new System.EventHandler(this.ButtonDrawPlot_Click);
            // 
            // EditFunction2
            // 
            this.EditFunction2.Location = new System.Drawing.Point(60, 24);
            this.EditFunction2.Name = "EditFunction2";
            this.EditFunction2.Size = new System.Drawing.Size(572, 20);
            this.EditFunction2.TabIndex = 18;
            this.EditFunction2.TextChanged += new System.EventHandler(this.EditFunction2_TextChanged);
            // 
            // LabelFunction2
            // 
            this.LabelFunction2.AutoSize = true;
            this.LabelFunction2.Location = new System.Drawing.Point(0, 31);
            this.LabelFunction2.Name = "LabelFunction2";
            this.LabelFunction2.Size = new System.Drawing.Size(60, 13);
            this.LabelFunction2.TabIndex = 17;
            this.LabelFunction2.Text = "Function 2:";
            // 
            // BoxFromToY
            // 
            this.BoxFromToY.Controls.Add(this.CheckAutoY);
            this.BoxFromToY.Controls.Add(this.EditToY);
            this.BoxFromToY.Controls.Add(this.EditFromY);
            this.BoxFromToY.Controls.Add(this.LabelToY);
            this.BoxFromToY.Controls.Add(this.LablelFromY);
            this.BoxFromToY.Location = new System.Drawing.Point(189, 59);
            this.BoxFromToY.Name = "BoxFromToY";
            this.BoxFromToY.Size = new System.Drawing.Size(180, 86);
            this.BoxFromToY.TabIndex = 16;
            this.BoxFromToY.TabStop = false;
            this.BoxFromToY.Text = "Y";
            // 
            // CheckAutoY
            // 
            this.CheckAutoY.AutoSize = true;
            this.CheckAutoY.Location = new System.Drawing.Point(9, 65);
            this.CheckAutoY.Name = "CheckAutoY";
            this.CheckAutoY.Size = new System.Drawing.Size(48, 17);
            this.CheckAutoY.TabIndex = 12;
            this.CheckAutoY.Text = "Auto";
            this.CheckAutoY.UseVisualStyleBackColor = true;
            this.CheckAutoY.CheckStateChanged += new System.EventHandler(this.CheckAutoY_CheckStateChanged);
            // 
            // EditToY
            // 
            this.EditToY.Location = new System.Drawing.Point(57, 39);
            this.EditToY.Name = "EditToY";
            this.EditToY.Size = new System.Drawing.Size(117, 20);
            this.EditToY.TabIndex = 11;
            this.EditToY.Text = "20";
            // 
            // EditFromY
            // 
            this.EditFromY.Location = new System.Drawing.Point(57, 13);
            this.EditFromY.Name = "EditFromY";
            this.EditFromY.Size = new System.Drawing.Size(117, 20);
            this.EditFromY.TabIndex = 10;
            this.EditFromY.Text = "-20";
            // 
            // LabelToY
            // 
            this.LabelToY.AutoSize = true;
            this.LabelToY.Location = new System.Drawing.Point(6, 46);
            this.LabelToY.Name = "LabelToY";
            this.LabelToY.Size = new System.Drawing.Size(23, 13);
            this.LabelToY.TabIndex = 9;
            this.LabelToY.Text = "To:";
            // 
            // LablelFromY
            // 
            this.LablelFromY.AutoSize = true;
            this.LablelFromY.Location = new System.Drawing.Point(6, 16);
            this.LablelFromY.Name = "LablelFromY";
            this.LablelFromY.Size = new System.Drawing.Size(33, 13);
            this.LablelFromY.TabIndex = 8;
            this.LablelFromY.Text = "From:";
            // 
            // BoxFromToX
            // 
            this.BoxFromToX.Controls.Add(this.EditToX);
            this.BoxFromToX.Controls.Add(this.EditFromX);
            this.BoxFromToX.Controls.Add(this.ToLabel);
            this.BoxFromToX.Controls.Add(this.FromLabel);
            this.BoxFromToX.Location = new System.Drawing.Point(3, 59);
            this.BoxFromToX.Name = "BoxFromToX";
            this.BoxFromToX.Size = new System.Drawing.Size(180, 65);
            this.BoxFromToX.TabIndex = 15;
            this.BoxFromToX.TabStop = false;
            this.BoxFromToX.Text = "X";
            // 
            // EditToX
            // 
            this.EditToX.Location = new System.Drawing.Point(57, 39);
            this.EditToX.Name = "EditToX";
            this.EditToX.Size = new System.Drawing.Size(117, 20);
            this.EditToX.TabIndex = 11;
            this.EditToX.Text = "50";
            // 
            // EditFromX
            // 
            this.EditFromX.Location = new System.Drawing.Point(57, 13);
            this.EditFromX.Name = "EditFromX";
            this.EditFromX.Size = new System.Drawing.Size(117, 20);
            this.EditFromX.TabIndex = 10;
            this.EditFromX.Text = "-50";
            // 
            // ToLabel
            // 
            this.ToLabel.AutoSize = true;
            this.ToLabel.Location = new System.Drawing.Point(6, 46);
            this.ToLabel.Name = "ToLabel";
            this.ToLabel.Size = new System.Drawing.Size(23, 13);
            this.ToLabel.TabIndex = 9;
            this.ToLabel.Text = "To:";
            // 
            // FromLabel
            // 
            this.FromLabel.AutoSize = true;
            this.FromLabel.Location = new System.Drawing.Point(6, 16);
            this.FromLabel.Name = "FromLabel";
            this.FromLabel.Size = new System.Drawing.Size(33, 13);
            this.FromLabel.TabIndex = 8;
            this.FromLabel.Text = "From:";
            // 
            // LabelFunction1
            // 
            this.LabelFunction1.AutoSize = true;
            this.LabelFunction1.Location = new System.Drawing.Point(0, 7);
            this.LabelFunction1.Name = "LabelFunction1";
            this.LabelFunction1.Size = new System.Drawing.Size(60, 13);
            this.LabelFunction1.TabIndex = 14;
            this.LabelFunction1.Text = "Function 1:";
            // 
            // EditFunction1
            // 
            this.EditFunction1.Location = new System.Drawing.Point(60, 0);
            this.EditFunction1.Name = "EditFunction1";
            this.EditFunction1.Size = new System.Drawing.Size(572, 20);
            this.EditFunction1.TabIndex = 13;
            this.EditFunction1.Text = "sin(x)*x";
            // 
            // AxisBox
            // 
            this.AxisBox.BackColor = System.Drawing.Color.White;
            this.AxisBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AxisBox.Location = new System.Drawing.Point(15, 12);
            this.AxisBox.Name = "AxisBox";
            this.AxisBox.Size = new System.Drawing.Size(1154, 460);
            this.AxisBox.TabIndex = 15;
            this.AxisBox.TabStop = false;
            // 
            // ColorDialogPlot
            // 
            this.ColorDialogPlot.Color = System.Drawing.Color.Red;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 641);
            this.Controls.Add(this.LabelPosition);
            this.Controls.Add(this.PlotBox);
            this.Controls.Add(this.AxisBox);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FunctionPlotter";
            ((System.ComponentModel.ISupportInitialize)(this.PlotBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown)).EndInit();
            this.BoxFromToY.ResumeLayout(false);
            this.BoxFromToY.PerformLayout();
            this.BoxFromToX.ResumeLayout(false);
            this.BoxFromToX.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AxisBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PlotBox;
        private System.Windows.Forms.Label LabelPosition;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ButtonDrawPlot;
        private System.Windows.Forms.TextBox EditFunction2;
        private System.Windows.Forms.Label LabelFunction2;
        private System.Windows.Forms.GroupBox BoxFromToY;
        private System.Windows.Forms.CheckBox CheckAutoY;
        private System.Windows.Forms.TextBox EditToY;
        private System.Windows.Forms.TextBox EditFromY;
        private System.Windows.Forms.Label LabelToY;
        private System.Windows.Forms.Label LablelFromY;
        private System.Windows.Forms.GroupBox BoxFromToX;
        private System.Windows.Forms.TextBox EditToX;
        private System.Windows.Forms.TextBox EditFromX;
        private System.Windows.Forms.Label ToLabel;
        private System.Windows.Forms.Label FromLabel;
        private System.Windows.Forms.Label LabelFunction1;
        private System.Windows.Forms.TextBox EditFunction1;
        private System.Windows.Forms.PictureBox AxisBox;
        private System.Windows.Forms.Button ButtonColorPlot;
        private System.Windows.Forms.ColorDialog ColorDialogPlot;
        private System.Windows.Forms.NumericUpDown NumericUpDown;
        private System.Windows.Forms.Label LabelPlotWidth;
        private System.Windows.Forms.ComboBox ComboBoxPlot;
        private System.Windows.Forms.CheckBox CheckBoxIntersect;
    }
}

