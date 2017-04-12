using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// ========================================================
    // 描 述：简单的mesh合并，将子类下的所有网格合并
    // 作 者：ZhangShouYang
    // 创建时间：#CreateTime#
    // 版 本：v 1.0
// ========================================================

namespace Hstj{
    [RequireComponent (typeof(MeshRenderer))]
    [RequireComponent (typeof(MeshFilter))]

    public class Simple_MeshCombine : MonoBehaviour {
	
        //// Use this for initialization
        //void Start () {
	    
        //}
	
        //// Update is called once per frame
        //void Update () {
	
        //}

        [ContextMenu("网格合并")]
        public void MeshCombine_Test()
        {
            MeshRenderer[] meshRenders = transform.GetComponentsInChildren<MeshRenderer>();
            //Material[] materials = new Material[meshRenders.Length];
            List<Material> materialList = new List<Material>();

            for (int i = 0; i < meshRenders.Length; ++i)
            {
                if (meshRenders[i].sharedMaterial != null)
                {
                    if (!materialList.Contains(meshRenders[i].sharedMaterial))
                        materialList.Add(meshRenders[i].sharedMaterial);
                }
            }

            // 获取mesh
            MeshFilter[] meshFilters = transform.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combineInstances = new CombineInstance[meshFilters.Length];

            for (int j = 0; j < meshFilters.Length; ++j)
            {
                combineInstances[j].mesh = meshFilters[j].sharedMesh;
                // 模型空间坐标转化为世界坐标
                combineInstances[j].transform = meshFilters[j].transform.localToWorldMatrix;
                
                // collider
                BoxCollider collider = meshFilters[j].gameObject.GetComponent<BoxCollider>();
                if (collider)
                {
                    BoxCollider newCollider = gameObject.AddComponent(collider.GetType()) as BoxCollider;
                    if (newCollider != null)
                    {
                        newCollider.isTrigger = collider.isTrigger;
                        newCollider.center = collider.transform.localPosition;
                        newCollider.size = new Vector3(0.6f, 0.6f, 0.8f);
                    }
                }
                meshFilters[j].gameObject.SetActive(false);
            }
            
            // 合并材质
            transform.GetComponent<MeshRenderer>().sharedMaterials = materialList.ToArray() ;

            // 合并网格
            transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combineInstances, true);


            transform.gameObject.SetActive(true);
        }

        [ContextMenu ("去掉包围盒")]
        public void removeBosCollider()
        {
            Collider[] colliders = transform.GetComponents<Collider>();
            for (int i= 0; i<colliders.Length; ++i)
            {
                GameObject.DestroyImmediate(colliders[i]);
            }
        }

    }
}

