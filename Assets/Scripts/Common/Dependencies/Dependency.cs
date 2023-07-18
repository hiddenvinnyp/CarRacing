using UnityEngine;

public abstract class Dependency : MonoBehaviour
{
    protected virtual void BindAll(MonoBehaviour monoBehaviorInScene) { }
    protected void FindAllObjectsToBind()
    {
        MonoBehaviour[] monoInScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < monoInScene.Length; i++)
        {
            BindAll(monoInScene[i]);
        }
    }
    protected void Bind<T>(MonoBehaviour bindObject, MonoBehaviour target) where T : class 
    {
        if (target is IDependancy<T>) (target as IDependancy<T>).Construct(bindObject as T);
    }
}
