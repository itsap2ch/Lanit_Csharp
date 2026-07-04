public class Rectangle
{
    
    private double _width;
    private double _height;

    public double X { get; private set; }
    public double Y { get; private set; }
    public double Width
    {
        get { return _width; }
        private set
        {
            if (value <= 0)
                throw new ArgumentException("Ширина должна быть положительной");
            _width = value;
        }
    }

    public double Height
    {
        get { return _height; }
        private set
        {
            if (value <= 0)
                throw new ArgumentException("Высота должна быть положительной");
            _height = value;
        }
    }

    public Rectangle(double x, double y, double width, double height) 
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public double Perimeter
    {
        get { return 2 * (_width + _height); }
    }

    public double Area
    {
        get { return _width * _height; }
    }

    public static void Run(double x, double y, double w, double h)
    {
        try
        {
            var rect = new Rectangle(x, y, w, h);
            Console.WriteLine("Left Corner: (" + rect.X + "; " + rect.Y + "), Width: " + rect.Width + ", Height: " + rect.Height + ".");
            Console.WriteLine("Area: " + rect.Area + ", Perimeter: " + rect.Perimeter + ".");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

}