namespace SpaceBattle.Lib;

public interface IUObject
{
    IDictionary<string, object> properties
    {
        set;
        get;
    }
    void set_property(string key, object value);
    object get_property(string key);
}

