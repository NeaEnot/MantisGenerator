using System.Collections.Generic;

namespace MantisGenerator.Models
{
    public class Node
    {
        public string Name { get; set; }
        public int Probability { get; set; }

        public List<Node> Children { get; private set; }

        public string Color { get { return Probability > 0 ? "Black" : "Gray"; } }

        public Node()
        {
            Children = new List<Node>();
        }

        public bool Delete(Node node)
        {
            if (Children.Contains(node))
            {
                Children.Remove(node);
                return true;
            }
            else
            {
                foreach (Node child in Children)
                {
                    if (child.Delete(node))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
