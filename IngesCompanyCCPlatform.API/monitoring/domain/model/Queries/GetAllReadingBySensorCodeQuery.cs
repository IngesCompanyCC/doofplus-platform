namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public record GetAllReadingBySensorCodeQuery(SensorCode sensorCode) {
    public GetAllReadingBySensorCodeQuery(SensorCode sensorCode) {
        if (sensorCode == null) {
            throw new ArgumentNullException(nameof(sensorCode));
        }
    }
}