using UnityEngine;

namespace _Project.Scripts
{
    public class GridCell : MonoBehaviour
    {
        public enum Color
        {
            Red,
            Blue,
            Green,
            Yellow
        }
        
        public bool isSafeCell;
        public PlayerChip thisCellPlayerChip;
        public bool isEntryCell;
        public bool isWinCell;
        public Color thisCellColor;
    }
}
