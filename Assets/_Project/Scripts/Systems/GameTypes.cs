public enum Intent { Attack, Rest, Loot }
public enum OutcomeGrade { Fail, Mixed, Success, Crit }

public struct EventResult
{
    public Intent Intent;
    public OutcomeGrade Grade;
    public int DeltaHP;
    public int DeltaXP;
    public int DeltaGold;
    public bool HeroDied;   // hoy no lo usamos, pero queda listo
}
