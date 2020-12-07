using System;
using OpenQA.Selenium;
using System.Collections.Generic;


//парсер с сайта из D&D5
namespace Parser
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //таблица яды
            Poisons my_poisons = new Poisons();
            my_poisons.startBrowser();
            List<string> str = my_poisons.findAnItem();
            await my_poisons.writingToFileAsync(str);

            //1. таблица Безделушки
            Trinkets my_trinkets = new Trinkets();
            my_trinkets.startBrowser();
            List<string> str_1 = my_trinkets.findAnItem();
            await my_trinkets.writingToFileAsync(str_1);

            //2. Доспехи и щиты
            Armor_and_shields my_armor_and_shields = new Armor_and_shields();
            my_armor_and_shields.startBrowser();
            List<string> str_2 = my_armor_and_shields.findAnItem();
            await my_armor_and_shields.writingToFileAsync(str_2);

            //3. таблица Драгоценные камни
            Gems my_gems = new Gems();
            my_gems.startBrowser();
            List<string> str_3 = my_gems.findAnItem();
            await my_gems.writingToFileAsync(str_3);

            //4. Таблица Инмтрументы
            Tools my_tools = new Tools();
            my_tools.startBrowser();
            List<string> str_4 = my_tools.findAnItem();
            await my_tools.writingToFileAsync(str_4);

            //5. Таблица Монеты
            Coins my_coins = new Coins();
            my_coins.startBrowser();
            List<string> str_5 = my_coins.findAnItem();
            await my_coins.writingToFileAsync(str_5);

            //6. Таблица Оружие
            Arms my_arms = new Arms();
            my_arms.startBrowser();
            List<string> str_6 = my_arms.findAnItem();
            await my_arms.writingToFileAsync(str_6);

            //7. таблица Произведения искусства
            Works_of_art my_works_of_art = new Works_of_art();
            my_works_of_art.startBrowser();
            List<string> str_7 = my_works_of_art.findAnItem();
            await my_works_of_art.writingToFileAsync(str_7);

            //8. таблица Снаряжения
            Equipment my_equipment = new Equipment();
            my_equipment.startBrowser();
            //IReadOnlyList<IWebElement> table = my_parser.findAnItem();
            List<string> str_8 = my_equipment.findAnItem();
            await my_equipment.writingToFileAsync(str_8);

            //9. таблица Сокровищница
            Treasury my_treasury = new Treasury();
            my_treasury.startBrowser();
            List<string> str_9 = my_treasury.findAnItem();
            await my_treasury.writingToFileAsync(str_9);
        }
    }
}
