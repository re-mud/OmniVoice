using OmniVoice.Presentation.Common.Views;

namespace OmniVoice.Presentation.Models;

public class EllipseModel : ModelBase
{
    public double Width { 
        get => _width; 
        set
        {
            _width = value;
            OnPropertyChanged(nameof(Width));
        }
    }
    public double Height
    {
        get => _height;
        set
        {
            _height = value;
            OnPropertyChanged(nameof(Height));
        }
    }
    public double Rotate
    {
        get => _rotate;
        set
        {
            _rotate = value;
            OnPropertyChanged(nameof(Rotate));
        }
    }

    private double _width;
    private double _height;
    private double _rotate;

    public EllipseModel(double width, double height, double rotate)
    {
        _width = width;
        _height = height;
        _rotate = rotate;
    }
}