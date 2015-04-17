using System;
using System.Collections.Concurrent;
using System.Runtime.Remoting.Messaging;

namespace SystemClock {
  /// <summary>
  /// Delegates to CallContext.LogicalGetData and .LogicalSetData, as this is the Task-aware way to
  /// get the equivalent of thread local storage.
  /// </summary>
  public class RobustThreadLocal<T> {
    readonly ConcurrentDictionary<string, T> _contextMappings = new ConcurrentDictionary<string, T>();
    readonly Func<T> _initialValue;
    readonly string _key;

    public RobustThreadLocal() : this(() => default(T)) { }

    public RobustThreadLocal(Func<T> initialValue) {
      _initialValue = initialValue;
      _key = Guid.NewGuid().ToString();

      CallContext.LogicalSetData(_key, default(T));
    }

    string GetMappingKey() {
      var mappingKey = (string)CallContext.LogicalGetData(_key);
      if (mappingKey != null) 
        return mappingKey;

      mappingKey = Guid.NewGuid().ToString();
      CallContext.LogicalSetData(_key, mappingKey);

      return mappingKey;
    }

    public T Value {
      get {
        var mappingKey = GetMappingKey();
        return _contextMappings.GetOrAdd(mappingKey, _ => _initialValue());
      }
      set {
        var mappingKey = GetMappingKey();
        _contextMappings[mappingKey] = value;
      }
    }
  }
}
