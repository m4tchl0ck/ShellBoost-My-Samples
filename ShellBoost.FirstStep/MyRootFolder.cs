using ShellBoost.Core;
using ShellBoost.Core.WindowsShell;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            yield return new ShellItem(this, new StringKeyShellItemId("My First Item.txt"));
            yield return new OnDemandFolder(this);

            foreach (var shellItem in base.EnumItems(options))
            {
                yield return shellItem;
            }
        }

        protected override void MergeContextMenu(ShellFolder folder, IReadOnlyList<ShellItem> items, ShellMenu existingMenu, ShellMenu appendMenu)
        {
            if (items.Count == 1 && !items[0].IsFolder)
            {
                // this is one shell item, do something else
            }
            else
            {
                // tags are placeholder for anything. we use them to remember what's the real command behind the menu item
                if (existingMenu.Items.FirstOrDefault(i => i.Text == "New") == null)
                {
                    var newItem = new ShellMenuItem(appendMenu, "New Item");
                    appendMenu.Items.Add(newItem);
                }
            }
        }
    }

    public class OnDemandFolder : ShellFolder
    {
        public OnDemandFolder(ShellFolder parent)
            : base(parent, new DirectoryInfo($@"C:\tmp\somePath"))
        {

        }

        protected override void MergeContextMenu(ShellFolder folder, IReadOnlyList<ShellItem> items, ShellMenu existingMenu, ShellMenu appendMenu)
        {
            base.MergeContextMenu(folder, items, existingMenu, appendMenu);

            // tags are placeholder for anything. we use them to remember what's the real command behind the menu item
            if (existingMenu.Items.FirstOrDefault(i => i.Text == "New") == null)
            {
                var newItem = new ShellMenuItem(appendMenu, "New");
                appendMenu.Items.Add(newItem);
            }
        }
    }
}
