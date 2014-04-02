using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using StackCalculator;

namespace FunctionPlotter {
    public partial class MainForm : Form {

        private double fromX;
        private double fromY;

        private double toX;
        private double toY;

        private double costX;
        private double costY;

        private Point mouseDown;
        private Point lastPoint;

        public MainForm() {
            InitializeComponent();
            MouseWheel += MouseWheelHandler;
            ComboBoxPlot.SelectedIndex = 0;
        }

        void DrawDraggingShape() {
                int minX = Math.Min(lastPoint.X, mouseDown.X);
                int maxX = Math.Max(lastPoint.X, mouseDown.X);
                int minY = Math.Min(lastPoint.Y, mouseDown.Y);
                int maxY = Math.Max(lastPoint.Y, mouseDown.Y);
                Point pnt = PlotBox.PointToScreen(new Point(minX, minY));
                ControlPaint.DrawReversibleFrame(new Rectangle(pnt, new Size(maxX - minX, maxY - minY)), Color.Red, FrameStyle.Thick);
        } 

        void MouseWheelHandler(object sender, MouseEventArgs e) {
            if (PlotBox.Enabled) {
                double minY = 0, maxY = 0;
                int absDelta = Math.Abs(e.Delta)/10;
                int sign = Math.Sign(e.Delta);

                double minX = (fromX + sign * absDelta / costX);
                double maxX = (toX - sign * absDelta / costX);

                    if (!CheckAutoY.Checked) {
                        minY = (fromY + sign * absDelta / costY);
                        maxY = (toY - sign * absDelta / costY);
                    }
                    if (Math.Abs(maxX - minX) >= 0.01 && (CheckAutoY.Checked || Math.Abs(maxY - minY) >= 0.01)) {
                        EditFromX.Text = minX.ToString();
                        EditToX.Text = maxX.ToString();
                        if (!CheckAutoY.Checked) {
                            EditFromY.Text = minY.ToString();
                            EditToY.Text = maxY.ToString();
                        }
                        ButtonDrawPlot_Click(sender, null);
                    }

            }
        }

        private double AbcMax(double a, double b) {
            return Math.Max(Math.Abs(a), Math.Abs(b));
        }

