namespace DnDEZBackEnd.Models.Public_Classes
{
    public class StatAdjuster
    {
        public Character abilityRaceDecrease(Character c)
        {
            Race ogRace = DnDRaceDAL.getRace(c.Race);
            foreach (CharAbilityScore abi in c.CharAbilityScores)
            {
                if (ogRace.ability_bonuses.FirstOrDefault(a => a.ability_score.index == abi.Index) != null)
                {
                    Ability_Bonuses bonus = ogRace.ability_bonuses.FirstOrDefault(a => a.ability_score.index == abi.Index);
                    c.CharAbilityScores.FirstOrDefault(a => a.Index == abi.Index).Value -= bonus.bonus;
                    c.CharAbilityScores.FirstOrDefault(a => a.Index == abi.Index).RacialBonus = false;
                }
            }

            return c;
        }

        public Character abilityRaceIncrease(Character c, Race r)
        {
            foreach (CharAbilityScore abi in c.CharAbilityScores)
            {
                if (r.ability_bonuses.FirstOrDefault(a => a.ability_score.index == abi.Index) != null)
                {
                    Ability_Bonuses bonus = r.ability_bonuses.FirstOrDefault(a => a.ability_score.index == abi.Index);
                    c.CharAbilityScores.FirstOrDefault(a => a.Index == abi.Index).Value += bonus.bonus;
                    c.CharAbilityScores.FirstOrDefault(a => a.Index == abi.Index).RacialBonus = true;
                }
            }

            return c;
        }
    }
}
