using Rocket.API;

namespace PlayerHome
{
    public class Config : IRocketPluginConfiguration
    {
        public bool cancelOnMove;
        public int delay;
        public void LoadDefaults()
        {
            cancelOnMove = true;
            delay = 5;
        }
    }
}
