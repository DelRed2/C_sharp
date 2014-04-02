using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;

namespace Fractal {

    public struct FractaltPosition {
        public double Width, Height, CenterX, CenterY;
    }

    class FractalGenerator {

        public static Bitmap Create(FractaltPosition position, int imageWidth, int imageHeight, int maxIterations) {
            double left = position.CenterX - (position.Width / 2.0);
            double top = position.CenterY - (position.Height / 2.0);

            double costX = position.Width / imageWidth;
            double costY = position.Height / imageHeight;

            byte[] data = new byte[imageWidth * imageHeight];

            Parallel.For(0, imageHeight, y => {
                for (int x = 0; x < imageWidth; ++x) {
                    Complex c = new Complex(x * costX + left, y * costY + top);
                    Complex z = c;
                    for (int iteration = 0; iteration < maxIterations; iteration++) {
                        if (z.Magnitude > 4) {
                            data[y * imageWidth + x] = (byte)iteration;
                            break;
                        }
                        z = (z * z) + c;
                    }
                }
            });

            Bitmap bitmap = new Bitmap(imageWidth, imageHeight);
            for (int y = 0; y < imageHeight; ++y) {
                for (int x = 0; x < imageWidth; ++x) {
                    bitmap.SetPixel(x, y, Color.FromArgb(255, 0, data[y * imageWidth + x] * 5 % 256, data[y * imageWidth + x] * 5 % 256));
                }
            }
            return bitmap;
        }
    }
}
