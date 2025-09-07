using UnityEngine;

public class RuleBasedAuthor : MonoBehaviour, IAuthor
{
    public string GetLogLine(HeroSnapshot before, HeroIntent intent, ConsequenceResult result)
    {
        switch (intent.Type)
        {
            case IntentType.Rest:   return "Descansa y se recompone.";
            case IntentType.Attack: return "Ataca y aprende algo.";
            case IntentType.Loot:   return "Encuentra un pequeño botín.";
            default:                return "Sigue su camino…";
        }
    }
}
