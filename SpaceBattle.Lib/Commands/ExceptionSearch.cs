namespace SpaceBattle.Lib;

using Hwdtech;

public class ExceptionSearch
{
    private SpaceBattle.Lib.ICommand target;

    public ExceptionSearch(SpaceBattle.Lib.ICommand obj)
    {
        this.target = obj;
    }

    public void Execute()
    {
        try
        {
            target.Execute();
        }
        catch (Exception e)
        {
            Exception findedE = IoC.Resolve<Exception>("Game.DecisionTree.Exceptions", target, e);
            IoC.Resolve<SpaceBattle.Lib.ICommand>("Game.Handler.ExceptionHandler", findedE).Execute();
        }
    }
}

