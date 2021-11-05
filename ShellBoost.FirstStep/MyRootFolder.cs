using ShellBoost.Core;

namespace ShellBoost.FirstStep
{
    public class MyRootFolder : ShellFolder  // this base class is located in ShellBoost.Core
    {
        // reference to the ShellFolderServer is now available as the FolderServer instance property 
        public MyRootFolder(ShellItemIdList idList)
            : base(idList)
        {
        }
    }
}
