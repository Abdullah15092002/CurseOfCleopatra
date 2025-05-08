using System.Collections;
using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.Controllers;
using Assets.Scripts.Managers;
using UnityEngine.EventSystems;
using Assets.Scripts.Pointer;


namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {

        private GameObject cameraObj;
        private Animator animator;
        private Rigidbody rb;
        private PlayerAnimatorController animatorControls;

        private bool isSwipeFromUI = false;
        private int next_x_pos;
        private float swipeThreshold = 50f;
        private bool Left, Right;
        private bool isJumpDown = false;
        private bool canMoveLeftRight = true;
        private bool canMove = true;
        Vector2 startPoint;

        void Start()
        {
            cameraObj=GetComponentInChildren<Camera>(true)?.gameObject;
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            animatorControls = new PlayerAnimatorController(animator);
            GameManager.Instance.ResetGame();
        }

        void Update()
        {
            if (animatorControls.IsInDeathState())
            {
                GameManager.Instance.isGroundMove = false;               
            }
            InputControl();
        }
        public void InputControl()
        {
             
            if (Input.GetKeyUp(KeyCode.Space))
            {
                GameManager.Instance.StartGame();           
                animatorControls.SetRunTrue();
            }
                                                       
            if (GameManager.Instance.currentlyMove)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //Pause Butonuna tıkladığında algılamaması için
                    isSwipeFromUI = UIInputBlocker.IsPointerOverUIWithTag();
                    startPoint = Input.mousePosition;
                }
                else if (!isSwipeFromUI && Input.GetMouseButtonUp(0))
                {
                    Vector2 endPoint = Input.mousePosition;
                    float swipeDistance = (endPoint - startPoint).magnitude;

                    if (swipeDistance >= swipeThreshold)
                    {
                        Vector2 swipeDirection = endPoint - startPoint;
                        swipeDirection.Normalize();

                        if (swipeDirection.x < -0.5f && Mathf.Abs(swipeDirection.y) < 0.5 && canMove && canMoveLeftRight)
                        {
                            if (!animatorControls.GetBoolJump() && !animatorControls.GetBoolSlide())
                                animatorControls.SetLeftTrue();
                            else
                                Left = true;
                            if (rb.position.x >= 1 && rb.position.x < 3)
                            {
                                next_x_pos = 0;
                            }
                            else if (rb.position.x >= -1 && rb.position.x < 1)
                            {
                                next_x_pos = -2;
                            }
                            StartCoroutine(ToLeft(next_x_pos));
                        }
                        else if (swipeDirection.x > 0.5f && Mathf.Abs(swipeDirection.y) < 0.5f && canMove && canMoveLeftRight)
                        {
                            if (!animatorControls.GetBoolJump() && !animatorControls.GetBoolSlide())
                                animatorControls.SetRightTrue();
                            else
                                Right = true;

                            if (rb.position.x >= -3 && rb.position.x < -1)
                            {
                                next_x_pos = 0;
                            }
                            else if (rb.position.x >= -1 && rb.position.x < 1)
                            {
                                next_x_pos = 2;
                            }
                            StartCoroutine(ToRight(next_x_pos));
                        }
                        else if (swipeDirection.y > 0.5f && Mathf.Abs(swipeDirection.x) < 0.5f)
                        {
                            rb.position = new Vector3(next_x_pos, transform.position.y, transform.position.z);
                            animatorControls.StopThis(
                                AnimationStates.Slide, AnimationStates.Left,
                                AnimationStates.Right, AnimationStates.Jump);
                            animatorControls.SetJumpTrue();
                            //jumpFX.Play();
                        }
                        else if (swipeDirection.y < -0.5f && Mathf.Abs(swipeDirection.x) < 0.5f)
                        {
                            animatorControls.SetJumpFalse();
                            animatorControls.SetSlideTrue();
                            //slideFX.Play();
                        }
                    }
                }
            }
            IEnumerator ToLeft(int next_x_pos)
            {
                canMove = false;

                float timer = 0.001f;
                yield return new WaitForSeconds(timer);
                transform.position = new Vector3(this.next_x_pos + 1f, transform.position.y, transform.position.z);
                yield return new WaitForSeconds(timer);

                transform.position = new Vector3(this.next_x_pos + 0.5f, transform.position.y, transform.position.z);
                yield return new WaitForSeconds(timer);

                canMove = true;
            }

            IEnumerator ToRight(int next_x_pos)
            {
                canMove = false;

                float timer = 0.001f;
                yield return new WaitForSeconds(timer);
                transform.position = new Vector3(this.next_x_pos - 0.8f, transform.position.y, transform.position.z);
                yield return new WaitForSeconds(timer);

                transform.position = new Vector3(this.next_x_pos - 0.6f, transform.position.y, transform.position.z);
                yield return new WaitForSeconds(timer);

                transform.position = new Vector3(this.next_x_pos - 0.4f, transform.position.y, transform.position.z);
                yield return new WaitForSeconds(timer);

                transform.position = new Vector3(this.next_x_pos - 0.2f, transform.position.y, transform.position.z);
                yield return new WaitForSeconds(timer);

                canMove = true;
            }
        }
        IEnumerator WaitGameOver()
        {
            PowerUpManager.Instance.GameOverRegulations();
            float timer = 1.5f;
            yield return new WaitForSeconds(timer);
            GameManager.Instance.GameOver();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("HitTheLeg"))
            {
                animatorControls.SetHitTheLegTrue();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            string tag = other.tag;

            if (PowerUpManager.Instance.isShieldActive)
            {
                if (PowerUpManager.Instance.isShieldHitCooldown) return;
                HandleShieldCollision(tag);
            }
            else
            {
                HandleUnprotectedCollision(tag);
            }
        }

        private void HandleShieldCollision(string tag)
        {
             switch (tag)
            {
                case "Obs":
                case "LeftTripping":
                case "RightTripping":
                case "FallDamage":
                    StartCoroutine(PowerUpManager.Instance.ShieldHitCooldown());
                    break;

                case "NoLeftRight":
                    canMoveLeftRight = false;
                    break;
            }
        }

        private void HandleUnprotectedCollision(string tag)
        {
            switch (tag)
            {
                case "Obs":
                    animatorControls.SetDeadTrue();
                    StartCoroutine(MoveBackwardOnDeath());
                    StartCoroutine(WaitGameOver());
                    break;

                case "LeftTripping":
                    animatorControls.SetLeftTrippingTrue();
                    animatorControls.SetDeadFalse();
                    StartCoroutine(MoveRightOnDeath());
                    StartCoroutine(WaitGameOver());
                    break;

                case "RightTripping":
                    animatorControls.SetRightTrippingTrue();
                    animatorControls.SetDeadFalse();
                    StartCoroutine(MoveLeftOnDeath());
                    StartCoroutine(WaitGameOver());
                    break;

                case "FallDamage":
                    cameraObj.transform.parent = null;
                    animatorControls.SetFallDeadTrue();
                    StartCoroutine(WaitGameOver());
                    break;

                case "NoLeftRight":
                    canMoveLeftRight = false;
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("NoLeftRight"))
            {
                canMoveLeftRight = true;
            }
        }
        private IEnumerator MoveBackwardOnDeath()
        {
            float moveTime = 0.5f;
            float elapsedTime = 0f;
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);

            while (elapsedTime < moveTime)
            {
                transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / moveTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos;
            swipeThreshold = 0;
        }

        private IEnumerator MoveLeftOnDeath()
        {
            float moveTime = 0.5f;
            float elapsedTime = 0f;
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);

            while (elapsedTime < moveTime)
            {
                transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / moveTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos;
        }

        private IEnumerator MoveRightOnDeath()
        {
            float moveTime = 0.5f;
            float elapsedTime = 0f;
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);

            while (elapsedTime < moveTime)
            {
                transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / moveTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos;
        }


        void ToggleOff(string Name)
        {
            animator.SetBool(Name, false);
            isJumpDown = false;
        }

        void JumpDown()
        {
            isJumpDown = true;
        }

        private void OnAnimatorMove()
        {
            if (GameManager.Instance.isDead) return;

            if (animatorControls.GetBoolFallDead())
            {
                rb.MovePosition(rb.position + Vector3.down * animator.deltaPosition.magnitude);
                swipeThreshold = 0;
            }

            if (animatorControls.GetBoolLeftTripping())
            {
                rb.MovePosition(rb.position + new Vector3(2f, 0, 0) * animator.deltaPosition.magnitude);
            }

            if (animatorControls.GetBoolRightTripping())
            {
                rb.MovePosition(rb.position + new Vector3(-2f, 0, 0) * animator.deltaPosition.magnitude);
            }

            else if (animatorControls.GetBoolJump())
            {
                if (isJumpDown)
                    rb.MovePosition(rb.position + new Vector3(0, 0f, 0) * animator.deltaPosition.magnitude);
                else
                    rb.MovePosition(rb.position + new Vector3(0, 0.8f, 0) * animator.deltaPosition.magnitude);
            }
            else if (animatorControls.GetBoolRight())
            {
                if (rb.position.x < next_x_pos)
                    rb.MovePosition(rb.position + new Vector3(1, 0, 0) * animator.deltaPosition.magnitude);
                else
                {
                    rb.position = new Vector3(next_x_pos, transform.position.y, transform.position.z);
                    animatorControls.SetRightFalse();
                }
            }
            else if (animatorControls.GetBoolLeft())
            {
                if (rb.position.x > next_x_pos)
                    rb.MovePosition(rb.position + new Vector3(-1, 0, 0) * animator.deltaPosition.magnitude);
                else
                {
                    rb.position = new Vector3(next_x_pos, transform.position.y, transform.position.z);
                    animatorControls.SetLeftFalse();
                }
            }

            if (Left)
            {
                if (rb.position.x > next_x_pos)
                    rb.MovePosition(rb.position + new Vector3(-1, 0, 0) * animator.deltaPosition.magnitude);
                else
                    Left = false;
            }

            else if (Right)
            {
                if (rb.position.x < next_x_pos)
                    rb.MovePosition(rb.position + new Vector3(1, 0, 0) * animator.deltaPosition.magnitude);
                else
                    Right = false;
            }
        }
    }
}
