using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabRepeater : MonoBehaviour {
    [SerializeField]
    private GameObject m_prefab = null;

    [SerializeField]
    private Transform m_poolParent = null;

    [SerializeField]
    private int m_poolSize = 50;

    [SerializeField]
    private Vector3 m_gap = Vector3.forward;

    private Transform[] m_pool;
    private int m_lastCheckedIndex;

    void Start () {
        m_pool = new Transform[m_poolSize];

        for (int i = 0; i < m_poolSize; ++i)
        {
            var instance = Instantiate(m_prefab, m_poolParent);
            instance.transform.position = m_gap * i;
            m_pool[i] = instance.transform;
        }
        m_lastCheckedIndex = 0;
	}
	

	void Update () {
        for (int i = 0; i < m_poolSize; ++i)
        {
            int transformIndex = (m_lastCheckedIndex + i) % m_poolSize;
            Transform cp = m_pool[transformIndex];
            if (cp.position.z < transform.position.z)
            {
                var prevIndex = (transformIndex - 1);
                if (prevIndex < 0)
                    prevIndex = m_poolSize + prevIndex;
                cp.position = m_pool[prevIndex].position + m_gap;
            }
            else
            {
                m_lastCheckedIndex = transformIndex;
                break;
            }
        }
	}
}
