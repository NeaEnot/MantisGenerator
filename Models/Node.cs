﻿using System.Collections.Generic;

namespace MantisGenerator.Models
{
    public class Node
    {
        public Node()
        {
            Children = new List<Node>();
        }

        public string Name { get; set; }
        public List<Node> Children { get; }
    }
}
