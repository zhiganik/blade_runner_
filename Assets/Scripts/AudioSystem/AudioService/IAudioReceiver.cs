using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioReceiver
{
    void ReceiveAudioData(float[] channel);
}
