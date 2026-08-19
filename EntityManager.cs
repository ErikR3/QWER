namespace QWER;

public class EntityManager
{
    private int[] sparse = new int[ECSConfig.MaxEntities];
    private int[] dense = new int[ECSConfig.MaxEntities];
    private int denseCount = 0;

    public EntityManager()
    {

    }

    public void PrintSets()
    {
        // Dense-raden
        Console.Write("dense:  [");
        for (int i = 0; i < denseCount; i++)
        {
            Console.Write(dense[i]);
            if (i < denseCount - 1) Console.Write(", ");
        }
        Console.WriteLine("]");

        // Sparse-raden
        Console.Write("sparse: [");
        for (int i = 0; i < sparse.Length; i++)
        {
            // Anta t.ex. -1 som "oanvänd" markör i din faktiska array
            string val = sparse[i] == -1 ? "_" : sparse[i].ToString();
            Console.Write(val.PadLeft(2));
            if (i < sparse.Length - 1) Console.Write(", ");
        }
        Console.WriteLine("]");

        // Indexraden (för tydlighet)
        Console.Write("         ");
        for (int i = 0; i < sparse.Length; i++)
        {
            Console.Write(i.ToString().PadLeft(2));
            if (i < sparse.Length - 1) Console.Write("  ");
        }
        Console.WriteLine();
    }

    public int[] GetSparse => sparse;

    public int[] GetDense => dense;

    public int GetSpecDense(int entityId) => dense[entityId];

    public int GetSpecSparse(int entityId)
    {
        return 0;
    }

    public int GetDenseCount() => denseCount;

    public void AddEntity(int entityId)
    {
        if (denseCount == dense.Length)
            Array.Resize(ref dense, dense.Length * 2);
        dense[denseCount++] = entityId;
        sparse[denseCount] = entityId;
    }

    public int GetEntity(int entityId)
    {
        return 0;
    }
}
