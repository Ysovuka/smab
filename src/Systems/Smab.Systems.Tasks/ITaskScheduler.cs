using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Systems.Tasks
{
    public interface ITaskScheduler
    {
        Task<bool> Create(string name, Func<Task> action, string interval, DateTime lastRun = default(DateTime));
        Task<bool> Delete(string name);
        Task Start();
        Task Start(string name);
        Task Stop();
        Task Stop(string name);
    }
}
