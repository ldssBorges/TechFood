namespace TechFood.Common.DTO.Hook;

public class HookRequestDTO
{
    public string Action { get; set; }
    public HookRequestDataDTO Data { get; set; }
    public string DateCreated { get; set; }
    public string Type { get; set; }
    public long UserId { get; set; }
}

public class HookRequestDataDTO
{
    public string Id { get; set; }
}
