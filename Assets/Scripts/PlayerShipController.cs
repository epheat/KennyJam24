using UnityEngine;

public class PlayerShipController : MonoBehaviour {

    public float BaseMoveSpeed = 2.0f;
    public float BaseRotationSpeed = 60.0f;

    public float BaseCannonVelocity = 10.0f;
    public float BaseCannonReload = 10.0f;
    public float BaseHitPoints = 100.0f;

    [SerializeField] private Cannonball CannonballPrefab;
    [SerializeField] private GameObject ProjectilesParent;
    [SerializeField] private GameObject CannonPos;
    [SerializeField] private Rigidbody ShipRigidbody;
    [SerializeField] private bool UseAlternateFiringAngle;

    void Start() {
        InputManager.Instance.MouseClickEvent += this.FireToward;
    }

    void FixedUpdate() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        this.ShipRigidbody.MoveRotation(Quaternion.Euler(0, horizontalInput * this.BaseRotationSpeed * Time.deltaTime, 0) * this.ShipRigidbody.rotation);

        Vector3 forwardMovement = this.transform.forward * verticalInput * this.BaseMoveSpeed * Time.deltaTime;
        this.ShipRigidbody.MovePosition(this.ShipRigidbody.position + forwardMovement);
    }

    void FireToward(Vector3 point) {
        Debug.Log($"Firing toward {point}");

        this.CalculateLaunchAngle(this.CannonPos.transform.position, point, this.BaseCannonVelocity, out Vector3 launchAngle);
        Cannonball cannonball = Object.Instantiate(this.CannonballPrefab, this.ProjectilesParent.transform, true);
        cannonball.transform.position = this.CannonPos.transform.position;
        cannonball.Rigidbody.velocity = launchAngle;
    }

    void OnDestroy() {
        InputManager.Instance.MouseClickEvent -= this.FireToward;
    }


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
