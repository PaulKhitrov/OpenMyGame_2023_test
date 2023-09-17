using App.Scripts.Infrastructure.LevelParsingModule.Controllers.IO;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using App.Scripts.Infrastructure.LevelParsingModule.Managers;
using App.Scripts.Infrastructure.LevelParsingModule.Models;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

namespace App.Scripts.Infrastructure.LevelParsingModule.Parsers
{
    public class WordSearchLevelDataParser : IWordSearchLevelDataParser, IParsingLevelStrategy
    {
        private List<LevelInfo> _LevelsInfoDataList = new List<LevelInfo>();
        private List<WordSearchParserLevelModel> _wordSearchLevelModelslist = new List<WordSearchParserLevelModel>();

        public WordSearchLevelDataParser()
        {
            InitParser();
        }

        private void InitParser()
        {
            string dir = Path.Combine(Application.dataPath, "App", "Resources", "WordSearch", "Levels");
            string[] jsonLevelFilesPath = Directory.GetFiles(dir, "*.json");

            List<string> jsonStringslist = new List<string>(GetJsonStringsList(jsonLevelFilesPath));

            _LevelsInfoDataList = GetLevelInfoList(jsonStringslist);

            _wordSearchLevelModelslist = GetWordSearchLevelModelsList(_LevelsInfoDataList);
        }

        private List<string> GetJsonStringsList(string[] jsonLevelFilesPath)
        {
            List<string> jsonStringList = new List<string>();
            for (int i = 0; i < jsonLevelFilesPath.Length; i++)
            {
                jsonStringList.Add(LoadDataFromJsonFile(jsonLevelFilesPath[i]));
            }
            return jsonStringList;
        }

        private List<LevelInfo> GetLevelInfoList(List<string> jsonStrings)
        {
            List<LevelInfo> levelInfos = new List<LevelInfo>();
            foreach (var item in jsonStrings)
            {
                levelInfos.Add(ParseJsonToLevelInfo(item));
            }
            return levelInfos;
        }

        private List<WordSearchParserLevelModel> GetWordSearchLevelModelsList(List<LevelInfo> levelsInfoDataList)
        {
            List<WordSearchParserLevelModel> levelModels = new List<WordSearchParserLevelModel>();
            int levelIndex = 1;

            foreach (var item in levelsInfoDataList)
            {
                levelModels.Add(new WordSearchParserLevelModel(levelIndex, item));
                levelIndex++;
            }
            return levelModels;
        }

        private string LoadDataFromJsonFile(string filePath)
        {
            JsonFileIoMethod jsonFileIoMethod = new JsonFileIoMethod(filePath);
            return jsonFileIoMethod.Load();
        }

        private LevelInfo ParseJsonToLevelInfo(string jsonString)
        {
            LevelInfo levelInfo = JsonUtility.FromJson<LevelInfo>(jsonString);
            return levelInfo;
        }

        public object GetLevelModel(int levelIndex)
        {
            if (_wordSearchLevelModelslist.Exists(lvl => lvl.LevelIndex == levelIndex))
            {
                return _wordSearchLevelModelslist.Find(lvl => lvl.LevelIndex == levelIndex);
            }
            else
            {
                throw new ArgumentNullException("A level with this index does not exist!");
            }
        }

        public WordSearchParserLevelModel GetWordSearchParserLevelModel(int levelIndex)
        {
            return GetLevelModel(levelIndex) as WordSearchParserLevelModel;
        }
    }
}