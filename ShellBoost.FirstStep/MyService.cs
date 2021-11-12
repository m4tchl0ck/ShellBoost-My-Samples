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
        private readonly MultiPointSynchronizer _mps;

        public MyService()
        {
            // create the MPS. We need a globally unique identifier (could be a guid)
            _mps = new MultiPointSynchronizer("MyCompany.MyProgram");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var onDemandRootPath = @"c:\tmp\somePath";

            var displayName = "My Cloud File Provider";
            ShellRegistration.RegisterCloudStorageProvider(onDemandRootPath, displayName, Guid.Parse("0495e8fd-db76-4dae-a7fa-69f9135c6d88"));

            // Register the File On-Demand root, use an end-user friendly display name.
            // Note his must not be ran all the time, just once, for example at install time
            var reg = new OnDemandLocalFileSystemRegistration();
            reg.ProviderName = displayName;
            reg.ProviderDisplayName = reg.ProviderName;

            OnDemandLocalFileSystem.EnsureRegistered(onDemandRootPath, reg);

            // add one endpoint with the ShellBoost-provided on-demand local file system
            _mps.AddEndPoint("local", new OnDemandLocalFileSystem(onDemandRootPath));

            // add one endpoint with a custom file system (cloud, etc.)
            _mps.AddEndPoint("local2", new CustomFileSystem());

            // start the MPS
            _mps.Start();

            Console.WriteLine("Started.");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _mps.Stop();
            _mps.Dispose();

            ShellRegistration.UnregisterCloudStorageProvider(Guid.Parse("0495e8fd-db76-4dae-a7fa-69f9135c6d88"));
            Console.WriteLine("Stoped.");

            return Task.CompletedTask;
        }
    }
}
