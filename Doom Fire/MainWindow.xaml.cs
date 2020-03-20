using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Doom_Fire
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        DrawableCanvas dc;
        Fire fire;
        public MainWindow()
        {
            InitializeComponent();

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {            
            dc.Clear();
            fire.Update();
            fire.Draw(dc, (int)canvas.ActualWidth / 6, (int)canvas.ActualHeight);
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            dc = new DrawableCanvas();
            fire = new Fire(37, 37);

            canvas.Children.Add(dc);
        }
    }
}
