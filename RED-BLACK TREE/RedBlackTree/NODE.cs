using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    class NODE
    {
        public NODE left;
        public NODE right;
        public NODE parent;
        public int data;
        public string color;
        public NODE(int data, string color)
        {
            this.data = data;
            left = null;
            right = null;
            parent = null;
            this.color = color;

        }
        public NODE() { }
    }

}
