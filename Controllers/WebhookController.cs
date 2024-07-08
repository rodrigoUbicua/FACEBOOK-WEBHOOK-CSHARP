using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FACEBOOK_WEBHOOK_C
{
    [Route("api/webhook")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private const string VERIFY_TOKEN = "testando";

        // GET: api/webhook
        [HttpGet]
        public IActionResult Get()
        {
            var mode = Request.Query["hub.mode"];
            var token = Request.Query["hub.verify_token"];
            var challenge = Request.Query["hub.challenge"];

            if (mode == "subscribe" && token == VERIFY_TOKEN)
            {
                Console.WriteLine("WEBHOOK_VERIFIED");
                return Content(challenge, "text/plain"); // Retorna o desafio como texto simples
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string body = await reader.ReadToEndAsync();
                    // Aqui você pode imprimir o corpo da requisição
                    Console.WriteLine("Webhook Data:");
                    Console.WriteLine(body);

                    // Parse do corpo como JObject
                    JObject data = JObject.Parse(body);

                    // Aqui você pode processar os dados do webhook conforme necessário
                    return Ok(data); // Retorna os dados processados como JSON
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing webhook: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
