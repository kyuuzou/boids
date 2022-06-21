using System;
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
    private Slider maximumSpeed;

    [SerializeField]
    private Slider minimumSpeed;

    [SerializeField]
    private Slider maximumPerceivableBoids;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private Toggle showGrid;
    
    [SerializeField]
    private Toggle testSubject;
    
    [SerializeField]
    private Slider totalBoids;
    
    [SerializeField]
    private Toggle wrapAroundBoundingVolume;

    [Header("Counters")] 
    [SerializeField]
    private Text boidCounter;

    [SerializeField]
    private Text fpsCounter;

    [SerializeField]
    private float fpsUpdateRate = 4.0f;
 
    private int frameCount = 0;
    private float deltaTime = 0.0f;
    private float fps = 0.0f;
    
    public float MaximumPerceivableBoidsFloat { set => this.settings.MaximumPerceivableBoids = (int) value; }
    public float TotalBoidsFloat { set => this.settings.TotalBoids = (int) value; }

    private void Awake() {
        this.settings.Bounds = this.boundingVolume.bounds;
    }

    private void LateUpdate() {
        this.boidCounter.text = $"total boids: {this.settings.TotalBoids}";
        this.UpdateFrameRate();
    }

    private void OnShowGridChanged(bool showGrid) {
        this.boundingVolume.enabled = showGrid;
    }
    
    private void Start() {
        this.boundsAvoidanceAversion.value = this.boundsAvoidance.Aversion;
        this.boundsAvoidanceMargin.value = this.boundsAvoidance.Margin;
        this.boundsAvoidanceWeight.value = this.boundsAvoidance.Weight;
        
        this.collisionAvoidanceDistance.value = this.collisionAvoidance.Distance;
        this.collisionAvoidanceWeight.value = this.collisionAvoidance.Weight;
        
        this.flockCenteringAttraction.value = this.flockCentering.Attraction;
        this.flockCenteringWeight.value = this.flockCentering.Weight;

        this.velocityMatchingWeight.value = this.velocityMatching.Weight;

        this.minimumSpeed.value = this.settings.MinimumSpeed;
        this.maximumSpeed.value = this.settings.MaximumSpeed;
        this.maximumPerceivableBoids.value = this.settings.MaximumPerceivableBoids;
        this.showGrid.isOn = this.settings.ShowGrid;
        this.testSubject.isOn = this.settings.TestSubject;
        this.totalBoids.value = this.settings.TotalBoids;
        this.wrapAroundBoundingVolume.isOn = this.settings.WrapAroundBoundingVolume;
        
        this.OnShowGridChanged(this.settings.ShowGrid);
        this.settings.ShowGridChanged += this.OnShowGridChanged;
    }
    
    private void UpdateFrameRate() {
        this.frameCount++;
        this.deltaTime += Time.unscaledDeltaTime;
        
        if (this.deltaTime > 1.0f / this.fpsUpdateRate) {
            this.fps = this.frameCount / this.deltaTime;
            this.frameCount = 0;
            this.deltaTime -= 1.0f / this.fpsUpdateRate;
        }
        
        this.fpsCounter.text = $"fps: {this.fps:F1}";
    }
}
