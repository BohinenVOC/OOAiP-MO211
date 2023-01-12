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

        var ExceptionTreeStrategy = new ExceptionTreeStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.DecisionTree.Exceptions", (object[] args) => ExceptionTreeStrategy.run_strategy(args)).Execute();

        var handler = new Mock<SpaceBattle.Lib.ICommand>();
        var newTree = new Dictionary<Type, IDictionary<Type, SpaceBattle.Lib.ICommand>>
        {
            {typeof(MoveCommand), new Dictionary<Type, SpaceBattle.Lib.ICommand>
            {
                {typeof(ArgumentException), handler.Object}
            }},
        };
        var GetTreeStrategy = new Mock<IStrategy>();
        GetTreeStrategy.Setup(o => o.run_strategy()).Returns(newTree);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Trees.Exception", (object[] args) => GetTreeStrategy.Object.run_strategy(args)).Execute();

        var defTree = new Dictionary<Type, SpaceBattle.Lib.ICommand>
        {
            {typeof(ArgumentException), handler.Object}
        };
        var GetDefaultTreeStrategy = new Mock<IStrategy>();
        GetDefaultTreeStrategy.Setup(o => o.run_strategy()).Returns(defTree);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Trees.ExceptionDefault", (object[] args) => GetDefaultTreeStrategy.Object.run_strategy(args)).Execute();

        var GetDefaultHandlerStrategy = new Mock<IStrategy>();
        GetDefaultHandlerStrategy.Setup(o => o.run_strategy()).Returns(handler.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Exception.DefaultHandler", (object[] args) => GetDefaultHandlerStrategy.Object.run_strategy()).Execute();

    }
    [Fact]
    public void ACommandAnExceptionTest()
    {
        var mo = new Mock<IMovable>();
        var mc = new MoveCommand(mo.Object);
        var e = new ArgumentException();
        var c = new ExceptionSearch(mc, e);

        c.Execute();
    }
    [Fact]
    public void ACommandDefaultExceptionTest()
    {
        var mo = new Mock<IMovable>();
        var mc = new MoveCommand(mo.Object);
        var e = new IndexOutOfRangeException();
        var c = new ExceptionSearch(mc, e);

        c.Execute();
    }
    [Fact]
    public void DefaultCommandAnExceptionTest()
    {
        var mc = new Mock<SpaceBattle.Lib.ICommand>();
        var e = new ArgumentException();
        var c = new ExceptionSearch(mc.Object, e);

        c.Execute();
    }
}

