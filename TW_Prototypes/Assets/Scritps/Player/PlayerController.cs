using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private InputActionAsset _inputActionAsset;
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _parryAction;
    [SerializeField] private InputActionReference _shootAction;


    private Vector2 _moveVector;

    private Vector3 _moveDir;


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

        if (TryGetComponent<PlayerParry>(out var absorbTest))
        {
            _parryAction.action.started -= absorbTest.CallParryCoroutine;
        }

        if (TryGetComponent<PlayerShoot>(out var playerShoot))
        {
            _shootAction.action.started -= playerShoot.CallShoot;
        }
    }


    private void StopMoving(InputAction.CallbackContext obj)
    {
        _moveVector = Vector3.zero;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        _moveVector = ctx.ReadValue<Vector2>();
    }

    void Update()
    {
        if (_moveVector != Vector2.zero)
        {
            _moveDir = new Vector3(_moveVector.x, _moveVector.y, 0);

            transform.position += _moveDir * (_moveSpeed * Time.deltaTime);
        }
    }
}