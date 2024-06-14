using Plugin.Maui.Audio;

namespace LoginApp.Pages
{
    public partial class MusicPlayerPage : ContentPage
    {
        private readonly IAudioManager _audioManager;
        private IAudioPlayer _audioPlayer;
        private bool _isPlaying;

        public MusicPlayerPage(Music selectedMusic)
        {
            InitializeComponent();
            _audioManager = AudioManager.Current;

            musicTitle.Text = selectedMusic.Nome;
            musicArtist.Text = selectedMusic.Artista;

            InitializePlayer(selectedMusic.AudioSource);
        }

        private async void InitializePlayer(string audioSource)
        {
            _audioPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(audioSource));
        }

        private void OnPlayPauseButtonClicked(object sender, EventArgs e)
        {
            if (_audioPlayer == null)
                return;

            if (_isPlaying)
            {
                _audioPlayer.Pause();
                playPauseButton.Source = "play.svg";
            }
            else
            {
                _audioPlayer.Play();
                playPauseButton.Source = "pause.svg";
            }

            _isPlaying = !_isPlaying;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _audioPlayer?.Stop();
            _audioPlayer?.Dispose();
        }
    }
}
