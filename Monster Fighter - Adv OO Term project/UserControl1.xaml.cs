using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonsterLib
{
    public class hero
    {
        public String name;
        public Boolean block = false;
        public int health = 20;
        public int lvl = 1;
        public int exp = 0;
        public healthPotion potion = new healthPotion(50);
        public Boolean alive = true;

        // DEPENDENCY
        public void attack(monster monster1)
        {
            Random random = new Random();
            if (this.lvl == 1)
            {
                monster1.health = monster1.health - random.Next(3, 5);
            }
            if (this.lvl == 2)
            {
                monster1.health = monster1.health - random.Next(5, 7);

            }
            if (this.lvl == 3)
            {
                monster1.health = monster1.health - random.Next(5, 13);

            }
            if (this.lvl == 4)
            {
                monster1.health = monster1.health - random.Next(6, 15);
            }
        }
        public void death()
        {
            if (health <= 0)
            {
                alive = false;
            }
            else
            {
                alive = true;
            }
        }

        // AGGREGATION + COMPISITION
        public Boolean heal()
        {
            if (potion.getHealth() > 0)
            {
                health = health + 10;
                potion.setHealth(potion.getHealth() - 10);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
    // INHERITANCE
    public class monster : hero
    {
        public healthPotion potion1 = new healthPotion(10);

        public monster(String name, int health, int lvl)
        {
            this.name = name;
            this.health = health;
            this.lvl = lvl;
            this.alive = true;

        }


        // DEPENDENCY
        public void attack(hero hero1)
        {
            Random random = new Random();
            if (lvl == 1)
            {
                hero1.health = hero1.health - random.Next(3, 5);
            }
            if (lvl == 2)
            {
                hero1.health = hero1.health - random.Next(3, 8);

            }
            if (lvl == 3)
            {
                hero1.health = hero1.health - random.Next(5, 10);

            }
            if (lvl == 4)
            {
                hero1.health = hero1.health - random.Next(7, 17);
            }
        }

    }
    public class healthPotion
    {
        private int health = 20;
        public healthPotion(int health1)
        {
            health = health1;
        }
        public int getHealth()
        {
            return health;
        }
        public void setHealth(int newHealth)
        {
            health = newHealth;
        }
    }
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
    }
}
