using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CookieNoir.VDay
{
    public class JigsawButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private Vector3 _defaultScale = Vector3.one;
        [SerializeField] private Vector3 _changedScale = new(1.5f, 1.5f, 1.5f);
        [SerializeField] private AnimationCurve _toChangedScaleCurve;
        [SerializeField, Min(0f)] private float _toChangedScaleDuration = 0.3f;
        [SerializeField] private AnimationCurve _toDefaultScaleCurve;
        [SerializeField, Min(0f)] private float _toDefaultScaleDuration = 0.3f;
        [SerializeField] private Graphic _targetGraphic;
        [SerializeField] private Gradient _onClickColorGradient;
        [SerializeField, Min(0f)] private float _colorChangeDuration = 0.3f;
        [field: SerializeField] public UnityEvent OnClick { get; private set; }
        private IEnumerator _enterExitCoroutine;
        private IEnumerator _onClickCoroutine;

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.SetAsLastSibling();
            ChangeScale(_changedScale, _toChangedScaleCurve, _toChangedScaleDuration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ChangeScale(_defaultScale, _toDefaultScaleCurve, _toDefaultScaleDuration);
        }

        private void ChangeScale(Vector3 targetScale, AnimationCurve curve, float duration)
        {
            if (transform.localScale == targetScale)
            {
                return;
            }
            if (!isActiveAndEnabled ||
                curve == null ||
                duration <= 0f)
            {
                transform.localScale = targetScale;
                return;
            }
            if (_enterExitCoroutine != null)
            {
                StopCoroutine(_enterExitCoroutine);
            }
            _enterExitCoroutine = ChangeScaleOverTime(transform, targetScale, curve, duration);
            StartCoroutine(_enterExitCoroutine);
        }

        private static IEnumerator ChangeScaleOverTime(Transform transform, Vector3 targetScale, AnimationCurve curve, float duration)
        {
            Vector3 initialScale = transform.localScale;
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                t = curve.Evaluate(t);
                transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localScale = targetScale;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            OnClick.Invoke();
            PlayOnClickAnimation();
        }

        private void PlayOnClickAnimation()
        {
            if (!isActiveAndEnabled ||
                _targetGraphic == null ||
                _onClickColorGradient == null ||
                _toDefaultScaleDuration <= 0f)
            {
                return;
            }
            if (_onClickCoroutine != null)
            {
                StopCoroutine(_onClickCoroutine);
            }
            _onClickCoroutine = ChangeColorOverTime(_targetGraphic, _onClickColorGradient, _colorChangeDuration);
            StartCoroutine(_onClickCoroutine);
        }

        private static IEnumerator ChangeColorOverTime(Graphic targetGraphic, Gradient gradient, float duration)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                targetGraphic.color = gradient.Evaluate(t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            targetGraphic.color = gradient.Evaluate(1f);
        }

        private void OnDisable()
        {
            _enterExitCoroutine = null;
            _onClickCoroutine = null;
        }
    }
}
