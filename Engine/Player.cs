using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Player : LivingCreature
    {

        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }
        public int Gold { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }

        public Player(int maximumpoints, int currentpoints, int gold, int experiencepoints, int level) : base(maximumpoints, currentpoints)
        {
            Gold = gold;
            ExperiencePoints = experiencepoints;
            Level = level;

            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }
        
    }
}
