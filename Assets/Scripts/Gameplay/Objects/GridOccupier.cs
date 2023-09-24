using Gameplay.Management;
using UnityEngine;

namespace Gameplay.Objects
{
    public class GridOccupier : MonoBehaviour
    {
        private GridCell connectedGridCell;

        public void ConnectGridCell(GridCell cell)
        {
            connectedGridCell = cell;
        }

        public void FreeGridCell()
        {
            connectedGridCell.FreeCell();
            connectedGridCell = null;
        }
    }
}
