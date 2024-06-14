namespace LoginApp.Services
{
    public class MusicService
    {
        public List<Card> GetMusicList()
        {
            return new List<Card>
            {
                new Card { Name = "What I've Done", ImageSource = "linkinpark.svg", AudioSource = "What I've Done.mp3" },
                new Card { Name = "Sweet Child O' Mine", ImageSource = "gunsnroses.svg", AudioSource = "Sweet Child O' Mine.mp3" },
            };
        }
    }

    public class Card
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public string AudioSource { get; set; }
    }
}
