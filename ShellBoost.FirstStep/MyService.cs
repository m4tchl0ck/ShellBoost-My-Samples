using Microsoft.Extensions.Hosting;
using ShellBoost.Core;
using ShellBoost.Core.Synchronization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShellBoost.FirstStep
{
    public class MyService : IHostedService
    {
        private readonly ShellFolderServer _shellFolderServer;
        private readonly ShellFolderConfiguration _shellFolderConfiguration;
        private MultiPointSynchronizer _mps;

        public MyService()
        {
            _shellFolderServer = new MyShellFolderServer();
            _shellFolderConfiguration = new ShellFolderConfiguration();

            // create the MPS. We need a globally unique identifier (could be a guid)
            _mps = new MultiPointSynchronizer("MyCompany.MyProgram");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ShellFolderServer.RegisterNativeDll(RegistrationMode.User);

            var onDemandRootPath = @"c:\tmp\somePath";

            // Register the File On-Demand root, use an end-user friendly display name.
            // Note his must not be ran all the time, just once, for example at install time
            var reg = new OnDemandLocalFileSystemRegistration();
            reg.ProviderName = "My Cloud File Provider";

            OnDemandLocalFileSystem.EnsureRegistered(onDemandRootPath, reg);

            // add one endpoint with the ShellBoost-provided on-demand local file system
            _mps.AddEndPoint("local", new OnDemandLocalFileSystem(onDemandRootPath));

            // add one endpoint with a custom file system (cloud, etc.)
            _mps.AddEndPoint("local2", new CustomFileSystem());

            // start the MPS
            _mps.Start();

            _shellFolderServer.Start(_shellFolderConfiguration); // start the server
            Console.WriteLine("Started.");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _shellFolderServer.Stop();
            _shellFolderServer.Dispose();
            _mps.Stop();
            _mps.Dispose();
            ShellFolderServer.UnregisterNativeDll(RegistrationMode.User);
            Console.WriteLine("Stoped.");

            return Task.CompletedTask;
        }
    }
}
