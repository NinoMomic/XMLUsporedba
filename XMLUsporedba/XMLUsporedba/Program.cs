using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Net;

namespace LINQ_XML_usporedba
{
    internal class Program
    {
        static void Main(string[] args)
        {

            XDocument prvi = XDocument.Load(@"Downloads\prvi.xml");
            XDocument drugi = XDocument.Load(@"Downloads\drugi.xml");


            var result1 = from books1 in prvi.Descendants("book")
                          from books2 in drugi.Descendants("book")
                          select new
                          {
                              book1 = new
                              {
                                  id = books1.Attribute("id").Value,
                                  image = books1.Attribute("image").Value,
                                  name = books1.Attribute("name").Value
                              },
                              book2 = new
                              {
                                  id = books2.Attribute("id").Value,
                                  image = books2.Attribute("image").Value,
                                  name = books2.Attribute("name").Value
                              }
                          };

            var result2 = from i in result1
                          where (i.book1.id == i.book2.id
                                 || i.book1.image == i.book2.image
                                 || i.book1.name == i.book2.name) &&
                                 !(i.book1.id == i.book2.id
                                 && i.book1.image == i.book2.image
                                 && i.book1.name == i.book2.name)
                          select i;

            Console.WriteLine("Issued\tIssue type\t\tIssueInFirst\tIssueInSecond");


            int err = 0;
            foreach (var j in result2)
            {
                err++;
                string message = Convert.ToString(err);


                if (j.book1.id != j.book2.id)
                    message += "\tid is different\t\t" + j.book1.id + "\t\t" + j.book2.id;

                else if (j.book1.image != j.book2.image)
                    message += "\timage is different\t" + j.book1.image + "\t\t" + j.book2.image;

                if (j.book1.name != j.book2.name)
                    message += "\tname is different\t" + j.book1.name + "\t\t" + j.book2.name;

                Console.WriteLine(message);
            }

            Console.ReadKey();
        }
    }
}