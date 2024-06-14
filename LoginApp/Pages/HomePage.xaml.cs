using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Plugin.Maui.Audio;

namespace LoginApp.Pages
{
    public partial class HomePage : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<Card> Cards { get; set; }
        public ICommand TapImageCommand { get; set; }
        public ICommand PlayPauseCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        private readonly IAudioManager _audioManager;
        private IAudioPlayer _audioPlayer;
        private Card _currentCard;
        private int _currentCardIndex;
        private bool _isPlaying; // Propriedade para rastrear o estado de reprodução

        public event PropertyChangedEventHandler PropertyChanged; // Evento para notificar mudanças de propriedade

        public HomePage()
        {
            InitializeComponent();

            // Inicialize a coleção de cards
            Cards = new ObservableCollection<Card>
            {
                new Card { ImageSource = "linkinpark.svg", AudioSource = "What I've Done.mp3" },
                new Card { ImageSource = "gunsnroses.svg", AudioSource = "Sweet Child O' Mine.mp3" },
                new Card { ImageSource = "dragonforce.svg", AudioSource = "Through the Fire and Flames.mp3" },
                new Card { ImageSource = "dontstopbelieven.svg", AudioSource = "Don't Stop Believin'.mp3" },
                new Card { ImageSource = "bohemianrhapsody.svg", AudioSource = "Bohemian Rhapsody.mp3" },
                new Card { ImageSource = "demons.svg", AudioSource = "Demons.mp3" },
                new Card { ImageSource = "peladosemsantos.svg", AudioSource = "Pelados em Santos.mp3" }
            };

            // Inicialize comandos
            TapImageCommand = new Command<Card>(OnTapImage);
            PlayPauseCommand = new Command(OnPlayPause);
            NextCommand = new Command(OnNext);
            PreviousCommand = new Command(OnPrevious);

            // Inicialize o gerenciador de áudio
            _audioManager = AudioManager.Current;

            // Defina o BindingContext
            BindingContext = this;
        }

        public string PlayPause => _isPlaying ? "pause.svg" : "play.svg";

        private async void OnTapImage(Card card)
        {
            if (_currentCard == card)
            {
                OnPlayPause();
                return;
            }

            // Pare o áudio anterior, se houver
            if (_audioPlayer != null)
            {
                _audioPlayer.Stop();
                _audioPlayer.Dispose();
                _audioPlayer = null;
            }

            // Crie um novo player e toque a música associada ao card
            _audioPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(card.AudioSource));
            _audioPlayer.Play();
            _isPlaying = true;
            OnPropertyChanged(nameof(PlayPause));

            // Atualize o card atual
            _currentCard = card;
            _currentCardIndex = Cards.IndexOf(card);
        }

        private void OnPlayPause()
        {
            if (_audioPlayer == null) return;

            if (_audioPlayer.IsPlaying)
            {
                _audioPlayer.Pause();
                _isPlaying = false;
            }
            else
            {
                _audioPlayer.Play();
                _isPlaying = true;
            }
            OnPropertyChanged(nameof(PlayPause));
        }

        private void OnNext()
        {
            if (_currentCardIndex < Cards.Count - 1)
            {
                _currentCardIndex++;
                carouselView.Position = _currentCardIndex;
                OnTapImage(Cards[_currentCardIndex]);
            }
        }

        private void OnPrevious()
        {
            if (_currentCardIndex > 0)
            {
                _currentCardIndex--;
                carouselView.Position = _currentCardIndex;
                OnTapImage(Cards[_currentCardIndex]);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Card
    {
        public string ImageSource { get; set; }
        public string AudioSource { get; set; }
    }
}
