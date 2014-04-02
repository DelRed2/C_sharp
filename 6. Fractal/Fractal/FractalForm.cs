using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fractal {
    public partial class FractalForm : Form {

        private FractaltPosition Position = new FractaltPosition { Width = 4, Height = 4, CenterX = -1, CenterY = 0 };
        private Size lastWindowSize = Size.Empty;
        private bool leftMouseDown;
        private Point mouseClick;

        public FractalForm() {
            InitializeComponent();
            lastWindowSize = Size;
            MouseWheel += MouseWheelHandler;
            RepaintFractal();

        }

        private void MouseWheelHandler(object sender, MouseEventArgs e) {
            double factor = (e.Delta > 0 ? 1 / (double)UpDownFactor.Value : (double)UpDownFactor.Value);
            Position.Width *= factor;
            Position.Height *= factor;
            RepaintFractal();
        }

        private void RepaintFractal() {
            FractalBox.Image = FractalGenerator.Create(Position, FractalBox.Size.Width, FractalBox.Size.Height, (int) UpDownIterations.Value);
        }

        private void FractalForm_Resize(object sender, EventArgs e) {
            Position.Width *= Size.Width / (double) lastWindowSize.Width;
            Position.Height *= Size.Height / (double) lastWindowSize.Height;

            lastWindowSize = Size;
            RepaintFractal();
        }

        private void FractalBox_MouseDoubleClick(object sender, MouseEventArgs e) {
            Position.CenterX += ((e.X - (FractalBox.Width / 2.0)) / FractalBox.Width) * Position.Width;
            Position.CenterY += ((e.Y - (FractalBox.Height / 2.0)) / FractalBox.Height) * Position.Height;

            double factor = (e.Button == MouseButtons.Left ? 1 / (double) UpDownFactor.Value : (double)UpDownFactor.Value);
            Position.Width *= factor;
            Position.Height *= factor;
            RepaintFractal();
        }

        private void FractalBox_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                mouseClick = e.Location;
                leftMouseDown = false;
            }
        }

        private void FractalBox_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                mouseClick = e.Location;
                leftMouseDown = true;
            }
        }

        private void ButtonRepaint_Click(object sender, EventArgs e) {
            RepaintFractal();
        }

        private void FractalBox_MouseMove(object sender, MouseEventArgs e) {
            Point delta = new Point(e.X - mouseClick.X, e.Y - mouseClick.Y);
            if (delta != Point.Empty && leftMouseDown) {
                Position.CenterX -= delta.X * Position.Width / FractalBox.Width;
                Position.CenterY -= delta.Y * Position.Height / FractalBox.Height;

                mouseClick = e.Location;
                RepaintFractal();
            }
        }
    }
}
