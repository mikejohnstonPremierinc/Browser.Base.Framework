using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using Browser.Core.Framework;
using OpenQA.Selenium;

namespace LMS.AppFramework.Constants_UAMS_
{
    public static class Constants_UAMS
    {
        public static By Tbl1ViewAllBtn = By.XPath("(//div[@class='activity-card-list card'])[1]//button[text()='View All'] | (//div[@class='activity-card-list card'])[1]//div[contains(text(), 'No activities available')]");
        public static By Tbl2ViewAllBtn = By.XPath("(//div[@class='activity-card-list card'])[2]//button[text()='View All'] | (//div[@class='activity-card-list card'])[2]//div[contains(text(), 'No activities available')]");
        public static By Tbl3ViewAllBtn = By.XPath("(//div[@class='activity-card-list card'])[3]//button[text()='View All'] | (//div[@class='activity-card-list card'])[3]//div[contains(text(), 'No activities available')]");
        public static By Tbl4ViewAllBtn = By.XPath("(//div[@class='activity-card-list card'])[4]//button[text()='View All'] | (//div[@class='activity-card-list card'])[4]//div[contains(text(), 'No activities available')]");
        public static By Tbl5ViewAllBtn = By.XPath("(//div[@class='activity-card-list card'])[5]//button[text()='View All'] | (//div[@class='activity-card-list card'])[5]//div[contains(text(), 'No activities available')]");
        public static By Tbl6ViewAllBtn = By.XPath("(//div[@class='activity-card-list card'])[6]//button[text()='View All'] | (//div[@class='activity-card-list card'])[6]//div[contains(text(), 'No activities available')]");


        public static By Tbl1 = By.XPath("(//div[@class='activity-card-list card'])[1]");
        public static By Tbl1FirstLnk = By.XPath("(//div[@class='activity-card-list card'])[1]//div[not(@class='activity-card-inner hiddenActivityRows')]/div/H4 | (//div[@class='activity-card-list card'])[1]//div[contains(text(), 'No activities available')]");
        public static By Tbl2 = By.XPath("(//div[@class='activity-card-list card'])[2]");
        public static By Tbl2FirstLnk = By.XPath("(//div[@class='activity-card-list card'])[2]//div[not(@class='activity-card-inner hiddenActivityRows')]/div/H4 | (//div[@class='activity-card-list card'])[2]//div[contains(text(), 'No activities available')]");
        public static By Tbl3 = By.XPath("(//div[@class='activity-card-list card'])[3]");
        public static By Tbl3FirstLnk = By.XPath("(//div[@class='activity-card-list card'])[3]//div[not(@class='activity-card-inner hiddenActivityRows')]/div/H4 | (//div[@class='activity-card-list card'])[3]//div[contains(text(), 'No activities available')]");
        public static By Tbl4 = By.XPath("(//div[@class='activity-card-list card'])[4]");
        public static By Tbl4FirstLnk = By.XPath("(//div[@class='activity-card-list card'])[4]//div[not(@class='activity-card-inner hiddenActivityRows')]/div/H4 | (//div[@class='activity-card-list card'])[4]//div[contains(text(), 'No activities available')]");
        public static By Tbl5 = By.XPath("(//div[@class='activity-card-list card'])[5]");
        public static By Tbl5FirstLnk = By.XPath("(//div[@class='activity-card-list card'])[5]//div[not(@class='activity-card-inner hiddenActivityRows')]/div/H4 | (//div[@class='activity-card-list card'])[5]//div[contains(text(), 'No activities available')]");
        public static By Tbl6 = By.XPath("(//div[@class='activity-card-list card'])[6]");
        public static By Tbl6FirstLnk = By.XPath("(//div[@class='activity-card-list card'])[6]//div[not(@class='activity-card-inner hiddenActivityRows')]/div/H4 | (//div[@class='activity-card-list card'])[6]//div[contains(text(), 'No activities available')]");

    }

}