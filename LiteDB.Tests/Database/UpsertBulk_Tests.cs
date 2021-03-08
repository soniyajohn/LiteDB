using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LiteDB.Tests.Database
{
    [TestClass]
    public class UpsertBulk_Tests
    {
        #region Model
        public class Person
        {
            [BsonId]
            public string Id { get; set; }
            public string Fullname { get; set; }
        }
        #endregion

        [TestMethod]
        public void UpsertBulk()
        {
            using (var f = new TempFile())
            {
                var persons = new List<Person>()
                {
                    new Person { Id="1", Fullname = "User1" },
                    new Person { Id="2",Fullname = "User2" },
                    new Person { Id="3",Fullname = "User3" },
                    new Person { Id="4",Fullname = "User4" },
                    new Person { Id="5",Fullname = "User5" },
                    new Person { Id="6",Fullname = "User6" },
                    new Person { Id="7",Fullname = "User7" },
                    new Person { Id="8",Fullname = "User8" },
                    new Person { Id="9",Fullname = "User9" },
                    new Person { Id="10",Fullname = "User10" },
                    new Person { Id="11",Fullname = "User11" },
                    new Person { Id="12",Fullname = "User12" },
                    new Person { Id="13",Fullname = "User13" },
                    new Person { Id="14",Fullname = "User14" },
                    new Person { Id="15",Fullname = "User15" },
                    new Person { Id="16",Fullname = "User16" },
                    new Person { Id="17",Fullname = "User17" },
                    new Person { Id="18",Fullname = "User18" },
                    new Person { Id="19",Fullname = "User19" },
                    new Person { Id="20",Fullname = "User20" },
                    new Person { Id="21",Fullname = "User21" },
                    new Person { Id="22",Fullname = "User22" },
                    new Person { Id="23",Fullname = "User23" },
                    new Person { Id="24",Fullname = "User24" },
                    new Person { Id="25",Fullname = "U2ser25" },
                    new Person { Id="26",Fullname = "User26" },
                    new Person { Id="27",Fullname = "User27" },
                    new Person { Id="28",Fullname = "User28" },
                    new Person { Id="29",Fullname = "User29" },
                    new Person { Id="30",Fullname = "User30" },
                    new Person { Id="31",Fullname = "User31" },
                    new Person { Id="32",Fullname = "User32" },
                    new Person { Id="33",Fullname = "User33" },
                    new Person { Id="34",Fullname = "User34" },
                    new Person { Id="35",Fullname = "User35" },
                    new Person { Id="36",Fullname = "User36" },
                    new Person { Id="37",Fullname = "User37" },
                    new Person { Id="38",Fullname = "User38" },
                    new Person { Id="39",Fullname = "User39" },
                    new Person { Id="40",Fullname = "User40" },
                    new Person { Id="41",Fullname = "User41" },
                    new Person { Id="42",Fullname = "User42" },
                    new Person { Id="43",Fullname = "User43" },
                    new Person { Id="44",Fullname = "User44" },
                    new Person { Id="45",Fullname = "User45" },
                    new Person { Id="46",Fullname = "User46" },
                    new Person { Id="47",Fullname = "User47" },
                    new Person { Id="48",Fullname = "User48" },
                    new Person { Id="49",Fullname = "User49" },
                    new Person { Id="50",Fullname = "User50" },
                    new Person { Id="51",Fullname = "User51" },
                    new Person { Id="52",Fullname = "User52" },
                    new Person { Id="53",Fullname = "User53" },
                    new Person { Id="54",Fullname = "User54" },
                    new Person { Id="55",Fullname = "User55" },
                    new Person { Id="56",Fullname = "User56" },
                    new Person { Id="57",Fullname = "User57" },
                    new Person { Id="58",Fullname = "User58" },
                    new Person { Id="59",Fullname = "User59" },
                    new Person { Id="60",Fullname = "User60" },
                    new Person { Id="61",Fullname = "User61" },
                    new Person { Id="62",Fullname = "User62" },
                    new Person { Id="63",Fullname = "User63" },
                    new Person { Id="64",Fullname = "User64" },
                    new Person { Id="65",Fullname = "User65" },
                    new Person { Id="66",Fullname = "User66" },
                    new Person { Id="67",Fullname = "User67" },
                    new Person { Id="68", Fullname= "User68" },
                    new Person { Id="69",Fullname = "User69" },
                    new Person { Id="70", Fullname = "User70" },
                    new Person { Id="71",Fullname = "User71" },
                    new Person { Id="72",Fullname = "User72" },
                    new Person { Id="73",Fullname = "User73" },
                    new Person { Id="74",Fullname = "User74" },
                    new Person { Id="75",Fullname = "User75" },
                    new Person { Id="76",Fullname = "User76" },
                    new Person { Id="77",Fullname = "User77" },
                    new Person { Id="78",Fullname = "User78" },
                    new Person { Id="79",Fullname = "User79" },
                    new Person { Id="80",Fullname = "User80" },
                    new Person { Id="81",Fullname = "User81" },
                    new Person { Id="82",Fullname = "User82" },
                    new Person { Id="83",Fullname = "User83" },
                    new Person { Id="84",Fullname = "User84" },
                    new Person { Id="85",Fullname = "User85" },
                    new Person { Id="86",Fullname = "User86" },
                    new Person { Id="87",Fullname = "User87" },
                    new Person { Id="88",Fullname = "User88" },
                    new Person { Id="89",Fullname = "User89" },
                    new Person { Id="90",Fullname = "User90" },
                    new Person { Id="91",Fullname = "User91" },
                    new Person { Id="92",Fullname = "User92" },
                    new Person { Id="93",Fullname = "User93" },
                    new Person { Id="94",Fullname = "User94" },
                    new Person { Id="95",Fullname = "User95" },
                    new Person { Id="96",Fullname = "User96" },
                    new Person { Id="97",Fullname = "User97" },
                    new Person { Id="98",Fullname = "User98" },
                    new Person { Id="99",Fullname = "User99" },
                    new Person { Id="100",Fullname = "User100" },
                    new Person {Id="101",Fullname = "User101"},
                    new Person { Id="102",Fullname = "User102" },
                    new Person { Id="103",Fullname = "User103" },
                    new Person { Id="104",Fullname = "User104" },
                    new Person { Id="105",Fullname = "User105" },
                    new Person { Id="106",Fullname = "User106" },
                    new Person { Id="107",Fullname = "User107" },
                    new Person { Id="108",Fullname = "User108" },
                    new Person { Id="109",Fullname = "User109" },
                    new Person { Id="110",Fullname = "User110" }
                };

                using (var db = new LiteDatabase(f.Filename))
                {
                    var p = db.GetCollection<Person>("Person").UpsertBulk(persons, 100);
                    Assert.AreEqual(110, p);
                }
            }
        }
    }
}

