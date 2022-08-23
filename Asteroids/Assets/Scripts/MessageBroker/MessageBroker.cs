using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RRRStudyProject
{
    public class MessageBroker : IMesssageBroker
    {
        private static MessageBroker _instance;
        private readonly Dictionary<Type, List<Delegate>> _subscribers;

        public static MessageBroker Instance
        {
            get
            {
                _instance ??= new MessageBroker();
                return _instance;
            }
        }

        private MessageBroker()
        {
            _subscribers = new Dictionary<Type, List<Delegate>>();
        }

        public void Dispose()
        {
            _subscribers?.Clear();
        }

        public void Publish<T>(object source, T message)
        {
            if (message == null || source == null)
                return;
            if (!_subscribers.ContainsKey(typeof(T)))
            {
                return;
            }
            var delegates = _subscribers[typeof(T)];
            if (delegates == null || delegates.Count == 0) return;
            var payload = new MessagePayload<T>(message, source);
            foreach (var handler in delegates.Select
            (item => item as Action<MessagePayload<T>>))
            {
                Task.Factory.StartNew(() => handler?.Invoke(payload));
            }
        }

        public void Subscribe<T>(Action<MessagePayload<T>> subscribtion)
        {
            var delegates = _subscribers.ContainsKey(typeof(T)) ?
                            _subscribers[typeof(T)] : new List<Delegate>();
            if (!delegates.Contains(subscribtion))
            {
                delegates.Add(subscribtion);
            }
            _subscribers[typeof(T)] = delegates;
        }

        public void Unsubscribe<T>(Action<MessagePayload<T>> subscribtion)
        {
            if (!_subscribers.ContainsKey(typeof(T))) return;
            var delegates = _subscribers[typeof(T)];
            if (delegates.Contains(subscribtion))
                delegates.Remove(subscribtion);
            if (delegates.Count == 0)
                _subscribers.Remove(typeof(T));
        }
    }
}