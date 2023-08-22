namespace DockerComposeRiderIssue.Infrastructure.Entities.Models;

public class BusinessCentralInstance : BaseEntity
{
    public int Id { get; set; }

    public string? NavDomain { get; set; }

    public string? NavAdminUserName { get; set; }

    public string? NavAdminPassword { get; set; }

    public string? TenantId { get; set; }

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public string AppUrl { get; set; } = null!;

    public string? PayrollCommitId { get; set; }
}