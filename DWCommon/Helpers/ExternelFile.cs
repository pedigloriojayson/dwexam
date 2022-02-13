using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWCommon.Helpers
{
    public class ExternelFile
    {
        public StreamReader GetCSVFile()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
            var res = configuration.GetSection("AppSettings:CSVFile");

            return new StreamReader(string.Format(AppDomain.CurrentDomain.BaseDirectory + "/{0}", res.Value));
        }
    }
}
