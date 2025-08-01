﻿using PetFamily.Domain.Shared;

namespace PetFamily.API.Response;

public record Envelope
{
    private Envelope(object? result, Error? error)
    {
        Result = result;
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
        TimeGenerated = DateTime.Now;
    }

    public object? Result { get; }

    public string? ErrorCode { get; }

    public string? ErrorMessage { get; }

    public DateTime TimeGenerated { get; }

    private static Envelope Ok(object? result = null) =>
        new Envelope(result, null);
    
    public static Envelope Error(Error error) =>
        new Envelope(null, error);
}