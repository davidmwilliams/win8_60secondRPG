using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace _60s_RPG
{
    public sealed partial class MainPage : Page
    {
        enum States { STARTING, CHOOSECHARACTER, INROOM, FIGHTING, DEATH };
        enum Classes { FIGHTER, MAGE, THIEF };

        private States State;
        private Classes Class;
        private int HP;
        private int MaxHP;
        private int XP;
        private int Level;
        private int NextLevelXP;
        private int Strength;
        private int Magic;
        private int MaxMagic;
        private int Agility;
        private int Protection;
        private int Gold;
        private int Food;
        private int HPPotions;
        private int ManaPotions;

        private string MonsterName;
        private int MonsterHP;
        private int MonsterStrength;
        private int MonsterAgility;

        private Random random = new Random();

        public MainPage()
        {
            this.InitializeComponent();
            BeginTheAdventure();
        }

        private void DisplayStats()
        {
            txtHP.Text = String.Format("{0} / {1}", HP, MaxHP);
            txtXP.Text = String.Format("{0} / level {1}", XP, Level);
            txtStrength.Text = String.Format("{0}", Strength);
            txtMagic.Text = String.Format("{0} / {1}", Magic, MaxMagic);
            txtAgility.Text = String.Format("{0}", Agility);
            txtProtection.Text = String.Format("{0}", Protection);
            txtGold.Text = String.Format("{0}", Gold);
            txtFood.Text = String.Format("{0}", Food);
            txtHPPotions.Text = String.Format("{0}", HPPotions);
            txtManaPotions.Text = String.Format("{0}", ManaPotions);

            if (ManaPotions == 0)
                bFour.Visibility = Visibility.Collapsed;
            else
                bFour.Visibility = Visibility.Visible;

            if (Food == 0)
                bFive.Visibility = Visibility.Collapsed;
            else
                bFive.Visibility = Visibility.Visible;

            if (HPPotions == 0)
                bSix.Visibility = Visibility.Collapsed;
            else
                bSix.Visibility = Visibility.Visible;

            if (State == States.FIGHTING)
            {
                if (Magic == 0)
                    bThree.Visibility = Visibility.Collapsed;
                else
                    bThree.Visibility = Visibility.Visible;
            }
        }

        private void BeginTheAdventure()
        {
            HP = 0;
            MaxHP = 0;
            XP = 0;
            Level = 1;
            NextLevelXP = 20;
            Strength = 0;
            Magic = 0;
            MaxMagic = 0;
            Agility = 0;
            Protection = 0;
            Gold = 0;
            Food = 0;
            HPPotions = 0;
            ManaPotions = 0;
            DisplayStats();

            txtMain.Text =
            "                 ,\n" +
            "                / \\\n" +
            "               {   }                    #####  ####\n" +
            "               p   !                    #      #  #\n" +
            "               ; : ;                    #####  #  #\n" +
            "               | : |                    #   #  #  #\n" +
            "               | : |                    #####  ####\n" +
            "               l ; l                    S E C O N D\n" +
            "               l ; l                    ####   ####  ####\n" +
            "               I ; I                    #   #  #  #  #\n" +
            "               I ; I                    ####   ####  # ##\n" +
            "               I ; I                    #  #   #     #  #\n" +
            "               I ; I                    #   #  #     ####\n" +
            "               d | b\n" +
            "               H | H\n" +
            "               H | H\n" +
            "               H I H\n" +
            "       ,;,     H I H     ,;,\n" +
            "      ;H@H;    ;_H_;,   ;H@H;\n" +
            "      `\\Y/d_,;|4H@HK|;,_b\\Y/'\n" +
            "       '\\;MMMMM$@@@$MMMMM;/'\n" +
            "         \"~~~*;!8@8!;*~~~\"\n" +
            "               ;888;\n" +
            "               ;888;\n" +
            "               ;888;\n" +
            "               ;888;\n" +
            "               d8@8b\n" +
            "               O8@8O\n" +
            "               T808T\n" +
            "                `~`\n\n\n" +
            "W E L C O M E   T O   T H E   A D V E N T U R E\n\n" +
            "Are you ready to begin?\n\n";

            State = States.STARTING;
            bForward.Visibility = Visibility.Collapsed;
            bLeft.Visibility = Visibility.Collapsed;
            bRight.Visibility = Visibility.Collapsed;
            bThree.Visibility = Visibility.Collapsed;
            bFour.Visibility = Visibility.Collapsed;
            bFive.Visibility = Visibility.Collapsed;
            bSix.Visibility = Visibility.Collapsed;
            bOne.Content = "Yes";
            bTwo.Content = "No";
            bOne.Visibility = Visibility.Visible;
            bTwo.Visibility = Visibility.Visible;
        }

        private void PickCharacter(Classes c)
        {
            Class = c;

            if (c == Classes.FIGHTER)
            {
                txtMain.Text = "Aha, you are a muscle-bound barbarian, bursting with strength, oozing with adventure, and clad in protective chainmail.\n\n";
                HP = 20;
                Strength = 20;
                Magic = 0;
                Agility = 5;
                Protection = 20;
                HPPotions = 2;
            }
            else if (c == Classes.MAGE)
            {
                txtMain.Text = "Aha, you are a bookish wizened individual, controlling the elements and forces around you with magical power.\n\n";
                HP = 18;
                Strength = 5;
                Magic = 20;
                Agility = 10;
                Protection = 5;
                HPPotions = 2;
                ManaPotions = 3;
            }
            if (c == Classes.THIEF)
            {
                txtMain.Text = "Aha, you are a shady character, sneaking in the dark, finding loot and stealthily approaching foes.\n\n";
                HP = 18;
                Strength = 10;
                Magic = 0;
                Agility = 20;
                Protection = 5;
                HPPotions = 5;
            }

            MaxHP = HP;
            MaxMagic = Magic;

            txtMain.Text = txtMain.Text + "The adventure commences!\n\n" +
                "Do you go left, right or straight ahead?";

            bForward.Content = "Forward";
            bLeft.Content = "Left";
            bRight.Content = "Right";

            bOne.Content = "Fight";
            bTwo.Content = "Flee";
            bThree.Content = "Magic";
            bFour.Content = "Mana pot'n";
            bFive.Content = "Eat food";
            bSix.Content = "HP potion";

            SetRoomState();
        }

        private void bOne_Click(object sender, RoutedEventArgs e)
        {
            if (State == States.STARTING)
            {
                txtMain.Text = "Great!\n\n" +
                    "First things first ...\n\n" +
                    "What type of character are you?";

                State = States.CHOOSECHARACTER;
                bOne.Content = "Warrior";
                bTwo.Content = "Mage";
                bThree.Content = "Thief";
                //                bFour.Content = "";
                bThree.Visibility = Visibility.Visible;
                //                bFour.Visibility = Visibility.Visible;
            }
            else if (State == States.CHOOSECHARACTER)
                PickCharacter(Classes.FIGHTER);
            else if (State == States.FIGHTING)
                PlayerHits();
            else if (State == States.DEATH)
                BeginTheAdventure();
        }

        private void bTwo_Click(object sender, RoutedEventArgs e)
        {
            if (State == States.STARTING)
            {
                int r = random.Next(1, 4);

                switch (r)
                {
                    case 1:
                        txtMain.Text = "Hmm, you are not ready to begin.\n\n" +
                            "I am not sure what to do in that case. How about you have a glass of milk and come back.\n\n" +
                            "...\n\n...\n\n...\n\n" +
                            "Are you ready to begin now?\n\n";
                        break;
                    case 2:
                        txtMain.Text = "Hmm, ok, but you clicked on me. I didn't click on you.\n\n" +
                            "Take a deep breath and relax.\n\n" +
                            "...\n\n...\n\n...\n\n" +
                            "Are you ready to begin now?\n\n";
                        break;
                    default:
                        txtMain.Text = "Eh?\n\n" +
                            "...\n\n...\n\n...\n\n" +
                            "Are you ready to begin now?\n\n";
                        break;
                }
            }
            else if (State == States.CHOOSECHARACTER)
                PickCharacter(Classes.MAGE);
            else if (State == States.FIGHTING)
                PlayerFlees();
        }

        private void bThree_Click(object sender, RoutedEventArgs e)
        {
            if (State == States.CHOOSECHARACTER)
                PickCharacter(Classes.THIEF);
            else if (State == States.FIGHTING)
                PlayerMagicHits();
        }

        private void bFour_Click(object sender, RoutedEventArgs e)
        {
            // Mana potion
            Magic = MaxMagic;
            ManaPotions--;
            DisplayStats();
        }

        private void bFive_Click(object sender, RoutedEventArgs e)
        {
            // Food
            HP += 10;
            if (HP > MaxHP)
                HP = MaxHP;
            Food--;
            DisplayStats();
        }

        private void bSix_Click(object sender, RoutedEventArgs e)
        {
            // HP potion
            HP = MaxHP;
            HPPotions--;
            DisplayStats();
        }

        private void bForward_Click(object sender, RoutedEventArgs e)
        {
            EnterNewRoom();
        }

        private void bLeft_Click(object sender, RoutedEventArgs e)
        {
            EnterNewRoom();
        }

        private void bRight_Click(object sender, RoutedEventArgs e)
        {
            EnterNewRoom();
        }

        private void EnterNewRoom()
        {
            // Room description
            int r = random.Next(1, 20);

            switch (r)
            {
                case 1:
                    txtMain.Text = "You are in a dank dungeon.\n\n";
                    break;
                case 2:
                    txtMain.Text = "You are in a small dark room. You can barely see your own hands.\n\n";
                    break;
                case 3:
                    txtMain.Text = "You are in a twisty corridor.\n\n";
                    break;
                case 4:
                    txtMain.Text = "You are in a servant's quarters. Perhaps a crusty old butler or maybe a jolly old chef?\n\n";
                    break;
                case 5:
                    txtMain.Text = "You are wading through a smelly sewer. Good thing you wore your boots!\n\n";
                    break;
                case 6:
                    txtMain.Text = "You are in a disused vault. A rat runs off with what looks to be the last vestige of some old paper money.\n\n";
                    break;
                case 7:
                    txtMain.Text = "You are in a garden. Footprints in the soil show many people have been this way ... and that they didn't keep off the grass!\n\n";
                    break;
                case 8:
                    txtMain.Text = "You are in a small corridor. Paintings of former leaders regal the walls.\n\n";
                    break;
                case 9:
                    txtMain.Text = "You are in a smelly dungeon. Dragon poop lines the wall.\n\n";
                    break;
                case 10:
                    txtMain.Text = "You are in a moist dungeon. Slime and moss festoon the wall.\n\n";
                    break;
                case 11:
                    txtMain.Text = "You are crossing a bridge over a moat. Sharp spikes periodically dot the walls, caked in dry blood.\n\n";
                    break;
                case 12:
                    txtMain.Text = "You are in a disused jail cell. Scratches in the wall tell you the prisoners counted away the days. And they had surprisingly sharp fingernails.\n\n";
                    break;
                case 13:
                    txtMain.Text = "You are in a large ballroom. Tapestries along the wall have faded over time.\n\n";
                    break;
                case 14:
                    txtMain.Text = "You are in the kitchens. Rats spill out of the pantry.\n\n";
                    break;
                case 15:
                    txtMain.Text = "You are in a small passageway.\n\n";
                    break;
                default:
                    txtMain.Text = "You are in a bland room which looks like it fits a generic adventure.\n\n";
                    break;
            }

            // Treasure?
            r = random.Next(1000);
            if (r > 650)
            {
                txtMain.Text += "Woot! You have found loot!\n";

                r = random.Next(1, 6);
                int z;
                switch (r)
                {
                    case 1:
                        z = random.Next(1, 1000);
                        txtMain.Text += String.Format("You scoop up {0} gold!\n\n", z);
                        Gold += z;
                        break;
                    case 2:
                        z = random.Next(1, 5);
                        txtMain.Text += String.Format("You find {0} pieces of food. Phew!\n\n", z);
                        Food += z;
                        break;
                    case 3:
                        z = random.Next(1, 3);
                        txtMain.Text += String.Format("You collect {0} health potions.\n\n", z);
                        HPPotions += z;
                        break;
                    case 4:
                        z = random.Next(1, 3);
                        txtMain.Text += String.Format("You collect {0} mana potions.\n\n", z);
                        ManaPotions += z;
                        if (MaxMagic == 0)
                            txtMain.Text += "(I guess that's a bit useless to you ... maybe you can sell it to a wizard later.)\n\n";
                        break;
                    default:
                        int y = random.Next(4);
                        z = random.Next(1, 10);

                        switch(y)
                        {
                            case 1:
                                txtMain.Text += "You have found a new set of mean-looking gloves.";
                                break;
                            case 2:
                                txtMain.Text += "You pick up a powerful new set of pants. Just your size!";
                                break;
                            case 3:
                                txtMain.Text +=
"               <>\n" +
"             .::::.\n" +
"         @\\\\/W\\/\\/W\\//@\n" +
"          \\\\/^\\/\\/^\\//\n" +
"           \\_O_<>_O_/\n" +
"      ____________________\n" +
"     |<><><>  |  |  <><><>|\n" +
"     |<>      |  |      <>|\n" +
"     |<>      |  |      <>|\n" +
"     |<>   .--------.   <>|\n" +
"     |     |   ()   |     |\n" +
"     |_____| (O\\/O) |_____|\n" +
"     |     \\   /\\   /     |\n" +
"     |------\\  \\/  /------|\n" +
"     |       '.__.'       |\n" +
"     |        |  |        |\n" +
"     :        |  |        :\n" +
"      \\<>     |  |     <>/\n" +
"       \\<>    |  |    <>/\n" +
"        \\<>   |  |   <>/\n" +
"         `\\<> |  | <>/'\n" +
"           `-.|  |.-`\n" +
"              '--'\n";

                                txtMain.Text += "You find a cool shield. That will come in handy.";
                                break;
                            default:
                                txtMain.Text += "You find a tough hat. Yeahhh!";
                                break;
                        }
                        
                        txtMain.Text += String.Format (" Your protection increases by {0}.\n\n", z);
                        Protection += z;
                        break;
                }

                DisplayStats();
            }

            // Monster?
            r = random.Next(1000);
            if (r < 500)
            {
                int z = random.Next(1, 10);
                switch (z)
                {
                    case 1:
                        MonsterName = "troll";
                        break;

                    case 2:
                        txtMain.Text +=
"                   (    )\n" +
"                  ((((()))\n" +
"                  |o\\ /o)|\n" +
"                  ( (  _')\n" +
"                   (._.  /\\__\n" +
"                  ,\\___,/ '  ')\n" +
"    '.,_,,       (  .- .   .    )\n" +
"     \\   \\\\     ( '        )(    )\n" +
"      \\   \\\\    \\.  _.__ ____( .  |\n" +
"       \\  /\\\\   .(   .'  /\\  '.  )\n" +
"        \\(  \\\\.-' ( /    \\/    \\)\n" +
"         '  ()) _'.-|/\\/\\/\\/\\/\\|\n" +
"             '\\\\ .( |\\/\\/\\/\\/\\/|\n" +
"               '((  \\    /\\    /\n" +
"               ((((  '.__\\/__.')\n" +
"                ((,) /   ((()   )\n" +
"                 \"..-,  (()(\"   /\n" +
"                  _//.   ((() .\"\n" +
"          _____ //,/\" ___ ((( ', ___\n" +
"                           ((  )\n" +
"                            / /\n" +
"                          _/,/'\n" +
"                        /,/,\"\n";

                        MonsterName = "mighty orc";
                        break;

                    case 3:
                        txtMain.Text +=
"             _._\n" +
"          .-'   `\n" +
"        __|__\n" +
"       /     \\\n" +
"       |()_()|\n" +
"       \\{o o}/\n" +
"        =\\o/=\n" +
"         ^ ^\n";
                        MonsterName = "giant rat";
                        break;

                    case 4:
                        MonsterName = "kobold";
                        break;

                    case 5:
                        MonsterName = "massive man-eating lizard";
                        break;

                    case 6:
                        txtMain.Text +=

"         mm\n" +
"      /^(  )^\\\n" +
"      \\,(..),/\n" +      
"        V~~V\n";
                        MonsterName = "zubat";
                        break;

                    case 7:
                        MonsterName = "teenage harpy";
                        break;

                    case 8:
                        txtMain.Text +=
"           ;               ,\n" +
"         ,;                 '.\n" +
"        ;:                   :;\n" +
"       ::                     ::\n" +
"       ::                     ::\n" +
"       ':                     :\n" +
"        :.                    :\n" +
"     ;' ::                   ::  '\n" +
"    .'  ';                   ;'  '.\n" +
"   ::    :;                 ;:    ::\n" +
"   ;      :;.             ,;:     ::\n" +
"   :;      :;:           ,;\"      ::\n" +
"   ::.      ':;  ..,.;  ;:'     ,.;:\n" +
"    \"'\"...   '::,::::: ;:   .;.;\"\"'\n" +
"        '\"\"\"....;:::::;,;.;\"\"\"\n" +
"    .:::.....'\"':::::::'\",...;::::;.\n" +
"   ;:' '\"\"'\"\";.,;:::::;.'\"\"\"\"\"\"  ':;\n" +
"  ::'         ;::;:::;::..         :;\n" +
" ::         ,;:::::::::::;:..       ::\n" +
" ;'     ,;;:;::::::::::::::;\";..    ':.\n" +
"::     ;:\"  ::::::\"\"\"'::::::  \":     ::\n" +
" :.    ::   ::::::;  :::::::   :     ;\n" +
"  ;    ::   :::::::  :::::::   :    ;\n" +
"   '   ::   ::::::....:::::'  ,:   '\n" +
"    '  ::    :::::::::::::\"   ::\n" +
"       ::     ':::::::::\"'    ::\n" +
"       ':       \"\"\"\"\"\"\"'      ::\n" +
"        ::                   ;:\n" +
"        ':;                 ;:\"\n" +
"          ';              ,;'\n" +
"            \"'           '\"\n" +
"              '\n";
                        MonsterName = "fierce spider";
                        break;

                    default:
                        MonsterName = "balrog";
                        break;
                }

                // Set monster stats
                MonsterHP = random.Next(Level, 20 + Level);
                MonsterStrength = random.Next(Level, 20 + Level);
                MonsterAgility = random.Next(Level, 20 + Level);

                txtMain.Text += String.Format ("You encounter a {0}!\n\nIt has\n\tHP:\t\t{1}\n\tStrength:\t{2}\n\tAgility:\t{3}\n\n",
                    MonsterName, MonsterHP, MonsterStrength, MonsterAgility);

                // Monster hits first
                if (MonsterAgility > Agility)
                    MonsterHits();

                SetFightingState();
            }
            else
            {
                txtMain.Text += "\n\nWhat now, champ? Left, right or straight ahead?";
                SetRoomState();
            }
        }

        private void SetFightingState()
        {
            State = States.FIGHTING;
            bForward.Visibility = Visibility.Collapsed;
            bLeft.Visibility = Visibility.Collapsed;
            bRight.Visibility = Visibility.Collapsed;
            bOne.Visibility = Visibility.Visible;
            bTwo.Visibility = Visibility.Visible;
            if (Magic == 0)
                bThree.Visibility = Visibility.Collapsed;
            else
                bThree.Visibility = Visibility.Visible;
            DisplayStats();
        }

        private void SetRoomState()
        {
            State = States.INROOM;
            bForward.Visibility = Visibility.Visible;
            bLeft.Visibility = Visibility.Visible;
            bRight.Visibility = Visibility.Visible;
            bOne.Visibility = Visibility.Collapsed;
            bTwo.Visibility = Visibility.Collapsed;
            bThree.Visibility = Visibility.Collapsed;
            DisplayStats();
        }

        private void MonsterHits()
        {
            txtMain.Text += String.Format ("The {0} attacks ...\n\n", MonsterName);
            // Work out monster damage
            if (random.Next(100) <= 2)
            {
                txtMain.Text += String.Format ("Ouch! The {0} performs a critical hit and you take {1} damage.\n\n", MonsterName, MonsterStrength);
                HP -= MonsterStrength;
            }
            else
            {
                if (Protection == 0)
                {
                    txtMain.Text += String.Format("Without any protective clothing, the {0} hits you for {1}.\n\n", MonsterName, MonsterStrength);
                    HP -= MonsterStrength;
                }
                else if (MonsterStrength > Protection)
                {
                    int Damage = MonsterStrength - Protection;
                    txtMain.Text += String.Format("Thanks to your protective attire the {0} only hits you for {1}.\n\n", MonsterName, Damage);
                    HP -= Damage;
                    Protection--;
                }
                else
                {
                    txtMain.Text += String.Format("Haha! Your protective outfit renders the attack useless!\n\n", MonsterName);
                    Protection--;
                }
            }

            if (HP <= 0)
                PlayerDies();

            if (Protection < 0)
                Protection = 0;
            DisplayStats();

            txtMain.Text += "What shall you do now, my leige?";
        }

        private void PlayerHits()
        {
            txtMain.Text = "You strike at the beast!\n\n";

            if (random.Next(100) <= 2)
            {
                txtMain.Text += String.Format("Bam! You perform a critical hit on the {0} and strike it for {1} damage.\n\n", MonsterName, Strength);
                MonsterHP -= Strength;
                MonsterAgility--;
            }
            else
            {
                if (MonsterAgility > Agility)
                {
                    txtMain.Text += String.Format("The monster is pretty nimble ... but I think you are wearing it out even though you missed.\n\n");
                    MonsterAgility--;
                }
                else
                {
                    int Damage = random.Next(Strength/2, Strength);
                    txtMain.Text += String.Format("The {0} tries to dodge ... but you hit it for {1}.\n\n", MonsterName, Damage);
                    if (Damage == 0)
                        txtMain.Text += "(Admittedly, that is not a very strong hit!)\n\n";
                    MonsterAgility--;
                    MonsterHP -= Damage;
                }
            }

            txtMain.Text += String.Format("The {0} now has HP: {1}, Strength: {2}, Agility: {3}.\n\n", MonsterName, MonsterHP, MonsterStrength, MonsterAgility);

            if (MonsterHP <= 0)
                MonsterDies();
            else
                MonsterHits();
        }

        private void PlayerMagicHits()
        {
            Magic--;

            txtMain.Text = "Kablam! You hit the monster with powerful magic.\n";
            MonsterHP -= Strength;

            if (MonsterHP <= 0)
                MonsterDies();
            else
                MonsterHits();
        }

        private void PlayerFlees()
        {
            if (Agility > MonsterAgility)
            {
                txtMain.Text = String.Format("You easily slip away from the {0}.\n\n", MonsterName);
                txtMain.Text += "In which direction would you like to sally forth?\n\n";
                SetRoomState();
            }
            else if ((random.Next(1, Agility)) >= (Agility / 2))
            {
                txtMain.Text = String.Format("It wasn't easy, but you manage to break away from the {0}.\n\n", MonsterName);
                txtMain.Text += "In which direction will you proceed?\n\n";
                SetRoomState();
            }
            else
            {
                txtMain.Text = String.Format("You try to flee ... but the {0} blocks you!!\n\n", MonsterName);
                MonsterHits();
            }
        }

        private void PlayerDies()
        {
            txtMain.Text += "Oh dear, you appear to have departed the realm of the living. I guess what goes around comes around, eh?\n\n";
            txtMain.Text += String.Format("You reached level {0} and found {1} gold.\n\n", Level, Gold);
            txtMain.Text += "Maybe the next adventurer will find it?\n\n";

            State = States.DEATH;
            bForward.Visibility = Visibility.Collapsed;
            bLeft.Visibility = Visibility.Collapsed;
            bRight.Visibility = Visibility.Collapsed;
            bOne.Content = "Again?";
            bOne.Visibility = Visibility.Visible;
            bTwo.Visibility = Visibility.Collapsed;
            bThree.Visibility = Visibility.Collapsed;
            bFour.Visibility = Visibility.Collapsed;
            bFive.Visibility = Visibility.Collapsed;
            bSix.Visibility = Visibility.Collapsed;

            Food = 0;
            ManaPotions = 0;
            HPPotions = 0;
        }

        private void MonsterDies()
        {
            txtMain.Text += String.Format ("You killed the {0}. One less monster in the world.\n\n", MonsterName);
            XP += 20;

            int z = random.Next(1, 1000);
            if (Class == Classes.THIEF)
                z += random.Next(1, 500);
            txtMain.Text += String.Format("You take its {0} gold. Nyuck nyuck nyuck.\n\n", z);
            Gold += z;

            // level up?
            if (XP >= NextLevelXP)
            {
                txtMain.Text += "Congratulations! You levelled up. You feel stronger, smarter, faster!\n\n";
                Level++;
                NextLevelXP = XP + (Level * 20);
                Strength++;
                Agility++;
                HP++;
                MaxHP++;
                Magic++;
                MaxMagic++;
            }

            txtMain.Text += "\n\nWhere shall you go now? Left, right or straight ahead?";
            SetRoomState();
        }
    }
}
