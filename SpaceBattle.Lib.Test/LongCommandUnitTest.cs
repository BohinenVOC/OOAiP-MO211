namespace SpaceBattle.Lib;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;

public class LongCommandTests
{
    public LongCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        Mock<SpaceBattle.Lib.ICommand> mockCommand = new Mock<ICommand>();
        mockCommand.Setup(x => x.Execute());

        Mock<IStrategy> mockStrategyCreateMacroCmd = new Mock<IStrategy>();
        mockStrategyCreateMacroCmd.Setup(m => m.run_strategy(It.IsAny<object[]>())).Returns(mockCommand.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.CreateMacro", (object[] args) => mockStrategyCreateMacroCmd.Object.run_strategy(args)).Execute();
        
        Mock<IStrategy> mockStrategyInject = new Mock<IStrategy>();
        mockStrategyInject.Setup(m => m.run_strategy(It.IsAny<object[]>())).Returns(mockCommand.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.Inject", (object[] args) => mockStrategyInject.Object.run_strategy(args)).Execute();
        
        Mock<IStrategy> mockStrategyReparetCmd = new Mock<IStrategy>();
        mockStrategyReparetCmd.Setup(m => m.run_strategy(It.IsAny<object[]>())).Returns(mockCommand.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.Repeat", (object[] args) => mockStrategyReparetCmd.Object.run_strategy(args)).Execute();
    }

    [Fact]
    public void LongCommandTest()
    {
        IStrategy LongTermOperation = new LongCommand();
        string name = "Moving";
        var obj = new Mock<IUObject>();
        Assert.NotNull(LongTermOperation.run_strategy(name, obj.Object));
    }
}
