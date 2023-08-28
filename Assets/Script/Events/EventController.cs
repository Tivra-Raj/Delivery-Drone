using System;

namespace Events
{
    public class EventController
    {
        public event Action BaseEvent;
        public void InvokeEvent() => BaseEvent?.Invoke();
        public void AddListener(Action listener) => BaseEvent += listener;
        public void RemoveListener(Action listener) => BaseEvent -= listener;
    }
}