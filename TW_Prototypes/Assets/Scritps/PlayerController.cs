using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActionAsset;

    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _parryAction;

    private Vector2 _moveVector;

    private Vector3 _moveDir;

    public enum PlayerType
    {
        PLAYER1,
        PLAYER2
    }

    [SerializeField] private PlayerType _playerType;
    [SerializeField] private float _moveSpeed;

    private void OnEnable()
    {
        _inputActionAsset.Enable();

        _moveAction.action.performed += OnMove;

        _moveAction.action.canceled += StopMoving;

        if (TryGetComponent<AbsorbTest>(out var absorbTest))
        {
            _parryAction.action.performed += absorbTest.CallParryCoroutine;
        }
    }

    private void OnDisable()
    {
        _moveAction.action.performed -= OnMove;

        _moveAction.action.canceled -= StopMoving;

        if (TryGetComponent<AbsorbTest>(out var absorbTest))
        {
            _parryAction.action.performed -= absorbTest.CallParryCoroutine;
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