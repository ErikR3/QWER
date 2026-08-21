namespace QWER;

public class EntityManager
{
    private int[] sparse = new int[ECSConfig.MaxEntities];
    private int[] dense = new int[ECSConfig.MaxEntities];
    private int denseCount = 0;
    private int[] unUsedId = new int[ECSConfig.MaxEntities];
    private int freeCount = ECSConfig.MaxEntities;

    public EntityManager()
    {
        Array.Fill(sparse, -1);
        for (int i = 0; i < ECSConfig.MaxEntities; i++)
        {
            unUsedId[(ECSConfig.MaxEntities - 1) - i] = i;
        }
    }

    public int[] GetSparse => sparse;

    public int[] GetDense => dense;

    public int[] GetUnUsedId => unUsedId;

    public int GetDenseCount() => denseCount;

    private int GetFreeId()
    {
        if (freeCount != 0)
        {
            var entityId = unUsedId[freeCount - 1];
            return entityId;
        } else {
            return -1;
        }
    }

    public int GetEntityAt(int denseIndex)
    {
        if (denseIndex < 0 || denseIndex >= denseCount)
            throw new ArgumentOutOfRangeException(nameof(denseIndex));
        return dense[denseIndex];
    }

    public int GetDenseIndex(int entityId) => sparse[entityId];

    public void AddEntity()
    {
        var entityId = GetFreeId();
        dense[denseCount] = entityId;
        sparse[entityId] = denseCount;
        denseCount++;

        unUsedId[freeCount - 1] = -1;
        freeCount--;
    }

    public void RemoveEntity(int entityId)
    {
        var index = sparse[entityId];
        int lastEntity = dense[denseCount - 1];
        dense[index] = lastEntity;
        sparse[lastEntity] = index;
        denseCount--;
        sparse[entityId] = -1;

        unUsedId[freeCount] = entityId;
        freeCount++;
    }
}
