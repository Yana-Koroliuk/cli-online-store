using System;
using System.Linq;
using ConsoleMenu;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ConsoleApp1
{
    /// <summary>
    /// Main program entry point.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main method to start the application.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            UserMenuController.Start();
        }
    }
}