namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public record GetAllAlertsBySensorCodeQuery(SensorCode sensorCode) {
    public GetAllAlertsBySensorCodeQuery(SensorCode sensorCode) {
        if (sensorCode == null) {
            throw new ArgumentNullException(nameof(sensorCode));
        }
    }
}