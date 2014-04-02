using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace NoteBook {
    class Program {

        static void PrintHelp() {
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Load PhoneBook");
            Console.WriteLine("2. Save PhoneBook");
            Console.WriteLine("3. Add record");
            Console.WriteLine("4. Delete record");
            Console.WriteLine("5. Find pearson");
            Console.WriteLine("6. Find phone");
            Console.WriteLine("7. Print phonebook");
            Console.WriteLine("8. Print pearsons with letter");
            Console.WriteLine("9. Print nearest birthdays");
            Console.WriteLine();
        }

        static void Main(string[] args) {
            NoteBook book = new NoteBook();
            int input = -1;

            PrintHelp();

            while (input != 0) {
                Console.Write("Input: ");
                if (int.TryParse(Console.ReadLine() ?? "-1", out input)) {
                } else input = -1;

                switch (input) {
                    case 0: {
                        break;
                    }

                    case 1: {
                        book.LoadFromXml("book.xml");
                        break;
                    }

                    case 2: {
                        book.SaveToXml("book.xml");
                        break;
                    }

                    case 3: {
                        var pi = new NoteBook.PearsonInfo();

                        Console.Write("Input Name*: ");
                        pi.Name = Console.ReadLine();

                        Console.Write("Input Surname: ");
                        pi.Surname = Console.ReadLine();

                        Console.Write("Input Patronymic: ");
                        pi.Patronymic = Console.ReadLine();

                        Console.Write("Input Birthday: ");
                        string str = Console.ReadLine();
                        if (!string.IsNullOrEmpty(str)) {
                            string[] bdc = str.Split('.');
                            if (bdc.Count() == 2) {
                                pi.BirthDay = new DateTime(1900, int.Parse(bdc[1]), int.Parse(bdc[0]));
                            } else if (bdc.Count() == 3) {
                                pi.BirthDay = new DateTime(int.Parse(bdc[2]), int.Parse(bdc[1]), int.Parse(bdc[0]));
                            } else {
                                Console.WriteLine("Wrong Date");
                                break;
                            }
                        }

                        Console.WriteLine("Input Phones: ");
                        pi.Phones = new List<string>();
                        do {
                            str = Console.ReadLine();
                            if (NoteBook.CheckPhone(str)) pi.Phones.Add(str);
                            else if (str != "") Console.WriteLine("Bad phone");
                        } while (str != "");
                        if (pi.Phones.Count == 0) pi.Phones = null;

                        Console.WriteLine("Input Emails: ");
                        pi.Emails = new List<string>();
                        do {
                            str = Console.ReadLine();
                            if (NoteBook.CheckEmail(str)) pi.Emails.Add(str);
                            else if (str != "") Console.WriteLine("Bad email");
                        } while (str != "");
                        if (pi.Emails.Count == 0) pi.Phones = null;

                        Console.Write("Input info: ");
                        str = Console.ReadLine();
                        if (str != "") pi.Info = str;

                        book.Add(pi);
                        break;
                    }

                    case 4: {
                        var p = new NoteBook.Pearson();

                        Console.Write("Input Name*: ");
                        p.Name = Console.ReadLine();

                        Console.Write("Input Surname: ");
                        p.Surname = Console.ReadLine();

                        Console.Write("Input Patronymic: ");
                        p.Patronymic = Console.ReadLine();

                        book.Remove(p);
                        break;
                    }

                    case 5: {
                        var p = new NoteBook.Pearson();

                        Console.Write("Input Name*: ");
                        p.Name = Console.ReadLine();

                        Console.Write("Input Surname: ");
                        p.Surname = Console.ReadLine();

                        Console.Write("Input Patronymic: ");
                        p.Patronymic = Console.ReadLine();

                        book.PrintPearsonsInfo(p);
                        break;
                    }

                    case 6: {
                        Console.Write("Input phone: ");
                        book.PrintPearsonsWithPhone(Console.ReadLine());
                        break;
                    }

                    case 7: {
                        book.PrintBook();
                        break;
                    }

                    case 8: {
                        Console.Write("Input letter: ");
                        char c = Console.ReadKey().KeyChar;
                        Console.WriteLine();
                        book.PrintPearsonsWithLetter(c);
                        break;
                    }

                    case 9: {
                        Console.Write("Input number of days: ");
                        string str = Console.ReadLine();
                        int d = 31;
                        if (string.IsNullOrEmpty(str)) d = int.Parse(str);
                        book.PrintNearestBirthDates(d);
                        break;
                    }

                    default: {
                        Console.WriteLine("Wrong Input");
                        break;
                    }
                }

            }
        }
    }
}
