using System.Collections.Concurrent;

namespace System.Clock
{
    /// <summary>
    /// Delegates to CallContext.LogicalGetData and .LogicalSetData, as this is the Task-aware way to
    /// get the equivalent of thread local storage.
    /// </summary>
    public class RobustThreadLocal<T>
    {
        readonly ConcurrentDictionary<string, T> _contextMappings = new ConcurrentDictionary<string, T>();
        readonly Func<T> _initialValue;
        readonly string _key;

        public RobustThreadLocal() : this(() => default(T))
        {
        }

        public RobustThreadLocal(Func<T> initialValue)
        {
            _initialValue = initialValue;
            _key = Guid.NewGuid().ToString();

            CallContext.SetData(_key, default(T));
        }

        string GetMappingKey()
        {
            var mappingKey = (string)CallContext.GetData(_key);
            if (mappingKey != null)
                return mappingKey;

            mappingKey = Guid.NewGuid().ToString();
            CallContext.SetData(_key, mappingKey);

            return mappingKey;
        }

        public T Value
        {
            get
            {
                var mappingKey = GetMappingKey();
                return _contextMappings.GetOrAdd(mappingKey, _ => _initialValue());
            }
            set
            {
                var mappingKey = GetMappingKey();
                _contextMappings[mappingKey] = value;
            }
        }
    }
}