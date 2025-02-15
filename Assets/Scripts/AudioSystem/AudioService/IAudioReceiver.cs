namespace Assets.AudioSystem.AudioService
{
    public interface IAudioReceiver
    {
        void ReceiveAudioData(float[] channel);
    }
}
