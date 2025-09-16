namespace Infrastructure.Providers;

using System;
using Application.Interfaces.Providers;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}