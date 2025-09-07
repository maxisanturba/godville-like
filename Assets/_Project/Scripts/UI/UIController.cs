using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text textHP, textLevel, textXP, textGold;

    public void Refresh(Hero h)
    {
        if (h == null) return;

        textHP.text = $"Salud: {h.HP}/{h.HPMax}";
        textLevel.text = $"Nivel: {h.Level}";
        textXP.text = $"Experiencia: {h.XP}/{h.XpThreshold}";
        textGold.text = $"Oro: {h.Gold}";
    }
}
