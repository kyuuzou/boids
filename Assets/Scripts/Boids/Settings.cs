using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject  {
    
    [field: SerializeField]
    public bool CollisionAvoidance { get; set; }
    
    [field: SerializeField]
    public bool FlockCentering { get; set; }

    [field: SerializeField]
    public bool VelocityMatching { get; set; }
    
    [field: SerializeField]
    public bool WrapAroundBoundingVolume { get; set; }

}
