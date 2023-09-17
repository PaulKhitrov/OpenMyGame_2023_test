using System.Collections.Generic;
using System.Linq;
using App.Scripts.Libs.Factory;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel
{
    public class FactoryLevelModel : IFactory<LevelModel, LevelInfo, int>
    {
        public LevelModel Create(LevelInfo value, int levelNumber)
        {
            var model = new LevelModel();

            model.LevelNumber = levelNumber;

            model.Words = value.words;
            model.InputChars = BuildListChars(value.words);

            return model;
        }

        private List<char> BuildListChars(List<string> words)
        {
            //напиши реализацию не меняя сигнатуру функции
            List<char> allLetters = new List<char>();
            Dictionary<int, List<char>> wordsForLetters = new Dictionary<int, List<char>>();
            List<char> charsForButtons = new List<char>();


            foreach (var item in words)
            {
                allLetters = allLetters.Concat(item.ToCharArray()).ToList();
            }

            allLetters = allLetters.Distinct().ToList();

            int key = 0;
            foreach (var item in words)
            {
                wordsForLetters.Add(key, item.ToCharArray().ToList());
                key++;
            }

            int currentCount = 0;
            int maxCount = 0;
            char currentChar;
            Dictionary<int, char> numberOfLetterButtons = new Dictionary<int, char>();
            List<int> maxLetterCount = new List<int>();

            foreach (var letterForCheck in allLetters)
            {
                foreach (var word in wordsForLetters.Values)
                {
                    currentCount = word.Where(x => x.Equals(letterForCheck)).Count();
                    maxLetterCount.Add(currentCount);
                    currentCount = 0;
                }
                maxCount = maxLetterCount.Max();
                currentChar = letterForCheck;

                for (int i = 1; i <= maxCount; i++)
                {
                    charsForButtons.Add(currentChar);
                }
                maxCount = 0;
                maxLetterCount.Clear();
            }
            return charsForButtons;
        }
    }
}