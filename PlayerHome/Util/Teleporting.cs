using Steamworks;
using System.Collections.Generic;

namespace PlayerHome.Util
{
    public class Teleporting
    {
        private readonly Dictionary<CSteamID, bool> cache;

        public Teleporting()
        {
            cache = new Dictionary<CSteamID, bool>();
        }

        public void Add(CSteamID key)
        {
            cache[key] = true;
        }
        public bool Has(CSteamID key)
        {
            if (cache.ContainsKey(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Remove(CSteamID key)
        {
            return cache.Remove(key);
        }

        public void Clear()
        {
            cache.Clear();
        }
    }
}
