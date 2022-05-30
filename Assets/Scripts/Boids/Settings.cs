using UnityEngine;

public class Settings : ScriptableObject  {
    
    [field: Header("Boids")]
    [field: SerializeField]
    public int TotalBoids { get; private set; } = 100;

    [field: SerializeField]
    public float MaximumSpeed { get; private set; } = 0.25f;

    [field: SerializeField]
    public float MinimumSpeed { get; private set; } = 0.2f;
    
    [field: Header("World")]
    [field: SerializeField]
    public bool WrapAroundBoundingVolume { get; set; }
    
    public Bounds Bounds { get; set; }
}
