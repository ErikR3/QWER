namespace QWER;

public static class ECSDebugger
{
    public static void PrintSets(EntityManager em)
    {
        var dense = em.GetDense;
        var sparse = em.GetSparse;
        int denseCount = em.GetDenseCount();

        Console.Write("dense:  [");
        for (int i = 0; i < denseCount; i++)
        {
            Console.Write(dense[i]);
            if (i < denseCount - 1) Console.Write(", ");
        }
        Console.WriteLine("]");

        int maxEntityId = -1;
        for (int i = 0; i < denseCount; i++)
        {
            if (dense[i] > maxEntityId) maxEntityId = dense[i];
        }
        int sparsePrintCount = maxEntityId + 1;

        Console.Write("sparse: [");
        for (int i = 0; i < sparsePrintCount; i++)
        {
            string val = sparse[i] == -1 ? "_" : sparse[i].ToString();
            Console.Write(val.PadLeft(2));
            if (i < sparsePrintCount - 1) Console.Write(", ");
        }
        Console.WriteLine("]");
    }
}
