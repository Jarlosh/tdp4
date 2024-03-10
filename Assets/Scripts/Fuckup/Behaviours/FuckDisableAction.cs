namespace TDPF.FuckUp
{
    public class FuckDisableAction: FuckUpBehaviour
    {
        protected override void SetState(bool state)
        {
            gameObject.SetActive(state);       
        }
    }
}