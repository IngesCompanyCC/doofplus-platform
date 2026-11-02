namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public record GetEquipmentByIdQuery(EquipmentId equipmentId) {
    public GetEquipmentByIdQuery(EquipmentId equipmentId) {
        if (equipmentId == null) {
            throw new ArgumentNullException(nameof(equipmentId));
        }
    }
}