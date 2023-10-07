using System.ComponentModel.DataAnnotations;

namespace app.Architecture.ValidationAttributes;
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class ArrayStringLengthAttribute : ValidationAttribute
{
    private readonly int _length;

    public ArrayStringLengthAttribute(int length)
    {
        _length = length;
    }
    public override bool IsValid(object? value)
    {
        if(value != null)
        {
            IEnumerable<string> values = (value as IEnumerable<string>)!;

            foreach(var str in values)
            {
                if(str == null)
                    return false;
                if(str.Length != _length)
                    return false;
            } 
            
            return true;
        }
        else
            return false;
    }
}