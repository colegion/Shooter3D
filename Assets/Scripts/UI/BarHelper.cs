using System;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class BarHelper : MonoBehaviour
    {
        [SerializeField] private BarType type;
        [SerializeField] private SpriteRenderer slider;

        private Vector3 _initialScale;
        private float _initialHorizontalScale;

        private void OnEnable()
        {
            Initialize();
        }

        private void LateUpdate()
        {
            if (Camera.main != null) transform.rotation = Camera.main.transform.rotation;
        }

        public void Initialize()
        {
            _initialScale = slider.transform.localScale;
            _initialHorizontalScale = _initialScale.x;
        }

        public void UpdateSelf(float newValue)
        {
            var ratio = newValue / Utilities.BaseHealth;
            var newHorizontalScale = ratio * _initialHorizontalScale;

            slider.transform.localScale = new Vector3(newHorizontalScale, _initialScale.y, _initialScale.z);
        }

        public BarType GetBarType()
        {
            return type;
        }
    }
}
