

#region using statements

#endregion

namespace DataJuggler.WPF.Controls
{

    #region delegate void ButtonClickHandler(int buttonNumber, string buttonText);
    public delegate void ButtonClickHandler(int buttonNumber, string buttonText);
    #endregion

    #region delegate void ExpandButtonClickHandler(object sender, bool expanded);
    public delegate void ExpandButtonClickHandler(object sender, bool expanded);
    #endregion

    #region delegate void OnCheckChangedHandler(object sender, bool isChecked);
    public delegate void OnCheckChangedHandler(object sender, bool isChecked);
    #endregion

}