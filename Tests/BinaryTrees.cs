using System;
using System.Threading.Tasks;
using Tests.Abstraction;

namespace Tests
{
    public class BinaryTrees : IPerfTest
    {
        private const int MinDepth = 4;
        public void Run(int n)
        {
            var maxDepth = Math.Max(MinDepth + 2, n);
            var stretchDepth = maxDepth + 1;

            TreeNode.BottomUpTree(stretchDepth).ItemCheck();

            var longLivedTree = TreeNode.BottomUpTree(maxDepth);

            for (var depth = MinDepth; depth <= maxDepth; depth += 2)
            {
                var iterations = 1 << (maxDepth - depth + MinDepth);

                for (var i = 1; i <= iterations; i++)
                {
                    TreeNode.BottomUpTree(depth).ItemCheck();
                }
            }

            longLivedTree.ItemCheck();
        }

        Task IPerfTest.Run(int i)
        {
            return Task.Run(() => Run(i));
        }

        private readonly struct TreeNode
        {
            private class Next
            {
                public TreeNode Left, Right;
            }

            private readonly Next _next;

            internal static TreeNode BottomUpTree(int depth) => depth > 0 ? new TreeNode(BottomUpTree(depth - 1), BottomUpTree(depth - 1)) : new TreeNode();
            private TreeNode(TreeNode left, TreeNode right) => _next = new Next {Left = left, Right = right};
            internal int ItemCheck() => _next == null ? 1 : 1 + _next.Left.ItemCheck() + _next.Right.ItemCheck();
        }
    }
}
