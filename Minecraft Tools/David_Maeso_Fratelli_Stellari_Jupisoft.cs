using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    /// <summary>
    /// Class designed to store the released music albums by David Maeso, Fratelli Stellari and Jupisoft.
    /// </summary>
    internal static class David_Maeso_Fratelli_Stellari_Jupisoft
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Pistas
        {
            internal string Título_Recursos;
            internal string Título;
            internal string URL_Mp3;

            internal Pistas(string Título_Recursos, string URL_Mp3)
            {
                this.Título_Recursos = Program.Traducir_Texto_Mayúsculas_Minúsculas_Automáticamente(Título_Recursos);
                this.Título = this.Título_Recursos.Replace("___", ":").Replace("__", ".").Replace("_", " ");
                this.URL_Mp3 = URL_Mp3;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Álbumes
        {
            internal string Título;
            internal string Recurso;
            internal int Año;
            internal string URL_Html;
            internal string URL_Zip;
            internal List<Pistas> Lista_Pistas;

            internal Álbumes(string Título, int Año, string URL_Html, string URL_Zip, List<Pistas> Lista_Pistas)
            {
                this.Título = Program.Traducir_Texto_Mayúsculas_Minúsculas_Automáticamente(Título);
                this.Recurso = this.Título.Replace(" ", "_").Replace("-", "_").Replace(".", "_").Replace(":", "_").Replace("'", "_").Replace("(", "_").Replace(")", "_").Replace("&", "_").Trim("_".ToCharArray());
                while (this.Recurso.Contains("__")) this.Recurso = this.Recurso.Replace("__", "_");
                this.Año = Año;
                this.URL_Html = URL_Html;
                this.URL_Zip = URL_Zip;
                this.Lista_Pistas = Lista_Pistas;
            }
        }

        /// <summary>
        /// List with links to the awesome free albums of my friend David Maeso, if you haven't listened to his music yet, you don't know what you've been missing, so please navigate to any of his links.
        /// </summary>
        internal static readonly List<Álbumes> Lista_Álbumes_David_Maeso = new List<Álbumes>(new Álbumes[]
        {
            new Álbumes("Air", 2019,
            "http://www.davidmaeso.com/album-26.html",
            "", null),
            new Álbumes("Fire", 2019,
            "http://www.davidmaeso.com/album-25.html",
            "", null),
            new Álbumes("Water", 2019,
            "http://www.davidmaeso.com/album-24.html",
            "", null),
            new Álbumes("Earth", 2019,
            "http://www.davidmaeso.com/album-23.html",
            "", null),
            new Álbumes("Habitat", 2018,
            "http://www.davidmaeso.com/album-22.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20HABITAT%20[2018].zip", null),
            new Álbumes("Blue", 2017,
            "http://www.davidmaeso.com/album-21.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20BLUE%20[2017].zip", null),
            new Álbumes("Green", 2017,
            "http://www.davidmaeso.com/album-20.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20GREEN%20[2017].zip", null),
            new Álbumes("Red", 2017,
            "http://www.davidmaeso.com/album-19.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20RED%20[2017].zip", null),
            new Álbumes("Sonicscape", 2017,
            "http://www.davidmaeso.com/album-18.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20SONICSCAPE%20[2017].zip", null),
            new Álbumes("Project", 2016,
            "http://www.davidmaeso.com/album-17.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20PROJECT%20[2016].zip", null),
            new Álbumes("Wallflowers", 2016,
            "http://www.davidmaeso.com/album-16.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20WALLFLOWERS%20[2016].zip", null),
            new Álbumes("Inconsequential", 2015,
            "http://www.davidmaeso.com/album-15.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20INCONSEQUENTIAL%20[2015].zip", null),
            new Álbumes("Haunting", 2014,
            "http://www.davidmaeso.com/album-14.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20HAUNTING%20[2000].zip", null),
            new Álbumes("Discothèque", 2014,
            "http://www.davidmaeso.com/album-13.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20DISCOTHEQUE%20[2014].zip", null),
            new Álbumes("Punchy", 2014,
            "http://www.davidmaeso.com/album-12.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20PUNCHY%20[2014].zip", null),
            new Álbumes("Outplace", 2014,
            "http://www.davidmaeso.com/album-11.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20OUTPLACE%20[2014].zip", null),
            new Álbumes("Weakness", 2012,
            "http://www.davidmaeso.com/album-10.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20WEAKNESS%20[2012].zip", null),
            new Álbumes("Alchemy", 2012,
            "http://www.davidmaeso.com/album-09.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20ALCHEMY%20[2012].zip", null),
            new Álbumes("Landscape", 2008,
            "http://www.davidmaeso.com/album-08.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20LANDSCAPE%20[2008].zip", null),
            new Álbumes("Phobia", 2008,
            "http://www.davidmaeso.com/album-07.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20PHOBIA%20[2008].zip", null),
            new Álbumes("Breathtaking", 2006,
            "http://www.davidmaeso.com/album-06.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20BREATHTAKING%20[2006].zip", null),
            new Álbumes("Wanted", 2006,
            "http://www.davidmaeso.com/album-05.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20WANTED%20[2006].zip", null),
            new Álbumes("Additional", 2004,
            "http://www.davidmaeso.com/album-04.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20ADDITIONAL%20[2004].zip", null),
            new Álbumes("Realistic", 2004,
            "http://www.davidmaeso.com/album-03.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20REALISTIC%20[2004].zip", null),
            new Álbumes("Endless", 2004,
            "http://www.davidmaeso.com/album-02.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20ENDLESS%20[2004].zip", null),
            new Álbumes("Ubiquitous", 2004,
            "http://www.davidmaeso.com/album-01.html",
            "http://www.davidmaeso.com/downloads/David%20Maeso%20_%20UBIQUITOUS%20[2004].zip", null)
        });

        /// <summary>
        /// List with links to the awesome albums by Fratelli Stellari, if you haven't listened to their music yet, you don't know what you've been missing, so please navigate to any of their links.
        /// </summary>
        internal static readonly List<Álbumes> Lista_Álbumes_Fratelli_Stellari = new List<Álbumes>(new Álbumes[]
        {
            // Fratelli Stellari albums:

            new Álbumes("50 Sfumature di Alieno", 2016,
            "https://fratellistellari.bandcamp.com/track/50-sfumature-di-alieno",
            "", null),
            new Álbumes("Advent of the Space Gods", 2016,
            "https://fratellistellari.bandcamp.com/album/advent-of-the-space-gods",
            "", null),
            new Álbumes("Aglien Discomix", 2016,
            "https://fratellistellari.bandcamp.com/album/aglien-discomix",
            "", null),
            new Álbumes("Electrowave", 2018,
            "https://fratellistellari.bandcamp.com/album/electrowave",
            "", null),
            new Álbumes("Galactic Sound", 2016,
            "https://fratellistellari.bandcamp.com/album/galactic-sound",
            "", null),
            new Álbumes("Instrumental Hits Vol. 1", 2017,
            "https://fratellistellari.bandcamp.com/album/instrumental-hits-vol-1",
            "", null),
            new Álbumes("Instrumental Hits Vol. 2", 2017,
            "https://fratellistellari.bandcamp.com/album/instrumental-hits-vol-2",
            "", null),
            new Álbumes("Le Sciantose Aliene (DJoNemesis & Lilly Remix)", 2016,
            "http://www.messaggidallestelle.altervista.org/le-sciantose-aliene.html",
            "", null),
            new Álbumes("Les Trois Mères - Deep Space Mix", 2017,
            "https://fratellistellari.bandcamp.com/track/les-trois-m-res-deep-space-mix",
            "", null),
            new Álbumes("Matres Alienorum", 2016,
            "https://fratellistellari.bandcamp.com/track/matres-alienorum",
            "", null),
            new Álbumes("Milky Way Super Mix", 2016,
            "https://fratellistellari.bandcamp.com/track/milky-way-super-mix",
            "", null),
            new Álbumes("Milky Way Super Mix (Instrumental)", 2017,
            "https://fratellistellari.bandcamp.com/track/milky-way-super-mix-instrumental",
            "", null),
            new Álbumes("Nightflight to Planet X", 2016,
            "https://fratellistellari.bandcamp.com/album/nightflight-to-planet-x",
            "", null),
            new Álbumes("Ufo Dance (Space Edit)", 2016,
            "https://fratellistellari.bandcamp.com/track/ufo-dance-space-edit",
            "", null),

            new Álbumes(), // Add a separator here.

            // DJoNemesis & Lilly albums:

            new Álbumes("An Ufo Over Turin", 2017,
            "https://djonemesis-lilly.bandcamp.com/track/an-ufo-over-turin",
            "", null),
            new Álbumes("Arrival Prophecy", 2017,
            "https://djonemesis-lilly.bandcamp.com/album/arrival-prophecy",
            "", null),
            new Álbumes("Baffo d'Oro", 2016,
            "http://djonemesis-lilly.bandcamp.com/track/baffo-doro",
            "", null),
            new Álbumes("Home Visitors", 2016,
            "http://djonemesis-lilly.bandcamp.com/track/home-visitors",
            "", null),
            new Álbumes("Instrumental Essence Vol. 1", 2017,
            "https://djonemesis-lilly.bandcamp.com/album/instrumental-essence-vol-1",
            "", null),
            new Álbumes("Instrumental Essence Vol. 2", 2017,
            "https://djonemesis-lilly.bandcamp.com/album/instrumental-essence-vol-2",
            "", null),
            new Álbumes("Interstellar Melody", 2016,
            "http://www.djonemesis.altervista.org/interstellar-melody.html",
            "", null),
            new Álbumes("Nibiru Remixing", 2017,
            "https://djonemesis-lilly.bandcamp.com/album/nibiru-remixing",
            "", null),
            new Álbumes("Ritornata dalla Luce", 2016,
            "http://djonemesis-lilly.bandcamp.com/track/ritornata-dalla-luce",
            "", null)
        });

        /// <summary>
        /// List with my own free music albums. What I do most is program cool applications like this one, but what I do most after it is compose new music, so please check any of the albums at least to see if you find any song to your liking, thanks a lot.
        /// </summary>
        internal static readonly List<Álbumes> Lista_Álbumes_Jupisoft = new List<Álbumes>(new Álbumes[]
        {
            new Álbumes("FLOATING CITY REMIXES", 2019,
            "http://jupisoft.x10host.com/html/floating_city_remixes.html",
            "", null),
            new Álbumes("NIGHT MIXES", 2019,
            "http://jupisoft.x10host.com/html/night_mixes.html",
            "http://www.mediafire.com/file/zf6ec8h4f42ldj9/JUPISOFT_-_NIGHT_MIXES.zip/file", null),
            new Álbumes("REMIXES 3.0", 2019,
            "http://jupisoft.x10host.com/html/remixes_3_0.html",
            "http://www.mediafire.com/file/wmw77cmtobdl10m/JUPISOFT_-_REMIXES_3.0.zip/file", null),
            new Álbumes("REMIXES 2.0", 2018,
            "http://jupisoft.x10host.com/html/remixes_2_0.html",
            "http://www.mediafire.com/file/crbvbww7is47gsn/JUPISOFT_-_REMIXES_2.0.zip/file", null),
            new Álbumes("SATELLITES 2.0", 2018,
            "http://jupisoft.x10host.com/html/satellites_2_0.html",
            "http://www.mediafire.com/file/9c5zqohxl5fx57x/JUPISOFT_-_SATELLITES_2.0.zip/file", null),
            new Álbumes("INFINITE MIXES", 2018,
            "http://jupisoft.x10host.com/html/infinite_mixes.html",
            "http://www.mediafire.com/file/1pp1o2th73obeot/JUPISOFT_-_INFINITE_MIXES.zip", null),
            new Álbumes("LIGHTS AND SHADOWS", 2018,
            "http://jupisoft.x10host.com/html/lights_and_shadows.html",
            "http://www.mediafire.com/file/okxb98mbx97n2h9/JUPISOFT_-_LIGHTS_AND_SHADOWS.zip/file", null),
            new Álbumes("EXTENDED MIXES 4.0", 2018,
            "http://jupisoft.x10host.com/html/extended_mixes_4_0.html",
            "", null),
            //new Álbumes("LOW MIXES 6.0", 2018,
            //"http://jupisoft.x10host.com/html/low_mixes_6_0.html",
            //"", null),
            new Álbumes("EXTENDED MIXES 3.0", 2018,
            "http://jupisoft.x10host.com/html/extended_mixes_3_0.html",
            "", null),
            new Álbumes("ANUNNAKI", 2018,
            "http://jupisoft.x10host.com/html/anunnaki.html",
            "http://www.mediafire.com/file/x5bmmli5ccg5402/JUPISOFT_-_ANUNNAKI.zip", null),
            new Álbumes("HIGH MIXES", 2018,
            "http://jupisoft.x10host.com/html/high_mixes.html",
            "http://www.mediafire.com/file/522yv6f21md9ayz/JUPISOFT_-_HIGH_MIXES.zip", null),
            //new Álbumes("LOW MIXES 5.0", 2018,
            //"http://jupisoft.x10host.com/html/low_mixes_5_0.html",
            //"http://www.mediafire.com/file/khbrbglobb5gkfg/JUPISOFT_-_LOW_MIXES_5.0.zip", null),
            //new Álbumes("LOW MIXES 4.0", 2018,
            //"http://jupisoft.x10host.com/html/low_mixes_4_0.html",
            //"http://www.mediafire.com/file/vay5ri1ug8lm8pp/JUPISOFT_-_LOW_MIXES_4.0.zip", null),
            new Álbumes("EXTENDED MIXES 2.0", 2018,
            "http://jupisoft.x10host.com/html/extended_mixes_2_0.html",
            "http://www.mediafire.com/file/umd0inhmzmy1wx8/JUPISOFT_-_EXTENDED_MIXES_2.0.zip", null),
            new Álbumes("MINECRAFT 2.0", 2018,
            "http://jupisoft.x10host.com/html/minecraft_2_0.html",
            "http://www.mediafire.com/file/66lyd3od9d2kglm/JUPISOFT_-_MINECRAFT_2.0.zip", null),
            new Álbumes("REMIXES", 2018,
            "http://jupisoft.x10host.com/html/remixes.html",
            "http://www.mediafire.com/file/cwuj3443mj2chhf/JUPISOFT_-_REMIXES.zip", null),
            new Álbumes("MONSTER HIGH 3.0", 2018,
            "http://jupisoft.x10host.com/html/monster_high_3_0.html",
            "http://www.mediafire.com/file/96c7xswpa985suw/JUPISOFT_-_MONSTER_HIGH_3.0.zip", null),
            new Álbumes("MONSTER HIGH 2.0", 2018,
            "http://jupisoft.x10host.com/html/monster_high_2_0.html",
            "http://www.mediafire.com/file/bp75lc3il5n3qq0/JUPISOFT_-_MONSTER_HIGH_2.0.zip", null),
            new Álbumes("MONSTER HIGH", 2017,
            "http://jupisoft.x10host.com/html/monster_high.html",
            "http://www.mediafire.com/file/7o9ut8slfl2yt5p/JUPISOFT_-_MONSTER_HIGH.zip", null),
            new Álbumes("EXTENDED MIXES", 2017,
            "http://jupisoft.x10host.com/html/extended_mixes.html",
            "http://www.mediafire.com/file/1ut12e427y0ii22/JUPISOFT_-_EXTENDED_MIXES.zip", null),
            new Álbumes("THANKS AGAIN", 2017,
            "http://jupisoft.x10host.com/html/thanks_again.html",
            "http://www.mediafire.com/file/b9yhl70udksv7zq/JUPISOFT_-_THANKS_AGAIN.zip", null),
            //new Álbumes("LOW MIXES 3.0", 2017,
            //"http://jupisoft.x10host.com/html/low_mixes_3_0.html",
            //"http://www.mediafire.com/file/rhqdq8iplr7dp38/JUPISOFT_-_LOW_MIXES_3.0.zip", null),
            //new Álbumes("LOW MIXES 2.0", 2017,
            //"http://jupisoft.x10host.com/html/low_mixes_2_0.html",
            //"http://www.mediafire.com/file/3ipn9k5yxqbsx9f/JUPISOFT_-_LOW_MIXES_2.0.zip", null),
            //new Álbumes("LOW MIXES", 2017,
            //"http://jupisoft.x10host.com/html/low_mixes.html",
            //"http://www.mediafire.com/file/l2u7msw6gjsed9j/JUPISOFT_-_LOW_MIXES.zip", null),
            new Álbumes("SATELLITES", 2017,
            "http://jupisoft.x10host.com/html/satellites.html",
            "http://www.mediafire.com/file/3697tf9g387t3ea/JUPISOFT_-_SATELLITES.zip", null),
            new Álbumes("THANK YOU", 2017,
            "http://jupisoft.x10host.com/html/thank_you.html",
            "http://www.mediafire.com/file/sm27cx2lszxfcnw/JUPISOFT_-_THANK_YOU.zip", null),
            new Álbumes("SEASONS", 2017,
            "http://jupisoft.x10host.com/html/seasons.html",
            "http://www.mediafire.com/file/dt7dp2d2bi05b8o/JUPISOFT_-_SEASONS.zip", null),
            new Álbumes("SOLAR SYSTEM", 2017,
            "http://jupisoft.x10host.com/html/solar_system.html",
            "http://www.mediafire.com/file/q7k6dt0vmd1xl2d/JUPISOFT_-_SOLAR_SYSTEM.zip", null),
            new Álbumes("MINECRAFT", 2017,
            "http://jupisoft.x10host.com/html/minecraft.html",
            "http://www.mediafire.com/file/p94opbcw520q3ww/JUPISOFT_-_MINECRAFT.zip", null),
            new Álbumes("MACHINES", 2017,
            "http://jupisoft.x10host.com/html/machines.html",
            "http://www.mediafire.com/file/wl8e520nlbv5023/JUPISOFT_-_MACHINES.zip", null),
            new Álbumes("EMOTIONS", 2017,
            "http://jupisoft.x10host.com/html/emotions.html",
            "http://www.mediafire.com/file/itp62d5yt33ycyb/JUPISOFT_-_EMOTIONS.zip", null),
            new Álbumes("DAY AND NIGHT", 2017,
            "http://jupisoft.x10host.com/html/day_and_night.html",
            "http://www.mediafire.com/file/04o6mv1p9lxjk26/JUPISOFT_-_DAY_AND_NIGHT.zip", null),
            new Álbumes("RESURRECTION", 2017,
            "http://jupisoft.x10host.com/html/resurrection.html",
            "http://www.mediafire.com/file/lgr94577ons01ab/JUPISOFT_-_RESURRECTION.zip", null),
            






            /*new Álbumes("ANUNNAKI", 2018,
            "http://jupisoft.x10host.com/html/anunnaki.html",
            "http://www.mediafire.com/file/x5bmmli5ccg5402/JUPISOFT_-_ANUNNAKI.zip", null),
            new Álbumes("DAY AND NIGHT", 2017,
            "http://jupisoft.x10host.com/html/day_and_night.html",
            "http://www.mediafire.com/file/04o6mv1p9lxjk26/JUPISOFT_-_DAY_AND_NIGHT.zip", null),
            new Álbumes("EMOTIONS", 2017,
            "http://jupisoft.x10host.com/html/emotions.html",
            "http://www.mediafire.com/file/itp62d5yt33ycyb/JUPISOFT_-_EMOTIONS.zip", null),
            new Álbumes("EXTENDED MIXES", 2017,
            "http://jupisoft.x10host.com/html/extended_mixes.html",
            "http://www.mediafire.com/file/1ut12e427y0ii22/JUPISOFT_-_EXTENDED_MIXES.zip", null),
            new Álbumes("EXTENDED MIXES 2.0", 2018,
            "http://jupisoft.x10host.com/html/extended_mixes_2_0.html",
            "http://www.mediafire.com/file/umd0inhmzmy1wx8/JUPISOFT_-_EXTENDED_MIXES_2.0.zip", null),
            new Álbumes("EXTENDED MIXES 3.0", 2018,
            "http://jupisoft.x10host.com/html/extended_mixes_3_0.html",
            "", null),
            new Álbumes("EXTENDED MIXES 4.0", 2018,
            "http://jupisoft.x10host.com/html/extended_mixes_4_0.html",
            "", null),
            new Álbumes("HIGH MIXES", 2018,
            "http://jupisoft.x10host.com/html/high_mixes.html",
            "http://www.mediafire.com/file/522yv6f21md9ayz/JUPISOFT_-_HIGH_MIXES.zip", null),
            new Álbumes("INFINITE MIXES", 2018,
            "http://jupisoft.x10host.com/html/infinite_mixes.html",
            "", null),
            new Álbumes("LIGHTS AND SHADOWS", 2018,
            "http://jupisoft.x10host.com/html/lights_and_shadows.html",
            "http://www.mediafire.com/file/okxb98mbx97n2h9/JUPISOFT_-_LIGHTS_AND_SHADOWS.zip/file", null),
            new Álbumes("LOW MIXES", 2017,
            "http://jupisoft.x10host.com/html/low_mixes.html",
            "http://www.mediafire.com/file/l2u7msw6gjsed9j/JUPISOFT_-_LOW_MIXES.zip", null),
            new Álbumes("LOW MIXES 2.0", 2017,
            "http://jupisoft.x10host.com/html/low_mixes_2_0.html",
            "http://www.mediafire.com/file/3ipn9k5yxqbsx9f/JUPISOFT_-_LOW_MIXES_2.0.zip", null),
            new Álbumes("LOW MIXES 3.0", 2017,
            "http://jupisoft.x10host.com/html/low_mixes_3_0.html",
            "http://www.mediafire.com/file/rhqdq8iplr7dp38/JUPISOFT_-_LOW_MIXES_3.0.zip", null),
            new Álbumes("LOW MIXES 4.0", 2018,
            "http://jupisoft.x10host.com/html/low_mixes_4_0.html",
            "http://www.mediafire.com/file/vay5ri1ug8lm8pp/JUPISOFT_-_LOW_MIXES_4.0.zip", null),
            new Álbumes("LOW MIXES 5.0", 2018,
            "http://jupisoft.x10host.com/html/low_mixes_5_0.html",
            "http://www.mediafire.com/file/khbrbglobb5gkfg/JUPISOFT_-_LOW_MIXES_5.0.zip", null),
            new Álbumes("LOW MIXES 6.0", 2018,
            "http://jupisoft.x10host.com/html/low_mixes_6_0.html",
            "", null),
            new Álbumes("MACHINES", 2017,
            "http://jupisoft.x10host.com/html/machines.html",
            "http://www.mediafire.com/file/wl8e520nlbv5023/JUPISOFT_-_MACHINES.zip", null),
            new Álbumes("MINECRAFT", 2017,
            "http://jupisoft.x10host.com/html/minecraft.html",
            "http://www.mediafire.com/file/p94opbcw520q3ww/JUPISOFT_-_MINECRAFT.zip", null),
            new Álbumes("MINECRAFT 2.0", 2018,
            "http://jupisoft.x10host.com/html/minecraft_2_0.html",
            "http://www.mediafire.com/file/66lyd3od9d2kglm/JUPISOFT_-_MINECRAFT_2.0.zip", null),
            new Álbumes("MONSTER HIGH", 2017,
            "http://jupisoft.x10host.com/html/monster_high.html",
            "http://www.mediafire.com/file/7o9ut8slfl2yt5p/JUPISOFT_-_MONSTER_HIGH.zip", null),
            new Álbumes("MONSTER HIGH 2.0", 2018,
            "http://jupisoft.x10host.com/html/monster_high_2_0.html",
            "http://www.mediafire.com/file/bp75lc3il5n3qq0/JUPISOFT_-_MONSTER_HIGH_2.0.zip", null),
            new Álbumes("MONSTER HIGH 3.0", 2018,
            "http://jupisoft.x10host.com/html/monster_high_3_0.html",
            "http://www.mediafire.com/file/96c7xswpa985suw/JUPISOFT_-_MONSTER_HIGH_3.0.zip", null),
            new Álbumes("REMIXES", 2018,
            "http://jupisoft.x10host.com/html/remixes.html",
            "http://www.mediafire.com/file/cwuj3443mj2chhf/JUPISOFT_-_REMIXES.zip", null),
            new Álbumes("REMIXES 2.0", 2018,
            "http://jupisoft.x10host.com/html/remixes_2_0.html",
            "", null),
            new Álbumes("RESURRECTION", 2017,
            "http://jupisoft.x10host.com/html/resurrection.html",
            "http://www.mediafire.com/file/lgr94577ons01ab/JUPISOFT_-_RESURRECTION.zip", null),
            new Álbumes("SATELLITES", 2017,
            "http://jupisoft.x10host.com/html/satellites.html",
            "http://www.mediafire.com/file/3697tf9g387t3ea/JUPISOFT_-_SATELLITES.zip", null),
            new Álbumes("SEASONS", 2017,
            "http://jupisoft.x10host.com/html/seasons.html",
            "http://www.mediafire.com/file/dt7dp2d2bi05b8o/JUPISOFT_-_SEASONS.zip", null),
            new Álbumes("SOLAR SYSTEM", 2017,
            "http://jupisoft.x10host.com/html/solar_system.html",
            "http://www.mediafire.com/file/q7k6dt0vmd1xl2d/JUPISOFT_-_SOLAR_SYSTEM.zip", null),
            new Álbumes("THANK YOU", 2017,
            "http://jupisoft.x10host.com/html/thank_you.html",
            "http://www.mediafire.com/file/sm27cx2lszxfcnw/JUPISOFT_-_THANK_YOU.zip", null),
            new Álbumes("THANKS AGAIN", 2017,
            "http://jupisoft.x10host.com/html/thanks_again.html",
            "http://www.mediafire.com/file/b9yhl70udksv7zq/JUPISOFT_-_THANKS_AGAIN.zip", null)*/
        }); // And other new free albums will come soon as well.
    }
}
