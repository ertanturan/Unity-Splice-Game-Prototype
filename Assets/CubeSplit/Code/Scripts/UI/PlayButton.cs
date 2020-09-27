public class PlayButton : ButtonComponent
{
    public override void OnButtonClick()
    {
        GameManager.Instance.StartGame();
        GetComponentInParent<WindowUI>().Hide();
    }
}
