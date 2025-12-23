using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MagnetFix_NativeModloader
{
    public class WorldBoxMod : MonoBehaviour
    {
        public void Awake()
        {
            Debug.Log($"{MethodBase.GetCurrentMethod().DeclaringType.Namespace} loading...");

            var libraries = new Dictionary<string, byte[]>
        {
            { "Mono.Cecil", Assemblies.Mono_Cecil },
            { "MonoMod.Utils", Assemblies.MonoMod_Utils },
            { "MonoMod.RuntimeDetour", Assemblies.MonoMod_RuntimeDetour },
            { "0Harmony", Assemblies._0Harmony }
        };

            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Select(a => a.GetName().Name)
                .ToList();

            foreach (var lib in libraries)
            {
                try
                {
                    if (!loadedAssemblies.Contains(lib.Key))
                    {
                        Assembly.Load(lib.Value);
                        Debug.Log($"[{MethodBase.GetCurrentMethod().DeclaringType.Namespace}] {lib.Key} loaded from memory");
                    }
                    else
                    {
                        Debug.Log($"[{MethodBase.GetCurrentMethod().DeclaringType.Namespace}] {lib.Key} is already loaded, skipping");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType.Namespace}] Failed to load {lib.Key}: {e.Message}");
                }
            }

            GameObject gameObject = new GameObject(MethodBase.GetCurrentMethod().DeclaringType.Namespace);
            DontDestroyOnLoad(gameObject);
            gameObject.AddComponent<Main>();
        }
    }
}
