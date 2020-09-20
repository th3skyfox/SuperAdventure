using Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SuperAdventure
{
    public partial class SuperAdventure : Form
    {
        private Player _player;
        private Monster _currentMonster;
        public SuperAdventure()
        { 
            InitializeComponent();
            _player = new Player(10,10,5,0,1);

            MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            _player.Inventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD),3));
            
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblLevel.Text = _player.Level.ToString();

        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToNorth);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToEast);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToSouth);
        }

        private void MoveTo(Location newLocation)
        {
            // Check Location require item to enter
            if (newLocation.ItemRequiredToEnter != null)
            {
                bool playerhasitem = false;

                // looping through player inventory
                foreach(InventoryItem ii in _player.Inventory)
                {
                    if(newLocation.ItemRequiredToEnter.ID == ii.Details.ID)
                    {
                        // item require found in player inventory
                        playerhasitem = true;

                        // no need to search afterwards
                        break;
                    }
                }

                if(!playerhasitem)
                {
                    //Displaying messages and exit movement;
                    rtbMessages.Text += "you don't have" + newLocation.ItemRequiredToEnter.Name + " to enter in the location" + Environment.NewLine;
                    return;
                }
            }

            _player.CurrentLocation = newLocation;

            rtbLocation.Text = "You are at " + newLocation.Name;

            btnEast.Visible = (newLocation.LocationToEast != null);
            btnWest.Visible = (newLocation.LocationToWest != null);
            btnNorth.Visible = (newLocation.LocationToNorth != null);
            btnSouth.Visible = (newLocation.LocationToSouth != null);

            _player.CurrentHitPoints = _player.MaximumHitPoints;

            lblHitPoints.Text = _player.CurrentHitPoints.ToString();

            if (newLocation.QuestAvailableHere != null)
            {
                bool playerhasquest = false;
                bool playercompletedquest = false;
                foreach(PlayerQuest playerquest in _player.Quests)
                {
                    if(playerquest.Details.ID == newLocation.QuestAvailableHere.ID)
                    {
                        playerhasquest = true;
                        if (playerquest.IsCompleted)
                        {
                            playercompletedquest = true;
                        }
                        break;

                    }
                }
                if (playerhasquest)
                {
                    if (!playercompletedquest)
                    {
                        bool playerhasallitem = true;

                        foreach(QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                        {
                            bool founditemininventory = false;
                            foreach(InventoryItem ii in _player.Inventory)
                            {
                                if(qci.Details.ID == ii.Details.ID)
                                {
                                    founditemininventory = true;
                                    if(qci.Quantity > ii.Quantity)
                                    {
                                        playerhasallitem = false;
                                        break;
                                    }
                                    break;
                                }

                            }

                            if(!founditemininventory)
                            {
                                playerhasallitem = false;
                                break;

                            }

                        }

                        if (playerhasallitem)
                        {
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += "You complete the '" + newLocation.QuestAvailableHere.Name + "' quest." + Environment.NewLine;

                            foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                            {
                                foreach (InventoryItem ii in _player.Inventory)
                                {
                                    if (qci.Details.ID == ii.Details.ID)
                                    {
                                        ii.Quantity -= qci.Quantity;
                                    }

                                }
                            }
                            rtbMessages.Text += "You receive :" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardExperiencePoints.ToString() + "experience points" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardGold.ToString() + "Gold" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardItem.Name + "Items" + Environment.NewLine;
                            rtbMessages.Text += Environment.NewLine;
                            

                            _player.ExperiencePoints += newLocation.QuestAvailableHere.RewardExperiencePoints;

                            _player.Gold += newLocation.QuestAvailableHere.RewardGold;

                            bool additemtoinventory = false;
                            foreach (InventoryItem ii in _player.Inventory)
                            {
                                if (ii.Details.ID == newLocation.QuestAvailableHere.RewardItem.ID)
                                {
                                    additemtoinventory = true;
                                    ii.Quantity += 1;
                                    break;
                                }
                            }

                            if (!additemtoinventory)
                            {
                                _player.Inventory.Add(new InventoryItem(newLocation.QuestAvailableHere.RewardItem, 1));
                            }



                            foreach (PlayerQuest pq in _player.Quests)
                            {
                                if(pq.Details.ID == newLocation.QuestAvailableHere.ID)
                                {
                                    pq.IsCompleted = true;
                                    break;
                                }
                            }
                        }

                    }

                }
                else
                {
                    rtbMessages.Text += "You receive the " + newLocation.QuestAvailableHere.Name + " quest." + Environment.NewLine;
                    rtbMessages.Text += newLocation.QuestAvailableHere.Description + Environment.NewLine;
                    rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;


                    foreach(QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                    {
                        if (qci.Quantity == 1)
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;
                            
                        }
                        else
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                        }
                    }
                    rtbMessages.Text += Environment.NewLine;
                    
                    _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));
                }
            }

            if (newLocation.MonsterLivingHere != null)
            {
                rtbMessages.Text += "You see a monster " + newLocation.MonsterLivingHere.Name + Environment.NewLine;

                Monster standardMonster = World.MonsterByID(newLocation.MonsterLivingHere.ID);
                _currentMonster = new Monster(standardMonster.ID, standardMonster.Name, standardMonster.MaximumDamage,standardMonster.RewardExperiencePoints, standardMonster.RewardGold,standardMonster.MaximumHitPoints,standardMonster.CurrentHitPoints);

                foreach(LootItem lt in standardMonster.LootTable)
                {
                    _currentMonster.LootTable.Add(lt);
           
                }
                cboPotions.Visible = true;
                cboWeapons.Visible = true;
                btnUseWeapon.Visible = true;
                btnUsePotion.Visible = true;


            }
            else
            {
                _currentMonster = null;

                cboPotions.Visible = false;
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
                btnUsePotion.Visible = false;

            }

            dgvInventory.RowHeadersVisible = false;

            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";

            foreach(InventoryItem ii in _player.Inventory)
            {
                if(ii.Quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] { ii.Details.Name, ii.Quantity.ToString() });
                }

            }

            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;

            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done ?";

            foreach(PlayerQuest pq in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] { pq.Details.Name, pq.IsCompleted.ToString() });
            }

            List<Weapon> weapons = new List<Weapon>();

            foreach(InventoryItem ii in _player.Inventory)
            {
                if(ii.Details is Weapon)
                {
                    if(ii.Quantity > 0)
                    {
                        weapons.Add((Weapon)ii.Details);
                    }
                }
            }
            if(weapons.Count == 0)
            {
                cboWeapons.Visible =false;
                btnUseWeapon.Visible = false;

            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";

                cboWeapons.SelectedIndex = 0;

            }
            List<HealingPotion> healingPotions = new List<HealingPotion>();

            foreach(InventoryItem ii in _player.Inventory)
            {
                if(ii.Details is HealingPotion)
                {
                    if (ii.Quantity > 0)
                    {
                        healingPotions.Add((HealingPotion)ii.Details);
                    }
                }
                
            }             


            if(healingPotions.Count == 0)
            {
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPotions;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";

                cboPotions.SelectedIndex = 0;
            }
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {

        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {

        }
    }
}

