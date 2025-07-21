namespace PetFamily.Domain.Shared;

public class Constants
{
    public static class PhoneNumber
    {
        public const string PATTERN = @"\+7 - \d{3} - \d{3} - \d{2} - \d{2}";
    }

    public static class Email
    {
        public const string PATTERN = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|ru)$";
    }

    public static class Text
    {
        public const int MAX_HIGH_TEXT_LENGTH = 1000;
        public const int MAX_LOW_TEXT_LENGTH = 100;
    }
}