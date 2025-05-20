using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CookieNoir.VDay
{
    public class URLFromTextOpener : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _targetText;
        [SerializeField] private PointerEventData.InputButton _inputButton = PointerEventData.InputButton.Left;
        [SerializeField] private string _linkID = "url";

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_targetText == null ||
                eventData.button != _inputButton ||
                !TryFindURL(out string url))
            {
                return;
            }
            Application.OpenURL(url);
        }

        private bool TryFindURL(out string url)
        {
            url = null;
            bool result = false;
            var textInfo = _targetText.textInfo;
            if (textInfo == null)
            {
                return result;
            }
            var linkInfos = textInfo.linkInfo;
            if (linkInfos == null ||
                linkInfos.Length == 0)
            {
                return result;
            }
            for (int i = 0; i < linkInfos.Length; ++i)
            {
                var linkInfo = linkInfos[i];
                if (linkInfo.GetLinkID() != _linkID)
                {
                    continue;
                }
                var linkText = linkInfo.GetLinkText();
                if (string.IsNullOrEmpty(linkText))
                {
                    continue;
                }
                url = linkText;
                result = true;
                break;
            }
            return result;
        }
    }
}
