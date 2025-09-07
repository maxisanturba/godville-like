using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class UILog : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;
    [SerializeField] private int maxLines = 10;

    private readonly Queue<string> lines = new Queue<string>();

    public void Append(string line)
    {
        lines.Enqueue(line);

        if (lines.Count > maxLines) lines.Dequeue();

        // logText.text = string.Join("\n", lines);

        var sb = new StringBuilder();
        foreach (var l in lines) sb.AppendLine(l);

        logText.text = sb.ToString();
    }
    [ContextMenu("Test Log")]
    private void TestLog()
    {
        Append("El héroe comienza su aventura");
        Append("Camina hacia la salida del pueblo");
        Append("Al salir se ha encontrado con un conejo endiablado");
    }
}
