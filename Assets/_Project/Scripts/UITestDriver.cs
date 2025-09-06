using UnityEngine;

public class UITestDriver : MonoBehaviour
{
    [SerializeField] private UIController ui;
    [SerializeField] private UILog log;

    private void Start()
    {
        if (log)
        {
            log.Clear();
            log.Append("Comienza la odisea de un héroe ligeramente confundido.");
            log.Append("Se acomoda la capa. Nadie lo mira, por suerte.");
        }
        if (ui) ui.Refresh();
    }
}
