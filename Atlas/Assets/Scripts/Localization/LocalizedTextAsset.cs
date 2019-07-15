// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Localization
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "LocalizedTextAsset", menuName = "Localization/Text Asset")]
    public class LocalizedTextAsset : LocalizedAsset<TextAsset>
    {
        [Serializable]
        private class TextAssetLocaleItem : LocaleItem<TextAsset> { };

        [SerializeField]
        private TextAssetLocaleItem[] m_LocaleItems = new TextAssetLocaleItem[1];

        public override LocaleItemBase[] LocaleItems { get { return m_LocaleItems; } }
    }
}
