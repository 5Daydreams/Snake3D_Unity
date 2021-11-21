using System.Collections;
using System.Collections.Generic;
using _Code.Scriptables.TrackableValue;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameOverTextUpdater : MonoBehaviour
{
    [SerializeField] private TrackableInt _scoreValue;
    private Text _text;
    void OnEnable()
    {
        _text = this.GetComponent<Text>();

        _text.text = _scoreValue.Value.ToString();
    }
}
