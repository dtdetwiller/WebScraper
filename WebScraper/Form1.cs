using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Keys = OpenQA.Selenium.Keys;

namespace WebScraper
{
    public partial class Form1 : Form
    {
        // The web driver
        IWebDriver _driver;
        // Array that holds all the courses information as a string.
        public List<string> courses;
        // List of open tabs
        public List<string> tabs;

        public Form1()
        {
            InitializeComponent();

            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            this.outputBox.Text += "Starting Chrome...\n";

            saveButton.Enabled = false;
            if (_driver == null)
            {
                _driver = new ChromeDriver();
                // Set implicit wait
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            }

            courses = new List<string>();
            tabs = new List<string>();
        }

        /// <summary>
        /// Method is called when the find enrollments button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findEnrollments_Click(object sender, EventArgs e)
        {
            // Disable the save button
            saveButton.Enabled = false;

            // Action for the driver to allow new tab creation
            Actions newTab = new Actions(_driver);

            string year = this.yearTextBox2.Text;
            string semester = this.semesterTextBox2.Text;
            int numCourses = (int)this.numEnrollments.Value;
            int count = 0;

            string URL = GetURL(year, semester, "index");

            _driver.Navigate().GoToUrl(URL);

            // Get the list of links for each department
            var results = _driver.FindElements(By.CssSelector(".btn.btn-light.btn-block"));

            // Loop through each department
            foreach (var r in results)
            {
                // If we have scraped the specified number of courses, break
                if (count >= numCourses)
                    break;

                // Create new tabs for each click to avoid stale reference exception
                if (count > 1)
                {
                    // Get rid of the last tab
                    _driver.SwitchTo().Window(tabs[1]);
                    tabs = tabs.Where(val => val != tabs[1]).ToList();
                    _driver.Close();
                    _driver.SwitchTo().Window(tabs[0]);

                    // Click on this department and get the list of all courses
                    // And open it in a new tab
                    newTab.KeyDown(Keys.LeftControl).Click(r).KeyUp(Keys.LeftControl).Build().Perform();
                    tabs = _driver.WindowHandles.ToList();
                    _driver.SwitchTo().Window(tabs[1]);
                }
                else
                {
                    // Click on this department and get the list of all courses
                    // And open it in a new tab
                    newTab.KeyDown(Keys.LeftControl).Click(r).KeyUp(Keys.LeftControl).Build().Perform();
                    tabs = _driver.WindowHandles.ToList();
                    _driver.SwitchTo().Window(tabs[1]);
                }

                // Find all the courses
                var result2 = _driver.FindElement(By.Id("class-details"));
                var results2 = result2.FindElements(By.XPath("./*[@class=\"class-info card mt-3\"]"));

                // Index so that the seating availability table index can be found easily for this course
                var courseCount = 0;

                // Loop through each course in the department
                foreach (var r2 in results2)
                {
                    // Stop the process once reached the amount of courses needed to be scraped
                    if (count >= numCourses)
                        break;

                    Course c = new Course();
                    c.year = year;
                    c.semester = semester;

                    // Get the header for this course
                    var header = r2.FindElement(By.TagName("h3"));

                    if (header != null)
                    {
                        // Gets the course (CS 2420)
                        string courseNum = header.Text.Split('-')[0].Trim().ToUpper();
                        string[] depAndNum = courseNum.Split(' ');

                        // Check if course is between 1000 and 6999
                        if (int.Parse(depAndNum[1]) >= 1000 && int.Parse(depAndNum[1]) <= 6999)
                        {
                            // Separate department and number
                            c.department = depAndNum[0];
                            c.number = depAndNum[1];

                            // Get the course title
                            string text = header.Text.Split('-')[1].Trim();
                            c.title = text.Substring(4);

                            // Check if the course is a lecuture/seminar, if not then continue.
                            var list = result2.FindElement(By.CssSelector(".row.breadcrumb-list.list-unstyled"));
                            if (CourseIsLecture(list.FindElements(By.TagName("li"))))
                            {
                                c.count = courseCount;
                                GetCourseInformation(r2, c);
                            }
                            else
                            {
                                courseCount++;
                                continue;
                            }
                        }
                    }

                    // Increment the course count on this department page
                    courseCount++;
                    // Increment total course count
                    count++;
                }

            }

            // Enable the save button so the user can save the courses
            saveButton.Enabled = true;
        }

