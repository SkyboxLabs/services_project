using System;
using System.Collections.Generic;

namespace InterviewTest.Model
{
    public class TreeNode
    {
        public int Score { get; }

        public List<Guid> UserIds { get; set; }

        public int LeftCount { get; set; }

        public int RightCount { get; set; }

        public TreeNode Parent { get; set; }

        public TreeNode Left { get; set; }

        public TreeNode Right { get; set; }

        public TreeNode(Guid userId, int score)
        {
            Score = score;
            UserIds = new List<Guid> {userId};
            LeftCount = 0;
            RightCount = 0;
        }
    }
}
