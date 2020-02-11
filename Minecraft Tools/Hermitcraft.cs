using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Tools
{
    internal static class Hermitcraft
    {
        /// <summary>
        /// Structure that holds up all the information about a Hermit.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Hermits
        {
            internal string Nombre;
            internal string Nombres;
            internal string Nombre_Real;
            internal DateTime Fecha_Nacimiento;
            internal string País;
            internal List<string> Lista_Perfiles_Minecraft;
            internal List<string> Lista_URL_Skins; // Descargar en directo cada skin y tener opción de ver desde memoria si falla.
            internal List<string> Lista_URL_Youtube;
            internal List<string> Lista_URL_Twitch;
            internal List<string> Lista_URL_Twitter;
            internal List<string> Lista_URL_Discord;
            internal List<string> Lista_URL_Patreon;
            internal List<string> Lista_URL_Mixer;
            internal List<string> Lista_URL_Facebook;
            internal List<string> Lista_URL_Instagram;
            internal List<string> Lista_URL_Sito_Web;
            internal List<string> Lista_URL_Correo;
            internal List<string> Lista_URL_Reddit;
            internal List<string> Lista_URL_Otros;
            internal string Biografía;
            internal bool Miembro_Antiguo;

            internal Hermits(string Nombre, string Nombres, string Nombre_Real, DateTime Fecha_Nacimiento, string País, List<string> Lista_Nombres_Minecraft, List<string> Lista_URL_Youtube, List<string> Lista_URL_Twitch, List<string> Lista_URL_Twitter, List<string> Lista_URL_Discord, List<string> Lista_URL_Patreon, List<string> Lista_URL_Mixer, List<string> Lista_URL_Facebook, List<string> Lista_URL_Instagram, List<string> Lista_URL_Sito_Web, List<string> Lista_URL_Correo, List<string> Lista_URL_Reddit, string Biografía)
            {
                this = new Hermits(Nombre, Nombres, Nombre_Real, Fecha_Nacimiento, País, Lista_Nombres_Minecraft, Lista_URL_Youtube, Lista_URL_Twitch, Lista_URL_Twitter, Lista_URL_Discord, Lista_URL_Patreon, Lista_URL_Mixer, Lista_URL_Facebook, Lista_URL_Instagram, Lista_URL_Sito_Web, Lista_URL_Correo, Lista_URL_Reddit, null, Biografía, false);
            }

            internal Hermits(string Nombre, string Nombres, string Nombre_Real, DateTime Fecha_Nacimiento, string País, List<string> Lista_Nombres_Minecraft, List<string> Lista_URL_Youtube, List<string> Lista_URL_Twitch, List<string> Lista_URL_Twitter, List<string> Lista_URL_Discord, List<string> Lista_URL_Patreon, List<string> Lista_URL_Mixer, List<string> Lista_URL_Facebook, List<string> Lista_URL_Instagram, List<string> Lista_URL_Sito_Web, List<string> Lista_URL_Correo, List<string> Lista_URL_Reddit, List<string> Lista_URL_Otros, string Biografía)
            {
                this = new Hermits(Nombre, Nombres, Nombre_Real, Fecha_Nacimiento, País, Lista_Nombres_Minecraft, Lista_URL_Youtube, Lista_URL_Twitch, Lista_URL_Twitter, Lista_URL_Discord, Lista_URL_Patreon, Lista_URL_Mixer, Lista_URL_Facebook, Lista_URL_Instagram, Lista_URL_Sito_Web, Lista_URL_Correo, Lista_URL_Reddit, Lista_URL_Otros, Biografía, false);
            }

            internal Hermits(string Nombre, string Nombres, string Nombre_Real, DateTime Fecha_Nacimiento, string País, List<string> Lista_Nombres_Minecraft, List<string> Lista_URL_Youtube, List<string> Lista_URL_Twitch, List<string> Lista_URL_Twitter, List<string> Lista_URL_Discord, List<string> Lista_URL_Patreon, List<string> Lista_URL_Mixer, List<string> Lista_URL_Facebook, List<string> Lista_URL_Instagram, List<string> Lista_URL_Sito_Web, List<string> Lista_URL_Correo, List<string> Lista_URL_Reddit, List<string> Lista_URL_Otros, string Biografía, bool Miembro_Antiguo)
            {
                this.Nombre = Nombre;
                this.Nombres = Nombres;
                this.Nombre_Real = Nombre_Real;
                this.Fecha_Nacimiento = Fecha_Nacimiento;
                this.País = País;
                this.Lista_Perfiles_Minecraft = Lista_Nombres_Minecraft;
                if (Lista_Nombres_Minecraft != null && Lista_Nombres_Minecraft.Count > 0)
                {
                    this.Lista_URL_Skins = new List<string>();
                    foreach (string Texto_Nombre in Lista_Nombres_Minecraft)
                    {
                        this.Lista_URL_Skins.Add("http://skins.minecraft.net/MinecraftSkins/" + Texto_Nombre + ".png");
                    }
                }
                else this.Lista_URL_Skins = null;
                this.Lista_URL_Youtube = Lista_URL_Youtube;
                this.Lista_URL_Twitch = Lista_URL_Twitch;
                this.Lista_URL_Twitter = Lista_URL_Twitter;
                this.Lista_URL_Discord = Lista_URL_Discord;
                this.Lista_URL_Patreon = Lista_URL_Patreon;
                this.Lista_URL_Mixer = Lista_URL_Mixer;
                this.Lista_URL_Facebook = Lista_URL_Facebook;
                this.Lista_URL_Instagram = Lista_URL_Instagram;
                this.Lista_URL_Sito_Web = Lista_URL_Sito_Web;
                this.Lista_URL_Correo = Lista_URL_Correo;
                this.Lista_URL_Reddit = Lista_URL_Reddit;
                this.Lista_URL_Otros = Lista_URL_Otros;
                this.Biografía = Biografía;
                this.Miembro_Antiguo = Miembro_Antiguo;
            }

            internal static readonly Hermits[] Matriz_Hermits = new Hermits[26]
            {
                new Hermits
                (
                    "BDubs",
                    "B Double O 100",
                    "John",
                    new DateTime(1982, 10, 12),
                    "United States",
                    new List<string>(new string[] { "BdoubleO100" }),
                    new List<string>(new string[] { "https://youtube.com/bdoubleo100", "https://www.youtube.com/user/BdoubleOLIVE", "https://www.youtube.com/channel/UCcBycEGmj161_90p5DanjzA" }),
                    new List<string>(new string[] { "http://www.twitch.tv/bdoubleo" }),
                    new List<string>(new string[] { "https://twitter.com/BdoubleO100" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/BdoubleO100" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/bdoubleo", "http://www.reddit.com/user/IamnotBdoubleO" }),
                    new List<string>(new string[] { "http://bdoubleo.spreadshirt.com/" }),
                    "BdoubleO100, known simply as BdoubleO, or his personal name John, is an American Let's Play commentator, a former member of Mindcrack, and an active member of Hermitcraft. He was invited to join the Mindcrack Server by Guude in September 2011. On 3 April 2015, it was announced BdoubleO would be parting ways with Mindcrack due to legal changes within the brand. In his words, Bdubs \"was told that [he] was not allowed to be an official member anymore\". He joined Hermitcraft at the start of Season 5 in April 2017.\r\n\r\nBdoubleO has a younger brother Joe, known as Pungence, who also does YouTube full time, and a younger sister Abigail \"Abbye\", a cosmetologist.\r\n\r\nSince mid-2012, BdoubleO has done YouTube full time. Bdubs previously worked as a general contractor for 8 years in a family-run business.\r\n\r\nOn 5 December 2013, BdoubleO and his wife Nicole announced they were expecting their first child, later revealing the gender to be female. Ariana Calliste was born on 16 July 2014, weighing 6 lb 7 oz. BdoubleO describes the birth of his daughter as \"the best thing that's ever happened to [him]\".\r\n\r\nOn 31 October 2015, Nicole surprised BdoubleO with the announcement of their second child. Her name was revealed to be Eden in a 15 May 2016 vlog. Eden was born on 21 May 2016 at 5:30 AM. According to her birth vlog, Eden shares a birthday with BdoubleO's sister, Abigail.\r\n\r\nIn \"Minecraft Peep Show\", BdoubleO tries out the new features of upcoming versions of Minecraft in survival single player. He has suspended the series since the Mindcrack server started using snapshots of Minecraft.\r\n\r\nUpon the birth of his daughter, BdoubleO began using his secondary channel \"BdoubleOLIVE\" as a vlogging channel, specifically for updates about his daughter.\r\n\r\nBdoubleO's popular \"Building with BdoubleO\" survival singleplayer series returned on 24 November 2014 shortly after hitting 1,000,000 YouTube subscribers. During the return episode of the series, Bdubs announced a tertiary YouTube channel \"BDUBSwithCHEESE\". The channel is a pay-to-subscribe channel that will allow subscribed users to no longer see advertisements on his main channel, and will have access to behind-the-scenes footage of Bdubs' video creation.\r\n\r\nBdoubleO is a fan of popular musician Ronald Jenkees. He has close contact with Jenkees and was allowed to play many of Jenkees' music in his videos.\r\n\r\nJenkees himself was pleased that BdoubleO has exposed many people to his music. BdoubleO praises and acknowledges Jenkees' musical talent many times in his videos.\r\n\r\nBdoubleO was a fan of Guude's Mindcrack series since season 2. He messaged Guude one day to ask if he could make an intro music for him. Guude didn't message him back as he was flooded with messages. So BdoubleO donated to Guude and reminded him of the offer. After a while of corresponding, BdoubleO asked Guude if he was interested in doing Vechs' CTM map \"Legendary\". Together they formed a team, OOG, and has created one of the most engaging Minecraft series. Since then, the duo has played together on \"Nightmare Realm\", \"Sea of Flame II\", Portal 2, and Saints Row: The Third. BdoubleO has produced intro music for several series, including Race for the Wool, Saints Row: The Third, Saints Row IV, Spellbound Caves, Sea of Flames II, Building with BdoubleO, Nightmare Realms, and Portal 2. Much of his music can be downloaded at http://www.mediafire.com/?70vve7ie4rui8u9\r\n\r\nOn the season 3 map, BdoubleO rebuilt his house at the spawn village and also built a home near Guude in the mountains. Following his love for music, he built the Record Shop and has collected Music Discs to sell there. He has worked on a massive construction project to build the Arena. BdoubleO and generikb teamed up to form the B-Team, which spawned from their actions against Etho. Together they perform comical hijinks around the server.\r\n\r\nIn Season 4, BdoubleO took the task of building the town hall. The town hall served as the focal point for the new Spawn area on the server. On the server, he resided in his modern house within the vicinity of Spawn. BdoubleO also constructed the nether hub with Etho. This served as the main transportation system on the Mindcrack server. Some of his other builds included docks in the jungle that had a path that lead to a jungle temple that was going to be made into a small adventure map, and an arena he is building with Etho.\r\n\r\nIn Season 5, Bdubs found an area in a roofed forest, where he built his treehouse.\r\n\r\nBdoubleO appeared as guest on the HermitCraft server on 21 October 2012 during Keralis1's livestream, but didn't officially join the server until 5 January 2013. He teamed up with generikb and new members Juicetra, Pungence, and skyzm, in response to Biffa2001's and Xisuma's prank on Generikb. Their first action was to litter Biffa's and Xisuma's bases with giant letter B's. He later issued a build-off challenge to Keralis1, before becoming inactive on the server.\r\n\r\nBdoubleO appeared as a guest in HermitCraft UHC 9 in September 2015.\r\n\r\nHe rejoined the server at the start of HermitCraft 5 in April 2017, forming a team with Etho, Docm77, VintageBeef, named the New Hermit Order (nHo).\r\n\r\nBdoubleO received a Cape for attending MineCon 2011 and MineCon 2013. Although he attended MineCon 2012, he gave away his cape. In addition to his primary skin, BdoubleO has a Rambo-themed skin created by kurtmac for use in Mindcrack Ultra Hardcore. He had alternated between the two skins at times, before creating a skin in August 2014 that incorporates the designs of both skins, through new customization options added in 1.8.\r\n\r\nOther skins used by BdoubleO include a Star Trek-themed skin during his \"Herobrine's Return\" series, a modified version of his pastry chef skin wearing a ski mask while \"robbing\" members of the server, a prison uniform during his Prison Break series, and a Steve skin body to imitate the skin of Keralis1.\r\n\r\nBdoubleO is whitelisted on the \"CrewCraft\" server: a survival-multiplayer Minecraft server that features popular YouTube personalities. His only involvement on the server was fighting the Wither with Crew members: KYR_SP33DY, JahovazWitniss, MrNobodyEpic, ShadowBeatz, and SideArms. He was most likely invited due to his collaboration with Sp33dy a few months prior."
                ),
                new Hermits
                (
                    "Biffa",
                    "Biffa 2001, Biffa, Agent B",
                    "",
                    new DateTime(1, 1, 1),
                    "United Kingdom",
                    new List<string>(new string[] { "Biffa2001", "Biffa001" }),
                    new List<string>(new string[] { "https://youtube.com/biffaplays" }),
                    new List<string>(new string[] { "http://www.twitch.tv/biffa2001" }),
                    new List<string>(new string[] { "https://twitter.com/Biffa2001" }),
                    new List<string>(new string[] { "https://discord.gg/JoinBiffa" }),
                    new List<string>(new string[] { "https://www.patreon.com/Biffa2001" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/Biffa2001" }),
                    new List<string>(new string[] { "https://plus.google.com/u/0/115160611594796760063" }),
                    "Biffa2001 is an English Let's Play commentator, a founding member of the HermitCraft server, and a guest of the Mindcrack modded server since February 2014.\r\n\r\nBiffa is from England, United Kingdom. When asked about his city of residence, Biffa says \"not so up North but not so down South\". Biffa is a husband, and a father of two sons. His second son, Theo, was born on 1 January 2012. Aside from YouTube, Biffa works two jobs but despite his busy schedule, he finds time to record whenever he can.\r\n\r\nBiffa is a founding member of the HermitCraft server. He had been invited to join by generikb due to his work on Let's Plays.\r\n\r\nOn HermitCraft 2.0, Biffa's first house was a largely wooden home open to the air at the main town. He initially established a remote base at a witch hut but gave it up after finding out that most of the hut is outside the swamp. He found another witch hut and established a base there. He had cleared a huge perimeter centered around the witch hut to improve the efficiency of the witch farm, which is based on King_Happy's witch farm design. He was in the process of building a massive base in the sky above the witch hut, larger than his Bowl. He had plans to build a dark room mob farm above the witch farm. The sky base could be accessed using Ender Pearl elevators. He planned to have automated machines and several farms for crops, animals, and villagers.\r\n\r\nBiffa joined the Mindcrack modded server in February 2014, during its third map (Crack the Beast). He was invited to join the server as a guest to continue his collaboration with W92Baj. Biffa helped develop the CrackPack modpack, and appeared on Mindcrack's CrackPack server on the Best Team. He reappeared during season 2 of the CrackPack server.\r\n\r\nAfter a year long absence of Mindcrack's modded server, Biffa returned as a guest on the upon the launch of a new CrackPack server in June 2016.\r\n\r\nBiffa's current skin is modified from his previous skin; his helmet lifted with his face visible. This skin was made by Awsam36.\r\n\r\nBiffa reached 1,111 subscribers on 11 November 2011 (11/11/11).\r\n\r\nIn the English Premier League, Biffa follows Tottenham Hotspur F.C."
                ),
                new Hermits
                (
                    "Cleo",
                    "Zombie Cleo",
                    "",
                    new DateTime(1980, 5, 16),
                    "United Kingdom",
                    new List<string>(new string[] { "ZombieCleo", "Cleophas" }),
                    new List<string>(new string[] { "https://youtube.com/ZombieCleo" }),
                    new List<string>(new string[] { "http://www.twitch.tv/zombiecleo" }),
                    new List<string>(new string[] { "https://twitter.com/ZombieCleo" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/zombiecleo" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    "ZombieCleo, or simply Cleo, is a Let's Play commentator and an active member of the HermitCraft server. She was invited onto the server by joehillssays for the HermitCraft map reset on June 2013. She runs a personal Minecraft survival multiplayer server called \"Bob\" for her friends. Cleo used the Minecraft account Cleophas until May 2012.\r\n\r\nCleo worked as a teacher in real life and is proud of her career. Her previous job was a geotechnical engineer working on public buildings. While she was working on a school, she became inspired to become a teacher. She has a degree in geology and taught science in school for about eight years. Her students do not know about her life outside school. She left the job due to her health but plans to work as a substitute teacher instead.\r\n\r\nThe name ZombieCleo comes from her World of Warcraft character where she was the undead. She describes herself a \"reclusive nerdy Internet geek\".\r\n\r\nCleo started her YouTube channel to keep in touch with her best friend in what she was doing. Cleo's first experience in Minecraft was Super Hostile after she watched the Yogcast playing it and bought the game because of them. She will not be doing collaborations with Vechs due to their busy schedule and will not join MindCrack if asked to join. She considers YouTube a hobby and does not have plans to work full time on it even if it becomes feasible. She doesn't want to make something she enjoys a lot into a job.\r\n\r\nMany of Cleo's builds on the server were inspired by the builds on her own personal server, Bob. Cleo has built a temple as her home at town and helped build the Hanging Gardens, a group project to build the ancient wonders of the world. At the new 1.7 town, she built two tall glass towers connected by a glass bridge on top of the peaks of the hills.\r\n\r\nCleo is currently building a museum called the Natural History and Science Museum, inspired by the London Natural History Museum. She plans to build various exhibitions in it. The museum centerpiece is a giant dinosaur skull with gold teeth. Displays in the museum includes King Khufu, Koh e Poor, notable minerals, and ancient armor. Floating \"balloons\" are used to light the place. Underneath the museum, Cleo dug a massive chamber that would house Garthulu the squid. It is meant represent a Cthulhu that follows her everywhere she goes and will appear out of a massive portal. The walls are lined with sand and dark platforms for mobs to spawn. She added a massive hall called the Hall Of Theoretical Biology where plants with custom names are on display.\r\n\r\nCleo started a publishing and distribution company called Cleo's Copying Ink in the commercial district at the new town. There she writes the Hermiton Herald periodical that details current events and posts advertisements on the server. She also started a flower shop nearby.\r\n\r\nCleo's current skin is a zombified version of her previous skin, which can be seen in the background image of her TwitchTV and Twitter profiles."
                ),
                new Hermits
                (
                    "Cubfan",
                    "Cub Fan 135",
                    "",
                    new DateTime(1, 1, 1),
                    "United States",
                    new List<string>(new string[] { "cubfan135" }),
                    new List<string>(new string[] { "https://youtube.com/cubfan135" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://twitter.com/cubfan135" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.mixer.com/cubfan135" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    "cubfan135 is an active member of the HermitCraft server since February 2016."
                ),
                new Hermits
                (
                    "Doc",
                    "Doc M 77",
                    "Steffen Mark Mössner",
                    new DateTime(1977, 7, 5),
                    "Germany",
                    new List<string>(new string[] { "Docm77" }),
                    new List<string>(new string[] { "https://www.youtube.com/docm77" }),
                    new List<string>(new string[] { "http://www.twitch.tv/docm77live" }),
                    new List<string>(new string[] { "https://twitter.com/docm77" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/Docm77" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "http://www.docm77.de/" }),
                    new List<string>(new string[] { "Docm77@docm77.de" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://plus.google.com/u/0/116336713104226040425/videos", "http://steamcommunity.com/profiles/76561198052375146" }),
                    "Docm77, also known by his personal name Steffen Mark Mössner (pronounced \"mœsnɐ\" in German, anglicised as \"mɒsnər\"), is a German Let's Play commentator, a member of Mindcrack since November 2011, and a member of HermitCraft since March 2015.\r\n\r\nDoc was born in Ulm in the former nation of West Germany, and currently lives in Karlsruhe, both of which are located in Baden-Württemberg in current-day Germany. Doc speaks German natively, and learned English during his schooling. He has described his current dialect as \"Denglish\". Doc's parents separated when he was young. His mother had to struggle with a poorly-paid job while his father owned a company and was a self-made millionaire.\r\n\r\nDoc went to university to study electrical engineering and has a degree in it. He interned at Siemens in order to progress through his studies; his subject was regenerative energies. His team managed to develop something that could've change the world, but company leaders rejected it because of shareholder interests. Out of anger and frustration, Doc decided to turn to nursing as his second study and ended up in geriatric healthcare. He worked there for 8 years. He specialized in dementia research, and received a PhD for his work. At some point in Doc's life, he played basketball at a professional level for 15 years. Doc has mentioned that he has a doctor's degree in both social science and political science.\r\n\r\nIn December 2012, Doc announced that he had taken up YouTube as a full-time job.\r\n\r\nDoc has a Minecraft series called \"The Minecraft World Tour\" in which he builds structures and redstone contraptions and tests various aspects of the game in single player.\r\n\r\nDoc joined the Mindcrack Server in November 2011 alongside Etho, having been invited by Guude after competing in the Race for Wool tournament. According to Etho, Doc had initially hesitated joining due to his already running \"World Tour\" series, but decided to join anyway. Doc's earliest activity on the server was in January 2012 when he littered Guude's house with boats, together with Etho. By March, Doc was still logged out at Guude's house, yet to begin his series.\r\n\r\nHe began his server Let's Play in May 2012, traveling to new chunks in search of a jungle biome, settling relatively close to PauseUnpause's home.\r\n\r\nOn the 4th map, Docm77 founded Team DnA with AnderZEL, made a villager trading and breeding system, and cleared a perimeter for a witch farm. Notch and Dinnerbone, along with a handful of Mindcrackers, helped Doc with this project. Doc also made Doc Shop at spawn. This is his shop where he sells products from his witch farm and villager trading.\r\n\r\nOn the 5th map, Docm once again showed interest in villagers. He started off with a basic infinite breeder, so he can get good trades.\r\n\r\nDoc's favorite color is green. His favorite movie is Fear and Loathing in Las Vegas, favorite book is The Lord of the Rings, and favorite video game is Minecraft. Pearl Jam is his favorite band. He supports Bayern Munich.\r\n\r\nOn 20 February 2015, Docm announced via Twitter and a Reddit post on /r/mindcrack that he would be joining Season 7 of HermitCraft Ultra Hardcore as a guest. Former active member of the MindCrack Fan Server and Mindcrack UHC Season 16 guest brianmcn commented on the post linking to Docm's previously expressed dislike for all non-Mindcrack UHC events.\r\n\r\n\"The only uhc I play in mindcrack. I don't like it when other people play it and flood youtube with it and delude it.\"\r\n\r\nDocm replied criticizing Brian's work for SethBling, defending himself saying \"I just trolled him back[...] There is nothing to it\" and \"No shaming anywhere. Just shots fired back.\" Docm claims he \"never held [the opinion shared his October 2014 tweet]\" and \"can't believe that people [took his tweet seriously]\".\r\n\r\nDoc's original skin was made by WandererDavid, in a skin contest. The skin was a mix between a Creeper and the Terminator wearing a white coat. After Doc's \"nerfing\" by Dinnerbone, he used a \"genetically engineered\" skin made by Nilba. Nilba later updated the skin to use new features added in 14w03a. Docm77 received a cape for attending MineCon 2012, MineCon 2013, and MineCon 2015.\r\n\r\nThe handle \"Docm77\" is a combination of his nickname from when he played basketball (Doctor), his last name (Mössner) and his year of birth (1977).\r\n\r\nDoc's basketball nickname comes from the similarity of his play style to Julius Erving, whose nickname was \"Doctor J\".\r\n\r\nDoc began creating videos in English instead of German because his first video was a response to Etho.\r\n\r\nDoc is one of the tallest Mindcrackers, at 1.95m or 6'5\". Vechs and Arkas are the only Mindcrackers known to be taller.\r\n\r\nDoc weighs 85kg.\r\n\r\nDoc's shoe size is 48 in European, which is around 13 in the United States.\r\n\r\nDoc participated in a slam dunk contest in 2004.\r\n\r\nDoc once played against Dirk Nowitzki.\r\n\r\nDoc has family living in Canada, presumably in British Columbia.\r\n\r\nDoc titled his first episode on both the Mindcrack Server and the HermitCraft server \"Humble Beginnings\"."
                ),
                new Hermits
                (
                    "Etho",
                    "Etho, Etho's Lab, Etho Slab, Treetho, Ladders",
                    "",
                    new DateTime(1, 1, 1),
                    "Canada",
                    new List<string>(new string[] { "Etho" }),
                    new List<string>(new string[] { "https://www.youtube.com/ethoslab" }),
                    new List<string>(new string[] { "http://www.twitch.tv/ethotv" }),
                    new List<string>(new string[] { "https://twitter.com/EthoLP" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/EthosLab" }),
                    "Etho is a Canadian Let's Play commentator, a member of HermitCraft and a former member of Mindcrack. Etho joined the Mindcrack Server in November 2011, at the same time as Docm77, after befriending Guude through the second Race for Wool. Etho joined the HermitCraft Server in March 2015. On 3 April 2015, it was announced that Etho would be parting ways with Mindcrack. Etho explained his decision to leave was influenced by hesitation to sign the trademark agreement and be involved in the expanding business side of the Mindcrack name.\r\n\r\nEtho has been generally reticent regarding personal details about his life outside of YouTube. Etho previously worked in horticulture as part of a family-owned greenhouse business. Before revealing his previous profession, viewers had regularly claimed Etho to be a teacher, an IT professional or even a member of NASA. The comments of this video were later disabled due to a certain user that traced this business and released images of Etho and his location. Etho does not have any post-secondary education, graduating from high school with 35 of his credits coming from work experience. In June 2012, Etho began producing YouTube content full time, having left the greenhouse business to do so.\r\n\r\nOne of Etho's major hobbies prior to his activities on YouTube involved programming. He has been programming since age 9. A major goal of his was to create an emulator that would provide a user-friendly GUI that would display information about selected games in a similar manner to Xbox Live Marketplace. The emulator would have been applicable to older console games. Etho authored a separate program that collected box art, manuals, reviews, guides, music, and other aspects of each game; the emulator then allowed users to browse the compiled database for a desired game. He had some concerns about the program, including copyright legality and the amount of effort required to produce a final product. Among other things, Etho designed an experimental 2.5D engine to tinker with simulated three-dimensional perspective in video games. He was inspired to do so after playing a game with a poorly designed 2.5D engine. YouTube has since replaced his programming activities.\r\n\r\nDespite not making an official public appearance, Etho has revealed some details about his appearance on occasion. He is 6'2\", sometimes wears glasses, and has described his hairstyle as similar to of one of the possible Cube World characters. Etho says he is thin due to his high metabolism.\r\n\r\nAt both MineCon 2012 and MineCon 2013, generikb posted multiple pictures to Twitter claiming to have met Etho in person, though that was not the case.\r\n\r\nEtho had hinted at attending MineCon 2014, but later said he wouldn't be attending after learning the event would be held in Europe.\r\n\r\nBefore starting his Minecraft Let's Play, Etho uploaded a number of Harvest Moon 64 videos to YouTube, all without commentary, and some video game programming showcases prior to that. His most popular and longest running Minecraft series is titled \"Etho Plays Minecraft\". The series, originally called \"Let's Play Minecraft\", is a singleplayer survival Let's Play that is still currently being produced.\r\n\r\nOn the Mindcrack Season 3 server, many components of Etho's base were underground. His mob system was the most prominent structure on the surface. The nearby island has his free-range Cow farm and Sugar Cane farm. Underneath the mob system is his Mancave, which was smoothed out by Zisteau in a prank. In the cave, Etho had the largest Slime farm in the server. In the Nether, Etho had built a Dual Blaze Farm with a prominent structure surrounding it, simulating a hanging platform. Water source blocks were used as decoration within the structure. Outside the structure, there were five water source blocks spread throughout the ceiling of the Nether. Etho is part of Team Canada, an alliance formed by PauseUnpause to prank others as a group. He collaborated with Docm77 to create the server's first Wither Skeleton spawning pad. Etho went on to create his new base and storage room underground in a jungle some what near Zisteau's place. He planned the new base to be underground in a lush jungle with a hanging storage room with a beacon and mob tower farms, although he didn't finish before the map was reset.\r\n\r\nEtho's first endeavors on the modded Feed the Beast server was Etho Corp, a company in which he serves as founder and chief executive officer. Under the banner of Etho Corp, Etho built his base on the server, which includes floating islands of various purposes (tree farming and storage, among others). The Honeycomb Labs, the basis for much of his bee research on the server, was situated nearby the main components of his base. After the server reset, Etho initially decided to power his operations entirely on biomass but later deviated from this plan, showing particular interest in the Railcraft mod.\r\n\r\nEtho's main base of operations on the Season 4 server is EthoCorp Laboratories, a large modern building within spawn which also served as a potion shop. Early in the season, Etho made the Horse Timer to accurately measure how fast a horse is, built the Death Games, a community pvp game, and collaborated with BdoubleO100 to build the nether hub. Etho's second base in the world, known as \"Ethopia\", was a mountain range that Etho flooded, built a tree farm on, and had planned to build various other functional buildings at the location, but was left incomplete. Near spawn, Etho also built Underground Sound, a place players could create their own tunes using a sequence of note blocks.\r\n\r\nOn the Crack the Beast server, Etho built multiple bases throughout the world including a Tinker's base, a main base, a spawner base, and a castle. With the exception of his castle, all his bases were built underground. Due to changes in the mod pack, a map reset was celebrated with a large PvP battle at Etho's castle resulting in its demise.\r\n\r\nOn the CrackPack server, Etho constructed a spawner base using autospawners to benefit his team. Etho started working on a underground tunnel system connecting to the rest of his team's buildings.\r\n\r\nOn the Season 5 map, Etho once again organized the Death Games. Along with SethBling and Nebris, Etho cured some zombie villagers resulting in the maps first villager population. He then proceeded to starting up a Craft Security service in which he \"rescues\" crafting tables that could've been stolen, and hides them somewhere underground. Upon request, he will return the crafting table to the owner.\r\n\r\nOn 16 October 2014, Etho stated on /r/mindcrack that due to his disillusionment with the servers' future, he does not foresee playing on the Mindcrack servers unless a drastic change in direction is taken. On 3 April 2015, it was announced Etho would be leaving Mindcrack as an official member. He explained during a livestream that he was hesitant to sign Mindcrack's trademark agreement and be involved in the expanding business side of the Mindcrack name. He says he remains in good friendship with Zisteau, Docm77, AnderZEL, BdoubleO100 and generikb.\r\n\r\nEtho reappeared on the Mindcrack Server on 21 December 2016 to assist VintageBeef in his prank on PauseUnpause.\r\n\r\nEtho joined the HermitCraft server in March 2015, at the same time as Docm77, following extended inactivity on the Mindcrack Server. He says joined to feel the same sense of community he felt during Season 3 of Mindcrack. Etho is currently known for working on several mini-games on the season 4 map with Docm77.\r\n\r\nEtho's Minecraft skin is Kakashi Hatake from the Japanese manga and anime franchise Naruto. He chose it because it was one of the most detailed skins at the time, even though there are better versions nowadays. In January 2014, Etho updated his skin to utilize new skin customization added in 1.8.\r\n\r\nEtho has a strong dislike of onions.\r\n\r\nSparkling soft drinks cause Etho nose bleeds.\r\n\r\nEtho can turn his feet 180 degrees backwards, and can make a loud popping sound by creating a vacuum in his ear with his pinky.\r\n\r\nEtho was one of the members who had \"op\" status on the Mindcrack Server.\r\n\r\nEtho once had a cat when he was younger that swam out into a pond just to catch a duck for its kittens.\r\n\r\nIt is reported that Etho's cousin is professional Starcraft II player, LiquidSheth, though Etho has not confirmed this.\r\n\r\nEtho's favorite brand of cereal is Mini-Wheats.\r\n\r\nEtho has never gotten a flu shot before.\r\n\r\nHe has had a minecraft update with a reference that says etho slab.\r\n\r\nEtho's favorite music disc is ward"
                ),
                new Hermits
                (
                    "False",
                    "False Symmetry",
                    "Katy",
                    new DateTime(1991, 5, 31),
                    "United Kingdom",
                    new List<string>(new string[] { "falsesymmetry", "truesymmetry" }),
                    new List<string>(new string[] { "https://youtube.com/FalseSymmetry", "https://www.youtube.com/channel/UCpArlUtSgiPGBklMDzwrr2g", "https://www.youtube.com/user/truesymmetry" }),
                    new List<string>(new string[] { "http://www.twitch.tv/FalseSymmetry" }),
                    new List<string>(new string[] { "https://twitter.com/falsesymmetry" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/falsesymmetry" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://instagram.com/falsesymmetry" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "falsesymmetry@outlook.com" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/falsesymmetry" }),
                    new List<string>(new string[] { "http://truesymmetry.deviantart.com/", "http://falsesymmetry.tumblr.com/" }),
                    "FalseSymmetry, known simply as False, is a British Let's Play commentator and a member of HermitCraft since June 2014, having been invited to join by Xisuma.\r\n\r\nFalse has a degree in graphic design.\r\n\r\nFalse made her first public appearance in a HermitCraft episode on 28 March 2015, prior to attending MineCon 2015.\r\n\r\nFalse was invited to join HermitCraft by Xisuma, at the same time as zueljin. Her first video from the server was released on 21 June 2014."
                ),
                new Hermits
                (
                    "Hypno",
                    "Hypnotizd, Hypno",
                    "",
                    new DateTime(1981, 7, 3),
                    "United States",
                    new List<string>(new string[] { "hypnotizd" }),
                    new List<string>(new string[] { "https://youtube.com/hypnotizd" }),
                    new List<string>(new string[] { "http://www.twitch.tv/hypnotizd" }),
                    new List<string>(new string[] { "https://twitter.com/hypnotizd_" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/hypnotizd" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.facebook.com/HypnotizdGaming" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "hypnotizdbusiness@gmail.com" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/hypnotizd" }),
                    new List<string>(new string[] { "https://plus.google.com/u/0/+hypnotizd/" }),
                    "Hypnotizd, or simply Hypno, is a Let's Play commentator and an active member of the HermitCraft server. He is one of the founding members of the server. He also runs the HermitCraft Feed the Beast server, of which he is funding under generikb's permission.\r\n\r\nOn the HermitCraft 1.0 map, Hypno constructed a quad hostile mob farm consisting of two zombie and two spider spawners at the start of the map. This farm was used by many members of the server until he built the Enderman farm. Soon afterwards, Hypno moved thousands of blocks away to a jungle on another continent where he built a temple made of stone bricks and grass."
                ),
                new Hermits
                (
                    "iJevin",
                    "i Jevin, Jevin",
                    "",
                    new DateTime(1, 1, 1), // new DateTime(1987, 12), ???...
                    "United States",
                    new List<string>(new string[] { "iJevin" }),
                    new List<string>(new string[] { "https://youtube.com/ijevin" }),
                    new List<string>(new string[] { "http://www.twitch.tv/ijevin" }),
                    new List<string>(new string[] { "https://twitter.com/ijevin" }),
                    new List<string>(new string[] { "https://discord.gg/fPb5MHX" }),
                    new List<string>(new string[] { "https://www.patreon.com/iJevin" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "mc.jevination@gmail.com" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://plus.google.com/u/0/117296085286820311352", "http://ijevin.spreadshirt.com/" }),
                    "iJevin is a Let's Play commentator and an active member of the HermitCraft server. He joined in December 2013. He was invited to the server by hypnotizd, KingDaddyDMAC, and Jessassin when they met at MineCon 2013. His former Minecraft username was iJevinYT.\r\n\r\n"
                ),
                new Hermits
                (
                    "Impulse",
                    "Impulse SV, impulse",
                    "",
                    new DateTime(1, 1, 1),
                    "United States",
                    new List<string>(new string[] { "impulseSV" }),
                    new List<string>(new string[] { "https://www.youtube.com/impulseSV" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://twitter.com/impulseSV" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/impulseSV" }),
                    new List<string>(new string[] { "https://www.mixer.com/impulseSV" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/impulseSV" }),
                    new List<string>(new string[] { "https://beam.pro/impulseSV" }),
                    "impulseSV is a Let's Play commentator and an active member of the HermitCraft server since January 2015. Before joining the HermitCraft server, Impulse participated as a guest in HermitCraft Ultra Hardcore Season 6.\r\n\r\nImpulse is a former active member of TangoTek's now defunct Moonlight Server, from its creation in December 2013, to the server's closure in July 2014. Impulse is also a former member of JL2579's ZipKrowd server, having joined in July 2013.\r\n\r\nImpulse received a Cape for attending MineCon 2013.\r\n\r\n"
                ),
                new Hermits
                (
                    "Iskall",
                    "Iskall 85",
                    "",
                    new DateTime(1, 1, 1),
                    "Sweden",
                    new List<string>(new string[] { "iskall85" }),
                    new List<string>(new string[] { "https://youtube.com/Iskall85" }),
                    new List<string>(new string[] { "http://www.twitch.tv/iskall85" }),
                    new List<string>(new string[] { "https://twitter.com/iskall85" }),
                    new List<string>(new string[] { "https://discord.gg/iskall85" }),
                    new List<string>(new string[] { "https://www.patreon.com/iskall85" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "iskall85@gmail.com" }),
                    new List<string>(new string[] { "" }),
                    "iskall85 is an active member of the HermitCraft server since February 2016.\r\n\r\nIskall was a professional Quake player before Minecraft, which is how he got his name (\"Iskall\" means \"ice-cold\" in Swedish and is used to refer to someone with a steady nerve). He was part of Notch's original test group for Minecraft.\r\n\r\nIskall began his LP career on a server between him and his friends. His first popular video was a wither skeleton farm, which was, in fact, the first video to cover that subject. He eventually started a modded series that grew his fanbase and lasted for over 100 episodes. He and PythonGB met in a Skype group for a Let's Play of Agrarian Skies, eventually starting a vanilla server called KingdomCraft, which grew to include Welsknight, Rendog, Cubfan135, and GoodTimesWithScar.\r\n\r\nDuring the launch of HermitCraft Season 4, Python invited the five members of KingdomCraft to join HermitCraft, including Iskall."
                ),
                new Hermits
                (
                    "Jessassin",
                    "Jessassin, TheJessassin, Jesse",
                    "",
                    new DateTime(1, 1, 1),
                    "United States",
                    new List<string>(new string[] { "Jessassin" }),
                    new List<string>(new string[] { "https://youtube.com/thejessassin" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://twitter.com/jessassin" }),
                    new List<string>(new string[] { "https://discord.gg/jessassin" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.mixer.com/Jessassin" }),
                    new List<string>(new string[] { "https://www.facebook.com/Jessassin" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "http://jessassin.net/" }),
                    new List<string>(new string[] { "jessassin@gmail.com" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/Jessassin" }),
                    new List<string>(new string[] { "http://steamcommunity.com/id/Jessassin" }),
                    "Jessassin, also known by his YouTube name TheJessassin or his personal name Jesse, is a Let's Play commentator and an active member of the HermitCraft server.\r\n\r\nIn the first map, Jessassin built his entire base underwater. Unfortunately due to lack of time and the added difficulty of building underwater, his base was never fully finished. Additionally, his large project to build a large dome under water had all of the water removed but had little contents inside before the map was reset. In Hypno's Hermitcraft Feed the Beast server, he built the spawn building."
                ),
                new Hermits
                (
                    "JoeHills",
                    "Joe Hills Says, Joe Hills, JoeHillsTSD, Joe",
                    "",
                    new DateTime(1, 1, 1),
                    "",
                    new List<string>(new string[] { "joehillssays" }),
                    new List<string>(new string[] { "https://youtube.com/joehillstsd" }),
                    new List<string>(new string[] { "http://www.twitch.tv/joehills" }),
                    new List<string>(new string[] { "https://twitter.com/joehills" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/joehills" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.facebook.com/JoeHillsTSD" }),
                    new List<string>(new string[] { "https://instagram.com/joehillstsd" }),
                    new List<string>(new string[] { "http://teamsnowday.com/" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/joehills" }),
                    new List<string>(new string[] { "https://plus.google.com/+JoeHillsTSD", "https://plus.google.com/+JoeHillsSays", "https://beam.pro/joehills" }),
                    "joehillssays, also known as JoeHillsTSD, or simply his personal name Joe Hills, is an American Let's Play commentator and a member of HermitCraft since May 2012. Joe Hills is part of Team Snow Day (TSD), \"a small group of folks who make funny things. [They] created goofy tabletop RPG Pitfalls and Penguins, and the comic Jack of All Blades\". He lives in Nashville, Tennessee.\r\n\r\nJoe had reportedly asked Guude if he could join the Mindcrack Server at MineCon 2011, but the group did not unanimously vote for it. Guude later mentioned that Joe more-or-less lost the position to mcgamer, who had also asked to join around the same time. Joe developed the CrackCam website, mindcrackcam.com, which was published on 16 August 2015.\r\n\r\nJoe joined the HermitCraft server in May 2012, having been invited by generikb.\r\n\r\nAfter the June 2013 map reset, Joe claimed a mountain top near town as his home. On it, he built a mausoleum, based on the Mausoleum at Halicarnassus, with the central tomb to Biffa's Bowl. He invited his friend ZombieCleo to join the server for the map reset.\r\n\r\nJoe and his wife Marion's first child, Corrinne Amelia Hills, was born on 1 February 2013, weighing 7lb13oz.\r\n\r\n"
                ),
                new Hermits
                (
                    "Keralis",
                    "Keralis 1, Keralis",
                    "",
                    new DateTime(1, 1, 1),
                    "",
                    new List<string>(new string[] { "Keralis1", "Mrs_Keralis" }),
                    new List<string>(new string[] { "https://youtube.com/keralis" }),
                    new List<string>(new string[] { "http://www.twitch.tv/keralis" }),
                    new List<string>(new string[] { "https://twitter.com/worldofkeralis", "https://twitter.com/Mrs_Keralis" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://instagram.com/iamkeralis", "https://instagram.com/mrs_keralis" }),
                    new List<string>(new string[] { "http://www.keralis.net/" }),
                    new List<string>(new string[] { "keralis@keralis.net" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "http://keralis.spreadshirt.com/" }),
                    "Keralis1, or simply Keralis, is a Polish-Swedish Let's Play commentator and a member of HermitCraft since May 2012. Keralis is well-known for his natural talent for building in Minecraft; he has his own server called World of Keralis, where people can apply and build to the best of their ability.\r\n\r\nBorn in Kielce, Poland, Keralis moved to Sweden at age 5. He currently lives in Ystad. Keralis' username is based on his first and last name. He first used it in World of Warcraft.\r\n\r\nBefore taking up YouTube full time on 1 March 2013, Keralis worked in logistics at a trucking company, organizing trucks travelling across Central Europe. He has mentioned previously working at DHL.\r\n\r\nKeralis started his YouTube channel when he wanted to make a video to showcase his build, a wooden hotel, on Planet Minecraft. The video gained an immense number of views, which made him realize that YouTube can be something for him. He started making videos of him building houses and it took off since.\r\n\r\nIn addition to the many humorous phrases Keralis often says, he also says certain Minecraft terminologies in a different way.\r\n\r\nKeralis joined HermitCraft in May 2012, having been invited to join by generikb. Keralis' wife first joined the server in December 2012 as Mrs_Keralis. Her account has since been used as a camera account.\r\n\r\nOn HermitCraft 2.0, Keralis built a home at town which was based on a design from his previous \"Let's Build\" video. The house often received praise from members of the server who viewed it. He collaborated with Xisuma in building a huge Asian-themed temple.\r\n\r\nKeralis settled on an extreme hills biome to the north of topmass. There he built a campsite and was building a tourist attraction called Boobie Mountain Resort, named after a nearby hill that resembles a boob. The resort featured lodges, tents, an RV park, a pool shaped like boobs, tree houses, a playground, his personal modern home, and an airfield. The modern home was also based on a previous Let's Build, a 24x24 modern house built in creative. Some structures built at Boobie Mountain like the treehouses have made later been featured in his Let's Build series.\r\n\r\nIn the 1.7 Minecraft update, Keralis traveled to a mesa biome with the help of viewers during a Twitch livestream. There he established the Boobie Outpost, an extension to his resort with an Old West theme, and built a saloon, a bank, and a church.\r\n\r\nKeralis is left handed. In December 2014, Keralis obtained a Swedish passport for the first time. As of February 2018, he still holds a Polish passport.\r\n\r\nTogether with his wife, whom he refers to as \"wifey\", Keralis has two sons. His first son, Damian, was born on 9 June 2013, and his second was born on 15 October 2015. Damian holds only an English passport, through his mother's nationality.\r\n\r\nIn April 2014, Keralis described the far-right Swedish Democrats political party as \"idiots\". After the party gained 29 seats in the 2014 Swedish general election, Keralis commented on their anti-immigration policy, tweeting \"Bad election, the fascists are moving forward. Time for me to abandon Sweden but with abandon I mean get deported. Anyone got a good country?\""
                ),
                new Hermits
                (
                    "Mumbo",
                    "Mumbo Jumbo, Mumbo, Mumbo Jumbo, Bumbo",
                    "Oli",
                    new DateTime(1995, 12, 1),
                    "United Kingdom",
                    new List<string>(new string[] { "Mumbo", "MumboJumbo" }),
                    new List<string>(new string[] { "https://youtube.com/ThatMumboJumbo" }),
                    new List<string>(new string[] { "http://www.twitch.tv/thatmumbojumbo" }),
                    new List<string>(new string[] { "https://twitter.com/ThatMumboJumbo" }),
                    new List<string>(new string[] { "https://discord.gg/mumbojumbo" }),
                    new List<string>(new string[] { "https://www.patreon.com/thatmumbojumbo" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://instagram.com/officialmumbo" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "mumbojumbolio@gmail.com" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/ThatMumboJumbo" }),
                    "Mumbo, also known as ThatMumboJumbo, simply MumboJumbo, or his personal name Oli,[2] is an active member of the HermitCraft server since June 2013. Mumbo changed his Minecraft name from MrMumbo in February 2015.\r\n\r\nIn addition to the usual Let's Plays, Mumbo also has several tutorial and demonstration videos on machines built in Minecraft. He is notable for the recurring theme of placing Boats and Hoes, based on the music video \"Boats N' Hoes\" from Step Brothers, in the chests of many of his tutorials. There is no specific series for many of his tutorials and demonstrations.\r\n\r\nOn Hermitcraft 2, Mumbo designed the town square at the center of town. It is a giant circular bowl depressed into the ground. He built a temporary Iron Golem farm high in the sky above spawn and has since created what is known as the Iron Foundry, a design by Tango Tek, that produces over 1700 iron per hour. He cleared out a jungle and built a hostile mob farm over it. The farm uses giant dark platforms with water periodically flushing them out. The mobs will die of fall damage over a hopper system. This was actually causing quite a bit of lag, because it was loading in and out so many mobs, so he re-designed it to be turned on and off using a wireless redstone signal, activated from his AFK platform.\r\n\r\nMumbo has also opened a shop called the Redstone Consultancy, where he designs and builds redstone contraptions for other Hermitcraft members. Some projects include a door for ZombieCleo, an item elevator for Biffa, an elevator in monkeyfarm's giant skull, a design for a Wither Skeleton Farm with Xisuma, and an elevator for iJevin. A running gag of the Consultancy is Mumbo always messes up on the first try, and has to modify or rebuild the contraption. His prices are very steep, but he does usually supply all of the materials.\r\n\r\nMumbo's general schedule for releasing HermitCraft episodes are full episodes in the weekends that are like visual diaries on the server, and bonus episodes midweek where he talks to his viewers."
                ),
                new Hermits
                (
                    "PythonGB",
                    "Python GB, Python, Craig, Pythonator",
                    "",
                    new DateTime(1, 1, 1),
                    "United Kingdom",
                    new List<string>(new string[] { "PythonGB" }),
                    new List<string>(new string[] { "https://youtube.com/user/PythonGB" }),
                    new List<string>(new string[] { "http://www.twitch.tv/PythonGB" }),
                    new List<string>(new string[] { "https://twitter.com/PythonGB" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/PythonGB" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "pythongb@gmail.com" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/PythonGB" }),
                    "PythonGB is an active member of the HermitCraft server since July 2015 having being invited by Xisuma. Prior to joining HermitCraft, Python participated as a guest in HermitCraft Ultra Hardcore Season 8 having been invited by xBCrafted.\r\n\r\nPythonGB's skin is based of a red Creeper in a suit.\r\n\r\n"
                ),
                new Hermits
                (
                    "ReNDoG",
                    "Ren The Dog, ReNDoG, Ren",
                    "",
                    new DateTime(1, 1, 1),
                    "United Kingdom (but he's South African)",
                    new List<string>(new string[] { "Renthedog" }),
                    new List<string>(new string[] { "https://youtube.com/rendog" }),
                    new List<string>(new string[] { "http://www.twitch.tv/rendogtv" }),
                    new List<string>(new string[] { "https://twitter.com/renthedog" }),
                    new List<string>(new string[] { "https://discord.gg/rendog" }),
                    new List<string>(new string[] { "https://www.patreon.com/rendog" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "rendog.official@gmail.com" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/rendog" }),
                    "Renthedog, also known as ReNDoG, is an active member of the HermitCraft server since February 2016.\r\n\r\nRen was born in Bloemfontein, South Africa. He is of Afrikaner origin (white South African of Dutch descent). He moved to England in approximately 2006.\r\n\r\n"
                ),
                new Hermits
                (
                    "Scar",
                    "Good Times With Scar",
                    "",
                    new DateTime(1, 1, 1),
                    "United States",
                    new List<string>(new string[] { "GoodTimeWithScar" }),
                    new List<string>(new string[] { "https://youtube.com/goodtimeswithscar" }),
                    new List<string>(new string[] { "http://www.twitch.tv/goodtimeswithscar" }),
                    new List<string>(new string[] { "https://twitter.com/GTWScar" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "gtwsbusiness@gmail.com" }),
                    new List<string>(new string[] { "" }),
                    "GoodTimeWithScar is an active member of the HermitCraft server since February 2016."
                ),
                new Hermits
                (
                    "Stress",
                    "Stress Monster 101",
                    "Natalie",
                    new DateTime(1, 1, 1),
                    "United Kingdom",
                    new List<string>(new string[] { "Stressmonster101" }),
                    new List<string>(new string[] { "https://youtube.com/stressmonster101" }),
                    new List<string>(new string[] { "http://www.twitch.tv/stressmonster" }),
                    new List<string>(new string[] { "https://twitter.com/Stressmonsterin" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/Stressmonster101" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://instagram.com/stressmonsterin" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "stressmonster101@gmail.com" }),
                    new List<string>(new string[] { "" }),
                    "Stressmonster101, also known as Stress or by her personal name Natalie, is an active member of the HermitCraft server since April 2017."
                ),
                new Hermits
                (
                    "TangoTek",
                    "Tango Tek",
                    "",
                    new DateTime(1, 1, 1),
                    "United States",
                    new List<string>(new string[] { "Tango", "HermitCam" }),
                    new List<string>(new string[] { "https://youtube.com/TangoTekLP" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://twitter.com/TangoTekLP" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/tangotek" }),
                    new List<string>(new string[] { "https://www.mixer.com/tangotek" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "tangotek1@gmail.com" }),
                    new List<string>(new string[] { "" }),
                    "TangoTek, also known simply as Tango, is a former member of the ZipKrowd server and an active member of the HermitCraft server. He joined HermitCraft as a guest on November 2013 primarily to help MrMumbo build his \"Iron Foundry\", an iron farm designed by Tango. He officially joined the server on July 2014."
                ),
                new Hermits
                (
                    "Tinfoilchef",
                    "Tinfoil Chef, TFC, Selif1",
                    "",
                    new DateTime(1959, 6, 29),
                    "United States",
                    new List<string>(new string[] { "Tinfoilchef" }),
                    new List<string>(new string[] { "https://youtube.com/selif1", "https://www.youtube.com/user/TinFoilChefDotCom" }),
                    new List<string>(new string[] { "http://www.twitch.tv/tinfoilchef" }),
                    new List<string>(new string[] { "https://twitter.com/tinfoilchef" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/tinfoilchef" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "http://tinfoilchef.com/", "http://dilithiumcrystalworks.com/" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://plus.google.com/u/0/111143431020143287164/posts", "https://plus.google.com/u/0/+TinfoilChef/posts" }),
                    "Tinfoilchef is an active member of the HermitCraft server. He initially joined the HermitCraft Feed the Beast server but in October 2013 all FTB members were allowed onto the main HermitCraft server.\r\n\r\nOn HermitCraft 2, when TFC joined the vanilla server, he traveled out of town in the direction of Xisuma's wastelands and built a temporary base there. After the new town was settled, he traveled to the town and built a giant glass mushroom tower.\r\n\r\nOn HermitCraft 3, he built a giant green hairy floating eyeball in Hermit Hills.\r\n\r\nOn HermitCraft 4, he built a House of Cards on the edge of the blue district. Then he transformed this House into a large Diamond shaped Diamond made out of blue stained glass, and several blocks of Diamonds."
                ),
                new Hermits
                (
                    "VintageBeef",
                    "Vintage Beef, Beef, Vintage, Dan",
                    "Dan",
                    new DateTime(1981, 8, 26),
                    "",
                    new List<string>(new string[] { "VintageBeef" }),
                    new List<string>(new string[] { "https://youtube.com/vintagebeef" }),
                    new List<string>(new string[] { "http://www.twitch.tv/vintagebeef" }),
                    new List<string>(new string[] { "https://twitter.com/vintagebeeflp" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/vintagebeef" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.facebook.com/VintageBeef" }),
                    new List<string>(new string[] { "https://instagram.com/vintagebeefyt" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "vbeef.business@gmail.com" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/VintageBeef" }),
                    new List<string>(new string[] { "http://vintagebeef.spreadshirt.com/", "", "", "", "", "" }),
                    "VintageBeef, also known by his personal name Daniel \"Dan\" M., is a Portuguese Canadian Let's Play commentator, a member of Mindcrack since May 2011, and a member of HermitCraft since September 2016. He joined Mindcrack after placing first in the second contest, though Guude later said he would've invited him to join regardless of the outcome.\r\n\r\nVintageBeef was born and raised in Toronto, Ontario and currently lives north of Toronto. Beef's parents come from Lisbon, Portugal, and spoke Portuguese at home.\r\n\r\nBeef attended \"Portuguese school\" until he was 12, in addition to his regular schooling. Beef has a sister 5.5 years younger than himself. Beef attended the University of Windsor, but transferred to the University of Toronto after one year. Beef graduated with a degree in sociology, which he says he has put to zero use.\r\n\r\nPrior to his YouTube career, Beef was a used-car photographer - he has mentioned previously working at AutoTrader. In his spare time, he still does photography and, as of the late, mainly macro. During his late-teens, Beef was working as a butcher at Loblaws, which was the inspiration for the handle \"VintageBeef\".\r\n\r\nBefore he started uploading videos on his current channel, Beef used the YouTube account \"thebigmandan2\". Using that account, Beef was an early subscriber of Guude and AnderZEL. Beef says Guude and Anderz was the reason he began playing Minecraft, also saying he began uploading YouTube videos because he felt that there was no point building something amazing to have no one else see it.\r\n\r\nUpon the announcement of the second contest for entry into the Mindcrack Server, Beef submitted a video showing off a museum of various Mindcrack references and builds.\r\n\r\nBeef's first public appearance was at Orlando, Florida in November 2013 for MineCon 2013. Before attending MineCon, Beef joked that his \"manly good looks\" were comparable to \"a combination of Brad Pitt, George Clooney and Antonio Banderas\". In June 2014, Beef mentioned he still receives requests to make his first public appearance from some viewers, citing his lack of vlogs as an explanation. In August 2014, Beef was featured in an article by Tony Dreier, who noted \"[Beef] doesn't crave the spotlight\" and \"... doesn't show himself at all\". Beef has since made his first vlog, to celebrate hitting 1,000,000 YouTube subscribers.\r\n\r\nBeef was initiated into the Mindcrack Prank Wars by davmandave and Kennedyzak when they built an assortment of noise machines under his house. Since then, Beef has actively set up pranks against other players and retaliated against ones set up for him. Team Canada was formed through the Prank Wars, when VintageBeef and Etho helped PauseUnpause set up a prank on Guude.\r\n\r\nOn the post Beta 1.8 map, VintageBeef's two main projects were his castle, a replica of Hunyad Castle, a near six-hundred year old castle in Hunedoara, Romania, and his Jungle Village. The jungle island was discovered by Nebris but used by Beef because he wanted to add more variety to his builds. While the server was using the 1.6 snapshots, Beef and Guude built a western themed town.\r\n\r\nOn the Feed the Beast server, VintageBeef was present for the unveiling, along with the rest of Team Nancy Drew. He planned to build a cannery for zombie flesh, but after a map reset (and a subsequent start of a new season), he decided instead to build a zoo that was planned to eventually house animals and mobs that Feed the Beast introduced. He eventually went back to the cannery as a side project.\r\n\r\nVintageBeef's first project on the Season 4 map is a massive sea fortress nicknamed the Lilly pad. Other projects included a steampunk village at an extreme hills biome and an evil castle on an island. VintageBeef built a variety of \"pads\" around his steampunk village as he did whilst working on the Lilly Pad including a sheep pad and a futuristic chicken pad. Beef made a giant hot air balloon to connect his nether portal too.\r\n\r\nAt the beginning of season 5, Beef once again was specific in choosing which type of block he broke first.\r\n\r\nBeef is a fan of the Montreal Canadiens in the NHL. His favorite animal is a bear. He is 6'2.\r\n\r\nBeef is the owner of two dogs; an English Bulldog named Bubba since June 2013, and a French Bulldog named Daisy since 17 August 2015. Prior to owning Bubba, Beef's first dog was a female Pit bull named Angel, and his second was a male Rottweiler named Bailey.\r\n\r\nBeef was previously in a relationship with Tawnee Blakely (known on Twitter as @tawnchayy). He had mentioned on 3 August 2014 that the two were dating, having previously interacted on Twitter as early as 23 July. She featured in his ALS ice bucket challenge video on 19 August. Without specifically naming Tawnee, Beef said in a video on 4 November 2014, \"I had been seeing this girl for a little while, and I wont go into detail, but she kinda broke the old ticker a few times. I kept coming back for more because I'm a glutton for punishment apparently\", concluding \"the good news is that it's all over now\".\r\n\r\nBeef was in a relationship with model and makeup artist Sara Tyler Tyson, since 14 November 2014. Known in Minecraft as Saratylerr, Sara has featured regularly in Beef's Pocket Pixels Pixelmon series - her first appearance being in episode 12 on 24 October 2015. While Beef was ill, Sara recorded a special unnumbered episode of Pixelmon by herself, which was released on 14 January 2016. A popular request from viewers was for Sara to began her own Let's Play. Her channel, Sarzillaxo, was created on 19 January. Beef made mentioned of this on 28 January, boosting the channel to approximately 2,000 subscribers overnight. The pair broke up in May 2017.\r\n\r\nAlthough he is a fan of The Apprentice, Beef says he would not support Donald Trump in his 2016 presidential campaign. He says Bernie Sanders is the candidate he associates with most.\r\n\r\nVintageBeef's skin was made by Blae000 in Beef's skin contest. Although he attended MineCon 2013 and MineCon 2015, Beef did not redeem his capes. He did, however, redeem his cape for attending MineCon 2016.\r\n\r\nVintageBeef's earliest known Minecraft skin is The Stig from British TV show Top Gear. Beef also briefly used a Montreal Canadiens skin.\r\n\r\nDuring his time on the pre-Beta 1.8 server, VintageBeef was referred to a number of times as \"ModernChicken\" by adlingtont and as \"steakdude\" by ShreeyamGFX. These nicknames have since fallen out of use."
                ),
                new Hermits
                (
                    "Welsknight",
                    "Wels Knight",
                    "",
                    new DateTime(1, 1, 1),
                    "United States",
                    new List<string>(new string[] { "Welsknight" }),
                    new List<string>(new string[] { "https://youtube.com/welsknightgaming" }),
                    new List<string>(new string[] { "http://www.twitch.tv/welsknight" }),
                    new List<string>(new string[] { "https://twitter.com/welsknightplays" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/WelsknightGaming" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "welsknightgaming.business@gmail.com" }),
                    new List<string>(new string[] { "" }),
                    "Welsknight is an active member of the HermitCraft server since February 2016."
                ),
                new Hermits
                (
                    "xBCrafted",
                    "xB Crafted, xB",
                    "",
                    new DateTime(1, 1, 1),
                    "United States",
                    new List<string>(new string[] { "xBCrafted", "xBCam" }),
                    new List<string>(new string[] { "https://youtube.com/xbxaxcx" }),
                    new List<string>(new string[] { "http://www.twitch.tv/xbcrafted" }),
                    new List<string>(new string[] { "https://twitter.com/xBCrafted" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/xBCrafted" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "xBCrafted@Gmail.com" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/xBCrafted" }),
                    "xBCrafted is an active member of the HermitCraft server. He joined the server in August 2014 after being invited by topmass. He uploaded his first episode on the vanilla server on 10 September 2014.\r\n\r\nxB had been previously been suggested to join HermitCraft by Jessassin, but he was not accepted by the group. He joined the HermitCraft server in August 2014.\r\n\r\nxB's Minecraft skin was originally based on Gregory House from House.\r\n\r\n"
                ),
                new Hermits
                (
                    "Xisuma",
                    "Xisuma Void, Xisumavoid, X",
                    "",
                    new DateTime(1, 1, 1),
                    "United Kingdom",
                    new List<string>(new string[] { "Xisuma", "xisumavoid", "EvilXisuma" }),
                    new List<string>(new string[] { "https://youtube.com/xisumavoid", "https://www.youtube.com/channel/UCL5W9kuKIQtXEVAxSadB2BQ" }),
                    new List<string>(new string[] { "http://www.twitch.tv/xisuma" }),
                    new List<string>(new string[] { "https://twitter.com/xisumavoid" }),
                    new List<string>(new string[] { "https://discord.gg/xisuma" }),
                    new List<string>(new string[] { "https://www.patreon.com/Xisuma" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "xisumavoid.com" }),
                    new List<string>(new string[] { "xvpress@gmail.com" }),
                    new List<string>(new string[] { "http://www.reddit.com/user/Xisuma" }),
                    "Xisuma (pronounced \"ɪ suː mə\"), also known as xisumavoid, is a Let's Play commentator and an active member of the HermitCraft server. He is the founding member of the Agency and developer of many popular maps, including Rush, Cube Control, and Gold Rush. He is the co-founder of Respawn Network server with Docm77.\r\n\r\nXisuma prefers to stay anonymous online, and has not shared many personal details. But we know he has a beard after saying on his stream on the platform twitch.tv.\r\n\r\nIn June, Xisuma also started uploading FTL: Faster than Light Let's Plays, getting through multiple playthroughs of the game. He ended after failing to defeat the FTL: Advanced Edition flagship in August, in an episode aptly named 'FTL: Advanced Edition 13 Unlucky Thirteen'.\r\n\r\nOn Hermitcraft II, Xisuma's first home after the map reset was an adobe. Underneath the adobe is a huge ravine system. He slabbed the entire ravine because he likes the fog effect in the ravine during certain times of day. He had planned to set up a base there but light sources ruined the look of the ravine. Xisuma now lives in a custom desert temple, sometimes called the \"Jungle Temple\", in the Xisuma-made biome, \"The Wastelands\". He has started to build his main base underground in \"The Wastelands\". In the new 1.7 Hermit village, Xisuma built a beautiful modern house originally built by his friend, Keevan (Kevan) on Keralis's creative server. He also built shops around town including the \"Feather Falling Shop\" and the \"Dig 4 Hire\" shop. He has built several community buildings, particularly around the main town, including stackable automated Melon and Pumpkin farms, cow farm, enchantment room, Enderman farm, tree farm, Wither Skeleton farm, Community Smelting Foundry and a slime farm. Xisuma occasionally uses his alternative account xisumavoid as a cameraman and to do maintenance work on the server like loading chunks for the 1.7 update.\r\n\r\nXisuma's skin is based on a character from Doom. It was made by Rabenschild for Xisuma's skin competition. His previous skin was also based on a character from the same game."
                ),
                new Hermits
                (
                    "Zedaph",
                    "Zedaph",
                    "",
                    new DateTime(1, 1, 1),
                    "United Kingdom",
                    new List<string>(new string[] { "Zedaph" }),
                    new List<string>(new string[] { "https://youtube.com/zedaphplays" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://twitter.com/ZedaphPlays" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "https://www.patreon.com/zedaphplays" }),
                    new List<string>(new string[] { "https://www.mixer.com/zedaphplays" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "" }),
                    new List<string>(new string[] { "zedaph@gmail.com" }),
                    new List<string>(new string[] { "" }),
                    "Zedaph is an active member of the HermitCraft server since April 2017."
                )
            };
        }
    }
}
