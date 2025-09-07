using UnityEngine;

public class RandomIntentResolver : MonoBehaviour, IIntentResolver
{
    [Header("Refs")]
    [SerializeField] private MonoBehaviour rngBehaviour;
    private IRNG rng;

    [Header("Umbrales")]
    [Range(0f, 1f)] public float lowHpThreshold = 0.30f;

    [Header("Pesos en ZONA SEGURA")]
    [Range(0f, 1f)] public float safe_Attack = 0.50f;
    [Range(0f, 1f)] public float safe_Loot = 0.30f;
    [Range(0f, 1f)] public float safe_Rest = 0.20f;

    [Header("Pesos con HP BAJO (≤ umbral)")]
    [Range(0f, 1f)] public float low_Attack = 0.10f;
    [Range(0f, 1f)] public float low_Loot = 0.10f;
    [Range(0f, 1f)] public float low_Rest = 0.80f;


    private void Awake()
    {
        rng = rngBehaviour as IRNG;
        if (rng == null)
        {
            Debug.LogError("[RandomIntentResolver] rngBehaviour NO implementa IRNG.");
        }
    }

    public HeroIntent GetNextIntent(Hero hero)
    {
        if (hero == null)
        {
            Debug.LogWarning("[RandomIntentResolver] Hero es null; devolviendo Rest por seguridad.");
            return new HeroIntent(IntentType.Rest);
        }

        float ratio = (hero.HPMax > 0) ? (hero.HP / (float)hero.HPMax) : 0f;
        bool lowHp = ratio <= lowHpThreshold;

        // Elegir pesos segun estado
        float wAttack = lowHp ? low_Attack : safe_Attack;
        float wLoot = lowHp ? low_Loot : safe_Loot;
        float wRest = lowHp ? low_Rest : safe_Rest;

        // Normalizar por si los sliders no suman 1
        float sum = wAttack + wLoot + wRest;
        if (sum <= 0f)
        {
            Debug.LogWarning("[RandomIntentResolver] Suma de pesos = 0. Forzando Rest.");
            return new HeroIntent(IntentType.Rest);
        }
        wAttack /= sum; wLoot /= sum; wRest /= sum;

        //tirar dado [0, 1]
        float r = (rng != null) ? rng.NextFloat() : Random.value;

        //Intervalos acumulados
        float cutAttack = wAttack;
        float cutLoot = wAttack + wLoot;

        if (r < cutAttack) return new HeroIntent(IntentType.Attack);
        else if (r < cutLoot) return new HeroIntent(IntentType.Loot);
        else return new HeroIntent(IntentType.Rest);
    }
}
