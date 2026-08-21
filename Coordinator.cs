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

    public int CreateEntity(){
        em.AddEntity();



        return 0;
    }
}
