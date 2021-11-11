Author:    Daniel Detwiller
Partner:   None
Date:      11-04-2021
Course:    CS 4540, University of Utah, School of Computing
Copyright: CS 4540 and Daniel Detwiller - This work may not be copied for use in Academic Coursework.

Deployed URL:  N/A
Github Page:   https://github.com/uofu-cs4540-fall2021/ps7-webscraper-dtdetwiller

Comments to Evaluators:

  Input syntax: Course must be in the format CS 2420 (CS2420 will not be accepted)
				Year must be in the format 2019 (19, 19', or 2k19 won't be accepted)
				Semester is a dropdown menu

				Once done scraping, click the save button in the top right. Make sure to save file as a CSV file.


Assignment Specific Write-up:

  1. Peer review: 
		Name: Kolby Kunz
		UID: u1117352

  2. Time out value:
		I chose 5 seconds because that was recommended. When trying to find the seating availability link, some pages are
		different so I have a try catch for two situations, so sometimes it takes 5 seconds to find the chart.
  
  3. HTMLAgilityPack vs. Selenium:
		We are using selenium because it keeps track of cookies on its own. In HTMLAgilityPack you would have to keep track of 
		the cookies manually. Selenium handles everything a browser does and AgilityPack allows you to do C# processing on text files
		or webpages easily. It;s best to use selenium when you are acting like a browser.
  
  4. Thoughts:
		Selenium was pretty easy to use once I started to understand the FindElement and FindElements methods. Selenium was acutally really cool
		too, the way you can navigate through a website super quickly and take data down really impressed me. The one part that I struggled on
		was using Selenium in a forloop repeating operations. When I would go back to the page to start the loop process over again, the DOM
		would be destroyed and I would get a Stale Reference Exception. It took me a bit to solve this problem. I created new tabs for each operation
		and get track of each tab in an array. When the iteration was done, I would destroy that tab and then switch back to the original tab
		that had the needed webpage on it. This worked very well, but took a while to figure out and implement.

		I think it would be much easier to use selenium to test our TA Applicaiton site because we won't have to scrape any data, we will just have
		to navigate through the site and use all the functions. This will be easy with the Click() method and the SendKeys() method. 
		

Peers Helped:

  o) None

Peers Consulted:

   o) None, just TA's

Acknowledgements:

   - Selenium WebDriver and ChromeDriver

References:

   1. Save File Dialog code: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.savefiledialog?view=windowsdesktop-5.0