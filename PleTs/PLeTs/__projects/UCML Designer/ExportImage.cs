using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace ShapeConnectors
{
    class ExportPng
    {
        public static void ExportDiagramToPng(Canvas myCanvas, String fileName)
        {
            var CanvasSize = myCanvas.RenderSize;
            // Get the size of canvas
            var size = new Size(CanvasSize.Width, CanvasSize.Height);
            // Measure and arrange the surface
            // VERY IMPORTANT
            myCanvas.Measure(size);
            myCanvas.Arrange(new Rect(size));

            // Create a render bitmap and push the surface to it
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(myCanvas);

            // Create a file stream for saving image
            using (System.IO.FileStream outStream = new System.IO.FileStream(fileName.Replace(".png", "") + ".png", FileMode.Create))
            {
                // Use png encoder for our data
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                // push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);
            }
        } 
    }
}
