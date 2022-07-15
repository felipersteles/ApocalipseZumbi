using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace felipsteles
{
    public class PlayerLocomotion : MonoBehaviour
    {

        PlayerManager playerManager;
        private Transform followTransform;
        InputHandler inputHandler;
        public Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimatorHandler animatorHandler;
        [HideInInspector]
        public CharacterController characterController;

        [Header("Movement Stats")]
        [SerializeField] //to change in game
        float movementSpeed = 5;
        //[SerializeField]
        //float sprintSpeed = 7;
        [SerializeField]
        float sensitivity = 1f;
        

        // Start is called before the first frame update
        void Start()
        {
            playerManager = GetComponent<PlayerManager>();
            characterController = GetComponent<CharacterController>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();

            followTransform = GameObject.Find("FollowTarget").transform;
            myTransform = transform;

            animatorHandler.Initialize();

            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float delta = Time.deltaTime;
        }

        #region Movement

        private void HandleRotation()
        {
            var horizontal = Input.GetAxis("Mouse X");
            var vertical = -Input.GetAxis("Mouse Y");

            #region Player Based Rotation

            transform.rotation *= Quaternion.AngleAxis(horizontal * sensitivity, Vector3.up);

            #endregion

            #region Follow Transform Rotation

            followTransform.rotation *= Quaternion.AngleAxis(horizontal * sensitivity, Vector3.up);

            #endregion
        
            #region Vertical Rotation

            followTransform.rotation *= Quaternion.AngleAxis(vertical * sensitivity, Vector3.right);

            //Clamping the vertical rotation
            var angles = followTransform.localEulerAngles;
            angles.z = 0;

            var angle = followTransform.localEulerAngles.x;

            if (angle > 180 && angle < 340)
                angles.x = 340;
            else if(angle < 180 && angle > 40)
                angles.x = 40;

            followTransform.localEulerAngles = angles;
            #endregion

            myTransform.rotation = Quaternion.Euler(0, followTransform.rotation.eulerAngles.y, 0);
            followTransform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }

        public void HandleMovement(float delta)
        {
            if (playerManager.isInteracting)
                return;

            moveDirection = new Vector3(inputHandler.horizontal, 0, inputHandler.vertical);

            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

            if (animatorHandler.canRotate)
            {
                HandleRotation();
            }

            if( inputHandler.vertical != 0)
                characterController.SimpleMove(transform.forward * movementSpeed * inputHandler.vertical);
        }

        #endregion
    }
}