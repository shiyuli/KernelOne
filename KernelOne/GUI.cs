using Cosmos.HAL;
using Cosmos.System.Graphics;

namespace KernelOne
{
    public static class GUI
    {
        private static Canvas _canvas;
        private static Pen _pen { get; set; }
        private static System.Drawing.Size _screenSize { get; set; }

        private static Mouse _mouse;

        static GUI()
        {
            _canvas = FullScreenCanvas.GetFullScreenCanvas();
            _pen = new Pen(Color.White);
            _screenSize = new System.Drawing.Size(1920, 1080);

            _canvas.Mode = new Mode(_screenSize.Width, _screenSize.Height, ColorDepth.ColorDepth32);
            _mouse = new Mouse();
            _mouse.Initialize((uint)_screenSize.Width, (uint)_screenSize.Height);
            Cosmos.Core.INTs.IRQContext context = new Cosmos.Core.INTs.IRQContext();
            _mouse.HandleMouse(ref context);

            Core.MouseEnabled = true;
        }

        public static void Clear()
        {
            Clear(Color.Black);
        }

        public static void Clear(Color color)
        {
            _canvas.Clear(color);
        }

        public static void Test()
        {
            Bitmap bitmap = new Bitmap(
                10, 10,
                new byte[] {
                    0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0,
                    255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                    0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                    0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 23, 59, 88, 255,
                    23, 59, 88, 255, 0, 255, 243, 255, 0, 255, 243, 255, 23, 59, 88, 255, 23, 59, 88, 255, 0, 255, 243, 255, 0,
                    255, 243, 255, 0, 255, 243, 255, 23, 59, 88, 255, 153, 57, 12, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255,
                    243, 255, 0, 255, 243, 255, 153, 57, 12, 255, 23, 59, 88, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243,
                    255, 0, 255, 243, 255, 0, 255, 243, 255, 72, 72, 72, 255, 72, 72, 72, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0,
                    255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 72, 72,
                    72, 255, 72, 72, 72, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                    10, 66, 148, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255,
                    243, 255, 10, 66, 148, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 10, 66, 148, 255, 10, 66, 148, 255,
                    10, 66, 148, 255, 10, 66, 148, 255, 10, 66, 148, 255, 10, 66, 148, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255,
                    243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 10, 66, 148, 255, 10, 66, 148, 255, 10, 66, 148, 255, 10, 66, 148,
                    255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                    0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                }, ColorDepth.ColorDepth32);

            _canvas.DrawImage(bitmap, new Point(10, 10));
        }

        #region Draw methods

        public static void Draw(Point point)
        {
            _canvas.DrawPoint(_pen, point.X, point.Y);
        }

        public static void Draw(Point pointStart, Point pointFinish)
        {
            _canvas.DrawLine(_pen, pointStart.X, pointStart.Y, pointFinish.X, pointFinish.Y);
        }

        #endregion

        public static void DrawMouse()
        {
            Pen mousePen = new Pen(Color.White, 1);
            DrawMouse(mousePen, _mouse.X, _mouse.Y);
        }

        public static void DrawMouse(Pen pen, int x, int y)
        {
            _canvas.DrawPoint(pen, x, y);
            _canvas.DrawPoint(pen, x + 1, y + 1);
            _canvas.DrawPoint(pen, x + 1, y);
            _canvas.DrawPoint(pen, x, y + 1);
            _canvas.DrawPoint(pen, x + 2, y + 1);
            _canvas.DrawPoint(pen, x + 1, y + 2);
            _canvas.DrawPoint(pen, x + 2, y + 2);
            _canvas.DrawPoint(pen, x + 3, y + 3);
            _canvas.DrawPoint(pen, x + 4, y + 4);
        }
    }
}