        private List<PointF>[] GetPoints(out List<PointF> intersect, params string[] functions) {
            checked {
                Dictionary<string, double> dict = new Dictionary<string, double>();
                List<PointF>[] pointsList = new List<PointF>[functions.Count()];
                intersect = new List<PointF>();

                for (int i = 0; i < functions.Count(); ++i) {
                    pointsList[i] = new List<PointF>();
                }

                double[] res = new double[functions.Count()];
                double[] resPrev = new double[functions.Count()];

                fromX = double.Parse(EditFromX.Text);
                toX = double.Parse(EditToX.Text);
                costX = PlotBox.Width/Math.Abs(toX - fromX);
                double epsilonX = 1f/(costX*(CheckBoxIntersect.Checked ? costX : 1));

                if (!CheckAutoY.Checked) {
                    fromY = double.Parse(EditFromY.Text);
                    toY = double.Parse(EditToY.Text);
                    costY = PlotBox.Height/Math.Abs(toY - fromY);

                    for (double i = fromX; i <= toX; i += epsilonX) {
                        dict["x"] = i;
                        for (int j = 0; j < functions.Count(); ++j) {
                            resPrev[j] = res[j];
                            res[j] = Calculator.Calculate(functions[j], dict);
                            if (!double.IsNaN(res[j])) {
                                double newY = (toY - res[j])*costY;
                                if (newY > AbcMax(toY, fromY)*costY*5) newY = AbcMax(toY, fromY)*costY*5;
                                if (newY < -AbcMax(toY, fromY) * costY * 5) newY = -AbcMax(toY, fromY) * costY * 5;
                                pointsList[j].Add(new PointF((float) ((i - fromX)*costX), (float) (newY)));
                            }
                        }
                        if (functions.Count() == 2 && i > fromX && CheckBoxIntersect.Checked) {
                            if ((resPrev[0] <= resPrev[1]) && (res[0] >= res[1]) ||
                                (resPrev[0] >= resPrev[1]) && (res[0] <= res[1])
                                || Math.Abs(res[0] - res[1]) < 1f/costY) {
                                intersect.Add(new PointF((float) ((i - fromX)*costX), (float) ((toY - res[0])*costY)));
                                intersect.Add(new PointF((float) ((i - fromX)*costX), (float) ((toY - res[1])*costY)));
                            }
                        }
                    }
                } else {
                    double[] maxY = new double[functions.Count()];
                    double[] minY = new double[functions.Count()];
                    for (int i = 0; i < functions.Count(); ++i) {
                        maxY[i] = double.NegativeInfinity;
                        minY[i] = double.PositiveInfinity;
                    }

                    for (double i = fromX; i <= toX; i += epsilonX) {
                        dict["x"] = i;
                        for (int j = 0; j < functions.Count(); ++j) {
                            resPrev[j] = res[j];
                            res[j] = Calculator.Calculate(functions[j], dict);
                            if (!double.IsNaN(res[j])) {
                                if (res[j] > maxY[j]) maxY[j] = res[j];
                                if (res[j] < minY[j]) minY[j] = res[j];
                                pointsList[j].Add(new PointF((float) ((i - fromX)*costX), (float) res[j]));
                            }
                        }
                    }

                    double maxInY = maxY.Max();
                    double minInY = minY.Min();

                    if (Math.Abs(maxInY - minInY) < double.Epsilon) {
                        maxInY += 1;
                        minInY -= 1;
                    }

                    costY = PlotBox.Height/Math.Abs(maxInY - minInY);

                    for (int j = 0; j < functions.Count(); ++j) {
                        for (int i = 0; i < pointsList[j].Count; ++i) {
                            pointsList[j][i] = new PointF(pointsList[j][i].X,
                                (float) ((maxInY - pointsList[j][i].Y)*costY));
                        }
                    }

                    if (functions.Count() == 2 && CheckBoxIntersect.Checked) {
                        for (int i = 0; i < intersect.Count; ++i) {
                            intersect[i] = new PointF(intersect[i].X, (float) ((maxInY - intersect[i].Y)*costY));
                        }
                    }

                    fromY = minInY;
                    toY = maxInY;

                    EditFromY.Text = (fromY).ToString();
                    EditToY.Text = (toY).ToString();

                }

                return pointsList;
            }
        }

        /*private List<PointF>[] GetPoints2(out List<PointF> intersect, params string[] functions) {
            Dictionary<string, double> dict = new Dictionary<string, double>();
            List<PointF>[] pointsList = new List<PointF>[functions.Count()];
            intersect = new List<PointF>();

            for (int i = 0; i < functions.Count(); ++i) {
                pointsList[i] = new List<PointF>();
            }

            double[] res = new double[functions.Count()];

            fromX = double.Parse(EditFromX.Text);
            toX = double.Parse(EditToX.Text);
            costX = PlotBox.Width/Math.Abs(toX - fromX);
            double epsilonX = 1f/costX;

            for (double i = fromX; i <= toX; i += epsilonX) {
                    dict["x"] = i;
                    for (int j = 0; j < functions.Count(); ++j) {
                        res[j] = Calculator.Calculate(functions[j], dict);
                        if (!double.IsNaN(res[j])) {
                            pointsList[j].Add(new PointF((float)i, (float)res[j]));
                        }
                    }
                }
            return pointsList;
        }*/

        private void PlotBox_MouseMove(object sender, MouseEventArgs e) {
            float x = (float)(e.X / costX + fromX);
            float y = (float)(toY - e.Y / costY);
            LabelPosition.Text = x + @" x " + y;

            if (e.Button == MouseButtons.Left) {
                DrawDraggingShape();
                lastPoint = e.Location;
                DrawDraggingShape();
            }
        }

