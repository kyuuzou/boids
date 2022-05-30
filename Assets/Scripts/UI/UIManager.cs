using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    
    [Header("Bounds Avoidance")]
    [SerializeField]
    private BoundsAvoidance boundsAvoidance;

    [SerializeField]
    private Slider boundsAvoidanceAversion;

    [SerializeField]
    private Slider boundsAvoidanceMargin;
    
    [SerializeField]
    private Slider boundsAvoidanceWeight;
    
    [Header("Collision Avoidance")]
    [SerializeField]
    private CollisionAvoidance collisionAvoidance;
    
    [SerializeField]
    private Slider collisionAvoidanceDistance;
    
    [SerializeField]
    private Slider collisionAvoidanceWeight;

    [Header("Flock Centering")]
    [SerializeField]
    private FlockCentering flockCentering;

    [SerializeField]
    private Slider flockCenteringAttraction;
    
    [SerializeField]
    private Slider flockCenteringWeight;

    [Header("Velocity Matching")]
    [SerializeField]
    private VelocityMatching velocityMatching;

    [SerializeField]
    private Slider velocityMatchingWeight;

    [Header("Settings")]
    [SerializeField]
    private SpriteRenderer boundingVolume;
    
    [SerializeField]
    private Settings settings;

    [SerializeField]
    private Toggle wrapAroundBoundingVolume;

    private void Start() {
        this.boundsAvoidanceAversion.value = this.boundsAvoidance.Aversion;
        this.boundsAvoidanceMargin.value = this.boundsAvoidance.Margin;
        this.boundsAvoidanceWeight.value = this.boundsAvoidance.Weight;
        
        this.collisionAvoidanceDistance.value = this.collisionAvoidance.Distance;
        this.collisionAvoidanceWeight.value = this.collisionAvoidance.Weight;
        
        this.flockCenteringAttraction.value = this.flockCentering.Attraction;
        this.flockCenteringWeight.value = this.flockCentering.Weight;

        this.velocityMatchingWeight.value = this.velocityMatching.Weight;

        this.settings.Bounds = this.boundingVolume.bounds;
        this.wrapAroundBoundingVolume.isOn = this.settings.WrapAroundBoundingVolume;
        
        this.boundingVolume.enabled = false;
    }
}
