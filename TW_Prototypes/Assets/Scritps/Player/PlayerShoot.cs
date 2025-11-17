using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public void CallShoot(InputAction.CallbackContext obj)
    {
        Debug.Log(name + " : Shooting");
    }
}