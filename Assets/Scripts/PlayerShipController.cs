using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerShipController : MonoBehaviour {

    public float BaseMoveSpeed = 2.0f;
    public float BaseRotationSpeed = 60.0f;

    public float BaseCannonVelocity = 8.0f;
    public float BaseCannonDamage = 5.0f;
    public float BaseCannonReload = 10.0f;
    public float BaseHitPoints = 100.0f;

    private Dictionary<PowerUpType, float> PowerUps = new();

    [SerializeField] private Cannonball CannonballPrefab;
    [SerializeField] private GameObject ProjectilesParent;
    [SerializeField] private GameObject CannonPosRight;
    [SerializeField] private GameObject CannonPosLeft;
    [SerializeField] private Rigidbody ShipRigidbody;
    [SerializeField] private bool UseAlternateFiringAngle;
    [SerializeField] private ParticleSystem CollectParticles;

    [SerializeField] private BoatConnection BoatConnectionPrefab;
    [SerializeField] private Transform ConnectionsParent;

    public bool IsStopped;

    public static PlayerShipController Instance;

    private void Awake(){
        Instance = this;
    }

    void Start() {
        InputManager.Instance.MouseClickEvent += this.FireToward;
    }

    public float GetMoveSpeed() {
        return this.BaseMoveSpeed + this.PowerUps.GetValueOrDefault(PowerUpType.BoatSpeed, 0);
    }

    private float GetCannonDamage() {
        return this.BaseCannonDamage + this.PowerUps.GetValueOrDefault(PowerUpType.CannonRange, 0);
    }

    void FixedUpdate() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        this.ShipRigidbody.MoveRotation(Quaternion.Euler(0, horizontalInput * this.BaseRotationSpeed * Time.deltaTime, 0) * this.ShipRigidbody.rotation);

        Vector3 forwardMovement = this.transform.forward * verticalInput * this.GetMoveSpeed() * Time.deltaTime;
        this.ShipRigidbody.MovePosition(this.ShipRigidbody.position + forwardMovement);
        
        this.IsStopped = verticalInput == 0;
    }

    void FireToward(Vector3 worldPosition, Vector3 screenPosition) {
        Debug.Log($"Firing cannon!");
        if (!CannonballManager.Instance.CanUseBall()){
            return;
        }
        float screenWidth = Screen.width;
        Transform cannon = screenPosition.x > screenWidth / 2 ? this.CannonPosRight.transform : this.CannonPosLeft.transform;

        Cannonball cannonball = Object.Instantiate(this.CannonballPrefab, this.ProjectilesParent.transform, true);
        cannonball.damage = (int) this.GetCannonDamage();
        cannonball.transform.position = cannon.position;
        cannonball.Rigidbody.velocity = cannon.forward * this.BaseCannonVelocity;

        CannonballManager.Instance.UseBall();
    }

    void OnDestroy() {
        InputManager.Instance.MouseClickEvent -= this.FireToward;
    }

    public void ApplyPowerup(PowerUp powerUp) {
        this.CollectParticles.Play();
        this.PowerUps[powerUp.type] = this.PowerUps.GetValueOrDefault(powerUp.type, 0) + powerUp.amount;
        if (powerUp.type == PowerUpType.CannonCapacity) {
            CannonballManager.Instance.IncreaseMaxBalls();
        }
        if (powerUp.type == PowerUpType.CannonRange) {

        }
    }

    public BoatConnection CreateConnection(GameObject connectedShip) {
        BoatConnection connect = Object.Instantiate(this.BoatConnectionPrefab, this.ConnectionsParent, true);
        connect.SetEndPoints(this.transform, connectedShip.transform);
        return connect;
    }


    // Now unused
    private bool CalculateLaunchAngle(Vector3 startPos, Vector3 targetPos, float projectileVelocity, out Vector3 launch) {
        float g = Physics.gravity.y;
        Vector3 direction = targetPos - startPos;
        float x = direction.x;
        float y = direction.y;

        float v2 = projectileVelocity * projectileVelocity;
        float underSqrt = v2 * v2 - g * (g * x * x + 2 * y * v2);

        float launchAngle;
        if (underSqrt < 0) {
            // can't reach that far. Just fire at 45 deg.
            launch = direction.normalized;
            launch.y = 1;
            launch.Normalize();
            launch *= projectileVelocity;
            return false;
        }

        float sqrt = Mathf.Sqrt(underSqrt);
        float gX = g * x;

        if (this.UseAlternateFiringAngle) {
            launchAngle = Mathf.Atan2(v2 + sqrt, gX);
        } else {
            launchAngle = Mathf.Atan2(v2 - sqrt, gX);
        }

        launch = direction.normalized;
        launch.y = Mathf.Tan(launchAngle);
        launch.Normalize();
        launch *= projectileVelocity;

        return true;
    }
}
