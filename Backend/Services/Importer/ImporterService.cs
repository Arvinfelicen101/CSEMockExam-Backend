using System.Text;
using Backend.Repository.Importer;
using Backend.DTOs.Importer;
using Backend.Services.ParagraphManagement;
using Backend.Services.YearPeriodManagement;
using ClosedXML.Excel;

namespace Backend.Services.Importer;

public class ImporterService : IImporterService
{
    private readonly IImporterRepository _repository;
    private readonly IYearPeriodService _yearPeriodService;
    private readonly IParagraphManagementService _paragraphService;

    public ImporterService(IImporterRepository repository, IYearPeriodService yearPeriodService)
    {
        _repository = repository;
        _yearPeriodService = yearPeriodService;
    }

    public async Task<IEnumerable<YearPeriodDTO>> getCategoryFK()
    {
        return await _yearPeriodService.GetAllService();
    }
    
    public async Task<IEnumerable<ParagraphDTO>> getParagraphFK()
    {
        return await _paragraphService.GetAllService();
    }

    public async Task<FKDataDTOs> ExistingCache()
    {
        var YearPeriodData = await getCategoryFK();
        var ParagraphData = await getParagraphFK();
        var fkData = new FKDataDTOs()
        {
            YearPeriodFK = YearPeriodData,
            ParagraphFK = ParagraphData
        };
        return fkData;
    }
    
    //should be map and insert fk data
    public async Task ProcessFileAsync(ImporterDTO xlsx)
    {
        var fkData = await ExistingCache();
        var result = await ServiceHelper.ParseFileAsync(xlsx);
        var mappeddata = await ServiceHelper.ImportFKMapper(result, fkData);
        
        //insert data to repository
        
        //instead of looking at cache use mapped data, THEN INSERT QUESTIONS
        
    }
    
    
    //map and insert question and choices data
    
    
}