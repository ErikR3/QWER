namespace QWER;

public class ComponentStorage
{
    private int[] sparse;
    private int[] denseEntities;
    private T[] denseComponents;
    private int count;

    public ComponentStorage(int maxEntityId, int capacity = 16)
    {
        sparse = new int { maxEntityId };
        Array.fill(sparse, -1);
        denseEntities = new int[capacity];
        denseComponents = new T[capacity];
    }
}
