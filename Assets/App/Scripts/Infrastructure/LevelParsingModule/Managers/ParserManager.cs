namespace App.Scripts.Infrastructure.LevelParsingModule.Managers
{
    public class ParserManager : IParserManager
    {
        private IParsingLevelStrategy _parsingStrategy;

        public object GetLevel(int levelIndex)
        {
            return _parsingStrategy.GetLevelModel(levelIndex);
        }

        public void SetParsingStrategy(IParsingLevelStrategy parsingStrategy)
        {
            _parsingStrategy = parsingStrategy;
        }
    }
}