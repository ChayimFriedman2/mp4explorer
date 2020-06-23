//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.Generic;
using System.Linq;
using CMStream.Mp4;

namespace Mp4Explorer
{
    public class Mp4TreeNode : ImageTreeViewItem
    {
        public Mp4Box Box { get; set; }

        public Mp4TreeNode(Mp4Box box)
        {
            Box = box;
            Text = Mp4Util.FormatFourChars(box.Type);
            ImageSource = GetImageSource(box.Type);
            IEnumerable<Mp4Box> children = box switch
            {
                Mp4ContainerBox containerBox => containerBox.Children,
                Mp4StsdBox stsdBox => stsdBox.Entries,
                Mp4DrefBox drefBox => drefBox.Entries,
                _ => Enumerable.Empty<Mp4Box>(),
            };
            foreach (Mp4Box child in children)
            {
                Items.Add(new Mp4TreeNode(child));
            }
        }

        private string GetImageSource(uint type) =>
            $"/Mp4Explorer;component/Images/{Mp4Util.FormatFourChars(type)}_16.ico";
    }
}
