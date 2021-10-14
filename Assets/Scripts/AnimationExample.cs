using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExample : MonoBehaviour
{
    public Animation gameObjAnimation;

    public AnimationClip run;
    public AnimationClip idle;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayAnimation(run);
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            PlayAnimation(idle);
        }
    }

    private void PlayAnimation(AnimationClip c)
    {
        gameObjAnimation.CrossFade(c.name);
    }
}
