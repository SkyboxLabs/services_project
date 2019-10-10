using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.Model
{
    public class TreeNodeCollection
    {
        private TreeNode Root;

        public void Add(Guid userId, int score)
        {
            if (this.Root == null)
            {
                this.Root = new TreeNode(userId, score);
                return;
            }

            var current = this.Root;
            while (true)
            {
                if (current.Score > score)
                {
                    current.LeftCount++;
                    if (current.Left == null)
                    {
                        current.Left = new TreeNode(userId, score);
                        current.Left.Parent = current;
                        return;
                    }
                    else
                    {
                        current = current.Left;
                    }
                }
                else if (current.Score < score)
                {
                    current.RightCount++;
                    if (current.Right == null)
                    {
                        current.Right = new TreeNode(userId, score);
                        current.Right.Parent = current;
                        return;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    current.UserIds.Add(userId);
                    return;
                }
            }
        }

        public int GetPosition(int score)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(Root);
            TreeNode current = null;
            while (true)
            {
                current = queue.Dequeue();
                if (current.Score == score)
                {
                    break;
                }


                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }

                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }
            }
            
            return GetPosition(current);
        }

        private int GetPosition(TreeNode current)
        {
            if (current == null) return 0;

            int parentPosition = GetPosition(current.Parent);

            if (current.Parent == null || current.Parent.Score > current.Score)
            {
                return parentPosition + current.RightCount + (current.Parent?.UserIds?.Count ?? 1);
            }
            else if (current.Parent.Score < current.Score)
            {
                return parentPosition - current.LeftCount - 1;
            }
            else
            {
                return parentPosition;
            }

            // Contemplating replacing all but the top if with
            //return GetPosition(current.Parent) + (current.Parent == null || current.Parent.Score > current.Score)
            //    ? current.RightCount + (current.Parent?.UserIds?.Count ?? 1)
            //    : (current.Parent.Score < current.Score) ? -1 * (current.LeftCount + 1) : 0;
        }
    }
}
