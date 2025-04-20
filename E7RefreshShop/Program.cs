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
            // Load the images for the currencies
            Currency currency = new Currency();
            currency.RefreshButton = new Bitmap(Path.Combine("assests", "refreshbutton.png"));
            currency.FriendShip = new Bitmap(Path.Combine("assests", "friendship.png"));
            currency.Mystic = new Bitmap(Path.Combine("assests", "mysticONE.png"));
            currency.Covenant = new Bitmap(Path.Combine("assests", "Covenant.png"));

            //Move the window to predefined size and position
            MoveWindowHelper.MoveGoogleGamesWindowHelper();

            //Start listening for Esc key press
            HotKeyListener.StartListening();

            // Loop over screenshot and check for currencies untill user presses Esc
            while (!HotKeyListener.EscPressed)
            {
                //Wait for a second to allow the game to load
                System.Threading.Thread.Sleep(1000);
               
                //Take a screenshot of the game window
                var screenShot = ScreenshotHelper.TakeScreenShot();

                //Check for currencies in the screenshot
                ScreenshotHelper.CheckForCurrencies(screenShot,currency);

                //need to add functionaly here so that the program scrolls to the bottom of the shop and checks all again to ensure all items were verified
                MouseHelper.ScrollDown();

                //Sleep for a second to allow the scroll to finish
                System.Threading.Thread.Sleep(1500);

                //Take a screenshot of the game window
                var screenShotAfterScrolling = ScreenshotHelper.TakeScreenShot();

                //Check for currencies in the screenshot after scrolling
                ScreenshotHelper.CheckForCurrencies(screenShotAfterScrolling, currency);
                ScreenshotHelper.RefreshStore(screenShotAfterScrolling, currency);

                //Sleep the program to allow for the refresh animation to finish before restarting the loop
                Thread.Sleep(1000);
            }
        }
    }
}
