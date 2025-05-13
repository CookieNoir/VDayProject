using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CookieNoir.VDay
{
    public class EnterCodeScreen : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TMP_Text _text;
        [field: SerializeField] public UnityEvent OnShow { get; private set; }
        [field: SerializeField] public UnityEvent OnHide { get; private set; }
        private string _targetCode;
        private UnityEvent _onSuccess;
        private bool _isShown = false;

        public void Show(string text, string targetCode, UnityEvent onSuccess)
        {
            if (_isShown ||
                _inputField == null)
            {
                return;
            }
            if (_text != null)
            {
                _text.SetText(text);
            }
            _targetCode = targetCode;
            _onSuccess = onSuccess;
            _inputField.SetTextWithoutNotify(string.Empty);
            _isShown = true;
            OnShow.Invoke();
        }

        private void Validate(string code)
        {
            if (!_isShown ||
                _onSuccess == null ||
                code == null)
            {
                return;
            }
            if (code.ToUpper() == _targetCode)
            {
                _onSuccess.Invoke();
            }
        }

        public void Hide()
        {
            if (!_isShown)
            {
                return;
            }
            _isShown = false;
            OnHide.Invoke();
        }

        private void OnEnable()
        {
            if (_inputField != null)
            {
                _inputField.onEndEdit.AddListener(Validate);
            }
        }

        private void OnDisable()
        {
            if (_inputField != null)
            {
                _inputField.onEndEdit.RemoveListener(Validate);
            }
        }
    }
}
