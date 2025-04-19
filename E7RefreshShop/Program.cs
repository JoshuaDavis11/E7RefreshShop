using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


namespace E7RefreshShop
{
    class Program
    {
        static void Main(string[] args)
        {
            int friendshipCounter = 0;
            int bookmarkCounter = 0;
            int mysticCounter = 0;
            int refreshCounter = 0;
            //Move the window to predefined size and position
            MoveWindowHelper.MoveGoogleGamesWindowHelper();

            //Start listening for Esc key press
            HotKeyListener.StartListening();

            //Get the path to the template images
            string refreshTemplatePath = Path.Combine("assests", "refreshbutton.png");
            Bitmap refreshTemplate = new Bitmap(refreshTemplatePath);

            string friendshipTemplatePath = Path.Combine("assests", "friendship.png");
            Bitmap friendshipTemplate = new Bitmap(friendshipTemplatePath);

            string bookmarksTemplatePath = Path.Combine("assests", "Covenant.png");
            Bitmap bookmarksTemplate = new Bitmap(bookmarksTemplatePath);

            string mysticTemplatePath = Path.Combine("assests", "mysticONE.png");
            Bitmap mysticTemplate = new Bitmap(bookmarksTemplatePath);

                    
            Point buyItemConfirmationPos = (new Point(1000, 625));

            while (!HotKeyListener.EscPressed)
            {
                System.Threading.Thread.Sleep(1000);
               
                //Take a screenshot of the game window
                var screenShot = ScreenshotHelper.TakeScreenShot();


                Point? refreshButtonPos = ScreenshotHelper.FindTemplate(screenShot, refreshTemplate);
                var friendshipPos = ScreenshotHelper.FindAllTemplateMatches(screenShot, friendshipTemplate);
                var bookmarkPos = ScreenshotHelper.FindAllTemplateMatches(screenShot, bookmarksTemplate);
                var mysticPos = ScreenshotHelper.FindAllTemplateMatches(screenShot, mysticTemplate);



                foreach (var pos in friendshipPos)
                {
                    //Console.WriteLine("Found Friendship");
                    MouseHelper.BuyItemClickAt(pos);
                    Thread.Sleep(1000);
                    MouseHelper.RefreshButtonClickAt(buyItemConfirmationPos);
                    Thread.Sleep(2000);
                    friendshipCounter++;
                }
                //currently the mystic template is never matched, need to fix this
                foreach (var pos in mysticPos)
                {
                    //Console.WriteLine("Found Mystic");
                    MouseHelper.BuyItemClickAt(pos);
                    Thread.Sleep(1000);
                    MouseHelper.RefreshButtonClickAt(buyItemConfirmationPos);
                    Thread.Sleep(2000);
                    mysticCounter++;
                }
                foreach (var pos in bookmarkPos)
                {
                    //Console.WriteLine("Found Bookmark");
                    MouseHelper.BuyItemClickAt(pos);
                    Thread.Sleep(1000);
                    MouseHelper.RefreshButtonClickAt(buyItemConfirmationPos);
                    Thread.Sleep(2000);
                    bookmarkCounter++;
                }
                if (refreshButtonPos != null)
                {
                    //Console.WriteLine("Found Refresh button");
                    MouseHelper.RefreshButtonClickAt(refreshButtonPos.Value);
                    Thread.Sleep(1000);
                    MouseHelper.RefreshButtonClickAt(new Point(1100, 600));
                    refreshCounter++;
                }
                Console.WriteLine($"Friendship Count: {friendshipCounter}, Bookmark Count: {bookmarkCounter}, " +
                    $"Mystic Count {mysticCounter}, Refresh Count {refreshCounter} ");
               Thread.Sleep(1000);

            }
        }
    }
}
