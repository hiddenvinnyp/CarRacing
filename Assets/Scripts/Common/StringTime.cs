using System;

public static class StringTime
{
    public static string SecondToTimeSpring(float seconds)
    {
        return TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss\:ff");
    }
}
