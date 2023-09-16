using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Infrastructure.LevelParsingModule.Models
{
    public class FillWordLevelModel
    {
        private readonly int _levelIndex;
        private readonly List<char> _letters;
        private readonly Vector2Int _sizeGrid;

        public FillWordLevelModel(int levelIndex, Vector2Int sizeGrid, List<char> lettersForGrid)
        {
            _levelIndex = levelIndex;
            _letters = new List<char>(lettersForGrid);
            _sizeGrid = new Vector2Int(sizeGrid.x, sizeGrid.y);
        }

        public int LevelIndex => _levelIndex;
        public IEnumerable<char> Letters => _letters.AsReadOnly();
        public Vector2Int SizeGrid => _sizeGrid;
    }
}