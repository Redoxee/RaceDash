using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
    [SerializeField]
    Camera m_gameCamera = null;
    
    [SerializeField]
    private float m_speed = 400;
    private float m_baseSpeed;
    [SerializeField]
    private float m_fullSpeed = 800;
    private float m_baseFieldOfView;
    [SerializeField]
    private float m_fullSpeedFieldOfView = .5f; // gee I don't know
    [SerializeField]
    private float m_cameraShakeAmplitude = 1f;
    [SerializeField]
    private float m_cameraShakeSpeed = 10;
    [SerializeField]
    private AnimationCurve m_speedUpAnimation = AnimationCurve.Linear(0, 0, 0, 0);
    private float m_animationTimer = -1;



    private void Start()
    {
        m_baseSpeed = m_speed;
        m_baseFieldOfView = m_gameCamera.fieldOfView;
    }

    void LateUpdate () {
        transform.position += m_speed * Vector3.forward * Time.deltaTime;
	}

    public void StartSpeedUpAnimation()
    {
        m_animationTimer = 0;
        ComputeSpeedUpAnimation(m_animationTimer);
    }

    private void ComputeSpeedUpAnimation(float timer)
    {
        float currentValue = m_speedUpAnimation.Evaluate(m_animationTimer);

        m_gameCamera.fieldOfView = Mathf.Lerp(m_baseFieldOfView, m_fullSpeedFieldOfView, currentValue);
        m_speed = Mathf.Lerp(m_baseSpeed, m_fullSpeed, currentValue);

        float shakeTimer = m_cameraShakeSpeed * timer;
        Vector3 shakeDecal = new Vector3(Mathf.Sin(shakeTimer), Mathf.Cos(shakeTimer * 4), 0) * currentValue * m_cameraShakeAmplitude;
        m_gameCamera.transform.localPosition = shakeDecal;

    }

    private void Update()
    {
        if (m_animationTimer >= 0)
        {
            m_animationTimer += Time.deltaTime;
            float duration = m_speedUpAnimation.keys[m_speedUpAnimation.length - 1].time;
            if (m_animationTimer < duration)
            {
                ComputeSpeedUpAnimation(m_animationTimer);
            }
            else
            {
                m_animationTimer = -1;
                ComputeSpeedUpAnimation(0);
            }
        }
    }

}
