using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaDiscoTex
{
    class Program
    {
        static void Main(string[] args)
        {
            Disco disco = new Disco();

            disco.Open();

            Console.ReadLine();

        }
    }

    class Song
    {
        public string Name { get; private set; }
        public int Duration { get; private set; }

        public Song(string name, int duration)
        {
            Name = name;
            Duration = duration;
        }


    }

    class Disco
    {
        public SemaphoreSlim danceFloor;
        public bool IsOpen { get; private set; }
        private List<Song> playlist;
        int maxGuests;

        public Disco()
        {
            maxGuests = 15;
            danceFloor = new SemaphoreSlim(maxGuests);
            playlist = new List<Song>()
            {
                new Song("I Feel Love", 3460),
                new Song("Funkytown", 3580),
                new Song("Carwash", 3180),
                new Song("Born To Be Alive", 5540),
                new Song("Keep On Dancin", 3440),
                new Song("Ain't No Stoppin' Us Now", 3350),
                new Song("Never Can Say Goodbye", 2570),
                new Song("And The Beat Goes On", 3250),
                new Song("Knock On Wood", 3390),
                new Song("Boogie Oogie Oogie", 3360)
            };
        }

        public void Open()
        {
            IsOpen = true;
            // spawn a random number of guests.
            Random random = new Random();
            int numGuests = random.Next(50, 100);
            for (int guestNumber = 0; guestNumber < numGuests; guestNumber++)
            {
                Song favoriteSong = playlist[random.Next(0, 9)];
                Guest guest = new Guest(this, guestNumber, favoriteSong);
                new Thread(guest.EnterDisco).Start();
            }

            // when empty or time is up, close disco. 
            Thread.Sleep(500);
            int time = 0;
            int maxTime = 80;
            while (danceFloor.CurrentCount < 15 && time < maxTime)
            {
                Thread.Sleep(100);
                time++;
            }
            if (time < maxTime)
            {
                Console.WriteLine("Disco is empty! Shutting down early tonight!");
            }
            else
            {
                IsOpen = false;
                int guests = maxGuests - danceFloor.CurrentCount;
                Console.WriteLine("Kicking out {0} guest(s) and closing for tonight.", guests);
                danceFloor.Release(guests);
            }
        }
    }

    class Guest
    {
        private Disco disco;
        private int guestNumber;
        private Song favoriteSong;
        public Guest(Disco disco, int guestNumber, Song favoriteSong)
        {
            this.disco = disco;
            this.guestNumber = guestNumber;
            this.favoriteSong = favoriteSong;

        }
        public void EnterDisco()
        {
            disco.danceFloor.Wait();
            if (disco.IsOpen)
            {
                Console.WriteLine("Guest number {0} entered!", guestNumber);
                Random random = new Random();
                Thread.Sleep(favoriteSong.Duration);
                disco.danceFloor.Release();
                Console.WriteLine("Guest number {0} left!", guestNumber);
            }
            else
            {
                Console.WriteLine("Guest number {0} goes home crying", guestNumber);
            }
        }
    }
}
