using ShellBoost.Core;

namespace ShellBoost.FirstStep
{
    public class MyShellFolderServer : ShellFolderServer // this base class is located in ShellBoost.Core
    {
        private MyRootFolder _root;

        // only the Shell knows our root folder PIDL
        protected override ShellFolder GetFolderAsRoot(ShellItemIdList idl)
        {
            if (_root == null)
            {
                _root = new MyRootFolder(idl);
            }
            return _root;
        }
    }
}
