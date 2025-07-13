using Web_Domain.Rules;

namespace Web_Infraestructure.Data.Rule;

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
