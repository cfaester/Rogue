﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDetection : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 2;

    private bool _isShowingButton;
    private GameObject _activeOpponent;

    void OnTriggerStay(Collider other)
    {
        float dist = (transform.position - other.transform.position)
            .sqrMagnitude;

        if (other.gameObject == _activeOpponent && dist > maxDistance &&
            _isShowingButton)
        {
            JuiceController.Instance.DisableCombat();
            _isShowingButton = false;
            _activeOpponent = null;
        }
        else if ((other.tag == "AI" || other.tag == "Player") &&
                 dist < maxDistance &&
                 GameManager.Instance.IsPlayerTurn(gameObject))
        {
            JuiceController.Instance.EnableCombat(gameObject, other.gameObject);
            _isShowingButton = true;
            _activeOpponent = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "AI" || other.tag == "Player") {
            JuiceController.Instance.DisableCombat();
            _isShowingButton = false;
            _activeOpponent = null;
        }
    }
}
