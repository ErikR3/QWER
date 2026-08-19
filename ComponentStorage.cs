namespace QWER;

public interface IComponentStorage
{
    bool HasComponent(int entityId);
    void RemoveComponent(int entityId);
}

public class ComponentStorage<T> : IComponentStorage where T : struct
{
    private int[] sparse;
    private int[] denseEntities;
    private T[] denseComponents;
    private int count;

    public ComponentStorage(int maxEntityId = ECSConfig.MaxEntities, int capacity = 16)
    {
        sparse = new int[maxEntityId];
        Array.Fill(sparse, -1);
        denseEntities = new int[capacity];
        denseComponents = new T[capacity];
        count = 0;
    }

    public int[] GetSparse => sparse;

    public T[] GetDenseComponents => denseComponents;

    public int[] GetDenseEntities => denseEntities;

    public int GetDenseCount() => count;

    public bool HasComponent(int entityId) => sparse[entityId] != -1;

    public void AddComponent(int entityId, T component)
    {
        if (HasComponent(entityId))
        {
            denseComponents[sparse[entityId]] = component;
            return;
        }

        if (count == denseEntities.Length)
        {
            Array.Resize(ref denseEntities, denseEntities.Length * 2);
            Array.Resize(ref denseComponents, denseComponents.Length * 2);
        }

        denseEntities[count] = entityId;
        denseComponents[count] = component;
        sparse[entityId] = count;
        count++;
    }

    public void RemoveComponent(int entityId)
    {
        if (!HasComponent(entityId))
            return;

        int index = sparse[entityId];
        int lastIndex = count - 1;
        int lastEntity = denseEntities[lastIndex];

        denseEntities[index] = lastEntity;
        denseComponents[index] = denseComponents[lastIndex];
        sparse[lastEntity] = index;

        count--;
        sparse[entityId] = -1;
    }

    public ref T GetComponent(int entityId)
    {
        if (!HasComponent(entityId))
            throw new KeyNotFoundException($"Entity {entityId} saknar {typeof(T).Name}");
        return ref denseComponents[sparse[entityId]];
    }

    public IEnumerable<int> GetEntities()
    {
        for (int i = 0; i < count; i++)
            yield return denseEntities[i];
    }
}
