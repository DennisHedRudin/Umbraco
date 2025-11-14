using System.Diagnostics;
using Azure;
using Azure.Communication.Email;
using UmbracoCMS.ViewModels;

namespace UmbracoCMS.Services
{


    public class CommunicationService(IConfiguration configuration, EmailClient client) : ICommunicationService
    {
        private readonly EmailClient _client = client;
        private readonly IConfiguration _configuration = configuration;

        public async Task<EmailResult> SendCallbackEmailAsync(CallbackFormViewModel model)
        {
            try
            {
                var sender = _configuration["ACS:SenderAddress"]!;
                var subject = $"Callback Request {model.Name}";

                var plainText = $@"
                Hi {model.Name}! 

                Thank you for reaching out to us about {model.SelectedOption}.
                We'll contact you on {model.Phone}.

                Have a great day!

                Kind Regards, Umbraco Team";

                var htmlContent = $@"
                <html>
                    <body>
                        <h2> Hi {model.Name}!</h2>
                        <p>Tank you for reaching out to us about <strong>{model.SelectedOption}</strong>.</p>
                        <p>We'll contact you on <strong>{model.Phone}</strong>.</p>
                        <br/>
                        <p>Have a great day!<br/>Kind Regards, Umbraco Team</p>
                    </body>
                </html>";


                var emailMessage = new EmailMessage(
                    senderAddress: sender,
                    recipients: new EmailRecipients([new(model.Email)]),
                    content: new EmailContent(subject)
                    {
                        Html = htmlContent,
                        PlainText = plainText,
                    });

                await _client.SendAsync(WaitUntil.Completed, emailMessage);
                return new EmailResult { Success = true };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new EmailResult { Success = false, Error = "Failed to send email" };
            }

        }

        public async Task<EmailResult> SendQuestionEmailAsync(QuestionFormViewModel model)
        {
            try
            {
                var sender = _configuration["ACS:SenderAddress"]!;
                var subject = $"Question from {model.Name}";
                var plainText = $@"
                Hi {model.Name}! 

                Thank you for your question.
                We'll contact you on soon.

                Have a great day!

                Kind Regards, Umbraco Team";

                var htmlContent = $@"
                <html>
                    <body>
                        <h2> Hi {model.Name}!</h2>
                        <p>Tank you for your question.</p>
                        <p>We'll contact you on soon..</p>
                        <br/>
                        <p>Have a great day!<br/>Kind Regards, Umbraco Team</p>
                    </body>
                </html>";

                var emailMessage = new EmailMessage(
                    senderAddress: sender,
                    recipients: new EmailRecipients([new(model.QuestionEmail)]),
                    content: new EmailContent(subject)
                    {
                        Html = htmlContent,
                        PlainText = plainText,
                    });

                await _client.SendAsync(WaitUntil.Completed, emailMessage);
                return new EmailResult { Success = true };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new EmailResult { Success = false, Error = "Failed to send email" };
            }
        }


        public async Task<EmailResult> SendSupportEmailAsync(SupportFormViewModel model)
        {
            try
            {
                var sender = _configuration["ACS:SenderAddress"]!;
                var subject = $"Support request";
                var plainText = $@"
                Hi! 

                Thank you for reaching out.
                We'll contact you on soon.

                Have a great day!

                Kind Regards, Umbraco Team";

                var htmlContent = $@"
                <html>
                    <body>
                        <h2> Hi!</h2>
                        <p>Tank you for your question.</p>
                        <p>We'll contact you on soon..</p>
                        <br/>
                        <p>Have a great day!<br/>Kind Regards, Umbraco Team</p>
                    </body>
                </html>";

                var emailMessage = new EmailMessage(
                    senderAddress: sender,
                    recipients: new EmailRecipients([new(model.SupportEmail)]),
                    content: new EmailContent(subject)
                    {
                        Html = htmlContent,
                        PlainText = plainText,
                    });
                await _client.SendAsync(WaitUntil.Completed, emailMessage);
                return new EmailResult { Success = true };

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new EmailResult { Success = false, Error = "Failed to send email" };
            }


        }

    }


}
