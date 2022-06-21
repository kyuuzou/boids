using UnityEngine;

public delegate void ShowGridChangedHandler(bool showGrid);
public delegate void TotalBoidsChangedHandler(int boids);

public class Settings : ScriptableObject {

    [Header("Boids")]
    [SerializeField]
    private int totalBoids = 100;
    public int TotalBoids {
        get => this.totalBoids;
        set {
            this.totalBoids = value;
            this.TotalBoidsChanged?.Invoke(value);
        }
    }

    public event TotalBoidsChangedHandler TotalBoidsChanged;
    
    [field: SerializeField]
    public float MaximumSpeed { get; set; } = 0.25f;

    [field: SerializeField]
    public float MinimumSpeed { get; set; } = 0.2f;
    
    [field: SerializeField]
    public int MaximumPerceivableBoids { get; set; } = 10;

    [field: Header("World")]
    [field: SerializeField]
    public bool TestSubject { get; set; }

    [SerializeField] 
    private bool showGrid;
    public bool ShowGrid {
        get => this.showGrid;
        set {
            this.showGrid = value;
            this.ShowGridChanged?.Invoke(value);
        }
    }
    public event ShowGridChangedHandler ShowGridChanged;

    [field: SerializeField]
    public bool WrapAroundBoundingVolume { get; set; }

    public Bounds Bounds { get; set; }
}
