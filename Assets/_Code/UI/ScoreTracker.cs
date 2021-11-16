using System;
using System.Collections;
using System.Collections.Generic;
using _Code.Scriptables.SimpleValues;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TrackableInt _scoreValue;
    private Text _textField;

    private void Awake()
    {
        if (_scoreValue == null)
        {
            Debug.LogError("No Score value assigned to be tracked.");
            return;
        }

        _textField = this.GetComponent<Text>();
    }

    private void OnEnable()
    {
        _scoreValue.CallbackOnValueChanged.AddListener(OverwritePreviousScore);
        _scoreValue.SetValue(0);
    }

    private void OnDisable()
    {
        _scoreValue.CallbackOnValueChanged.RemoveListener(OverwritePreviousScore);
    }

    private void AddToPlayerCurrentScore(int newScore)
    {
        _textField.text = newScore.ToString();
    }

    private void OverwritePreviousScore(int eventValue)
    {
        _textField.text = eventValue.ToString();
    }
}
