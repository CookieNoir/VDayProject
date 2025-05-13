using System.Collections;
using UnityEngine;

namespace CookieNoir.VDay
{
    public class CanvasGroupAnimator : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AnimationCurve _showAlphaCurve;
        [SerializeField, Min(0f)] private float _showDuration = 0.3f;
        [SerializeField] private AnimationCurve _hideAlphaCurve; // Will be evaluated as 1f - value
        [SerializeField, Min(0f)] private float _hideDuration = 0.3f;
        private IEnumerator _coroutine;

        public void Show()
        {
            if (_canvasGroup == null)
            {
                return;
            }
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.gameObject.SetActive(true);
            if (_showAlphaCurve == null ||
                _showDuration <= 0f ||
                !isActiveAndEnabled)
            {
                return;
            }
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = ShowOverTime(_canvasGroup, _showAlphaCurve, _showDuration);
            StartCoroutine(_coroutine);
        }

        private static IEnumerator ShowOverTime(CanvasGroup canvasGroup, AnimationCurve curve, float duration)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                canvasGroup.alpha = curve.Evaluate(t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = curve.Evaluate(1f);
        }

        public void Hide()
        {
            if (_canvasGroup == null)
            {
                return;
            }
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            if (_hideAlphaCurve == null ||
                _hideDuration <= 0f ||
                !isActiveAndEnabled)
            {
                _canvasGroup.gameObject.SetActive(false);
                return;
            }
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = HideOverTime(_canvasGroup, _hideAlphaCurve, _hideDuration);
            StartCoroutine(_coroutine);
        }

        private static IEnumerator HideOverTime(CanvasGroup canvasGroup, AnimationCurve curve, float duration)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                canvasGroup.alpha = 1f - curve.Evaluate(t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1f - curve.Evaluate(1f);
            canvasGroup.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _coroutine = null;
        }
    }
}
