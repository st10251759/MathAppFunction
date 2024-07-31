using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MathFunctionApp
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("AddTwoFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Get the 'number' query parameter
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            if (!int.TryParse(query["number"], out int number))
            {
                var badResponse = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Please pass a valid number in the query string as '?number={value}'");
                return badResponse;
            }

            // Add 2 to the number
            int result = number + 2;

            // Create response
            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteStringAsync(result.ToString());

            return response;
        }
        [Function("AddTwoNumbersFunction")]
        public async Task<HttpResponseData> AddTwoNumbers([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Get the 'number1' and 'number2' query parameters
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            if (!int.TryParse(query["number1"], out int number1) || !int.TryParse(query["number2"], out int number2))
            {
                var badResponse = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Please pass valid numbers in the query string as '?number1={value}&number2={value}'");
                return badResponse;
            }

            // Add the two numbers
            int result = number1 + number2;

            // Create response
            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteStringAsync(result.ToString());

            return response;
        }
    }
}