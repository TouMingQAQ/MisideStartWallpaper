using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class SkinBoneEditor : MonoBehaviour
{
    public SkinnedMeshRenderer skin;
    public Transform root;
    [ReadOnly]
    public Transform fromRootBone;
    [ReadOnly]
    public Transform toRootBone;
    public List<Transform> fromBones;
    public List<Transform> copyBones;
    [Button]
    public void Bake()
    {
        CollectBones();
        CollectBonesFromCopyRoot();
        BackBones();
    }

    public void CollectBones()
    {
        fromRootBone = skin.rootBone;
        fromBones.Clear();
        fromBones.AddRange(skin.bones);
    }
    public void BackBones()
    {
        List<Transform> bones = new List<Transform>();
        toRootBone = copyBones.Find(x => x.name == fromRootBone.name);
        foreach (var bone in fromBones)
        {
           var findBone =  copyBones.Find(x => x.name == bone.name);
           bones.Add(findBone);
        }
        copyBones.Clear();
        copyBones.AddRange(bones.ToArray());
        
        skin.bones = copyBones.ToArray();
        skin.rootBone = toRootBone;
    }
    public void CollectBonesFromCopyRoot()
    {
        if(root == null)
            return;
        copyBones.Clear();
        var t = root.GetComponentsInChildren<Transform>();
        copyBones.AddRange(t);
    }
}
