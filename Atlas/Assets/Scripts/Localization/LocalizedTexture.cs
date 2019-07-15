// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Localization
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "LocalizedTexture", menuName = "Localization/Texture")]
    public class LocalizedTexture : LocalizedAsset<Texture>
    {
        [Serializable]
        private class TextureLocaleItem : LocaleItem<Texture> { };

        [SerializeField]
        private TextureLocaleItem[] m_LocaleItems = new TextureLocaleItem[1];

        public override LocaleItemBase[] LocaleItems { get { return m_LocaleItems; } }
    }
}
