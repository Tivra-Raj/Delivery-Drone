using DeliveryLocation;
using Package;
using Sound;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MVCs
{
    [RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
    public class DroneView : MonoBehaviour
    {
        private Rigidbody droneRigidBody;
        [SerializeField] private Transform attachPoint;
        [SerializeField] private Transform rayPoint;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask packageLayer;
        private GameObject PackageHolder;
        private bool isAttached = false;

        public DroneEngine[] engines;
        
        public Vector2 Movement { get; private set; }
        public float YawPedals { get; private set; }
        public float Throttle { get; private set; }
        public DroneController DroneController { get; private set; }
        public bool IsAttached { get => isAttached; set => isAttached = value; }

        public void SetDroneController(DroneController droneController) => DroneController = droneController;
        
        private void Awake() => droneRigidBody = GetComponent<Rigidbody>();

        private void Start()
        {
            if (droneRigidBody)
            {
                droneRigidBody.mass = DroneController.DroneModel.DroneWeight;
            }
        }

        private void Update()
        {
            AttachingAndDetachingPackageFromDrone();
        }

        private void FixedUpdate()
        {
            if (!droneRigidBody)
            {
                return;
            }

            DroneController.HandlePhysics(); 
        }

        public Rigidbody GetRigidbody() => droneRigidBody;

        //Getting All these input from New Unity Input System
        private void OnMovement(InputValue value)  // to Move and tilt Drone using W,A,S,D key.
        {
            Movement = value.Get<Vector2>();
        }

        private void OnYawPedals(InputValue value)  // to Rotate Drone Left or Right using Arrow left and right key.
        {
            YawPedals = value.Get<float>();
        }

        private void OnThrottle(InputValue value) // to ascend and descend Drone using Arrow up and down key.
        {
            Throttle = value.Get<float>();
        }

        private void AttachingAndDetachingPackageFromDrone()
        {
            if (Physics.Raycast(rayPoint.position, -transform.up, out RaycastHit hit, rayDistance, packageLayer))
            {
                if (Keyboard.current.eKey.wasPressedThisFrame && !IsAttached)
                {
                    SoundService.Instance.PlaySoundEffects(SoundType.PackageAttaching);
                    IsAttached = true;
                    PackageHolder = hit.collider.gameObject;
                    PackageHolder.GetComponent<Rigidbody>().isKinematic = true;
                    PackageHolder.GetComponent<Collider>().isTrigger = true;
                    PackageHolder.transform.SetParent(attachPoint.transform, true);

                    if(DeliveryLocationService.Instance.spwanStatus == DeliveryLocationSpwanStatus.DeSpwaned)
                    {
                        DeliveryLocationService.Instance.SpawnNewDeliveryLocation();
                    }
                    
                    PackageService.Instance.PackageMarker.SetActive(false);
                    
                }
                else if (Keyboard.current.eKey.wasPressedThisFrame && IsAttached)
                {
                    SoundService.Instance.PlaySoundEffects(SoundType.PackageAttaching);
                    IsAttached = false;
                    PackageHolder.GetComponent<PackageView>().PackageController.SubscribeEvents();
                    PackageHolder.GetComponent<Rigidbody>().isKinematic = false;
                    PackageHolder.GetComponent<Collider>().isTrigger = false;
                    PackageHolder.transform.SetParent(null);
                    PackageHolder = null;
                }
            }

            Debug.DrawRay(rayPoint.position, -transform.up * rayDistance);
        }

        public void stopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("GasPoint") && Input.GetKey(KeyCode.E))
            {
                DroneController.RefillFuel();
            }
        }
    }
}