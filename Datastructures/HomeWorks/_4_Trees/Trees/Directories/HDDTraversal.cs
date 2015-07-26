namespace Directories
{
    using System.IO;
    using System.Linq;

    static class HddTraversal
    {
        public static Folder GetFilesAndFoldersForCurrentPath(string startPoint)
        {
            var foldersPath = Directory.GetDirectories(startPoint);
            var fileNames = Directory.GetFiles(startPoint);
            var files = new File[fileNames.Length];
            var dir = new DirectoryInfo(startPoint);
            var currentDirectoryName = dir.Name;
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo finfo = new FileInfo(fileNames[i]);
                files[i] = new File(finfo.Name, finfo.Length, fileNames[i]);
            }

            var currentFolderFilesSize = files.Sum(f => f.Size);

            var childFolders = new Folder[foldersPath.Length];
            for (int i = 0; i < foldersPath.Length; i++)
            {
                childFolders[i] = GetFilesAndFoldersForCurrentPath(foldersPath[i]);
            }

            return new Folder(dir.Name, startPoint, files.Sum(f => f.Size) + childFolders.Sum(f => f.Size), files, childFolders);
        }

        public static long GetSubtreeSize(Folder root, string subtreeRootName)
        {
            var subRoot = root.ChildFolders.FirstOrDefault(f => f.Name == subtreeRootName);
            if (subRoot != null)
            {
                return subRoot.Size;
            }

            return -1;
        }
    }
}
