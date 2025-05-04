

using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class PlayerAnimatorController
    {
        private readonly UnityEngine.Animator _animator;
        public PlayerAnimatorController(UnityEngine.Animator animator)
        {
            _animator = animator;
        }
        public void SetRunTrue() => _animator.SetBool("Run", true);
        public void SetJumpTrue() => _animator.SetBool("Jump", true);
        public void SetSlideTrue() => _animator.SetBool("Slide", true);
        public void SetLeftTrue() => _animator.SetBool("Left", true);
        public void SetRightTrue() => _animator.SetBool("Right", true);
        public void SetDeadTrue() => _animator.SetBool("Dead", true);
        public void SetFallDeadTrue() => _animator.SetBool("FallDead", true);
        public void SetLeftTrippingTrue() => _animator.SetBool("LeftTripping", true);
        public void SetRightTrippingTrue() => _animator.SetBool("RightTripping", true);
        public void SetHitTheLegTrue()=>_animator.SetBool("HitTheLeg", true);

        public void SetRunFalse() => _animator.SetBool("Run", false);
        public void SetJumpFalse() => _animator.SetBool("Jump", false);
        public void SetSlideFalse() => _animator.SetBool("Slide", false);
        public void SetLeftFalse() => _animator.SetBool("Left", false);
        public void SetRightFalse() => _animator.SetBool("Right", false);
        public void SetDeadFalse() => _animator.SetBool("Dead", false);
        public void SetFallDeadFalse() => _animator.SetBool("FallDead", false);
        public void SetLeftTrippingFalse() => _animator.SetBool("LeftTripping", false);
        public void SetRightTrippingFalse() => _animator.SetBool("RightTripping", false);
        public void SetHitTheLegFalse() => _animator.SetBool("HitTheLeg", false);


        public bool GetBoolRun() => _animator.GetBool("Run");
            public bool GetBoolJump() => _animator.GetBool("Jump");
            public bool GetBoolSlide() => _animator.GetBool("Slide");
            public bool GetBoolLeft() => _animator.GetBool("Left");
            public bool GetBoolRight() => _animator.GetBool("Right");

            public bool GetBoolDead() => _animator.GetBool("Dead");
            public bool GetBoolFallDead() => _animator.GetBool("FallDead");
            public bool GetBoolLeftTripping() => _animator.GetBool("LeftTripping");
            public bool GetBoolRightTripping() => _animator.GetBool("RightTripping");
            public bool GetBoolHitTheLeg() => _animator.GetBool("HitTheLeg");


        public void StopAll()
        {
            string[] boolParams = {
            "Run", "Jump", "Slide", "Left", "Right",
            "Dead", "FallDead", "LeftTripping", "RightTripping"
        };

            foreach (var param in boolParams)
            {
                _animator.SetBool(param, false);
            }
        }
        public void StopThis(params AnimationStates[] states) 
        {
            foreach (var item in states)
            {
                _animator.SetBool(item.ToString(), false);                
            }
        }
        public bool IsInDeathState()
        {
            return _animator.GetBool("Dead") || _animator.GetCurrentAnimatorStateInfo(0).IsName("FallDead") ||
                   _animator.GetCurrentAnimatorStateInfo(0).IsName("HitTheLeg") ||
                   _animator.GetCurrentAnimatorStateInfo(0).IsName("LeftTripping") ||
                   _animator.GetCurrentAnimatorStateInfo(0).IsName("RightTripping");
        }
        public Vector3 GetAnimatorDeltaPosition() => _animator.deltaPosition;
    } 
}

