namespace SpaceBattle.Lib;
using Hwdtech;

public class RegisterExeptionHandlerCommand : ICommand
{
    public Type cmd, ex;
    public ICommand handler;
    public RegisterExeptionHandlerCommand(Type handle_cmd, Type handle_ex, ICommand _handler)
    {
        cmd = handle_cmd;
        ex = handle_ex;
        handler = _handler;
    }
    public void Execute()
    {
        var solution_tree = IoC.Resolve<IDictionary<int, ICommand>>("Game.Handler.GetTree");
        solution_tree.Add(cmd.GetHashCode(), handler);
    }
}