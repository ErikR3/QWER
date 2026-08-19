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

    public int GetSpecDense(int entityId) => dense[entityId];

    public int GetSpecSparse(int entityId)
    {
        return 0;
    }

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

    public void RemoveEntity()
    {

    }

    public int GetEntity(int entityId)
    {
        return 0;
    }
}
