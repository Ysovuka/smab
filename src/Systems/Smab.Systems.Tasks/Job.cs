using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Smab.Systems.Tasks
{
    public class Job
    {
        private AutoResetEvent _waitEvent = new AutoResetEvent(false);
        private RegisteredWaitHandle _handle;
        private readonly Func<Task> _action;
        private readonly TimeSpan _interval;

        private DateTime _lastRun;
        private bool _running = false;
        public Job(string name, Func<Task> action, TimeSpan interval, DateTime lastRun = default(DateTime))
        {
            Name = name;
            _action = action;
            _interval = interval;
            _lastRun = (lastRun == default(DateTime)) ? DateTime.Now : lastRun;
        }

        public string Name { get; private set; }
        public Task Start()
        {
            _running = true;
            _handle = ThreadPool.RegisterWaitForSingleObject(_waitEvent,
                new WaitOrTimerCallback(WaitProcedure), this, 1000, false);

            return Task.CompletedTask;
        }

        public Task Stop()
        {
            _running = false;
            return Task.CompletedTask;
        }

        private async void WaitProcedure(object state, bool timedOut)
        {
            if (!_running)
            {
                _handle.Unregister(null);
                return;
            }

            if (_lastRun.Add(_interval) < DateTime.Now)
            {
                _lastRun = DateTime.Now;
                await _action();
            }
        }
    }
}
