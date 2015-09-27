namespace TowerOfHanoi
{
    class TowerOfHanoiMain
    {
        static void Main()
        {
            int numberOfDisks = 5;
            var towers = new Towers(numberOfDisks);
            towers.PrintResult();
            towers.MoveDisks(numberOfDisks);
        }
    }
}
