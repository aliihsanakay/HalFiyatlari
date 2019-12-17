using HalFiyatlari.Schedule.Models;
using HtmlAgilityPack;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private void BtnIstanbulBelediyesi_Click(object sender, RoutedEventArgs e)
        {
            List<WebSiteHalData> listHalData = new List<WebSiteHalData>();
            for (int p = 5; p < 8; p++)
            {
                var client = new RestClient("http://gida.ibb.istanbul/hal-mudurlugu/hal-fiyatlari.html");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded", "tarih=" + DateTime.Now.ToString("yyyy-MM-dd") + "&KategoriId00=" + p + "&send00=1", ParameterType.RequestBody);//KategoriId00 5 Meyve 6 Sebze 7 İhtal Ürünler
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
                                    dataItem.Unit = tdItem.InnerText.Trim().ToUpper();
                                    break;
                                case 2://en düşük fiyat
                                    dataItem.MinPrice = Double.Parse(tdItem.InnerText.Trim().Replace(",", ".").Replace("TL", ""));
                                    break;
                                case 3://En yüksek fiyat
                                    dataItem.MaxPrice = Double.Parse(tdItem.InnerText.Trim().Replace(",", ".").Replace("TL", ""));
                                    break;

                            }


                            if (i == 3)
                            {
                                dataItem.Currency = "TL";
                                switch (p)
                                {
                                    case 5:
                                        dataItem.ProductCategory = "Meyve";
                                        break;
                                    case 6:
                                        dataItem.ProductCategory = "Sebze";
                                        break;
                                    case 7:
                                        dataItem.ProductCategory = "İthal Ürünler";
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

        }

        private void BtnIzmirBelediyesi_Click(object sender, RoutedEventArgs e)
        {
            List<WebSiteHalData> listHalData = new List<WebSiteHalData>();
            var client = new RestClient("http://eislem.izmir.bel.tr/halfiyatlari.aspx");
            var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "efbb6658-fc42-2a47-e316-27d7c0aa814e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"dropYil\"\r\n\r\n2019\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"dropAy\"\r\n\r\n12\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"dropGun\"\r\n\r\n17\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
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
                            dataItem.ProductCategory = trItem.InnerText.Trim();
                            break;
                        case 1://isim
                            dataItem.ProductName = trItem.InnerText.Trim();
                            break;
                        case 2://Birim
                            dataItem.Unit = trItem.InnerText.Trim().ToUpper();
                            break;
                        case 3:
                            dataItem.MinPrice = Double.Parse(trItem.InnerText.Trim().Replace(",", ".").Replace("TL", ""));
                            break;
                        case 4:
                            dataItem.MaxPrice = Double.Parse(trItem.InnerText.Trim().Replace(",", ".").Replace("TL", ""));
                            break;
                    }

                    if (i == 4)
                    {
                        dataItem.Currency = "TL";
                        listHalData.Add(dataItem);
                        dataItem = new WebSiteHalData();
                        i = 0;

                    }
                    else
                        i++;
                }
            }
        }

        private void BtnAntalyaBelediyesi_Click(object sender, RoutedEventArgs e)
        {
            List<WebSiteHalData> listHalData = new List<WebSiteHalData>();
            var data = parseUsingPDFBox(@"C:\New folder\fb457ec1-6bf1-4b3d-bc1e-eb5f2b4574b7.pdf");
            data = data.Replace(@"ÜRÜNLER#BİRİMİ^ORTALAMA EN ^DÜŞÜK FİYAT ^TEKLİFİ^ORTALAMA EN ^YÜKSEK FİYAT ^TEKLİFİ^ÜRÜNLER#BİRİMİ#,^ORTALAMA EN ^YÜKSEK FİYAT ^TEKLİFİ^", "");
            data = data.Replace(@"^ANTALYA BÜYÜKŞEHİR BELEDİYESİ MERKEZ TOPTANCI HAL ESNAFINDAN ALINAN FİYATLARA GÖRE OLUŞTURULAN PİYASA FİYAT LİSTESİDİR. ^BİLGİLENDİRME AMAÇLI OLUP, RESMİ EVRAK NİTELİĞİ TAŞIMAZ.^             ANTALYA BÜYÜKŞEHİR BELEDİYESİ MERKEZ HAL İŞLENMEMİŞ ÜRÜN FİYAT ^LİSTESİ^"+DateTime.Now.ToString("dd.MM.yyyy"), "");
            foreach (var lineItem in data.Split('^'))
            {
                int i = 0;
                WebSiteHalData dataItem = new WebSiteHalData();
               var orjLineItem = lineItem.Trim().Replace(" ", "*").Replace("****************","#");
              
                foreach (var rowItem in orjLineItem.Split('#'))
                {
                    switch (i)
                    {
                        case 0:
                            dataItem.ProductName = rowItem.Replace("*"," ").Trim();
                            break;
                        case 1:
                            dataItem.Unit = rowItem.Trim().ToUpper();
                            break;
                        case 2:                          
                            dataItem.MinPrice = Double.Parse(rowItem.Trim().Replace(",", ".").Replace("TL", ""));
                            break;
                        case 3:
                            dataItem.MaxPrice = Double.Parse(rowItem.Trim().Replace(",", ".").Replace("TL", ""));
                            break;

                    }
                    i++;
                }
                if (dataItem.MaxPrice == 0)
                    dataItem.MaxPrice = dataItem.MinPrice;
                dataItem.ProductCategory = "Sebze";
                dataItem.Currency = "TL";
                listHalData.Add(dataItem);
                   
               
            }
        }

        private static string parseUsingPDFBox(string filePath)
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
    }
}
