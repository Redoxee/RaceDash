using System;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public const int c_nbControls = 3;
    public Button[] Buttons = new Button[c_nbControls];

    private Action<int> m_controlWatcher = null;

    public void RegisterControlWatcher(Action<int> watcher)
    {
        m_controlWatcher = watcher;
    }

    public void OnControlPressed(int index)
    {
        m_controlWatcher(index);
    }


}
