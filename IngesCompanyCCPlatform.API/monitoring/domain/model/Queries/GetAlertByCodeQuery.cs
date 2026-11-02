namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public record GetAlertByCodeQuery(AlertCode code) {
    public GetAlertByCodeQuery(AlertCode code) {
        if (code == null) {
            throw new ArgumentNullException(nameof(code));
        }
    }
}