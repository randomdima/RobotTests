using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Anki.Vector;

namespace RobotTests
{
    public class RobotScheduler : IAsyncDisposable
    {
        private Robot _robot;
        private List<Task> _tasks = new List<Task>();
        private CancellationTokenSource _tokenSource;

        private RobotScheduler()
        {
        }

        public static async Task<RobotScheduler> Connect()
        {
            var scheduler = new RobotScheduler();
            await scheduler.ConnectInernal();
            return scheduler;
        }

        private async Task ConnectInernal()
        {
            _robot = await Robot.NewConnection();
            _tokenSource = new CancellationTokenSource();
            await _robot.Control.RequestControl();
        }

        public void AddBehavior(Func<Robot, CancellationToken, Task> fn)
        {
            var task = Task.Run(() => fn(_robot, _tokenSource.Token), _tokenSource.Token);
            _tasks.Add(task);
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                _tokenSource.Cancel();
                await Task.WhenAll(_tasks);
                await _robot.Motors.StopAllMotors();
            }
            finally
            {
                _robot.Dispose();
                _tokenSource.Dispose();
            }
        }
    }
}