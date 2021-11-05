using ShellBoost.Core;
using ShellBoost.Core.WindowsShell;
using System.Collections.Generic;

namespace ShellBoost.FirstStep
{
    public class MyRootFolder : ShellFolder  // this base class is located in ShellBoost.Core
    {
        // reference to the ShellFolderServer is now available as the FolderServer instance property 
        public MyRootFolder(ShellItemIdList idList)
            : base(idList)
        {
        }

        public override IEnumerable<ShellItem> EnumItems(SHCONTF options)
        {
            // note by default, ShellBoost uses the key/ID value as the display name if it’s not explicitly defined
            yield return new ShellFolder(this, new StringKeyShellItemId("My First Folder"));
            yield return new ShellItem(this, new StringKeyShellItemId("My First Item"));
        }
    }
}
