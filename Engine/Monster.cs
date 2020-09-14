using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace Engine
{
    public class Monster : LivingCreature
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RewardExperiencePoints { get; set; }
        public int RewardGold { get; set; }

        public List<LootItem> LootTable { get; set; }

        public Monster(int id, string name, int rewardexperiencepoints, int rewardgold, int maximumpoints, int currentpoints) : base(maximumpoints, currentpoints) 
        {
            ID = id;
            Name = name;
            RewardExperiencePoints = rewardexperiencepoints;
            RewardGold = rewardgold;

            LootTable = new List<LootItem>();
        }
    }
}
