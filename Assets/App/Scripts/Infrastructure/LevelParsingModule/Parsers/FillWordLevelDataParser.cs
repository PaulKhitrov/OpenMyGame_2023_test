using App.Scripts.Infrastructure.LevelParsingModule.Managers;
using App.Scripts.Infrastructure.LevelParsingModule.Models;
using App.Scripts.Infrastructure.LevelParsingModule.Controllers.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using System.IO;

namespace App.Scripts.Infrastructure.LevelParsingModule.Parsers
{
    public class FillWordLevelDataParser : IFillWordLevelDataParser, IParsingLevelStrategy
    {
        private Dictionary<int, List<int>> _levels_WordsNumber_Dictionary = new Dictionary<int, List<int>>();
        private Dictionary<int, List<char>> _wordsNumber_letters_Dictionary = new Dictionary<int, List<char>>();
        private Dictionary<int, List<int>> _wordsNumber_LettersPosition_Dict = new Dictionary<int, List<int>>();
        private List<FillWordLevelModel> _levelModelsList;

        public FillWordLevelDataParser()
        {
            InitParser();
        }

        public void InitParser()
        {
            PrepareLoadedData();
            _levelModelsList = new List<FillWordLevelModel>(GetLevelModelsList());
            _levelModelsList = ValidateLevelModelsList(_levelModelsList);
        }

        private void PrepareLoadedData()
        {
            Dictionary<int, List<string>> LevelWordsNumberLoadDict = new Dictionary<int, List<string>>();
            Dictionary<int, List<string>> LevelWordsLettersPositionLoadDict = new Dictionary<int, List<string>>();
            Dictionary<int, string> wordsLoadDictionary = new Dictionary<int, string>();

            char s = Path.DirectorySeparatorChar;
            string levelsFile = Application.dataPath + s + "App" + s + "Resources" + s + "Fillwords" + s + "pack_0.txt";
            string wordsFile = Application.dataPath + s + "App" + s + "Resources" + s + "Fillwords" + s + "words_list.txt";

            string[] lvlloadData = LoadDataFromFille(levelsFile);
            string[] wordsLoadData = LoadDataFromFille(wordsFile);

            lvlloadData = lvlloadData.Distinct().ToArray(); //удаляем повторяющиеся строки

            for (int i = 0; i < lvlloadData.Length; i++)
            {
                LevelWordsNumberLoadDict.Add(i + 1, new List<string>(lvlloadData[i].Split(' ')));
                LevelWordsLettersPositionLoadDict.Add(i + 1, new List<string>(lvlloadData[i].Split(' ')));
            }

            for (int i = 0; i < wordsLoadData.Length; i++)
            {
                wordsLoadDictionary.Add(i, wordsLoadData[i]);
            }

            ParseLevelsWordsNumber(LevelWordsNumberLoadDict);
            ParseLevelsWordsLettersPositions(LevelWordsLettersPositionLoadDict);
            ParseWordsNumbersLettersPosition(LevelWordsNumberLoadDict, LevelWordsLettersPositionLoadDict);
            ParseWordsByLetters(wordsLoadDictionary);
        }

        private string[] LoadDataFromFille(string filleName)
        {
            TxtFileIoMethode txtFileLoader = new TxtFileIoMethode(filleName);
            return txtFileLoader.Load();
        }

        private void ParseLevelsWordsNumber(Dictionary<int, List<string>> LevelWordsNumberLoadDict)
        {
            foreach (var lvl in LevelWordsNumberLoadDict.Values)
            {
                for (int i = lvl.Count - 1; i >= 0; i--)
                {
                    if (lvl[i].Contains(";") == true)
                    {
                        lvl.Remove(lvl[i]);
                    }
                }
            }

            foreach (var lvl in LevelWordsNumberLoadDict)
            {
                _levels_WordsNumber_Dictionary.Add(lvl.Key, new List<int>(lvl.Value.ConvertAll(int.Parse)));
            }
        }

        private void ParseLevelsWordsLettersPositions(Dictionary<int, List<string>> LevelWordsLettersPositionLoadDict)
        {
            foreach (var lvl in LevelWordsLettersPositionLoadDict.Values)
            {
                for (int i = lvl.Count - 1; i >= 0; i--)
                {
                    if (lvl[i].Contains(";") == false)
                    {
                        lvl.Remove(lvl[i]);
                    }
                }
            }
        }

