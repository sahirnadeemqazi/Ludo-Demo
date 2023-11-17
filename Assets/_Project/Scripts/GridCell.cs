using UnityEngine;

namespace _Project.Scripts
{
    public class GridCell : MonoBehaviour
    {
        // declaration of the available colors to select from
        public enum Color
        {
            Red,
            Blue,
            Green,
            Yellow
        }
        
        public bool isSafeCell;     // will be used to check the safe cell if there are opponents
        public PlayerChip thisCellPlayerChip;      // will be used to store the chips currently on this specific cell
        public bool isEntryCell;        // will be true if this cell is the entry cell to the win cells
        public bool isWinCell;      // will be true if it is the last cell
        public Color thisCellColor; // this will be used to verify the entry cell to the win cells
    }
}
