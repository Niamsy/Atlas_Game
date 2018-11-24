﻿// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace Localization
{
	/// <summary>
	/// 
	/// </summary>
    public abstract class LocalizedAssetBehaviour : MonoBehaviour
    {
		/// <summary>
		/// 
		/// </summary>
        protected abstract void UpdateComponentValue();

        protected virtual void OnEnable()
        {
            UpdateComponentValue();
            LocalizationManager.Instance.LocaleChanged += Localization_LocaleChanged;
        }

        protected virtual void OnDisable()
        {
            LocalizationManager.Instance.LocaleChanged -= Localization_LocaleChanged;
        }

        private void Localization_LocaleChanged(object sender, LocaleChangedEventArgs e)
        {
            UpdateComponentValue();
        }
    }
}
