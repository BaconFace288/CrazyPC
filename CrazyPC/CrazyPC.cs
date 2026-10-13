using System;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace CrazyPC
{
    internal class CrazyPC
    {
        // Random object to generate random numbers for all threads
        public static Random _random = new Random();

        // Flag to gracefully signal all threads when the time limit expires
        public static volatile bool _isRunning = true;

        // 1. Crazy Mouse Movement Thread
        public static void CrazyMouseThread()
        {
            Console.WriteLine("[Thread Started] Crazy Mouse Movement");
            while (_isRunning)
            {
                // Generate random screen coordinates within the primary screen bounds
                int screenWidth = Screen.PrimaryScreen?.Bounds.Width ?? 1920;
                int screenHeight = Screen.PrimaryScreen?.Bounds.Height ?? 1080;

                int moveX = _random.Next(screenWidth);
                int moveY = _random.Next(screenHeight);

                // Set mouse position using System.Drawing.Point
                Cursor.Position = new Point(moveX, moveY);

                // Sleep between moves so the user can see the mouse jump around
                Thread.Sleep(_random.Next(150, 400));
            }
        }

        // 2. Keyboard Input Thread
        public static void CrazyKeyboardThread()
        {
            Console.WriteLine("[Thread Started] Crazy Keyboard Input");
            while (_isRunning)
            {
                // Generate a random letter from 'A' to 'Z'
                char key = (char)(_random.Next(26) + 65);

                // 50% chance to make it lowercase
                if (_random.Next(2) == 0)
                {
                    key = char.ToLower(key);
                }

                // Send the keystroke to the active window
                SendKeys.SendWait(key.ToString());

                // Sleep between keystrokes
                Thread.Sleep(_random.Next(500, 1500));
            }
        }

        // 3. System Sounds Thread
        public static void CrazySoundThread()
        {
            Console.WriteLine("[Thread Started] Crazy System Sounds");
            while (_isRunning)
            {
                // Randomly select one of the 5 standard system sounds
                int soundChoice = _random.Next(5);
                switch (soundChoice)
                {
                    case 0:
                        SystemSounds.Asterisk.Play();
                        break;
                    case 1:
                        SystemSounds.Beep.Play();
                        break;
                    case 2:
                        SystemSounds.Exclamation.Play();
                        break;
                    case 3:
                        SystemSounds.Hand.Play();
                        break;
                    case 4:
                        SystemSounds.Question.Play();
                        break;
                }

                // Sleep between playing sounds
                Thread.Sleep(_random.Next(1000, 2500));
            }
        }

        // 4. Pop-up Messages Thread
        public static void CrazyPopupThread()
        {
            Console.WriteLine("[Thread Started] Crazy Pop-up Messages");
            string[] messages = new string[]
            {
                "Warning: CPU fan running in reverse!",
                "Critical Alert: Mouse battery is dangerously caffeinated.",
                "Error 404: Desktop wallpaper not found.",
                "Is your refrigerator running? You better catch it!",
                "Notice: System is performing unexpected operations."
            };

            while (_isRunning)
            {
                // Wait a few seconds before popping up a message box
                Thread.Sleep(_random.Next(3000, 6000));

                if (!_isRunning) break;

                string randomMessage = messages[_random.Next(messages.Length)];

                // Show a pop-up window
                MessageBox.Show(randomMessage, "CrazyPC Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Method to initialize and run all threads concurrently
        public static void CrazyFunctionCall()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("   CrazyPC Multithreading Lab Demo Starting");
            Console.WriteLine("   All 4 threads will run simultaneously for 20s");
            Console.WriteLine("=================================================\n");

            _isRunning = true;

            // Step 4: Create Thread objects using ThreadStart delegates
            Thread mouseThread = new Thread(new ThreadStart(CrazyMouseThread));
            Thread keyboardThread = new Thread(new ThreadStart(CrazyKeyboardThread));
            Thread soundThread = new Thread(new ThreadStart(CrazySoundThread));
            Thread popupThread = new Thread(new ThreadStart(CrazyPopupThread));

            // Mark threads as background threads so they automatically terminate if the app exits
            mouseThread.IsBackground = true;
            keyboardThread.IsBackground = true;
            soundThread.IsBackground = true;
            popupThread.IsBackground = true;

            // Start each thread
            mouseThread.Start();
            keyboardThread.Start();
            soundThread.Start();
            popupThread.Start();

            // Run for 20 seconds as required by the lab
            Console.WriteLine("\n[Main Thread] Sleeping for 20 seconds while worker threads run...\n");
            Thread.Sleep(20000);

            Console.WriteLine("\n[Main Thread] 20 seconds elapsed. Stopping threads...");

            // Signal worker loops to stop gracefully
            _isRunning = false;

            // If running on legacy .NET Framework, Thread.Abort() is traditionally called here.
            // In modern .NET (.NET Core / .NET 5+), Thread.Abort throws PlatformNotSupportedException,
            // so we handle both safely.
            try
            {
#pragma warning disable SYSLIB0006
                // Traditional .NET Framework abort:
                // mouseThread.Abort();
                // keyboardThread.Abort();
                // soundThread.Abort();
                // popupThread.Abort();
#pragma warning restore SYSLIB0006
            }
            catch (PlatformNotSupportedException)
            {
                // Graceful fallback for modern .NET runtimes
            }

            Console.WriteLine("[Main Thread] All threads stopped successfully!");
        }
    }
}
