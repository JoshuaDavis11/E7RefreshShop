using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.IO;


namespace E7RefreshShop
{
    class Program
    {
        static void Main(string[] args)
        {
            //Move the window to predefined size and position
            MoveWindowHelper.MoveGoogleGamesWindowHelper();

            //Get the path to the template images
            string refreshTemplatePath = Path.Combine("assests", "refreshbutton.png");
            Bitmap refreshTemplate = new Bitmap(refreshTemplatePath);

            string friendshipTemplatePath = Path.Combine("assests", "friendship.png");
            Bitmap friendshipTemplate = new Bitmap(friendshipTemplatePath);

            //string bookmarksTemplatePath = Path.Combine("assests", "cov.JPG");
            //Bitmap bookmarksTemplate = new Bitmap(bookmarksTemplatePath);

            //string mysticTemplatePath = Path.Combine("assests", "mys.JPG");
            //Bitmap mysticTemplate = new Bitmap(bookmarksTemplatePath);






            //TODO: loop this entire process until the user presses a key
            //Take a screenshot of the game window
            var screenShot = ScreenshotHelper.TakeScreenShot();

            //Compare the screenshot with the template
            Point refreshButtonPos = (Point)ScreenshotHelper.FindTemplate(screenShot, refreshTemplate);
            Point friendshipPos = (Point)ScreenshotHelper.FindTemplate(screenShot, friendshipTemplate);

            //Point bookmarkPos = (Point)ScreenshotHelper.FindTemplate(screenShot, bookmarksTemplate);
            //Point mysticPos = (Point)ScreenshotHelper.FindTemplate(screenShot, mysticTemplate);


            //Move mouse to the location of the refresh button
            //MouseHelper.MoveMouse(refreshButtonPos.X,refreshButtonPos.Y);
            //MouseHelper.LeftClick();

            //Move mouse to the location of the friendship button
            if(friendshipPos != null)
            {
                Console.WriteLine(friendshipPos);
                MouseHelper.ClickAt(friendshipPos);
            }
            else
            {
                
            }
            


        }
    }
}
