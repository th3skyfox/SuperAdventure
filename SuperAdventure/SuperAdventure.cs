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

        }

        private void btnWest_Click(object sender, EventArgs e)
        {

        }

        private void btnEast_Click(object sender, EventArgs e)
        {

        }

        private void btnSouth_Click(object sender, EventArgs e)
        {

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
                    _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));
                }
            }




        }
    }
}
