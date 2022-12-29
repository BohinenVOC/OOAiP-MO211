namespace SpaceBattle.Lib;

using Hwdtech;

public class ExceptionSearch
{
    private SpaceBattle.Lib.ICommand target;
    private Exception except;

    public ExceptionSearch(SpaceBattle.Lib.ICommand obj, Exception e)
    {
        this.target = obj;
        this.except = e;
    }

    public void Execute()
    {
        IoC.Resolve<SpaceBattle.Lib.ICommand>("Game.DecisionTree.Exceptions").Execute();
    }
}

