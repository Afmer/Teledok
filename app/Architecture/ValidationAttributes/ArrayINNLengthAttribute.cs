using System.ComponentModel.DataAnnotations;

namespace app.Architecture.ValidationAttributes;
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class ArrayINNLengthAttribute : ValidationAttribute
{
    private readonly int _length = 10;
    public override bool IsValid(object? value)
    {
        if(value != null)
        {
            IEnumerable<string> values = (value as IEnumerable<string>)!;
            bool isRight = true;
            string wrongStrings = "";
            foreach(var str in values)
            {
                if(str == null || str.Length != _length)
                {
                    isRight = false;
                    wrongStrings += ' ' + str;
                }
                else
                {
                    foreach(var alpha in str)
                    {
                        if(!char.IsDigit(alpha))
                        {
                            isRight = false;
                            wrongStrings += ' ' + str;
                            break;
                        }
                    }
                }
            } 
            if(!isRight)
            {
                ErrorMessage = $"Следующие строки не ИНН: {wrongStrings}";
                return false;
            }
        }
        return true;
    }
}