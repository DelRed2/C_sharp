using System;
using System.Collections.Generic;

namespace Tree {
    public class BinaryTree<TK, TV> where TK : IComparable<TK> {

        private class TreeNode<T1, T2> {
            public T1 Key;
            public T2 Value;

            public TreeNode<T1, T2> Left;
            public TreeNode<T1, T2> Right;


            public TreeNode(T1 key, T2 val) {
                Key = key;
                Value = val;
            }
        }

        public class ValueWrapper<T2> {
            public T2 Value;

            public ValueWrapper(T2 val) {
                Value = val;
            }
        }

        private TreeNode<TK, TV> tree;

        public void Insert(TK key, TV val) {
            if (tree == null) tree = new TreeNode<TK, TV>(key, val);
            else {
                TreeNode<TK, TV> curNode = tree;
                while (true) {
                    if (key.CompareTo(curNode.Key) == 1) {
                        if (curNode.Right != null) {
                            curNode = curNode.Right;
                            continue;
                        } else {
                            var newNode = new TreeNode<TK, TV>(key, val);
                            curNode.Right = newNode;
                            break;
                        }
                    }
                    if (key.CompareTo(curNode.Key) == -1) {
                        if (curNode.Left != null) {
                            curNode = curNode.Left;
                            continue;
                        } else {
                            var newNode = new TreeNode<TK, TV>(key, val);
                            curNode.Left = newNode;
                            break;
                        }
                    }
                    curNode.Value = val;
                    break;
                }
            }
        }

        private ValueWrapper<TV> Find(TK key, TreeNode<TK, TV> node) {
            if (key.CompareTo(node.Key) == 0) return new ValueWrapper<TV>(node.Value);
            if (key.CompareTo(node.Key) == 1) { return node.Right != null ? Find(key, node.Right) : null; }
            return node.Left != null ? Find(key, node.Left) : null;
        }

        public ValueWrapper<TV> Find(TK key) {
            return tree != null ? Find(key, tree) : null;
        }

        public bool Remove(TK key) {
            TreeNode<TK, TV> curNode = tree, y = null;

            while (curNode != null) {
                int cmp = key.CompareTo(curNode.Key);
                if (cmp == 0) break;
                y = curNode;
                curNode = cmp < 0 ? curNode.Left : curNode.Right;
            }

            if (curNode == null) return false;

            if (curNode.Right == null) {
                if (y == null) tree = curNode.Left;
                else {
                    if (curNode == y.Left) y.Left = curNode.Left;
                    else y.Right = curNode.Left;
                }
            } else {
                TreeNode<TK, TV> TheMostLeft = curNode.Right;
                y = null;
                while (TheMostLeft.Left != null) {
                    y = TheMostLeft;
                    TheMostLeft = TheMostLeft.Left;
                }

                if (y != null) y.Left = TheMostLeft.Right;
                else curNode.Right = TheMostLeft.Right;
                curNode.Key = TheMostLeft.Key;
                curNode.Value = TheMostLeft.Value;
            }
            return true;
        }

        public IEnumerator<TV> GetBFSEnumerator() {
            if (tree == null) yield break;
            var queue = new Queue<TreeNode<TK, TV>>();
            queue.Enqueue(tree);
            while (queue.Count > 0) {
                var node = queue.Dequeue();
                yield return node.Value;
                if (node.Left != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);
            }
        }

        /*private void DFSvisit(Queue<TreeNode<T1, T2>> queue, TreeNode<T1, T2> node) {
            queue.Enqueue(node);
            if (node.Left != null) DFSvisit(queue, node.Left);
            if (node.Right != null) DFSvisit(queue, node.Right);
        }*/

        /*public IEnumerator<T2> GetDFSEnumerator() {
            if (tree == null) yield break;
            var queue = new Queue<TreeNode<T1, T2>>();
            DFSvisit(queue, tree);
            while (queue.Count > 0) { yield return queue.Dequeue().Value; }
        }*/

        public IEnumerator<TV> GetDFSEnumerator() {
            if (tree == null) yield break;
            if (tree.Left == null && tree.Right == null) {
                yield return tree.Value;
                yield break;
            }

            var stack = new Stack<TreeNode<TK, TV>>();
            TreeNode<TK, TV> curr = tree;

            do {
                stack.Push(curr);
                yield return curr.Value;

                if (curr.Left != null) {
                    curr = curr.Left;
                    continue;
                }
                if (curr.Right != null) {
                    curr = curr.Right;
                    continue;
                }

                stack.Pop();
                TreeNode<TK, TV> prevCurr = stack.Pop();

                while (true) {
                    if (prevCurr.Right != null && prevCurr.Right != curr) {
                        curr = prevCurr.Right;
                        stack.Push(prevCurr);
                        break;
                    } else {
                        if (stack.Count == 0) break;
                        curr = prevCurr;
                        prevCurr = stack.Pop();
                    }
                }

            } while (stack.Count > 0);
        }

        private IEnumerator<TreeNode<TK, TV>> GetBFSNodeEnumerator() {
            if (tree == null) yield break;
            var queue = new Queue<TreeNode<TK, TV>>();
            queue.Enqueue(tree);
            while (queue.Count > 0) {
                var node = queue.Dequeue();
                yield return node;
                if (node.Left != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);
            }
        }

        public bool IsCorrect() {
            var t = GetBFSNodeEnumerator();
            while (t.MoveNext()) {
                if (t.Current.Left != null && t.Current.Left.Key.CompareTo(t.Current.Key) == 1) return false;
                if (t.Current.Right != null && t.Current.Right.Key.CompareTo(t.Current.Key) == -1) return false;
            }
            return true;
        }
    }
}