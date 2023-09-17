using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;

namespace App.Scripts.Infrastructure.LevelParsingModule.Models
{
    public class WordSearchParserLevelModel
    {
        private readonly int _levelIndex;
        private readonly LevelInfo _levelInfo;

        public WordSearchParserLevelModel(int levelIndex, LevelInfo levelInfo)
        {
            _levelIndex = levelIndex;
            _levelInfo = levelInfo;
        }

        public int LevelIndex => _levelIndex;
        public LevelInfo LevelInfo => _levelInfo;
    }
}