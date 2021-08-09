using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestAPI.Models;

namespace TestAPI.Controllers 
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly IConfiguration configuration;

        public LogController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<ListLogResponseModel>> Get()
        {
            try
            {
                ListLogResponseModel listLogResponseModel = new ListLogResponseModel();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    configuration.GetSection("LogProxyAPI:Token").Value);
                var response = await httpClient.GetAsync(configuration.GetSection("LogProxyAPI:BaseUrl").Value + configuration.GetSection("LogProxyAPI:GetMessage").Value);

                listLogResponseModel = JsonSerializer.Deserialize<ListLogResponseModel>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return Ok(listLogResponseModel);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpPost]
        public async Task<ActionResult<CreateLogResponseModel>> Post(CreateLogRequestModel createLogRequestModel)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    configuration.GetSection("LogProxyAPI:Token").Value);
                var content = new StringContent(JsonSerializer.Serialize(createLogRequestModel), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(configuration.GetSection("LogProxyAPI:BaseUrl").Value + configuration.GetSection("LogProxyAPI:CreateMessage").Value, content);

                var createLogResponseModel = JsonSerializer.Deserialize<CreateLogResponseModel>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return Ok(createLogResponseModel);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }





    }
}
