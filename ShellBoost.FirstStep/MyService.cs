using Microsoft.Extensions.Hosting;
using ShellBoost.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShellBoost.FirstStep
{
    public class MyService : IHostedService
    {
        private readonly ShellFolderServer _shellFolderServer;
        private readonly ShellFolderConfiguration _shellFolderConfiguration;

        public MyService()
        {
            _shellFolderServer = new MyShellFolderServer();
            _shellFolderConfiguration = new ShellFolderConfiguration();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ShellFolderServer.RegisterNativeDll(RegistrationMode.User);

            _shellFolderServer.Start(_shellFolderConfiguration); // start the server
            Console.WriteLine("Started.");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _shellFolderServer.Stop();
            _shellFolderServer.Dispose();
            ShellFolderServer.UnregisterNativeDll(RegistrationMode.User);
            Console.WriteLine("Stoped.");

            return Task.CompletedTask;
        }
    }
}
