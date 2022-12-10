using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonsterLib;


namespace MonsterFighter
{
    public partial class MonsterFighter : Form
    {
        int dmg;
        int potions = 2;
        hero hero = new hero();
        monster reaper = new monster("Reaper", 15, 1);
        monster ogre = new monster("Icy Ogre", 30, 2);
        monster sorc = new monster("Dark Sorcerer", 55, 3);
        monster drag = new monster("Scorched Dragon", 80, 3);


        public MonsterFighter()
        {
            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                hero.name = textBox1.Text;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (hero.name.Length > 25)
            {
                label2.Text = "Name is too long. Please re-enter";
                label2.Visible = true;
                label2.BackColor = Color.Red;
                textBox1.Text = "";
            }
            if (hero.name.Length == 0)
            {
                label2.Text = "Please enter a name";
                label2.Visible = true;
                label2.BackColor = Color.Red;
                textBox1.Text = "";
            }
            else
            {
                label2.Visible = false;
                textBox1.Visible = false;
                label1.Visible = false;
                button1.Visible = false;
                pictureBox1.Visible = false;
                HeroName.Text = hero.name;
                HeroName.Visible = true;
                heroPic.Visible = true;
                heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;
                heroStats.Visible = true;
                reaperPic.Visible = true;
                attackBut.Visible = true;
                healBut.Visible = true;
                blockBut.Visible = true;
                monsterHealthLab.Text = "Health: " + reaper.health;
                monsterHealthLab.Visible = true;
                monsterNameLab.Text = reaper.name;
                monsterNameLab.Visible = true;
                potLab.Visible = true;
                potLab.Text = "Potions: " + potions;
            }
        }

        private void attackBut_Click(object sender, EventArgs e)
        {
            attackBut.Visible = false;
            healBut.Visible = false;
            blockBut.Visible = false;
            if (reaper.alive)
            {
                dmg = reaper.health;
                hero.attack(reaper);
                dmg = dmg - reaper.health;
                dialogLab.Text = hero.name + " attacked  Reaper for " + dmg + " points of damage!";
                monsterHealthLab.Text = "Health: " + reaper.health;
            }
            else if (ogre.alive)
            {
                dmg = ogre.health;
                hero.attack(ogre);
                dmg = dmg - ogre.health;
                dialogLab.Text = hero.name + " attacked Icy Ogre for " + dmg + " points of damage!";
                monsterHealthLab.Text = "Health: " + ogre.health;
            }
            else if (sorc.alive)
            {
                dmg = sorc.health;
                hero.attack(sorc);
                dmg = dmg - sorc.health;
                dialogLab.Text = hero.name + " attacked Dark Sorcerer for " + dmg + " points of damage!";
                monsterHealthLab.Text = "Health: " + sorc.health;
            }
            else if (drag.alive)
            {
                dmg = drag.health;
                hero.attack(drag);
                dmg = dmg - drag.health;
                dialogLab.Text = hero.name + " attacked Scorched Dragon for " + dmg + " points of damage!";
                monsterHealthLab.Text = "Health: " + drag.health;
            }

            dialogLab.Visible = true;
            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (reaper.alive)
            {
                reaper.death();
                if (reaper.alive == false)
                {

                    dialogLab.Text = "Reaper was defeated!\n" + hero.name + " leveled up!\nReady for the next battle?";
                    monsterHealthLab.Text = "Health: 0";
                    monsterDeath.Visible = true;
                    reaperPic.Visible = false;
                    hero.lvl = hero.lvl + 1;
                    heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;
                    nextBattleBut.Visible = true;
                    quitBut.Visible = true;
                    timer1.Enabled = false;
                }
                else
                {
                    dmg = hero.health;
                    reaper.attack(hero);
                    if (hero.block)
                    {
                        hero.health = hero.health + 3;
                    }
                    dmg = dmg - hero.health;
                    dialogLab.Text = "Reaper attacked " + hero.name + " for " + dmg + " points of damage!";
                    timer1.Enabled = false;
                    attackBut.Visible = true;
                    blockBut.Visible = true;
                    healBut.Visible = true;
                    heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;
                }
            }
            else if (reaper.alive == false && ogre.alive == true)
            {
                ogre.death();
                if (ogre.alive == false)
                {
                    dialogLab.Text = "Icy Ogre was defeated!\n" + hero.name + " leveled up!\nReady for the next battle?";
                    monsterHealthLab.Text = "Health: 0";
                    monsterDeath.Visible = true;
                    ogrePic.Visible = false;
                    hero.lvl = hero.lvl + 1;
                    heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;
                    nextBattleBut.Visible = true;
                    quitBut.Visible = true;
                    timer1.Enabled = false;
                }
                else
                {
                    dmg = hero.health;
                    ogre.attack(hero);
                    dmg = dmg - hero.health;
                    dialogLab.Text = "Ogre attacked " + hero.name + " for " + dmg + " points of damage!";
                    timer1.Enabled = false;
                    attackBut.Visible = true;
                    blockBut.Visible = true;
                    healBut.Visible = true;
                    heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;
                }
            }
            else if (ogre.alive == false && sorc.alive == true)
            {
                sorc.death();
                if (sorc.alive == false)
                {
                    dialogLab.Text = "Dark sorcerer was defeated!\n" + hero.name + " leveled up!\nReady for the next battle?";
                    monsterHealthLab.Text = "Health: 0";
                    monsterDeath.Visible = true;
                    sorcererPic.Visible = false;
                    hero.lvl = hero.lvl + 1;
                    heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;
                    nextBattleBut.Visible = true;
                    quitBut.Visible = true;
                    timer1.Enabled = false;
                    
                }

                else
                {
                    dmg = hero.health;
                    sorc.attack(hero);
                    dmg = dmg - hero.health;
                    dialogLab.Text = "Dark Sorcerer attacked " + hero.name + " for " + dmg + " points of damage!";
                    timer1.Enabled = false;
                    attackBut.Visible = true;
                    blockBut.Visible = true;
                    healBut.Visible = true;
                    heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;
                }
            }
            else if (sorc.alive == false && drag.alive == true)
            {
                drag.death();
                if (drag.alive == false)
                {
                    dialogLab.Text = "The Scorched Dragon was defeated!\n" + hero.name + " saved the kingdom and brought peace to the land!";
                    monsterHealthLab.Text = "Health: 0";
                    monsterDeath.Visible = true;
                    dragonPic.Visible = false;
                    hero.lvl = hero.lvl + 1;
                    heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;
                    quitBut.Visible = true;
                    timer1.Enabled = false;
                    monsterHealthLab.Visible = false;
                    monsterNameLab.Visible = false;
                    retry.Visible = true;
                 
                }
                else
                {
                    dmg = hero.health;
                    drag.attack(hero);
                    dmg = dmg - hero.health;
                    dialogLab.Text = "Scorched Dragon attacked " + hero.name + " for " + dmg + " points of damage!";
                    timer1.Enabled = false;
                    attackBut.Visible = true;
                    blockBut.Visible = true;
                    healBut.Visible = true;
                    heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;
                }
            }
            hero.death();
            if (hero.alive == false)
            {
                heroPic.Visible = false;
                dialogLab.Text = "Game over, " + hero.name + " was defeated in battle.";
                timer1.Enabled = false;
                quitBut.Visible = true;
                attackBut.Visible = false;
                blockBut.Visible = false;
                healBut.Visible = false;
                retry.Visible = true;
            }
        }

