using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

/**
 * Class: StateMachine.cs
 * Desc: StateMachine Base Class
 * Author: Jinwoo Park
 * Date: 2016.2.19
 */
public class StateMachine<T> : MonoBehaviour {
	List<States<T>> stateList = new List<States<T>> ();
	States<T> currentStates;

	public delegate IEnumerator CurrentStateDelegate();

	/** 
	 * operator override ==,!=
	 */
	public static bool operator ==(StateMachine<T> msm, T state) {
		
		if (msm != null && msm.currentStates != null) {
			return msm.currentStates.ID.Equals (state);
		} 
		return msm == null && state == null;
	}
	public static bool operator !=(StateMachine<T> msm, T state) {
		if (msm != null && msm.currentStates!=null) {
			return !msm.currentStates.ID.Equals(state);
		} 
		return (msm != null || state != null);

	}

	//avoid compile warnning
	public override bool Equals (System.Object other) {
		return object.ReferenceEquals (this, other);
	}
	//avoid compile warnning
	public override int GetHashCode () {
		return base.GetHashCode ();
	}

	/**
	 * Get States class
	 * (Dic으로는 제네릭(T) Key타입으로 검색이 불가능 하므로 List로 교체)
	 */
	public States<T> GetStates(T id) {
		foreach (States<T> t in stateList) {
			if (t.ID.Equals (id)) {
				return t;
			} 
		}
		return null;

	}

	/**
	 * AddState
	 * @param States<T> - state add
	 */
	public void AddState(States<T> states) {
		stateList.Add (states);
	}
	public void AddState(T stateID, CurrentStateDelegate statedelegate=null) {

		AddState (new States<T> (stateID, statedelegate));
	}

	/**
	 * Remove State
	 * @param States<T> - state remove
	 */
	public void RemoveState(States<T> states) {
		stateList.Remove (states);
	}

	/**
	 * Remove State by StateID
	 * @param T - remove id
	 */
	public void RemoveState(T id) {
		stateList.Remove (GetStates(id));
	}

	/**
	 * Change State
	 * @param T - to State id
	 */
	public void ChangeState(T stateId) {
		States<T> states=GetStates(stateId);

		if(this!=stateId) {
			if (currentStates != null) {
				StopCoroutine (currentStates.stateDelegate.Method.Name);
			}
			if (states != null) {	// 목표 States를 찾지 못한경우.
				currentStates = states;
				Debug.Log (states.stateDelegate());
				StartCoroutine (states.stateDelegate.Method.Name);


			} else
            {
                currentStates = null;
            }


		}

	}

}

/**
 * Class: StateMachine.cs
 * Desc: States Class
 * Author: Jinwoo Park
 * Date: 2016.2.19
 */
public class States<T>{
	T stateID;
	public T ID { get{return stateID;}}

	public StateMachine<T>.CurrentStateDelegate stateDelegate;

	public States(T id,StateMachine<T>.CurrentStateDelegate dele) {
		stateID = id;
		stateDelegate=dele;
		Debug.Log (stateDelegate.Method.Name);
	}

}
