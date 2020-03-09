using HalFiyatlari.Schedule.Models;
using HalFiyatlari.Schedule.Models.WebSiteModel;
using HtmlAgilityPack;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HalFiyatlari.Schedule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        //KİLOGRAM
        //    ADET
        //    KUTU
        //    KASA
        //    BAG
        //    BAĞ
        //    DEMET


        public string GenerateUnit(string input)
        {
            switch (input)
            {
                case "KİLOGRAM":
                case "KILOGRAM":
                case "KG":
                    return "KG";
                case "ADET":
                    return "AD";
                case "BAĞ":
                case "BAG":
                    return "BAG";
                default:
                    return input.ToUpper();
            }
        }
        public string GenerateProductCategory(string input)
        {
            switch (input)
            {
                case "İTHAL..":
                    return "ITHAL ÜRÜNLER";
                default:
                    return input.ToUpper();
            }
        }
        private void BtnIstanbulBelediyesi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WebSiteHalData> listHalData = new List<WebSiteHalData>();
                for (int p = 5; p < 8; p++)
                {
                    var client = new RestClient("http://gida.ibb.istanbul/hal-mudurlugu/hal-fiyatlari.html");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddParameter("application/x-www-form-urlencoded", "tarih=" + DateTime.Now.ToString("yyyy-MM-dd") + "&Kategori=" + p + "&send00=1", ParameterType.RequestBody);//KategoriId00 5 Meyve 6 Sebze 7 İhtal Ürünler
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = response.Content;
                        HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                        document.LoadHtml(content); // html kodlarını bir HtmlDocment nesnesine yüklüyoruz.
                        HtmlNodeCollection trList = document.DocumentNode.SelectNodes("//table//tr"); // a etiketlerinin içerisinden class haberbas olanları seçiyoruz.
                        foreach (var trItem in trList.Skip(1))
                        {
                            HtmlNodeCollection tdList = trItem.SelectNodes("//td");
                            var i = 0;
                            WebSiteHalData dataItem = new WebSiteHalData();
                            foreach (var tdItem in tdList)
                            {
                                switch (i)
                                {
                                    case 0://Ürün adı
                                        dataItem.ProductName = tdItem.InnerText.Trim();
                                        break;
                                    case 1://Birimi
                                        dataItem.Unit = GenerateUnit(tdItem.InnerText.Trim().ToUpper());
                                        break;
                                    case 2://en düşük fiyat
                                        dataItem.MinPrice = Double.Parse(tdItem.InnerText.Trim().Replace(",", ".").Replace("TL", ""), CultureInfo.InvariantCulture);
                                        break;
                                    case 3://En yüksek fiyat
                                        dataItem.MaxPrice = Double.Parse(tdItem.InnerText.Trim().Replace(",", ".").Replace("TL", ""), CultureInfo.InvariantCulture);
                                        break;

                                }


                                if (i == 3)
                                {
                                    dataItem.Currency = "TL";
                                    dataItem.CustomerId = 1;
                                    switch (p)
                                    {
                                        case 5:
                                            dataItem.ProductCategory = "MEYVE";
                                            break;
                                        case 6:
                                            dataItem.ProductCategory = "SEBZE";
                                            break;
                                        case 7:
                                            dataItem.ProductCategory = "İTHAL ÜRÜNLER";
                                            break;
                                    }
                                    listHalData.Add(dataItem);
                                    dataItem = new WebSiteHalData();
                                    i = 0;
                                }
                                else
                                    i++;
                            }
                        }

                    }


                }
                addListLog("IBB Data Okuma Bitti=>" + listHalData.Count());
                Product.InsertProduct(listHalData);
            }
            catch (Exception ex)
            {

                addListLog("IBB Error=>" + ex.Message);
            }
        }

        private void BtnIzmirBelediyesi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WebSiteHalData> listHalData = new List<WebSiteHalData>();
                var client = new RestClient("http://eislem.izmir.bel.tr/halfiyatlari.aspx");
                var request = new RestRequest(Method.POST);
                request.AddHeader("postman-token", "efbb6658-fc42-2a47-e316-27d7c0aa814e");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
                request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"dropYil\"\r\n\r\n2019\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"dropAy\"\r\n\r\n12\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"dropGun\"\r\n\r\n19\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = response.Content;
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(content); // html kodlarını bir HtmlDocment nesnesine yüklüyoruz.
                    HtmlNodeCollection trList = document.DocumentNode.SelectNodes("//table[@class='beyan line-hover']//tr//td");
                    var i = 0;
                    WebSiteHalData dataItem = new WebSiteHalData();
                    foreach (var trItem in trList)
                    {
                        switch (i)
                        {
                            case 0://Kategori
                                dataItem.ProductCategory = GenerateProductCategory(trItem.InnerText.Trim());
                                break;
                            case 1://isim
                                dataItem.ProductName = trItem.InnerText.Trim();
                                break;
                            case 2://Birim
                                dataItem.Unit = GenerateUnit(trItem.InnerText.Trim().ToUpper());
                                break;
                            case 3:

                                dataItem.MinPrice = Double.Parse(trItem.InnerText.Trim().Replace(",", ".").Replace("TL", ""), CultureInfo.InvariantCulture);
                                break;
                            case 4:
                                dataItem.MaxPrice = Double.Parse(trItem.InnerText.Trim().Replace(",", ".").Replace("TL", ""), CultureInfo.InvariantCulture);
                                break;
                        }

                        if (i == 4)
                        {
                            dataItem.Currency = "TL";
                            dataItem.CustomerId = 2;
                            listHalData.Add(dataItem);
                            dataItem = new WebSiteHalData();
                            i = 0;

                        }
                        else
                            i++;
                    }
                    addListLog("Izmır Data Okuma Bitti=>" + listHalData.Count());
                    Product.InsertProduct(listHalData);
                }
            }
            catch (Exception ex)
            {

                addListLog("Izmır Erro=>" + ex.Message);
            }
        }

        private void BtnAntalyaBelediyesi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new RestClient("http://www.antalya.bel.tr/halden-gunluk-fiyatlar");
                var request = new RestRequest(Method.POST);
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(content); // html kodlarını bir HtmlDocment nesnesine yüklüyoruz.
                HtmlNodeCollection trList = document.DocumentNode.SelectNodes("//table//tr//td");
                var fileName = System.AppDomain.CurrentDomain.BaseDirectory + "\\"+ "Ant-" + DateTime.Now.ToShortDateString()+".pdf";
                if(trList.Count>=2)
                {
                    var link = trList[1].InnerHtml.ToString();
                    var doc = new HtmlDocument();
                    doc.LoadHtml(link);
                    var nodes = doc.DocumentNode.SelectNodes("a[@href]").FirstOrDefault().Attributes.FirstOrDefault().Value;
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(nodes, fileName);

                }

                List<WebSiteHalData> listHalData = new List<WebSiteHalData>();
                var data = parseUsingPDFBox(fileName);
                data = data.Replace(@"ÜRÜNLER#BİRİMİ^ORTALAMA EN ^DÜŞÜK FİYAT ^TEKLİFİ^ORTALAMA EN ^YÜKSEK FİYAT ^TEKLİFİ^ÜRÜNLER#BİRİMİ#,^ORTALAMA EN ^YÜKSEK FİYAT ^TEKLİFİ^", "");
                data = data.Replace(@"^ANTALYA BÜYÜKŞEHİR BELEDİYESİ MERKEZ TOPTANCI HAL ESNAFINDAN ALINAN FİYATLARA GÖRE OLUŞTURULAN PİYASA FİYAT LİSTESİDİR. ^BİLGİLENDİRME AMAÇLI OLUP, RESMİ EVRAK NİTELİĞİ TAŞIMAZ.^             ANTALYA BÜYÜKŞEHİR BELEDİYESİ MERKEZ HAL İŞLENMEMİŞ ÜRÜN FİYAT ^LİSTESİ^" + DateTime.Now.ToString("dd.MM.yyyy"), "");
                foreach (var lineItem in data.Split('^'))
                {
                    int i = 0;
                    WebSiteHalData dataItem = new WebSiteHalData();
                    var orjLineItem = lineItem.Trim().Replace(" ", "*").Replace("****************", "#");

                    foreach (var rowItem in orjLineItem.Split('#'))
                    {
                        switch (i)
                        {
                            case 0:
                                dataItem.ProductName = rowItem.Replace("*", " ").Trim();
                                break;
                            case 1:
                                dataItem.Unit = GenerateUnit(rowItem.Trim().ToUpper());
                                break;
                            case 2:
                                dataItem.MinPrice = Double.Parse(rowItem.Trim().Replace(",", ".").Replace("TL", ""), CultureInfo.InvariantCulture);
                                break;
                            case 3:
                                dataItem.MaxPrice = Double.Parse(rowItem.Trim().Replace(",", ".").Replace("TL", ""), CultureInfo.InvariantCulture);
                                break;

                        }
                        i++;
                    }
                    if (dataItem.MaxPrice == 0)
                        dataItem.MaxPrice = dataItem.MinPrice;
                    dataItem.ProductCategory = "Sebze";
                    dataItem.CustomerId = 3;

                    dataItem.Currency = "TL";
                    listHalData.Add(dataItem);


                }
                addListLog("Antalya Data Okuma Bitti=>" + listHalData.Count());
                Product.InsertProduct(listHalData);
            }
            catch (Exception ex)
            {
                addListLog("Antalya Hata=>" + ex.Message);
              
            }
        }

        private  string parseUsingPDFBox(string filePath)
        {
            try
            {
                PDDocument doc = null;

                try
                {

                    doc = PDDocument.load(filePath);
                    PDFTextStripper stripper = new PDFTextStripper();
                    stripper.setLineSeparator("^");
                    stripper.setWordSeparator("#");
                    return stripper.getText(doc);
                }
                finally
                {
                    if (doc != null)
                    {
                        doc.close();
                    }
                }
            }
            catch (Exception ex)
            {

                addListLog("PDF OKUMA HATA=>" + ex.Message);
            }
            return null;
        }

        private void BtnAnkaraBelediyesi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WebSiteHalData> listHalData = new List<WebSiteHalData>();
                var client = new RestClient("https://www.ankara.bel.tr/index.php/tools/blocks/fruit_vegetable_fish_market/hal_balik_tablo.php?tip=hal&bitis=" + DateTime.Now.ToString("dd/MM/yyyy") + "&baslangic=" + DateTime.Now.ToString("dd/MM/yyyy") + "&isMobile=false");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                IRestResponse response = client.Execute(request);
                RestSharp.Serialization.Json.JsonSerializer s = new RestSharp.Serialization.Json.JsonSerializer();
                var result = s.Deserialize<AnkaraBBHalModel>(response);

                foreach (var item in result.aaData)
                {
                    WebSiteHalData dataItem = new WebSiteHalData();
                    dataItem.ProductName = item[1].ToString();
                    dataItem.Unit = GenerateUnit(item[2].ToString().ToUpper());
                    dataItem.MinPrice = Double.Parse(item[3].Trim().Replace(",", ".").Replace("TL", ""), CultureInfo.InvariantCulture);
                    dataItem.MaxPrice = Double.Parse(item[4].Trim().Replace(",", ".").Replace("TL", ""), CultureInfo.InvariantCulture);
                    dataItem.ProductCategory = "Sebze";
                    dataItem.Currency = "TL";
                    dataItem.CustomerId = 4;
                    listHalData.Add(dataItem);
                }
                addListLog("Ankara Data Okuma Bitti=>" + listHalData.Count());
                Product.InsertProduct(listHalData);
            }
            catch (Exception ex)
            {

                addListLog("Ankara BB HATA=>" + ex.Message);
            }
        }

        private void BtnYalovaBelediyesi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WebSiteHalData> listHalData = new List<WebSiteHalData>();
                var client = new RestClient("https://ebelediye.yalova.bel.tr/HalFiyatSorgulama.aspx");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                client.Encoding = Encoding.UTF8;
                IRestResponse response = client.Execute(request);

                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(response.Content); // html kodlarını bir HtmlDocment nesnesine yüklüyoruz.
                HtmlNodeCollection tdList = document.DocumentNode.SelectNodes("//table[@class='gridStyle']//tr[@class='gridRowStyle']//td");
                int i = 0;
                bool dateCheck = false;
                WebSiteHalData dataItem = new WebSiteHalData();
                foreach (var tdItem in tdList)
                {
                    if (i == 0 && tdItem.InnerText.Trim() == DateTime.Now.ToString("dd.MM.yyyy"))
                    {
                        dateCheck = true;
                    }
                    if (dateCheck)
                    {
                        switch (i)
                        {

                            case 1://Min Fiyat
                                dataItem.MinPrice = Double.Parse(tdItem.InnerText.Trim().Replace(",", ".").Replace("TL", ""), CultureInfo.InvariantCulture);
                                break;
                            case 2://Max Fiyat
                                dataItem.MaxPrice = Double.Parse(tdItem.InnerText.Trim().Replace(",", ".").Replace("TL", ""), CultureInfo.InvariantCulture);
                                break;
                            case 3://Ürün Adı
                                dataItem.ProductName = tdItem.InnerText.Trim();
                                if (dataItem.ProductName.Contains("(") && dataItem.ProductName.Contains(")"))
                                    dataItem.Unit = GenerateUnit(dataItem.ProductName.Substring(dataItem.ProductName.IndexOf("(") + 1, (dataItem.ProductName.IndexOf(")") - dataItem.ProductName.IndexOf("(")) - 1));
                                else
                                    dataItem.Unit = "KG";
                                break;
                        }
                        if (i == 3)
                        {
                            dataItem.Currency = "TL";
                            listHalData.Add(dataItem);
                            dataItem = new WebSiteHalData();
                            i = 0;
                            dateCheck = false;

                        }
                        else
                            i++;

                    }

                }
                addListLog("Yalova Data Okuma Bitti=>" + listHalData.Count());
                Product.InsertProduct(listHalData);
            }
            catch (Exception ex)
            {
                addListLog("Yalova BB HATA=>" + ex.Message);

            }
        }


        void addListLog(string Message)
        {
            lstLog.Items.Insert(0, Message);
            lstLog.Items.Refresh();
        }
    }
}
