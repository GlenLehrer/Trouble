using System.Reflection;
using ZGT.Trouble.MAUI.UI.MyMauiApp;
using Microsoft.Maui.Graphics.Platform;
using System.Drawing;
namespace ZGT.Trouble.MAUI.UI
{
    namespace MyMauiApp
    {
        public class GraphicsDrawable : IDrawable
        {      
            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                Microsoft.Maui.Graphics.IImage image;
                Assembly assembly = GetType().GetTypeInfo().Assembly;
                //string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                //for(int i = 0; i < names.Length; i++)
                //{
                //    canvas.DrawString(names[i], 20, i * 10, HorizontalAlignment.Left);
                //}

                using (Stream stream = assembly.GetManifestResourceStream("ZGT.Trouble.MAUI.UI.Resources.Images.troubleboard_colors.png"))
                {
                    image = PlatformImage.FromStream(stream);
                }

                if (image != null)
                {
                    image.Downsize(image.Width / 6, image.Height / 6);
                    canvas.DrawImage(image, 10, 10, image.Width / 6, image.Height / 6);
                    SolidPaint solidPaint= new SolidPaint(Microsoft.Maui.Graphics.Color.FromRgba(255, 0, 0, 1.0f));
                    RectF solidRectangle = new RectF(10 + 1.35f, 10 + 8.68f, 20, 20);
                    canvas.SetFillPaint(solidPaint, solidRectangle);
                    canvas.FillRoundedRectangle(solidRectangle, 12);
                }

                //int x = (int)dirtyRect.Width;
                //int y = (int)dirtyRect.Height;


                //canvas.FillColor = Colors.Yellow;
                //canvas.FillRectangle(dirtyRect.Left, dirtyRect.Top, x / 2, (y / 11));
                //canvas.FillColor = Colors.Blue;
                //canvas.FillRectangle(dirtyRect.Right / 2, dirtyRect.Top, x / 2, (y / 11));
                //canvas.FillColor = Colors.Green;
                //canvas.FillRectangle(dirtyRect.Left, dirtyRect.Bottom - (y / 11), x / 2, (y / 11));
                //canvas.FillColor = Colors.Red;
                //canvas.FillRectangle(dirtyRect.Right / 2, dirtyRect.Bottom - (y / 11), x / 2, (y / 11));
                ///*
                //canvas.FillRectangle(dirtyRect.Left, dirtyRect.Top, (x / 11), (y / 11));
                //canvas.FillRectangle(dirtyRect.Left + (x / 11), dirtyRect.Top, (x / 11), (y / 11));
                //canvas.FillRectangle(dirtyRect.Left + 2 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
                //canvas.FillRectangle(dirtyRect.Left + 3 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
                //canvas.FillRectangle(dirtyRect.Left + 4 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
                //canvas.FillRectangle(dirtyRect.Left + 5 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
                //canvas.FillColor = Colors.Blue;
                //canvas.FillRectangle(dirtyRect.Left + 6 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
                //canvas.FillRectangle(dirtyRect.Left + 7 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
                //canvas.FillRectangle(dirtyRect.Left + 8 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
                //canvas.FillRectangle(dirtyRect.Left + 9 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
                //canvas.FillRectangle(dirtyRect.Left + 10 * (x / 11), dirtyRect.Top, (x / 11), (y / 11));
                //*/
                //canvas.StrokeColor = Colors.Black;
                //canvas.StrokeSize = 2;

                //canvas.DrawLine(dirtyRect.Left, dirtyRect.Top, dirtyRect.Right, dirtyRect.Top);
                //canvas.DrawLine(dirtyRect.Left, dirtyRect.Top + (y / 11), dirtyRect.Right, dirtyRect.Top + (y / 11));

                //canvas.DrawLine(dirtyRect.Left, dirtyRect.Bottom - (y / 11), dirtyRect.Right, dirtyRect.Bottom - (y / 11));
                //canvas.DrawLine(dirtyRect.Left, dirtyRect.Bottom, dirtyRect.Right, dirtyRect.Bottom);
            }
        }
    }

    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            graphicsView.Drawable = new GraphicsDrawable();
        }


        private void OnCounterClicked(object sender, EventArgs e)
        {
            //SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
