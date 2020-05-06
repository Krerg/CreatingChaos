
using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaraAnimator : MonoBehaviour
{

    public enum SaraAnimation
    {
        idle_sara,
        run_start_sara,
        run_cycle_sara,
        run_end_sara,
        jump_start_sara,
        jump_up_cycle_sara,
        jump_down_cycle_sara,
        jump_end_sara,
        push_start_sara,
        push_cycle_sara,
        push_end_sara,
        pull_start_sara,
        pull_cycle_sara,
        pull_end_sara,
    }

    SkeletonAnimation spineAnimation;
    SaraAnimation currentAnimation;
    bool wasFalling;
    bool wasInAir;
    bool wasMoving;

    public bool isSara = false;

    void Start()
    {
        currentAnimation = SaraAnimation.idle_sara;
        spineAnimation = GetComponent<SkeletonAnimation>();
        spineAnimation.AnimationName = currentAnimation.ToString();
        spineAnimation.AnimationState.End += OnAnimationEnded;
    }

    public void Jump()
    {
        SetAnimation(SaraAnimation.jump_start_sara, false);
    }

    public void BeginPush()
    {
        SetAnimation(SaraAnimation.push_start_sara, false);
    }

    public void EndPush()
    {
        if (currentAnimation == SaraAnimation.push_start_sara || currentAnimation == SaraAnimation.push_cycle_sara)
            SetAnimation(SaraAnimation.push_end_sara, false);
    }

    public void BeginPull()
    {
        SetAnimation(SaraAnimation.pull_start_sara, false);
    }

    public void EndPull()
    {
        if (currentAnimation == SaraAnimation.pull_start_sara || currentAnimation == SaraAnimation.pull_cycle_sara)
            SetAnimation(SaraAnimation.pull_end_sara, false);
    }

    public void UpdateAnimation(bool isMoving, bool inAir, bool isFalling)
    {
        wasInAir = inAir;
        wasFalling = isFalling;
        wasMoving = isMoving;

        switch (currentAnimation)
        {
            case SaraAnimation.idle_sara:
                if (inAir && isFalling)
                    SetAnimation(SaraAnimation.jump_down_cycle_sara, true);
                else if (isMoving)
                    SetAnimation(SaraAnimation.run_start_sara, false);
                break;

            case SaraAnimation.run_start_sara:
                if (inAir && isFalling)
                    SetAnimation(SaraAnimation.jump_down_cycle_sara, true);
                else if (!isMoving)
                    SetAnimation(SaraAnimation.run_end_sara, false);
                break;

            case SaraAnimation.run_cycle_sara:
                if (inAir && isFalling)
                    SetAnimation(SaraAnimation.jump_down_cycle_sara, true);
                else if (!isMoving)
                    SetAnimation(SaraAnimation.run_end_sara, false);
                break;

            case SaraAnimation.run_end_sara:
                if (inAir && isFalling)
                    SetAnimation(SaraAnimation.jump_down_cycle_sara, true);
                else if (isMoving)
                    SetAnimation(SaraAnimation.run_start_sara, false);
                break;

            case SaraAnimation.jump_start_sara:
                if (!inAir)
                    SetAnimation(SaraAnimation.jump_end_sara, false);
                break;

            case SaraAnimation.jump_up_cycle_sara:
                if (!inAir)
                    SetAnimation(SaraAnimation.jump_end_sara, false);
                else if (isFalling)
                    SetAnimation(SaraAnimation.jump_down_cycle_sara, true);
                break;

            case SaraAnimation.jump_down_cycle_sara:
                if (!inAir)
                    SetAnimation(SaraAnimation.jump_end_sara, false);
                break;

            case SaraAnimation.jump_end_sara:
                if (inAir)
                    SetAnimation(SaraAnimation.jump_start_sara, false);
                break;
        }
    }

    void OnAnimationEnded(TrackEntry track)
    {
        switch (currentAnimation)
        {
            case SaraAnimation.run_start_sara:
                SetAnimation(SaraAnimation.run_cycle_sara, true);
                break;

            case SaraAnimation.run_end_sara:
                SetAnimation(SaraAnimation.idle_sara, true);
                break;

            case SaraAnimation.jump_start_sara:
                SetAnimation(wasFalling ? SaraAnimation.jump_down_cycle_sara : SaraAnimation.jump_up_cycle_sara, true);
                break;

            case SaraAnimation.jump_end_sara:
                SetAnimation(wasMoving ? SaraAnimation.run_cycle_sara : SaraAnimation.idle_sara, true);
                break;

            case SaraAnimation.push_start_sara:
                SetAnimation(SaraAnimation.push_cycle_sara, true);
                break;

            case SaraAnimation.pull_start_sara:
                SetAnimation(SaraAnimation.pull_cycle_sara, true);
                break;

            case SaraAnimation.push_end_sara:
            case SaraAnimation.pull_end_sara:
                if (wasInAir)
                    SetAnimation(wasFalling ? SaraAnimation.jump_down_cycle_sara : SaraAnimation.jump_up_cycle_sara, true);
                else if (wasMoving)
                    SetAnimation(SaraAnimation.run_cycle_sara, true);
                else
                    SetAnimation(SaraAnimation.idle_sara, true);
                break;
        }
    }

    void SetAnimation(SaraAnimation anim, bool loop)
    {
        if (currentAnimation != anim || spineAnimation.loop != loop)
        {
            currentAnimation = anim;
            spineAnimation.loop = loop;
            spineAnimation.AnimationName = currentAnimation.ToString();
        }
    }
}
