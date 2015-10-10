using System;
using System.Threading.Tasks;
using Events;
using Infrastructure;
using System.Threading;

namespace MyTests.Services.Implementation
{
    public interface ITestEvent
    {

    }
    public class TestEvent1:ITestEvent
    {
    }
   
   
    public class TestEventHandler1 : IHandler<TestEvent1>
    {
        public async Task<bool> HandleAsync(TestEvent1 e, CancellationToken ct)
        {
            return await e.ReplyAsync("TestEventHandler1");
        }
    }
    public class TestEventHandler2 : IHandler<TestEvent1>
    {
        public async Task<bool> HandleAsync(TestEvent1 e, CancellationToken ct)
        {
            await Task.Delay(1000);
            ct.ThrowIfCancellationRequested();
            return await e.ReplyAsync("TestEventHandler2");
        }
    }
    public class TestEventHandler3 : IHandler<TestEvent1>
    {
        public async Task<bool> HandleAsync(TestEvent1 e, CancellationToken ct)
        {
            await Task.Delay(2000);
            return await e.ReplyAsync("TestEventHandler3");
        }
    }
}
