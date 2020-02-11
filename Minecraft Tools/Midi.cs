using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reproductor_Multimedia
{
    public class Midi
    {
        public enum PatchType
        {
            None = 0,
            AcousticGrandPiano = 1,
            BrightAcousticPiano = 2,
            ElectricGrandPiano = 3,
            HonkyTonkPiano = 4,
            ElectricPiano1 = 5,
            ElectricPiano2 = 6,
            HarpsiChord = 7,
            Clavinet = 8,
            Celesta = 9,
            Glockenspiel = 10,
            MusicBox = 11,
            Vibraphone = 12,
            Marimba = 13,
            Xylophone = 14,
            TubularBells = 15,
            Dulcimer = 16,
            DrawbarOrgan = 17,
            PercussiveOrgan = 18,
            RockOrgan = 19,
            ChurchOrgan = 20,
            ReedOrgan = 21,
            Accordion = 22,
            Harmonica = 23,
            TangoAccordion = 24,
            AcousticGuitar1 = 25,
            AcousticGuitar2 = 26,
            ElectricGuitar3 = 27,
            ElectricGuitar4 = 28,
            ElectricGuitar5 = 29,
            OverdrivenGuitar = 30,
            DistortionGuitar = 31,
            GuitarHarmonics = 32,
            AcousticBass = 33,
            ElectricBass1 = 34,
            ElectricBass2 = 35,
            FretlessBass = 36,
            SlapBass1 = 37,
            SlapBass2 = 38,
            SynthBass1 = 39,
            SynthBass2 = 40,
            Violin = 41,
            Viola = 42,
            Cello = 43,
            Contrabass = 44,
            TremoloStrings = 45,
            PizzicatoStrings = 46,
            OrchestralHarp = 47,
            Timpani = 48,
            StringEnsemble1 = 49,
            StringEnsemble2 = 50,
            Synthstrings1 = 51,
            Synthstrings2 = 52,
            ChoirAahs = 53,
            VoiceOohs = 54,
            SynthVoice = 55,
            OrchestraHit = 56,
            TRUMPET = 57,
            Trombone = 58,
            Tuba = 59,
            MutedTRUMPET = 60,
            FrenchHorn = 61,
            BrassSection = 62,
            SynthBrass1 = 63,
            SynthBrass2 = 64,
            SopranoSax = 65,
            AltoSax = 66,
            TenorSax = 67,
            BaritoneSax = 68,
            Oboe = 69,
            EnglishHorn = 70,
            Bassoon = 71,
            Clarinet = 72,
            Piccolo = 73,
            Flute = 74,
            Recorder = 75,
            PanFlute = 76,
            BlownBottle = 77,
            Shakuhachi = 78,
            Whistle = 79,
            Ocarina = 80,
            Lead1 = 81,
            Lead2 = 82,
            Lead3 = 83,
            Lead4 = 84,
            Lead5 = 85,
            Lead6 = 86,
            Lead7 = 87,
            Lead8 = 88,
            Pad1 = 89,
            Pad2 = 90,
            Pad3 = 91,
            Pad4 = 92,
            Pad5 = 93,
            Pad6 = 94,
            Pad7 = 95,
            Pad8 = 96,
            Fx1 = 97,
            Fx2 = 98,
            Fx3 = 99,
            Fx4 = 100,
            Fx5 = 101,
            Fx6 = 102,
            Fx7 = 103,
            Fx8 = 104,
            Sitar = 105,
            Banjo = 106,
            Shamisen = 107,
            Koto = 108,
            Kalimba = 109,
            Bagpipe = 110,
            Fiddle = 111,
            Shanai = 112,
            TinkleBell = 113,
            Agogo = 114,
            SteelDrums = 115,
            Woodblock = 116,
            TailoDrum = 117,
            MelodicDrum = 118,
            SynthDrum = 119,
            ReverseCymbal = 120,
            GuitarFretNoise = 121,
            BreathNoise = 122,
            Seashore = 123,
            BirdTweet = 124,
            TelephoneRing = 125,
            Helicopter = 126,
            Applause = 127,
            Gunshot = 128
        };

        // These are fixed data:
        byte[] MIDIHeader = new byte[] { 0x4D, 0x54, 0x68, 0x64, 0x0, 0x0, 0x0, 0x6 };
        byte[] SubFormatType = new byte[] { 0x0, 0x1 }; // Type-1 MIDI file (as opposed to Type-0)

        const byte ticksPerBeat = 0x80;
        // These could be changed (in theory) by the program
        public byte[] Speed = new byte[] { 0x0, ticksPerBeat }; // Default to 128

        public List<Track> Tracks = new List<Track>();
        public Track AddTrack(PatchType Instrumento)
        {
            Track t = new Track();
            t.Channel = (sbyte)(Tracks.Count + 1);
            t.AddNoteOnOffEvent(0, Track.NoteEvent.ProgramChange, (byte)Instrumento, 0);
            Tracks.Add(t);
            return t;
        }

        public void Load(string filepath)
        {
            // Maybe later...
        }

        public void Save(string filepath)
        {
            try
            {
                FileStream Lector = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                Lector.SetLength(0L);
                Lector.Seek(0L, SeekOrigin.Begin);

                Lector.Write(MIDIHeader, 0, MIDIHeader.Length);
                Lector.Write(SubFormatType, 0, SubFormatType.Length);

                ushort numTracks = 0;
                foreach (Track t in Tracks)
                {
                    numTracks += 1;
                }
                byte[] byteTracks = new byte[2];
                byteTracks[0] = (byte)((numTracks >> 8) & 0xFF);
                byteTracks[1] = (byte)((numTracks & 0xFF));
                Lector.Write(byteTracks, 0, byteTracks.Length);

                Lector.Write(Speed, 0, Speed.Length);
                Lector.Flush();

                foreach (Track t in Tracks) t.Save(Lector);

                Lector.Close();
                Lector.Dispose();
                Lector = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save the file due to the following error: " + ex.Message);
                return;
            }
        }

        public class Track
        {
            public enum NoteEvent
            {
                NoteOff = 0x8,
                NoteOn = 0x9,

                // Advanced
                AfterTouch = 0xA,
                ControlChange = 0xB,
                ProgramChange = 0xC,
                ChannelPressure = 0xD,
                PitchWheel = 0xE,
                SystemExclusive = 0xF
            }

            // These are fixed data:
            byte[] TrackHeader = new byte[] { 0x4D, 0x54, 0x72, 0x6B };
            byte[] TrackOut = new byte[] { 0x0, 0xFF, 0x2F, 0x0 };

            // These can be changed by the program

            List<byte> TrackData = new List<byte>();
            List<byte> TrackMetadata = new List<byte>();
            public sbyte Channel = -1;
            bool ValidTrack()
            {
                return Channel >= 0;
            }

            public void Save(FileStream Lector)
            {
                if (ValidTrack())
                {
                    Lector.Write(TrackHeader, 0, TrackHeader.Length);

                    uint TrackSize = (uint)(TrackData.Count + TrackMetadata.Count + TrackOut.Length);
                    byte[] byteTrackSize = new byte[4];
                    byteTrackSize[0] = (byte)((TrackSize >> 24) & 0xFF);
                    byteTrackSize[1] = (byte)((TrackSize >> 16) & 0xFF);
                    byteTrackSize[2] = (byte)((TrackSize >> 8) & 0xFF);
                    byteTrackSize[3] = (byte)((TrackSize & 0xFF));

                    Lector.Write(byteTrackSize, 0, byteTrackSize.Length);
                    Lector.Write(TrackData.ToArray(), 0, TrackData.Count);
                    Lector.Write(TrackMetadata.ToArray(), 0, TrackMetadata.Count);
                    Lector.Write(TrackOut, 0, TrackOut.Length);
                    Lector.Flush();
                }
            }

            public void AddNoteOnOffEvent(double beatOffset, NoteEvent ev, byte note, byte volume)
            {
                if (!ValidTrack()) return;
                uint tickOffset = (uint)(beatOffset * 1/*ticksPerBeat*/);
                if (ev == NoteEvent.NoteOn || ev == NoteEvent.NoteOff)
                {
                    TrackData.AddRange(TranslateTickTime(tickOffset));
                    TrackData.Add((byte)(((byte)ev << 4) | ((byte)Channel & 0xF)));
                    TrackData.Add(note);
                    TrackData.Add(volume);
                }
                else if (ev == NoteEvent.ProgramChange) // Error handling here, or other handling
                {
                    TrackData.AddRange(TranslateTickTime(tickOffset));
                    TrackData.Add((byte)(((byte)ev << 4) | ((byte)Channel & 0xF)));
                    TrackData.Add(note); // Instrumento
                }
            }

            byte[] TranslateTickTime(uint ticks)
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
                    else break;
                }
                return blist.ToArray();
            }
        }
    }
}
