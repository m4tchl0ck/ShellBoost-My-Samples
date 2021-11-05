using ShellBoost.Core.Synchronization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ShellBoost.FirstStep
{
    public class CustomFileSystem : ISyncFileSystem, ISyncFileSystemRead
    {
        public bool HasCapability(SyncFileSystemCapability capability)
            => false;

        public string RootId { get => "285286ae-d71c-4564-94ce-419162977f5a"; }
        public EndPointSynchronizer EndPointSynchronizer { get; set; }

        public IEnumerable<StateSyncEntry> EnumerateEntries(SyncContext context, StateSyncEntry parentEntry,
            SyncEnumerateEntriesOptions options = null)
        {
            return new[]
            {
                new StateSyncEntry() {Id = "SomeFile1", FileName = "SomeFile1.txt", Size = 13, Attributes = SyncEntryAttributes.None, CreationTime = DateTimeOffset.Now, LastWriteTime = DateTimeOffset.Now, ParentId = "285286ae-d71c-4564-94ce-419162977f5a"},
                new StateSyncEntry() {Id = "SomeFile2", FileName = "SomeFile2.txt", Size = 13, Attributes = SyncEntryAttributes.None, CreationTime = DateTimeOffset.Now, LastWriteTime = DateTimeOffset.Now, ParentId = "285286ae-d71c-4564-94ce-419162977f5a"},
                new StateSyncEntry() {Id = "SomeFile3", FileName = "SomeFile3.txt", Size = 13, Attributes = SyncEntryAttributes.None, CreationTime = DateTimeOffset.Now, LastWriteTime = DateTimeOffset.Now, ParentId = "285286ae-d71c-4564-94ce-419162977f5a"}
            };
        }

        public async Task GetEntryContentAsync(SyncContext context, StateSyncEntry entry, Stream output,
            SyncGetEntryContentOptions options = null)
        {
            await output.WriteAsync(Encoding.UTF8.GetBytes(entry.FileName));
        }
    }
}
