using TechFood.Common.DTO.ValueObjects;

namespace TechFood.Common.DTO;

public class CustomerDTO : EntityDTO
{
    public NameDTO Name { get; set; } = null!;

    public EmailDTO Email { get; set; } = null!;

    public DocumentDTO Document { get; set; } = null!;

    public PhoneDTO? Phone { get; set; } = null!;
}
