using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CookieNoir.VDay
{
    public class JigsawButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private Vector3 _defaultScale = Vector3.one;
        [SerializeField] private Vector3 _changedScale = new(1.5f,1.5f,1.5f);
        [SerializeField, Min(0f)] private float _changeTime = 0.3f;
        private IEnumerator _coroutine;

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.SetAsLastSibling();
            ChangeScale(_changedScale);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ChangeScale(_defaultScale);
        }

        private void ChangeScale(Vector3 targetScale)
        {
            if (transform.localScale == targetScale)
            {
                return;
            }
            if (!isActiveAndEnabled)
            {
                transform.localScale = targetScale;
                return;
            }
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = ChangeScaleOverTime(targetScale, _changeTime);
            StartCoroutine(_coroutine);
        }

        private IEnumerator ChangeScaleOverTime(Vector3 targetScale, float duration)
        {
            Vector3 initialScale = transform.localScale;
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                t = Mathf.SmoothStep(0f, 1f, t);
                transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localScale = targetScale;
        }
    }
}
