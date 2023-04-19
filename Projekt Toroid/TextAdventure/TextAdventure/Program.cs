using System;
using System.Runtime.InteropServices;

namespace TextAdventure
{
    class Program
    {
        static int playerX;
        static int playerY;
        static int playerPhysicalAtack = 1;
        static int playerMagicalAtack = 1;
        static int playerPhysicalDefense = 2;
        static int playerMagicalDefense = 2;
        static int playerHealth = 5;
        static int playerMaxHealth = 5;
        static int Mana = 5;
        static int MaxMana = 5;
        static int[] exp = { 0, 4, 1, 3 }; // [0] ilosc exp, [1] ilosc max exp, [2] aktualny lvl, [3] punkty umiejetnosci
        static int coins = 0;
        static int potion = 0, potionheal;
        static Weaponary emptyslot = new Weaponary("None\t\t", 0, 0, 0, 0, "none", 0, 0);
        static Weaponary[,] playerEQ = new Weaponary[4,2]; // [0] weapon, [1] armor, [2] helmet, [3] boots
        static Spells emptyspell = new Spells("None"," ", 0, 0, 0, 0, 0, " ");
        static Spells[] playerSpells = new Spells[4];
        static void WorldCreator(int[,] world,int size,int enemyT1, int enemyT2, int enemyT3, int treasure, int shop,int suprise)
        {           
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    world[x, y] = 0;
                }
            }
            Random roll = new();
            playerX = roll.Next(0, size);
            playerY = roll.Next(0, size);
            TilesCreator(world, enemyT1, 1, size);
            TilesCreator(world, enemyT2, 2, size);
            TilesCreator(world, enemyT3, 3, size);
            TilesCreator(world, treasure, 4, size);
            TilesCreator(world, shop, 5, size);
            TilesCreator(world, suprise, 6, size);
        }//Dodac tworzenie przeciwnikow, sklepów itp.
        static void TilesCreator(int[,] world,int number_of_tiles,int tileID,int size)
        {
            Random roll = new();
            if (number_of_tiles > Freespace(world,size))
            {
                number_of_tiles = Freespace(world, size);
            }
            for (int i = 0; i < number_of_tiles; i++)
            {
                int x = roll.Next(0, size);
                int y = roll.Next(0, size);
                if (world[x, y] == 0)
                {
                    world[x, y] = tileID;
                }
                else //gdy wylosuje ponownie zajęte miejsce
                {
                    bool loopset = true;
                    while (loopset)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            x++;
                            if (x < 0) { x += size; }
                            if (x >= size) { x -= size; }
                            if (world[x, y] == 0 && loopset)
                            {
                                world[x, y] = tileID;
                                loopset = false;
                            }
                            y++;
                            if (y < 0) { y += size; }
                            if (y >= size) { y -= size; }
                            if (world[x, y] == 0 && loopset)
                            {
                                world[x, y] = tileID;
                                loopset = false;
                            }
                            x--;
                            if (x < 0) { x += size; }
                            if (x >= size) { x -= size; }
                            if (world[x, y] == 0 && loopset)
                            {
                                world[x, y] = tileID;
                                loopset = false;
                            }
                        }
                        if (loopset) //gdy pętla będzie za długa
                        {
                            for (int jy = 0; jy < size; jy++)
                            {
                                for (int jx = 0; jx < size; jx++)
                                {
                                    if (loopset && world[jx,jy]==0) { world[jx, jy] = tileID; loopset = false; break; }
                                }
                            }
                        }
                    }
                }
            }
        }
        static int Freespace(int[,] world, int size)
        {
            int zero=0;
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (world[x,y]==0) { zero++; }
                }
            }
            return zero;
        }
        static void MapPrinter(int[,] world, int size)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("[P]");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        if (world[x, y] == 0)
                        {
                            Console.Write("[ ]");
                        }
                        if (world[x, y] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("[E]");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (world[x, y] == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("[E]");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (world[x, y] == 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("[E]");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (world[x, y] == 4)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[T]");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (world[x, y] == 5)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("[S]");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (world[x, y] == 6)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("[?]");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (world[x, y] == 10)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("[P]");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }                    
                }
                Console.WriteLine();
            }
        }
        static void HealthBarPrinter(int hp, int maxhp, int pshield, int mshield, string color)
        {
            int hold = maxhp - hp;
            if (color == "g")
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            if (color == "r")
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("HP: {0}/{1}", hp, maxhp);
            for (int i = 0; i < hp; i++)
            {
                Console.Write("|");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            if (color == "g") Console.ForegroundColor = ConsoleColor.Green;
            if (color == "r") Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < hold; i++)
            {
                Console.Write("|");
            }
            Console.WriteLine();
            hold = maxhp - pshield;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("PS: {0}/{1}", pshield, maxhp);
            for (int i = 0; i < pshield; i++)
            {
                Console.Write("|");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < hold; i++)
            {
                Console.Write("|");
            }
            Console.WriteLine();
            hold = maxhp - mshield;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("MS: {0}/{1}", mshield, maxhp);
            for (int i = 0; i < mshield; i++)
            {
                Console.Write("|");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < hold; i++)
            {
                Console.Write("|");
            }
            Console.WriteLine();
            if (color == "g")
            {
                hold = MaxMana - Mana;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Mana: {0}/{1}", Mana, MaxMana);
                for (int i = 0; i < Mana; i++)
                {
                    Console.Write("|");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Blue;
                for (int i = 0; i < hold; i++)
                {
                    Console.Write("|");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
        static void ExpBarPrinter()
        {
            int lvlup = 0;
            while (exp[0] >= exp[1])
            {
                lvlup++;
                exp[0] -= exp[1];
                exp[1]++;
                exp[2]++;
                exp[3]++;
                playerHealth++;
                playerMaxHealth++;
            }
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            int hold = exp[1] - exp[0];
            Console.Write("LvL{0}: ", exp[2]); Console.Write("{0}/{1}", exp[0], exp[1]);
            for (int i = 0; i < exp[0]; i++)
            {
                Console.Write("|");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < hold; i++)
            {
                Console.Write("|");
            }
            if (lvlup > 0)
            {
                Console.WriteLine("+{0}", lvlup);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White; 
                Console.WriteLine();
            }
        }
        static void StatsPrinter()
        {
            int hold = playerMaxHealth - playerHealth;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("HP: {0}/{1}", playerHealth, playerMaxHealth);
            for (int i = 0; i < playerHealth; i++)
            {
                Console.Write("|");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < hold; i++)
            {
                Console.Write("|");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Coins - {0}", coins);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Move(int[,] world, int size)
        {
            while (true)
            {
                string direction = Console.ReadLine();
                direction = direction.ToLower();
                if (direction == "a")
                {
                    playerX--; if (playerX < 0) playerX += size; break;
                }
                else if (direction == "d")
                {
                    playerX++; if (playerX >= size) playerX -= size; break;
                }
                else if (direction == "w")
                {
                    playerY--; if (playerY < 0) playerY += size; break;
                }
                else if (direction == "s")
                {
                    playerY++; if (playerY >= size) playerY -= size; break;
                }
                else if (direction == "e")
                {
                    EQ(); MapPrinter(world, size);
                }
                else if (direction == "p")
                {
                    SpendPoint(); MapPrinter(world, size);
                }
                else if (direction == "l")
                {
                    SpellsEQ(); MapPrinter(world, size);
                }
                else if (direction == "help")
                {                
                    Console.WriteLine("W,A,S,D - move");
                    Console.WriteLine("E - Equipment");
                    Console.WriteLine("P - Spend points");
                    Console.WriteLine("L - Spells");
                }
                else { Console.WriteLine("Try again"); }
            }
        }
        static void Game(int[,] world, int size)
        {
            MapPrinter(world, size);
            Random roll = new();
            Enemy Slime = new ("Slime", 10, 0, 2, 3, 0, 1);
            Enemy Wolf = new ("Wolf", 12, 3, 0, 2, 0, 1);
            Enemy Rat = new("Rat", 8, 5, 0, 0, 2, 1);
            Enemy[] enemy_tier1 = { Slime, Wolf, Rat };
            Enemy Rabit = new("Rabid Rabit", 20, 10, 0, 5, 5, 2);
            Enemy BKnight = new("The Black Knight of Three Stripes", 30, 7, 0, 10, 4, 2);
            Enemy[] enemy_tier2 = { Rabit, BKnight };
            Weaponary Sword = new Weaponary("Sword\t\t", 2, 0, 0, 0, "weapon", 2, 1); //typ weapon/armor/helmet/boots
            Weaponary Mace = new Weaponary("Mace\t\t\t", 5, 0, 0, 0, "weapon", 3, 2);
            Weaponary WarAxe = new Weaponary("War Axe\t\t", 8, 0, 0, 0, "weapon", 5, 3);
            Weaponary WoodenRod = new Weaponary("Wooden rod\t", 0, 3, 0, 0, "weapon", 2, 4);
            Weaponary TheHolyPan = new Weaponary("The Holy Pan\t", 8, 0, 4, 0, "weapon", 10, 3);
            Weaponary BadWifiRod = new Weaponary("The Rod of Bad Wifi", 0, 10, 0, 0, "weapon", 8, 4);
            Weaponary[] weaponary = { Sword, Mace, WarAxe, WoodenRod, TheHolyPan, BadWifiRod };
            while (true)
            {
                potionheal = exp[2] + 2;
                Move(world, size);
                Console.Clear();
                MapPrinter(world, size);
                if (world[playerX,playerY]==1)
                {
                    Fight(enemy_tier1[roll.Next(0, enemy_tier1.Length)]);
                    world[playerX, playerY] = 0;
                    Console.Clear();
                    MapPrinter(world, size);
                }
                else if (world[playerX,playerY]==2)
                {
                    Fight(enemy_tier2[roll.Next(0, enemy_tier2.Length)]);
                    world[playerX, playerY] = 0;
                    Console.Clear();
                    MapPrinter(world, size);
                }
                else if (world[playerX,playerY]==4)
                {
                    Treasure(enemy_tier2[roll.Next(enemy_tier2.Length)], weaponary[roll.Next(weaponary.Length)]);
                    world[playerX, playerY] = 0;
                    Console.Clear();
                    MapPrinter(world, size);
                }
                else if (world[playerX,playerY]==5)
                {
                    int shopselect = roll.Next(0, 3);
                    if (shopselect == 0)
                    {
                        Shop(weaponary);
                    }
                    if (shopselect == 1)
                    {
                        Shop(weaponary);
                    }
                    if (shopselect == 2)
                    {
                        Shop(weaponary);
                    }
                    world[playerX, playerY] = 0;
                    Console.Clear();
                    MapPrinter(world, size);
                }
            }
        }
        static void Fight(Enemy enemy)
        {
            Random roll = new();
            bool cast = true;
            int enemyHealth = enemy.health, enemyMaxHealth = enemy.health;
            int enemyAction;
            int playerPDshield = 0, playerMDshield = 0, enemyPDshield = 0, enemyMDshield = 0;
            int ppDMG = 0, pmDMG = 0, epDMG = 0, emDMG = 0;
            int pPAbuff = playerEQ[0, 0].bonusPA + playerEQ[1, 0].bonusPA + playerEQ[2, 0].bonusPA + playerEQ[3, 0].bonusPA;
            int pMAbuff = playerEQ[0, 0].bonusMA + playerEQ[1, 0].bonusMA + playerEQ[2, 0].bonusMA + playerEQ[3, 0].bonusMA;
            int pPSbuff = playerEQ[0, 0].bonusPD + playerEQ[1, 0].bonusPD + playerEQ[2, 0].bonusPD + playerEQ[3, 0].bonusPD;
            int pMSbuff = playerEQ[0, 0].bonusMD + playerEQ[1, 0].bonusMD + playerEQ[2, 0].bonusMD + playerEQ[3, 0].bonusMD;
            int ePAbuff = 0, eMAbuff = 0, ePSbuff = 0, eMSbuff = 0;
            while(true)
            {
                HealthBarPrinter(playerHealth, playerMaxHealth, playerPDshield, playerMDshield, "g");
                HealthBarPrinter(enemyHealth, enemyMaxHealth, enemyPDshield, enemyMDshield, "r");
                enemyAction = roll.Next(1, 7);
                if (enemy.tier == 1)
                {                    
                    if (enemyAction <= 3)
                    {
                        Console.WriteLine("{0} will atack", enemy.name);
                    }
                    else if (enemyAction <= 5)
                    {
                        Console.WriteLine("{0} guards up", enemy.name);
                    }
                    else
                    {
                        Console.WriteLine("{0} will ???", enemy.name);
                    }
                }
                if (enemy.tier == 2)
                {
                    if(enemyAction <= 3)
                    {
                        Console.WriteLine("{0} will atack", enemy.name);
                    }
                    else if (enemyAction <= 5)
                    {
                        Console.WriteLine("{0} guards up", enemy.name);
                    }
                    else
                    {
                        Console.WriteLine("{0} will ???", enemy.name);
                    }
                }
                //wprowadzić tier2 i tier3
                while (true)
                {
                    cast = false;
                    Console.WriteLine("A - attack");
                    Console.WriteLine("PS - physical shield");
                    Console.WriteLine("MS - magical shield");
                    Console.WriteLine("P - potion ({0})", potion);
                    SpellWrite(playerSpells[0]);
                    SpellWrite(playerSpells[1]);
                    SpellWrite(playerSpells[2]);
                    SpellWrite(playerSpells[3]);
                    string choice = Console.ReadLine();
                    choice = choice.ToLower();
                    if (choice == "a")
                    {
                        ppDMG = playerPhysicalAtack + pPAbuff;
                        if (ppDMG<1) ppDMG = 1;
                        break;
                    }
                    else if (choice == "ps")
                    {
                        playerPDshield += playerPhysicalDefense + pPSbuff;
                        if (playerPDshield < 0) playerPDshield = 0;
                        else if (playerPDshield > playerMaxHealth) playerPDshield = playerMaxHealth;
                        break;
                    }
                    else if (choice == "ms")
                    {
                        playerMDshield += playerMagicalDefense + pMSbuff;
                        if (playerMDshield < 0) playerMDshield = 0;
                        else if (playerMDshield > playerMaxHealth) playerMDshield = playerMaxHealth;
                        break;
                    }
                    else if (choice == "p")
                    {
                        if (potion > 0)
                        {
                            playerHealth += potionheal;
                            potion--;
                            if (playerHealth > playerMaxHealth) { playerHealth = playerMaxHealth; }
                            break;
                        }
                        else Console.WriteLine("You don't have any potions");
                    }
                    for (int x = 0; x < playerSpells.Length; x++)
                    {
                        if (choice == playerSpells[x].shortName & choice != emptyspell.shortName)
                        {
                            if (playerSpells[x].manaCost <= Mana)
                            {
                                if (playerSpells[x].efectID == 1)
                                {
                                    ppDMG = playerPhysicalAtack + pPAbuff;
                                    ppDMG = ppDMG * playerSpells[x].powerMultiplayer;
                                    if (ppDMG < 1) ppDMG = 1;
                                }
                                else if (playerSpells[x].efectID == 2)
                                {
                                    pmDMG = playerMagicalAtack + pMAbuff;
                                    pmDMG = pmDMG * playerSpells[x].powerMultiplayer;
                                    if (pmDMG < 1) pmDMG = 1;
                                }
                                else if (playerSpells[x].efectID == 3)
                                {
                                    playerPDshield += (playerPhysicalDefense + pPSbuff) * playerSpells[x].powerMultiplayer;
                                    if (playerPDshield < 0) playerPDshield = 0;
                                    else if (playerPDshield > playerMaxHealth) playerPDshield = playerMaxHealth;
                                    playerMDshield += (playerMagicalDefense + pMSbuff) * playerSpells[x].powerMultiplayer;
                                    if (playerMDshield < 0) playerMDshield = 0;
                                    else if (playerMDshield > playerMaxHealth) playerMDshield = playerMaxHealth;
                                }
                                else if (playerSpells[x].efectID == 4 || playerSpells[x].efectID == 5)
                                {
                                    SpellCast(playerSpells[x], ref pPAbuff, ref pMAbuff,ref pPSbuff, ref pMSbuff, ref ePAbuff, ref eMAbuff, ref ePSbuff, ref eMSbuff);
                                }
                                Mana -= playerSpells[x].manaCost;
                                cast = true;
                            }
                            else { Console.WriteLine("Not enough mana"); cast = false; }
                            break;
                        }                    
                    }
                    if (cast == true) break;
                }
                if (enemy.tier == 1)
                {
                    if (enemyAction <= 3)
                    {
                        if (enemy.pAtack > enemy.mAtack)
                        {
                            epDMG = enemy.pAtack + ePAbuff;
                            if (epDMG < 1) epDMG = 1; 
                        }
                        else 
                        {
                            emDMG = enemy.mAtack + eMAbuff;
                            if (emDMG < 1) emDMG = 1;
                        }
                    }
                    else if (enemyAction <= 5)
                    {
                        if (enemy.pDefense > enemy.mDefense)
                        {
                            enemyPDshield += enemy.pDefense + ePSbuff;
                            if (enemyPDshield < 0) enemyPDshield = 0;
                            else if (enemyPDshield > enemyMaxHealth) enemyPDshield = enemyMaxHealth;
                        }
                        else
                        {
                            enemyMDshield += enemy.mDefense + eMSbuff;
                            if (enemyMDshield < 0) enemyMDshield = 0;
                            else if (enemyMDshield > enemyMaxHealth) enemyMDshield = enemyMaxHealth;
                        }
                    }
                } //tier 1
                else if (enemy.tier == 2)
                {
                    if (enemyAction <= 3)
                    {
                        if (enemy.pAtack > enemy.mAtack)
                        {
                            epDMG = enemy.pAtack + ePAbuff;
                            if (epDMG < 1) epDMG = 1;
                        }
                        else
                        {
                            emDMG = enemy.mAtack + eMAbuff;
                            if (emDMG < 1) emDMG = 1;
                        }
                    }
                    else if (enemyAction <= 5)
                    {
                        if (enemyPDshield > enemyMDshield)
                        {
                            enemyMDshield += enemy.mDefense + eMSbuff;
                            if (enemyMDshield < 0) enemyMDshield = 0;
                            else if (enemyMDshield > enemyMaxHealth) enemyMDshield = enemyMaxHealth;
                        }
                        else if (enemyMDshield > enemyPDshield)
                        {
                            enemyPDshield += enemy.pDefense + ePSbuff;
                            if (enemyPDshield < 0) enemyPDshield = 0;
                            else if (enemyPDshield > enemyMaxHealth) enemyPDshield = enemyMaxHealth;
                        }
                        else if (playerMagicalAtack > playerPhysicalAtack)
                        {
                            enemyMDshield += enemy.mDefense + eMSbuff;
                            if (enemyMDshield < 0) enemyMDshield = 0;
                            else if (enemyMDshield > enemyMaxHealth) enemyMDshield = enemyMaxHealth;
                        }
                        else
                        {
                            enemyPDshield += enemy.pDefense + ePSbuff;
                            if (enemyPDshield < 0) enemyPDshield = 0;
                            else if (enemyPDshield > enemyMaxHealth) enemyPDshield = enemyMaxHealth;
                        }
                    }
                } //tier 2
                else if (enemy.tier == 3)
                {
                    if (enemyAction <= 3)
                    {
                        if (playerPhysicalDefense < playerMagicalDefense)
                        {
                            epDMG = enemy.pAtack + ePAbuff;
                            if (epDMG < 1) epDMG = 1;
                        }
                        else
                        {
                            emDMG = enemy.mAtack + eMAbuff;
                            if (emDMG < 1) emDMG = 1;
                        }
                    }
                    else if (enemyAction <= 5)
                    {
                        if (enemyPDshield > enemyMDshield)
                        {
                            enemyMDshield += enemy.mDefense + eMSbuff;
                            if (enemyMDshield < 0) enemyMDshield = 0;
                            else if (enemyMDshield > enemyMaxHealth) enemyMDshield = enemyMaxHealth;
                        }
                        else if (enemyMDshield > enemyPDshield)
                        {
                            enemyPDshield += enemy.pDefense + ePSbuff;
                            if (enemyPDshield < 0) enemyPDshield = 0;
                            else if (enemyPDshield > enemyMaxHealth) enemyPDshield = enemyMaxHealth;
                        }
                        else if (playerMagicalAtack > playerPhysicalAtack)
                        {
                            enemyMDshield += enemy.mDefense + eMSbuff;
                            if (enemyMDshield < 0) enemyMDshield = 0;
                            else if (enemyMDshield > enemyMaxHealth) enemyMDshield = enemyMaxHealth;
                        }
                        else
                        {
                            enemyPDshield += enemy.pDefense + ePSbuff;
                            if (enemyPDshield < 0) enemyPDshield = 0;
                            else if (enemyPDshield > enemyMaxHealth) enemyPDshield = enemyMaxHealth;
                        }
                    }
                }//tier 3
                if (ppDMG > 0) Console.WriteLine("You deal {0} physical dmg", ppDMG);
                if (pmDMG > 0) Console.WriteLine("You deal {0} magical dmg", pmDMG);
                if (epDMG > 0) Console.WriteLine("{0} deal {1} physical dmg", enemy.name, epDMG);
                if (emDMG > 0) Console.WriteLine("{0} deal {1} magical dmg", enemy.name, emDMG);
                Dmg(ref ppDMG, ref enemyPDshield, ref enemyHealth);                
                Dmg(ref pmDMG, ref enemyMDshield, ref enemyHealth);                
                Dmg(ref epDMG, ref playerPDshield, ref playerHealth);
                Dmg(ref emDMG, ref playerMDshield, ref playerHealth);
                if (Mana < MaxMana) Mana += 1; //Dodać bonusowy przyrost many
                if (enemyHealth <= 0)
                {
                    exp[0] += enemy.tier * 2;
                    coins += enemy.tier * 3;
                    Console.WriteLine("You have deafeted {0}, earned {1} exp and {2} coins.", enemy.name, enemy.tier * 2, enemy.tier * 3);
                    ExpBarPrinter();
                    break;
                }
                else if (playerHealth <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("YOU DIED");
                    Console.ForegroundColor = ConsoleColor.White;
                    Environment.Exit(0);
                }
            }                        
            Console.ReadKey();
        } //Dokończyć system walki!!
        static void Dmg(ref int atkDMG,ref int defShield,ref int defHP)
        {
            if (atkDMG <= defShield)
            {
                defShield -= atkDMG;
            }
            else if (atkDMG > defShield)
            {
                atkDMG -= defShield;
                defHP -= atkDMG;
                defShield = 0;
            }
            atkDMG = 0;
        }
        static void Shop(Weaponary[] weaponary)
        {
            Random roll = new();
            Weaponary[] shopoptions = new Weaponary[3];
            int position, choice = 0;
            int potionprice = 2;
            for (int x = 0; x < shopoptions.Length; x++)
            {
                shopoptions[x] = weaponary[roll.Next(weaponary.Length)]; 
            }
            while (true)
            {
                StatsPrinter();
                position = 1;
                Console.Write("{0} {1} - ", position, shopoptions[0].eqname);
                if (shopoptions[0].price > coins) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(shopoptions[0].price); Console.ForegroundColor = ConsoleColor.White; }
                else { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(shopoptions[0].price); Console.ForegroundColor = ConsoleColor.White; }

                position = 2;
                Console.Write("{0} {1} - ", position, shopoptions[1].eqname);
                if (shopoptions[1].price > coins) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(shopoptions[1].price); Console.ForegroundColor = ConsoleColor.White; }
                else { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(shopoptions[1].price); Console.ForegroundColor = ConsoleColor.White; }

                position = 3;
                Console.Write("{0} {1} - ", position, shopoptions[2].eqname);
                if (shopoptions[2].price > coins) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(shopoptions[2].price); Console.ForegroundColor = ConsoleColor.White; }
                else { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(shopoptions[2].price); Console.ForegroundColor = ConsoleColor.White; }

                Console.Write("4 HP potion (Instant heal {0}) - ", potionheal);
                if (potionprice > coins) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(potionprice); Console.ForegroundColor = ConsoleColor.White; }
                else { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(potionprice); Console.ForegroundColor = ConsoleColor.White; }
                Console.WriteLine("5 Equipment");
                Console.WriteLine("6 Exit");
                bool choicecheck = false;
                while (choicecheck == false)
                {
                    choicecheck = int.TryParse(Console.ReadLine(), out choice);
                    if (choice < 1 || choice > 6 || choicecheck == false) { Console.WriteLine("Enter number from 1 to 6"); choicecheck = false; }
                }
                if (choice == 1)
                {
                    if (shopoptions[0].price > coins) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Not enaugh coins"); Console.ForegroundColor = ConsoleColor.White; }
                    else
                    {
                        if (shopoptions[0].type == "weapon")
                        {
                            if (playerEQ[0, 0] == emptyslot)
                            {
                                playerEQ[0, 0] = shopoptions[0];
                            }
                            else
                            {
                                playerEQ[0, 1] = shopoptions[0];
                            }
                            coins -= shopoptions[0].price;
                        }
                        if (shopoptions[0].type == "armor")
                        {
                            if (playerEQ[1, 0] == emptyslot)
                            {
                                playerEQ[1, 0] = shopoptions[0];
                            }
                            else
                            {
                                playerEQ[1, 1] = shopoptions[0];
                            }
                            coins -= shopoptions[0].price;
                        }
                        if (shopoptions[0].type == "helmet")
                        {
                            if (playerEQ[2, 0] == emptyslot)
                            {
                                playerEQ[2, 0] = shopoptions[0];
                            }
                            else
                            {
                                playerEQ[2, 1] = shopoptions[0];
                            }
                            coins -= shopoptions[0].price;
                        }
                        if (shopoptions[0].type == "boots")
                        {
                            if (playerEQ[3, 0] == emptyslot)
                            {
                                playerEQ[3, 0] = shopoptions[0];
                            }
                            else
                            {
                                playerEQ[3, 1] = shopoptions[0];
                            }
                            coins -= shopoptions[0].price;
                        }
                    }
                }
                if (choice == 2)
                {

                    if (shopoptions[1].price > coins) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Not enaugh coins"); Console.ForegroundColor = ConsoleColor.White; }
                    else
                    {
                        if (shopoptions[1].type == "weapon")
                        {
                            if (playerEQ[0, 0] == emptyslot)
                            {
                                playerEQ[0, 0] = shopoptions[1];
                            }
                            else
                            {
                                playerEQ[0, 1] = shopoptions[1];
                            }
                            coins -= shopoptions[1].price;
                        }
                        if (shopoptions[1].type == "armor")
                        {
                            if (playerEQ[1, 0] == emptyslot)
                            {
                                playerEQ[1, 0] = shopoptions[1];
                            }
                            else
                            {
                                playerEQ[1, 1] = shopoptions[1];
                            }
                            coins -= shopoptions[1].price;
                        }
                        if (shopoptions[1].type == "helmet")
                        {
                            if (playerEQ[2, 0] == emptyslot)
                            {
                                playerEQ[2, 0] = shopoptions[1];
                            }
                            else
                            {
                                playerEQ[2, 1] = shopoptions[1];
                            }
                            coins -= shopoptions[1].price;
                        }
                        if (shopoptions[1].type == "boots")
                        {
                            if (playerEQ[3, 0] == emptyslot)
                            {
                                playerEQ[3, 0] = shopoptions[1];
                            }
                            else
                            {
                                playerEQ[3, 1] = shopoptions[1];
                            }
                            coins -= shopoptions[1].price;
                        }
                    }
                }
                if (choice == 3)
                {
                    if (shopoptions[2].price > coins) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Not enaugh coins"); Console.ForegroundColor = ConsoleColor.White; }
                    else
                    {
                        if (shopoptions[2].type == "weapon")
                        {
                            if (playerEQ[0,0] == emptyslot)
                            {
                                playerEQ[0, 0] = shopoptions[2];
                            }
                            else
                            {
                                playerEQ[0,1] = shopoptions[2];
                            }
                            coins -= shopoptions[2].price;
                        }
                        if (shopoptions[2].type == "armor")
                        {
                            if (playerEQ[1, 0] == emptyslot)
                            {
                                playerEQ[1, 0] = shopoptions[2];
                            }
                            else
                            {
                                playerEQ[1, 1] = shopoptions[2];
                            }
                            coins -= shopoptions[2].price;
                        }
                        if (shopoptions[2].type == "helmet")
                        {
                            if (playerEQ[2, 0] == emptyslot)
                            {
                                playerEQ[2, 0] = shopoptions[2];
                            }
                            else
                            {
                                playerEQ[2, 1] = shopoptions[2];
                            }
                            coins -= shopoptions[2].price;
                        }
                        if (shopoptions[2].type == "boots")
                        {
                            if (playerEQ[3, 0] == emptyslot)
                            {
                                playerEQ[3, 0] = shopoptions[2];
                            }
                            else
                            {
                                playerEQ[3, 1] = shopoptions[2];
                            }
                            coins -= shopoptions[2].price;
                        }
                    }
                }
                if (choice == 4)
                {
                    if (potionprice > coins) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Not enaugh coins"); Console.ForegroundColor = ConsoleColor.White; }
                    else { potion++; coins -= potionprice; }                    
                }
                if (choice == 5)
                {
                    EQ();
                }
                if (choice == 6)
                {
                    break;
                }               
            }
        }
        static void Treasure(Enemy enemy, Weaponary treasure)
        {
            Random roll = new ();
            string choice;
            int chest = roll.Next(20);
            if (chest < 5)
            {
                coins += 5;
                Console.WriteLine("You found 5 coins");
            }
            else if (chest < 10)
            {
                coins += 8;
                Console.WriteLine("You found 8 coins");
            }
            else if (chest < 15)
            {
                coins += 12;
                Console.WriteLine("You found 12 coins");
            }
            else if (chest < 18)
            {
                coins += 18;
                Console.WriteLine("You found 18 coins");
            }
            else if (chest < 19)
            {
                Fight(enemy);
                while (true)
                {
                    Console.WriteLine("You found 15 coins and {0}", treasure.eqname);
                    Console.WriteLine("Do you want to pick this?");
                    Console.WriteLine("Y - Yes");
                    Console.WriteLine("N - No (Earn more coins - {0})", treasure.price);
                    Console.WriteLine("E - Change equipment");
                    choice = Console.ReadLine();
                    choice = choice.ToLower();
                    if (choice == "y")
                    {
                        if (treasure.type == "weapon")
                        {
                            if (playerEQ[0, 0] == emptyslot)
                            {
                                playerEQ[0, 0] = treasure;
                            }
                            else
                            {
                                playerEQ[0, 1] = treasure;
                            }
                        }
                        if (treasure.type == "armor")
                        {
                            if (playerEQ[1, 0] == emptyslot)
                            {
                                playerEQ[1, 0] = treasure;
                            }
                            else
                            {
                                playerEQ[1, 1] = treasure;
                            }
                        }
                        if (treasure.type == "helmet")
                        {
                            if (playerEQ[2, 0] == emptyslot)
                            {
                                playerEQ[2, 0] = treasure;
                            }
                            else
                            {
                                playerEQ[2, 1] = treasure;
                            }
                        }
                        if (treasure.type == "boots")
                        {
                            if (playerEQ[3, 0] == emptyslot)
                            {
                                playerEQ[3, 0] = treasure;
                            }
                            else
                            {
                                playerEQ[3, 1] = treasure;
                            }
                        }
                        break;
                    }
                    if (choice == "n")
                    {
                        coins += treasure.price;
                        break;
                    }
                    if (choice == "e")
                    {
                        EQ();
                    }
                }
            }
            else
            {
                Fight(enemy);
                coins += 25;
                Console.WriteLine("You pick up 25 coins");
            }
            Console.ReadKey();
        }//dodać mimika
        static void SpendPoint()
        {
            while (exp[3]>0)
            {
                Console.WriteLine("{0} skill points to spend", exp[3]);
                Console.WriteLine("PA - PhysicalAtack {0}", playerPhysicalAtack);
                Console.WriteLine("MA - MagicalAtack {0}", playerMagicalAtack);
                Console.WriteLine("PD - physicalDefense {0}", playerPhysicalDefense);
                Console.WriteLine("MD - MagicalDefense {0}", playerMagicalDefense);
                Console.WriteLine("Done - leave point spending");
                string choice = Console.ReadLine();
                choice = choice.ToLower();
                if (choice == "pa")
                {
                    playerPhysicalAtack++;
                    exp[3]--;
                }
                else if (choice == "ma")
                {
                    playerMagicalAtack++;
                    exp[3]--;
                }
                else if (choice == "pd")
                {
                    playerPhysicalDefense++;
                    exp[3]--;
                }
                else if (choice == "md")
                {
                    playerMagicalDefense++;
                    exp[3]--;
                }
                else if (choice == "done")
                { break; }
                else Console.WriteLine("Try again");
            }
            if (exp[3] == 0) { Console.WriteLine("No enough skill point"); Console.ReadKey(); }
            Console.Clear();
        }
        static void EQ()
        {
            Weaponary hold; int choice = 0;
            while (true)
            {
                StatsPrinter();
                Console.WriteLine("Equipment \t \tBag");
                Console.WriteLine("1)");
                Console.WriteLine("{0} \t{1}", playerEQ[0, 0].eqname, playerEQ[0, 1].eqname);
                Console.Write("PA-{0} MA-{1} PD-{2} MD-{3}\t", playerEQ[0, 0].bonusPA, playerEQ[0, 0].bonusMA, playerEQ[0, 0].bonusPD, playerEQ[0, 0].bonusMD);
                Console.WriteLine("PA-{0} MA-{1} PD-{2} MD-{3}", playerEQ[0, 1].bonusPA, playerEQ[0, 1].bonusMA, playerEQ[0, 1].bonusPD, playerEQ[0, 1].bonusMD);
                Console.WriteLine("2)");
                Console.WriteLine("{0} \t{1}", playerEQ[1, 0].eqname, playerEQ[1, 1].eqname);
                Console.Write("PA-{0} MA-{1} PD-{2} MD-{3}\t", playerEQ[1, 0].bonusPA, playerEQ[1, 0].bonusMA, playerEQ[1, 0].bonusPD, playerEQ[1, 0].bonusMD);
                Console.WriteLine("PA-{0} MA-{1} PD-{2} MD-{3}", playerEQ[1, 1].bonusPA, playerEQ[1, 1].bonusMA, playerEQ[1, 1].bonusPD, playerEQ[1, 1].bonusMD);
                Console.WriteLine("3)");
                Console.WriteLine("{0} \t{1}", playerEQ[2, 0].eqname, playerEQ[2, 1].eqname);
                Console.Write("PA-{0} MA-{1} PD-{2} MD-{3}\t", playerEQ[2, 0].bonusPA, playerEQ[2, 0].bonusMA, playerEQ[2, 0].bonusPD, playerEQ[2, 0].bonusMD);
                Console.WriteLine("PA-{0} MA-{1} PD-{2} MD-{3}", playerEQ[2, 1].bonusPA, playerEQ[2, 1].bonusMA, playerEQ[2, 1].bonusPD, playerEQ[2, 1].bonusMD);
                Console.WriteLine("4)");
                Console.WriteLine("{0} \t{1}", playerEQ[3, 0].eqname, playerEQ[3, 1].eqname);
                Console.Write("PA-{0} MA-{1} PD-{2} MD-{3}\t", playerEQ[3, 0].bonusPA, playerEQ[3, 0].bonusMA, playerEQ[3, 0].bonusPD, playerEQ[3, 0].bonusMD);
                Console.WriteLine("PA-{0} MA-{1} PD-{2} MD-{3}", playerEQ[3, 1].bonusPA, playerEQ[3, 1].bonusMA, playerEQ[3, 1].bonusPD, playerEQ[3, 1].bonusMD);
                Console.WriteLine("5) Potion({0}) - heal {1} hp", potion, potionheal);
                Console.WriteLine("6) Exit");
                bool choicecheck = false;
                while (choicecheck == false)
                {
                    choicecheck = int.TryParse(Console.ReadLine(), out choice);
                    if (choice < 1 || choice > 6 || choicecheck == false) { Console.WriteLine("Enter number from 1 to 6"); choicecheck = false; }
                }
                if (choice == 1)
                {
                    hold = playerEQ[0, 0];
                    playerEQ[0, 0] = playerEQ[0, 1];
                    playerEQ[0, 1] = hold;
                }
                else if (choice == 2)
                {
                    hold = playerEQ[1, 0];
                    playerEQ[1, 0] = playerEQ[1, 1];
                    playerEQ[1, 1] = hold;
                }
                else if (choice == 3)
                {
                    hold = playerEQ[2, 0];
                    playerEQ[2, 0] = playerEQ[2, 1];
                    playerEQ[2, 1] = hold;
                }
                else if (choice == 4)
                {
                    hold = playerEQ[3, 0];
                    playerEQ[3, 0] = playerEQ[3, 1];
                    playerEQ[3, 1] = hold;
                }
                else if (choice == 5)
                {
                    if (potion > 0)
                    {
                        playerHealth += potionheal;
                        potion--;
                        if (playerHealth > playerMaxHealth) { playerHealth = playerMaxHealth; }
                    }
                    else Console.WriteLine("You don't have any potions");
                }
                else if (choice == 6)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < playerSpells.Length; j++)
                        {
                            if (playerSpells[j].reqEQ == 0) ;
                            else if (playerSpells[j].reqEQ != playerEQ[i, 0].id)
                            {
                                playerSpells[j] = emptyspell;
                            }
                        }
                    }
                    break;
                }
            }
            Console.Clear();
        }
        static void SpellsEQ()
        {
            //(spellName, shortname, efectID, powerMultiplayer, manaCost, reqLvl, reqEQ, description)
            Spells Fireball = new Spells("Fire strike","fb", 2, 1, 1, 1, 0, "Shoot fire ebers into enemy");
            Spells OmniShield = new("Omni shield", "os", 3, 1, 3, 2, 0, "Gain both shields");
            Spells WiFiCurse = new("Bad WiFi Curse", "wfc", 5, 4, 3, 1, 4, "Lower enemy atack stats");
            while (true)
            {
                SpellWrite(Fireball);
                SpellWrite(OmniShield);
                SpellWrite(WiFiCurse);
                Console.WriteLine("reset - Reset spell");
                Console.WriteLine("exit");
                string choice = Console.ReadLine();
                choice = choice.ToLower();
                SpellCheck(Fireball, ref choice);
                SpellCheck(OmniShield, ref choice);
                SpellCheck(WiFiCurse, ref choice);
                if (choice == "reset")
                {
                    playerSpells[0] = emptyspell; playerSpells[1] = emptyspell; playerSpells[2] = emptyspell; playerSpells[3] = emptyspell;
                }
                else if (choice == "exit") break;
            }
            Console.Clear();
        } // 1 spell zadający dmg 2 zadający magiczny dmg 3 obronny 4 nakładający efekt na gracza 5 nakładający efekt na przeciwnika
        static void SpellWrite(Spells spell)
        {
            if (spell.reqEQ == 0 )
            {
                if (spell.reqLvl <= exp[2])
                {
                    Console.WriteLine("{0} - {1} (cost {2} mana)", spell.shortName,spell.spellName,spell.manaCost);
                    Console.WriteLine("\t{0}", spell.description);
                }
            }
            else if(spell.reqEQ == playerEQ[0,0].id || spell.reqEQ == playerEQ[1, 0].id || spell.reqEQ == playerEQ[2, 0].id || spell.reqEQ == playerEQ[3, 0].id)
            {
                if(spell.reqLvl <= exp[2])
                {
                    Console.WriteLine("{0} - {1} (cost {2} mana) - {2}", spell.shortName, spell.spellName, spell.manaCost, spell.reqEQ);
                    Console.WriteLine("\t{0}", spell.description);
                }
            }
        }
        static void SpellCheck(Spells spell,ref string choice)
        {
            if (playerSpells[0].shortName == choice || playerSpells[1].shortName == choice || playerSpells[2].shortName == choice || playerSpells[3].shortName == choice)
            {
                Console.WriteLine("You allready know this spell");
                choice = "null";
            }
            else if (choice == spell.shortName)
            {
                if (spell.reqEQ == 0)
                {
                    if (spell.reqLvl <= exp[2])
                    {
                        for (int i = 0; i < playerSpells.Length; i++)
                        {
                            if (playerSpells[i].spellName == "None")
                            {
                                playerSpells[i] = spell;
                                Console.WriteLine("{0} added to spellset",spell.spellName);
                                choice = "null";
                                break;
                            }
                            else if (i == playerSpells.Length - 1)
                            {
                                Console.WriteLine("You dodn't have free slot for this spell");
                                choice = "null";
                            }
                        }
                    }
                }
                else if (spell.reqEQ == playerEQ[0, 0].id || spell.reqEQ == playerEQ[1, 0].id || spell.reqEQ == playerEQ[2, 0].id || spell.reqEQ == playerEQ[3, 0].id)
                {
                    if (spell.reqLvl <= exp[2])
                    {
                        for (int i = 0; i < playerSpells.Length; i++)
                        {
                            if (playerSpells[i].spellName == "None")
                            {
                                playerSpells[i] = spell;
                                Console.WriteLine("{0} added to spellset", spell.spellName);
                                choice = "null";
                                break;
                            }
                            else if (i == playerSpells.Length - 1)
                            {
                                Console.WriteLine("You dodn't have free slot for this spell");
                                choice = "null";
                            }
                        }
                    }
                }
            }
        }
        static void SpellCast(Spells spell, ref int pPAbuff, ref int pMAbuff, ref int pPSbuff, ref int pMSbuff, ref int ePAbuff, ref int eMAbuff, ref int ePSbuff, ref int eMSbuff)
        {
            if (spell.efectID == 4)
            {

            }
            else if (spell.efectID == 5)
            {
                if (spell.shortName == "wfc")
                {
                    ePAbuff -= spell.powerMultiplayer;
                    eMAbuff -= spell.powerMultiplayer;
                    if (ePAbuff < -8)
                    {
                        ePAbuff = -8; 
                        Console.WriteLine("Enemy's physical atack can't be lowered");
                    }
                    else Console.WriteLine("Enemy's physical atack lowered by {0}", spell.powerMultiplayer);
                    if (eMAbuff < -8)
                    {
                        eMAbuff = -8;
                        Console.WriteLine("Enemy's magical atack can't be lowered");
                    }
                    else Console.WriteLine("Enemy magical atack lowered by {0}", spell.powerMultiplayer);
                }
            }
        }//uzupełnic po stworzeniu spelli
        static void Main(string[] args)
        {
            Random roll = new();
            int size = 25;
            int[,] world = new int[size, size];
            SpendPoint();
            WorldCreator(world, size, roll.Next(40,60), 40, 10, 20, 15, 10);            
            //WorldCreator(world, size, 100, 100, 100, 100, 150, 100);
            playerEQ[0,0] = emptyslot; playerEQ[0,1] = emptyslot;
            playerEQ[1,0] = emptyslot; playerEQ[1,1] = emptyslot;
            playerEQ[2,0] = emptyslot; playerEQ[2,1] = emptyslot;
            playerEQ[3,0] = emptyslot; playerEQ[3,1] = emptyslot;
            playerSpells[0] = emptyspell; playerSpells[1] = emptyspell; playerSpells[2] = emptyspell; playerSpells[3] = emptyspell;   
            Game(world, size);            
        }
    }
}
