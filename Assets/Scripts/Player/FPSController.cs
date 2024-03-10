using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    [SerializeField] InputActionReference moveInput;
    
    [SerializeField] InputActionReference runInput;

    [SerializeField] InputActionReference rotationInput;

    [SerializeReference] Transform cameraTransform;

    [SerializeField] [Range(0.1f, 10)] float speed = 1;
    [SerializeField] [Range(0.1f, 10)] float runSpeedCoefficient = 2;

    [SerializeField] float acceleration = 1;

    [SerializeReference]
    FMODUnity.StudioEventEmitter walkSound;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        moveInput.action.Enable();
        runInput.action.Enable();
        Cursor.visible = false; 
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
        walkSound.Stop();
    }

    private CharacterController characterController;
    private float currencAcceleration;

    private void Update()
    {
        TryMove();
    }

    private void TryMove()
    {
        if (!moveInput.action.IsPressed())
        {
            if (currencAcceleration != 0) {
                walkSound.Stop();
            }
            currencAcceleration = 0;
            return;
        }

        if (!walkSound.IsPlaying()) {
            walkSound.Play();
        }

        var max = runInput.action.IsPressed() ? runSpeedCoefficient : 1f;
        currencAcceleration = Mathf.Clamp(currencAcceleration + acceleration * Time.deltaTime, 0, max);

        var moveValue = moveInput.action.ReadValue<Vector2>();

        var right = cameraTransform.right;
        var forward = Vector3.Cross(right, Vector3.up).normalized;
        var moveVector = forward * moveValue.y + right * moveValue.x;
        moveVector.Normalize();

        characterController.SimpleMove(moveVector * speed * currencAcceleration);
    }
}