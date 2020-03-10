using System.Collections.Generic;
using SharpGrip.FlashMessages.Models;

namespace SharpGrip.FlashMessages
{
    public interface IFlasher
    {
        public void Add(string type, string message);
        public IEnumerable<Message> GetMessages();
        public IEnumerable<Message> PeekMessages();
        public void Clear();
    }
}