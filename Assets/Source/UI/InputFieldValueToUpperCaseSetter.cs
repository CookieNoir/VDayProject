using TMPro;
using UnityEngine;

namespace CookieNoir.VDay
{
    public class InputFieldValueToUpperCaseSetter : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;

        private void ToUpper(string value)
        {
            if (_inputField == null)
            {
                return;
            }
            value = value.ToUpper();
            _inputField.SetTextWithoutNotify(value);
        }

        private void OnEnable()
        {
            if (_inputField != null)
            {
                _inputField.onValueChanged.AddListener(ToUpper);
            }
        }

        private void OnDisable()
        {
            if (_inputField != null)
            {
                _inputField.onValueChanged.RemoveListener(ToUpper);
            }
        }
    }
}
