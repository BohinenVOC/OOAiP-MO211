namespace SpaceBattle.Lib;
using Hwdtech;
public class LongCommand : IStrategy
{
    public object run_strategy(params object[] args)
    {
        string cmd = (string)args[0];
        IUObject obj = (IUObject)args[1];

        var macro_cmd = IoC.Resolve<ICommand>("Game.Commands.CreateMacro", cmd, obj);

        ICommand repeat_cmd = IoC.Resolve<ICommand>("Game.Commands.Repeat", macro_cmd);
        ICommand inject_command = IoC.Resolve<ICommand>("Game.Commands.Inject", repeat_cmd);
        return inject_command;
    }
}