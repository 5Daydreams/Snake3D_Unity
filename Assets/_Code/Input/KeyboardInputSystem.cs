using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Code.Input
{
    public class KeyboardInputSystem : MonoBehaviour
    {
        [SerializeField] private InputCode[] inputList;
        private bool[] lastInputState;

        private void Awake()
        {
            lastInputState = new bool[inputList.Length];
        }

        private void Update()
        {
            for (int i = 0; i < inputList.Length; i++)
            {
                InputCode inputCode = inputList[i];
            
                bool currentInputState = UnityEngine.Input.GetKey(inputCode.InputKey);
            
                CheckEventToTrigger(lastInputState[i],currentInputState, inputCode);
            
                lastInputState[i] = currentInputState;
            }
        }
    
        void CheckEventToTrigger(bool previousState, bool currentState, InputCode inputCode)
        {
            bool noInputTaken = !previousState && !currentState;

            if (noInputTaken)
            {
                return;
            }
        
            bool keyPressed = !previousState && currentState;
            if (keyPressed)
            {
                inputCode.InputTapped.Invoke();
                return;
            }
        
            bool keyReleased = previousState && !currentState;
            if (keyReleased)
            {
                inputCode.InputReleased.Invoke();
                return;
            }

            bool keyHeld = previousState && currentState;
            if (keyHeld)
            {
                inputCode.InputHeld.Invoke();
                return;
            }
        }
    }

    [Serializable]
    public struct InputCode
    {
        [TextArea] public string Description;
        public string InputKey;
        public UnityEvent InputHeld;
        public UnityEvent InputTapped;
        public UnityEvent InputReleased;
    }
}