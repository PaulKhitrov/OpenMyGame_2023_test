using App.Scripts.Infrastructure.LevelParsingModule.Models;

namespace App.Scripts.Infrastructure.LevelParsingModule.Parsers
{
    public interface IFillWordLevelDataParser
    {
        FillWordLevelModel GetFillWordlevel(int levelIndex);
    }
}