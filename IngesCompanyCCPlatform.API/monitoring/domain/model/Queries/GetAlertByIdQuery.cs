namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public record GetAlertByIdQuery(Long id) {
    public GetAlertByIdQuery(Long id) {
        if (id == null) {
            throw new ArgumentNullException(nameof(id));
        }
    }
}