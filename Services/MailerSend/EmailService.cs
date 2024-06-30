using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniDrive.Helpers;

namespace MiniDrive.Services.MailerSend
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _ApiKey;
        private readonly string? _fromEmail;

        public EmailService(HttpClient httpClient, IConfiguration configuration){

            _ApiKey = configuration["MailerSend:ApiKey"];
            _fromEmail = configuration["MailerSend:FromEmail"];
            _httpClient = httpClient;
        }

      

        public async Task<string> SendWelcomeEmail(string toEmail, string templatePath, Dictionary<string, string> placeholders)
        {
            var subject = $"Bienvenido a nuestra Plataforma!!";
            //var body = $"El cupon se ha creado con exito!";
            var template = EmailTemplateHelper.LoadTemplate(templatePath);
            var body = EmailTemplateHelper.ReplacePlaceholders(template, placeholders);

            var requestContent = new {
                from = new {email= _fromEmail},
                to = new[] {new{ email =  toEmail}},
                subject,
                text = body,
                html = template
            };

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _ApiKey);
        
        try
        {
            var response = await _httpClient.PostAsJsonAsync("https://api.mailersend.com/v1/email", requestContent);

            response.EnsureSuccessStatusCode();
            return "Se ha enviado con exito !";
        }
        catch (HttpRequestException ex)
        {
            // Manejo de errores
            // Puedes registrar el error o lanzar una excepción personalizada
            return "Error  : " + ex.Message;
            throw new ApplicationException("Error sending email through MailerSend -:", ex);
        }
        }
    }
}