namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public record GetAllSensorByEquipmentCodeQuery(EquipmentCode equipmentCode) {
    public GetAllSensorByEquipmentCodeQuery(EquipmentCode equipmentCode) {
        if (equipmentCode == null) {
            throw new ArgumentNullException(nameof(equipmentCode));
        }
    }
}