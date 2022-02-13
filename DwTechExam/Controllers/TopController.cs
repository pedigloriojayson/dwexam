using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DwTechExam.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TopController : ControllerBase
    {

        private readonly ILogger<TopController> _logger;

        public TopController(ILogger<TopController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public DWCommon.Responses.ResponseConfirmed Confirmed(string observation_date, int max_results)
        
        {

            var check_date = DWCommon.Helpers.GlobalHelpers.ConverStringToSqlDate(observation_date);

            if (!check_date.Key)
                return new DWCommon.Responses.ResponseConfirmed()
                {
                    observation_date = check_date.Value,
                    countries = new DWCommon.Responses.ConfirmedData[] { }
                };


            return new DWCommon.Responses.ResponseConfirmed()
            {
                observation_date = check_date.Value,
                countries = new DWApp.DbService.DbApplication().GetReport(check_date.Value, max_results).ToArray()
            };
        }
    }
}