using OmniVoice.Presentation.Common.Views;
using System.Windows.Media;

namespace OmniVoice.Presentation.Models;

public class MenuButtonModel : ModelBase
{
    public bool State { get; private set; }

    private SolidColorBrush _color;
    public SolidColorBrush Color
    {
        get { return _color; }
        set
        {
            _color = value;
            OnPropertyChanged(nameof(Color));
        }
    }

    private SolidColorBrush _normalColor;
    private SolidColorBrush _clickedColor;

    private string _content;
    public string Content
    {
        get { return _content; }
        set
        {
            _content = value;
            OnPropertyChanged(nameof(Content));
        }
    }

    private string _pageId;
    public string PageId
    {
        get { return _pageId; }
        set
        {
            _pageId = value;
            OnPropertyChanged(nameof(PageId));
        }
    }

    public MenuButtonModel(string content, string pageId, SolidColorBrush normalColor, SolidColorBrush clickedColor)
    {
        _content = content;
        _pageId = pageId;
        _normalColor = normalColor;
        _clickedColor = clickedColor;   

        _color = _normalColor;
    }

    public MenuButtonModel(string content, string pageId, string normalColor, string clickedColor) : this(content, pageId,
        new SolidColorBrush((Color)ColorConverter.ConvertFromString(normalColor)), new SolidColorBrush((Color)ColorConverter.ConvertFromString(clickedColor))) { }

    public void SetState(bool state)
    {
        State = state;

        Color = State ? _clickedColor : _normalColor;
    }
}