using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    class REDBLACKTREE
    {

        NODE root;
        NODE LatestNode, U, V;
        //lastest node store the refrence of the lastest node to be added to restore the property viloation of red black tree...
        //V is the node to be deleted.
        //u is the node which replaces deleted node (v).
        bool IsleftU = false;
        //to check if the deleted node is left child of it's parent...
        bool IsrightU = false;
        //to check if the deleted node is right child of it's parent...

        public REDBLACKTREE()
        {//setting console background color for better output...
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
        }

        public void Add(int i)
        {

            root = Add(i, root);
            InsertFixedUp(LatestNode);


        }
        private NODE Add(int i, NODE temp)
        {
            if (root == null)
            {
                NODE newnode = new NODE(i, color.black);
                root = newnode;
                return root;
            }
            else if (temp == null)
            {
                NODE newnode = new NODE(i, color.red);
                LatestNode = newnode;
                return newnode;
            }
            else if (i > temp.data)
            {
                temp.right = Add(i, temp.right);
                temp.right.parent = temp;
                return temp;
            }
            else if (i < temp.data)
            {
                temp.left = Add(i, temp.left);
                temp.left.parent = temp;
                return temp;
            }
            else
                //element already exist so return the node...
                return temp;
        }
        public void inorder()
        {
            inorder(root);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        private void inorder(NODE temp)
        {

            if (temp != null)
            {

                inorder(temp.left);
                if (temp.color == color.red)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(temp.data + "=>Color=" + temp.color);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(temp.data + "=>Color=" + temp.color);

                }
                inorder(temp.right);
            }
        }
        private NODE minimum(NODE node)
        {
            if (node.left == null)
            {
                return node;
            }
            else
            {
                return minimum(node.left);
            }
        }
        private NODE GetSuccessor(NODE node)
        {
            if (node != null)
            {
                return minimum(node.right);
            }
            return null;
        }
        public void delete(int i)
        {

            root = delete(i, root);

            if (V!=null&&IsBlack(V))
            {


                DeletionFixed(U);
            }

        }
        private NODE delete(int i, NODE node)
        {
            if (node != null)
            {
                if (node.data == i)
                {
                    V = node;
                    IsleftU = IsLeft(node);
                    IsrightU = IsRight(node);
                    if (node.left == null && node.right == null)
                    {
                        //u=node beacuse u is null so for the purpose of fix up set node ref in u
                        U = node;

                        return null;
                    }
                    else if (node.left == null && node.right != null)
                    {

                        node.right.parent = node.parent;
                        node = node.right;
                        U = node;
                        return node;
                    }
                    else if (node.right == null && node.left != null)
                    {
                        node.left.parent = node.parent;
                        node = node.left;
                        U = node;
                        return node;
                    }
                    else
                    {
                        node.data = GetSuccessor(node).data;
                        node.right = delete(node.data, node.right);

                        return node;
                    }
                }
                else
                {
                    if (i > node.data)
                    {
                        node.right = delete(i, node.right);
                        return node;
                    }
                    else
                    {
                        node.left = delete(i, node.left);
                        return node;
                    }
                }

            }
            else
                return null;
        }
        public void search(int i)
        {
            NODE x = search(i, root);
            if (x == null)
                Console.WriteLine("Value not found");
            else
                Console.WriteLine("Value found");

        }
        private NODE search(int i, NODE node)
        {
            if (node == null)
                return null;
            else if (i == node.data)
            {
                return node;
            }
            else if (i > node.data)
            {
                return search(i, node.right);
            }
            else
            {
                return search(i, node.left);
            }
        }

        private NODE rotateleft(NODE node)
        {
            NODE temp = node.right;
            node.right = temp.left;
            if (temp.left != null)
            {
                temp.left.parent = node;
            }
            temp.parent = node.parent;
            if (node.parent == null)
                root = temp;
            else if (node == node.parent.left)
            {
                node.parent.left = temp;
            }
            else
            {
                node.parent.right = temp;
            }
            temp.left = node;
            node.parent = temp;
            return temp;
        }

        private NODE rotateright(NODE node)
        {

            NODE temp = node.left;
            node.left = temp.right;
            if (temp.right != null)
            {
                temp.right.parent = node;
            }
            temp.parent = node.parent;
            if (node.parent == null)
                root = temp;
            else if (node == node.parent.left)
            {
                node.parent.left = temp;
            }
            else
            {
                node.parent.right = temp;
            }
            temp.right = node;
            node.parent = temp;
            return temp;
        }
        private void InsertFixedUp(NODE newnode)
        {
            NODE uncle;
            while (IsRed(newnode) && (newnode.parent != null && IsRed(newnode.parent)))
            {
                //case1 uncle is red...
                uncle = GetUncle(newnode);
                if (IsRed(uncle))
                {
                    uncle.color = color.black;
                    newnode.parent.color = color.black;
                    GetGrandParent(newnode).color = color.red;
                    newnode = GetGrandParent(newnode);
                }
                else
                {//case uncle black...
                    if (IsRight(newnode.parent) && IsRight(newnode))
                    {
                        newnode = rotateleft(GetGrandParent(newnode));
                        newnode.color = color.black;
                        newnode.right.color = color.red;
                        newnode.left.color = color.red;
                    }
                    else if (IsRight(newnode.parent) && IsLeft(newnode))
                    {
                        newnode = rotateright(newnode.parent);
                        newnode = rotateleft(newnode.parent);
                        newnode.color = color.black;

                        newnode.left.color = color.red;
                        newnode.right.color = color.red;
                    }
                    else if (IsLeft(newnode.parent) && IsLeft(newnode))
                    {
                        newnode = rotateright(GetGrandParent(newnode));
                        newnode.color = color.black;
                        newnode.right.color = color.red;
                        newnode.left.color = color.red;
                    }

                    else if (IsLeft(newnode.parent) && IsRight(newnode))
                    {
                        newnode = rotateleft(newnode.parent);
                        newnode = rotateright(newnode.parent);
                        newnode.color = color.black;
                        newnode.left.color = color.red;
                        newnode.right.color = color.red;

                    }
                }
                if (newnode.parent == null)
                {
                    root = newnode;
                    root.color = color.black;
                }
            }
        }
        private NODE GetUncle(NODE newnode)
        {
            NODE uncle;
            if (newnode.parent == null)
                return null;
            else if (newnode.parent == newnode.parent.parent.left)
            {
                return uncle = newnode.parent.parent.right;
            }
            else
            {
                return uncle = newnode.parent.parent.left;
            }
        }
        private bool IsLeft(NODE node)
        {//return true if node is left child of its parent...
            if (node == root)
                return false;
            else if (node == null)
                return false;
            else if (node == node.parent.left)
                return true;
            else
                return false;
        }
        private bool IsRight(NODE node)
        {//return true if node is right child of its parent...
            if (node == root)
                return false;
            else if (node == null)
                return false;
            else if (node == node.parent.right)
                return true;
            else
                return false;
        }
        private bool IsRed(NODE node)
        {
            if (node == null)
                return false;
            else if (node.color == color.red)
                return true;
            else
                return false;
        }
        private bool IsBlack(NODE node)
        {
            if (node == null)
            {
                return true;
            }
            else if (node.color == color.black)
                return true;
            else return false;
        }
        private NODE GetSiblling(NODE node)
        {
            if (node == null)
            {
                return node;
            }
            if (IsleftU)
            {

                return node.parent.right;
            }
            else
            {
                return node.parent.left;
            }
        }
        private NODE GetGrandParent(NODE newnode)
        {//return grandparent...
            if (newnode == null)
            {
                return newnode;
            }
            else
                return newnode.parent.parent;
        }
        private void DeletionFixed(NODE U)
        {

            NODE sibling = GetSiblling(U);

            while (U != root && U != null && IsBlack(U))
            {
                //Console.WriteLine(U.parent.right.data);
                sibling = GetSiblling(U);

                //case 2
                if (IsBlack(sibling) && (IsRed(sibling.left) || IsRed(sibling.right)))
                {//rightright

                    if (IsRight(sibling) && (IsRed(sibling.right)))
                    {
                        sibling.color = sibling.parent.color;
                        sibling.parent.color = color.black;
                        sibling.right.color = color.black;
                        rotateleft(U.parent);
                        sibling = GetSiblling(U);
                        U = root;
                    }
                    //leftleft
                    else if (IsLeft(sibling) && (IsRed(sibling.left)))
                    {
                        sibling.color = sibling.parent.color;
                        sibling.parent.color = color.black;
                        sibling.left.color = color.black;
                        rotateright(U.parent);
                        sibling = GetSiblling(U);
                        U = root;
                    }
                    //leftright
                    else if (IsLeft(sibling) && IsRed(sibling.right))
                    {

                        sibling.right.color = color.black;
                        sibling.color = color.red;
                        rotateleft(sibling);
                        sibling = GetSiblling(U);
                        //now case converted to left left case

                    }
                    //rigthleft
                    else if (IsRight(sibling) && IsRed(sibling.left))
                    {
                        sibling.left.color = color.black;
                        sibling.color = color.red;
                        rotateright(sibling);
                        sibling = GetSiblling(U);
                        //now case converted to right right case

                    }

                }
                //case 3 sibling and its childs are black...;

                else if (IsBlack(sibling) && IsBlack(sibling.right) && IsBlack(sibling.left))
                {

                    sibling.color = color.red;
                    U = U.parent;
                    IsleftU = IsLeft(U);
                    IsrightU = IsRight(U);


                }//4 case red sibling black childs...
                else if (IsRed(sibling) && IsBlack(sibling.right) && IsBlack(sibling.left))
                {


                    if (IsLeft(sibling))
                    {
                        sibling.parent.color = color.red;
                        sibling.color = color.black;
                        rotateright(sibling.parent);
                    }
                    else
                    {

                        sibling.parent.color = color.red;
                        sibling.color = color.black;
                        rotateleft(sibling.parent);


                    }
                }

            }
            //case1 id v is black u is red simply make u(replaced v at its postion) red
            //case 3 recur for parent if parent was red loop breaks and parent becomes black...
            U.color = color.black;
        }
        public int Height()
        {
            return Height(root);
        }
        private int Height(NODE node)
        {
            if (node == null)
                return -1;
            int heightL = Height(node.right);
            int heightR = Height(node.left);
            if (heightL > heightR)
            {
                return heightL + 1;
            }
            else
            {

                return heightR + 1;
            }
        }
        public int Count()
        {
            return Count(root);
        }
        private int Count(NODE temp)
        {
            if (temp == null)
            {
                return 0;
            }
            return 1 + Count(temp.left) + Count(temp.right);
        }
        public void BlackHeight()
        {
            BlackHeight(root);


        }
        private void BlackHeight(NODE node)
        {//Test for root to leaf node height....
         //according to red black tree  Every path from a node (including root) to any of its descendant NULL node has the same number of black nodes.
            NODE node1 = node;
            int rb = 0, lb = 0;

            while (node1 != null)
            {
                if (node1 != null && IsBlack(node1))
                    ++lb;
                node1 = node1.right;
            }
            Console.WriteLine("Root to Left leaf node black height=>" + lb);
            node1 = node;

            while (node1 != null)
            {

                if (node1 != null && IsBlack(node1))
                    ++rb;
                node1 = node1.right;
            }
            Console.WriteLine("Root to Right leaf node black height=>" + rb);

        }


    }
}

