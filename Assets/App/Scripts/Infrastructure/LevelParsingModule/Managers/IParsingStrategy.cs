
namespace App.Scripts.Infrastructure.LevelParsingModule.Managers
{
    public interface IParsingLevelStrategy
    {
        object GetLevelModel(int levelIndex);
    }
}