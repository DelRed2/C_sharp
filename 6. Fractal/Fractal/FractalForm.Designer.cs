namespace Fractal {
    partial class FractalForm {
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
            this.FractalBox = new System.Windows.Forms.PictureBox();
            this.Panel = new System.Windows.Forms.Panel();
            this.PanelInner = new System.Windows.Forms.Panel();
            this.UpDownIterations = new System.Windows.Forms.NumericUpDown();
            this.LabelIterations = new System.Windows.Forms.Label();
            this.UpDownFactor = new System.Windows.Forms.NumericUpDown();
            this.LabelScale = new System.Windows.Forms.Label();
            this.ButtonRepaint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FractalBox)).BeginInit();
            this.Panel.SuspendLayout();
            this.PanelInner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // FractalBox
            // 
            this.FractalBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FractalBox.Location = new System.Drawing.Point(0, 0);
            this.FractalBox.Name = "FractalBox";
            this.FractalBox.Size = new System.Drawing.Size(484, 462);
            this.FractalBox.TabIndex = 0;
            this.FractalBox.TabStop = false;
            this.FractalBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FractalBox_MouseDoubleClick);
            this.FractalBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FractalBox_MouseDown);
            this.FractalBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FractalBox_MouseMove);
            this.FractalBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FractalBox_MouseUp);
            // 
            // Panel
            // 
            this.Panel.Controls.Add(this.PanelInner);
            this.Panel.Controls.Add(this.ButtonRepaint);
            this.Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel.Location = new System.Drawing.Point(0, 435);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(484, 27);
            this.Panel.TabIndex = 7;
            // 
            // PanelInner
            // 
            this.PanelInner.Controls.Add(this.UpDownIterations);
            this.PanelInner.Controls.Add(this.LabelIterations);
            this.PanelInner.Controls.Add(this.UpDownFactor);
            this.PanelInner.Controls.Add(this.LabelScale);
            this.PanelInner.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelInner.Location = new System.Drawing.Point(0, 0);
            this.PanelInner.Name = "PanelInner";
            this.PanelInner.Size = new System.Drawing.Size(250, 27);
            this.PanelInner.TabIndex = 10;
            // 
            // UpDownIterations
            // 
            this.UpDownIterations.Location = new System.Drawing.Point(193, 4);
            this.UpDownIterations.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.UpDownIterations.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.UpDownIterations.Name = "UpDownIterations";
            this.UpDownIterations.Size = new System.Drawing.Size(50, 20);
            this.UpDownIterations.TabIndex = 12;
            this.UpDownIterations.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // LabelIterations
            // 
            this.LabelIterations.AutoSize = true;
            this.LabelIterations.Location = new System.Drawing.Point(114, 7);
            this.LabelIterations.Name = "LabelIterations";
            this.LabelIterations.Size = new System.Drawing.Size(73, 13);
            this.LabelIterations.TabIndex = 11;
            this.LabelIterations.Text = "Max Iterations";
            // 
            // UpDownFactor
            // 
            this.UpDownFactor.Location = new System.Drawing.Point(68, 3);
            this.UpDownFactor.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.UpDownFactor.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.UpDownFactor.Name = "UpDownFactor";
            this.UpDownFactor.Size = new System.Drawing.Size(40, 20);
            this.UpDownFactor.TabIndex = 10;
            this.UpDownFactor.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // LabelScale
            // 
            this.LabelScale.AutoSize = true;
            this.LabelScale.Location = new System.Drawing.Point(3, 5);
            this.LabelScale.Name = "LabelScale";
            this.LabelScale.Size = new System.Drawing.Size(64, 13);
            this.LabelScale.TabIndex = 9;
            this.LabelScale.Text = "Scale factor";
            // 
            // ButtonRepaint
            // 
            this.ButtonRepaint.Dock = System.Windows.Forms.DockStyle.Right;
            this.ButtonRepaint.Location = new System.Drawing.Point(409, 0);
            this.ButtonRepaint.Name = "ButtonRepaint";
            this.ButtonRepaint.Size = new System.Drawing.Size(75, 27);
            this.ButtonRepaint.TabIndex = 9;
            this.ButtonRepaint.Text = "Repaint";
            this.ButtonRepaint.UseVisualStyleBackColor = true;
            this.ButtonRepaint.Click += new System.EventHandler(this.ButtonRepaint_Click);
            // 
            // FractalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.Panel);
            this.Controls.Add(this.FractalBox);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "FractalForm";
            this.Text = "Fractal";
            this.Resize += new System.EventHandler(this.FractalForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.FractalBox)).EndInit();
            this.Panel.ResumeLayout(false);
            this.PanelInner.ResumeLayout(false);
            this.PanelInner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownFactor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox FractalBox;
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.Button ButtonRepaint;
        private System.Windows.Forms.Panel PanelInner;
        private System.Windows.Forms.NumericUpDown UpDownIterations;
        private System.Windows.Forms.Label LabelIterations;
        private System.Windows.Forms.NumericUpDown UpDownFactor;
        private System.Windows.Forms.Label LabelScale;
    }
}

