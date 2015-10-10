using Events;
using MyTests.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AutofacConfig.Register();
            //new TestEvent1().WaitAsync<string>();
            //var res = new TestEvent1().WaitAsync<string>().Result;
            //var obs = new TestEvent1().WaitAllAsync<string>();
            using (Event.Subscribe((object x) => Task.FromResult(true), false))
            {
                try
                {
                    bool r = (1).RaiseAsync().Result;
                }
                catch
                {
                    Console.WriteLine("Value types cannot be used to raise event");
                }
            }
            Console.WriteLine("\tTest1");
            Console.WriteLine("TestEventHandler2 is going to be canceled, but TestEventHandler1 and TestEventHandler3 will reply");

            CancellationTokenSource ctc = new CancellationTokenSource(900);
            try
            {
                new TestEvent1().ExecuteAsync(async (string e) =>
                {
                    Console.WriteLine(e);
                    return true;
                }, ctc.Token).Wait();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\tTest2");
            Console.WriteLine("WaitAll() returns TestEventHandler1's and TestEventHandler3's replies");
            ctc = new CancellationTokenSource(900);
            IEnumerable<string> results = null;
            try
            {
                results = new TestEvent1().WaitAll<string>(ctc.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                foreach (var r in results)
                {
                    Console.WriteLine(r);
                }
            }

            Console.WriteLine("\tTest3");
            Console.WriteLine("WaitAsync casts cancel exception");
            ctc = new CancellationTokenSource(900);
            try
            {
                var res = new TestEvent1().WaitAsync<string>(ctc.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
