using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using App.Scripts.Infrastructure.LevelParsingModule.Entry;
using App.Scripts.Infrastructure.LevelParsingModule.Parsers;
using App.Scripts.Infrastructure.LevelParsingModule.Managers;
using App.Scripts.Infrastructure.LevelParsingModule.Models;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel.ProviderWordLevel
{
    public class ProviderWordLevel : IProviderWordLevel
    {
        private IParserManager _parserManager;

        public ProviderWordLevel()
        {
            _parserManager = InitParsingModule();
        }

        private IParserManager InitParsingModule()
        {
            return ParsingModuleEntry.CreateParser(new WordSearchLevelDataParser());
        }

        public LevelInfo LoadLevelData(int levelIndex)
        {
            //напиши реализацию не меняя сигнатуру функции

             var wordSearchLevelModel = _parserManager.GetLevel(levelIndex) as WordSearchParserLevelModel;

             LevelInfo levelInfo = wordSearchLevelModel.LevelInfo;

            return levelInfo;
        }
    }
}