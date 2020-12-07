using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.IO;
using System.Text.Encodings.Web;

namespace Parser
{
    //Таблица Инмтрументы
    class Tools
    {
		private IWebDriver browser;
		//Запуск браузера
		public void startBrowser()
		{
			browser = new OpenQA.Selenium.Chrome.ChromeDriver();
			browser.Manage().Window.Maximize();
			browser.Navigate().GoToUrl("https://dungeon.su/articles/inventory/100-tools/");
		}

		//поиск элемента
		public List<string> findAnItem()
		{
			IWebElement nameTable = browser.FindElement(By.ClassName("tableTitle"));
			IReadOnlyList<IWebElement> td = browser.FindElements(By.TagName("td"));

			List<string> str = new List<string>();
			str.Add(Convert.ToString(nameTable.Text));
			for (int i = 0; i < td.Count; i++)
			{
				if (6 < i)
				{
					str.Add(Convert.ToString(td[i].Text));
				}
			}

			browser.Close();

			return str;
		}

		public async Task writingToFileAsync(List<string> str)
		{

			JsonSerializerOptions options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
				WriteIndented = true
			};


			// сохранение данных
			using (FileStream fs = new FileStream("Tools.json", FileMode.OpenOrCreate))
			{
				//записывает как UTF-8, но отображаются как ASCII символы
				await JsonSerializer.SerializeAsync<List<string>>(fs, str, options);
				Console.WriteLine("Data has been saved to file Tools.json");
			}
		}
	}
}
