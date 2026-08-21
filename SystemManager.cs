namespace QWER;

public class SystemManager
{
    private List<ISystem> systems = new List<ISystem>();
    private Coordinator c;

    public SystemManager(Coordinator newCoordinator)
    {
        c = newCoordinator;
    }

    public void RegisterSystem(ISystem system)
    {
        systems.Add(system);
    }

    public void Update(float deltaTime)
    {
        foreach (ISystem system in systems)
        {
            system.Update(c, deltaTime);
        }
    }
}
