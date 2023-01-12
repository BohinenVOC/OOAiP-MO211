namespace SpaceBattle.Lib;

using Hwdtech;

public class ExceptionTreeStrategy : IStrategy
{
    public object run_strategy(params object[] args)
    {
        var com = (ICommand)args[0];
        var e = (Exception)args[1];
        var tree = IoC.Resolve<IDictionary<Type, IDictionary<Type, ICommand>>>("Game.Trees.Exception");
        var firstCheck = tree.TryGetValue(com.GetType(), out var firstValue)
            ? firstValue
            : IoC.Resolve<IDictionary<Type, ICommand>>("Game.Trees.ExceptionDefault"); ;
        var secondCheck = firstCheck.TryGetValue(e.GetType(), out var secondValue)
            ? secondValue
            : IoC.Resolve<ICommand>("Game.Exception.DefaultHandler", e.GetType());
        Console.Write(e.GetType());
        Console.Write(IoC.Resolve<ICommand>("Game.Exception.DefaultHandler", e));
        return secondCheck;
    }
}
