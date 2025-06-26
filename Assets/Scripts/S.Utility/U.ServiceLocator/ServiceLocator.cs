using System;
using System.Collections.Generic;

namespace S.Utility.U.ServiceLocator
{
    internal class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new();
        private readonly Dictionary<Type, List<object>> _actionsWaitingForServices = new();

        public void Add<T>(T service) where T : class
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                UnityEngine.Debug.LogWarning($"[ServiceLocator] Service {type.Name} ya registrado. Reemplazando.");
            }

            _services[type] = service;
            TriggerActionsWaitingForService(service);
        }

        public void WaitFor<T>(Action<T> onServiceAvailable) where T : class
        {
            Type type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                onServiceAvailable.Invoke((T)service);
            }
            else
            {
                AddWaitingAction(type, onServiceAvailable);
            }
        }

        public void Clear()
        {
            foreach (object service in _services.Values)
            {
                if (service is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            _services.Clear();
            _actionsWaitingForServices.Clear();
        }

        public bool Exists<T>() where T : class
        {
            return _services.ContainsKey(typeof(T));
        }

        private void TriggerActionsWaitingForService<T>(T service) where T : class
        {
            Type type = typeof(T);

            if (_actionsWaitingForServices.TryGetValue(type, out var actions))
            {
                foreach (Action<T> action in actions)
                {
                    action.Invoke(service);
                }
                actions.Clear();
            }
        }

        private void AddWaitingAction<T>(Type type, Action<T> action)
        {
            if (!_actionsWaitingForServices.TryGetValue(type, out var actions))
            {
                actions = new List<object>();
                _actionsWaitingForServices[type] = actions;
            }
            actions.Add(action);
        }

        public T Get<T>() where T : class
        {
            return (T)_services[typeof(T)];
        }

        public void Clean<T>(bool dispose = true) where T : class
        {
            var type = typeof(T);

            if (_services.TryGetValue(type, out var service))
            {
                if (dispose && service is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                _services.Remove(type);
            }

            if(_actionsWaitingForServices.TryGetValue(type, out var actions))
            {
                actions.Clear();
                _actionsWaitingForServices.Remove(type);
            }
        }

        public void ClearAllWaiting()
        {
            _actionsWaitingForServices.Clear();
        }
    }
}
