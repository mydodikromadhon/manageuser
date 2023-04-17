using CRUD.ManagementUser.Domain.Interfaces;

namespace CRUD.ManagementUser.Domain.Abstracts;

public abstract class FileEntity : AuditableEntity, IHasFile
{
    public string FileName { get; set; } = default!;
    public string FileContentType { get; set; } = default!;
    public long FileSize { get; set; }
    public string StorageFileId { get; set; } = default!;
}
