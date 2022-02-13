using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DWInfra
{
    public class SqlServer
    {
        public void SetupData()
        {
            var conn = new DWCommon.Common.Db().GetSqlConnection();

            try
            {
                var extracted_data = ExtractDataFromCsvFile();

                conn.Open();
                CreateTable(conn);
                StoreExtractedData(conn, extracted_data);


            }
            catch (PostgresException err)
            {
                throw new Exception("Setup failed", 
                    err.InnerException != null ? err.InnerException : err);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();

                conn.Dispose();
            }
        }

        private void CreateTable(NpgsqlConnection conn)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS covid_observations ( " +
                    "sno INT NOT NULL," +
                    "observation_date DATE NOT NULL," +
                    "country VARCHAR(64) NOT NULL," +
                    "confirmed INT NOT NULL," +
                    "deaths INT NOT NULL," +
                    "recovered INT NOT NULL," +
                    "PRIMARY KEY (sno)" +
                    ");" +
                    "TRUNCATE TABLE covid_observations;";
                cmd.ExecuteNonQuery();
            }
        }
        
        private void StoreExtractedData(NpgsqlConnection conn, DWCommon.Common.CovidInfo[] data)
        {
            var dict_data = data.ToDictionary(y => y.Key, y => y);

            using (var cmd = conn.CreateCommand())
            {
                var tr = conn.BeginTransaction();
                string d = "";
                try
                {
                    while (dict_data.Count > 0)
                    {
                        var to_process = dict_data.Take(1000).ToList();

                        var cmd_stringBuilder = new StringBuilder();
                        cmd_stringBuilder.Append("INSERT INTO covid_observations VALUES ");

                        foreach (var item in to_process)
                        {
                            dict_data.Remove(item.Key);
                            cmd_stringBuilder.Append(String.Format("({0},'{1}','{2}',{3},{4},{5}),", 
                               item.Key, item.Value.Date, DWCommon.Helpers.GlobalHelpers.FormatSqlString(item.Value.Country), 
                               item.Value.Confirmed, item.Value.Deaths, item.Value.Recovered));

                        }
                        cmd_stringBuilder.Length--;
                        cmd_stringBuilder.Append(";");

                        cmd.CommandText = cmd_stringBuilder.ToString();
                        d = cmd.CommandText;
                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit();
                }
                catch (Exception)
                {
                    tr.Rollback();

                }
            }

        }

        private DWCommon.Common.CovidInfo[] ExtractDataFromCsvFile()
        {
            List<DWCommon.Common.CovidInfo> _ret = new List<DWCommon.Common.CovidInfo>();

            using (var reader = new DWCommon.Helpers.ExternelFile().GetCSVFile())
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var check_sno = DWCommon.Helpers.GlobalHelpers.ConvertStringToInt(values[0]);
                    if (!check_sno.Key) continue;

                    _ret.Add(new DWCommon.Common.CovidInfo()
                    {
                        Key = DWCommon.Helpers.GlobalHelpers.ConvertStringToInt(values[0]).Value,
                        Date = values[1],
                        Country = values[3],
                        Confirmed = DWCommon.Helpers.GlobalHelpers.ConvertStringToInt(values[5]).Value,
                        Deaths = DWCommon.Helpers.GlobalHelpers.ConvertStringToInt(values[6]).Value,
                        Recovered = DWCommon.Helpers.GlobalHelpers.ConvertStringToInt(values[7]).Value,
                    });
                }
            }

            return _ret.ToArray();
        }
    }
}
