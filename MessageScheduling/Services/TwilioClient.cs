using MessageScheduling.Interfaces;
using Microsoft.Extensions.Configuration;
using Twilio.Clients;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace MessageScheduling.Services
{
    public class TwilioClient : ITwilioClient
    {
        private readonly TwilioRestClient _client;

        public TwilioClient(IConfiguration configuration)
        {
            var accountSid = configuration["Twilio:AccountSid"];
            var authToken = configuration["Twilio:AuthToken"];
            _client = new TwilioRestClient(accountSid, authToken);
        }

        public async Task<MessageScheduling.Models.MessageResource> SendSmsAsync(string to, string from, string message)
    {
        var twilioMessage = await MessageResource.CreateAsync(
            to: new PhoneNumber(to),    
            from: new PhoneNumber(from),
            body: message,
            client: _client
        );

        return MapToMessageResource(twilioMessage);
    }

    public async Task<MessageScheduling.Models.CallResource> MakeCallAsync(string to, string from, string url)
    {
        var twilioCall = await CallResource.CreateAsync(
            to: new PhoneNumber(to),
            from: new PhoneNumber(from),
            url: new Uri(url),
            client: _client
        );

        return MapToCallResource(twilioCall);
    }

    public async Task<MessageScheduling.Models.MessageResource> GetMessageStatusAsync(string messageSid)
    {
        var twilioMessage = await MessageResource.FetchAsync(
            pathSid: messageSid,
            client: _client
        );

        return MapToMessageResource(twilioMessage);
    }

    public async Task<MessageScheduling.Models.CallResource> GetCallStatusAsync(string callSid)
    {
        var twilioCall = await CallResource.FetchAsync(
            pathSid: callSid,
            client: _client
        );

        return MapToCallResource(twilioCall);
    }

    private MessageScheduling.Models.MessageResource MapToMessageResource(MessageResource twilioMessage)
    {
        return new MessageScheduling.Models.MessageResource
        {
            Sid = twilioMessage.Sid,
            Body = twilioMessage.Body,
            From = twilioMessage.From,           
            To = twilioMessage.To,
            Status = ParseMessageStatus(twilioMessage.Status.ToString()),
            DateCreated = twilioMessage.DateCreated?.Date ?? DateTime.UtcNow,
            DateSent = twilioMessage.DateSent?.Date ?? DateTime.UtcNow,
            Price = twilioMessage.Price,
            ErrorMessage = twilioMessage.ErrorMessage
        };
    }

    private MessageScheduling.Models.CallResource MapToCallResource(CallResource twilioCall)
    {
        return new MessageScheduling.Models.CallResource
        {
            Sid = twilioCall.Sid,
            From = twilioCall.From,
            To = twilioCall.To,
            Status = ParseCallStatus(twilioCall.Status.ToString()),
            StartTime = twilioCall.StartTime?.Date ?? DateTime.UtcNow,
            EndTime = twilioCall.EndTime?.Date ?? DateTime.UtcNow,
            Price = twilioCall.Price,
            ErrorMessage = null // Twilio call resource doesn't have direct error message
        };
    }

    private Configurations.MessageStatus ParseMessageStatus(string status)
    {
        return Enum.TryParse<Configurations.MessageStatus>(status, true, out var result) 
            ? result 
            : Configurations.MessageStatus.Failed;
    }

    private Configurations.CallStatus ParseCallStatus(string status)
    {
        return Enum.TryParse<Configurations.CallStatus>(status, true, out var result) 
            ? result 
            : Configurations.CallStatus.Failed;
    }
    }
}
