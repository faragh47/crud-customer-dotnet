using System;
using System.Text.RegularExpressions;
namespace CleanArchitecture.Domain.ValueObjects;
public class BankAccountNumber
{
    public string Value { get; }

    // Regular expression to validate exactly 11 numeric digits
    private static readonly Regex BankAccountNumberRegex = new Regex(@"^\d{11}$");
    private BankAccountNumber() { Value = ""; }
    public BankAccountNumber(string accountNumber)
    {
        if (!IsValid(accountNumber))
            throw new ArgumentException("Bank account number must be exactly 11 digits.");

        Value = accountNumber;
    }

    public static bool IsValid(string accountNumber)
    {
        // Validate the format (exactly 11 digits)
        return BankAccountNumberRegex.IsMatch(accountNumber);
    }

}
