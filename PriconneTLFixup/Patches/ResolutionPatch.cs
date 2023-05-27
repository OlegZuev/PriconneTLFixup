﻿using Cute;
using HarmonyLib;
using UnityEngine;

namespace PriconneTLFixup.Patches;

/**
 * Technically not a problem with the translation patch, but this allows to scale up the game window to full resolution.
 */
[HarmonyPatch(typeof(StandaloneWindowResize), nameof(StandaloneWindowResize.getOptimizedWindowSize))]
public class ResolutionPatch
{
    public static bool Prefix(ref Vector3 __result, int _width, int _height)
    {
        var resolution = Screen.currentResolution;
        var aspectRatio = 1.7777778f;
        float num = _width / (float)_height;
        if (num < aspectRatio)
        {
            _height = Mathf.Clamp(_height, (int)((float)resolution.height / 10), (int)resolution.height);
            _width = Mathf.RoundToInt(_height * aspectRatio);
        }
        else if (num > aspectRatio)
        {
            _width = Mathf.Clamp(_width, (int)((float)resolution.width / 10), (int)resolution.width);
            _height = Mathf.RoundToInt(_width / aspectRatio);
        }

        __result = new Vector3(_width, _height, num);
        return false;
    }
}

[HarmonyPatch(typeof(StandaloneWindowResize), nameof(StandaloneWindowResize.DisableMaximizebox))]
public class MaximizePatch
{
    public static bool Prefix()
    {
        return false;
    }
}