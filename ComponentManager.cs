public class ComponentManager
{
    private readonly Dictionary<Type, object> _stores = new();

    private Dictionary<int, T> GetStore<T>() where T : struct
    {
        if (!_stores.TryGetValue(typeof(T), out var store))
        {
            store = new Dictionary<int, T>();
            _stores[typeof(T)] = store;
        }
        return (Dictionary<int, T>)store;
    }

    public void AddComponent<T>(int entityId, T component) where T : struct
    {
        GetStore<T>()[entityId] = component;
    }

    public bool HasComponent<T>(int entityId) where T : struct
    {
        return GetStore<T>().ContainsKey(entityId);
    }

    public ref T GetComponent<T>(int entityId) where T : struct
    {
        var store = GetStore<T>();
        if (!store.ContainsKey(entityId))
            throw new KeyNotFoundException($"Entity {entityId} saknar {typeof(T).Name}");
        return ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrNullRef(store, entityId);
    }

    public void RemoveComponent<T>(int entityId) where T : struct
    {
        GetStore<T>().Remove(entityId);
    }

    public IEnumerable<int> GetEntitiesWith<T>() where T : struct
    {
        return GetStore<T>().Keys;
    }
}
