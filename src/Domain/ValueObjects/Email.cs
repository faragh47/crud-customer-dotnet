using System;
using System.Text.RegularExpressions;
namespace CleanArchitecture.Domain.ValueObjects;
public class Email : ValueObject
{
    public string Value { get; }
    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );
    public Email()
    {
        Value = "";
    }
    public Email(string email)
    {
        if (!IsValid(email))
            throw new ArgumentException("Invalid email format.");
        Value = email;
    }
    public static bool IsValid(string email)
    {
        return EmailRegex.IsMatch(email);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.Equals(Value, StringComparison.OrdinalIgnoreCase);
    }
}
