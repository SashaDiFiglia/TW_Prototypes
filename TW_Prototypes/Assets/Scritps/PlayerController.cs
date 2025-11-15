using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerType
    {
        PLAYER1,
        PLAYER2
    }

    [SerializeField] private PlayerType _playerType;
    [SerializeField] private float _moveSpeed;

    void Update()
    {
        var moveX = 0f;
        var moveY = 0f;

        switch (_playerType)
        {
            case PlayerType.PLAYER1:
                moveX = Input.GetAxisRaw("Horizontal");
                moveY = Input.GetAxisRaw("Vertical");
                break;

            case PlayerType.PLAYER2:
                moveX = Input.GetAxisRaw("Horizontal2");
                moveY = Input.GetAxisRaw("Vertical2");
                break;
            default:
                Debug.Log("sono fuori dal tunnel");
                throw new ArgumentOutOfRangeException();
        }

        var movement = new Vector3(moveX, moveY, 0f).normalized;
        transform.Translate(movement * (_moveSpeed * Time.deltaTime));
    }
}