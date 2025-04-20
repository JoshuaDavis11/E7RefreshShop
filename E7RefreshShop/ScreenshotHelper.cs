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
           
            return screenshot;
        }

        //Template matching
        public static Point? FindTemplate(Bitmap sourceImage, Bitmap templateImage, double threshold = 0.90)
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

        public static List<Point> FindAllTemplateMatches(Bitmap sourceImage, Bitmap templateImage, double threshold = 0.95)
        {
            var source = sourceImage.ToImage<Bgr, byte>();
            var template = templateImage.ToImage<Bgr, byte>();

            // Convert to HSV and filter for colorful (non-grey) pixels
            var hsvImage = source.Convert<Hsv, byte>();
            var saturationChannel = hsvImage.Split()[1]; // S = Saturation

            // Threshold: only keep pixels with high enough saturation
            var mask = saturationChannel.ThresholdBinary(new Gray(80), new Gray(255)); // tweak the threshold if needed

            // Match template using masked image
            var result = source.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);

            var resultClone = result.Clone();
            List<Point> matchPoints = new List<Point>();

            while (true)
            {
                resultClone.MinMax(out double[] minVals, out double[] maxVals, out Point[] minLocs, out Point[] maxLocs);

                if (maxVals[0] < threshold)
                    break;

                Point matchLoc = maxLocs[0];
                int centerX = matchLoc.X + template.Width / 2 + 50;
                int centerY = matchLoc.Y + template.Height / 2 + 100;
                matchPoints.Add(new Point(centerX, centerY));

                Rectangle matchRect = new Rectangle(matchLoc, template.Size);
                resultClone.Draw(matchRect, new Gray(0), -1); // mask this match out
            }

            return matchPoints;
        }


        public static void CheckForCurrencies(Bitmap screenShot, Currency currency)
        {
            
            var friendshipPos = ScreenshotHelper.FindAllTemplateMatches(screenShot, currency.FriendShip);
            var bookmarkPos = ScreenshotHelper.FindAllTemplateMatches(screenShot, currency.Covenant);
            var mysticPos = ScreenshotHelper.FindAllTemplateMatches(screenShot, currency.Mystic);


            foreach (var pos in friendshipPos)
            {
                //Console.WriteLine("Found Friendship");
                MouseHelper.BuyItemClickAt(pos);
                Thread.Sleep(1000);
                MouseHelper.RefreshButtonClickAt(new Point(1000, 625));
                Thread.Sleep(2000);

            }
            //currently the mystic template is never matched, need to fix this
            foreach (var pos in mysticPos)
            {
                //Console.WriteLine("Found Mystic");
                MouseHelper.BuyItemClickAt(pos);
                Thread.Sleep(1000);
                MouseHelper.RefreshButtonClickAt(new Point(1000, 625));
                Thread.Sleep(2000);

            }
            foreach (var pos in bookmarkPos)
            {
                //Console.WriteLine("Found Bookmark");
                MouseHelper.BuyItemClickAt(pos);
                Thread.Sleep(1000);
                MouseHelper.RefreshButtonClickAt(new Point(1000, 625));
                Thread.Sleep(2000);

            }

           
        }
        public static void RefreshStore(Bitmap screenShot, Currency currency)
        {

            Point? refreshButtonPos = ScreenshotHelper.FindTemplate(screenShot, currency.RefreshButton);
            if (refreshButtonPos != null)
            {
                Console.WriteLine("Found Refresh button");
                MouseHelper.RefreshButtonClickAt(refreshButtonPos.Value);
                Thread.Sleep(1000);
                MouseHelper.RefreshButtonClickAt(new Point(1100, 600));

            }
        }

    }
}
