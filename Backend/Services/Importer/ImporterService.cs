using Backend.Repository.Importer;

namespace Backend.Services.Importer;

public class ImporterService : IImporterService
{
    private readonly IImporterRepository _repository;

    public ImporterService(IImporterRepository repository)
    {
        _repository = repository;
    }

    // public async Task<bool> CheckFileAsync(IFormFile file)
    // {
    //     if (file != null) return true;
    //     return false;
    // }
}