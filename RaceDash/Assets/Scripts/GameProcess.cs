using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : MonoBehaviour {
    [SerializeField]
    private GUIManager m_gui = null;

    [SerializeField]
    private Character m_character = null;

    [SerializeField]
    private WorldManager m_worldManager = null;

    private void Awake()
    {
        m_gui.RegisterControlWatcher(OnControlActivated);
    }

    private void OnControlActivated(int index)
    {
        m_character.CurrentLane = (Character.Lane)index;

        m_worldManager.StartSpeedUpAnimation();// for TestPurpose
    }
}
