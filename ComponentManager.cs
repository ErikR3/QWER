namespace QWER;

public class ComponentManager
{
    private readonly Dictionary<Type, IComponentStorage> stores = new();

    private ComponentStorage<T> GetStorage<T>() where T : struct
    {
        var type = typeof(T);
        if (!stores.TryGetValue(type, out var store))
        {
            store = new ComponentStorage<T>();
            stores[type] = store;
        }
        return (ComponentStorage<T>)store;
    }

    public void AddComponent<T>(int entityId, T component) where T : struct
    {
        GetStorage<T>().AddComponent(entityId, component);
    }

    public bool HasComponent<T>(int entityId) where T : struct
    {
        return GetStorage<T>().HasComponent(entityId);
    }

    public ref T GetComponent<T>(int entityId) where T : struct
    {
        return ref GetStorage<T>().GetComponent(entityId);
    }

    public void RemoveComponent<T>(int entityId) where T : struct
    {
        GetStorage<T>().RemoveComponent(entityId);
    }

    public IEnumerable<int> GetEntitiesWith<T>() where T : struct
    {
        return GetStorage<T>().GetEntities();
    }

    public void EntityDestroyed(int entityId)
    {
        foreach (var store in stores.Values)
            store.RemoveComponent(entityId);
    }
}
