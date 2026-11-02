namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public record GetAllReadingsByBatchCodeQuery(BatchCode batchCode) {
    public GetAllReadingsByBatchCodeQuery(BatchCode batchCode) {
        if (batchCode == null) {
            throw new ArgumentNullException(nameof(batchCode));
        }
    }
}