using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBullet : MonoBehaviour {
    [SerializeField] private float timeInAir = 1f;
    [SerializeField] private float targetScale = .5f;
    [SerializeField] private float additionalY = 1f; 
    private Vector2 targetPos;
    private float initialScale;
    private float currentAdditionalY;
    

    private void Start() {
        initialScale = transform.localScale.x;
        
        Tweener.AddTween(() => transform.position.x, (x) => SetPositionX(x), targetPos.x, timeInAir,
            TweenMethods.Linear);
        Tweener.AddTween(() => transform.position.y, (x) => SetPositionY(x), targetPos.y, timeInAir,
            TweenMethods.Linear);
        
        Tweener.AddTween(() => currentAdditionalY, (x) => AddAdditionalY(x), additionalY, timeInAir / 2f, TweenMethods.SoftLog, () => {
            Tweener.AddTween(() => currentAdditionalY, (x) => AddAdditionalY(x), 0f, timeInAir / 2f, TweenMethods.Quadratic);
        });
        
        Tweener.AddTween(() => transform.localScale.x, (x) => SetScaleX(x), targetScale, timeInAir / 2f,
            TweenMethods.Linear,
            () => {
                Tweener.AddTween(() => transform.localScale.x, (x) => SetScaleX(x), initialScale, timeInAir / 2f);
            });
        
        Tweener.AddTween(() => transform.localScale.y, (x) => SetScaleY(x), targetScale, timeInAir / 2f,
            TweenMethods.Linear,
            () => {
                Tweener.AddTween(() => transform.localScale.y, (x) => SetScaleY(x), initialScale, timeInAir / 2f);
            });
    }

    private void SetPositionX(float x) {
        transform.position = new Vector2(x, transform.position.y);
    }
    
    private void SetPositionY(float y) {
        transform.position = new Vector2(transform.position.x, y + currentAdditionalY);
    }

    private void SetScaleX(float x) {
        transform.localScale = new Vector2(x, transform.localScale.y);
    }
    
    private void SetScaleY(float y) {
        transform.localScale = new Vector2(transform.localScale.x, y);
    }

    private void AddAdditionalY(float y) {
        currentAdditionalY = y;
    }

    public void SetTargetLocation(Vector2 pos) {
        targetPos = pos;
    }
}