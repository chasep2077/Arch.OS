using System.Collections.Generic;

namespace ArchOS
{
    public class Level
    {
        private readonly List<Level> _children = new List<Level>();

        public Level Parent { get; private set; }
        public IReadOnlyList<Level> Children => _children.AsReadOnly();
        public Function Function { get; private set; }

        public Level(Function function = null)
        {
            Function = function ?? new NoneFunction();
        }

        public bool AddChild(Level child)
        {
            if (child == null || _children.Contains(child) || child.Parent != null) return false;

            _children.Add(child);
            child.Parent = this;
            return true;
        }

        public bool RemoveChild(Level child)
        {
            if (!_children.Remove(child)) return false;

            child.Parent = null;
            return true;
        }

        public void SetFunction(Function function)
        {
            Function = function ?? new NoneFunction();
        }
    }
}
