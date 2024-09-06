using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AuthService.Models;

[Route("api/[controller]")]
[ApiController]
public class CloudinaryController : ControllerBase
{
    private readonly Cloudinary _cloudinary;
    private readonly CloudinarySettings _settings;

    public CloudinaryController(IOptions<CloudinarySettings> settings)
    {
        _settings = settings.Value;

        var account = new Account(
            settings.Value.CloudName,
            settings.Value.ApiKey,
            settings.Value.ApiSecret
        );
        _cloudinary = new Cloudinary(account);
    }

    [HttpGet("signature")]
    public IActionResult GetSignature()
    {
        var parameters = new Dictionary<string, object>
        {
            { "timestamp", ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString() },
            { "upload_preset", "new_preset" }

        };
        string signature = _cloudinary.Api.SignParameters(parameters);

        return Ok(new
        {
            signature = signature,
            timestamp = parameters["timestamp"],
            apiKey = _settings.ApiKey
        });
    }
}
