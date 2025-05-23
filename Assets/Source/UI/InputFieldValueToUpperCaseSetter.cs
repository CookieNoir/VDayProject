using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CookieNoir.VDay
{
    public class InputFieldValueToUpperCaseSetter : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private InputField _legacyInputField;

        private void ToUpper(string value)
        {
            if (_inputField == null)
            {
                return;
            }
            value = value.ToUpper();
            _inputField.SetTextWithoutNotify(value);
        }

        private void ToUpperLegacy(string value)
        {
            if (_legacyInputField == null)
            {
                return;
            }
            value = value.ToUpper();
            _legacyInputField.SetTextWithoutNotify(value);
        }

        private void OnEnable()
        {
            if (_inputField != null)
            {
                _inputField.onValueChanged.AddListener(ToUpper);
            }
            if (_legacyInputField != null)
            {
                _legacyInputField.onValueChanged.AddListener(ToUpperLegacy);
            }
        }

        private void OnDisable()
        {
            if (_inputField != null)
            {
                _inputField.onValueChanged.RemoveListener(ToUpper);
            }
            if (_legacyInputField != null)
            {
                _legacyInputField.onValueChanged.AddListener(ToUpperLegacy);
            }
        }
    }
}
