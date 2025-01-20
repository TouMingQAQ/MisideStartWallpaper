#if UNITY_EDITOR
using System.Collections.Generic;
using MagicaCloth2;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using VInspector;

public class MisideEditor : MonoBehaviour
{
    
    public Transform root;
    [Tab("Cloth")]
    public List<MagicaCloth> clothList = new();
    [Tab("Collider")]
    public List<ColliderComponent> colliderList = new();

    [Button]    
    [Tab("Collider")]
    public void CollectColliders()
    {
        if(root == null)
            return;
        colliderList.Clear();
        var colliders = root.GetComponentsInChildren<ColliderComponent>();
        colliderList.AddRange(colliders);
        EditorUtility.SetDirty(this.gameObject);
    }
    [Button]  
    [Tab("Cloth")]
    public void CollectCloths()
    {
        if(root == null)
            return;
        clothList.Clear();
        var cloths = root.GetComponentsInChildren<MagicaCloth>();
        clothList.AddRange(cloths);
        EditorUtility.SetDirty(this.gameObject);
    }
    [Button]  
    [Tab("Cloth")]
    public void SetCollider()
    {
        foreach (var cloth in clothList)
        {
            cloth.SerializeData.colliderCollisionConstraint.colliderList.Clear();
            cloth.SerializeData.colliderCollisionConstraint.colliderList.AddRange(colliderList);
        }
        EditorUtility.SetDirty(this.gameObject);
    }
}
#endif