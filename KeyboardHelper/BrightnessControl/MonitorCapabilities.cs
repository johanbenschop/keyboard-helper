using System;

namespace KeyboardHelper.BrightnessControl
{
    [Flags]
    public enum MonitorCapabilities
    {
        Brightness = 1,
        ColorTemperature = 2,
        Contrast = 4,
        Degauss = 8,
        DisplayAreaPosition = 16,
        DisplayAreaSize = 32,
        MonitorTechnologyType = 64,
        None = 128,
        RedGreenBlueDrive = 256,
        RedGreenBlueGain = 512,
        RestoreFactoryColorDefaults = 1024,
        RestoreFactoryDefaults = 2048,
        RestoreFactoryDefaultsEnablesMonitorSettings = 4096,
    }
}
