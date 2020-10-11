public class NoThanksButton : ButtonComponent
{

    public override void OnButtonClick()
    {
        GameManager.Instance.RestartGmae();
    }
}
