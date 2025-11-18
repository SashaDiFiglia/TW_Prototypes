using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private InputActionAsset _inputActionAsset;
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _parryAction;
    [SerializeField] private InputActionReference _shootAction;


    private Vector2 moveVector;

    private Vector3 moveDir;


    private void Start()
    {
        StartCoroutine(SmoothRotationCoroutine());
    }

    void Update()
    {
        if (moveVector != Vector2.zero)
        {
            moveDir = new Vector3(moveVector.x, moveVector.y, 0);

            transform.position += moveDir * (_moveSpeed * Time.deltaTime);

            //transform.forward = moveDir;
        }
    }

    private void OnEnable()
    {
        _inputActionAsset.Enable();

        _moveAction.action.performed += OnMove;

        _moveAction.action.canceled += StopMoving;

        if (TryGetComponent<PlayerParry>(out var playerParry))
        {
            _parryAction.action.started += playerParry.CallParryCoroutine;
        }

        if (TryGetComponent<PlayerShoot>(out var playerShoot))
        {
            _shootAction.action.started += playerShoot.CallShoot;
        }
    }

    private void OnDisable()
    {
        _moveAction.action.performed -= OnMove;

        _moveAction.action.canceled -= StopMoving;

        if (TryGetComponent<PlayerParry>(out var playerParry))
        {
            _parryAction.action.started -= playerParry.CallParryCoroutine;
        }

        if (TryGetComponent<PlayerShoot>(out var playerShoot))
        {
            _shootAction.action.started -= playerShoot.CallShoot;
        }
    }

    private IEnumerator SmoothRotationCoroutine()
    {
        while (true)
        {
            if (moveDir.magnitude < 0.001f)
            {
                yield return null;

                continue;
            }

            var targetRotation = Quaternion.LookRotation(moveDir, -Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _moveSpeed);

            yield return null;
        }
    }


    private void StopMoving(InputAction.CallbackContext obj)
    {
        moveVector = Vector3.zero;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveVector = ctx.ReadValue<Vector2>();
    }
}