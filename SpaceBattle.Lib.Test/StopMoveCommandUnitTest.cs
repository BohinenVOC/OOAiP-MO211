namespace SpaceBattle.Lib.Test;

using Moq;
using Hwdtech;
using Hwdtech.Ioc;

class RemovePropertyStratagy : IStrategy
{
    public object run_strategy(params object[] args)
    {
        IUObject obj = (IUObject)args[0];
        string property_to_remove = (string)args[1];
        var result = new Mock<SpaceBattle.Lib.IUObject>();

        result.SetupProperty(result => result.properties, new Dictionary<string, object>());

        foreach(var property in obj.properties)
        {
            if (property.Key != property_to_remove)
            {
                result.Setup(result => result.set_property(property.Key, property.Value));
            }
        }
        return (result.Object);
    }
}

class EmptyCommandStrategy : IStrategy
{
    public object run_strategy(params object[] args)
    {
        var result = new Mock<SpaceBattle.Lib.ICommand>();
        return (result.Object);
    }
}

class InjectCommandStrategy : IStrategy
{
    public object run_strategy(params object[] args)
    {
        SpaceBattle.Lib.ICommand cmd_to_inject = (SpaceBattle.Lib.ICommand)args[0];
        SpaceBattle.Lib.ICommand inject_cmd = (SpaceBattle.Lib.ICommand)args[1];
        
        cmd_to_inject = inject_cmd;
        return (cmd_to_inject);
    }
}
public class StopMovingCommandsUnitTests
{
    static StopMovingCommandsUnitTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var RemovePropertyStratagy = new RemovePropertyStratagy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Uobject.RemoveProperty", (object[] args) => RemovePropertyStratagy.run_strategy(args)).Execute();

        var EmptyCommandStrategy = new EmptyCommandStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.EmptyCommand", (object[] args) => EmptyCommandStrategy.run_strategy(args)).Execute();

        var InjectCommandStrategy = new InjectCommandStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.Inject", (object[] args) => InjectCommandStrategy.run_strategy(args)).Execute();    
    }

    [Fact]
    public void StartMoveCommandGood()
    {
        var mcs_obj = new Mock<IMoveCommandStopable>();
        var move_command = new Mock<SpaceBattle.Lib.ICommand>();
        var uobj = new Mock<IUObject>();
        var dict = new Dictionary<string, object>(){["velocity"] = new Vector(1)};
        
        mcs_obj.SetupGet(com => com.uobj).Returns(uobj.Object);
        mcs_obj.SetupProperty(com => com.uobj.properties, dict);
        mcs_obj.SetupGet(com => com.move_command).Returns(move_command.Object);
        var smc = new StopMoveCommand(mcs_obj.Object);
        
        smc.Execute();
        
        mcs_obj.VerifyAll();
    }
}
