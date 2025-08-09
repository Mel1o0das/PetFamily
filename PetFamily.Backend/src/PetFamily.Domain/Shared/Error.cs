namespace PetFamily.Domain.Shared;

public record Error
{
    public const string SEPARATOR = "||"; 
    
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }

    private Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static Error Validation(string code, string message) => 
        new (code, message, ErrorType.VALIDATION);
    
    public static Error NotFound(string code, string message) => 
        new (code, message, ErrorType.NOTFOUND);
    
    public static Error Failure(string code, string message) => 
        new (code, message, ErrorType.FAILURE);
    
    public static Error Conflict(string code, string message) => 
        new (code, message, ErrorType.CONFLICT);

    public string Serialize()
    {
        return string.Join(SEPARATOR, Code, Message, Type);
    }
    
    public static Error Deserialize(string serialized)
    {
        var parts = serialized.Split(SEPARATOR);

        if (parts.Length < 3)
        {
            throw new ArgumentException("Invalid serialized format.");
        }

        if (Enum.TryParse<ErrorType>(parts[2], out var type) == false)
        {
            throw new ArgumentException("Invalid serialized format.");
        }

        return new Error(parts[0], parts[1], type);
    }
}

public enum ErrorType
{
    /// <summary>
    /// Ошибка валидации
    /// </summary>
    VALIDATION,
    
    /// <summary>
    /// Ошибка "не найден ресурс"
    /// </summary>
    NOTFOUND,
    
    /// <summary>
    /// Ошибка сервера
    /// </summary>
    FAILURE,
    
    /// <summary>
    /// Конфликт
    /// </summary>
    CONFLICT,
}