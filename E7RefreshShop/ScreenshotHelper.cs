using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E7RefreshShop
{
    public class ScreenshotHelper
    {
        public static Bitmap GetScreenshot(Rectangle area)
        {
            Bitmap bmp = new(area.Width, area.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(area.Location, Point.Empty, area.Size);
            }

            return bmp;
        }
        public static Bitmap TakeScreenShot()
        {
            Rectangle captureArea = new Rectangle(100, 100, 1650, 739);
            Bitmap screenshot = GetScreenshot(captureArea);
            screenshot.Save("sceenshot.png");
            Console.WriteLine("Screenshot saved as screenshot.png");

            return screenshot;
        }

        // Template matching
        public static Point? FindTemplate(Bitmap sourceImage, Bitmap templateImage, double threshold = 0.95)
        {
            using var source = sourceImage.ToImage<Bgr, byte>();
            using var template = templateImage.ToImage<Bgr, byte>();
            using var result = source.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);

            result.MinMax(out double[] minValues, out double[] maxValues, out Point[] minLoc, out Point[] maxLoc);

            if (maxValues[0] >= threshold)
            {
                // Adjust to center of match
                int centerX = maxLoc[0].X + template.Width / 2 + 50;
                int centerY = maxLoc[0].Y + template.Height / 2 + 100;
                return new Point(centerX, centerY);
            }

            return null;
        }

    }
}
