using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bt05
{
    public class BinarySearchTree
    {
        Node root; 

        public BinarySearchTree()
        {
            root = null;
        }

        public int Search(int value)
        {
            Node foundNode = search(root, value);

            if (foundNode == null)
            {
                throw new Exception();
            }
            else
            {
                return foundNode.Value;
            }
        }

        private Node search(Node node, int value)
        {
            if (node == null)
            {
                return null;
            }
            else if (node.Value == value)
            {
                return node;
            }
            else if (node.Value > value)
            {
                return search(node.LeftChild, value);
            }
            else 
            {
                return search(node.RightChild, value);
            }
        }

        public void Insert(int value)
        {
            Node nodeToInsert = new Node() { Value = value };

            // move the null check into insert?
            if (root == null)
            {
                root = nodeToInsert;
            }
            else
            {
                insert(root, nodeToInsert);
            }
        }
        
        private void insert(Node parentNode, Node nodeToInsert)
        {
            if (nodeToInsert.Value > parentNode.Value)
            {
                if (parentNode.RightChild == null)
                {
                    parentNode.RightChild = nodeToInsert;
                }
                else
                {
                    insert(parentNode.RightChild, nodeToInsert);
                }
            }
            if (nodeToInsert.Value < parentNode.Value)
            {
                if (parentNode.LeftChild == null)
                {
                    parentNode.LeftChild = nodeToInsert;
                }
                else
                {
                    insert(parentNode.LeftChild, nodeToInsert);
                }
            }
        }

        public void Delete(int value)
        {
            root = delete(root, value);
        }

        private Node delete(Node nodeToCheck, int valueToDelete)
        {
            if (nodeToCheck == null)
            {
                return null;
            }
            else if (nodeToCheck.Value < valueToDelete)
            {
                nodeToCheck.RightChild = delete(nodeToCheck.RightChild, valueToDelete);
            }
            else if (nodeToCheck.Value > valueToDelete)
            {
                nodeToCheck.LeftChild = delete(nodeToCheck.LeftChild, valueToDelete);
            }
            else
            {
                if (nodeToCheck.LeftChild == null && nodeToCheck.RightChild == null)
                {
                    // No children - delete by referring to null instead 
                    nodeToCheck = null;
                }
                else if (nodeToCheck.LeftChild != null && nodeToCheck.RightChild != null)
                {
                    // Two children - Copy the largest value on the left hand side and delete 
                    // node with value that was copied
                    nodeToCheck.Value = findMax(nodeToCheck.LeftChild).Value; 
                    nodeToCheck.LeftChild = delete(nodeToCheck.LeftChild, nodeToCheck.Value);
                }
                else if (nodeToCheck.RightChild != null)
                {
                    // One child - delete by referring to the one child
                    nodeToCheck = nodeToCheck.RightChild;
                }
                else 
                {
                    // One child - delete by referring to the one child
                    nodeToCheck = nodeToCheck.LeftChild;
                }
            }

            return nodeToCheck;
        }

        private Node findMax(Node node)
        {
            if (node.RightChild == null)
            {
                return node;
            }
            else
            {
                return findMax(node.RightChild);
            }
        }

        public int[] GetInOrder()
        {
            List<Node> nodes = new List<Node>();
            addNodeInOrder(root, nodes);
            int[] nodeValues = nodes.Select<Node, int>(node => node.Value).ToArray();

            return nodeValues;
        }

        private void addNodeInOrder(Node nodeToAdd, List<Node> nodes)
        {
            if (nodeToAdd == null)
            {
                return;
            }
            addNodeInOrder(nodeToAdd.LeftChild, nodes);
            nodes.Add(nodeToAdd);
            addNodeInOrder(nodeToAdd.RightChild, nodes);
        }

        public int[] GetPreOrder()
        {
            List<Node> nodes = new List<Node>();
            addNodePreOrder(root, nodes);
            int[] nodeValues = nodes.Select<Node, int>(node => node.Value).ToArray();

            return nodeValues;
        }

        private void addNodePreOrder(Node nodeToAdd, List<Node> nodes)
        {
            if (nodeToAdd == null)
            {
                return;
            }
            nodes.Add(nodeToAdd);
            addNodePreOrder(nodeToAdd.LeftChild, nodes);
            addNodePreOrder(nodeToAdd.RightChild, nodes);
        }

        public int[] GetPostOrder()
        {
            List<Node> nodes = new List<Node>();
            addNodePostOrder(root, nodes);
            int[] nodeValues = nodes.Select<Node, int>(node => node.Value).ToArray();

            return nodeValues;
        }

        private void addNodePostOrder(Node nodeToAdd, List<Node> nodes)
        {
            if (nodeToAdd == null)
            {
                return;
            }
            addNodePostOrder(nodeToAdd.LeftChild, nodes);
            addNodePostOrder(nodeToAdd.RightChild, nodes);
            nodes.Add(nodeToAdd);
        }

        class Node
        {
            public int Value { get; set; }
            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }
        }
    }
}
