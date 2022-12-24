namespace SpaceBattle.Lib;

using Hwdtech;
public class StopMoveCommand : ICommand
{
    private IMoveCommandStopable stop_obj;
    public StopMoveCommand(IMoveCommandStopable obj)
    {
        stop_obj = obj;
    }

    public void Execute()
    {
        var obj = stop_obj.uobj;
        var cmd = stop_obj.move_command;

        IoC.Resolve<IUObject>("Game.Uobject.RemoveProperty", obj, "velocity");
        var empt_cmd = IoC.Resolve<ICommand>("Game.Commands.EmptyCommand");
        IoC.Resolve<ICommand>("Game.Commands.Inject", cmd, empt_cmd).Execute();
    }
}

