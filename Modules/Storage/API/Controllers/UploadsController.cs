using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Storage.API.Controllers;

[ApiController]
[Route("api/uploads")]
public class UploadsController : ControllerBase
{
    private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

    public UploadsController()
    {
        if (!Directory.Exists(_uploadPath)) Directory.CreateDirectory(_uploadPath);
    }

    [HttpPost("receipt")]
    public async Task<ActionResult<Result<string>>> UploadReceipt(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest(Result<string>.FailureResult("No file uploaded"));

        var fileId = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(_uploadPath, fileId);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(Result<string>.SuccessResult(fileId));
    }

    [HttpGet("{fileId}")]
    public async Task<IActionResult> GetFile(string fileId)
    {
        var filePath = Path.Combine(_uploadPath, fileId);
        if (!System.IO.File.Exists(filePath)) return NotFound();

        var memory = new MemoryStream();
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;
        return File(memory, "application/octet-stream", fileId);
    }
}
