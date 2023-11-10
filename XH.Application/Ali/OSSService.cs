using Aliyun.OSS;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public bool PutData(List<IFormFile> files)
    {
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

            Client.PutObject("wsy-xh-bucket", DateTimeOffset.Now.ToUnixTimeMilliseconds() + type, file.OpenReadStream());

        });

        return true;
    }
}
