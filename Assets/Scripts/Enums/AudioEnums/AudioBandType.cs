namespace Assets.Enums.AudioEnums
{
    // https://fdstar.com/3456-pamyatka_muzykantu_chastoty.html
    // https://www.cuidevices.com/blog/understanding-audio-frequency-range-in-audio-design
    public enum AudioBandType
    {
        SubBass,    // 10 - 80 hertz
        Bass,  // 80 - 200 hertz
        LowerMidrange,    // 200 - 500 hertz
        Midrange, // 500 - 1500 hertz
        UpperMidrange, // 1500 - 2500 hertz
        HigherMidrange,   // 2500 - 5000 hertz
        Presence,    // 5000 - 10000 hertz
        Brilliance   // 10000 - 20000 hertz
    }
}