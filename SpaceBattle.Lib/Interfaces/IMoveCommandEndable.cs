namespace SpaceBattle.Lib;

public interface IMoveCommandStopable
{
    ICommand move_command
    {
        get;
    }

    IUObject uobj
    {
        get;
    }
}

