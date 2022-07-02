using System;
using UnityEngine;

namespace Channels
{
    public abstract class ChannelBase<T> : ScriptableObject, IChannel<T>
    {
        private event Action<T> OnInvoke = _ => { };

        public void Subscribe(Action<T> action)
        {
            OnInvoke += action;
        }

        public void Unsubscribe(Action<T> action)
        {
            OnInvoke -= action;
        }

        public void Fire(T state)
        {
            OnInvoke.Invoke(state);
        }
    }
}