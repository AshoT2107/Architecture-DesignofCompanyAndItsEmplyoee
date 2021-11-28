using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repositoryManager;

        public WeatherForecastController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _logger = logger;
        }
        [HttpGet("getlog")]
        public IEnumerable<string> GetLog()
        {
            _logger.LogInfo("Here is info message from our values controller.");
            _logger.LogDebug("Here is debug message from our values controller.");
            _logger.LogWarn("Here is warn message from our values controller.");
            _logger.LogError("Here is an error message from our values controller.");

            return new string[] { "value1", "value2" };
        }
        [HttpGet("get")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "valule1", "value2" };
        }
    }
}
