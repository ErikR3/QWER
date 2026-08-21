using System.ComponentModel;

namespace QWER;

public class Coordinator {
    private EntityManager em;
    private ComponentManager cm;

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

    public void AddComponent<T>(int entityId, T component) where T : struct
    {
        if (em.ValidateIdInRange(entityId))
        {
            if (em.ValidateIdInUse(entityId))
            {
                cm.AddComponent<T>(entityId, component);
            }
            else
            {
                throw new InvalidOperationException($"EntityId {entityId} is not currently active");
            }
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(entityId));
        }
    }

    // When you call this function you need to explicitly state the struct type
    // Otherwise it won't compile because it cant check for whether the entity does
    // Have the specified component or not.

    public bool HasComponent<T>(int entityId) where T : struct
    {
        if (em.ValidateIdInRange(entityId))
        {
            if (em.ValidateIdInUse(entityId))
            {
                return cm.HasComponent<T>(entityId);
            }
            else
            {
                throw new InvalidOperationException($"EntityId {entityId} is not currently active");
            }
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(entityId));
        }
    }

    public ref T GetComponent<T>(int entityId) where T : struct
    {
        if (em.ValidateIdInRange(entityId))
        {
            if (em.ValidateIdInUse(entityId))
            {
                return ref cm.GetComponent<T>(entityId);
            }
            else
            {
                throw new InvalidOperationException($"EntityId {entityId} is not currently active");
            }
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(entityId));
        }
    }

    public void RemoveComponent<T>(int entityId) where T : struct
    {
        if (em.ValidateIdInRange(entityId))
        {
            if (em.ValidateIdInUse(entityId))
            {
                cm.RemoveComponent<T>(entityId);
            }
            else
            {
                throw new InvalidOperationException($"EntityId {entityId} is not currently active");
            }
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(entityId));
        }
    }

    public IEnumerable<int> GetEntitiesWith<T1, T2>() where T1 : struct where T2 : struct
    {
        return cm.GetEntitiesWith<T1, T2>();
    }
}
