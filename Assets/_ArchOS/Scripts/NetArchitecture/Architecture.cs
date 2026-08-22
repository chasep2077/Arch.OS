using NUnit.Framework;
using System.Collections.Generic;

namespace ArchOS
{
    public class Architecture
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Level Root { get; private set; }

        public Architecture(string name = "NewArch", string description = "", Level root = null)
        {
            Name = string.IsNullOrEmpty(name) ? "NewArch" : name;
            Description = description ?? "";
            Root = root ?? new Level();
        }

        public void SetName(string name)
        {
            Name = string.IsNullOrEmpty(name) ? "NewArch" : name;
        }

        public void SetDescription(string description)
        {
            Description = description ?? "";
        }

        public bool AddLevel(Level level, Level target)
        {
            if (level == null || target == null) return false;

            return target.AddChild(level);
        }

        // Removing a level promotes its first child into its position.
        // Remaining children become children of the promoted level.
        public bool RemoveLevel(Level level)
        {
            if (level == null || (level == Root && level.Children.Count == 0)) return false;

            Level parent = level.Parent;
            List<Level> children = new List<Level>(level.Children);

            if (parent == null)
            {
                // The level being removed is the root.
                Root = level.Children[0];

                for(int i = 1; i < level.Children.Count; i++)
                {
                    Root.AddChild(level.Children[i]);
                }
            }
            else
            {
                // Promotes all children to the removed level's parent.
                for (int i = 0; i < level.Children.Count; i++)
                {
                    parent.AddChild(level.Children[i]);
                }

                parent.RemoveChild(level);
            }
            
            // Detaches all children from the removed level.
            foreach (Level child in children)
            {
                level.RemoveChild(child);
            }

            return true;
        }
    }
}
