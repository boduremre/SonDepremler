using SonDepremler.Utils;
using System.Text.RegularExpressions;

namespace SonDepremler.Models
{
    public class Deprem
    {
        public DateTime Tarih { get; set; }
        public Konum Konumu { get; set; }
        public Siddet Siddeti { get; set; }
        public string Yer { get; set; }
        public string CozumNiteligi { get; set; }
        private List<string> CrawledNodes { get; set; }

        public Deprem()
        {          
        }

        public void GetData(int Take)
        {
            CrawlUtils crawl = new CrawlUtils("http://www.koeri.boun.edu.tr/scripts/lst8.asp");
            var temp = crawl.CrawlNodes("/html/body/pre").FirstOrDefault().InnerHtml;
            CrawledNodes = temp.Split('\n').Skip(7).Take(Take).ToList();
        }

        public List<Deprem> Get(int DateDiff = 0)
        {
            List<Deprem> result = new List<Deprem>();

            foreach (var item in CrawledNodes)
            {
                string[] temp = Regex.Replace(item.Trim(), @"\s+", " ").Split(' ');
                DateTime t = Convert.ToDateTime(temp[0] + " " + temp[1]);
                bool control = true;
                switch (DateDiff)
                {
                    case 0: control = true; break;
                    case 1: control = (t.ToShortDateString() == DateTime.Today.ToShortDateString()); break;
                    case 2: control = (t.Month == DateTime.Today.Month); break;
                    default:
                        control = false;
                        break;
                }
                if (control)
                {
                    double e = 0, b = 0, d = 0, md = 0, ml = 0, mw = 0;

                    Double.TryParse(temp[2].Replace(".", ","), out e);
                    Double.TryParse(temp[3].Replace(".", ","), out b);
                    Double.TryParse(temp[4].Replace(".", ","), out d);
                    Double.TryParse(temp[5].Replace(".", ","), out md);
                    Double.TryParse(temp[6].Replace(".", ","), out ml);
                    Double.TryParse(temp[7].Replace(".", ","), out mw);
                    string y = "";
                    string n = "";
                    if (item.Contains("REVIZE"))
                    {
                        y = String.Join(" ", temp.Skip(8).Take(temp.Length - 11));
                        n = "REVIZE";
                    }
                    else
                    {
                        y = String.Join(" ", temp.Skip(8).Take(temp.Length - 9));
                        n = "İLKSEL";
                    }

                    result.Add(new Deprem()
                    {
                        Tarih = t,
                        Konumu = new Konum()
                        {
                            Enlem = e,
                            Boylam = b,
                            Derinlik = d,
                        },
                        Siddeti = new Siddet()
                        {
                            MD = md,
                            ML = ml,
                            MW = mw,
                        },
                        Yer = y,
                        CozumNiteligi = n
                    });
                }
            }
            return result;
        }

    }
}