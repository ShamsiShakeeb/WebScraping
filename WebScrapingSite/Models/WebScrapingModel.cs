using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebScrapingSite.Models
{
    public class WebScrapingModel
    {
        [JsonProperty("id")]
        public int Id { set; get; }
        [JsonProperty("siteTitle")]
        public string SiteTitle { set; get; }
        [JsonProperty("loginLink")]
        public string LoginLink { set; get; }
        [JsonProperty("navigationLink")]
        public string NavigationLink { set; get; }
        [JsonProperty("uniqueTitle")]
        public string UniqueTitle { set; get; }
        [JsonProperty("queryCssSelector")]
        public string QueryCssSelector { set; get; }
        [JsonProperty("loginInfo")]
        public List<LoginInfo> LoginInfo { set; get; }
        [JsonProperty("elements")]
        public List<Elements> Elements { set; get; }
        [JsonProperty("actionButtonElements")]
        public List<ActionButtons> ActionButtons { set; get; }
        [JsonProperty("customProperties")]
        public List<CustomProperties> CustomProperties { set; get; }
    }
}
public class LoginInfo 
{
    [JsonProperty("htmlTag")]
    public string HtmlTag { set; get; }
    [JsonProperty("getElementBy")]
    public string GetElementBy { set; get; }
    [JsonProperty("key")]
    public string Key { set; get; }
    [JsonProperty("value")]
    public string Value { set; get; }
}
public class Elements 
{
    [JsonProperty("htmlTag")]
    public string HtmlTag { set; get; }
    [JsonProperty("getElementBy")]
    public string GetElementBy { set; get; }
    [JsonProperty("key")]
    public string Key { set; get; }
    [JsonProperty("value")]
    public string Value { set; get; }
}
public class ActionButtons 
{
    [JsonProperty("htmlTag")]
    public string HtmlTag { set; get; }
    [JsonProperty("getElementBy")]
    public string GetElementBy { set; get; }
    [JsonProperty("key")]
    public string Key { set; get; }
    [JsonProperty("value")]
    public string Value { set; get; }
}
public class CustomProperties
{
    [JsonProperty("htmlTag")]
    public string HtmlTag { set; get; }
    [JsonProperty("getElementBy")]
    public string GetElementBy { set; get; }
    [JsonProperty("key")]
    public string Key { set; get; }
    [JsonProperty("value")]
    public string Value { set; get; }
}
