namespace SpaceBattle.Lib;

public interface IMoveCommandStopable
{
    IEnumerable<object> properties
    {
        set;
        get;
    }
    ICommand move_command
    {
        get;
    }

    IUObject uobj
    {
        get;
    }
}

