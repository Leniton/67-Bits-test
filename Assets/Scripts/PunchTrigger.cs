using UnityEngine;

public class PunchTrigger : MonoBehaviour
{

    //total time: .58s
    //.5 + 1.4
    public void TriggerPunch()
    {
        Event<PunchTriggeredEvent>.CallEvent(new(1f));
    }
}
