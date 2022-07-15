using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace felipsteles
{
    public class AnimatorHandler : MonoBehaviour
    {
        private PlayerManager playerManager;
        public Animator anim;
        public InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        int vertical;
        int horizontal;
        public bool canRotate;

        public void Initialize()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            anim = GetComponent<Animator>();
            //inputHandler = GetComponentInParent<InputHandler>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();

            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        private void FixedUpdate()
        {
            anim.SetBool("isDead", playerManager.isDead);
        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.CrossFade(targetAnim, 0.2f);
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
        {
            #region Vertical
            float v = 0;

            if ( verticalMovement > 0 && verticalMovement < 0.55f )
            {
                v = 0.5f;
            }
            else if ( verticalMovement > 0.55f )
            {
                v = 1;
            }
            else if ( verticalMovement < 0 && verticalMovement > -0.55f )
            {
                v = -0.5f;
            }
            else if ( verticalMovement < -0.55f )
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;

            if ( horizontalMovement > 0 && horizontalMovement < 0.55f )
            {
                h = 0.5f;
            }
            else if ( horizontalMovement > 0.55f )
            {
                h = 1;
            }
            else if ( horizontalMovement < 0 && horizontalMovement > -0.55f )
            {
                h = -0.5f;
            }
            else if ( horizontalMovement < -0.55f )
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotation()
        {
            canRotate = false;
        }

        private void OnAnimatorMove()
        {
            if (playerManager.isInteracting == false)
                return;
            
            float delta = Time.deltaTime;
            playerLocomotion.GetComponent<Rigidbody>().drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotion.GetComponent<Rigidbody>().velocity = velocity;
        }
    }
}