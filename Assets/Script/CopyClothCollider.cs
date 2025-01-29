using System.Collections.Generic;
using MagicaCloth2;
using UnityEngine;
using VInspector;

public class CopyClothCollider : MonoBehaviour
{
    public Transform fromRoot;
    public Transform toRoot;

    [Button]
    public void Copy()
    {
        if(fromRoot== null)
            return;
        if(toRoot == null)
            return;
        List<ColliderComponent> colliders = new();
        colliders.AddRange(fromRoot.GetComponentsInChildren<ColliderComponent>());
        List<Transform> toBones = new();
        toBones.AddRange(toRoot.GetComponentsInChildren<Transform>());
        foreach (var colliderComponent in colliders)
        {
            var parentName = colliderComponent.transform.parent.name;
            var pos = colliderComponent.transform.position;
            var ro = colliderComponent.transform.rotation;
            var parent = toBones.Find(x => x.name == parentName);
            if(parent == null)
                continue;
            var newCollider = Instantiate(colliderComponent,parent,false);
            newCollider.transform.position = pos;
            newCollider.transform.rotation = ro;
        }
        
    }
}
