namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public enum CalibrationStatus
{
    Fit("Fit"),
    Due("Due"),
    Not-Fit("Not Fit");

    private final String value;

    CalibrationStatus(String value) {
        this.value = value;
    }

    public String value() {
        return value;
    }

    public static CalibrationStatus from(String value) {
        if (value == null || value.isBlank()) { return FIT; }
                for (CalibrationStatus status : values()) {
                    if (status.value.equalsIgnoreCase(value.trim()) ||
                            status.name().equalsIgnoreCase(value.trim()))
                    {   return status;  }
                } return FIT;
    }
}