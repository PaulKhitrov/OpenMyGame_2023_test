using System.IO;

namespace App.Scripts.Infrastructure.LevelParsingModule.Controllers.IO
{
    public class TxtFileIoMethode : ITxtFileIoMethod
    {
        private readonly string _fileName;

        public TxtFileIoMethode(string fileName)
        {
            _fileName = fileName;
        }

        public string[] Load()
        {
            return File.ReadAllLines(_fileName);
        }
    }
}
