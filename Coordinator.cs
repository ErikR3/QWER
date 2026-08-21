using System.ComponentModel;

namespace QWER;

public class Coordinator {
    private EntityManager em;
    private ComponentManager cm;
    private int[] EntitiesIdDense = new int[ECSConfig.MaxEntities];

    public Coordinator()
    {
        em = new EntityManager();
        cm = new ComponentManager();
    }

    public int CreateEntity()
    {
        var newEntityId = em.AddEntity();

        return newEntityId;
    }

    public void DestroyEntity(int entityId)
    {
        em.RemoveEntity(entityId);
        cm.EntityDestroyed(entityId);
    }
}
