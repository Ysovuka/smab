using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smab.Systems.Tasks
{
    public class TaskScheduler : ITaskScheduler
    {
        private IList<Job> _jobs = new List<Job>();

        public TaskScheduler() { }

        public async Task<bool> Create(string name, Func<Task> action, string interval, DateTime lastRun = default(DateTime))
        {
            if (_jobs.Any(j => j.Name.Equals(name))) return false;

            TimeSpan jobInterval = TimeSpan.Parse(interval);
            Job job = new Job(name, action, jobInterval, lastRun);
            _jobs.Add(job);

            await job.Start();

            return true;
        }

        public async Task<bool> Delete(string name)
        {
            if (!_jobs.Any(j => j.Name.Equals(name))) return false;

            Job job = _jobs.FirstOrDefault(j => j.Name.Equals(name));
            await job.Stop();

            _jobs.Remove(job);

            return true;
        }

        public async Task Start()
        {
            foreach(var job in _jobs)
            {
                await job.Start();
            }
        }

        public async Task Start(string name)
        {
            if (!_jobs.Any(j => j.Name.Equals(name))) throw new InvalidOperationException();

            Job job = _jobs.FirstOrDefault(j => j.Name.Equals(name));
            await job.Start();
        }

        public async Task Stop()
        {
            foreach(var job in _jobs)
            {
                await job.Stop();
            }
        }

        public async Task Stop(string name)
        {
            if (!_jobs.Any(j => j.Name.Equals(name))) throw new InvalidOperationException();

            Job job = _jobs.FirstOrDefault(j => j.Name.Equals(name));
            await job.Stop();
        }
    }
}
