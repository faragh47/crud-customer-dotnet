using PhoneNumbers;
namespace CleanArchitecture.Domain.ValueObjects;
public class PhoneNumber:ValueObject
{
    public ulong Value { get; }
    private static readonly PhoneNumberUtil PhoneUtil = PhoneNumberUtil.GetInstance();
    public PhoneNumber(ulong value, string region = "IR") // Default to "IR" for Iran
    {
        if (!IsValidMobileNumber(value.ToString(), region))
            throw new ArgumentException("Phone number is not a valid mobile number in Iran.");
        Value = value;
    }

    private static bool IsValidMobileNumber(string phoneNumber, string region)
    {
        try
        {
            var parsedNumber = PhoneUtil.Parse(phoneNumber, region);
            return PhoneUtil.IsValidNumber(parsedNumber) &&
                   PhoneUtil.GetNumberType(parsedNumber) == PhoneNumberType.MOBILE;
        }
        catch (NumberParseException)
        {
            return false;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
