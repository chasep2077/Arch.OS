using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace ArchOS
{
    public class ModuleManager : MonoBehaviour
    {
        public static ModuleManager Instance { get; private set; }

        [SerializeField] private List<ModuleSO> allModules = new List<ModuleSO>();

        private readonly Dictionary<string, ModuleSO> _moduleLookup = new Dictionary<string, ModuleSO>();

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            InitializeLookup();
        }

        private void InitializeLookup()
        {
            _moduleLookup.Clear();
            foreach (ModuleSO module in allModules)
            {
                if (module == null) continue;

                if (!_moduleLookup.ContainsKey(module.Name))
                {
                    _moduleLookup.Add(module.Name, module);
                }
                else
                {
                    Debug.LogWarning($"Duplicate module name found in ModuleManager: {module.Name}");
                }
            }
        }

        public ModuleSO GetModuleData(string moduleName)
        {
            if(_moduleLookup.TryGetValue(moduleName, out ModuleSO data))
            {
                return data;
            }

            Debug.LogError($"Module '{moduleName}' not found in ModuleManager!");
            return null;
        }

    }
}