        /// <summary>
        /// Method is called when the find course info button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findCourseInfo_Click(object sender, EventArgs e)
        {
            // Disable the save button
            saveButton.Enabled = false;

            Course c = new Course();

            c.year = this.yearTextBox1.Text;
            c.semester = this.semesterComboBox1.Text;

            string course = this.courseTextBox1.Text;
            string[] depAndNum = course.Split(' ');
            c.department = depAndNum[0];
            c.number = depAndNum[1];

            string URL = GetURL(c.year, c.semester, c.department);

            _driver.Navigate().GoToUrl(URL);

            var result = _driver.FindElement(By.Id("class-details"));
            var results = result.FindElements(By.XPath("./*[@class=\"class-info card mt-3\"]"));

            int courseCount = 0;

            foreach (var r in results)
            {
                result = r;
                //check if the course is the one we specified
                var header = r.FindElement(By.TagName("h3"));
                if (header != null)
                {
                    string courseNum = header.Text.Split('-')[0].Trim().ToUpper();
                    string[] courseSplit = courseNum.Split(' ');

                    // Check if course is between 1000 and 6999
                    if (int.Parse(courseSplit[1]) >= 1000 && int.Parse(courseSplit[1]) <= 6999)
                    {
                        string text = header.Text.Split('-')[1].Trim();
                        // Get the course title
                        c.title = text.Substring(4);
                        // If they equal eachother, the element in header is the one we want.
                        if (courseNum == course)
                        {
                            var list = result.FindElement(By.CssSelector(".row.breadcrumb-list.list-unstyled"));
                            if (CourseIsLecture(list.FindElements(By.TagName("li"))))
                                break;
                        }
                    }
                }
                courseCount++;
            }

            c.count = courseCount;

            GetCourseInformation(result, c);

            // Enable the save button so the user can save the course
            saveButton.Enabled = true;
        }

        /// <summary>
        /// Helper method that gets all of the courses information.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="c"></param>
        private void GetCourseInformation(IWebElement result, Course c)
        {

            var ul = result.FindElement(By.CssSelector(".row.breadcrumb-list.list-unstyled"));
            var results = ul.FindElements(By.TagName("li"));

            for (int i = 0; i < results.Count; i++)
            {
                string check = results[i].Text.Trim();
                // Get the course Number
                if (check.Contains("Class Number"))
                {
                    try
                    {
                        var span = results[i].FindElement(By.TagName("span"));
                        c.classNumber = span.Text.Trim();
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Exception: There was no course number for " + c.department + c.number);
                        c.classNumber = "--";
                    }
                }
                // Get the first course instructor.
                else if (check.Contains("Instructor"))
                {
                    try
                    {
                        var a = results[i].FindElement(By.TagName("a")).GetAttribute("href");
                        if (c.instructorsUNID != null)
                            c.instructorsUNID = c.instructorsUNID + " and " + a.Substring(24, 8);
                        else
                            c.instructorsUNID = a.Substring(24, 8);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Exception: There was no instructor listed for " + c.department + c.number);
                        c.instructorsUNID = "--";
                    }
                }
                // Get the course Component
                else if (check.Contains("Component"))
                {
                    try
                    {
                        var span = results[i].FindElement(By.TagName("span"));
                        c.component = span.Text.Trim();
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Exception: There was no component listed for " + c.department + c.number);
                        c.component = "--";
                    }
                }
                // Get the course units
                else if (check.Contains("Units"))
                {
                    try
                    {
                        var span = results[i].FindElement(By.TagName("span"));
                        c.credits = span.Text.Trim();
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Exception: There was were no units listed for " + c.department + c.number);
                        c.credits = "--";
                    }
                }
            }

            try
            {
                // Get the days/times of the course
                var tbody = result.FindElement(By.TagName("tbody"));
                var th1 = tbody.FindElement(By.CssSelector(".text-nowrap.text-left.p-0"));

                c.daysTimes = th1.Text.Trim();

                // Get the location of the course
                var th2 = tbody.FindElement(By.CssSelector(".text-nowrap.text-right.p-0"));

                c.location = th2.Text.Trim();
            }
            catch (Exception e)
            {
                c.daysTimes = "--";
                c.location = "--";
            }



            // Now find the course description
            c.description = FindCourseDescription(c.department + c.number);

            tabs = tabs.Where(val => val != tabs[2]).ToList();
            _driver.Close();
            _driver.SwitchTo().Window(tabs[1]);

            // Get the enrollment cap of the course
            GetEnrollment(c);

            courses.Add(c.GetString());
        }

