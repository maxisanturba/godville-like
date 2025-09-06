using UnityEngine;
using TMPro;
using System.Text;

public class UILog : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;
    [SerializeField] private int maxLines = 50;

    private readonly StringBuilder sb = new StringBuilder();
    private int lineCount = 0;

    public void Clear()
    {
        sb.Clear();
        lineCount = 0;
        if (logText) logText.text = "";
    }

    public void Append(string line)
    {
        if (string.IsNullOrEmpty(line)) return;

        sb.AppendLine(line);
        lineCount++;

        // recorte simple (no ultra eficiente, suficiente para MVP)
        while (lineCount > maxLines)
        {
            // elimina la primera l�nea
            int firstNewLine = sb.ToString().IndexOf('\n');
            if (firstNewLine >= 0) sb.Remove(0, firstNewLine + 1);
            lineCount--;
        }

        if (logText) logText.text = sb.ToString();
    }

    [ContextMenu("Log Test: a�adir 3 l�neas")]
    public void ContextMenu_Test()
    {
        Append("El h�roe mira el horizonte�");
        Append("Encuentra una moneda pegajosa (+1 oro).");
        Append("Piensa en una siesta �pica.");
    }
}
