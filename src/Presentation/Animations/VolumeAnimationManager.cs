using OmniVoice.Presentation.Models;

using System.Collections.ObjectModel;

namespace OmniVoice.Presentation.Animations;

public class VolumeAnimationManager
{
    public ObservableCollection<EllipseModel> EllipseModels { get; private set; }
    public float Speed = 1f;

    private const float Intensity = 30f;
    private const float SizeEllipse = 200f;
    private const int SizeMovingWindow = 40;

    private float _target = 0;
    private List<float> _movingWindow = new();

    public VolumeAnimationManager()
    {
        EllipseModels = new ObservableCollection<EllipseModel>
        {
            new EllipseModel(SizeEllipse, SizeEllipse, 0),
            new EllipseModel(SizeEllipse, SizeEllipse, 90),
        };
    }

    public void Tick(float volume)
    {
        if (volume == 0)
        {
            _target = 0;
        }
        else if (_movingWindow.Count == 0 || _movingWindow[_movingWindow.Count - 1] != volume)
        {
            _movingWindow.Add(volume);

            if (_movingWindow.Count > SizeMovingWindow)
            {
                _movingWindow.RemoveAt(0);
            }
        }

        if (volume != 0 && volume > GetAverageOfMinValues(_movingWindow, 3))
        {
            float max = GetAverageOfMaxValues(_movingWindow, 3);

            _target = volume / max * Intensity;

            if (_target > Intensity)
            {
                _target = Intensity;
            }
        }

        UpdateEllipses();
    }

    private void UpdateEllipses()
    {
        foreach (var ellipse in EllipseModels)
        {
            float factor = 0.5f * Speed;

            if (ellipse.Width > SizeEllipse + _target)
            {
                factor /= 3;
            }

            ellipse.Width = Interpolate(SizeEllipse + _target, ellipse.Width, factor);
            ellipse.Height = Interpolate(SizeEllipse + _target / 1.75f, ellipse.Height, factor);
            ellipse.Rotate += 1 * Speed;

            if (ellipse.Rotate > 360)
            {
                ellipse.Rotate -= 360;
            }
        }
    }

    private float GetAverageOfMaxValues(List<float> list, int count)
    {
        if (list == null || count <= 0) return 0;

        var maxValues = list.OrderByDescending(x => x).Take(count);

        return maxValues.Sum() / maxValues.Count();
    }

    private float GetAverageOfMinValues(List<float> list, int count)
    {
        if (list == null || count <= 0) return 0;

        var minValues = list.OrderBy(x => x).Take(count);

        return minValues.Sum() / minValues.Count();
    }

    private double Interpolate(double target, double current, double factor)
    {
        return current + (target - current) * factor;
    }
}