using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class World
    {
        public static readonly List<Item> Items = new List<Item>();
        public static readonly List<Monster> Monsters = new List<Monster>();
        public static readonly List<Quest> Quests = new List<Quest>();
        public static readonly List<Location> Locations = new List<Location>();

        public const int ITEM_ID_RUSTY_SWORD = 1;
        public const int ITEM_ID_RAT_TAIL = 2;
        public const int ITEM_ID_PIECE_OF_FUR= 3;
        public const int ITEM_ID_SNAKE_FANG = 4;
        public const int ITEM_ID_SNAKESKIN = 5;
        public const int ITEM_ID_CLUB = 6;
        public const int ITEM_ID_HEALING_POTION = 7;
        public const int ITEM_ID_SPIDER_FANG = 8;
        public const int ITEM_ID_SPIDER_SILK = 9;
        public const int ITEM_ID_ADVENTURE_PASS = 10;

        public const int MONSTER_ID_RAT = 1;
        public const int MONSTER_ID_SNAKE = 2;
        public const int MONSTER_ID_GIANT_SPIDER = 3;

        public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
        public const int QUEST_ID_CLEAR_FARMER_FIELD = 2;


        public const int LOCATION_ID_HOME = 1;
        public const int LOCATION_ID_TOWN_SQUARE = 2;
        public const int LOCATION_ID_ALCHEMIST_GARDEN= 3;
        public const int LOCATION_ID_HERB_GARDEN = 4;
        public const int LOCATION_ID_FARM_HOUSE = 5;
        public const int LOCATION_ID_FARM_FIELD = 6;
        public const int LOCATION_ID_GUARD_HOUSE = 7;
        public const int LOCATION_ID_BRIDGE = 8;
        public const int LOCATION_ID_SPIDER_FOREST = 9;

        static World()
        { 
            PopulateItems();
            PopulateMonsters();
            PopulateLocation();
            PopulateQuest();
        }

        private static void PopulateItems()
        {
            Items.Add(new Weapon(ITEM_ID_RUSTY_SWORD, "Rusty Sword", "Rusty Swords", 0, 5));
            Items.Add(new Item(ITEM_ID_RAT_TAIL,"Rat Tail","Rat Tails"));
            Items.Add(new Item(ITEM_ID_PIECE_OF_FUR,"Piece Of Fur","Piece of Furs"));
            Items.Add(new Item(ITEM_ID_SNAKE_FANG,"Snake Fang","Snake Fangs"));
            Items.Add(new Item(ITEM_ID_SNAKESKIN, "Snake Skin", "Snake Skins"));
            Items.Add(new Weapon(ITEM_ID_CLUB, "Club", "Clubs", 3, 10));
            Items.Add(new HealingPotion(ITEM_ID_HEALING_POTION, "Healing Potion", "Healing Potions", 5));
            Items.Add(new Item(ITEM_ID_SPIDER_FANG, "Spider Fang", "Spider Fangs"));
            Items.Add(new Item(ITEM_ID_SPIDER_SILK, "Spider Silk", "Spider Silks"));
            Items.Add(new Item(ITEM_ID_ADVENTURE_PASS, "Adventure Pass", "Adventure Passes"));
        }

        private static void PopulateMonsters()
        {
            Monster rat = new Monster(MONSTER_ID_RAT,"RAT",5,3,10,3);
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 75, false));
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_PIECE_OF_FUR), 75, true));

            Monster snake = new Monster(MONSTER_ID_SNAKE, "snake", 5, 3, 10, 3);
            snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKESKIN), 50, true));
            snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_FANG), 75, false));

            Monster spider = new Monster(MONSTER_ID_GIANT_SPIDER, "Giant Spider", 20, 10, 40, 10);
            spider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_FANG), 75, true));
            spider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_SILK), 25, false));

            Monsters.Add(rat);
            Monsters.Add(snake);
            Monsters.Add(spider);

        }

        private static void PopulateQuest()
        {
            Quest clearalchemistgardern = new Quest(QUEST_ID_CLEAR_ALCHEMIST_GARDEN, "Alchemist Garden", "Kill rats in the alchemist's garden and bring back 3 rat tails. You will receive a healing potion and 10 gold pieces.", 20, 10);

            clearalchemistgardern.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_RAT_TAIL),3));
            clearalchemistgardern.RewardItem = ItemByID(ITEM_ID_HEALING_POTION);

            Quest clearfarmerfield = new Quest(QUEST_ID_CLEAR_FARMER_FIELD, "Farmer Fields", "Kill snakes in the farmer's field and bring back 3 snake fangs. You will receive an adventurer's pass and 20 gold pieces.", 10, 20);
            clearalchemistgardern.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SNAKE_FANG), 3));
            clearalchemistgardern.RewardItem = ItemByID(ITEM_ID_ADVENTURE_PASS);

            Quests.Add(clearalchemistgardern);
            Quests.Add(clearfarmerfield);        
        }

        private static void PopulateLocation()
        {
            Location home = new Location(LOCATION_ID_HOME, "Home", "Your house. You really need to clean up the place.");
            Location townsquare = new Location(LOCATION_ID_TOWN_SQUARE, "Town Square","Your see a Fountain");
            Location alchemsisthut = new Location(LOCATION_ID_ALCHEMIST_GARDEN, "Alchemist Garden", "There are many things plants on the shelves");
            alchemsisthut.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN);

            Location alchemistGarden = new Location(LOCATION_ID_HERB_GARDEN, "Herb Garden", "Many Plants are growing here.");
            alchemistGarden.MonsterLivingHere = MonsterByID(MONSTER_ID_RAT);

            Location farmerfield = new Location(LOCATION_ID_FARM_FIELD, "Farmer Field", "You see rows of vegetables growing here.");
            farmerfield.MonsterLivingHere = MonsterByID(MONSTER_ID_SNAKE);

            Location farmhouse = new Location(LOCATION_ID_FARM_HOUSE, "farm house", "There is a small farmhouse, with a farmer in front.");
            farmhouse.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_FARMER_FIELD);

            Location guardhouse = new Location(LOCATION_ID_GUARD_HOUSE, "Guard House", "There is a large, tough-looking guard here.",ItemByID(ITEM_ID_ADVENTURE_PASS));

            Location bridge = new Location(LOCATION_ID_BRIDGE, "Bridge", "A stone bridge crosses a wide river.");

            Location spider_forest = new Location(LOCATION_ID_SPIDER_FOREST, "Forest", "You see spider webs covering forest.");
            spider_forest.MonsterLivingHere = MonsterByID(MONSTER_ID_GIANT_SPIDER);

            //Location connection

            home.LocationToNorth = townsquare;

            townsquare.LocationToNorth = alchemsisthut;
            townsquare.LocationToWest = farmhouse;
            townsquare.LocationToSouth = home;
            townsquare.LocationToEast = guardhouse;

            alchemsisthut.LocationToSouth = townsquare;
            alchemsisthut.LocationToNorth = alchemistGarden;

            alchemistGarden.LocationToSouth = alchemsisthut;

            farmhouse.LocationToWest = farmerfield;
            farmhouse.LocationToEast = townsquare;

            farmerfield.LocationToEast = farmhouse;

            guardhouse.LocationToWest = townsquare;
            guardhouse.LocationToEast = bridge;

            bridge.LocationToWest = guardhouse;
            bridge.LocationToEast = spider_forest;

            spider_forest.LocationToWest = bridge;

            Locations.Add(home);
            Locations.Add(townsquare);
            Locations.Add(alchemsisthut);
            Locations.Add(alchemistGarden);
            Locations.Add(farmhouse);
            Locations.Add(farmerfield);
            Locations.Add(guardhouse);
            Locations.Add(bridge);
            Locations.Add(spider_forest);


        }

        public static Item ItemByID(int id)
        {
            foreach(Item item in Items)
            {
                if(item.ID == id)
                {
                    return item;  
                }
            }
            return null;
        }

        public static Quest QuestByID(int id)
        {
            foreach(Quest quest in Quests)
            {
                if(quest.ID == id)
                {
                    return quest;
                }
            }
            return null;
        }

        public static Monster MonsterByID(int id)
        {
            foreach(Monster monster in Monsters)
            {
                if(monster.ID == id)
                {
                    return monster;
                }

            }
            return null;
        }

        public static Location LocationByID(int id)
        {
            foreach(Location location in Locations)
            {
                if(location.ID == id)
                {
                    return location;
                }
            }
            return null;
        }

    }
}