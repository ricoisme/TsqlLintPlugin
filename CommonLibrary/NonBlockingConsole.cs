using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public static class NonBlockingConsole
    {
        public static BlockingCollection<string> messageQueue = new BlockingCollection<string>();
        public static void Consumer()
        {
            var cts = new CancellationTokenSource();
            var t = Task.Run(() => NonBlockingConsumer(messageQueue, cts.Token));
            t.Wait();
        }
        static void NonBlockingConsumer(BlockingCollection<string> bc, CancellationToken ct)
        {
            var timeout = 10000;
            // IsCompleted == (IsAddingCompleted && Count == 0)
            while (bc.Count > 0 && !bc.IsCompleted)
            {
                var nextItem = "";
                try
                {
                    if (!bc.TryTake(out nextItem, timeout, ct))
                    {
                        Console.WriteLine(" Take Blocked");
                    }
                    else
                    {
                        Console.WriteLine(" Take:{0}", nextItem);
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Taking canceled.");
                    break;
                }
                // Slow down consumer just a little to cause
                // collection to fill up faster, and lead to "AddBlocked"
                //Thread.Sleep(1000);
            }
            Console.WriteLine("\r\nNo more items to take.");
        }

        public static void WriteLine(string value)
        {
            messageQueue.TryAdd(value, 10000);
        }
    }
}
