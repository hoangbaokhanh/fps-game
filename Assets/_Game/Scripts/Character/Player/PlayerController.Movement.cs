using NaughtyAttributes;
using UnityEngine;

namespace Fps.Character.Player
{
    public partial class PlayerController
    {
        [SerializeField, Required] private CharacterController characterController;
 
        [SerializeField] private int movementSpeed;
        [SerializeField] private int rotationSpeed;

        private float xRotation = 0f;
        private void OnInput(Input.Input input)
        {
            var moveVector = transform.right * input.MoveVector.x + transform.forward * input.MoveVector.y;
            
            Rotation(input.LookVector);
            characterController.Move(moveVector * movementSpeed * Time.deltaTime);
            if (moveVector != Vector3.zero)
            {
                if (input.Sprint)
                {
                    playerVisual.Run();
                }
                else
                {
                    playerVisual.Walk();
                }
            }
            else
            {
                playerVisual.Idle();
            }
        }

        private void Rotation(Vector2 axis)
        {
            xRotation -= axis.y * rotationSpeed * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); 
            transform.Rotate(Vector3.up * axis.x * rotationSpeed * Time.deltaTime);
        }
    }
}