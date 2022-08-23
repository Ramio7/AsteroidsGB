using System;

namespace RRRStudyProject
{
    public interface IMesssageBroker : IDisposable
    {
        void Publish<T>(object source, T message);
        void Subscribe<T>(Action<MessagePayload<T>> subscribtion);
        void Unsubscribe<T>(Action<MessagePayload<T>> subscribtion);
    }
}