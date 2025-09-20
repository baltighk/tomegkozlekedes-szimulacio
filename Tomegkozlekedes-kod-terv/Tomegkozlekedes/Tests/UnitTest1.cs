using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Tomegkozlekedes;

namespace Tomegkozlekedes.Tests
{
    [TestFixture]
    public class ModelTests
    {
        List<Jarmu> jarmuvek;
        List<Szerviz> cegek;
        List<Munkalap> munkalapok;

        [SetUp]
        public void Setup()
        {
            jarmuvek = new List<Jarmu>();
            cegek = new List<Szerviz>();
            munkalapok = new List<Munkalap>();
            Program.Beolvas("bemenet.txt",jarmuvek, cegek, munkalapok);
        }

        
        [Test]
        public void Beolvasas_HelyesAdatok()
        {
            Assert.That(jarmuvek.Count, Is.EqualTo(3));
            Assert.That(cegek.Count, Is.EqualTo(2));
        }

        
        [TestCase("Belvaros", typeof(Belvaros))]
        [TestCase("Kulvaros", typeof(Kulvaros))]
        [TestCase("Vegyes", typeof(Vegyes))]
        public void GetOvezet_ValidStrings(string input, Type expectedType)
        {
            var ovezet = Ovezet.GetOvezet(input);
            Assert.That(ovezet, Is.InstanceOf(expectedType));
        }

        [Test]
        public void GetOvezet_InvalidString()
        {
            Assert.Throws<Exception>(() => Ovezet.GetOvezet("HibasOvezet"));
        }

        
        [Test]
        public void AktualisErtek_Helyes()
        {
            var villamos = new Villamos(1, 2010, new Belvaros(), 10000000);
            int ertek = villamos.AktualisErtek(2020);
            
            Assert.That(ertek, Is.EqualTo(9000000));
        }

        /*
        [Test]
        public void Villamos_Fajta_ReturnsVillamos()
        {
            var v = new Villamos(1, 2010, new Belvaros(), 10000000);
            Assert.That(v.Fajta(), Is.EqualTo(JarmuFajta.Villamos));
        }
        [Test]
        public void Autobusz_Fajta_ReturnsAutobusz()
        {
            var a = new Autobusz(2, 2005, new Kulvaros(), 8000000);
            Assert.That(a.Fajta(), Is.EqualTo(JarmuFajta.Autobusz));
        }
        */
        
        public static IEnumerable GetFaktorTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new Villamos(2, 2018, new Belvaros(), 1200000), new Belvaros()
                ).Returns(1.0);

                yield return new TestCaseData(
                    new Autobusz(1, 2020, new Vegyes(), 1000000), new Vegyes()
                ).Returns(2.5);

                yield return new TestCaseData(
                    new Trolibusz(3, 2015, new Kulvaros(), 1100000), new Kulvaros()
                ).Returns(3.1);
            }
        }

        [Test, TestCaseSource(nameof(GetFaktorTestCases))]
        public double GetFaktor_ValidInputs(Jarmu jarmu, Ovezet ovezet)
        {
            return jarmu.GetFaktor(ovezet);
        }

        [Test]
        public void GetFaktor_InvalidInput()
        {
            var jarmu = new Autobusz(1, 2020, new Belvaros(), 10000);
            
            Assert.Throws<Exception>(() => jarmu.GetFaktor(new FakeOvezet()));
        }

        
        [Test]
        public void LegdragabbJarmu_Helyes()
        {
            var legdragabb = Jarmu.LegdragabbJarmu(jarmuvek, 2025);
            Assert.That(legdragabb, Is.Not.Null);
            Assert.That(legdragabb.Azonosito, Is.EqualTo(3));
        }

        
        [Test]
        public void JavitasAlattiArany()
        {
            var villamos = new Villamos(20,2025,new Kulvaros(),12000000);
            jarmuvek.Add(villamos);
            var szerviz = cegek[0];
            var javitas = new Munkalap(villamos, szerviz, DateTime.Now, null, SzervizTipus.Javitas);
            villamos.Munkalapok.Add(javitas);
            

            double arany = Jarmu.JavitasAlattiArany(jarmuvek);
            Assert.That(arany, Is.GreaterThan(0));
        }

        
        [Test]
        public void Munkalap_Konstruktor()
        {
            var j = jarmuvek[0];
            var c = cegek[0];
            var m = new Munkalap(j, c, new DateTime(2024, 1, 1), new DateTime(2024, 1, 2), SzervizTipus.Javitas);
            Assert.That(m.Jarmu, Is.EqualTo(j));
            Assert.That(m.Szerviz, Is.EqualTo(c));
            Assert.That(m.Kezdete, Is.EqualTo(new DateTime(2024, 1, 1)));
            Assert.That(m.Vege, Is.EqualTo(new DateTime(2024, 1, 2)));
            Assert.That(m.Tipus, Is.EqualTo(SzervizTipus.Javitas));
        }

        
        [Test]
        public void Munkalap_OsszKoltseg()
        {
            var j = jarmuvek[0];
            var c = cegek[0];
            var m = new Munkalap(j, c, DateTime.Now, DateTime.Now, SzervizTipus.Javitas);
            m.Munkafolyamatok.Add(new Munkafolyamat("Teszt", 1000));
            m.Munkafolyamatok.Add(new Munkafolyamat("Teszt2", 2000));
            Assert.That(m.OsszKoltseg(), Is.EqualTo(3000));
        }

        
        [Test]
        public void Munkafolyamat_Konstruktor()
        {
            var mf = new Munkafolyamat("Teszt folyamat", 12345);
            Assert.That(mf.Megnevezes, Is.EqualTo("Teszt folyamat"));
            Assert.That(mf.Koltseg, Is.EqualTo(12345));
        }

        
        [Test]
        public void Szerviz_Konstruktor()
        {
            var s = new Szerviz("Teszt Szerviz");
            Assert.That(s.Nev, Is.EqualTo("Teszt Szerviz"));
        }

        
        [Test]
        public void EloregedettAutobuszokAranya()
        {
            var lista = new List<Jarmu>
            {
                new Autobusz(1, 2000, new Belvaros(), 1000000), // régi
                new Autobusz(2, 2010, new Belvaros(), 1000000), // újabb
                new Villamos(3, 2012, new Belvaros(), 1000000)  // nem busz
            };
            double arany = Autobusz.EloregedettAutobuszokAranya(lista, 2020);
            Assert.That(arany, Is.EqualTo(0.5).Within(0.01));
        }

        
        private class FakeOvezet : Ovezet
        {
            public override double GetValue(Jarmu jarmu) => throw new Exception("Hibás övezet!");
        }
    }
}
