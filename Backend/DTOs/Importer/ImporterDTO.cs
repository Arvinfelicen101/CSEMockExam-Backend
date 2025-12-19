using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Importer;

public class ImporterDTO
{
    [Required]
    public IFormFile file { get; set; }
}