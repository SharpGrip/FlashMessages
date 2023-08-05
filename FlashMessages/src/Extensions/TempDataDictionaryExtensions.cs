using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SharpGrip.FlashMessages.Models;

namespace SharpGrip.FlashMessages.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        public static IEnumerable<Message> GetMessages(this ITempDataDictionary tempDataDictionary, string storageKey)
        {
            if (tempDataDictionary.TryGetValue(storageKey, out var messages))
            {
                return JsonSerializer.Deserialize<IList<Message>>(messages.ToString());
            }

            return new List<Message>();
        }

        public static IEnumerable<Message> PeekMessages(this ITempDataDictionary tempDataDictionary, string storageKey)
        {
            if (tempDataDictionary.Peek(storageKey) != null)
            {
                return JsonSerializer.Deserialize<IList<Message>>(tempDataDictionary.Peek(storageKey).ToString());
            }

            return new List<Message>();
        }

        public static void SetMessages(this ITempDataDictionary tempDataDictionary, string storageKey, IEnumerable<Message> messages)
        {
            tempDataDictionary[storageKey] = JsonSerializer.Serialize(messages);
        }
    }
}