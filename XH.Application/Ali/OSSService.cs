using Aliyun.OSS;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace XH.Application.Ali;
public class OSSService
{
    private readonly string _accessKeyId;
    private readonly string _accessKeySecret;
    private readonly string _endpoint;
    private readonly string[] _validFileType = { ".png", ".jpge", ".pdf", ".jpg" };

    private readonly ILogger _logger;

    public OssClient Client { get; }

    public OSSService(IConfiguration configuration, ILogger<OSSService> logger)
    {
        _logger = logger;
        var ossConf = configuration.GetSection("OSSConfig");
        _accessKeyId = ossConf.GetValue("AccessKeyId", "")!;
        _accessKeySecret = ossConf.GetValue("AccessKeySecret", "")!;
        _endpoint = ossConf.GetValue("EndPoint", "")!;

        Client = new OssClient(_endpoint, _accessKeyId, _accessKeySecret);
    }

    private bool ValidFileType(string fileName)
    {
        var type = Path.GetExtension(fileName);
        return _validFileType.Contains(type);
    }

    public List<string> PutData(List<IFormFile> files)
    {
        var fileNames = new List<string>(files.Count);
        files.ForEach(file =>
        {
            var type = Path.GetExtension(file.FileName);

            if (!ValidFileType(file.FileName))
            {
                _logger.LogWarning("{file} type:{type} is invalid!", file.FileName, type);
                return;
            }

            //if (!Client.DoesBucketExist(type.Replace(".", "")))
            //    Client.CreateBucket(type.Replace(".", ""));
            var fn = DateTimeOffset.Now.ToUnixTimeMilliseconds() + type;
            fileNames.Add(fn);

            Client.PutObject("wsy-xh-bucket", fn, file.OpenReadStream());

        });

        return fileNames;
    }
}
