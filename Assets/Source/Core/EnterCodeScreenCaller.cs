using UnityEngine;
using UnityEngine.Events;

namespace CookieNoir.VDay
{
    public class EnterCodeScreenCaller : MonoBehaviour
    {
        [SerializeField] private EnterCodeScreen _enterCodeScreen;
        [SerializeField] private string _text;
        [SerializeField] private string _targetCode = string.Empty;
        [SerializeField] private bool _showOnStart;
        [field: SerializeField] public UnityEvent OnSuccess { get; private set; }

        private void Start()
        {
            if (_showOnStart)
            {
                Show();
            }
        }

        public void Show()
        {
            if (_enterCodeScreen == null)
            {
                return;
            }
            _enterCodeScreen.Show(_text, _targetCode, OnSuccess);
        }

        private void OnValidate()
        {
            _targetCode = _targetCode.ToUpper();
        }
    }
}