        /// <summary>
        /// Helper method that gets the enrollment cap of the current class
        /// </summary>
        /// <param name="c"></param>
        private void GetEnrollment(Course c)
        {
            // Open a new tab for finding the course enrollment
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.open();");
            tabs = _driver.WindowHandles.ToList();
            _driver.SwitchTo().Window(tabs[2]);
            _driver.Navigate().GoToUrl(GetURL(c.year, c.semester, c.department));
            IWebElement result;

            // Find the seating availability chart for all the classes
            try
            {
                result = _driver.FindElement(By.XPath("//*[@id=\"uu-skip-target\"]/div[5]/div/a"));
            }
            catch (Exception e)
            {
                result = _driver.FindElement(By.XPath("//*[@id=\"uu-skip-target\"]/div[4]/div/a"));
            }

            result.Click();

            // Get the list of all courses
            var results = _driver.FindElements(By.XPath("//*[@id=\"seatingAvailabilityTable\"]/tbody//tr"));
            // Get the correct course
            result = results[c.count];
            // Get the enrollment element.
            result = result.FindElement(By.CssSelector("td:nth-of-type(6)"));

            c.enrollment = result.Text.Trim();

            tabs = tabs.Where(val => val != tabs[2]).ToList();
            _driver.Close();
            _driver.SwitchTo().Window(tabs[1]);
        }

        /// <summary>
        /// Helper method that finds the course description.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        private string FindCourseDescription(string course)
        {
            // Create new tab to avoid stale reference exception.
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.open();");
            tabs = _driver.WindowHandles.ToList();
            _driver.SwitchTo().Window(tabs[2]);

            _driver.Navigate().GoToUrl("https://catalog.utah.edu");

            IWebElement result = null;

            try
            {
                result = _driver.FindElement(By.XPath("//*[@id=\"top\"]/div/div[3]/div/nav/ul/li[2]/a"));
            }
            catch (Exception e)
            {
                _driver.Navigate().GoToUrl("https://catalog.utah.edu");
                result = _driver.FindElement(By.XPath("//*[@id=\"top\"]/div/div[3]/div/nav/ul/li[2]/a"));
            }
            result.Click();
            result = _driver.FindElement(By.XPath("/html/body/div[1]/main/section/div/div/div/div/div/div[2]/span/div/div/div/input"));
            result.SendKeys(course);
            result.SendKeys(OpenQA.Selenium.Keys.Enter);

            // Someonetime the search doesn't go through, if that happens try again.
            try
            {
                result = _driver.FindElement(By.XPath("//*[@id=\"kuali-catalog-main\"]/div[2]/div[2]/div[2]/div/div[2]/a"));
            }
            catch (Exception e)
            {
                result.SendKeys(Keys.Enter);
                result = _driver.FindElement(By.XPath("//*[@id=\"kuali-catalog-main\"]/div[2]/div[2]/div[2]/div/div[2]/a"));
            }

            result.Click();
            result = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]"));
            var results = result.FindElements(By.TagName("h3"));

