﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace UI.Helpers
{
    static class WebElementExtensions
    {
        /// <summary>
        ///    Highlights founded web element in the DOM.
        /// </summary>
        /// <param name="element">
        ///    Web element to highlight.
        /// </param>
        /// <param name="driver">
        ///    IWebDriver driver instance.
        /// </param>
        public static void WeHighlightElement(this IWebElement element, IWebDriver driver)
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(HighlightSettings.WeHighlightedColor, element);

        }

        /// <summary>
        ///    Finds element with web element in the DOM.
        /// </summary>
        /// <param name="element">
        ///    IWebElement element instance.
        /// </param>
        /// <param name="by">
        ///    Locator pointing to the web element to find.
        /// </param>
        /// <param name="driver">
        ///    IWebDriver driver instance.
        /// </param>
        /// <param name="sec">
        ///   Time to wait for driver to find the web element.
        ///   Default time is 10 seconds.
        /// </param>
        /// <returns>
        ///    IWebElement specified by locator.
        /// </returns>
        /// <exception cref="WebDriverTimeoutException">
        ///    Driver finding the web element timeouts after the specified time.
        /// </exception>
        public static IWebElement WeFindElement(this IWebElement element, IWebDriver driver, By by, int sec = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(sec));
                return wait.Until(drv =>
                {
                    try
                    {
                        element.WeHighlightElement(driver);
                        return element.FindElement(by);

                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                }
                );
            }
            catch (WebDriverTimeoutException te)
            {
                throw new WebDriverTimeoutException($"Method WeFindElement can not find element. Web element locator: {by}. Timeout in seconds: {sec}. \n {te.Message}");
            }
        }

        /// <summary>
        ///    Finds elements with web element in the DOM.
        /// </summary>
        /// <param name="element">
        ///    IWebElement element instance.
        /// </param>
        /// <param name="by">
        ///    Locator pointing to the web element to find.
        /// </param>
        /// <param name="driver">
        ///    IWebDriver driver instance.
        /// </param>
        /// <param name="sec">
        ///   Time to wait for driver to find the web element.
        ///   Default time is 10 seconds.
        /// </param>
        /// <returns>
        ///    IWebElement specified by locator.
        /// </returns>
        /// <exception cref="WebDriverTimeoutException">
        ///    Driver finding the web element timeouts after the specified time.
        /// </exception>
        public static IList<IWebElement> WeFindElements(this IWebElement element, IWebDriver driver, By by, int sec = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(sec));
                return wait.Until(drv =>
                {
                    try
                    {
                        element.WeHighlightElement(driver);
                        return element.FindElements(by);

                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                }
                );
            }
            catch (WebDriverTimeoutException te)
            {
                throw new WebDriverTimeoutException($"Method WeFindElements can not find element. Web element locator: {by}. Timeout in seconds: {sec}. \n {te.Message}");
            }
        }

        /// <summary>
        ///    Gets web element attribute value. 
        /// </summary>
        /// <param name="element">
        ///    IWebElement element instance.
        /// </param>
        /// <param name="driver">
        ///    IWebDriver driver instance.
        /// </param>
        /// <param name="propertyName">
        ///    Name of the web element property.
        /// </param>
        /// <returns>
        ///    Resturn web element property value as a string.
        /// </returns>
        public static string WeGetAttributeValue(this IWebElement element, IWebDriver driver, string propertyName)
        {
            element.WeHighlightElement(driver);
            return element.GetAttribute(propertyName);
        }
    }
}