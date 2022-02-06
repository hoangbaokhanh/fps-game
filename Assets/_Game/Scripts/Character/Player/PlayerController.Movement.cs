using NaughtyAttributes;
using UnityEngine;

namespace Fps.Character.Player
{
    public partial class PlayerController
    {
        [SerializeField, Required] private CharacterController characterController;
 
        [SerializeField] private int movementSpeed;
        [SerializeField] private float movementSpeedMultiplier;
        [SerializeField] private int rotationSpeed;

        private float xRotation = 0f;


        private void Move(Vector2 axis, bool sprint)
        {
            var moveVector = transform.right * axis.x + transform.forward * axis.y;
            var moveSpeed = sprint ? movementSpeed * movementSpeedMultiplier : movementSpeed;
            
            characterController.Move(moveVector * moveSpeed * Time.deltaTime);
            if (moveVector != Vector3.zero)
            {
                if (sprint)
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