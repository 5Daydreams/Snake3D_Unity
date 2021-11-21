using _Code.Scriptables.TrackableValue;
using UnityEngine;
using UnityEngine.UI;

namespace _Code.UI
{
    [RequireComponent(typeof(Text))]
    public class ScoreTracker : MonoBehaviour
    {
        [SerializeField] private TrackableInt _scoreValue;
        [SerializeField] private TrackableInt _highScoreValue;
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
            _scoreValue.CallbackOnValueChanged.AddListener(AddToPlayerCurrentScore);
            _scoreValue.Value = 0;
        }

        private void OnDisable()
        {
            _scoreValue.CallbackOnValueChanged.RemoveListener(AddToPlayerCurrentScore);
        }

        private void AddToPlayerCurrentScore(int newScore)
        {
            // Still need to do some math here
            OverwritePreviousScore(newScore);

            if (_scoreValue.Value > _highScoreValue.Value)
            {
                _highScoreValue.Value = _scoreValue.Value;
            }
        }

        private void OverwritePreviousScore(int eventValue)
        {
            _textField.text = eventValue.ToString();
        }
    }
}
