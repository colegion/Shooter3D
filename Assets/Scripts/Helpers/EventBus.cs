using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> EventListeners =
            new Dictionary<Type, List<Delegate>>();

        public static void Register<T>(Action<T> listener) where T : class
        {
            Type eventType = typeof(T);
            if (!EventListeners.ContainsKey(eventType))
            {
                EventListeners[eventType] = new List<Delegate>();
            }

            EventListeners[eventType].Add(listener);
        }

        public static void Unregister<T>(Action<T> listener) where T : class
        {
            Type eventType = typeof(T);
            if (EventListeners.TryGetValue(eventType, out var eventListeners))
            {
                eventListeners.Remove(listener);
            }
        }

        public static void Trigger<T>(T eventData) where T : class
        {
            Type eventType = typeof(T);
            if (EventListeners.TryGetValue(eventType, out var eventListeners))
            {
                foreach (var listener in new List<Delegate>(eventListeners))
                {
                    ((Action<T>)listener)(eventData);
                }
            }
        }

        public static void ClearEvents()
        {
            EventListeners.Clear();
        }
    }
}