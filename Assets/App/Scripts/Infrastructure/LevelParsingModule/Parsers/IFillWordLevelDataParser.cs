using App.Scripts.Infrastructure.LevelParsingModule.Models;

namespace App.Scripts.Infrastructure.LevelParsingModule.Parsers
{
    public interface IFillWordLevelDataParser
    {
        FillWordParserLevelModel GetFillWordlevel(int levelIndex);
    }
}