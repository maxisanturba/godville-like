public enum IntentType { Attack, Rest, Loot }
public enum OutcomeGrade { Fail, Mixed, Success, Crit }

public struct HeroIntent
{
    public IntentType Type;
    public HeroIntent(IntentType type) => Type = type;
}

public struct ConsequenceResult
{
    public int DeltaHP;
    public int DeltaXP;
    public int DeltaGold;
}

public struct HeroSnapshot
{
    public int HP, HPMax, XP, Gold;
    public HeroSnapshot(Hero h) { HP = h.HP; HPMax = h.HPMax; XP = h.XP; Gold = h.Gold; }
}
