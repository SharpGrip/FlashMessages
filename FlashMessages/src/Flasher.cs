using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using SharpGrip.FlashMessages.Extensions;
using SharpGrip.FlashMessages.Models;
using SharpGrip.FlashMessages.Options;

namespace SharpGrip.FlashMessages
{
    public class Flasher : IFlasher
    {
        private readonly ITempDataDictionary tempDataDictionary;
        private readonly string storageKey;
        private IList<Message> Messages { get; } = new List<Message>();

        public Flasher(ITempDataDictionaryFactory tempDataDictionaryFactory, IHttpContextAccessor httpContextAccessor, IOptions<FlashMessagesOptions> options)
        {
            tempDataDictionary = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);
            storageKey = options.Value.StorageKey;

            if (tempDataDictionary.TryGetValue(storageKey, out var messages))
            {
                Messages = JsonSerializer.Deserialize<IList<Message>>(messages.ToString());
            }
        }

        public void Add(string messageType, string messageText)
        {
            Messages.Add(new Message {Type = messageType, Text = messageText});
            tempDataDictionary.SetMessages(storageKey, Messages);
        }

        public IEnumerable<Message> GetMessages()
        {
            return tempDataDictionary.GetMessages(storageKey);
        }

        public IEnumerable<Message> PeekMessages()
        {
            return tempDataDictionary.PeekMessages(storageKey);
        }

        public void Clear()
        {
            Messages.Clear();
            tempDataDictionary.SetMessages(storageKey, Messages);
        }
    }
}