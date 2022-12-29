namespace SpaceBattle.Lib.Test;

using Hwdtech;
using Hwdtech.Ioc;
using Moq;

public class ExceptionFindUnitTest
{
    public ExceptionFindUnitTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var ExceptionStrategy = new Mock<IStrategy>();
        ExceptionStrategy.Setup(obj => obj.run_strategy()).Returns(new Mock<SpaceBattle.Lib.ICommand>().Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.DecisionTree.Exceptions", (object[] args) => ExceptionStrategy.Object.run_strategy(args)).Execute();
    }
    [Fact]
    public void commandGivingExceptions()
    {
        var commandWithException = new Mock<SpaceBattle.Lib.ICommand>();
        commandWithException.Setup(com => com.Execute()).Throws(new Exception());
        var exceptionOfCommand = new Mock<Exception>();
        var exceptionSearcher = new ExceptionSearch(commandWithException.Object, exceptionOfCommand.Object);

        exceptionSearcher.Execute();

    }
}

