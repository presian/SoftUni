namespace Directories
{
    class Folder
    {
        private string name;
        private string path;
        private File[] files;
        private Folder[] childFolders;
        private long size;

        public Folder(string name, string path, long size, File[] files=null, Folder[] childFolders=null)
        {
            this.Name = name;
            if (files == null)
            {
                files = new File[0];
            }

            this.Files = files;
            if (childFolders == null)
            {
                childFolders = new Folder[0];
            }

            this.ChildFolders = childFolders;

            this.Size = size;
            this.Path = path;
        }

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }

        public long Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public File[] Files
        {
            get
            {
                return this.files;
            }
            set
            {
                this.files = value;
            }
        }

        public Folder[] ChildFolders
        {
            get
            {
                return this.childFolders;
            }
            set
            {
                this.childFolders = value;
            }
        }
    }
}