        private void nextBattleBut_Click(object sender, EventArgs e)
        {
            if (hero.lvl == 2)
            {
                hero.health = 30;
                heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;

            }
            if (hero.lvl == 3)
            {
                hero.health = 40;
                heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;

            }
            if (hero.lvl == 4)
            {
                hero.health = 50;
                heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;

            }
            if (reaper.alive == false && ogre.alive == true)
            {

                ogrePic.Visible = true;
                monsterNameLab.Text = ogre.name;
                monsterHealthLab.Text = "Health: " + ogre.health;

            }
            if (ogre.alive == false && sorc.alive == true)
            {

                sorcererPic.Visible = true;
                monsterNameLab.Text = sorc.name;
                monsterHealthLab.Text = "Health: " + sorc.health;


            }
            if (sorc.alive == false && drag.alive == true)
            {

                dragonPic.Visible = true;
                monsterNameLab.Text = drag.name;
                monsterHealthLab.Text = "Health: " + drag.health;


            }
            hero.potion.setHealth(20);
            dialogLab.Text = "";
            dialogLab.Visible = false;
            attackBut.Visible = true;
            blockBut.Visible = true;
            healBut.Visible = true;
            nextBattleBut.Visible = false;
            quitBut.Visible = false;
            monsterDeath.Visible = false;
            potions = 2;
            potLab.Text = "Potions: " + potions;
        }

        private void retry_Click(object sender, EventArgs e)
        {
            heroPic.Visible = true;
            hero.health = 20;
            hero.lvl = 1;
            heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;
            HeroName.Visible = true;
            potions = 2;
            potLab.Text = "Potions: " + potions;

            reaper.alive = true;
            reaper.health = 15;
            ogre.alive = true;
            ogre.health = 35;
            sorc.alive = true;
            sorc.health = 55;
            drag.health = 80;
            drag.alive = true;
            reaperPic.Visible = true;
            ogrePic.Visible = false;
            sorcererPic.Visible = false;
            dragonPic.Visible = false;
            attackBut.Visible = true;
            healBut.Visible = true;
            blockBut.Visible = true;
            monsterDeath.Visible = false;
            monsterNameLab.Text = reaper.name;
            monsterHealthLab.Text = "Health: " + reaper.health;
            retry.Visible = false;
            quitBut.Visible = false;
            dialogLab.Visible = false;
            hero.potion.setHealth(20);
        }

        private void quitBut_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void healBut_Click(object sender, EventArgs e)
        {
            attackBut.Visible = false;
            healBut.Visible = false;
            blockBut.Visible = false;
            if (hero.heal())
            {
                potions--;
                heroStats.Text = "Level: " + hero.lvl + "\n" + "Health: " + hero.health;
                dialogLab.Text = hero.name + " healed 10 points of health!";
                dialogLab.Visible = true;
                potLab.Text = "Potions: " + potions;
                timer1.Enabled = true;
            }
            else
            {
                dialogLab.Text = "You are out of potions to heal with during this battle";
                dialogLab.Visible = true;
                attackBut.Visible = true;
                blockBut.Visible = true;
                healBut.Visible = true;
            }
        }

        private void blockBut_Click(object sender, EventArgs e)
        {
            hero.block = true;
            attackBut.Visible = false;
            healBut.Visible = false;
            blockBut.Visible = false;
            timer1.Enabled = true;
            dialogLab.Text = hero.name + " prepares to block!";
            dialogLab.Visible = true;
        }

        private void MonsterFighter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
   
}