            // Counts how many divs down the course description is on the page.
            int divCount = 1;
            foreach (var r in results)
            {
                if (r.Text.Contains("Course Description"))
                {
                    // Get the correct element witht he description in it (at index divCount)
                    result = _driver.FindElement(By.XPath($"//*[@id=\"__KUALI_TLP\"]/div/div/div[{divCount}]/span/div/div/div/div/div"));
                    break;
                }
                divCount++;
            }

            return result.Text.Trim();
        }

        /// <summary>
        /// This checks whether the course is a lecture/seminar or not.
        /// </summary>
        /// <param name="results"></param>
        /// <returns>
        /// returns true if it's a lecture/seminar and false otherwise.
        /// </returns>
        private bool CourseIsLecture(ReadOnlyCollection<IWebElement> results)
        {
            foreach (var r in results)
            {
                string check = r.Text.Trim();
                if (check.Contains("Component"))
                    if (r.FindElement(By.TagName("span")).Text.Contains("Lecture") || r.FindElement(By.TagName("span")).Text.Contains("Seminar"))
                        return true;
            }

            return false;
        }

        /// <summary>
        /// Helper method that gets the correct class schedule URL
        /// </summary>
        /// <param name="y"> The year </param>
        /// <param name="s"> The semester </param>
        /// <returns></returns>
        private string GetURL(string y, string s, string d)
        {
            string year = y.Substring(2);
            string semester = "";

            switch (s)
            {
                case "Fall":
                    semester = "8";
                    break;
                case "Spring":
                    semester = "4";
                    break;
                case "Summer":
                    semester = "6";
                    break;
            }

            if (d.Equals("index"))
                return "https://student.apps.utah.edu/uofu/stu/ClassSchedules/main/1" + year + semester + "/index.html";

            string URL = "https://student.apps.utah.edu/uofu/stu/ClassSchedules/main/1" + year + semester + "/class_list.html?subject=" + d;

            return URL;
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            // Shut down driver
            _driver.Close();
        }

        /// <summary>
        /// Pop up save file dialog for user to save courses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Reference: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.savefiledialog?view=windowsdesktop-5.0
        private void saveButton_Click(object sender, EventArgs e)
        {
            StreamWriter writer;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                writer = new StreamWriter(saveFileDialog1.FileName);
                // Code to write the stream goes here.
                foreach (string c in courses)
                {
                    writer.WriteLine(c);
                }

                writer.Close();
            }
        }
    }

    /// <summary>
    /// This class represents a course and all of the information we are scraping for the course.
    /// </summary>
    public class Course
    {
        // The course semester
        public string semester { get; set; }
        // The course year
        public string year { get; set; }
        // The course department
        public string department { get; set; }
        // The course number
        public string number { get; set; }
        // The course title
        public string title { get; set; }
        // The course instructors UNID
        public string instructorsUNID { get; set; }
        // The course credits (units)
        public string credits { get; set; }
        // The enrollement size
        public string enrollment { get; set; }
        // The description of the course
        public string description { get; set; }
        // The class number
        public string classNumber { get; set; }
        // The component of the course
        public string component { get; set; }
        // The days and times of the course
        public string daysTimes { get; set; }
        // The location(s) of the course
        public string location { get; set; }
        // The number in order down the page this course is
        public int count { get; set; }

        /// <summary>
        /// Returns the complete string of the course information for the CSV file.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            return semester + "," + year + "," + department + "," + number + "," + title + "," + instructorsUNID + "," + credits + "," + enrollment + "," + "\"" + description + "\"";
        }

    }
}
