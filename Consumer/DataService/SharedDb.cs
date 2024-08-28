using Consumer.Hubs;
using System.Collections.Concurrent;

namespace Consumer.DataService
{
    public class SharedDb
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections;

        public ConcurrentDictionary<string, UserConnection> Connections => _connections;
    }
}
