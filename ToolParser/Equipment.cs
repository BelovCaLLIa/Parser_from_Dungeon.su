using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Data;

namespace Parser
{
	//таблица снаряжения с сайта
	class Equipment
	{
		private IWebDriver browser;

		//запуск браузера
		public void startBrowser()
		{
			browser = new OpenQA.Selenium.Chrome.ChromeDriver();
			browser.Manage().Window.Maximize();
			browser.Navigate().GoToUrl("https://dungeon.su/articles/inventory/98-equipment/");
		}

		//поиск элемента
		public List<string> findAnItem()
		{
			List<string> head = new List<string>();

			//список из элементов с тегом tr (все, с поиском)
			IReadOnlyList<IWebElement> td = browser.FindElements(By.TagName("td"));

			List<string> str = new List<string>();
			for (int i = 0; i < td.Count; i++)
			{
				str.Add(Convert.ToString(td[i].Text));
			}

            for (int i = 0; i<str.Count; i++)
            {
				if (str[i] == "" || str[i] == "Предмет" || str[i] == "Стоимость" || str[i] == "Вес")
				{
					str.Remove(str[i]);
					i -= 1;
				}
            }

			/*
			//Создание бд
			DataSet equipment = new DataSet("Снаряжение");
			//Создание таблицы
			DataTable ammunition = new DataTable("Боеприпасы");
			//Создание колонки
			DataColumn subject = new DataColumn("Предмет", typeof(string));
			DataColumn cost = new DataColumn("Стоимость", typeof(string));
			DataColumn weight = new DataColumn("Вес", typeof(string));

			//Добавляем колонки в таблицу
			ammunition.Columns.AddRange(new DataColumn[] { subject, cost, weight });

			for (int i = 0; i < str.Count; i++)
			{
				//Создание строк
				DataRow row = ammunition.NewRow();
				row["Предмет"] = "";
				row["Стоимость"] = "";
				row["Вес"] = "";

				//Добавление строк в таблицу
				ammunition.Rows.Add(row);
			}

			//Добавление таблицы в бд
			equipment.Tables.Add(ammunition);

			Console.WriteLine(equipment);
			*/

			//IReadOnlyList<IWebElement> resultsTable = browser.FindElements(By.CssSelector("tr[class=\"table_header\"]"));

			/*
			//вывод содержимого в списке resultsTable
			for (int i = 0; i < tr.Count; i++) 
			{
				Console.WriteLine(tr[i].Text); 
			}
			*/

			browser.Close();

			return str;

		}

		public async Task writingToFileAsync(List<string> str)
		{
			/*
			List<string> str = new List<string>();
			for (int i = 0; i < _table.Count; i++)
			{
				str.Add(Convert.ToString(_table[i].Text));
			}
			*/

			JsonSerializerOptions options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
				WriteIndented = true
			};

		   
			// сохранение данных
			using (FileStream fs = new FileStream("Equipment.json", FileMode.OpenOrCreate))
			{
				//записывает как UTF-8, но отображаются как ASCII символы
				await JsonSerializer.SerializeAsync<List<string>>(fs, str, options);
				Console.WriteLine("Data has been saved to file Equipment.json");
			}

			/*
			// чтение данных
			using (FileStream fs = new FileStream("Table.json", FileMode.OpenOrCreate))
			{
				List<string> restoredTable = await JsonSerializer.DeserializeAsync<List<string>>(fs);
				for (int i = 0; i < restoredTable.Count; i++)
				{
					Console.WriteLine(restoredTable[i]);
				}
			}
			*/
		}
	}
}
