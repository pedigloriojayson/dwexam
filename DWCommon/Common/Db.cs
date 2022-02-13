using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DWCommon.Common
{
    public class Db
    {

        public NpgsqlConnection GetSqlConnection()
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            return new NpgsqlConnection()
            {
                ConnectionString = configuration.GetConnectionString("DatabaseConnection")
            };
        }
    }
}
