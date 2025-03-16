using System.Collections.Generic;

namespace I_am_Hero_WPF.Models
{
    public class HeroStatusEffectsResponse
    {
        public List<HeroStatusEffect> HeroStatusEffects { get; set; }
        public string Token { get; set; }
    }
}
