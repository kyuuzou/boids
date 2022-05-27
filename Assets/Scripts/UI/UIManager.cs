using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    
    [Header("UI Toggles")]
    [SerializeField]
    private Toggle collisionAvoidanceToggle;
    
    [SerializeField]
    private Toggle flockCenteringToggle;
    
    [SerializeField]
    private Toggle velocityMatchingToggle;
    
    [SerializeField]
    private Toggle wrapAroundBoundingVolumeToggle;

    [Header("Persistence")]
    [SerializeField]
    private Settings settings;

    private void Start() {
        this.collisionAvoidanceToggle.isOn = this.settings.CollisionAvoidance;
        this.flockCenteringToggle.isOn = this.settings.FlockCentering;
        this.velocityMatchingToggle.isOn = this.settings.VelocityMatching;
        this.wrapAroundBoundingVolumeToggle.isOn = this.settings.WrapAroundBoundingVolume;
    }
}
