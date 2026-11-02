namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public record GetEquipmentByCodeQuery(EquipmentCode equipmentCode) {
    public GetEquipmentByCodeQuery(EquipmentCode equipmentCode) {
        if (equipmentCode == null) {
            throw new ArgumentNullException(nameof(equipmentCode));
        }
    }
}