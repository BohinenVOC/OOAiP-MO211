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
        var properties = stop_obj.properties;

        properties.ToList().ForEach(property => IoC.Resolve<ICommand>("Game.Object.DeleteProperty", obj, property).Execute());
        IoC.Resolve<ICommand>("Game.Commands.InjectEmptyCommand", cmd);
    }
}


