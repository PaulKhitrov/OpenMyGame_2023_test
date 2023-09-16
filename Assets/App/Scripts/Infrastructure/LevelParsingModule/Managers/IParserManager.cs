
namespace App.Scripts.Infrastructure.LevelParsingModule.Managers
{
    public interface IParserManager
    {
        void SetParsingStrategy(IParsingLevelStrategy parsingStrategy);
        object GetLevel(int levelIndex);
    }
}