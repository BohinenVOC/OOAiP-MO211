namespace SpaceBattle.Lib;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

public class RegisterHandlerTests
{

    public RegisterHandlerTests()
    {

        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        Mock<SpaceBattle.Lib.ICommand> mockCommand = new Mock<ICommand>();
        mockCommand.Setup(x => x.Execute());

        Mock<IStrategy> mockGetTreeStrategt = new Mock<IStrategy>();
        mockGetTreeStrategt.Setup(m => m.run_strategy(It.IsAny<object[]>())).Returns(mockCommand.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Handler.GetTree", (Func<object[], object>)(args => {
            Mock<IDictionary<int, ICommand>> solution_tree = new Mock<IDictionary<int, ICommand>>();
            solution_tree.Setup(t => t.Add(It.IsAny<int>(), It.IsAny<ICommand>())).Callback(() => {});
            return solution_tree.Object;
        })).Execute();
    }

    [Fact]
    public void RegisterExeptionHandlerCommandTest()
    {
        var mock_cmd = new Mock<ICommand>();
        var cmd = typeof(MoveCommand);
        var ex = typeof(ArgumentException);
        ICommand RegisteredExeptionHandler = new RegisterExeptionHandlerCommand(cmd, ex, mock_cmd.Object);
        RegisteredExeptionHandler.Execute();
    }
}