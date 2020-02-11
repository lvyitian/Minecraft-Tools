using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Reproductor_Multimedia
{
    internal static class Midi2
    {
        // The length in bytes used to store the length of a chunk.
        private const int LengthByteCount = 4;

        // Length in bytes of the Midi file header.
        private const int FileHeaderLength = 6;

        // Length in bytes of the format data.
        private const int FormatByteCount = 2;

        // The format maximum value.
        private const int FormatMax = 2;

        // Length in bytes of the track count.
        private const int TrackCountByteCount = 2;

        // Length in bytes of the division data.
        private const int DivisionByteCount = 2;

        // Bit flat used to determine if a byte is a status byte.
        private const int StatusFlag = 0x80;

        // Number of bits to shift bytes in parsing data.
        private const int Shift = 7;

        // Masks Midi channel.
        private const int ChannelMask = 240;

        /// <summary>
        /// Represents the various channel message types.
        /// </summary>
        public enum ChannelCommand
        {
            /// <summary>
            /// Represents the note-off command type.
            /// </summary>
            NoteOff = 0x80,

            /// <summary>
            /// Represents the note-on command type.
            /// </summary>
            NoteOn = 0x90,

            /// <summary>
            /// Represents the poly pressure (aftertouch) command type.
            /// </summary>
            PolyPressure = 0xA0,

            /// <summary>
            /// Represents the controller command type.
            /// </summary>
            Controller = 0xB0,

            /// <summary>
            /// Represents the program change command type.
            /// </summary>
            ProgramChange = 0xC0,

            /// <summary>
            /// Represents the channel pressure (aftertouch) command 
            /// type.
            /// </summary>
            ChannelPressure = 0xD0,

            /// <summary>
            /// Represents the pitch wheel command type.
            /// </summary>
            PitchWheel = 0xE0
        }

        /// <summary>
        /// Represents the various types.
        /// </summary>
        public enum MetaType
        {
            /// <summary>
            /// Represents sequencer number type.
            /// </summary>
            SequenceNumber,

            /// <summary>
            /// Represents the text type.
            /// </summary>
            Text,

            /// <summary>
            /// Represents the copyright type.
            /// </summary>
            Copyright,

            /// <summary>
            /// Represents the track name type.
            /// </summary>
            TrackName,

            /// <summary>
            /// Represents the instrument name type.
            /// </summary>
            InstrumentName,

            /// <summary>
            /// Represents the lyric type.
            /// </summary>
            Lyric,

            /// <summary>
            /// Represents the marker type.
            /// </summary>
            Marker,

            /// <summary>
            /// Represents the cue point type.
            /// </summary>
            CuePoint,

            /// <summary>
            /// Represents the program name type.
            /// </summary>
            ProgramName,

            /// <summary>
            /// Represents the device name type.
            /// </summary>
            DeviceName,

            /// <summary>
            /// Represents then end of track type.
            /// </summary>
            EndOfTrack = 0x2F,

            /// <summary>
            /// Represents the tempo type.
            /// </summary>
            Tempo = 0x51,

            /// <summary>
            /// Represents the Smpte offset type.
            /// </summary>
            SmpteOffset = 0x54,

            /// <summary>
            /// Represents the time signature type.
            /// </summary>
            TimeSignature = 0x58,

            /// <summary>
            /// Represents the key signature type.
            /// </summary>
            KeySignature,

            /// <summary>
            /// Represents the proprietary event type.
            /// </summary>
            ProprietaryEvent = 0x7F
        }

        // ...

        [StructLayout(LayoutKind.Sequential)]
        internal struct Notas
        {
            internal int Posición;
            internal int Duración;
            internal int Nota;

            internal Notas(int Posición, int Duración, int Nota)
            {
                this.Posición = Posición;
                this.Duración = Duración;
                this.Nota = Nota;
            }
        }

        internal static List<Notas> Leer_Archivo_MIDI(string Ruta)
        {


            return null;
        }

        internal static void Escribir_Archivo_MIDI(string Ruta, List<Notas> Lista_Notas)
        {
            if (!string.IsNullOrEmpty(Ruta) && Lista_Notas != null && Lista_Notas.Count > 0)
            {
                FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                Lector.SetLength(0L);
                Lector.Seek(0L, SeekOrigin.Begin);
                BinaryWriter Lector_Binario = new BinaryWriter(Lector, Encoding.ASCII);
                Lector_Binario.Write(new byte[4] { (byte)'M', (byte)'T', (byte)'h', (byte)'d', }); // Cabecera de archivo
                Lector_Binario.Write(new byte[4] { 0, 0, 0, 6 }); // Longitud cabecera

                Lector_Binario.Write(new byte[2] { 0, 0 }); // Formato. 0 = 1 sola pista.

                Lector_Binario.Write(new byte[2] { 0, 1 }); // Pistas

                Lector_Binario.Write(new byte[2] { 0, 96 }); // ¿División?

                Lector_Binario.Write(new byte[4] { (byte)'M', (byte)'T', (byte)'r', (byte)'k' }); // Cabecera de pista

                Lector_Binario.Write((int)0); // Longitud de la pista




                bool done = false;
                int value = 0;
                // While there are still bytes left to pack.
                while (!done)
                {
                    // Read next byte.
                    byte b = 0;
                    Lector_Binario.Write(b);

                    // If this is note the last byte.
                    if ((b & StatusFlag) == StatusFlag)
                    {
                        // Mask eigth bit.
                        b &= 0x7F;
                    }
                    // Else this is the last byte.
                    else
                    {
                        // Indicate that this is the last byte to pack.
                        done = true;
                    }

                    // Shift value and pack next byte.
                    value <<= Shift;
                    value |= b;
                }



                // ...

                for (int Índice = 0; Índice < Lista_Notas.Count; Índice++)
                {
                    Lector_Binario.Write(new byte[2] { 0, 1 }); // 




                }

                Lector_Binario.Close();
                Lector_Binario.Dispose();
                Lector_Binario = null;
                Lector.Close();
                Lector.Dispose();
                Lector = null;
            }
        }

        internal enum NoteEvent
        {
            NoteOff = 0x8,
            NoteOn = 0x9,

            // Advanced
            AfterTouch = 0xA,
            ControlChange = 0xB,
            ProgramChange = 0xC,
            ChannelPressure = 0xD,
            PitchWheel = 0xE
        }

        internal static int ticksPerBeat = 0x80;
        internal static sbyte Channel = -1;
        internal static List<byte> TrackData = new List<byte>();
        internal static List<byte> TrackMetadata = new List<byte>();

        internal static bool ValidTrack()
        {
            return Channel >= 0;
        }

        internal static byte[] TranslateTickTime(uint ticks)
        {
            uint value = ticks;
            uint buffer;
            buffer = ticks & 0x7F;
            value = value >> 7;
            while (value > 0)
            {
                buffer = buffer << 8;
                buffer = buffer | ((value & 0x7F) | 0x80);
                value = value >> 7;
            }

            // The encoded values are now in the buffer backwards, so retrieve them...
            List<byte> blist = new List<byte>();
            while (true)
            {
                blist.Add((byte)(0xFF & buffer));
                if ((buffer & 0x80) > 0)
                {
                    buffer = buffer >> 8;
                }
                else
                {
                    break;
                }
            }
            return blist.ToArray();
        }

        internal static void AddNoteOnOffEvent(double beatOffset, NoteEvent ev, byte note, byte volume)
        {
            if (!ValidTrack()) return;
            uint tickOffset = (uint)(beatOffset * ticksPerBeat);
            if (ev == NoteEvent.NoteOn || ev == NoteEvent.NoteOff)
            {
                TrackData.AddRange(TranslateTickTime(tickOffset));
                TrackData.Add((byte)(((byte)ev << 4) | ((byte)Channel & 0xF)));
                TrackData.Add(note);
                TrackData.Add(volume);
            }
            else
            {
                // Error handling here, or other handling
            }
        }
    }
}
