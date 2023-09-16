using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using System.Collections.Generic;
using App.Scripts.Infrastructure.LevelParsingModule.Entry;
using App.Scripts.Infrastructure.LevelParsingModule.Parsers;
using App.Scripts.Infrastructure.LevelParsingModule.Models;


namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class ProviderFillwordLevel : IProviderFillwordLevel
    {


        public GridFillWords LoadModel(int index)
        {
            //напиши реализацию не меняя сигнатуру функции

            var filldWordParser = ParsingModuleEntry.CreateParser(new FillWordLevelDataParser());
            FillWordLevelModel level = filldWordParser.GetLevel(index) as FillWordLevelModel;

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