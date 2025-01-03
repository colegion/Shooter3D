using UnityEngine;

namespace Helpers
{
    public class BarHelper : MonoBehaviour
    {
        [SerializeField] private BarType type;
        [SerializeField] private SpriteRenderer slider;

        private Vector3 _initialScale;
        private Quaternion _initialRotation;
        private float _initialHorizontalScale;

        private void OnEnable()
        {
            Initialize();
        }

        private void LateUpdate()
        {
            transform.rotation = _initialRotation;
        }

        private void Initialize()
        {
            _initialRotation = transform.rotation;
            _initialScale = slider.transform.localScale;
            _initialHorizontalScale = _initialScale.x;
        }

        public void UpdateSelf(float newValue)
        {
            var ratio = newValue / Utilities.BaseHealth;
            var newHorizontalScale = ratio * _initialHorizontalScale;

            slider.transform.localScale = new Vector3(newHorizontalScale, _initialScale.y, _initialScale.z);
        }

        public void ResetScale()
        {
            slider.transform.localScale = _initialScale;
        }
    }
}
