namespace Directories
{
    using System;
    using System.Linq;

    class Test
    {
        static void Main()
        {
            // Сменете папката с такава която не изисква административни права на файловата Ви система.
            var trav = HddTraversal.GetFilesAndFoldersForCurrentPath(@"D:\SoftUni");
            
            // divide result by 1048576 to get result in MB
            Console.WriteLine(trav.Size);

            // Сменето името на подпапката, която търсите според конкретния случей. 
            Console.WriteLine(HddTraversal.GetSubtreeSize(trav, "Demos"));
        }
    }
}
