using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ELTE.TodoList.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(TodoListDbContext context, string imageDirectory)
        {
            // Use only one of the below.
            // Create / update database based on migration classes.
            context.Database.Migrate();
            // Create database if not exists based on current code-first model (no migrations!).
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            if (context.Lists.Any())
            {
                return;
            }

            var applePath = Path.Combine(imageDirectory, "apple.png");
            var pearPath = Path.Combine(imageDirectory, "pear.png");
            var beerPath = Path.Combine(imageDirectory, "beer.png");

            IList<List> defaultLists = new List<List>
            {
                new List
                {
                    Name = "Bevásárlás",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Alma",
                            Deadline = DateTime.Now.AddDays(1),
                            Image = File.Exists(applePath) ? File.ReadAllBytes(applePath) : null
                        },
                        new Item()
                        {
                            Name = "Körte",
                            Deadline = DateTime.Now.AddDays(1),
                            Image = File.Exists(pearPath) ? File.ReadAllBytes(pearPath) : null

                        },
                        new Item()
                        {
                            Name = "Sör",
                            Deadline = DateTime.Now,
                            Image = File.Exists(beerPath) ? File.ReadAllBytes(beerPath) : null
                        }
                    }
                },

                new List
                {
                    Name = "Beadandók",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "MVC beadandó",
                            Description = "Nem lesz nehéz.",
                            Deadline = DateTime.Now.AddDays(7)
                        },
                        new Item()
                        {
                            Name = "WebAPI beadandó",
                            Description = "Ez még kevésbé lesz nehéz.",
                            Deadline = DateTime.Now.AddDays(10)
                        }
                    }
                },

                new List
                {
                    Name = "Fejlesztés",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Felhasználókezelés",
                            Description = "Bejelentkezés és kijelentkezés megvalósítása süti alapú authentikációval.",
                            Deadline = DateTime.Now.AddDays(4)
                        },
                        new Item()
                        {
                            Name = "Validáció",
                            Description = "A nézetmodell validációs annotációkkal történő ellátása és a szabályok teljesülésének ellenőrzése.",
                            Deadline = DateTime.Now.AddHours(4)
                        },
                        new Item()
                        {
                            Name = "Regisztráció",
                            Description = "Regisztráció implementációja email cím megerősítéssel.",
                            Deadline = DateTime.Now.AddDays(2)
                        },
                        new Item()
                        {
                            Name = "Asztali kliens",
                            Description = "Asztali WPF adminisztrációs kliens fejlesztése.",
                            Deadline = DateTime.Now.AddDays(12)
                        }
                    }
                }
            };

            context.AddRange(defaultLists);
            context.SaveChanges();
        }
    }
}
