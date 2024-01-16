using UnityEngine;

public static class NameValidator
{
    public static bool HasUniqueName<T>(Component component) where T : Component
    {
        T[] objects = GameObject.FindObjectsOfType<T>();

        foreach (T obj in objects)
        {
            if (obj != component && obj.gameObject.name == component.gameObject.name)
            {
                return false;
            }
        }

        return true;
    }
}
