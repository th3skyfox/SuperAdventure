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

            Monster monster = new Monster(1, "NineTail Cat", 5, 10, 20, 20);
            InitializeComponent();
            _player = new Player(10,10,5,0,1);

            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblLevel.Text = _player.Level.ToString();

        }

    }
}
