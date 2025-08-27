using TechFood.Common.DTO.Enums;

namespace TechFood.Common.DTO.ValueObjects;

public class DocumentDTO
{
    public DocumentTypeDTO Type { get; set; }

    public string Value { get; set; } = string.Empty;

}
