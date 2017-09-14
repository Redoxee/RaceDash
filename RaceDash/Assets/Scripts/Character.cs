using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public enum Lane { Left, Center, Right}


    [SerializeField]
    private Vector3 m_laneDecal = Vector3.zero;
    [SerializeField]
    private float m_laneSwitchDampFactor = .9f;

    private Lane m_currentLane = Lane.Center;
    private Vector3 m_currentDampedVelocity = Vector3.zero;

    private Vector3 m_currentTargetPosition;

    public Lane CurrentLane
    {
        get { return m_currentLane; }
        set
        {
            if (m_currentLane == value)
                return;
            m_currentLane = value;
            SelectTarget();
        }
    }

    private void SelectTarget()
    {
        m_currentTargetPosition = ((int)m_currentLane - 1f) * m_laneDecal;
    }

    private void Awake()
    {
        m_currentTargetPosition = transform.localPosition;
    }

    private void Update()
    {
        if((m_currentTargetPosition - transform.localPosition).sqrMagnitude > .001)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, m_currentTargetPosition, ref m_currentDampedVelocity, m_laneSwitchDampFactor);
        }
    }
}
