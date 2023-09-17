using UnityEngine;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using System.Collections.Generic;
using System.IO;

namespace App.Scripts.Infrastructure.LevelParsingModule.Controllers.IO
{
    public class JsonFileIoMethod : IJsonFileIoMethod
    {
        private readonly string _fileName;

        public JsonFileIoMethod(string fileName)
        {
            _fileName = fileName;
        }

        public string Load()
        {
            return File.ReadAllText(_fileName); 
        }
    }
}