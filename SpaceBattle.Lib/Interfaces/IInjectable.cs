namespace SpaceBattle.Lib;

public interface IInjectable
{
    public void Inject(ICommand other_command);
}
