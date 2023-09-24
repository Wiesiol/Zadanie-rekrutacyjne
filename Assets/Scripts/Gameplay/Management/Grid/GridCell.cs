using Gameplay.Objects;
using UnityEngine;

namespace Gameplay.Management
{
    public class GridCell
    {
        public Vector3 Position { get; private set; }
        public bool IsOcupied { get; private set; }

        public GridCell(Vector3 position)
        {
            this.Position = position;
        }

        public void ChangePosition(Vector3 position)
        {
            this.Position = position;
        }

        public void Occupy(GridOccupier occupier)
        {
            IsOcupied = true;
            occupier.ConnectGridCell(this);
        }

        public void FreeCell()
        {
            IsOcupied = false;
        }
    }
}