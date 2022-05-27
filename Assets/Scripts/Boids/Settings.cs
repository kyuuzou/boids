using UnityEngine;

public class Settings : ScriptableObject  {
    
    [field: Header("Behaviours")]
    [field: SerializeField]
    public bool CollisionAvoidance { get; set; }
    
    [field: SerializeField]
    public bool FlockCentering { get; set; }

    [field: SerializeField]
    public bool VelocityMatching { get; set; }
    
    [field: SerializeField]
    public bool WrapAroundBoundingVolume { get; set; }
    
    [field: Header("Boids")]
    [field: SerializeField]
    public int TotalBoids { get; private set; } = 100;

    [field: SerializeField]
    public float MaximumSpeed { get; private set; } = 0.25f;

    [field: SerializeField]
    public float MinimumSpeed { get; private set; } = 0.2f;
    
    [Header("World")]
    [SerializeField]
    private SpriteRenderer boundingVolume;
    public Bounds Bounds => this.boundingVolume.bounds;
}
