using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Refs TMP (asignar en Inspector)")]
    [SerializeField] private TMP_Text textHP;
    [SerializeField] private TMP_Text textLevel;
    [SerializeField] private TMP_Text textXP;
    [SerializeField] private TMP_Text textGold;

    [Header("Modelo (para pruebas)")]
    [SerializeField] private Hero hero = new Hero();  // temporal

    /// <summary>Llamalo cuando cambien los valores del héroe.</summary>
    public void Refresh()
    {
        if (textHP) textHP.text = $"Salud: {hero.HP}/{hero.HPMax}";
        if (textLevel) textLevel.text = $"Nivel: {hero.Level}";
        if (textXP) textXP.text = $"Experiencia: {hero.XP}/{hero.XpThreshold}";
        if (textGold) textGold.text = $"Oro: {hero.Gold}";
    }

    /// <summary>Prueba manual: incrementa algunos valores y refresca.</summary>
    [ContextMenu("UI Test: +2 XP, +1 Oro")]
    private void ContextMenu_TestIncrement()
    {
        hero.XP += 2;
        hero.Gold += 1;
        if (hero.XP > hero.XpThreshold) hero.XP = hero.XpThreshold;
        Refresh();
    }

    // Para que veas algo al darle Play
    private void Start() => Refresh();
}
