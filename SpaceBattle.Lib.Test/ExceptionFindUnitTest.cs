namespace SpaceBattle.Lib.Test;

using Hwdtech;
using Hwdtech.Ioc;
using Moq;

public class GetExceptionStrategy : IStrategy
{
    public object run_strategy(params object[] args)
    {
        return (new Exception());
    }
}

public class ExceptionHandleStrategy : IStrategy
{
    public object run_strategy(params object[] args)
    {
        var com = new Mock<SpaceBattle.Lib.ICommand>();
        com.Setup(obj => obj.Execute());
        return (com.Object);
    }
}

public class ExceptionFindUnitTest
{
    public ExceptionFindUnitTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var ExceptionStrategy = new GetExceptionStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.DecisionTree.Exceptions", (object[] args) => ExceptionStrategy.run_strategy(args)).Execute();

        var ExceptionHandlerStrategy = new ExceptionHandleStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Handler.ExceptionHandler", (object[] args) => ExceptionHandlerStrategy.run_strategy(args)).Execute();
    }
    [Fact]
    public void commandGivingExceptions()
    {
        var commandWithException = new Mock<SpaceBattle.Lib.ICommand>();
        commandWithException.Setup(com => com.Execute()).Throws(new Exception());
        var exceptionSearcher = new ExceptionSearch(commandWithException.Object);

        exceptionSearcher.Execute();

    }
    [Fact]
    public void commandNotGivingExceptions()
    {
        var commandWithException = new Mock<SpaceBattle.Lib.ICommand>();
        commandWithException.Setup(com => com.Execute());
        var exceptionSearcher = new ExceptionSearch(commandWithException.Object);

        exceptionSearcher.Execute();

    }
}

