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
    //таблица Драгоценные камни
    class Gems
    {
		private IWebDriver browser;
		//Запуск браузера
		public void startBrowser()
		{
			browser = new OpenQA.Selenium.Chrome.ChromeDriver();
			browser.Manage().Window.Maximize();
			browser.Navigate().GoToUrl("https://dungeon.su/articles/inventory/71-dragotsennye_kamni/");
		}

		//поиск элемента
		public List<string> findAnItem()
		{
			IReadOnlyList<IWebElement> strong = browser.FindElements(By.TagName("strong"));

			IReadOnlyList<IWebElement> td = browser.FindElements(By.TagName("td"));

			List<string> str = new List<string>();
			for (int i = 0; i < td.Count; i++)
			{
				if (6 < i)
				{
					str.Add(Convert.ToString(td[i].Text));
				}
				if (i == 6)
				{
					str.Add(Convert.ToString(strong[3].Text));
				}
				if (i == 32)
				{
					str.Add(Convert.ToString(strong[4].Text));
				}
				if (i == 58)
				{
					str.Add(Convert.ToString(strong[5].Text));
				}
				if (i == 80)
				{
					str.Add(Convert.ToString(strong[6].Text));
				}
				if (i == 94)
				{
					str.Add(Convert.ToString(strong[7].Text));
				}
				if (i == 112)
				{
					str.Add(Convert.ToString(strong[8].Text));
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
			using (FileStream fs = new FileStream("Gems.json", FileMode.OpenOrCreate))
			{
				//записывает как UTF-8, но отображаются как ASCII символы
				await JsonSerializer.SerializeAsync<List<string>>(fs, str, options);
				Console.WriteLine("Data has been saved to file Gems.json");
			}
		}
	}
}
