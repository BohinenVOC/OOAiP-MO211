namespace SpaceBattle;

public interface IMovemable
{
    protected static int[] position;
    protected static int[] velocity;
    protected static bool movable = true;
}


class SpaceBattle
{
    static int Main(string[] args)
    {
        Console.WriteLine(100);
        Console.ReadKey();
        return 0;
    }
}
