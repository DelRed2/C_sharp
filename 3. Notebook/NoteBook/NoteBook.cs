using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace NoteBook {
    public class NoteBook {
        public class PearsonInfo {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }
            public DateTime? BirthDay { get; set; }
            [XmlElement("phone")]
            public List<string> Phones { get; set; }
            [XmlElement("email")]
            public List<string> Emails { get; set; }
            public string Info { get; set; }

            public PearsonInfo() {}

            public PearsonInfo(string name, string surname = null, string patronymic = null, DateTime? birthDay = null,
                List<string> phones = null, List<string> emails = null, string info = null) {
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
                BirthDay = birthDay;
                Phones = phones;
                Emails = emails;
                Info = info;
            }

        }
        public class PearsonRecord {
            [XmlElement("Pearson")]
            public Pearson Key;
            [XmlElement("PearsonInfo")]
            public PearsonInfo Value;
            public PearsonRecord() {}
            public PearsonRecord(Pearson key, PearsonInfo value) {
                Key = key;
                Value = value;
            }
        }
        public class Pearson: IComparable<Pearson> {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }

            public Pearson() {}

            public Pearson(string name, string surname = null, string patronymic = null) {
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
            }

            public int CompareTo(Pearson other) {
                return String.Compare(Name + " " + Surname + " " + Patronymic,
                    other.Name + " " + other.Surname + " " + other.Patronymic);
            }
        }

        private readonly SortedDictionary<Pearson, PearsonInfo> book;
        //private readonly SortedDictionary<string, PearsonInfo> phoneBook;

        public NoteBook() {
            book = new SortedDictionary<Pearson, PearsonInfo>();
            //phoneBook = new SortedDictionary<string, PearsonInfo>();
        }

        public void SaveToXml(string filePath) {
            var tmp = new List<PearsonRecord>(book.Count);
            foreach (var pearson in book) {
                tmp.Add(new PearsonRecord(pearson.Key, pearson.Value));
            }

            using (var output = new StreamWriter(filePath)) {
                var xs = new XmlSerializer(tmp.GetType());
                xs.Serialize(output, tmp);
            }
        }

        public void LoadFromXml(string filePath) {
            var tmp = new List<PearsonRecord>();
            using (StreamReader sr = new StreamReader(filePath)) {
                XmlSerializer ser = new XmlSerializer(tmp.GetType());
                tmp = (List<PearsonRecord>) ser.Deserialize(sr);
            }

            book.Clear();

            foreach (var pearson in tmp) {
                book.Add(pearson.Key, pearson.Value);
            }

        }


        public static bool CheckEmail(string email) {
            const string expr = @"(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})";
            return Regex.IsMatch(email, expr, RegexOptions.IgnoreCase);
        }

        public static bool CheckPhone(string phone) {
            const string expr = @"^([+]?\d)?\d{10}";
            return Regex.Match(phone, expr, RegexOptions.IgnoreCase).Success;
        }

        public bool Add(PearsonInfo pi) {
            return Add(pi.Name, pi.Surname, pi.Patronymic, pi.BirthDay, pi.Phones, pi.Emails, pi.Info);
        }

        public bool Add(string name, string surname = null, string patronymic = null, DateTime? birthDay = null,
            List<string> phones = null, List<string> emails = null, string info = null) 
        {
            var prsInf = new PearsonInfo(name, surname, patronymic, birthDay, phones, emails, info);
            var tpl = new Pearson(name, surname, patronymic);

            if (book.ContainsKey(tpl)) return false;

            if (emails != null) {
                foreach (var mail in emails) {
                    if (!CheckEmail(mail)) return false;
                }
            }

            if (phones != null) {
                foreach (var phNumber in phones) {
                    if (!CheckPhone(phNumber)) return false;
                }
                /*foreach (var phNumber in phones) {
                    phoneBook.Add(phNumber, prsInf);
                }*/
            }
            book.Add(tpl, prsInf);

            return true;
        }

        public void Remove(Pearson p) {
            Remove(p.Name, p.Surname, p.Patronymic);
        }

        public void Remove(string name, string surname = null, string patronymic = null) {
            var tpl = new Pearson(name, surname, patronymic);
            PearsonInfo prsInf;

            if (!book.TryGetValue(tpl, out prsInf)) return;

            /*if (prsInf.Phones != null) {
                foreach (var phNumber in prsInf.Phones) {
                    phoneBook.Remove(phNumber);
                }
            }*/
            book.Remove(tpl);
        }

        private static void PrintPearsonInfo(PearsonInfo pearson) {
            Console.WriteLine(pearson.Name + " " + pearson.Surname + " " + pearson.Patronymic);
            if (pearson.BirthDay != null) {
                DateTime dt = pearson.BirthDay.Value;
                DateTimeFormatInfo dfi = new DateTimeFormatInfo();
                string format = String.Format("{0:dd} " + dfi.GetMonthName(dt.Month), dt);
                Console.WriteLine("\t" + format + (dt.Year != 1900 ? " " + dt.Year : ""));
            }
            if (pearson.Info != null) Console.WriteLine("\t" + pearson.Info);
            if (pearson.Emails != null) {
                foreach (var email in pearson.Emails) {
                    Console.WriteLine("\t" + email);
                }
            }
            if (pearson.Phones != null) {
                foreach (var phone in pearson.Phones) {
                    Console.WriteLine("\t" + phone);
                }
            }
        }

        /*public void PrintBook() {
            foreach (var pearson in book) {
                PrintPearsonInfo(pearson.Value);
                Console.WriteLine();
            }
        }*/

        private static void PrintPearsonsList(IEnumerable<PearsonInfo> tmp) {
            List<PearsonInfo> lst = tmp.ToList();
            foreach (var pearson in lst) {
                PrintPearsonInfo(pearson);
                Console.WriteLine();
            }
        }

        public void PrintBook() {
            var tmp = from pearson in book.Values 
                      orderby pearson.Surname orderby pearson.Name orderby pearson.Patronymic
                      select pearson;

            PrintPearsonsList(tmp);
        }

        public void PrintPearsonsWithLetter(Char c) {
            var tmp = from pearson in book
                      where Char.ToLower(pearson.Value.Surname[0]).Equals(Char.ToLower(c))
                      select pearson.Value;

            PrintPearsonsList(tmp);
        }

        public void PrintPearsonsWithPhone(string phone) {
            var tmp = from pearson in book.Values
                      where pearson.Phones.Any(ph => ph.Contains(phone))
                      select pearson;

            PrintPearsonsList(tmp);
        }

        public void PrintNearestBirthDates(int daysDelta = 31) {
            DateTime now = DateTime.Now;

            foreach (var pearson in book.Values) {
                if (pearson.BirthDay != null) {
                    int deltaYear = now.Year - pearson.BirthDay.Value.Year;
                    bool was = DateTime.Compare(pearson.BirthDay.Value.AddYears(deltaYear), now) < 0;
                    if (was && DateTime.Compare(pearson.BirthDay.Value.AddYears(deltaYear + 1), now.AddDays(daysDelta)) < 0 
                        || !was && DateTime.Compare(pearson.BirthDay.Value.AddYears(deltaYear), now.AddDays(daysDelta)) < 0) {
                        Console.WriteLine(pearson.Name + " " + pearson.Surname + " " + pearson.Patronymic);
                        DateTime dt = pearson.BirthDay.Value;
                        DateTimeFormatInfo dfi = new DateTimeFormatInfo();
                        string format = String.Format("{0:dd} " + dfi.GetMonthName(dt.Month), dt);
                        Console.WriteLine("\t" + format + (dt.Year != 1900 ? " " + dt.Year : ""));

                    }
                }
            }
            
        }

        public void PrintPearsonsInfo(Pearson p) {
            PrintPearsonsInfo(p.Name, p.Surname, p.Patronymic);
        }

        public void PrintPearsonsInfo(String name, String surname = null, String patronymic = null) {
            var tmp = from pearson in book.Values
                      where pearson.Name.Contains(name)
                      && ((surname != null && pearson.Surname.Contains(surname)) || surname == null)
                      && ((patronymic != null && pearson.Patronymic.Contains(patronymic)) || patronymic == null)
                      select pearson;

            PrintPearsonsList(tmp);
        }

    }

}
