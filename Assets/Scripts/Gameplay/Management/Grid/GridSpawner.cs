using Gameplay.Objects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Management
{
    public class GridSpawner : MonoBehaviour
    {
        public readonly static UnityEvent<int> OnNewGridSpawn = new();
        public readonly static UnityEvent<GridOccupier> OnConnectObjectToCell = new();

        [SerializeField] private GameObject gridCellPrefab;
        [SerializeField] private int cellCount = 500;
        [SerializeField] private Transform gridContainer;
        [SerializeField] private float cellSize = 1.5f;
        [SerializeField] private bool spawnGrid = false;
        private List<GridCell> cells = new List<GridCell>();
        private List<GridCell> unusedCells = new List<GridCell>();
        private float startX;
        private float startY;
        private float cameraDistance;
        private int cellsX;
        private int cellsY;
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            OnNewGridSpawn.AddListener(CreateGrid);
            OnConnectObjectToCell.AddListener(ConnectGameObjectToGrid);
        }

        private void OnDisable()
        {
            OnNewGridSpawn.RemoveListener(CreateGrid);
            OnConnectObjectToCell.RemoveListener(ConnectGameObjectToGrid);
        }

        public void ConnectGameObjectToGrid(GridOccupier gridOccupier)
        {
            var cell = RandomFreeCell();
            gridOccupier.transform.position = cell.Position;

            cell.Occupy(gridOccupier);
        }

        private GridCell RandomFreeCell()
        {            
            return cells.Random(x => !x.IsOcupied);
        }

        private void CreateGrid(int cellCount)
        {
            this.cellCount = cellCount;

            unusedCells = unusedCells.Union(cells).ToList();
            cells.Clear();

            CalculateGridSize();
            PositionGridCells();
        }

        private void CalculateGridSize()
        {
            cellsX = Mathf.CeilToInt(Mathf.Sqrt(cellCount));
            cellsY = Mathf.CeilToInt((float)cellCount / cellsX);
            cellCount = cellsX * cellsY;

            float totalWidth = cellsX * cellSize;
            float totalHeight = cellsY * cellSize;

            startX = mainCamera.transform.position.x - totalWidth / 2 + cellSize / 2;
            startY = mainCamera.transform.position.y + totalHeight / 2 - cellSize / 2;

            CalculateDistanceFromCamera(totalWidth, totalHeight);
        }

        private void CalculateDistanceFromCamera(float totalWidth, float totalHeight)
        {
            cameraDistance = Mathf.Max(totalWidth, totalHeight) / (2f * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad));
        }

        private void PositionGridCells()
        {
            int createdCells = 0;
            for (int y = 0; y < cellsY && createdCells < cellCount; y++)
            {
                for (int x = 0; x < cellsX && createdCells < cellCount; x++)
                {
                    var posX = startX + x * cellSize;
                    var posY = startY - y * cellSize;

                    Vector3 cellPosition = new Vector3(posX, posY, cameraDistance);

                    VisualizeGrid(cellPosition);

                    AddActiveCell(cellPosition);

                    createdCells++;
                }
            }
        }

        private void AddActiveCell(Vector3 cellPosition)
        {
            var cell = unusedCells.FirstOrDefault();

            if (cell == null)
            {
                cell = new(cellPosition);
                cells.Add(cell);
            }

            else
            {
                ChangeExistingCell(cellPosition, cell);
            }
        }

        private void ChangeExistingCell(Vector3 cellPosition, GridCell cell)
        {
            cell.FreeCell();
            cell.ChangePosition(cellPosition);
            unusedCells.Remove(cell);
            cells.Add(cell);
        }

        private void VisualizeGrid(Vector3 cellPosition)
        {
            if (spawnGrid)
            {
                var cell = Instantiate(gridCellPrefab, cellPosition, Quaternion.identity);

                cell.transform.parent = gridContainer;
            }
        }
    }
}
