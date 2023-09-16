using App.Scripts.Infrastructure.LevelParsingModule.Managers;

namespace App.Scripts.Infrastructure.LevelParsingModule.Entry
{
    public static class ParsingModuleEntry
    {
        public static IParserManager CreateParser(IParsingLevelStrategy parsingLevelStrategy)
        {
            var parseManager = new ParserManager();
            parseManager.SetParsingStrategy(parsingLevelStrategy);
            return parseManager;
        }
    }
}
