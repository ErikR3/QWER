namespace QWER;

public class EntityManager
{
    private int[] sparse = new int[ECSConfig.MaxEntities];
    private int[] dense = new int[ECSConfig.MaxEntities];
    private int denseCount = 0;

    public EntityManager()
    {
        Array.Fill(sparse, -1);
    }

    public int[] GetSparse => sparse;

    public int[] GetDense => dense;

    public int GetEntityAt(int denseIndex)
    {
        if (denseIndex < 0 || denseIndex >= denseCount)
            throw new ArgumentOutOfRangeException(nameof(denseIndex));
        return dense[denseIndex];
    }

    public int GetDenseIndex(int entityId) => sparse[entityId];

    public int GetDenseCount() => denseCount;

    /* dense:  [3, 7, 2]        // index 0, 1, 2
    sparse: [_, _, 2, 0, _, _, _, 1, _, _]
             0  1  2  3  4  5  6  7  8  9   <- detta är "värdet" */

    public void AddEntity(int entityId)
    {
        if (denseCount == dense.Length)
            Array.Resize(ref dense, dense.Length * 2);
        dense[denseCount] = entityId;
        sparse[entityId] = denseCount;
        denseCount++;
    }

    public void RemoveEntity(int entityId)
    {
        var index = sparse[entityId];
        int lastEntity = dense[denseCount - 1];
        dense[index] = lastEntity;
        sparse[lastEntity] = index;
        denseCount--;
        sparse[entityId] = -1;
    }

    public int GetEntity(int entityId)
    {
        return 0;
    }
}
