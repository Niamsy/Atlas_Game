// Copyright (c) H. Ibrahim Penekli. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Localization;
using UnityEditor;
using UnityEngine;

namespace Localization.Editor .Localization.Editor
{
    [InitializeOnLoad]
    public class UnityPackageInitializer
    {
        static UnityPackageInitializer()
        {
            // This will create settings if not exist.
            var localizationSettings = LocalizationSettings.Instance;
            if (!localizationSettings)
            {
                Debug.LogWarning("LocalizationSettings not found. Please create manually.");
            }
        }
    }
}
