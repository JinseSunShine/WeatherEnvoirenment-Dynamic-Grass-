using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// ========================================================
    // 描 述：处理草的受力情况，处理不同力的叠加效果
    // 实现原理：使用正玄波实现草的来回摆动，用指数衰减来模拟阻力
    // 作 者：ZhangShouYang
    // 创建时间：#CreateTime#
    // 版 本：v 1.0
// ========================================================

public class Force
{
    public float m_fTime = 0f;
    public Vector4 m_vForce;
    public float m_fMagnitude;
    public Vector4 m_vNormal;
    public Vector4 m_vPosition;
    public Force(Vector4 force, Vector4 position)
    {
        m_vForce = force;
        m_fMagnitude = m_vForce.magnitude;
        m_vNormal = m_vForce.normalized;
        m_vPosition = position;
    }
}

public class GrassForce : MonoBehaviour {

    // 受力列表
    public List<Force> m_ForceList = null;
    // 频率
    public float m_fWaveFrequency = 6.0f;
    // 阻力系数       
    public float m_fResistance = 0.3f;
    // 最大力大小
    public float m_fMaxForceMagnitude = 6.0f;
    // 受力时间间隔
    public float m_fAddForceTimeInterval = 0.5f;
    // 最大的受力个数
    public int m_nMaxForceNum = 3;
    
    public static AnimationCurve moveAnim = new AnimationCurve(new Keyframe(0,0,4,4),new Keyframe(0.25f,1,0,0),new Keyframe(0.5f,0,-4,-4),new Keyframe(0.75f,-1, 0,0),new Keyframe(1,0,4,4));
    
    // 最近添加力的时间
    private float m_fLastAddTime = 0f;
    private Material material;
    private bool m_bValiable = true;
    private float fInterval = 0f;
    private Vector3 direction = Vector3.zero;
    Vector4 accForce = Vector4.zero;
    Vector4 accPosition = Vector4.zero;
    Vector3 latestPos = Vector3.zero;

	// Use this for initialization
	void Start () {
        if (gameObject.renderer != null)
        {
            material = gameObject.renderer.material;
        }
        else
        {
            m_bValiable = false;
        }

        //StartCoroutine(DelayTest());
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_bValiable)
        {
            enabled = false;
            return;
        }
        UpdateForce();
	}

    void OnBecameVisible()
    {
        this.enabled = true;
    }
    void OnBecameInvisible()
    {
        this.enabled = false;
    }

    public void AddForce(Vector3 force, Vector3 position)
    {
        if (Time.time - m_fLastAddTime > m_fAddForceTimeInterval)
        {
            m_fLastAddTime = Time.time;

            if (m_ForceList == null)
                m_ForceList = new List<Force>();

            if (m_ForceList.Count < m_nMaxForceNum)
            {
                Vector4 newForce = new Vector4(force.x, force.y, force.z, 0);
                if (newForce.magnitude > m_fMaxForceMagnitude)
                {
                    newForce = newForce.normalized * m_fMaxForceMagnitude;
                }
                Vector4 newPos = new Vector4(position.x, position.y, position.z, 0);

                m_ForceList.Add(new Force(newForce, newPos));
                fInterval = 0;
            }
        }
    }

    private void UpdateForce()
    {
        if (m_ForceList == null || m_ForceList.Count <= 0)
        {
            fInterval = 0;
            return;
        }

        fInterval += Time.deltaTime;
        accForce = Vector4.zero;
        accPosition = Vector4.zero;

        for (int i = m_ForceList.Count - 1; i>= 0; --i)
        {
            if (m_ForceList[i].m_vForce.magnitude > 0.1f)
            {
                // [-1,1]正弦波模拟简谐运动
                float wave_factor = moveAnim.Evaluate(fInterval % 1);

                // 力的衰减
                m_ForceList[i].m_vForce = m_ForceList[i].m_vNormal * (m_ForceList[i].m_fMagnitude - m_fResistance * fInterval);
                m_ForceList[i].m_fTime += Time.deltaTime;

                // 累加
                accForce += m_ForceList[i].m_vForce * wave_factor;
                accPosition += m_ForceList[i].m_vPosition;
                accPosition *= 0.5f;
            }
            else
            {
                m_ForceList.RemoveAt(i);
            }
           
        }

        if (accForce != Vector4.zero)
        {
            if (material.HasProperty("_Force"))
            {
                // 世界坐标转到模型本地空间
                accForce = transform.InverseTransformVector(accForce);
                material.SetVector("_Force", accForce);
            }
            if (material.HasProperty("_position"))
            {
                // 世界坐标转到模型本地空间
                accPosition = transform.InverseTransformVector(accPosition);
                material.SetVector("_position", accPosition);
            }
        }
    }

    public float easeOutExpo(float start, float end, float value)
    {
        end -= start;
        return end * (Mathf.Pow(2, -10 * value) + 1) + start;
    }

    //void OnCollisionEnter(Collision collisionInfo)
    //{
    //    direction = collisionInfo.relativeVelocity;
    //    direction.y = 0;
    //    //AddForce(direction);
    //}
    //void OnCollisionExit(Collision collisionInfo)
    //{
    //    Debug.Log("OnCollisionExit " + collisionInfo.collider.transform.position);
    //}
    //void OnCollisionStay(Collision collisionInfo)
    //{
    //    Debug.Log("OnCollisionStay " + collisionInfo.collider.transform.position);
    //}
    void OnTriggerEnter(Collider other)
    {
        Transform otherTrans = other.transform;
        latestPos = otherTrans.position;

        StartCoroutine(checkAddForce(otherTrans));
    }
    IEnumerator checkAddForce(Transform trans)
    {
        yield return new WaitForSeconds(0.1f);
        if (trans == null || trans.gameObject == null)
            yield return null;

        direction = trans.position - latestPos;
        direction.y = 0;
        AddForce(direction *2, latestPos);
    }

    // Test Code
    IEnumerator DelayTest()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            Vector3 force = UnityEngine.Random.insideUnitSphere * 1;
            force.y = 0;
            AddForce(force, Vector3.zero);
        }
    }
}
