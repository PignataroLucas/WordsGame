using System;
using System.Threading;
using System.Threading.Tasks;

namespace S.Utility.U.ServiceLocator
{
    public class Services
    {
        private static ServiceLocator _instance = new();

        public static void Add<T>(T service) where T : class
        {
            _instance.Add(service);
        }

        public static void WaitFor<T>(Action<T> onServiceAvailable) where T : class
        {
            _instance.WaitFor(onServiceAvailable);
        }

        public static async Task<T> WaitForAsync<T>(CancellationToken token = default) where T : class
        {
            var tcs = new TaskCompletionSource<T>();
            using var ctr = token.Register(() => tcs.TrySetCanceled());

            WaitFor<T>(s => tcs.TrySetResult(s));
            return await tcs.Task;
        }

        public static T Get<T>() where T : class => _instance.Get<T>();
        public static bool Exists<T>() where T : class => _instance.Exists<T>();
        public static T TryGet<T>() where T : class => Exists<T>() ? Get<T>() : null;

        public static void Clean<T>(bool dispose = true) where T : class => _instance.Clean<T>(dispose);
        public static void Clear() => _instance.Clear();
        public static void ClearAllWaiting() => _instance.ClearAllWaiting();
    }
}
