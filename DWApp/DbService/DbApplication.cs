using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWCommon.Helpers;
namespace DWApp.DbService
{
    public class DbApplication
    {
        public List<DWCommon.Responses.ConfirmedData> GetReport(string observation_date, int max_result)
        {
            List<DWCommon.Responses.ConfirmedData> _ret = new List<DWCommon.Responses.ConfirmedData>();

            try
            {
                using (var connection = new DWCommon.Common.Db()
                    .GetSqlConnection())
                {
                    connection.Open();

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = String.Format(
                            "SELECT country, sum(confirmed) as total_confirmed, sum(deaths) as total_deaths, sum(recovered) as total_recovered " +
                            "FROM covid_observations where observation_date = '{0}' " +
                            "group by country order by total_confirmed desc limit {1}; ", observation_date, max_result);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _ret.Add(new DWCommon.Responses.ConfirmedData()
                                {
                                    country = reader["country"].ToString(),
                                    recovered = reader["total_recovered"].ConvertObjectToInt().Value,
                                    confirmed = reader["total_confirmed"].ConvertObjectToInt().Value,
                                    deaths = reader["total_deaths"].ConvertObjectToInt().Value,
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to retrieved report", 
                    e.InnerException == null ? e : e.InnerException);
            }
            return _ret;
        }
    }
}
