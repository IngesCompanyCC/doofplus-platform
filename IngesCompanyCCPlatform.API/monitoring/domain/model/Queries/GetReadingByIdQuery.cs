namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public record GetReadingByIdQuery(ReadingId readingId) {
    public GetReadingByIdQuery(ReadingId readingId) {
        if (readingId == null) {
            throw new ArgumentNullException(nameof(readingId));
        }
    }
}