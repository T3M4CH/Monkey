using System;

namespace Channels
{
    public interface IChannel<T>
    {
        void Subscribe(Action<T> action);
        void Unsubscribe(Action<T> action);
        void Fire(T state);
    }
}