using OpenQA.Selenium;

namespace Mainpro.AppFramework
{
    /// <summary>
    /// Elements that will exist on the login page
    /// </summary>
    public class EntryCarouselPathwayPageBys
    {

        // Buttons
        public readonly By EnterBtn = By.XPath("//div[@title='Enter']");
        public readonly By PreviousArrowBtn = By.XPath("//div[@title='//img[@alt='Previous']']");
        public readonly By NextArrowBtn = By.XPath("//img[@alt='Next']");
        public readonly By BackBtn = By.XPath("//div[@id='btnBackCarousel']/span");
        public readonly By NextBtn = By.XPath("//div[@id='btnNextCarousel']/span");
        public readonly By PlayBtn = By.XPath("//img[@id='playButton']");
        public readonly By PauseBtn = By.XPath("//img[@id='pauseButton']");
        public readonly By CarouselCircleBtn1 = By.XPath("//ol[@class='carousel-indicators']/li");
        public readonly By CarouselCircleBtn2 = By.XPath("//ol[@class='carousel-indicators']/li[2]");
        public readonly By CarouselCircleBtn3 = By.XPath("//ol[@class='carousel-indicators']/li[3]");
        public readonly By CarouselCircleBtn4 = By.XPath("//ol[@class='carousel-indicators']/li[4]");
        public readonly By CarouselCircleBtn5 = By.XPath("//ol[@class='carousel-indicators']/li[5]");
        public readonly By CarouselCircleBtn6 = By.XPath("//ol[@class='carousel-indicators']/li[6]");
        public readonly By PeerSupportedBeginBtn = By.XPath("//div[contains(@class,'peerSupportedWidget')]//button");
        public readonly By SelfGuidedBeginBtn = By.XPath("//div[contains(@class,'selfGuidedWidget')]//button");
        public readonly By PeerSupportedInfoBtn = By.XPath("//div[contains(@class,'peerSupportedWidget')]//img");
        public readonly By SelfGuidedInfoBtn = By.XPath("//div[contains(@class,'selfGuidedWidget')]//img");
        public readonly By PeerSupportedPleaseConfirmFormConfirmBtn = By.XPath("//div[contains(@class,'PeerSupportModal')]//span[text()='Confirm']");
        public readonly By PeerSupportedPleaseConfirmFormNoBtn = By.XPath("//div[contains(@class,'PeerSupportModal')]//span[text()='No']");
        public readonly By SelfGuidedModalPleaseConfirmFormConfirmBtn = By.XPath("//div[contains(@class,'SelfGuidedModal')]//span[text()='Confirm']");
        public readonly By SelfGuidedModalPleaseConfirmFormNoBtn = By.XPath("//div[contains(@class,'SelfGuidedModal')]//span[text()='No']");



        // Charts

        // Check boxes

        // General
        public readonly By CarouselCircleSlide1 = By.XPath("//div[@class='carousel-inner']/div[1]");
        public readonly By CarouselCircleSlide2 = By.XPath("//div[@class='carousel-inner']/div[2]");
        public readonly By CarouselCircleSlide3 = By.XPath("//div[@class='carousel-inner']/div[3]");
        public readonly By CarouselCircleSlide4 = By.XPath("//div[@class='carousel-inner']/div[4]");
        public readonly By CarouselCircleSlide5 = By.XPath("//div[@class='carousel-inner']/div[5]");
        public readonly By CarouselCircleSlide6 = By.XPath("//div[@class='carousel-inner']/div[6]");
        public readonly By ImageContainer = By.XPath("//div[@class='fireball-widget plpImageContainer']");


        

        // Labels
        public readonly By PLPActivityOverviewLbl = By.XPath("//h3[text()='PLP Activity Overview ']");

        
        // Links

        // Menu Items    

        // Radio buttons

        // Tables       

        // Tabs

        // Text boxes




    }
}