        private void ParseWordsNumbersLettersPosition(Dictionary<int, List<string>> LevelWordsNumberLoadDict, Dictionary<int, List<string>> LevelWordsLettersPositionLoadDict)
        {
            List<int> wordNumbers = new List<int>();
            foreach (var lvl in LevelWordsNumberLoadDict.Values)
            {
                foreach (var item in lvl)
                {
                    wordNumbers.Add(Int32.Parse(item));
                }
            }

            List<string> wordLettersPos = new List<string>();
            foreach (var lvl in LevelWordsLettersPositionLoadDict.Values)
            {
                foreach (var item in lvl)
                {
                    wordLettersPos.Add(item);
                }
            }

            wordNumbers = wordNumbers.Union(wordNumbers).ToList();
            wordLettersPos = wordLettersPos.Union(wordLettersPos).ToList();

            for (int i = 0; i < wordNumbers.Count; i++)
            {
                _wordsNumber_LettersPosition_Dict.Add(wordNumbers[i], new List<int>(Array.ConvertAll(wordLettersPos[i].Split(';'), int.Parse)));
            }
        }

        private void ParseWordsByLetters(Dictionary<int, string> wordsLoadDictionary)
        {
            foreach (var item in wordsLoadDictionary)
            {
                _wordsNumber_letters_Dictionary.Add(item.Key, new List<char>(item.Value.ToCharArray()));
            }
        }

        private FillWordLevelModel GenerateLevelModel(int levelIndex)
        {
            List<int> wordsNum = new List<int>(_levels_WordsNumber_Dictionary[levelIndex]);
            List<int> positionsList = new List<int>();
            List<char> lettersList = new List<char>();
            List<char> levelGridLettersList = new List<char>();

            for (int i = 0; i < wordsNum.Count; i++)
            {
                positionsList = positionsList.Concat(_wordsNumber_LettersPosition_Dict[wordsNum[i]]).ToList();
                lettersList = lettersList.Concat(_wordsNumber_letters_Dictionary[wordsNum[i]]).ToList();
            }

            foreach (var position in positionsList)
            {
                levelGridLettersList.Add(lettersList.ElementAt(position));
            }

            int sizeGrid = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(levelGridLettersList.Count)));

            return new FillWordLevelModel(levelIndex, new Vector2Int(sizeGrid, sizeGrid), levelGridLettersList);
        }

        private List<FillWordLevelModel> GetLevelModelsList()
        {
            List<FillWordLevelModel> levelsList = new List<FillWordLevelModel>();

            foreach (var key in _levels_WordsNumber_Dictionary.Keys)
            {
                levelsList.Add(GenerateLevelModel(key));
            }

            return levelsList;
        }

        private List<FillWordLevelModel> ValidateLevelModelsList(List<FillWordLevelModel> levelModelsList)
        {
            List<FillWordLevelModel> newlevelModelsList = new List<FillWordLevelModel>();

            for (int i = levelModelsList.Count - 1; i >= 0; i--)
            {
                if (levelModelsList[i].Letters.Count() != levelModelsList[i].SizeGrid.x * levelModelsList[i].SizeGrid.y)
                {
                    levelModelsList.Remove(levelModelsList[i]);
                }
            }

            int newlvlIndex = 1;
            foreach (var item in levelModelsList)
            {
                newlevelModelsList.Add(new FillWordLevelModel(newlvlIndex, item.SizeGrid, item.Letters.ToList()));
                newlvlIndex++;
            }

            return newlevelModelsList;
        }

        public FillWordLevelModel GetFillWordlevel(int levelIndex)
        {
            if (_levelModelsList.Exists(lvl => lvl.LevelIndex == levelIndex))
            {
                return _levelModelsList.Find(lvl => lvl.LevelIndex == levelIndex);
            }
            else
            {
                throw new ArgumentNullException("A level with this index does not exist!");
            }
        }

        public object GetLevelModel(int levelIndex)
        {
            return GetFillWordlevel(levelIndex);
        }
    }
}