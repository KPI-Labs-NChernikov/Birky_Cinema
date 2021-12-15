using Business.Interfaces;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Data
{
    public class CustomLogger
    {
        private readonly string initialPath = "C:\\Birky\\";

        private readonly IGenreService _genreService;

        public CustomLogger(IGenreService genreService)
        {
            _genreService = genreService;
        }

        private string GetTimeString(DateTime time)
        {
            return $"{time.Year}-{time.Month}-{time.Day}-{time.ToShortTimeString()}-{time.Ticks}";
        }

        private string GetDateString(DateTime time)
        {
            return $"{time.Year}-{time.Month}-{time.Day}";
        }

        private string GetPath(string date) => initialPath + date + ".txt";

        public async Task LoginUserAsync(User user)
        {
            if (!Directory.Exists(initialPath))
                Directory.CreateDirectory(initialPath);
            var time = DateTime.Now;
            var timeString = GetTimeString(time);
            string path = GetPath(GetDateString(time));
            string text = timeString + " User " + user.Email + " has just logged in" + "\n";
            if (!File.Exists(path))
            {
                using StreamWriter sw = File.CreateText(path);
                await sw.WriteAsync(text);
            }
            else
            {using StreamWriter sw = File.AppendText(path);
                await sw.WriteAsync(text);
            }
        }

        public async Task RegisterUserAsync(User user)
        {
            if (!Directory.Exists(initialPath))
                Directory.CreateDirectory(initialPath);
            var time = DateTime.Now;
            var timeString = GetTimeString(time);
            string path = GetPath(GetDateString(time));
            string text = timeString + " User " + user.Email + " has just registered" + "\n";
            text += "Information: \n";
            text += "Name: " + user.FirstName + " " + user.LastName + "\n";
            var phoneNumber = (user.PhoneNumber is null) ? "Не вказано" : user.PhoneNumber;
            text += "Phone number: " + phoneNumber + "\n";
            text += "Fav genres: " + string.Join(", ", (await _genreService.GetUserGenresAsync(user.Id)).Select(g => g.Name)) + "\n";
            if (!File.Exists(path))
            {using StreamWriter sw = File.CreateText(path);
                await sw.WriteAsync(text);
            }
            else
            {using StreamWriter sw = File.AppendText(path);
                await sw.WriteAsync(text);
            }
        }

        public async Task LogoutUserAsync(User user)
        {
            if (!Directory.Exists(initialPath))
                Directory.CreateDirectory(initialPath);
            var time = DateTime.Now;
            var timeString = GetTimeString(time);
            string path = GetPath(GetDateString(time));
            string text = timeString + " User " + user.Email + " has just logged out" + "\n";
            if (!File.Exists(path))
            {using StreamWriter sw = File.CreateText(path);
                await sw.WriteAsync(text);
            }
            else
            {
                using StreamWriter sw = File.AppendText(path);
                await sw.WriteAsync(text);
            }
        }
    }
}
