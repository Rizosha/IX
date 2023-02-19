using System;
using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using EnemyScripts.Lion;
using UnityEngine;

public class LionAnimationEventController : MonoBehaviour
{
    private Animator _animator;
    private LionPlayerDamage _lionPlayerDamage;
    
    private void Awake() {
        _animator = GetComponent<Animator>();
        _lionPlayerDamage = GetComponent<LionStateManager>().lionPlayerDamage;
    }

    void RightSwipeStart() {
        _lionPlayerDamage.doDamage = true;
    }
    
    void RightSwipeEnd() {
        _lionPlayerDamage.doDamage = false;
    }
    
    void LeftSwipeStart() {
        _lionPlayerDamage.doDamage = true;
    }
    
    void LeftSwipeEnd() {
        _lionPlayerDamage.doDamage = false;
    }
}