        private void PlotBox_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                mouseDown = e.Location;
                lastPoint = e.Location;
            }
        }

        private void PlotBox_MouseUp(object sender, MouseEventArgs e) {
            double minX = Math.Min(e.X / costX + fromX, mouseDown.X / costX + fromX);
            double maxX = Math.Max(e.X / costX + fromX, mouseDown.X / costX + fromX);

            double minY = Math.Min(toY - e.Y / costY, toY - mouseDown.Y / costY);
            double maxY = Math.Max(toY - e.Y / costY, toY - mouseDown.Y / costY);

            if (Math.Abs(maxX - minX) >= 0.01 && Math.Abs(maxY - minY) >= 0.01) {
                EditFromX.Text = minX.ToString();
                EditToX.Text = maxX.ToString();

                EditFromY.Text = minY.ToString();
                EditToY.Text = maxY.ToString();
                CheckAutoY.Checked = false;
                ButtonDrawPlot_Click(PlotBox, null);
            }
            //
            if (e.Button == MouseButtons.Left) {
                DrawDraggingShape();
            }
        }

        private void ButtonDrawPlot_Click(object sender, EventArgs e) {
            PlotBox.Image = new Bitmap(PlotBox.Width, PlotBox.Height);
            AxisBox.Image = new Bitmap(AxisBox.Width, AxisBox.Height);
            using (Graphics grPlot = Graphics.FromImage(PlotBox.Image))
            using (Graphics grAxis = Graphics.FromImage(AxisBox.Image)) {
                List<PointF> intersect;
                List<PointF>[] pointsList = EditFunction2.Text.Equals("")
                    ? GetPoints(out intersect, EditFunction1.Text)
                    : GetPoints(out intersect, EditFunction1.Text, EditFunction2.Text);

                var blackPen = new Pen(Brushes.Black, 1);
                var grayPen = new Pen(Brushes.Lavender, 1);
                var font = new Font("Calibri", 11);
                int x = PlotBox.Left - AxisBox.Left;
                int y = PlotBox.Top + PlotBox.Height - AxisBox.Top;

                grAxis.DrawLine(blackPen, new PointF(x - 2, PlotBox.Top - AxisBox.Top - 1),
                    new PointF(x - 2, PlotBox.Height + AxisBox.Top + 2));
                grAxis.DrawLine(blackPen, new PointF(x + PlotBox.Width - 1, PlotBox.Top - AxisBox.Top - 1),
                    new PointF(x + PlotBox.Width - 1, PlotBox.Height + AxisBox.Top + 2));

                grAxis.DrawLine(blackPen, new PointF(x - 2, y - 1), new PointF(x + PlotBox.Width - 1, y - 1));
                grAxis.DrawLine(blackPen, new PointF(x - 2, y - 2 - PlotBox.Height),
                    new PointF(x + PlotBox.Width - 1, y - 2 - PlotBox.Height));

                if (!(fromX < 0 && toX > 0)) {
                    for (int i = 0; i < 11; ++i) {
                        float position = (float) (i*((double) PlotBox.Width)/10);
                        grPlot.DrawLine(grayPen, new PointF(position, 0), new PointF(position, PlotBox.Height));
                        grAxis.DrawString(((float) (fromX + position/costX)).ToString(), font,
                            Brushes.Black, new PointF(x + position - 10, y + 2),
                            new StringFormat(StringFormatFlags.NoClip));
                    }
                }

                x = 10;
                y = PlotBox.Top - AxisBox.Top;

                if (!(fromY < 0 && toY > 0)) {
                    for (int i = 0; i < 7; ++i) {
                        float position = (float) (i*((double) PlotBox.Height)/6);
                        grPlot.DrawLine(grayPen, new PointF(0, position), new PointF(PlotBox.Width, position));
                        grAxis.DrawString(((float) (toY - position/costY)).ToString(), font,
                            Brushes.Black, new PointF(x, y + position - 10), new StringFormat(StringFormatFlags.NoClip));
                    }
                }

                if (fromY < 0 && toY > 0) {
                    double cost = ((double) PlotBox.Height)/6;

                    for (double i = toY*costY; i <= PlotBox.Height; i += cost) {
                        grPlot.DrawLine(grayPen, new PointF(0, (float) i), new PointF(PlotBox.Width, (float) i));
                        grAxis.DrawString(((float) (toY - i/costY)).ToString(), font,
                            Brushes.Black, new PointF(x, (float) (y + i - 10)),
                            new StringFormat(StringFormatFlags.NoClip));
                    }
                    for (double i = toY*costY - cost; i > 0; i -= cost) {
                        grPlot.DrawLine(grayPen, new PointF(0, (float) i), new PointF(PlotBox.Width, (float) i));
                        grAxis.DrawString(((float) (toY - i/costY)).ToString(), font,
                            Brushes.Black, new PointF(x, (float) (y + i - 10)),
                            new StringFormat(StringFormatFlags.NoClip));
                    }
                }

                x = PlotBox.Left - AxisBox.Left;
                y = PlotBox.Top + PlotBox.Height - AxisBox.Top;

                if (fromX < 0 && toX > 0) {
                    double cost = ((double) PlotBox.Width)/10;

                    for (double i = -fromX*costX; i <= PlotBox.Width; i += cost) {
                        grPlot.DrawLine(grayPen, new PointF((float) i, 0), new PointF((float) i, PlotBox.Height));
                        grAxis.DrawString(((float) (fromX + i/costX)).ToString(), font,
                            Brushes.Black, new PointF((float) (x + i - 6), y + 4),
                            new StringFormat(StringFormatFlags.NoClip));
                    }
                    for (double i = -fromX*costX - cost; i > 0; i -= cost) {
                        grPlot.DrawLine(grayPen, new PointF((float) i, 0), new PointF((float) i, PlotBox.Height));
                        grAxis.DrawString(((float) (fromX + i/costX)).ToString(), font,
                            Brushes.Black, new PointF((float) (x + i - 6), y + 4),
                            new StringFormat(StringFormatFlags.NoClip));
                    }

                    grPlot.DrawLine(blackPen, new PointF((float) (-fromX*costX), 0),
                        new PointF((float) (-fromX*costX), PlotBox.Height));
                }
                if (fromY < 0 && toY > 0) {
                    grPlot.DrawLine(blackPen, new PointF(0, (float) (toY*costY)),
                        new PointF(PlotBox.Width, (float) (toY*costY)));
                }

                grPlot.SmoothingMode = SmoothingMode.AntiAlias;
                Pen plotPen = new Pen(ButtonColorPlot.BackColor, (float) NumericUpDown.Value);

                switch (ComboBoxPlot.SelectedIndex) {
                    case 0:
                        plotPen.DashStyle = DashStyle.Solid;
                        break;
                    case 1:
                        plotPen.DashStyle = DashStyle.Dot;
                        break;
                    case 2:
                        plotPen.DashStyle = DashStyle.Dash;
                        break;
                    case 3:
                        plotPen.DashStyle = DashStyle.DashDot;
                        break;
                    case 4:
                        plotPen.DashStyle = DashStyle.DashDotDot;
                        break;
                }

                for (int i = 0; i < pointsList.Count(); ++i) {
                    PointF[] pnts = pointsList[i].ToArray();
                    grPlot.DrawCurve(plotPen, pnts);
                }

                if (CheckBoxIntersect.Checked) {
                    foreach (var point in intersect) {
                        grPlot.DrawEllipse(new Pen(Brushes.Blue, 2), point.X, point.Y, 2, 2);
                    }
                }

                LabelPosition.Text = "";
                LabelPosition.Visible = true;
                PlotBox.Enabled = true;
            }
        }

        private void CheckAutoY_CheckStateChanged(object sender, EventArgs e) {
            EditFromY.Enabled = !CheckAutoY.Checked;
            EditToY.Enabled = !CheckAutoY.Checked;
        }

        private void ButtonColorPlot_Click(object sender, EventArgs e) {
            ColorDialogPlot.ShowDialog();
            ButtonColorPlot.BackColor = ColorDialogPlot.Color;
        }

        private void EditFunction2_TextChanged(object sender, EventArgs e) {
            CheckBoxIntersect.Enabled = (EditFunction2.Text != "");
        }

        private void CheckBoxIntersect_CheckedChanged(object sender, EventArgs e) {
            CheckAutoY.Checked = false;
            CheckAutoY.Enabled = !CheckBoxIntersect.Checked;
        }

    }

}
