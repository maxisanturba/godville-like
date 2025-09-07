public interface IConsequenceRule
{
    ConsequenceResult Apply(HeroIntent intent, Hero hero);
}
