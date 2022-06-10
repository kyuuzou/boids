using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [SerializeField]
    private float cellSize = 10.0f;
    
    private SpriteRenderer spriteRenderer;

    // Using a dictionary so that the grid becomes essentially infinite in all directions
    private Dictionary<Vector2Int, HashSet<int>> cells;
    private Vector4[] occupiedCells;

    private static readonly int ShaderCellSize = Shader.PropertyToID("_cellSize");
    private static readonly int ShaderOccupiedCells = Shader.PropertyToID("_occupiedCells");
    private static readonly Vector4 BufferStop = new (0.0f, 0.0f, 0.0f, -1.0f);
    
    private void Awake() {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.cells = new Dictionary<Vector2Int, HashSet<int>>();
        
        // TODO: use a buffer that can expand so it's not necessary to try to predict the maximum number of cells.
        // From the documentation: "The array length can't be changed once it has been added to the block. If you
        // subsequently try to set a longer array into the same property, the length will be capped to the original
        // length and the extra items you tried to assign will be ignored. If you set a shorter array than the original
        // length, your values will be assigned but the original values will remain for the array elements beyond the
        // length of your new shorter array.
        // https://docs.unity3d.com/ScriptReference/Material.SetVectorArray.html
        this.occupiedCells = new Vector4[1023];

        this.spriteRenderer.sharedMaterial.SetFloat(Grid.ShaderCellSize, this.cellSize);
    }

    private Vector2Int CalculateCell(Vector2 position) {
        return new Vector2Int(
            (int)(position.x / cellSize + 1 * Mathf.Sign(position.x)),
            (int)(position.y / cellSize + 1 * Mathf.Sign(position.y))
        );
    }
    
    public void Add(ref Boid boid) {
        Vector2Int cell = this.CalculateCell(boid.Position);
        this.Add(ref boid, cell);
    }

    private void Add(ref Boid boid, Vector2Int cell) {
        if (!this.cells.ContainsKey(cell)) {
            this.cells[cell] = new HashSet<int>();
        }
  
        this.cells[cell].Add(boid.Identifier);
        boid.Cell = cell;
    }
    
    public void Move(ref Boid boid) {
        Vector2Int cell = this.CalculateCell(boid.Position);

        if (boid.Cell == cell) {
            return;
        }

        this.Remove(ref boid);
        this.Add(ref boid, cell);
    }

    private void LateUpdate() {
        int i = 0;
        
        foreach (Vector2Int key in this.cells.Keys) {
            this.occupiedCells[i++] = new Vector4(key.x, key.y);
        }

        this.occupiedCells[i] = Grid.BufferStop;
        this.spriteRenderer.sharedMaterial.SetVectorArray(Grid.ShaderOccupiedCells, this.occupiedCells);
    }

    public void Remove(ref Boid boid) {
        this.cells[boid.Cell].Remove(boid.Identifier);
  
        if (this.cells[boid.Cell].Count == 0) {
            this.cells.Remove(boid.Cell);
        }
    }
}
