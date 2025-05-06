using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Managers
{
    public static class EventManager
    {
        public static Action OnReset;
        public static Action<float> OnScoreChanged;
        public static Action<int> OnCoinCollected;//Altın toplandıgında InGAMEVİEW için
        public static Action<int,float>GetPoints;//GameOver Oldugunda degerlerı uı gonderır
        public static Action OnGameRestarted;//Oyun Restartlandığında InGameView dakı degerlerı 0 lar
        public static Action<int,int> OnUpdateHighScore;
        public static Action<float> OnScoreIncreased;
        public static Action OnMagnetCollected;
    }
}
