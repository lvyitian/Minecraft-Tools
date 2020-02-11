using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    public class MCI_Player
    {
        //bool a = mciGetErrorString(error, returnData, 128);

        Random randomNumber = new Random();
        private StringBuilder msg;  // MCI Error message
        private StringBuilder returnData;  // MCI return data
        private int error;
        private string Pcommand;  // String that holds the MCI command
        //private ListView playlist;  // ListView as a playlist with the song path
        private List<string> Lista_Rutas;
        public int NowPlaying { get; set; }
        public bool Paused { get; set; }
        public bool Loop { get; set; }
        public bool Shuffle { get; set; }

        [DllImport("winmm.dll")]
        private static extern int mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        [DllImport("winmm.dll")]
        public static extern int mciGetErrorString(int errCode, StringBuilder errMsg, int buflen);

        // When creating a new player object you have to pass to it a ListView object
        // that will hold all the information about the songs in the playlist
        public MCI_Player(List<string> Lista_Rutas)
        {
            this.Lista_Rutas = Lista_Rutas;
            NowPlaying = 0;
            Loop = true;
            Shuffle = false;
            Paused = false;
            msg = new StringBuilder(1024);
            returnData = new StringBuilder(1024);
        }

        #region Buttons

        /// <summary>
        /// public void Close();
        /// </summary>
        //public void Close()
        public void Dispose()
        {
            Pcommand = "close MediaFile";
            mciSendString(Pcommand, null, 0, IntPtr.Zero);
        }


        public bool Open(string sFileName)
        {
            Dispose();
            // Try to open as mpegvideo 
            Pcommand = "open \"" + sFileName + "\" type mpegvideo alias MediaFile";
            error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
            if (error != 0)
            {
                // Let MCI deside which file type the song is
                Pcommand = "open \"" + sFileName + "\" alias MediaFile";
                error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
                Obtener_Posibles_Errores();
                if (error == 0) return true;
                else return false;
            }
            else return true;
        }


        public bool Play(int track, int Balance, int Volumen)
        {
            if (Open(Lista_Rutas[track]))
            {

                Pcommand = "pause MediaFile";
                error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
                Obtener_Posibles_Errores();

                SetBalance_SetVolume(Balance, Volumen);

                Pcommand = "play MediaFile";
                error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
                Obtener_Posibles_Errores();
                if (error == 0)
                {
                    NowPlaying = track;
                    return true;
                }
                else
                {
                    Dispose();
                    return false;
                }
            }
            else return false;
        }

        public void Pause()
        {
            if (Paused)
            {
                Resume();
                Paused = false;
            }
            else if (IsPlaying())
            {
                Pcommand = "pause MediaFile";
                error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
                Obtener_Posibles_Errores();
                Paused = true;
            }
        }

        public void Stop()
        {
            Pcommand = "stop MediaFile";
            error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
            Obtener_Posibles_Errores();
            Paused = false;
            Dispose();
        }

        public void Resume()
        {
            Pcommand = "resume MediaFile";
            error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
            Obtener_Posibles_Errores();
        }
        #endregion 

        #region Status

        public bool IsPlaying()
        {
            Pcommand = "status MediaFile mode";
            error = mciSendString(Pcommand, returnData, 128, IntPtr.Zero);
            if (returnData.Length == 7 && returnData.ToString().Substring(0, 7) == "playing") return true;
            else return false;
        }

        public bool IsOpen()
        {
            Pcommand = "status MediaFile mode";
            error = mciSendString(Pcommand, returnData, 128, IntPtr.Zero);
            if (returnData.Length == 4 && returnData.ToString().Substring(0, 4) == "open") return true;
            else return false;
        }

        public bool IsPaused()
        {
            Pcommand = "status MediaFile mode";
            error = mciSendString(Pcommand, returnData, 128, IntPtr.Zero);
            if (returnData.Length == 6 && returnData.ToString().Substring(0, 6) == "paused") return true;
            else return false;
        }

        public bool IsStopped()
        {
            Pcommand = "status MediaFile mode";
            error = mciSendString(Pcommand, returnData, 128, IntPtr.Zero);
            if (returnData.Length == 7 && returnData.ToString().Substring(0, 7) == "stopped") return true;
            else return false;
        }
        #endregion

        #region Logic

        public int GetCurentMilisecond()
        {
            Pcommand = "status MediaFile position";
            error = mciSendString(Pcommand, returnData, returnData.Capacity, IntPtr.Zero);
            Obtener_Posibles_Errores();
            return int.Parse(returnData.ToString());
        }

        public void SetPosition(int miliseconds)
        {
            if (IsPlaying())
            {
                Pcommand = "play MediaFile from " + miliseconds.ToString();
                error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
                Obtener_Posibles_Errores();
            }
            else
            {
                Pcommand = "seek MediaFile to " + miliseconds.ToString();
                error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
                Obtener_Posibles_Errores();
            }
        }

        public int GetSongLength()
        {
            if (IsPlaying())
            {
                Pcommand = "status MediaFile length";
                error = mciSendString(Pcommand, returnData, returnData.Capacity, IntPtr.Zero);
                Obtener_Posibles_Errores();
                return int.Parse(returnData.ToString());
            }
            else return 0;
        }

        #endregion

        #region Audio
        [Obsolete]
        public bool SetVolume(int volume)
        {
            if (volume >= 0 && volume <= 1000)
            {
                Pcommand = "setaudio MediaFile volume to " + volume.ToString();
                error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
                Obtener_Posibles_Errores();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// De -1.000 (L) a +1.000 (R), 0 = Estéreo centrado. Volumen de 0 a +1.000.
        /// </summary>
        public bool SetBalance_SetVolume(int Balance, int Volumen)
        {
            int Balance_L = Balance <= 0 ? 1000 : 0;
            int Balance_R = Balance >= 0 ? 1000 : 0;
            if (Balance < 0) Balance_R = 1000 - Math.Abs(Balance * 10);
            if (Balance > 0) Balance_L = 1000 - Math.Abs(Balance * 10);
            Balance_L = (Balance_L * Volumen) / 100;
            Balance_R = (Balance_R * Volumen) / 100;
            Volumen *= 10;
            Pcommand = "setaudio MediaFile volume to " + Volumen.ToString();
            error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
            Obtener_Posibles_Errores();
            Pcommand = "setaudio MediaFile left volume to " + Balance_L.ToString();
            error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
            Obtener_Posibles_Errores();
            Pcommand = "setaudio MediaFile right volume to " + Balance_R.ToString();
            error = mciSendString(Pcommand, null, 0, IntPtr.Zero);
            Obtener_Posibles_Errores();
            return true;
        }

        #endregion

        public int GetSong(bool previous)
        {
            if (Shuffle)
            {
                int i;
                if (Lista_Rutas.Count == 1) return 0;
                while (true)
                {
                    i = randomNumber.Next(Lista_Rutas.Count);
                    if (i != NowPlaying) return i;
                }
            }
            else if (Loop && !previous)
            {
                if (NowPlaying == Lista_Rutas.Count - 1) return 0;
                else return NowPlaying + 1;
            }
            else if (Loop && previous)
            {
                if (NowPlaying == 0) return Lista_Rutas.Count - 1;
                else return NowPlaying - 1;
            }
            else
            {
                if (previous)
                {
                    if (NowPlaying != 0) return NowPlaying - 1;
                    else return 0;
                }
                else
                {
                    if (NowPlaying != Lista_Rutas.Count - 1) return NowPlaying + 1;
                    else return 0;
                }
            }
        }

        public void Obtener_Posibles_Errores()
        {
            if (error != 0)
            {
                returnData = new StringBuilder(1024);
                int Longitud = mciGetErrorString(error, returnData, 1024);
                string Mensaje = returnData.ToString();
                if (!string.IsNullOrEmpty(Mensaje))
                {
                    Depurador.Escribir_Excepción("[winmm.dll] " + Mensaje);
                }
            }
        }
    }
}
