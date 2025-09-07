using UnityEngine;

public class SimpleConsequenceRule : MonoBehaviour, IConsequenceRule
{
    public ConsequenceResult Apply(HeroIntent intent, Hero hero)
    {
        switch (intent.Type)
        {
            case IntentType.Rest:
                // +2 HP (cap en HPMax)
                return new ConsequenceResult { DeltaHP = +2, DeltaXP = 0, DeltaGold = 0 };
            case IntentType.Attack:
                // -1 HP, +1 XP
                return new ConsequenceResult { DeltaHP = -1, DeltaXP = +1, DeltaGold = 0 };
            case IntentType.Loot:
                // +1 Gold
                return new ConsequenceResult { DeltaHP = 0, DeltaXP = 0, DeltaGold = +1 };
            default:
                return new ConsequenceResult();
        }
    }
}
