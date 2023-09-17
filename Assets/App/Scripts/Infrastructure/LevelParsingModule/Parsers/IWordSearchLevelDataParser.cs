using App.Scripts.Infrastructure.LevelParsingModule.Models;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using System.Collections.Generic;

namespace App.Scripts.Infrastructure.LevelParsingModule.Parsers
{
    public interface IWordSearchLevelDataParser
    {
        WordSearchParserLevelModel GetWordSearchParserLevelModel(int levelIndex);
    }
}