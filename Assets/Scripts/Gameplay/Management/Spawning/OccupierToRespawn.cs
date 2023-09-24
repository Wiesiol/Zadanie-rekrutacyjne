using Gameplay.Objects;
using UnityEngine;

namespace Gameplay.Management
{
    public struct OccupierToRespawn
    {
        public readonly GridOccupier occupier;
        private readonly float timeToRespawn;
        public readonly bool IsTimeToRespawn => timeToRespawn <= Time.time;

        public OccupierToRespawn(GridOccupier gameObject, float timeToRespawn)
        {
            this.occupier = gameObject;
            this.timeToRespawn = timeToRespawn;
        }

        public void Respawn()
        {
            GridSpawner.OnConnectObjectToCell.Invoke(occupier);
            occupier.gameObject.SetActive(true);
        }
    }
}