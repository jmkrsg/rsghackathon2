﻿using IO.Swagger.Model;
using ReisekostenNative.DAO;
using ReisekostenNative.Models;
using ReisekostenNative.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReisekostenNative.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //DBTest();
            foo();

            Console.ReadLine();
        }

        private async static void foo ()
        {

            UIService.Instance.GetBelege("hugo", null);

            ReisekostenNative.RESTClient.RESTClient test = new ReisekostenNative.RESTClient.RESTClient();
            List<string> tmp = await test.GetTypesAsync();
            List<string> tmp2 = await test.GetStatiAsync();
            Beleg testBeleg = new Beleg();
            //testBeleg.BelegSize = 10;
            //testBeleg.Betrag = 11;
            testBeleg.Description = "servus";
            testBeleg.Status = Beleg.StatusEnum.ERFASST;
            //testBeleg.Belegnummer = 12;
            //testBeleg.Thumbnail = null;
            testBeleg.Type = "Hotel";
            testBeleg.Date = DateTime.UtcNow;
            int tmp4 = await test.CreateBeleg("hugo", testBeleg);
            List<Beleg> tmp3n = await test.GetBelegeByUserAsync("hugo");
            Beleg b = tmp3n.First();
            test.UpdateImage("hugo", b.Belegnummer.Value, new byte[] { 232, 23, 54, 87 });
            b.Description = "put-test";
            test.UpdateBeleg("hugo", b.Belegnummer.Value, b);
            tmp3n = await test.GetBelegeByUserAsync("hugo");
        }

        public static void DBTest()
        {
            Beleg b = new Beleg(1, "test", DateTime.Now, "Hotel", 100, Beleg.StatusEnum.ERFASST);
            var id = BelegDAO.Instance.StoreBeleg(b).Result;
            var loaded = BelegDAO.Instance.GetBeleg(id).Result;

            BelegArt ba = new BelegArt("Hotel");
            var baID = BelegArtenDAO.Instance.StoreBelegArt(ba);
            var baL = BelegArtenDAO.Instance.GetBelegArt("Hotel").Result.Name;

        }
    }
}
