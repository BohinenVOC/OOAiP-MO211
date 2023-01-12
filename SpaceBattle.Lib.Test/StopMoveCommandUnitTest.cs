namespace SpaceBattle.Lib.Test;

using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class StopMovingCommandsUnitTests
{
    static StopMovingCommandsUnitTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        
        Mock<SpaceBattle.Lib.ICommand> mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(m => m.Execute());

        Mock<IStrategy> mockStrategyDelete = new Mock<IStrategy>();
        mockStrategyDelete.Setup(m => m.run_strategy(It.IsAny<object[]>())).Returns(mockCommand.Object);
        IoC.Resolve<ICommand>("IoC.Register", "Game.Object.DeleteProperty", (object[] args) => mockStrategyDelete.Object.run_strategy(args)).Execute();

        Mock<IStrategy> mockStrategyInject = new Mock<IStrategy>();
        mockStrategyInject.Setup(m => m.run_strategy()).Returns(mockCommand.Object);
        IoC.Resolve<ICommand>("IoC.Register", "Game.Commands.InjectEmptyCommand", (object[] args) => mockStrategyInject.Object.run_strategy(args)).Execute();
    }

    [Fact]
    public void StopMoveCommandGood()
    {
        Mock<IMoveCommandStopable> stop_obj = new Mock<IMoveCommandStopable>();
        Mock<IUObject> mockUobj = new Mock<IUObject>();
        stop_obj.SetupGet(m => m.uobj).Returns(mockUobj.Object).Verifiable();
        stop_obj.SetupGet(m => m.properties).Returns(new List<string>() {"Velocity"}).Verifiable();
        SpaceBattle.Lib.ICommand stop_cmd = new StopMoveCommand(stop_obj.Object);
        stop_cmd.Execute();
    }

    [Fact]
    public void TestNegativeGetUobject()
    {
        Mock<IMoveCommandStopable> stop_obj = new Mock<IMoveCommandStopable>();
        stop_obj.SetupGet(m => m.uobj).Throws<Exception>().Verifiable();
        stop_obj.SetupGet(m => m.properties).Returns(new List<string>() {"Velocity"}).Verifiable();
        SpaceBattle.Lib.ICommand stop_cmd = new StopMoveCommand(stop_obj.Object);
        Assert.Throws<Exception>(() => stop_cmd.Execute());
    }

    [Fact]
    public void TestNegativeGetProperties()
    {
        Mock<IMoveCommandStopable> stop_obj = new Mock<IMoveCommandStopable>();
        Mock<IUObject> mockUobj = new Mock<IUObject>();
        stop_obj.SetupGet(m => m.uobj).Returns(mockUobj.Object).Verifiable();
        stop_obj.SetupGet(m => m.properties).Throws<Exception>().Verifiable();
        SpaceBattle.Lib.ICommand stop_cmd = new StopMoveCommand(stop_obj.Object);
        Assert.Throws<Exception>(() => stop_cmd.Execute());
    }
}

