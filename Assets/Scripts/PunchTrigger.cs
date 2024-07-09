using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
    public void TriggerPunch()
    {
        Event<PunchTriggeredEvemt>.CallEvent(new());
    }
}
