using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;

/// <summary>
///     Value converter that stores a <see cref="DateOnly" /> as a <see cref="DateTime" />.
/// </summary>
/// <remarks>
///     The MySQL provider reads <c>date</c> columns as <see cref="DateTime" /> and cannot cast them to
///     <see cref="DateOnly" />, so every <see cref="DateOnly" /> property goes through this converter.
/// </remarks>
public class DateOnlyConverter() : ValueConverter<DateOnly, DateTime>(
    date => date.ToDateTime(TimeOnly.MinValue),
    dateTime => DateOnly.FromDateTime(dateTime));
