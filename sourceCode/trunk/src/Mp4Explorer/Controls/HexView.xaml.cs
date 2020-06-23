using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfHexaEditor;

namespace Mp4Explorer.Controls
{
    /// <summary>
    /// Interaction logic for HexView.xaml
    /// </summary>
    public partial class HexView : UserControl
    {
        public MemoryStream Data
        {
            get => Viewer.Stream;
            set => Viewer.Stream = value;
        }
        public new FontFamily FontFamily
        {
            get => Viewer.FontFamily;
            set => Viewer.FontFamily = value;
        }
        public new double FontSize
        {
            get => Viewer.FontSize;
            set => Viewer.FontSize = value;
        }
        public new FontWeight FontWeight
        {
            get => Viewer.FontWeight;
            set => Viewer.FontWeight = value;
        }

        static HexView()
        {
            HexEditor.StreamProperty.AddOwner(typeof(HexView));
            HexEditor.FontFamilyProperty.AddOwner(typeof(HexView));
            // Those are inherited from Control and we already own:
            //HexEditor.FontSizeProperty.AddOwner(typeof(HexView));
            //HexEditor.FontWeightProperty.AddOwner(typeof(HexView));
        }

        public HexView()
        {
            InitializeComponent();
        }
    }
}
