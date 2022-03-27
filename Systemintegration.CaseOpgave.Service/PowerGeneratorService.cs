using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Systemintegration.CaseOpgave.Service.Contracts;
using Systemintegration.CaseOpgave.Shared.DataTransferObjects;

namespace Systemintegration.CaseOpgave.Service
{
    public sealed class PowerGeneratorService : IPowerGeneratorService
    {
        private const string uri = "ftp://inverter.westeurope.cloudapp.azure.com";

        private readonly IMemoryCache memoryCache;
        private readonly IOptions<SecretSettings> options;

        public PowerGeneratorService(IMemoryCache memoryCache, IOptions<SecretSettings> options)
        {
            this.memoryCache = memoryCache;
            this.options = options;
        }
         
        public IEnumerable<PowerOutputParam> GetPowerGeneratorParams()
        {
            string user = this.options.Value.PowerGeneratorUser;
            string pass = this.options.Value.PowerGeneratorPassword;

            List<PowerOutputParam> returnResult;

            returnResult = this.memoryCache.Get<List<PowerOutputParam>>("powerOutputCacheKey");

            if (returnResult is null)
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                request.Credentials = new NetworkCredential(user, pass);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                var stream = response.GetResponseStream();
                var reader = new StreamReader(stream);
                var allFiles = reader.ReadToEnd().Replace("\r", "").Split('\n');

                returnResult = new List<PowerOutputParam>();

                foreach (var file in allFiles)
                {
                    if (!string.IsNullOrWhiteSpace(file))
                    {
                        var extrDateTime = file.Split("-").Last();
                        var extrDate = extrDateTime.Substring(0, 6);

                        if (extrDate == DateTime.Today.AddYears(-1).ToString("yyMMdd"))
                        {
                            WebClient client = new WebClient() { Credentials = new NetworkCredential(user, pass) };

                            var getData = client.DownloadString($"{uri}/{file}");
                            var splitData = getData.Split("\n").Skip(6).SkipLast(2);

                            float firstRowPowerRead = float.Parse(splitData.First().Split(';').ElementAt(37));
                            float lastRowPowerRead = float.Parse(splitData.Last().Split(';').ElementAt(37));

                            returnResult.Add(new PowerOutputParam
                            {
                                DateTime = Convert.ToDateTime($"{extrDateTime.Substring(4, 2)}-{extrDateTime.Substring(2, 2)}-20{extrDateTime.Substring(0, 2)} {extrDateTime.Substring(6, 2)}:{extrDateTime.Substring(8, 2)}:{extrDateTime.Substring(10, 2)}"),

                                PowerOutput = lastRowPowerRead - firstRowPowerRead
                            });
                        }
                    }
                }
                reader.Close();
                response.Close();

                this.memoryCache.Set("powerOutputCacheKey", returnResult, TimeSpan.FromMinutes(1));
            }
            return returnResult.ToArray();
        }
    }
}
