using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace ArchOS
{
    public sealed class IceFunction : Function
    {
        private const int k_MaxIce = 3;

        private readonly List<Ice> _ice = new List<Ice>();

        public override FunctionType Type => FunctionType.ICE;
        public IReadOnlyList<Ice> Ice => _ice.AsReadOnly();

        public bool AddIce(Ice ice)
        {
            if (ice == null || _ice.Count >= k_MaxIce || _ice.Contains(ice)) return false;

            _ice.Add(ice);
            return true;
        }

        public bool RemoveIce(Ice ice)
        {
            return _ice.Remove(ice);
        }
    }
}
