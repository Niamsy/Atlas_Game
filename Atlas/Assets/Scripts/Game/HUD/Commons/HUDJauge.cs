﻿using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD.Commons
{
    public class HUDJauge : MonoBehaviour
    {
        [Header("Objects references")]
        [SerializeField] private Image _fill;
        [SerializeField] private Image _prefill;
        [SerializeField] private Text _valueText;
        
        [Header("Parameters variables")]
        [Tooltip("The speed at which the fill try to reach the prefill when filling")]
        public float FillingSpeed = 0.1f;
        [Tooltip("The speed at which the fill try to reach the prefill when emptying")]
        public float EmptyingSpeed = 2f;
        [Tooltip("See string.Format function, {0} in the actual value, {1} is the max value, {2} is the percentage")]
        public string TextFormat = "{0:F2} / {1:F2}";
        
        private float _targetValue;
        private float _value;
        private float _maxValue;
        
        private float ActualPercentage01
        {
            get { return (_value / _maxValue); }
        }
        
        private float ActualTargetPercentage01
        {
            get { return (_targetValue / _maxValue); }
        }
        public bool IsMoving
        {
            get { return (_value != _targetValue); }
        }

        public void Initialize(float value, float maxValue)
        {
            UpdateTargetValue(value);
            UpdateValue(value);
            _maxValue = maxValue;
            UpdateText();
        }
        
        public void SetValue(float newValue)
        {
            UpdateTargetValue(newValue);
        }

        private void UpdateValue(float newValue)
        {
            _value = newValue;
            _fill.fillAmount = ActualPercentage01;
        }

        private void UpdateTargetValue(float newValue)
        {
            _targetValue = newValue;
            _prefill.fillAmount = ActualTargetPercentage01;
        }
        
        private void Update()
        {
            if (IsMoving)
            {
                float val;
                if (_targetValue > _value)
                    UpdateValue(Mathf.MoveTowards(_value, _targetValue, (FillingSpeed * _maxValue) * Time.deltaTime));
                else
                    UpdateValue(Mathf.MoveTowards(_value, _targetValue, (EmptyingSpeed * _maxValue) * Time.deltaTime));
                UpdateText();
            }
        }

        private void UpdateText()
        {
            _valueText.text = string.Format(TextFormat, _value, _maxValue, (_value / _maxValue) * 100f);
        }
    }
}