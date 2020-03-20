using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Doom_Fire
{
    public class DrawableCanvas : FrameworkElement
    {
        VisualCollection children;        

        public DrawableCanvas()
        {
            children = new VisualCollection(this);
        }

        protected override int VisualChildrenCount
        {
            get { return children.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if(index < 0 || index >= children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return children[index];
        }

        public void Clear()
        {
            children.Clear();
        }

        //Internal methods to create the shapes
        DrawingVisual CreateDrawingVisualRectangle(Point position, Size size, Brush color, Pen border,
            double radiusX = 0, double radiusY = 0)
        {
            DrawingVisual dv = new DrawingVisual();

            DrawingContext dc = dv.RenderOpen();
          
            Rect rect = new Rect(position, size);
            dc.DrawRoundedRectangle(color, border, rect, radiusX, radiusY);
         
            dc.Close();

            return dv;
        }
        
        DrawingVisual CreateDrawingVisualEllipse(Point position, double radiusX, double radiusY, Brush color, Pen border)
        {
            DrawingVisual dv = new DrawingVisual();

            DrawingContext dc = dv.RenderOpen();

            dc.DrawEllipse(color, border, position, radiusX, radiusY);
            
            dc.Close();

            return dv;
        }

        DrawingVisual CreateDrawingVisualLine(Point initPosition, Point endPosition, Brush color, Pen border)
        {
            DrawingVisual dv = new DrawingVisual();

            DrawingContext dc = dv.RenderOpen();

            dc.DrawLine(border, initPosition, endPosition );

            dc.Close();

            return dv;
        }

        DrawingVisual CreateDrawingGeometry(Brush color, Pen border, Geometry geometry)
        {
            DrawingVisual dv = new DrawingVisual();

            DrawingContext dc = dv.RenderOpen();

            dc.DrawGeometry(color, border, geometry);

            dc.Close();

            return dv;
        }

        DrawingVisual CreateDrawingVisualImage(ImageSource image, Rect destination)
        {
            DrawingVisual dv = new DrawingVisual();
            
            DrawingContext dc = dv.RenderOpen();

            dc.DrawImage(image, destination);
            
            dc.Close();
            WriteableBitmap wb;
            
            return dv;
        }

        DrawingVisual CreateDrawingText(Point position, FormattedText text)
        {
            DrawingVisual dv = new DrawingVisual();

            DrawingContext dc = dv.RenderOpen();

            dc.DrawText(text, position);

            dc.Close();

            return dv;
        }

  

        
        public void DrawRectangle(Point position, Size size, Brush color, Pen border,
            double radiusX = 0, double radiusY = 0)
        {
            DrawingVisual dv = CreateDrawingVisualRectangle(position, size, color, border, radiusX, radiusY);

            children.Add(dv);
        }

        public void DrawEllipse(Point position, double radiusX, double radiusY, Brush color, Pen border)
        {
            DrawingVisual dv = CreateDrawingVisualEllipse(position, radiusX, radiusY, color, border);

            children.Add(dv);
        }

        public void DrawLine(Point initPosition, Point endPosition, Brush color, Pen border)
        {
            DrawingVisual dv = CreateDrawingVisualLine(initPosition, endPosition, color, border);

            children.Add(dv);
        }

        public void DrawGeometry(Brush color, Pen border, Geometry geometry)
        {
            DrawingVisual dv = CreateDrawingGeometry(color, border, geometry);

            children.Add(dv);
        }

        public void DrawImage(ImageSource image, Rect destination)
        {
            DrawingVisual dv = CreateDrawingVisualImage(image, destination);

            children.Add(dv);
        }

        public void DrawText(Point position, FormattedText text)
        {
            DrawingVisual dv = CreateDrawingText(position, text);

            children.Add(dv);
        }
    }
}
