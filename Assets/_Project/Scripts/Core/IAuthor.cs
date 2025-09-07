public interface IAuthor
{
    string GetLogLine(HeroSnapshot before, HeroIntent intent, ConsequenceResult result);
}
