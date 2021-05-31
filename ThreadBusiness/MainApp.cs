using System;
using System.Collections.Generic;
using System.Threading;

namespace ThreadAppBusiness
{
    public class MainApp
    {
        object tLock = new object();
        private string thread_name = "";
        Thread t;
        Thread t2;

        public event EventHandler AppEvent;

        public MainApp()
        {
            // Thread 1
            t = new Thread(Thread1);
            t.Name = "Thread1";
            t.IsBackground = false;
            t.Start();

            // Thread 2
            t2 = new Thread(Thread2);
            t2.Name = "Thread2";
            t2.IsBackground = true;
            t2.Start();
        }

        private void Thread1()
        {

            for (int i = 0; i < 1000; i++)
            {
                lock (tLock)
                {
                    thread_name = Thread.CurrentThread.Name + " - " + i.ToString();

                    // Invoke the event
                    AppEvent?.Invoke(this, null);
                }

                // Wait for 3s
                Thread.Sleep(3000);
            }           
        }

        private void Thread2()
        {
            for (int i = 0; i < 1000; i++)
            {
                lock (tLock)
                {                    
                    thread_name = Thread.CurrentThread.Name + " - " + i.ToString();

                    // Invoke the event
                    AppEvent?.Invoke(this, null);                 
                }

                // Wait for 5s
                Thread.Sleep(5000);
            }                        
        }

        public string ThreadName()
        {
            return thread_name;
        }

        public bool SetLock()
        {
            // If true get lock
            if (Monitor.TryEnter(tLock))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ReleaseLock()
        {
            // Release the lock
            Monitor.Exit(tLock);
        }


        public void AbortThreads()
        {
            t.Abort();
            t2.Abort();
        }

    }
}
