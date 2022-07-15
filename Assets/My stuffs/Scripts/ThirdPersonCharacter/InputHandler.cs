using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace felipsteles{
    public class InputHandler : MonoBehaviour
    {

        public float horizontal;
        public float vertical;
        public float moveAmount;

        private PlayerControls inputActions;

        public bool sprintFlag;

        Vector2 movementInput;

        private void Awake()
        {
            inputActions = new PlayerControls();
        }

        private void Update(){
            movementInput = inputActions.PlayerMovement.Movement.ReadValue<Vector2>();
        }

        public void OnEnable()
        {
            inputActions.Enable();
        }    

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
        }

        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        }
    }
}