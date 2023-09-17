using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using System.Collections.Generic;
using App.Scripts.Infrastructure.LevelParsingModule.Entry;
using App.Scripts.Infrastructure.LevelParsingModule.Parsers;
using App.Scripts.Infrastructure.LevelParsingModule.Models;
using App.Scripts.Infrastructure.LevelParsingModule.Managers;


namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class ProviderFillwordLevel : IProviderFillwordLevel
    {
        private IParserManager _fillWordLevelDataParser;

        public ProviderFillwordLevel()
        {
            _fillWordLevelDataParser = InitParser();
        }

        private IParserManager InitParser()
        {
            return ParsingModuleEntry.CreateParser(new FillWordLevelDataParser());
        }

        public GridFillWords LoadModel(int index)
        {
            //напиши реализацию не меняя сигнатуру функции

            FillWordParserLevelModel level = _fillWordLevelDataParser.GetLevel(index) as FillWordParserLevelModel;

            List<CharGridModel> charGridletters = new List<CharGridModel>();
            foreach (var letter in level.Letters)
            {
                charGridletters.Add(new CharGridModel(letter));
            }

            GridFillWords Grid = new GridFillWords(level.SizeGrid);
            int listItem = 0;
            for (int i = 0; i < Grid.Size.x; i++)
            {
                for (int j = 0; j < Grid.Size.y; j++)
                {
                    Grid.Set(i, j, charGridletters[listItem]);
                    listItem++;
                }
            }
            return Grid;
        }
    }
}