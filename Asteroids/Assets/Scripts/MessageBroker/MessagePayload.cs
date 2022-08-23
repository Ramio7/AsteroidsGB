using System;

namespace RRRStudyProject
{
    public class MessagePayload<T>
    {
        public object Who { get; set; }
        public T What { get; set; }
        public DateTime When { get; set; }
        public MessagePayload(T payload, object source)
        {
            Who = source;
            What = payload;
            When = DateTime.Now;
        }
    }
}