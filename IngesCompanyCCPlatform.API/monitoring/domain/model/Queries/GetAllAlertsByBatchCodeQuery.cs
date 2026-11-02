namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public record GetAllAlertsByBatchCodeQuery(BatchCode batchCode) {
    public GetAllAlertsByBatchCodeQuery(BatchCode batchCode) {
        if (batchCode == null) {
            throw new ArgumentNullException(nameof(batchCode));
        }
    }
}