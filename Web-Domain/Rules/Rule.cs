namespace Web_Domain.Rules;

public class Rule : IValueRule
{
    private double _value;
    public double GetValue()
    {
        return _value;
    }

    public void SetValue(double value)
    {
        _value = value;
    }
}
