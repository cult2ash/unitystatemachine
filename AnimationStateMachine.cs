using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

/**
 * Class: AnimationStateMachine.cs
 * Desc: AnimationStateMachine Base Class
 * Author: Jinwoo Park
 * Date: 2016.8.19
 */
public class AnimationStateMachine : StateMachine<int> {

    int currentStateHash;
    Animator animator;

	public static bool operator ==(AnimationStateMachine asm, string state) {
		return asm.currentStateHash == Animator.StringToHash (state);

	}
	public static bool operator !=(AnimationStateMachine asm, string state) {
		return asm.currentStateHash != Animator.StringToHash (state);

	}

	//avoid compile warnning
	public override bool Equals (System.Object other) {
		return object.ReferenceEquals (this, other);
	}

	//avoid compile warnning
	public override int GetHashCode () {
		return base.GetHashCode ();
	}

    public void Play(string name)
    {
        animator.Play(name);
    }

    public void AddState(string name, CurrentStateDelegate _delegate)
    {
        AddState(Animator.StringToHash(name), _delegate);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       
        if(currentStateHash != animator.GetCurrentAnimatorStateInfo(0).fullPathHash)
        {
            currentStateHash = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            ChangeState(currentStateHash);

        }

    }

}
